using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.Handler
{
	// Token: 0x0200003B RID: 59
	internal abstract class PropertyConversion
	{
		// Token: 0x0600029F RID: 671 RVA: 0x00017A02 File Offset: 0x00015C02
		protected PropertyConversion(PropertyTag clientPropertyTag, PropertyTag serverPropertyTag)
		{
			this.ClientPropertyTag = clientPropertyTag;
			this.ServerPropertyTag = serverPropertyTag;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x00017A18 File Offset: 0x00015C18
		internal bool TryConvertPropertyTagFromClient(PropertyTag clientPropertyTag, out PropertyTag serverPropertyTag)
		{
			if (clientPropertyTag == this.ClientPropertyTag)
			{
				serverPropertyTag = this.ServerPropertyTag;
				return true;
			}
			if (clientPropertyTag.PropertyId == this.ClientPropertyTag.PropertyId && clientPropertyTag.PropertyType == PropertyType.Unspecified)
			{
				serverPropertyTag = new PropertyTag(this.ServerPropertyTag.PropertyId, PropertyType.Unspecified);
				return true;
			}
			serverPropertyTag = clientPropertyTag;
			return false;
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00017A88 File Offset: 0x00015C88
		internal bool TryConvertPropertyTagToClient(PropertyTag serverPropertyTag, PropertyTag? originalClientPropertyTag, out PropertyTag clientPropertyTag)
		{
			if (this.CanConvertPropertyTagToClient(serverPropertyTag, originalClientPropertyTag))
			{
				if (serverPropertyTag == this.ServerPropertyTag)
				{
					clientPropertyTag = this.ClientPropertyTag;
				}
				else
				{
					clientPropertyTag = new PropertyTag(this.ClientPropertyTag.PropertyId, this.ServerPropertyTag.PropertyType);
				}
				return true;
			}
			clientPropertyTag = serverPropertyTag;
			return false;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x00017AEC File Offset: 0x00015CEC
		internal bool TryConvertPropertyValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, ref PropertyValue propertyValue)
		{
			if (propertyValue.PropertyTag == this.ClientPropertyTag)
			{
				propertyValue = new PropertyValue(this.ServerPropertyTag, this.ConvertValueFromClient(session, storageObjectProperties, propertyValue.Value));
				return true;
			}
			return false;
		}

		// Token: 0x060002A3 RID: 675 RVA: 0x00017B24 File Offset: 0x00015D24
		internal bool TryConvertPropertyValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, PropertyTag? originalClientPropertyTag, ref PropertyValue propertyValue)
		{
			if (this.CanConvertPropertyTagToClient(propertyValue.PropertyTag, originalClientPropertyTag))
			{
				if (propertyValue.PropertyTag == this.ServerPropertyTag)
				{
					propertyValue = new PropertyValue(this.ClientPropertyTag, this.ConvertValueToClient(session, storageObjectProperties, propertyValue.Value));
				}
				else
				{
					propertyValue = propertyValue.CloneAs(this.ClientPropertyTag.PropertyId);
				}
				return true;
			}
			return false;
		}

		// Token: 0x060002A4 RID: 676
		protected abstract object ConvertValueFromClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue);

		// Token: 0x060002A5 RID: 677
		protected abstract object ConvertValueToClient(StoreSession session, IStorageObjectProperties storageObjectProperties, object propertyValue);

		// Token: 0x060002A6 RID: 678 RVA: 0x00017B98 File Offset: 0x00015D98
		private bool CanConvertPropertyTagToClient(PropertyTag serverPropertyTag, PropertyTag? originalClientPropertyTag)
		{
			return (serverPropertyTag == this.ServerPropertyTag || (serverPropertyTag.PropertyId == this.ServerPropertyTag.PropertyId && serverPropertyTag.PropertyType == PropertyType.Error)) && (originalClientPropertyTag == null || originalClientPropertyTag.Value == this.ClientPropertyTag || (originalClientPropertyTag.Value.PropertyId == this.ClientPropertyTag.PropertyId && originalClientPropertyTag.Value.PropertyType == PropertyType.Unspecified));
		}

		// Token: 0x04000101 RID: 257
		internal readonly PropertyTag ClientPropertyTag;

		// Token: 0x04000102 RID: 258
		internal readonly PropertyTag ServerPropertyTag;
	}
}
