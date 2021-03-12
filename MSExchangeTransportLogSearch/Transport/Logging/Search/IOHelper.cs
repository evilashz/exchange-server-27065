using System;
using System.IO;
using System.Security;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.LogSearch;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000038 RID: 56
	internal class IOHelper
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00008F8C File Offset: 0x0000718C
		public static bool TryIOOperation(IOHelper.FileIOOperation fileIOOperation, out object returnedObject)
		{
			Exception arg = null;
			returnedObject = null;
			for (int i = 0; i < 10; i++)
			{
				try
				{
					returnedObject = fileIOOperation();
					return true;
				}
				catch (IOException ex)
				{
					arg = ex;
					Thread.Sleep(1000);
				}
				catch (UnauthorizedAccessException ex2)
				{
					arg = ex2;
					break;
				}
				catch (SecurityException ex3)
				{
					arg = ex3;
					break;
				}
			}
			ExTraceGlobals.ServiceTracer.TraceError<Exception>(0L, "Cannot perform FileIOOperation: {0}", arg);
			return false;
		}

		// Token: 0x02000039 RID: 57
		// (Invoke) Token: 0x0600012E RID: 302
		public delegate object FileIOOperation();
	}
}
