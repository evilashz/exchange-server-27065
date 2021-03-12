using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C8A RID: 3210
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal sealed class MessageSentRepresentingTypeProperty : SmartPropertyDefinition
	{
		// Token: 0x06007059 RID: 28761 RVA: 0x001F1A4C File Offset: 0x001EFC4C
		internal MessageSentRepresentingTypeProperty() : base("MessageSentRepresentingType", typeof(MessageSentRepresentingFlags), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, MessageSentRepresentingTypeProperty.PropertyDependencies)
		{
		}

		// Token: 0x0600705A RID: 28762 RVA: 0x001F1A70 File Offset: 0x001EFC70
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[] array = propertyBag.GetValue(InternalSchema.CreatorEntryId) as byte[];
			byte[] array2 = propertyBag.GetValue(InternalSchema.SenderEntryId) as byte[];
			byte[] array3 = propertyBag.GetValue(InternalSchema.SentRepresentingEntryId) as byte[];
			if (array == null || array2 == null || array3 == null)
			{
				return MessageSentRepresentingFlags.None;
			}
			if (ArrayComparer<byte>.Comparer.Equals(array, array3))
			{
				return MessageSentRepresentingFlags.None;
			}
			if (ArrayComparer<byte>.Comparer.Equals(array3, array2))
			{
				return MessageSentRepresentingFlags.SendAs;
			}
			return MessageSentRepresentingFlags.SendOnBehalfOf;
		}

		// Token: 0x04004D82 RID: 19842
		private static readonly PropertyDependency[] PropertyDependencies = new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.CreatorEntryId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.SenderEntryId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.SentRepresentingEntryId, PropertyDependencyType.NeedForRead)
		};
	}
}
