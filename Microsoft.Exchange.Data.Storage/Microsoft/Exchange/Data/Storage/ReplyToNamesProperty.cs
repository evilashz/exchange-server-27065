using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBE RID: 3262
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReplyToNamesProperty : SmartPropertyDefinition
	{
		// Token: 0x0600716E RID: 29038 RVA: 0x001F72B8 File Offset: 0x001F54B8
		internal ReplyToNamesProperty() : base("ReplyToNames", typeof(string), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiReplyToNames, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x0600716F RID: 29039 RVA: 0x001F72F8 File Offset: 0x001F54F8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem == null)
			{
				return propertyBag.GetValue(InternalSchema.MapiReplyToNames);
			}
			object names = ((ReplyTo)messageItem.ReplyTo).Names;
			if (names == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return names;
		}

		// Token: 0x06007170 RID: 29040 RVA: 0x001F7344 File Offset: 0x001F5544
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem != null)
			{
				try
				{
					((ReplyTo)messageItem.ReplyTo).Names = (string)value;
					return;
				}
				catch (CorruptDataException ex)
				{
					throw PropertyError.ToException(ex.LocalizedString, new PropertyError[]
					{
						new PropertyError(InternalSchema.ReplyToNames, PropertyErrorCode.SetCalculatedPropertyError)
					});
				}
			}
			propertyBag.SetValueWithFixup(InternalSchema.MapiReplyToNames, (string)value);
		}
	}
}
