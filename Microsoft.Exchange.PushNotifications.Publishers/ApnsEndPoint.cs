using System;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000021 RID: 33
	internal class ApnsEndPoint
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00005CE0 File Offset: 0x00003EE0
		public ApnsEndPoint(string host, int port, string feedbackHost, int feedbackPort)
		{
			this.Host = host;
			this.Port = port;
			this.FeedbackHost = feedbackHost;
			this.FeedbackPort = feedbackPort;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00005D05 File Offset: 0x00003F05
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00005D0D File Offset: 0x00003F0D
		public string Host { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00005D16 File Offset: 0x00003F16
		// (set) Token: 0x06000155 RID: 341 RVA: 0x00005D1E File Offset: 0x00003F1E
		public string FeedbackHost { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00005D27 File Offset: 0x00003F27
		// (set) Token: 0x06000157 RID: 343 RVA: 0x00005D2F File Offset: 0x00003F2F
		public int Port { get; private set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00005D38 File Offset: 0x00003F38
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00005D40 File Offset: 0x00003F40
		public int FeedbackPort { get; private set; }

		// Token: 0x04000079 RID: 121
		public const string DefaultSandboxHost = "gateway.sandbox.push.apple.com";

		// Token: 0x0400007A RID: 122
		public const string DefaultSandboxFeedbackHost = "feedback.sandbox.push.apple.com";

		// Token: 0x0400007B RID: 123
		public const string DefaultProductionHost = "gateway.push.apple.com";

		// Token: 0x0400007C RID: 124
		public const string DefaultProductionFeedbackHost = "feedback.push.apple.com";

		// Token: 0x0400007D RID: 125
		public const int DefaultPort = 2195;

		// Token: 0x0400007E RID: 126
		public const int DefaultFeedbackPort = 2196;

		// Token: 0x0400007F RID: 127
		internal static readonly ApnsEndPoint Sandbox = new ApnsEndPoint("gateway.sandbox.push.apple.com", 2195, "feedback.sandbox.push.apple.com", 2196);

		// Token: 0x04000080 RID: 128
		internal static readonly ApnsEndPoint Production = new ApnsEndPoint("gateway.push.apple.com", 2195, "feedback.push.apple.com", 2196);
	}
}
