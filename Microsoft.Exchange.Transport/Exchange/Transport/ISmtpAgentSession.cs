using System;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000308 RID: 776
	internal interface ISmtpAgentSession
	{
		// Token: 0x060021D6 RID: 8662
		IAsyncResult BeginNoEvent(AsyncCallback callback, object state);

		// Token: 0x060021D7 RID: 8663
		IAsyncResult BeginRaiseEvent(string eventTopic, object eventSource, object eventArgs, AsyncCallback callback, object state);

		// Token: 0x060021D8 RID: 8664
		SmtpResponse EndRaiseEvent(IAsyncResult ar);

		// Token: 0x060021D9 RID: 8665
		Task<SmtpResponse> RaiseEventAsync(string eventTopic, object eventSource, object eventArgs);

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x060021DA RID: 8666
		SmtpSession SessionSource { get; }

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x060021DB RID: 8667
		AgentLatencyTracker LatencyTracker { get; }

		// Token: 0x060021DC RID: 8668
		void Close();
	}
}
