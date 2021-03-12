using System;
using System.Runtime.InteropServices;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000020 RID: 32
	internal struct NATIVE_ESEBACK_CALLBACKS_IMPL
	{
		// Token: 0x06000077 RID: 119 RVA: 0x000030F8 File Offset: 0x000012F8
		internal NATIVE_ESEBACK_CALLBACKS_IMPL(ref NATIVE_ESEBACK_CALLBACKS native)
		{
			this.pfnPrepareInstance = Marshal.GetFunctionPointerForDelegate(native.pfnPrepareInstance);
			this.pfnDoneWithInstance = Marshal.GetFunctionPointerForDelegate(native.pfnDoneWithInstance);
			this.pfnGetDatabasesInfo = Marshal.GetFunctionPointerForDelegate(native.pfnGetDatabasesInfo);
			this.pfnFreeDatabasesInfo = Marshal.GetFunctionPointerForDelegate(native.pfnFreeDatabasesInfo);
			this.pfnIsSGReplicated = Marshal.GetFunctionPointerForDelegate(native.pfnIsSGReplicated);
			this.pfnFreeShipLogInfo = Marshal.GetFunctionPointerForDelegate(native.pfnFreeShipLogInfo);
			this.pfnServerAccessCheck = Marshal.GetFunctionPointerForDelegate(native.pfnServerAccessCheck);
			this.pfnTrace = Marshal.GetFunctionPointerForDelegate(native.pfnTrace);
		}

		// Token: 0x04000055 RID: 85
		public IntPtr pfnPrepareInstance;

		// Token: 0x04000056 RID: 86
		public IntPtr pfnDoneWithInstance;

		// Token: 0x04000057 RID: 87
		public IntPtr pfnGetDatabasesInfo;

		// Token: 0x04000058 RID: 88
		public IntPtr pfnFreeDatabasesInfo;

		// Token: 0x04000059 RID: 89
		public IntPtr pfnIsSGReplicated;

		// Token: 0x0400005A RID: 90
		public IntPtr pfnFreeShipLogInfo;

		// Token: 0x0400005B RID: 91
		public IntPtr pfnServerAccessCheck;

		// Token: 0x0400005C RID: 92
		public IntPtr pfnTrace;
	}
}
