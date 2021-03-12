using System;
using System.IO;
using System.Security;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x020000F9 RID: 249
	internal static class FileCleanup
	{
		// Token: 0x06000A04 RID: 2564 RVA: 0x0002EBB0 File Offset: 0x0002CDB0
		public static void TryDelete(string fName)
		{
			Exception ex = FileCleanup.Delete(fName);
			if (ex != null)
			{
				ExTraceGlobals.LogCopyTracer.TraceError<Exception, string>(0L, "FileCleanup: Ignoring exception during file delete for '{1}': {0}", ex, fName);
			}
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x0002EBDC File Offset: 0x0002CDDC
		public static Exception Delete(string fileFullPath)
		{
			Exception result = null;
			try
			{
				if (File.Exists(fileFullPath))
				{
					File.Delete(fileFullPath);
				}
			}
			catch (FileNotFoundException ex)
			{
				result = ex;
			}
			catch (IOException ex2)
			{
				result = ex2;
			}
			catch (SecurityException ex3)
			{
				result = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				result = ex4;
			}
			return result;
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x0002EC48 File Offset: 0x0002CE48
		public static void DisposeProperly(FileStream wfs)
		{
			try
			{
				wfs.Dispose();
			}
			catch (IOException arg)
			{
				ExTraceGlobals.LogCopyTracer.TraceError<IOException>(0L, "DisposeProperly: Ignoring exception during file close: {0}", arg);
			}
		}
	}
}
