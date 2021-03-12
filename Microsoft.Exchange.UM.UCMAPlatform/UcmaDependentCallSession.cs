using System;
using Microsoft.Exchange.UM.UMCore;
using Microsoft.Rtc.Collaboration;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000044 RID: 68
	internal class UcmaDependentCallSession : UcmaCallSession
	{
		// Token: 0x06000323 RID: 803 RVA: 0x0000CFC6 File Offset: 0x0000B1C6
		internal UcmaDependentCallSession(DependentSessionDetails details, ISessionSerializer serializer, ApplicationEndpoint localEndpoint, CallContext cc) : base(serializer, localEndpoint, cc)
		{
			base.DependentSessionDetails = details;
			this.OnOutboundCallRequestCompleted += details.OutBoundCallConnectedHandler;
			this.IgnoreBye = false;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000324 RID: 804 RVA: 0x0000CFEC File Offset: 0x0000B1EC
		protected override bool IsDependentSession
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0000CFEF File Offset: 0x0000B1EF
		// (set) Token: 0x06000326 RID: 806 RVA: 0x0000CFF7 File Offset: 0x0000B1F7
		internal bool IgnoreBye { get; set; }
	}
}
