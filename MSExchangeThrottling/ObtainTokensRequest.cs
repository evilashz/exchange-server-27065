using System;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000003 RID: 3
	internal class ObtainTokensRequest<T>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002288 File Offset: 0x00000488
		// (set) Token: 0x06000006 RID: 6 RVA: 0x00002290 File Offset: 0x00000490
		public T MailboxGuid { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000007 RID: 7 RVA: 0x00002299 File Offset: 0x00000499
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000022A1 File Offset: 0x000004A1
		public RequestedAction RequestedAction { get; private set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000022AA File Offset: 0x000004AA
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000022B2 File Offset: 0x000004B2
		public int RequestedTokenCount { get; private set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000022BB File Offset: 0x000004BB
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000022C3 File Offset: 0x000004C3
		public int TotalTokenCount { get; private set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000022CC File Offset: 0x000004CC
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000022D4 File Offset: 0x000004D4
		public string ClientHostName { get; private set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000022DD File Offset: 0x000004DD
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000022E5 File Offset: 0x000004E5
		public string ClientProcessName { get; private set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000011 RID: 17 RVA: 0x000022EE File Offset: 0x000004EE
		// (set) Token: 0x06000012 RID: 18 RVA: 0x000022F6 File Offset: 0x000004F6
		public string ClientType { get; private set; }

		// Token: 0x06000013 RID: 19 RVA: 0x000022FF File Offset: 0x000004FF
		public ObtainTokensRequest(T mailboxGuid, int requestedTokenCount, int totalTokenCount, RequestedAction requestedAction, string clientHostName, string clientProcessName, string clientType)
		{
			this.MailboxGuid = mailboxGuid;
			this.RequestedAction = requestedAction;
			this.RequestedTokenCount = requestedTokenCount;
			this.TotalTokenCount = totalTokenCount;
			this.ClientHostName = clientHostName;
			this.ClientProcessName = clientProcessName;
			this.ClientType = clientType;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x0000233C File Offset: 0x0000053C
		public ObtainTokensRequest(T mailboxGuid, int requestedTokenCount, int totalTokenCount)
		{
			this.MailboxGuid = mailboxGuid;
			this.RequestedTokenCount = requestedTokenCount;
			this.TotalTokenCount = totalTokenCount;
		}
	}
}
