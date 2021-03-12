using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000D9 RID: 217
	internal abstract class WnsXmlNotification : WnsNotification
	{
		// Token: 0x0600070D RID: 1805 RVA: 0x00016272 File Offset: 0x00014472
		public WnsXmlNotification(string appId, OrganizationId tenantId, string deviceUri) : base(appId, tenantId, deviceUri)
		{
		}

		// Token: 0x0600070E RID: 1806
		protected abstract void WriteWnsXmlPayload(WnsPayloadWriter wpw);

		// Token: 0x0600070F RID: 1807 RVA: 0x0001627D File Offset: 0x0001447D
		protected override void PrepareWnsRequest(WnsRequest wnsRequest)
		{
			wnsRequest.ContentType = "text/xml";
		}

		// Token: 0x06000710 RID: 1808 RVA: 0x0001628C File Offset: 0x0001448C
		protected sealed override string GetSerializedPayload(List<LocalizedString> errors)
		{
			WnsPayloadWriter wnsPayloadWriter = new WnsPayloadWriter();
			this.WriteWnsXmlPayload(wnsPayloadWriter);
			if (wnsPayloadWriter.IsValid)
			{
				return wnsPayloadWriter.Dump();
			}
			errors.AddRange(wnsPayloadWriter.ValidationErrors);
			return null;
		}
	}
}
