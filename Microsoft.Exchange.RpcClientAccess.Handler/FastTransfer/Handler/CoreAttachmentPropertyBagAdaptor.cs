using System;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x0200005E RID: 94
	internal sealed class CoreAttachmentPropertyBagAdaptor : CorePropertyBagAdaptor
	{
		// Token: 0x060003FC RID: 1020 RVA: 0x0001DC8C File Offset: 0x0001BE8C
		public CoreAttachmentPropertyBagAdaptor(ICorePropertyBag corePropertyBag, ICoreObject propertyMappingReference, Encoding string8Encoding, bool wantUnicode, bool isUpload) : base(new CoreObjectProperties(corePropertyBag), corePropertyBag, propertyMappingReference, ClientSideProperties.AttachmentInstance, PropertyConverter.Attachment, DownloadBodyOption.AllBodyProperties, string8Encoding, wantUnicode, isUpload, false)
		{
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x0001DCB8 File Offset: 0x0001BEB8
		public override void SetProperty(PropertyValue propertyValue)
		{
			if (!propertyValue.IsError && this.IgnoreUploadProperty(propertyValue.PropertyTag))
			{
				return;
			}
			base.SetProperty(propertyValue);
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x0001DCDA File Offset: 0x0001BEDA
		public override System.IO.Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			if (this.IgnoreUploadProperty(property))
			{
				return null;
			}
			return base.SetPropertyStream(property, dataSizeEstimate);
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x0001DCEF File Offset: 0x0001BEEF
		protected override bool IgnoreUploadProperty(PropertyTag property)
		{
			return CoreAttachmentPropertyBagAdaptor.StoreComputedAttachmentProperties.Contains(property, PropertyTag.PropertyIdComparer) || base.IgnoreUploadProperty(property);
		}

		// Token: 0x04000162 RID: 354
		private static readonly PropertyTag[] StoreComputedAttachmentProperties = new PropertyTag[]
		{
			PropertyTag.AttachmentNumber,
			PropertyTag.AccessLevel,
			PropertyTag.MappingSignature,
			PropertyTag.AttachmentSize
		};
	}
}
