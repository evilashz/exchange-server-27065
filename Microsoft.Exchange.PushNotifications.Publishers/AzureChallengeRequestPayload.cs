using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000061 RID: 97
	internal class AzureChallengeRequestPayload
	{
		// Token: 0x0600039E RID: 926 RVA: 0x0000C53F File Offset: 0x0000A73F
		public AzureChallengeRequestPayload(PushNotificationPlatform platform, string deviceChallenge)
		{
			this.DeviceChallenge = deviceChallenge;
			this.TargetPlatform = platform;
		}

		// Token: 0x170000E4 RID: 228
		// (get) Token: 0x0600039F RID: 927 RVA: 0x0000C555 File Offset: 0x0000A755
		// (set) Token: 0x060003A0 RID: 928 RVA: 0x0000C55D File Offset: 0x0000A75D
		public string DeviceChallenge { get; private set; }

		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x060003A1 RID: 929 RVA: 0x0000C566 File Offset: 0x0000A766
		// (set) Token: 0x060003A2 RID: 930 RVA: 0x0000C56E File Offset: 0x0000A76E
		public PushNotificationPlatform TargetPlatform { get; private set; }

		// Token: 0x060003A3 RID: 931 RVA: 0x0000C577 File Offset: 0x0000A777
		public override string ToString()
		{
			if (this.toString == null)
			{
				this.toString = string.Format("{{challenge:{0}; platform:{1}}}", this.DeviceChallenge, this.TargetPlatform);
			}
			return this.toString;
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
		internal void WriteAzureSecretRequestPayload(AzureChallengeRequestPayloadWriter apw)
		{
			ArgumentValidator.ThrowIfNull("apw", apw);
			if (!string.IsNullOrWhiteSpace(this.DeviceChallenge))
			{
				apw.AddChallenge(this.DeviceChallenge);
			}
			apw.AddPlatform(this.TargetPlatform);
		}

		// Token: 0x04000192 RID: 402
		private string toString;
	}
}
