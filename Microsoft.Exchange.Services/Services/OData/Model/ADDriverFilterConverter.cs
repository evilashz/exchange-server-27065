using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA3 RID: 3747
	internal class ADDriverFilterConverter : ODataFilterConverter
	{
		// Token: 0x06006194 RID: 24980 RVA: 0x001305C4 File Offset: 0x0012E7C4
		public ADDriverFilterConverter(EntitySchema schema) : base(schema)
		{
		}

		// Token: 0x06006195 RID: 24981 RVA: 0x001305D0 File Offset: 0x0012E7D0
		public QueryFilter ConvertFilterClause(FilterClause filterClause)
		{
			ArgumentValidator.ThrowIfNull("filterClause", filterClause);
			return this.ConvertFilterNode(filterClause.Expression);
		}

		// Token: 0x06006196 RID: 24982 RVA: 0x001305F8 File Offset: 0x0012E7F8
		public SortBy ConvertOrderByClause(OrderByClause orderByClause)
		{
			ArgumentValidator.ThrowIfNull("orderByClause", orderByClause);
			if (orderByClause.ThenBy != null)
			{
				throw new InvalidOrderByThenByException();
			}
			DirectoryPropertyProvider propertyProvider = this.GetPropertyProvider(orderByClause.Expression);
			ADPropertyDefinition adpropertyDefinition = propertyProvider.ADPropertyDefinition;
			SortBy result;
			if (orderByClause.Direction == null)
			{
				result = new SortBy(adpropertyDefinition, SortOrder.Ascending);
			}
			else
			{
				result = new SortBy(adpropertyDefinition, SortOrder.Descending);
			}
			return result;
		}

		// Token: 0x06006197 RID: 24983 RVA: 0x00130650 File Offset: 0x0012E850
		private QueryFilter ConvertFilterNode(QueryNode queryNode)
		{
			QueryFilter queryFilter = null;
			if (queryNode.Kind == 5)
			{
				UnaryOperatorNode unaryOperatorNode = (UnaryOperatorNode)queryNode;
				if (unaryOperatorNode.OperatorKind == 1)
				{
					queryFilter = new NotFilter(this.ConvertFilterNode(unaryOperatorNode.Operand));
				}
			}
			else if (queryNode.Kind == 4)
			{
				BinaryOperatorNode binaryOperatorNode = (BinaryOperatorNode)queryNode;
				if (binaryOperatorNode.OperatorKind == 1 || binaryOperatorNode.OperatorKind == null)
				{
					QueryFilter queryFilter2 = this.ConvertFilterNode(binaryOperatorNode.Left);
					QueryFilter queryFilter3 = this.ConvertFilterNode(binaryOperatorNode.Right);
					if (binaryOperatorNode.OperatorKind == 1)
					{
						queryFilter = new AndFilter(new QueryFilter[]
						{
							queryFilter2,
							queryFilter3
						});
					}
					else
					{
						queryFilter = new OrFilter(new QueryFilter[]
						{
							queryFilter2,
							queryFilter3
						});
					}
				}
				else
				{
					DirectoryPropertyProvider propertyProvider = this.GetPropertyProvider(binaryOperatorNode.Left);
					ADPropertyDefinition adpropertyDefinition = propertyProvider.ADPropertyDefinition;
					object constantValue = this.GetConstantValue(binaryOperatorNode.Right, base.GetEntityProperty(binaryOperatorNode.Left));
					switch (binaryOperatorNode.OperatorKind)
					{
					case 2:
						queryFilter = new ComparisonFilter(ComparisonOperator.Equal, adpropertyDefinition, constantValue);
						break;
					case 3:
						queryFilter = new ComparisonFilter(ComparisonOperator.NotEqual, adpropertyDefinition, constantValue);
						break;
					case 4:
						queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThan, adpropertyDefinition, constantValue);
						break;
					case 5:
						queryFilter = new ComparisonFilter(ComparisonOperator.GreaterThanOrEqual, adpropertyDefinition, constantValue);
						break;
					case 6:
						queryFilter = new ComparisonFilter(ComparisonOperator.LessThan, adpropertyDefinition, constantValue);
						break;
					case 7:
						queryFilter = new ComparisonFilter(ComparisonOperator.LessThanOrEqual, adpropertyDefinition, constantValue);
						break;
					}
				}
			}
			else if (queryNode.Kind == 8)
			{
				SingleValueFunctionCallNode singleValueFunctionCallNode = (SingleValueFunctionCallNode)queryNode;
				ODataFilterConverter.BinaryOperandPair binaryOperandPair = base.ParseBinaryFunctionParameters(singleValueFunctionCallNode);
				DirectoryPropertyProvider propertyProvider2 = this.GetPropertyProvider(binaryOperandPair.Left);
				ADPropertyDefinition adpropertyDefinition2 = propertyProvider2.ADPropertyDefinition;
				string text = this.GetConstantValue(binaryOperandPair.Right, base.GetEntityProperty(binaryOperandPair.Left)) as string;
				string name;
				if ((name = singleValueFunctionCallNode.Name) != null)
				{
					if (name == "contains")
					{
						queryFilter = new TextFilter(adpropertyDefinition2, text, MatchOptions.SubString, MatchFlags.IgnoreCase);
						goto IL_23D;
					}
					if (name == "startswith")
					{
						queryFilter = new TextFilter(adpropertyDefinition2, text, MatchOptions.Prefix, MatchFlags.IgnoreCase);
						goto IL_23D;
					}
					if (name == "endswith")
					{
						queryFilter = new TextFilter(adpropertyDefinition2, text, MatchOptions.Suffix, MatchFlags.IgnoreCase);
						goto IL_23D;
					}
				}
				throw new InvalidFilterNodeException(singleValueFunctionCallNode);
			}
			IL_23D:
			if (queryFilter == null)
			{
				throw new InvalidFilterNodeException(queryNode);
			}
			return queryFilter;
		}

		// Token: 0x06006198 RID: 24984 RVA: 0x001308A5 File Offset: 0x0012EAA5
		private DirectoryPropertyProvider GetPropertyProvider(QueryNode queryNode)
		{
			return base.GetEntityProperty(queryNode).ADDriverPropertyProvider as DirectoryPropertyProvider;
		}

		// Token: 0x06006199 RID: 24985 RVA: 0x001308B8 File Offset: 0x0012EAB8
		private object GetConstantValue(QueryNode queryNode, PropertyDefinition propertyDefinition)
		{
			DirectoryPropertyProvider directoryPropertyProvider = propertyDefinition.ADDriverPropertyProvider as DirectoryPropertyProvider;
			return directoryPropertyProvider.GetQueryConstant(base.ExtractConstantNodeValue(queryNode, propertyDefinition.Type));
		}
	}
}
