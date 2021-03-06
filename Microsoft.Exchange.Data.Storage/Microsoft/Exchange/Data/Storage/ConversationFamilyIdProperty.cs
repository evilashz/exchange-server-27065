using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4B RID: 3147
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ConversationFamilyIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F41 RID: 28481 RVA: 0x001DEE98 File Offset: 0x001DD098
		internal ConversationFamilyIdProperty() : base("ConversationFamilyId", typeof(ConversationId), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiConversationFamilyId, PropertyDependencyType.AllRead)
		})
		{
		}

		// Token: 0x06006F42 RID: 28482 RVA: 0x001DEED8 File Offset: 0x001DD0D8
		protected sealed override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			if (value == null)
			{
				propertyBag.Delete(InternalSchema.MapiConversationFamilyId);
				return;
			}
			ConversationId conversationId = value as ConversationId;
			if (conversationId == null)
			{
				throw new ArgumentException("value", "Must be null or ConversationId");
			}
			propertyBag.SetOrDeleteProperty(InternalSchema.MapiConversationFamilyId, conversationId.GetBytes());
		}

		// Token: 0x06006F43 RID: 28483 RVA: 0x001DEF21 File Offset: 0x001DD121
		protected override void InternalDeleteValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			propertyBag.Delete(InternalSchema.MapiConversationFamilyId);
		}

		// Token: 0x06006F44 RID: 28484 RVA: 0x001DEF30 File Offset: 0x001DD130
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(InternalSchema.MapiConversationFamilyId);
			byte[] array = value as byte[];
			if (array != null)
			{
				object result;
				try
				{
					result = ConversationId.Create(array);
				}
				catch (CorruptDataException)
				{
					result = new PropertyError(this, PropertyErrorCode.CorruptedData);
				}
				return result;
			}
			PropertyError propertyError = (PropertyError)value;
			if (propertyError.PropertyErrorCode == PropertyErrorCode.NotEnoughMemory)
			{
				return new PropertyError(this, PropertyErrorCode.CorruptedData);
			}
			return new PropertyError(this, propertyError.PropertyErrorCode);
		}
	}
}
