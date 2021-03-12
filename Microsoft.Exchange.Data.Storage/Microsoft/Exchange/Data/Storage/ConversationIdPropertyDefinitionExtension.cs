using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4F RID: 3151
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class ConversationIdPropertyDefinitionExtension
	{
		// Token: 0x06006F56 RID: 28502 RVA: 0x001DF404 File Offset: 0x001DD604
		internal static QueryFilter NativeFilterToConversationIdBasedSmartFilter(this SmartPropertyDefinition conversationIdSmartPropertyDefinition, QueryFilter filter, PropertyDefinition conversationIdNativePropertyDefinition)
		{
			SinglePropertyFilter singlePropertyFilter = filter as SinglePropertyFilter;
			if (singlePropertyFilter != null && singlePropertyFilter.Property.Equals(conversationIdNativePropertyDefinition))
			{
				ComparisonFilter comparisonFilter = filter as ComparisonFilter;
				if (comparisonFilter != null)
				{
					return new ComparisonFilter(comparisonFilter.ComparisonOperator, conversationIdSmartPropertyDefinition, ConversationId.Create((byte[])comparisonFilter.PropertyValue));
				}
				ExistsFilter existsFilter = filter as ExistsFilter;
				if (existsFilter != null)
				{
					return new ExistsFilter(conversationIdSmartPropertyDefinition);
				}
			}
			return null;
		}

		// Token: 0x06006F57 RID: 28503 RVA: 0x001DF464 File Offset: 0x001DD664
		internal static QueryFilter ConversationIdBasedSmartFilterToNativeFilter(this SmartPropertyDefinition conversationIdSmartPropertyDefinition, SinglePropertyFilter filter, PropertyDefinition conversationIdNativePropertyDefinition)
		{
			ComparisonFilter comparisonFilter = filter as ComparisonFilter;
			if (comparisonFilter != null)
			{
				ConversationId conversationId = (ConversationId)comparisonFilter.PropertyValue;
				return new ComparisonFilter(comparisonFilter.ComparisonOperator, conversationIdNativePropertyDefinition, conversationId.GetBytes());
			}
			ExistsFilter existsFilter = filter as ExistsFilter;
			if (existsFilter != null)
			{
				return new ExistsFilter(conversationIdNativePropertyDefinition);
			}
			throw conversationIdSmartPropertyDefinition.CreateInvalidFilterConversionException(filter);
		}
	}
}
