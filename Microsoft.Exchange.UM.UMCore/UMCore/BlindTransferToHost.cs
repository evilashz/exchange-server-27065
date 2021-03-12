using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001FE RID: 510
	internal class BlindTransferToHost : BlindTransferBase
	{
		// Token: 0x06000EF7 RID: 3831 RVA: 0x00043EA1 File Offset: 0x000420A1
		internal BlindTransferToHost(BaseUMCallSession session, CallContext context, PhoneNumber number, PlatformSipUri referredByUri) : base(session, context, number)
		{
			this.referredByUri = referredByUri;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x00043EB4 File Offset: 0x000420B4
		protected override PlatformSipUri GetReferredBySipUri()
		{
			return this.referredByUri;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x00043EBC File Offset: 0x000420BC
		protected override PlatformSipUri GetReferTargetUri(PhoneNumber phone, PlatformSipUri refByUri)
		{
			PlatformSipUri platformSipUri = Platform.Builder.CreateSipUri(string.Format(CultureInfo.InvariantCulture, "sip:{0}", new object[]
			{
				phone.ToDial
			}));
			if (!string.IsNullOrEmpty(platformSipUri.User) && UtilityMethods.IsAnonymousNumber(platformSipUri.User))
			{
				platformSipUri.User = "Anonymous";
			}
			PIIMessage data = PIIMessage.Create(PIIType._PhoneNumber, phone);
			CallIdTracer.TraceDebug(ExTraceGlobals.CallSessionTracer, this, data, "TransferToHost::GetReferTargetUri() phone = _PhoneNumber returning {0}", new object[]
			{
				platformSipUri
			});
			return platformSipUri;
		}

		// Token: 0x04000B31 RID: 2865
		private PlatformSipUri referredByUri;
	}
}
