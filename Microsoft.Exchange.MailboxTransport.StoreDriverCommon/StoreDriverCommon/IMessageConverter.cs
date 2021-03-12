using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x02000013 RID: 19
	internal interface IMessageConverter
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600005C RID: 92
		string Description { get; }

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600005D RID: 93
		bool IsOutbound { get; }

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600005E RID: 94
		Trace Tracer { get; }

		// Token: 0x0600005F RID: 95
		void LogMessage(Exception exception);
	}
}
