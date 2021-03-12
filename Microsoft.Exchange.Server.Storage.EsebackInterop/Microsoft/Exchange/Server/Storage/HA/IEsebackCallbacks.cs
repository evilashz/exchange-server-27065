using System;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200000D RID: 13
	public interface IEsebackCallbacks
	{
		// Token: 0x06000095 RID: 149
		int PrepareInstanceForBackup(IntPtr context, JET_INSTANCE instance, IntPtr reserved);

		// Token: 0x06000096 RID: 150
		int DoneWithInstanceForBackup(IntPtr context, JET_INSTANCE instance, uint complete, IntPtr reserved);

		// Token: 0x06000097 RID: 151
		int GetDatabasesInfo(IntPtr context, out MINSTANCE_BACKUP_INFO[] instances, uint reserved);

		// Token: 0x06000098 RID: 152
		int IsSGReplicated(IntPtr context, JET_INSTANCE instance, out bool isReplicated, out Guid guid, out MLOGSHIP_INFO[] info);

		// Token: 0x06000099 RID: 153
		int ServerAccessCheck();

		// Token: 0x0600009A RID: 154
		int Trace(string data);
	}
}
