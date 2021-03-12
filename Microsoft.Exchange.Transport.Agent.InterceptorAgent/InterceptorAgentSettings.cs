using System;

namespace Microsoft.Exchange.Transport.Agent.InterceptorAgent
{
	// Token: 0x02000017 RID: 23
	internal static class InterceptorAgentSettings
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000064E4 File Offset: 0x000046E4
		public static string ArchivePath
		{
			get
			{
				return "D:\\TransportRoles\\Interceptor";
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000110 RID: 272 RVA: 0x000064EB File Offset: 0x000046EB
		public static string ArchivedMessagesDirectory
		{
			get
			{
				return "ArchivedMessages";
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000111 RID: 273 RVA: 0x000064F2 File Offset: 0x000046F2
		public static string ArchivedMessageHeadersDirectory
		{
			get
			{
				return "ArchivedMessageHeaders";
			}
		}
	}
}
