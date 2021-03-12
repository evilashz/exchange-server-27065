using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C79 RID: 3193
	[ClassAccessLevel(AccessLevel.Implementation)]
	[Serializable]
	internal class LikersBlobProperty : SmartPropertyDefinition
	{
		// Token: 0x0600701F RID: 28703 RVA: 0x001F0778 File Offset: 0x001EE978
		internal LikersBlobProperty() : base("Likers", typeof(byte[]), PropertyFlags.None, PropertyDefinitionConstraint.None, new PropertyDependency[]
		{
			new PropertyDependency(InternalSchema.MapiLikersBlob, PropertyDependencyType.NeedForRead)
		})
		{
		}

		// Token: 0x06007020 RID: 28704 RVA: 0x001F07B8 File Offset: 0x001EE9B8
		protected override object InternalTryGetValue(PropertyBag.BasicPropertyStore propertyBag)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem == null)
			{
				return propertyBag.GetValue(InternalSchema.MapiLikersBlob);
			}
			object blob = ((Likers)messageItem.Likers).Blob;
			if (blob == null)
			{
				return new PropertyError(this, PropertyErrorCode.NotFound);
			}
			return blob;
		}

		// Token: 0x06007021 RID: 28705 RVA: 0x001F0804 File Offset: 0x001EEA04
		protected override void InternalSetValue(PropertyBag.BasicPropertyStore propertyBag, object value)
		{
			MessageItem messageItem = propertyBag.Context.StoreObject as MessageItem;
			if (messageItem != null)
			{
				try
				{
					((Likers)messageItem.Likers).Blob = (byte[])value;
					return;
				}
				catch (CorruptDataException ex)
				{
					throw PropertyError.ToException(ex.LocalizedString, new PropertyError[]
					{
						new PropertyError(InternalSchema.LikersBlob, PropertyErrorCode.SetCalculatedPropertyError)
					});
				}
			}
			propertyBag.SetValueWithFixup(InternalSchema.MapiLikersBlob, value);
		}
	}
}
