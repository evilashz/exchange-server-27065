using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000004 RID: 4
	internal class StoreDriverAgentRaisedException : LocalizedException
	{
		// Token: 0x0600000B RID: 11 RVA: 0x0000222A File Offset: 0x0000042A
		public StoreDriverAgentRaisedException(Exception actualAgentException) : base(new LocalizedString("Wrapper class for exceptions that agents throw"), actualAgentException)
		{
		}

		// Token: 0x0600000C RID: 12 RVA: 0x0000223D File Offset: 0x0000043D
		public StoreDriverAgentRaisedException(string agentName, Exception actualAgentException) : this(actualAgentException)
		{
			this.AgentName = agentName;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000224D File Offset: 0x0000044D
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002255 File Offset: 0x00000455
		public string AgentName { get; private set; }
	}
}
