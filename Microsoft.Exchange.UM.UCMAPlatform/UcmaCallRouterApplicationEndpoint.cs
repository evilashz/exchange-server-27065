using System;
using Microsoft.Rtc.Collaboration;
using Microsoft.Rtc.Signaling;

namespace Microsoft.Exchange.UM.UcmaPlatform
{
	// Token: 0x02000041 RID: 65
	internal class UcmaCallRouterApplicationEndpoint : ApplicationEndpoint
	{
		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060002EE RID: 750 RVA: 0x0000B800 File Offset: 0x00009A00
		// (remove) Token: 0x060002EF RID: 751 RVA: 0x0000B838 File Offset: 0x00009A38
		public event EventHandler<SessionReceivedEventArgs> LegacyLyncNotificationCallReceived;

		// Token: 0x060002F0 RID: 752 RVA: 0x0000B86D File Offset: 0x00009A6D
		public UcmaCallRouterApplicationEndpoint(CollaborationPlatform platform, ApplicationEndpointSettings settings) : base(platform, settings)
		{
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x0000B878 File Offset: 0x00009A78
		protected override bool HandleSignalingSession(SessionReceivedEventArgs args)
		{
			bool result = false;
			if (UcmaCallRouterApplicationEndpoint.IsUserEventNotificationCall(args) && this.LegacyLyncNotificationCallReceived != null)
			{
				this.LegacyLyncNotificationCallReceived(this, args);
				result = true;
			}
			return result;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0000B8A7 File Offset: 0x00009AA7
		private static bool IsUserEventNotificationCall(SessionReceivedEventArgs args)
		{
			return args.RequestUri.IndexOf("opaque=app:rtcevent", StringComparison.OrdinalIgnoreCase) > 0;
		}
	}
}
