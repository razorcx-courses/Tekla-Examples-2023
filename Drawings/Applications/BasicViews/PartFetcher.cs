using System.Collections;
using Tekla.Structures.Model;

namespace BasicViews
{
    public class PartFetcher
    {
        public ArrayList FetchParts(ModelObjectEnumerator selectedModelObjects)
        {
            var parts = new ArrayList();

            switch (selectedModelObjects.Current)
            {
                case Part _:
                    parts.Add(selectedModelObjects.Current.Identifier);
                    break;
                case Assembly assembly:
                    parts = GetAssemblyParts(assembly);
                    break;
                case BaseComponent component:
                    parts = GetComponentParts(component);
                    break;
                case Task task:
                    parts = GetTaskParts(task);
                    break;
            }

            return parts;
        }

        #region Model object child part fetching
        /// <summary>
        /// Gets list of assembly parts 
        /// </summary>
        /// <param name="SelectedModelObjects"></param>
        /// <returns></returns>
        private static ArrayList GetAssemblyParts(Assembly assembly)
        {
            var parts = new ArrayList();
            var assemblyChildren = (assembly).GetSecondaries().GetEnumerator();

            parts.Add((assembly).GetMainPart().Identifier);

            while (assemblyChildren.MoveNext())
                parts.Add(((ModelObject)assemblyChildren.Current)?.Identifier);

            return parts;
        }

        /// <summary>
        /// Gets list of component parts
        /// </summary>
        /// <param name="SelectedModelObjects"></param>
        /// <returns></returns>
        private static ArrayList GetComponentParts(BaseComponent component)
        {
            var parts = new ArrayList();
            IEnumerator myChildren = component.GetChildren();

            while (myChildren.MoveNext())
                parts.Add(((ModelObject)myChildren.Current)?.Identifier);

            return parts;
        }

        /// <summary>
        /// Gets list of task parts
        /// </summary>
        /// <param name="TaskMembers"></param>
        /// <returns></returns>
        private static ArrayList GetTaskParts(Task task)
        {
            var parts = new ArrayList();

            var myMembers = task.GetChildren();

            while (myMembers.MoveNext())
            {
                switch (myMembers.Current)
                {
                    case Task current:
                        parts.AddRange(GetTaskParts(current));
                        break;
                    case Part _:
                        parts.Add(myMembers.Current.Identifier);
                        break;
                }
            }

            return parts;
        }

        #endregion
    }
}