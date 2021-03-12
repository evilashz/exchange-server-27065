using System;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.Rpc.MailboxSearch
{
	// Token: 0x0200028B RID: 651
	internal abstract class MailboxSearchRpcServer : RpcServerBase
	{
		// Token: 0x06000C20 RID: 3104
		public abstract SearchErrorInfo Start(SearchId searchId, Guid ownerGuid);

		// Token: 0x06000C21 RID: 3105
		public abstract SearchErrorInfo GetStatus(SearchId searchId, out SearchStatus searchStatus);

		// Token: 0x06000C22 RID: 3106
		public abstract SearchErrorInfo Abort(SearchId searchId, Guid userGuid);

		// Token: 0x06000C23 RID: 3107
		public abstract SearchErrorInfo Remove(SearchId searchId, [MarshalAs(UnmanagedType.U1)] bool removeLogs);

		// Token: 0x06000C24 RID: 3108
		public abstract SearchErrorInfo StartEx(SearchId searchId, string ownerId);

		// Token: 0x06000C25 RID: 3109
		public abstract SearchErrorInfo AbortEx(SearchId searchId, string userId);

		// Token: 0x06000C26 RID: 3110
		public abstract SearchErrorInfo UpdateStatus(SearchId searchId);

		// Token: 0x06000C27 RID: 3111 RVA: 0x0002B400 File Offset: 0x0002A800
		public MailboxSearchRpcServer()
		{
		}

		// Token: 0x04000D2E RID: 3374
		public static IntPtr RpcIntfHandle = (IntPtr)<Module>.IMailboxSearch_v1_0_s_ifspec;
	}
}
