using System.Linq;
using ExpressionTreeViewer;
using Microsoft.VisualStudio.DebuggerVisualizers;

namespace TestApp
{
	class Program
	{
		static void Main()
		{
			var languages = new[] { "C#", "J#", "VB", "Delphi", "F#", "COBOL", "Python" };
			var a = languages.AsQueryable().Where(l => l.EndsWith("#") && l != "j#")
				.Take(3).Select(l => new { Name = l, IsSelected = true, Size = l.Length });
			var b = languages.AsQueryable().Select(item => item);
			var queryable = a.Join(b, outer => outer.Name, inner => inner, (x, y) => new { x = x, y = y }).OrderBy(item => item.x.Size);
			new VisualizerDevelopmentHost(queryable.Expression, typeof(ExpressionTreeVisualizer), typeof(ExpressionTreeObjectSource)).ShowVisualizer();
		}
	}
}
