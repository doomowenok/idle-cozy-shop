using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Infrastructure.StateMachine.States;
using UnityEngine;

namespace Infrastructure.StateMachine
{
    public sealed class ApplicationStateMachine : IApplicationStateMachine
    {
        private readonly IDictionary<Type, IExitableState> _states = new Dictionary<Type, IExitableState>(8);
        
        private IExitableState _currentState;
        private bool _changeStateInProgress;

        public void AddState<TState>(TState instance) where TState : class, IExitableState
        {
            _states.Add(typeof(TState), instance);
        } 

        public async UniTask Enter<TState>() where TState : class, IState
        {
            TState state = await ChangeState<TState>();
            await state.Enter();
            _changeStateInProgress = false;
        }

        public async UniTask Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadState<TPayload>
        {
            TState state = await ChangeState<TState>();
            await state.Enter(payload);
            _changeStateInProgress = false;
        }

        private async UniTask<TState> ChangeState<TState>() where TState : class, IExitableState
        {
            if (_changeStateInProgress)
            {
                Debug.LogWarning($"Trying to change state when changing in progress.");
                return null;
            }
            
            _changeStateInProgress = true;
            
            if (_currentState != null)
            {
                await _currentState.Exit();
            }

            TState state = GetState<TState>();
            _currentState = state;
            Debug.Log($"Current state is {typeof(TState).Name}.");

            _changeStateInProgress = false;
            
            return state;
        }

        private TState GetState<TState>() where TState : class, IExitableState => 
            _states[typeof(TState)] as TState;
    }
}