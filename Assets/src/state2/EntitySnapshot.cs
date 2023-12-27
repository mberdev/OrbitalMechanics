#nullable enable

using Assets.src.definitions;
using UnityEngine;

namespace Assets.src.state2
{
    /// <summary>
    /// Represents the "instantiation" of a definition node, 
    /// i.e. the state of a celestial body or object at a given instant in a timeline.
    /// </summary>
    public sealed class EntitySnapshot
    {
        // Time elapsed since Jan 1st 1950
        public long TimeMs { get; set; }

        /// <summary>
        /// "Previous == this" means this is the very first snapshot.
        /// </summary>
        public EntitySnapshot Previous { get; }

        // Velocity can be null because some entities are on rails,
        // i.e. we never need to remember their velocity.
        public Vector3? Velocity{ get; set; }

        /// <summary>
        /// Populate only if this snapshot is part of a global one.
        /// </summary>
        public GlobalSnapshot? GlobalSnapshot { get; }

        public DefinitionNode Definition { get; }

        public EntitySnapshot(DefinitionNode definition, long timeMs, EntitySnapshot previous, GlobalSnapshot? globalSnapshot)
        {
            Definition = definition;
            Previous = previous;
            TimeMs = timeMs;
            GlobalSnapshot = globalSnapshot;
        }

        /// <summary>
        /// Very first snapshot (no previous).
        /// </summary>
        public EntitySnapshot(DefinitionNode definition, long timeMs, GlobalSnapshot? globalSnapshot)
        {
            Definition = definition;
            Previous = this;
            TimeMs = timeMs;
            GlobalSnapshot = globalSnapshot;
        }

    }
}
