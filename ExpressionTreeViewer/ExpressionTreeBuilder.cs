﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace ExpressionTreeViewer
{
	public static class ExpressionTreeBuilder
	{
		public static ExpressionTreeNode GetExpressionTreeNode(Expression expression, string prefix = null)
		{
			ExpressionTreeNode node = null;
			if (expression is BinaryExpression)
			{
				var expr = expression as BinaryExpression;
				node = new ExpressionTreeNode(string.Format("BinaryExpression: [{0}]", expr.NodeType));
				node.Nodes.Add(GetExpressionTreeNode(expr.Left, "Left"));
				node.Nodes.Add(GetExpressionTreeNode(expr.Right, "Right"));
			}
			if (expression is BlockExpression)
			{
				var expr = expression as BlockExpression;
				node = new ExpressionTreeNode(string.Format("BlockExpression Expressions:"));
				expr.Expressions.ToList().ForEach(a => node.Nodes.Add(GetExpressionTreeNode(a)));
			}
			if (expression is ConditionalExpression)
			{
				var expr = expression as ConditionalExpression;
				node = new ExpressionTreeNode(string.Format("ConditionalExpression: [{0}]", expr.Type));
				node.Nodes.Add(GetExpressionTreeNode(expr.Test, "Test"));
				node.Nodes.Add(GetExpressionTreeNode(expr.IfTrue, "IfTrue"));
				node.Nodes.Add(GetExpressionTreeNode(expr.IfFalse, "IfFalse"));
			}
			if (expression is ConstantExpression)
			{
				var expr = expression as ConstantExpression;
				node = new ExpressionTreeNode(string.Format("ConstantExpression [{0}]: {1}", expr.Type, expr.Value));
			}
			if (expression is DebugInfoExpression)
			{
				var expr = expression as DebugInfoExpression;
			}
			if (expression is DefaultExpression)
			{
				var expr = expression as DefaultExpression;
				node = new ExpressionTreeNode(string.Format("DefaultExpression: [{0}]", expr.Type));
			}
			if (expression is DynamicExpression)
			{
				var expr = expression as DynamicExpression;
				node = new ExpressionTreeNode(string.Format("DynamicExpression [{0}] Arguments:", expr.DelegateType));
				expr.Arguments.ToList().ForEach(a => node.Nodes.Add(GetExpressionTreeNode(a)));
			}
			if (expression is GotoExpression)
			{
				var expr = expression as GotoExpression;
			}
			if (expression is IndexExpression)
			{
				var expr = expression as IndexExpression;
				node = new ExpressionTreeNode(string.Format("IndexExpression [{0}] Arguments:", expr.Indexer.Name));
				expr.Arguments.ToList().ForEach(a => node.Nodes.Add(GetExpressionTreeNode(a)));
			}
			if (expression is InvocationExpression)
			{
				var expr = expression as InvocationExpression;
				var argsNode = new ExpressionTreeNode(string.Format("Arguments: {0}", expr.Arguments.Count));

				expr.Arguments.ToList().ForEach(a => argsNode.Nodes.Add(GetExpressionTreeNode(a)));
				node = new ExpressionTreeNode(string.Format("InvocationExpression [{0}]:", expr.Type));
				node.Nodes.Add(GetExpressionTreeNode(expr.Expression, "Expression"));
				node.Nodes.Add(argsNode);
			}
			if (expression is LabelExpression)
			{
				var expr = expression as LabelExpression;
			}
			if (expression is LambdaExpression)
			{
				var expr = expression as LambdaExpression;
				var argsNode = new ExpressionTreeNode("Arguments:");

				expr.Parameters.ToList().ForEach((item) => argsNode.Nodes.Add(GetExpressionTreeNode(item)));
				node = new ExpressionTreeNode(string.Format("LambdaExpression [{0}]:", expr.ReturnType));
				node.Nodes.Add(argsNode);
				node.Nodes.Add(GetExpressionTreeNode(expr.Body, "Body"));
			}
			if (expression is ListInitExpression)
			{
				var expr = expression as ListInitExpression;
			}
			if (expression is LoopExpression)
			{
				var expr = expression as LoopExpression;
			}
			if (expression is MemberExpression)
			{
				var expr = expression as MemberExpression;
				node = new ExpressionTreeNode(string.Format("MemberExpression [{0}]: {1}", expr.Type, expr.Member.Name));
				node.Nodes.Add(GetExpressionTreeNode(expr.Expression, "Expression"));
			}
			if (expression is MemberInitExpression)
			{
				var expr = expression as MemberInitExpression;
				var child = new ExpressionTreeNode(string.Format("Bindings: {0}", expr.Bindings.Count));

				expr.Bindings.ToList().ForEach(binding =>
				{
					if (binding.BindingType == MemberBindingType.Assignment)
					{
						var assigment = (MemberAssignment)binding;
						var child2 = new ExpressionTreeNode(string.Format("{0}: {1}", assigment.BindingType, assigment.Member));
						child.Nodes.Add(child2);
						child2.Nodes.Add(GetExpressionTreeNode(assigment.Expression, "Expression"));
					}
					else
					{
						child.Nodes.Add(new ExpressionTreeNode(binding.BindingType + ": " + binding));
					}
				});
				node = new ExpressionTreeNode(string.Format("MemberInitExpression [{0}]:", expr.Type));
				node.Nodes.Add(GetExpressionTreeNode(expr.NewExpression, "NewExpression"));
				node.Nodes.Add(child);
			}
			if (expression is MethodCallExpression)
			{
				var expr = expression as MethodCallExpression;
				node = new ExpressionTreeNode(string.Format("MethodCallExpression [{0}] Arguments:", expr.Method.Name));
				expr.Arguments.ToList().ForEach(a => node.Nodes.Add(GetExpressionTreeNode(a)));
			}
			if (expression is NewArrayExpression)
			{
				var expr = expression as NewArrayExpression;
			}
			if (expression is NewExpression)
			{
				var expr = expression as NewExpression;
				node = new ExpressionTreeNode(string.Format("NewExpression Arguments:"));

				for (int i = 0; i < expr.Arguments.Count; i++)
				{
					if (expr.Members != null)
					{
						node.Nodes.Add(GetExpressionTreeNode(expr.Arguments[i], expr.Members[i].Name));
					}
					else
					{
						node.Nodes.Add(GetExpressionTreeNode(expr.Arguments[i]));
					}
				}
			}
			if (expression is ParameterExpression)
			{
				var expr = expression as ParameterExpression;
				node = new ExpressionTreeNode(string.Format("ParameterExpression [{0}]: {1}", expr.Type, expr.Name));
			}
			if (expression is RuntimeVariablesExpression)
			{
				var expr = expression as RuntimeVariablesExpression;
			}
			if (expression is SwitchExpression)
			{
				var expr = expression as SwitchExpression;
			}
			if (expression is TryExpression)
			{
				var expr = expression as TryExpression;
			}
			if (expression is TypeBinaryExpression)
			{
				var expr = expression as TypeBinaryExpression;
				node = new ExpressionTreeNode(string.Format("TypeBinaryExpression [{0}] Operand:", expr.TypeOperand));
				node.Nodes.Add(GetExpressionTreeNode(expr.Expression));
			}
			if (expression is UnaryExpression)
			{
				var expr = expression as UnaryExpression;
				node = new ExpressionTreeNode(string.Format("UnaryExpression [{0}] Operand:", expr.NodeType));
				node.Nodes.Add(GetExpressionTreeNode(expr.Operand));
			}

			if (node == null)
			{
				if (expression != null)
				{
					node = new ExpressionTreeNode(string.Format("Unkown Node [{0}-{1}]: {2}", expression.GetType(), expression.NodeType, expression));
				}
				else
				{
					node = new ExpressionTreeNode("NULL");
				}
			}

			if (!string.IsNullOrEmpty(prefix))
			{
				node.Text = string.Format("{0} => {1}", prefix, node.Text);
			}

			node.ExpressionString = expression + string.Empty;
			return node;
		}
	}

	[Serializable]
	public class ExpressionTreeNode
	{
		public ExpressionTreeNode(string text)
		{
			this.Text = text;
			this.Nodes = new List<ExpressionTreeNode>();
		}

		public string Text { get; set; }
		public List<ExpressionTreeNode> Nodes { get; set; }
		public string ExpressionString { get; set; }
	}
}