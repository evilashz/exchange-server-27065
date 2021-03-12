using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.RpcClientAccess.FastTransfer.Parser;
using Microsoft.Exchange.RpcClientAccess.Handler;
using Microsoft.Exchange.RpcClientAccess.Handler.StorageObjects;

namespace Microsoft.Exchange.RpcClientAccess.FastTransfer.Handler
{
	// Token: 0x02000082 RID: 130
	internal class RecipientPropertyBagAdaptor : CorePropertyBagAdaptor
	{
		// Token: 0x060004F4 RID: 1268 RVA: 0x000234A4 File Offset: 0x000216A4
		public RecipientPropertyBagAdaptor(ICorePropertyBag corePropertyBag, ICoreObject propertyMappingReference, Encoding string8Encoding, bool wantUnicode) : base(new CoreObjectProperties(corePropertyBag), corePropertyBag, propertyMappingReference, ClientSideProperties.RecipientInstance, PropertyConverter.Recipient, DownloadBodyOption.AllBodyProperties, string8Encoding, wantUnicode, false, false)
		{
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x000234CF File Offset: 0x000216CF
		public override System.IO.Stream SetPropertyStream(PropertyTag property, long dataSizeEstimate)
		{
			return MemoryPropertyBag.WrapPropertyWriteStream(this, property, dataSizeEstimate);
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x000234D9 File Offset: 0x000216D9
		protected override bool IgnoreDownloadProperty(PropertyTag property)
		{
			return false;
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x000234DC File Offset: 0x000216DC
		protected override bool IgnoreUploadProperty(PropertyTag property)
		{
			return false;
		}
	}
}
