using System;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;

namespace Microsoft.Exchange.Security.RightsManagement.StructuredStorage
{
	// Token: 0x020009A8 RID: 2472
	[Guid("0000000A-0000-0000-C000-000000000046")]
	[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	[ComImport]
	internal interface ILockBytes
	{
		// Token: 0x0600359A RID: 13722
		void ReadAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] [Out] byte[] buffer, int count, out int read);

		// Token: 0x0600359B RID: 13723
		void WriteAt(ulong offset, [MarshalAs(UnmanagedType.LPArray, SizeParamIndex = 2)] byte[] buffer, int count, out int written);

		// Token: 0x0600359C RID: 13724
		void Flush();

		// Token: 0x0600359D RID: 13725
		void SetSize(ulong cb);

		// Token: 0x0600359E RID: 13726
		void LockRegion(ulong libOffset, ulong cb, int dwLockType);

		// Token: 0x0600359F RID: 13727
		void UnlockRegion(ulong libOffset, ulong cb, int dwLockType);

		// Token: 0x060035A0 RID: 13728
		void Stat(out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg, [MarshalAs(UnmanagedType.I4)] [In] STATFLAG grfStatFlag);
	}
}
