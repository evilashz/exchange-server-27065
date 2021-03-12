using System;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000009 RID: 9
	public class ClientInfo
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600005C RID: 92 RVA: 0x00004568 File Offset: 0x00002768
		// (set) Token: 0x0600005D RID: 93 RVA: 0x00004570 File Offset: 0x00002770
		public string ApplicationId { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600005E RID: 94 RVA: 0x00004579 File Offset: 0x00002779
		// (set) Token: 0x0600005F RID: 95 RVA: 0x00004581 File Offset: 0x00002781
		public string ClientVersion { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000060 RID: 96 RVA: 0x0000458A File Offset: 0x0000278A
		// (set) Token: 0x06000061 RID: 97 RVA: 0x00004592 File Offset: 0x00002792
		public string ClientMachineName { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000062 RID: 98 RVA: 0x0000459B File Offset: 0x0000279B
		// (set) Token: 0x06000063 RID: 99 RVA: 0x000045A3 File Offset: 0x000027A3
		public string ClientProcessName { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000064 RID: 100 RVA: 0x000045AC File Offset: 0x000027AC
		// (set) Token: 0x06000065 RID: 101 RVA: 0x000045B4 File Offset: 0x000027B4
		public DateTime ConnectTime { get; set; }

		// Token: 0x06000066 RID: 102 RVA: 0x000045C0 File Offset: 0x000027C0
		public override string ToString()
		{
			return string.Format("[ApplicationId:{0}, ClientVersion:{1}, ClientMachineName:{2}, ClientProcessName:{3}, ConnectTime:{4}]", new object[]
			{
				this.ApplicationId,
				this.ClientVersion,
				this.ClientMachineName,
				this.ClientProcessName,
				this.ConnectTime
			});
		}
	}
}
