using System.Collections.Generic;
using JetBrains.Annotations;
using Scellecs.Morpeh;
using UnityEngine;
using VContainer;

namespace Infrastructure.EntityWorldManager
{
    public sealed class WorldManager : IWorldManager
    {
        private readonly IObjectResolver _context;
        
        private readonly Dictionary<WorldType, EntityWorld> _worlds = new Dictionary<WorldType, EntityWorld>();
        
        private EntityWorld _currentlyModificationWorld;

        public WorldManager(IObjectResolver context)
        {
            _context = context;
        }
        
        public IWorldManager CreateWorld(WorldType type)
        {
            if (!_worlds.TryGetValue(type, out var entityWorld))
            {
                entityWorld = new EntityWorld();
                
                World world = World.Create();
                SystemsGroup group = world.CreateSystemsGroup();
                
                entityWorld.World = world;
                entityWorld.SystemsGroup = group;
                
                entityWorld.World.AddSystemsGroup(0, entityWorld.SystemsGroup);

                _worlds.Add(type, entityWorld);
            }
            
            _currentlyModificationWorld = entityWorld;

            _currentlyModificationWorld.World.UpdateByUnity = false;
            
            return this;
        }
        
        public IWorldManager AddSystem<TSystem>() where TSystem : ISystem, new()
        {
            ISystem system = new TSystem();
            _context.Inject(system);
            
            _currentlyModificationWorld.SystemsGroup.AddSystem(system);
            
            return this;
        }

        public void StartWorld(WorldType type)
        {
            if (_worlds.TryGetValue(type, out var entityWorld))
            {
                entityWorld.World.UpdateByUnity = true;
                Debug.Log($"World started.");
            }
        }

        public void StopWorld(WorldType type)
        {
            if (_worlds.TryGetValue(type, out var entityWorld))
            {
                entityWorld.World.UpdateByUnity = false;
            }
        }
    }
}