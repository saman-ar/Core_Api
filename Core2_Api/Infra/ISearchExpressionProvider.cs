using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using Core2_Api.Infra;

namespace Core2_Api.Infra
{
	public interface ISearchExpressionProvider
	{
		ConstantExpression GetValue(string input);
	}
}

public class DefaultSearchExpressionProvider : ISearchExpressionProvider
{
	public virtual ConstantExpression GetValue(string input)
	{
		return Expression.Constant(input);
	}
}

public class StringToIntSearchExpressionProvider : DefaultSearchExpressionProvider
{
	public override ConstantExpression GetValue(string input)
	{
		if (!int.TryParse(input,out var result))
			throw new ArgumentException("Invalid search value");

		return Expression.Constant(result);
	}


}