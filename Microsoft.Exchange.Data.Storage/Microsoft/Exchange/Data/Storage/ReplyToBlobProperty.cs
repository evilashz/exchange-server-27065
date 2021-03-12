using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000CBD RID: 3261
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class ReplyToBlobProperty : SmartPropertyDefinition
	{
		// Token: 0x0600716B RID: 29035 RVA: 0x001F71AC File Offset: 0x001F53AC
		internal ReplyToBlobProperty() : base("ReplyTo", typeof(byte[]), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiReplyToBlob, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x0600716C RID: 29036 RVA: 0x001F71EC File Offset: 0x001F53EC
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem == null)
			{
				return propertyBag.GetValue(InternalSchema.MapiReplyToBlob);
			}
			object blob = ((ReplyTo)messageItem.ReplyTo).Blob;
			if (blob == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return blob;
		}

		// Token: 0x0600716D RID: 29037 RVA: 0x001F7238 File Offset: 0x001F5438
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem != null)
			{
				try
				{
					((ReplyTo)messageItem.ReplyTo).Blob = (byte[])value;
					return;
				}
				catch (CorruptDataException ex)
				{
					throw PropertyError.ToException(ex.LocalizedString, new PropertyError[]
					{
						new PropertyError(InternalSchema.ReplyToBlob, PropertyErrorCode.SetCalculatedPropertyError)
					});
				}
			}
			propertyBag.SetValueWithFixup(InternalSchema.MapiReplyToBlob, (byte[])value);
		}
	}
}
