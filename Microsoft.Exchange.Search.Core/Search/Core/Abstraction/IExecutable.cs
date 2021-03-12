using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200001D RID: 29
	internal interface IExecutable : IDiagnosable, IDisposable
	{
		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000086 RID: 134
		string InstanceName { get; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000087 RID: 135
		ICancelableAsyncResult AsyncResult { get; }

		// Token: 0x06000088 RID: 136
		IAsyncResult BeginExecute(AsyncCallback callback, object state);

		// Token: 0x06000089 RID: 137
		void EndExecute(IAsyncResult asyncResult);

		// Token: 0x0600008A RID: 138
		void CancelExecute();
	}
}
