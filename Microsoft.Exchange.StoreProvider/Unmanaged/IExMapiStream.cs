using System;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Mapi.Unmanaged
{
	// Token: 0x02000292 RID: 658
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal interface IExMapiStream : IExInterface, IDisposeTrackable, IDisposable
	{
		// Token: 0x06000C04 RID: 3076
		int Read(byte[] pv, uint cb, out uint cbRead);

		// Token: 0x06000C05 RID: 3077
		int Write(byte[] pv, int cb, out int pcbWritten);

		// Token: 0x06000C06 RID: 3078
		int Seek(long dlibMove, int dwOrigin, out long plibNewPosition);

		// Token: 0x06000C07 RID: 3079
		int SetSize(long libNewSize);

		// Token: 0x06000C08 RID: 3080
		int CopyTo(IFastStream pstm, long cb, IntPtr pcbRead, out long pcbWritten);

		// Token: 0x06000C09 RID: 3081
		int Commit(int grfCommitFlags);

		// Token: 0x06000C0A RID: 3082
		int Revert();

		// Token: 0x06000C0B RID: 3083
		int LockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000C0C RID: 3084
		int UnlockRegion(long libOffset, long cb, int dwLockType);

		// Token: 0x06000C0D RID: 3085
		int Stat(out STATSTG pstatstg, int grfStatFlag);

		// Token: 0x06000C0E RID: 3086
		int Clone(out IExInterface iStreamNew);
	}
}
