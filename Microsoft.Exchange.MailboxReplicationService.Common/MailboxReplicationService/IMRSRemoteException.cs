using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000187 RID: 391
	public interface IMRSRemoteException
	{
		// Token: 0x17000484 RID: 1156
		// (get) Token: 0x06000E97 RID: 3735
		// (set) Token: 0x06000E98 RID: 3736
		string OriginalFailureType { get; set; }

		// Token: 0x17000485 RID: 1157
		// (get) Token: 0x06000E99 RID: 3737
		// (set) Token: 0x06000E9A RID: 3738
		WellKnownException[] WKEClasses { get; set; }

		// Token: 0x17000486 RID: 1158
		// (get) Token: 0x06000E9B RID: 3739
		// (set) Token: 0x06000E9C RID: 3740
		int MapiLowLevelError { get; set; }

		// Token: 0x17000487 RID: 1159
		// (get) Token: 0x06000E9D RID: 3741
		// (set) Token: 0x06000E9E RID: 3742
		string RemoteStackTrace { get; set; }
	}
}
