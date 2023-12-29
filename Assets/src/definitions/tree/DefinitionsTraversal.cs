#nullable enable

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.src.definitions.tree
{
    public static class DefinitionsTraversal
    {
        public static void Traverse(JsonDefinitionNode node, Action<JsonDefinitionNode> action)
        {
            action(node);
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    Traverse(child, action);
                }
            }
        }

        /// <summary>
        /// Same as Traverse but lets delegate "action" transform each node as it gets traversed
        /// and passes the transformed version to the children. 
        /// </summary>
        public static void Traverse_PassParent<T>(JsonDefinitionNode node, Func<JsonDefinitionNode, T?, T> action, T parent)
        {
            T transformedNode = action(node, parent);
            if (node.Children != null)
            {
                foreach (var child in node.Children)
                {
                    Traverse_PassParent(child, action, transformedNode);
                }
            }
        }
    }
}
