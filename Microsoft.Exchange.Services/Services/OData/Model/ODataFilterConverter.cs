using System;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.OData.Core;
using Microsoft.OData.Core.UriParser.Semantic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EA0 RID: 3744
	internal abstract class ODataFilterConverter
	{
		// Token: 0x06006185 RID: 24965 RVA: 0x001300AD File Offset: 0x0012E2AD
		public ODataFilterConverter(EntitySchema schema)
		{
			ArgumentValidator.ThrowIfNull("schema", schema);
			this.EntitySchema = schema;
		}

		// Token: 0x17001668 RID: 5736
		// (get) Token: 0x06006186 RID: 24966 RVA: 0x001300C7 File Offset: 0x0012E2C7
		// (set) Token: 0x06006187 RID: 24967 RVA: 0x001300CF File Offset: 0x0012E2CF
		public EntitySchema EntitySchema { get; private set; }

		// Token: 0x06006188 RID: 24968 RVA: 0x001300D8 File Offset: 0x0012E2D8
		protected ODataFilterConverter.BinaryOperandPair ParseBinaryFunctionParameters(SingleValueFunctionCallNode functionNode)
		{
			ArgumentValidator.ThrowIfNull("functionNode", functionNode);
			QueryNode[] array = functionNode.Parameters.ToArray<QueryNode>();
			if (array == null || array.Length != 2)
			{
				throw new InvalidFilterNodeException(functionNode);
			}
			return new ODataFilterConverter.BinaryOperandPair(array[0], array[1]);
		}

		// Token: 0x06006189 RID: 24969 RVA: 0x00130118 File Offset: 0x0012E318
		protected QueryNode UnwrapConvertNode(QueryNode queryNode)
		{
			ConvertNode convertNode = queryNode as ConvertNode;
			if (convertNode != null)
			{
				return convertNode.Source;
			}
			return queryNode;
		}

		// Token: 0x0600618A RID: 24970 RVA: 0x00130138 File Offset: 0x0012E338
		protected PropertyDefinition GetEntityProperty(QueryNode queryNode)
		{
			queryNode = this.UnwrapConvertNode(queryNode);
			SingleValuePropertyAccessNode singleValuePropertyAccessNode = queryNode as SingleValuePropertyAccessNode;
			if (singleValuePropertyAccessNode == null)
			{
				throw new InvalidFilterNodeException(queryNode);
			}
			PropertyDefinition propertyDefinition = this.EntitySchema.ResolveProperty(singleValuePropertyAccessNode.Property.Name);
			if (!propertyDefinition.Flags.HasFlag(PropertyDefinitionFlags.CanFilter))
			{
				throw new PropertyNotSupportFilterException(propertyDefinition.Name);
			}
			return propertyDefinition;
		}

		// Token: 0x0600618B RID: 24971 RVA: 0x0013019C File Offset: 0x0012E39C
		protected object ExtractConstantNodeValue(QueryNode queryNode, Type type)
		{
			queryNode = this.UnwrapConvertNode(queryNode);
			ConstantNode constantNode = queryNode as ConstantNode;
			if (constantNode == null)
			{
				throw new InvalidFilterNodeException(queryNode);
			}
			if (constantNode.Value is ODataEnumValue)
			{
				ODataEnumValue odataEnumValue = constantNode.Value as ODataEnumValue;
				return Enum.Parse(type, odataEnumValue.Value);
			}
			return constantNode.Value;
		}

		// Token: 0x040034C8 RID: 13512
		public const string Contains = "contains";

		// Token: 0x040034C9 RID: 13513
		public const string StartsWith = "startswith";

		// Token: 0x040034CA RID: 13514
		public const string EndsWith = "endswith";

		// Token: 0x02000EA1 RID: 3745
		protected struct BinaryOperandPair
		{
			// Token: 0x0600618C RID: 24972 RVA: 0x001301F1 File Offset: 0x0012E3F1
			public BinaryOperandPair(QueryNode left, QueryNode right)
			{
				this.Left = left;
				this.Right = right;
			}

			// Token: 0x040034CC RID: 13516
			public QueryNode Left;

			// Token: 0x040034CD RID: 13517
			public QueryNode Right;
		}
	}
}
