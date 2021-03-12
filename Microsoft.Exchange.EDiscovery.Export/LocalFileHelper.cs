using System;
using System.IO;
using System.Security;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000015 RID: 21
	internal static class LocalFileHelper
	{
		// Token: 0x060000AF RID: 175 RVA: 0x000031F8 File Offset: 0x000013F8
		public static void CallFileOperation(Action action, ExportErrorType errorType)
		{
			Exception ex = null;
			try
			{
				action();
			}
			catch (IOException ex2)
			{
				ex = ex2;
			}
			catch (SecurityException ex3)
			{
				ex = ex3;
			}
			catch (UnauthorizedAccessException ex4)
			{
				ex = ex4;
			}
			if (ex != null)
			{
				Tracer.TraceError("LocalFileHelper.CallFileOperation: Failed with file operation, Exception: {0}", new object[]
				{
					ex
				});
				throw new ExportException(errorType, ex);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00003290 File Offset: 0x00001490
		public static void RemoveFile(string filePath, ExportErrorType errorType)
		{
			LocalFileHelper.CallFileOperation(delegate
			{
				if (File.Exists(filePath))
				{
					File.Delete(filePath);
				}
			}, errorType);
		}
	}
}
