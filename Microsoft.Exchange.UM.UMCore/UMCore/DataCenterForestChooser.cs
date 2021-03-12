using System;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000109 RID: 265
	internal class DataCenterForestChooser : IRedirectTargetChooser
	{
		// Token: 0x06000754 RID: 1876 RVA: 0x0001DBD8 File Offset: 0x0001BDD8
		public DataCenterForestChooser(CallContext callContext, string forestFqdn, string phoneNumber)
		{
			if (callContext == null)
			{
				throw new ArgumentNullException("callContext");
			}
			if (string.IsNullOrEmpty(forestFqdn))
			{
				throw new ArgumentOutOfRangeException("forestFqdn", forestFqdn, "Invalid forest FQDN");
			}
			if (string.IsNullOrEmpty(phoneNumber))
			{
				throw new ArgumentOutOfRangeException("phoneNumber", phoneNumber, "Invalid phone number");
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phoneNumber);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "DataCenterForestChooser constructor with forest FQDN = '{0}' and phone number = '_PhoneNumber'", new object[]
			{
				forestFqdn
			});
			this.callContext = callContext;
			this.forestFqdn = forestFqdn;
			this.phoneNumber = phoneNumber;
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000755 RID: 1877 RVA: 0x0001DC66 File Offset: 0x0001BE66
		public string SubscriberLogId
		{
			get
			{
				return this.phoneNumber;
			}
		}

		// Token: 0x06000756 RID: 1878 RVA: 0x0001DC70 File Offset: 0x0001BE70
		public bool GetTargetServer(out string fqdn, out int port)
		{
			fqdn = this.forestFqdn;
			port = Utils.GetRedirectPort(this.callContext.IsSecuredCall);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "DataCenterForestChooser::GetTargetServer() returning {0}:{1}", new object[]
			{
				fqdn,
				port
			});
			return true;
		}

		// Token: 0x06000757 RID: 1879 RVA: 0x0001DCBF File Offset: 0x0001BEBF
		public void HandleServerNotFound()
		{
		}

		// Token: 0x0400082A RID: 2090
		private CallContext callContext;

		// Token: 0x0400082B RID: 2091
		private string forestFqdn;

		// Token: 0x0400082C RID: 2092
		private string phoneNumber;
	}
}
