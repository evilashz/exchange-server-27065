using System;
using System.Net;

namespace Microsoft.Exchange.ProcessManager
{
	// Token: 0x0200080B RID: 2059
	internal interface ITcpListener
	{
		// Token: 0x17000B6E RID: 2926
		// (set) Token: 0x06002B38 RID: 11064
		int MaxConnectionRate { set; }

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06002B39 RID: 11065
		// (set) Token: 0x06002B3A RID: 11066
		bool ProcessStopping { get; set; }

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06002B3B RID: 11067
		IPEndPoint[] CurrentBindings { get; }

		// Token: 0x06002B3C RID: 11068
		bool IsListening();

		// Token: 0x06002B3D RID: 11069
		void SetBindings(IPEndPoint[] newBindings, bool invokeDelegateOnFailure);

		// Token: 0x06002B3E RID: 11070
		void StartListening(bool invokeDelegateOnFailure);

		// Token: 0x06002B3F RID: 11071
		void StopListening();

		// Token: 0x06002B40 RID: 11072
		void Shutdown();
	}
}
