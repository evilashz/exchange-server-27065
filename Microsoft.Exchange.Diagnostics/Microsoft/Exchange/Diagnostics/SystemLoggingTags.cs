using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x02000212 RID: 530
	public struct SystemLoggingTags
	{
		// Token: 0x04000B6A RID: 2922
		public const int SystemNet = 0;

		// Token: 0x04000B6B RID: 2923
		public const int SystemNetSocket = 1;

		// Token: 0x04000B6C RID: 2924
		public const int SystemNetHttpListener = 2;

		// Token: 0x04000B6D RID: 2925
		public const int SystemIdentityModelTrace = 3;

		// Token: 0x04000B6E RID: 2926
		public const int SystemServiceModelTrace = 4;

		// Token: 0x04000B6F RID: 2927
		public const int SystemServiceModelMessageLogging = 5;

		// Token: 0x04000B70 RID: 2928
		public const int SystemServiceModelMessageLogging_LogMalformedMessages = 6;

		// Token: 0x04000B71 RID: 2929
		public const int SystemServiceModelMessageLogging_LogMessagesAtServiceLevel = 7;

		// Token: 0x04000B72 RID: 2930
		public const int SystemServiceModelMessageLogging_LogMessagesAtTransportLevel = 8;

		// Token: 0x04000B73 RID: 2931
		public const int SystemServiceModelMessageLogging_LogMessageBody = 9;

		// Token: 0x04000B74 RID: 2932
		public static Guid guid = new Guid("F21F1E57-9689-46E5-BE7D-A84C9BCE0D94");
	}
}
