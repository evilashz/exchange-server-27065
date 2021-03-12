using System;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x0200001B RID: 27
	internal interface IRpcService : IDisposable
	{
		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000BC RID: 188
		string Name { get; }

		// Token: 0x060000BD RID: 189
		bool IsEnabled();

		// Token: 0x060000BE RID: 190
		void OnStartBegin();

		// Token: 0x060000BF RID: 191
		void OnStartEnd();

		// Token: 0x060000C0 RID: 192
		void OnStopBegin();

		// Token: 0x060000C1 RID: 193
		void OnStopEnd();

		// Token: 0x060000C2 RID: 194
		void HandleUnexpectedExceptionOnStart(Exception ex);

		// Token: 0x060000C3 RID: 195
		void HandleUnexpectedExceptionOnStop(Exception ex);
	}
}
