using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001EE RID: 494
	internal class SupervisedTransfer : TransferBase
	{
		// Token: 0x06000E8B RID: 3723 RVA: 0x000412B1 File Offset: 0x0003F4B1
		internal SupervisedTransfer(BaseUMCallSession session, CallContext context, PhoneNumber number, UMSubscriber referrer) : base(session, context)
		{
			this.number = number;
			this.referrer = referrer;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000412CC File Offset: 0x0003F4CC
		internal override void Transfer()
		{
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, this.number);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "SupervisedTransfer to: _PhoneNumber.", new object[0]);
			if (base.Context.DialPlan.URIType == UMUriType.SipName)
			{
				base.FrameTransferTargetAndTransferForSIPNames(this.number);
				return;
			}
			if (SipRoutingHelper.UseGlobalSBCSettingsForOutbound(base.Context.GatewayConfig))
			{
				base.Session.TransferAsync(base.GetReferTargetForPhoneNumbers(this.number, null));
				return;
			}
			base.Session.TransferAsync();
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x00041354 File Offset: 0x0003F554
		protected override PlatformSipUri GetReferredBySipUri()
		{
			return base.GetSipUriFromSubscriber(this.referrer);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x00041362 File Offset: 0x0003F562
		protected override PlatformSipUri GetReferTargetUri(PhoneNumber phone, PlatformSipUri refByUri)
		{
			return null;
		}

		// Token: 0x04000AF7 RID: 2807
		private UMSubscriber referrer;

		// Token: 0x04000AF8 RID: 2808
		private PhoneNumber number;
	}
}
