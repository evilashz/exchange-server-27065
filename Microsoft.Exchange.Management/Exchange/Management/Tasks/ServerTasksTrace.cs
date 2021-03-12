using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServicesServerTasks;
using Microsoft.Exchange.Management.IisTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076E RID: 1902
	internal sealed class ServerTasksTrace
	{
		// Token: 0x06004356 RID: 17238 RVA: 0x00114698 File Offset: 0x00112898
		private ServerTasksTrace()
		{
		}

		// Token: 0x06004357 RID: 17239 RVA: 0x001146A0 File Offset: 0x001128A0
		internal static void InitializeTracing()
		{
			if (IisTaskTrace.VDirTracer == null)
			{
				lock (ServerTasksTrace.syncLock)
				{
					if (IisTaskTrace.VDirTracer == null)
					{
						IisTaskTrace.InitializeTracing(ExTraceGlobals.TaskTracer, ExTraceGlobals.NonTaskTracer);
					}
				}
			}
		}

		// Token: 0x06004358 RID: 17240 RVA: 0x001146F8 File Offset: 0x001128F8
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x06004359 RID: 17241 RVA: 0x001146FA File Offset: 0x001128FA
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x040029FB RID: 10747
		private static object syncLock = new object();
	}
}
