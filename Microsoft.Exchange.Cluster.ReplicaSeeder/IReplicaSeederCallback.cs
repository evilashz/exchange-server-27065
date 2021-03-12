using System;
using System.Runtime.InteropServices;

// Token: 0x02000002 RID: 2
public interface IReplicaSeederCallback
{
	// Token: 0x06000072 RID: 114
	void ReportProgress(string edbName, long edbSize, long bytesRead, long bytesWritten);

	// Token: 0x06000073 RID: 115
	[return: MarshalAs(UnmanagedType.U1)]
	bool IsBackupCancelled();
}
