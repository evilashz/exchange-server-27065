using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x02000041 RID: 65
	internal class BlindTransferToPhone : BlindTransferBase
	{
		// Token: 0x060002AE RID: 686 RVA: 0x0000BE44 File Offset: 0x0000A044
		internal BlindTransferToPhone(BaseUMCallSession session, CallContext context, PhoneNumber number) : base(session, context, number)
		{
		}

		// Token: 0x060002AF RID: 687 RVA: 0x0000BE4F File Offset: 0x0000A04F
		protected override PlatformSipUri GetReferTargetUri(PhoneNumber phone, PlatformSipUri refByUri)
		{
			if (phone.UriType == UMUriType.SipName)
			{
				return Platform.Builder.CreateSipUri("SIP:" + phone.ToDial);
			}
			return base.GetReferTargetForPhoneNumbers(phone, refByUri);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x0000BE80 File Offset: 0x0000A080
		protected override PlatformSipUri GetReferredBySipUri()
		{
			PlatformSipUri platformSipUri = null;
			if (base.Context.CallType == 3)
			{
				platformSipUri = base.GetSipUriFromSubscriber(base.Context.CallerInfo);
			}
			else if (!string.IsNullOrEmpty(base.Context.OCFeature.ReferredBy))
			{
				PlatformSignalingHeader platformSignalingHeader = Platform.Builder.CreateSignalingHeader("Referred-By", base.Context.OCFeature.ReferredBy);
				platformSipUri = platformSignalingHeader.ParseUri();
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, "GetReferredBySipUri: returning {0} for call type {1}", new object[]
			{
				platformSipUri,
				base.Context.CallType
			});
			return platformSipUri;
		}
	}
}
