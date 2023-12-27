using Assets.src.definitions;
using Assets.src.definitions.tree;
using Assets.src.state2;
using System.Collections.Generic;

namespace Assets.src.state2
{
    /// <summary>
    /// Snapshot of all the entities in the universe.
    /// </summary>
    public sealed class GlobalSnapshot
    {
        /// <summary>
        /// Time elapsed since Jan 1st 1950
        /// Redundant to each entity snapshot's time. For robustness only.
        /// </summary>
        public long TimeMs { get; }

        public Dictionary<string, EntitySnapshot> Entities { get; } = new();

        private GlobalSnapshot()
        {
        }

        public static GlobalSnapshot CreateInitial(DefinitionRoot definitionRoot)
        {
            long timeStartMs = 0;

            var stateSnapshot = new GlobalSnapshot();
            var root = definitionRoot.Universe;

            DefinitionTraversal.Traverse(root, (node) =>
            {
                var stateEntity = new EntitySnapshot(node, timeStartMs, null);
                stateSnapshot.Entities.Add(node.Id, stateEntity);
            });

            return stateSnapshot;
        }

        // TODO
        public static GlobalSnapshot Create(DefinitionRoot definitionRoot, long timeMs)
        {
            //TODO
            var snapshot = CreateInitial(definitionRoot);
            return snapshot;
        }
    }
}
