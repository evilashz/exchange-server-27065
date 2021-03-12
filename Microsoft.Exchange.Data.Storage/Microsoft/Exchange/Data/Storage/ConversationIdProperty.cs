using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C4E RID: 3150
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ConversationIdProperty : SmartPropertyDefinition
	{
		// Token: 0x06006F50 RID: 28496 RVA: 0x001DF324 File Offset: 0x001DD524
		internal ConversationIdProperty(PropertyTagPropertyDefinition conversationIdPropertyDefinition, string propertyName) : base(propertyName, typeof(ConversationId), PropertyFlags.ReadOnly, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(conversationIdPropertyDefinition, PropertyDependencyType.NeedForRead)
		})
		{
			this.conversationIdPropertyDefinition = conversationIdPropertyDefinition;
		}

		// Token: 0x06006F51 RID: 28497 RVA: 0x001DF364 File Offset: 0x001DD564
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			object value = propertyBag.GetValue(this.conversationIdPropertyDefinition);
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

		// Token: 0x06006F52 RID: 28498 RVA: 0x001DF3D8 File Offset: 0x001DD5D8
		internal override QueryFilter NativeFilterToSmartFilter(QueryFilter filter)
		{
			return this.NativeFilterToConversationIdBasedSmartFilter(filter, this.conversationIdPropertyDefinition);
		}

		// Token: 0x06006F53 RID: 28499 RVA: 0x001DF3E7 File Offset: 0x001DD5E7
		internal override QueryFilter SmartFilterToNativeFilter(SinglePropertyFilter filter)
		{
			return this.ConversationIdBasedSmartFilterToNativeFilter(filter, this.conversationIdPropertyDefinition);
		}

		// Token: 0x06006F54 RID: 28500 RVA: 0x001DF3F6 File Offset: 0x001DD5F6
		protected override NativeStorePropertyDefinition GetSortProperty()
		{
			return this.conversationIdPropertyDefinition;
		}

		// Token: 0x17001E0A RID: 7690
		// (get) Token: 0x06006F55 RID: 28501 RVA: 0x001DF3FE File Offset: 0x001DD5FE
		public override StorePropertyCapabilities Capabilities
		{
			get
			{
				return StorePropertyCapabilities.All;
			}
		}

		// Token: 0x04004308 RID: 17160
		private PropertyTagPropertyDefinition conversationIdPropertyDefinition;
	}
}
