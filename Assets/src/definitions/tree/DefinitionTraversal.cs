using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.src.definitions.tree
{
    public static class DefinitionTraversal
    {
        public static void Traverse(DefinitionNode node, Action<DefinitionNode> action)
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
    }
}
