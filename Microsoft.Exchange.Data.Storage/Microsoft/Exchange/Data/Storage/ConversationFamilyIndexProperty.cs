using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4C RID: 3148
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ConversationFamilyIndexProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F45 RID: 28485 RVA: 0x001DEFA0 File Offset: 0x001DD1A0
		internal ConversationFamilyIndexProperty() : base("ConversationFamilyIndexProperty", typeof(byte[]), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiConversationFamilyId, PropertyDependencyType.NeedForRead),
			new PropertyDependency(InternalSchema.ConversationIndex, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06006F46 RID: 28486 RVA: 0x001DEFEC File Offset: 0x001DD1EC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			byte[] valueOrDefault = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.MapiConversationFamilyId, null);
			byte[] valueOrDefault2 = propertyBag.GetValueOrDefault<byte[]>(InternalSchema.ConversationIndex);
			if (valueOrDefault2 == null || valueOrDefault == null)
			{
				return null;
			}
			ConversationIndex conversationIndex;
			bool flag = ConversationIndex.TryCreate(valueOrDefault2, out conversationIndex);
			ConversationId conversationId = ConversationId.Create(valueOrDefault);
			if (!flag)
			{
				return null;
			}
			return conversationIndex.UpdateGuid(conversationId).ToByteArray();
		}
	}
}
