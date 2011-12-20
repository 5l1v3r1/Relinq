// This file is part of the re-linq project (relinq.codeplex.com)
// Copyright (c) rubicon IT GmbH, www.rubicon.eu
// 
// re-linq is free software; you can redistribute it and/or modify it under 
// the terms of the GNU Lesser General Public License as published by the 
// Free Software Foundation; either version 2.1 of the License, 
// or (at your option) any later version.
// 
// re-linq is distributed in the hope that it will be useful, 
// but WITHOUT ANY WARRANTY; without even the implied warranty of 
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the 
// GNU Lesser General Public License for more details.
// 
// You should have received a copy of the GNU Lesser General Public License
// along with re-linq; if not, see http://www.gnu.org/licenses.
// 
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Remotion.Linq.Clauses;
using Remotion.Linq.Clauses.ResultOperators;
using Remotion.Linq.Utilities;

namespace Remotion.Linq.Parsing.Structure.IntermediateModel
{
  /// <summary>
  /// Represents a <see cref="MethodCallExpression"/> for <see cref="Queryable.First{TSource}(System.Linq.IQueryable{TSource})"/>,
  /// <see cref="Queryable.First{TSource}(System.Linq.IQueryable{TSource},System.Linq.Expressions.Expression{System.Func{TSource,bool}})"/>,
  /// <see cref="Queryable.FirstOrDefault{TSource}(System.Linq.IQueryable{TSource})"/> or
  /// <see cref="Queryable.FirstOrDefault{TSource}(System.Linq.IQueryable{TSource},System.Linq.Expressions.Expression{System.Func{TSource,bool}})"/>.
  /// It is generated by <see cref="ExpressionTreeParser"/> when an <see cref="Expression"/> tree is parsed.
  /// When this node is used, it marks the beginning (i.e. the last node) of an <see cref="IExpressionNode"/> chain that represents a query.
  /// </summary>
  public class FirstExpressionNode : ResultOperatorExpressionNodeBase
  {
    public static readonly MethodInfo[] SupportedMethods = new[]
                                                           {
                                                               GetSupportedMethod (() => Queryable.First<object> (null)),
                                                               GetSupportedMethod (() => Queryable.First<object> (null, null)),
                                                               GetSupportedMethod (() => Queryable.FirstOrDefault<object> (null)),
                                                               GetSupportedMethod (() => Queryable.FirstOrDefault<object> (null, null)),
                                                               GetSupportedMethod (() => Enumerable.First<object> (null)),
                                                               GetSupportedMethod (() => Enumerable.First<object> (null, null)),
                                                               GetSupportedMethod (() => Enumerable.FirstOrDefault<object> (null)),
                                                               GetSupportedMethod (() => Enumerable.FirstOrDefault<object> (null, null)),
                                                           };

    public FirstExpressionNode (MethodCallExpressionParseInfo parseInfo, LambdaExpression optionalPredicate)
        : base (parseInfo, optionalPredicate, null)
    {
    }

    public override Expression Resolve (
        ParameterExpression inputParameter, Expression expressionToBeResolved, ClauseGenerationContext clauseGenerationContext)
    {
      ArgumentUtility.CheckNotNull ("inputParameter", inputParameter);
      ArgumentUtility.CheckNotNull ("expressionToBeResolved", expressionToBeResolved);

      // no data streams out from this node, so we cannot resolve any expressions
      throw CreateResolveNotSupportedException();
    }

    protected override ResultOperatorBase CreateResultOperator (QueryModel queryModel, ClauseGenerationContext clauseGenerationContext)
    {
      return new FirstResultOperator (ParsedExpression.Method.Name.EndsWith ("OrDefault"));
    }
  }
}
