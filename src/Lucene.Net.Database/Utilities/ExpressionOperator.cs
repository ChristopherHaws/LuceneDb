using System;
using System.Linq.Expressions;
using System.Text;

namespace Lucene.Net.Database.Utilities
{
	internal static class ExpressionOperator
	{
		public static string GetPropertyPath<TObj, TRet>(this TObj obj, Expression<Func<TObj, TRet>> expr)
		{
			return GetPropertyPath(expr);
		}

		public static string GetPropertyPath(this Expression expression)
		{
			var path = new StringBuilder();
			var memberExpression = GetMemberExpression(expression);

			do
			{
				if (path.Length > 0)
				{
					path.Insert(0, Constants.PropertySeparator);
				}

				path.Insert(0, memberExpression.Member.Name);
				memberExpression = GetMemberExpression(memberExpression.Expression);
			}
			while (memberExpression != null);

			return path.ToString();
		}

		public static MemberExpression GetMemberExpression(this Expression expression)
		{
			var memberExpression = expression as MemberExpression;
			if (memberExpression != null)
			{
				return memberExpression;
			}

			var lambdaExpression = expression as LambdaExpression;
			if (lambdaExpression == null)
			{
				return null;
			}

			var body = lambdaExpression.Body as MemberExpression;
			if (body != null)
			{
				return body;
			}

			var unaryExpression = lambdaExpression.Body as UnaryExpression;

			return (MemberExpression) unaryExpression?.Operand;
		}
	}
}