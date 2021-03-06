using System;
using System.ComponentModel;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000FC RID: 252
	internal static class DiskHelper
	{
		// Token: 0x06000A1E RID: 2590 RVA: 0x0002F144 File Offset: 0x0002D344
		public static Exception GetFreeSpace(string path, out ulong totalBytes, out ulong freeBytes)
		{
			Exception ex = null;
			ulong num = 0UL;
			totalBytes = 0UL;
			freeBytes = 0UL;
			if (!NativeMethods.GetDiskFreeSpaceEx(path, out freeBytes, out totalBytes, out num))
			{
				ex = new Win32Exception();
				ExTraceGlobals.ReplicaInstanceTracer.TraceError<string, Exception>(0L, "GetDiskFreeSpaceEx for path '{0}' failed with exception: {1}", path, ex);
			}
			return ex;
		}

		// Token: 0x06000A1F RID: 2591 RVA: 0x0002F184 File Offset: 0x0002D384
		public static int GetFreeSpacePercentage(ulong freeBytes, ulong totalBytes)
		{
			if (freeBytes == 0UL)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceError(0L, "freeBytes is 0.");
				return 0;
			}
			if (totalBytes == 0UL)
			{
				ExTraceGlobals.ReplicaInstanceTracer.TraceError(0L, "totalBytes cannot be 0.");
				return 0;
			}
			if (freeBytes > totalBytes)
			{
				string text = string.Format("freeBytes ({0}) cannot be > than totalBytes ({1}).", freeBytes, totalBytes);
				ExTraceGlobals.ReplicaInstanceTracer.TraceError(0L, text);
				DiagCore.AssertOrWatson(false, text, new object[0]);
				return 0;
			}
			return (int)(freeBytes / totalBytes * 100.0);
		}
	}
}
