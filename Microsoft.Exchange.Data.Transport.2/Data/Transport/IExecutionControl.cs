using System;

namespace Microsoft.Exchange.Data.Transport
{
	// Token: 0x0200006F RID: 111
	internal interface IExecutionControl
	{
		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000243 RID: 579
		string Id { get; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x06000244 RID: 580
		object CurrentEventSource { get; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000245 RID: 581
		object CurrentEventArgs { get; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000246 RID: 582
		string ExecutingAgentName { get; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000247 RID: 583
		string OutstandingEventTopic { get; }

		// Token: 0x06000248 RID: 584
		void HaltExecution();

		// Token: 0x06000249 RID: 585
		void OnStartAsyncAgent();

		// Token: 0x0600024A RID: 586
		void ResumeAgent();

		// Token: 0x0600024B RID: 587
		AgentAsyncCallback GetAgentAsyncCallback();
	}
}
