using System;
using System.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ServicesServerTasks;
using Microsoft.Exchange.Management.IisTasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000084 RID: 132
	internal sealed class SetupTasksTrace
	{
		// Token: 0x0600040A RID: 1034 RVA: 0x0001106D File Offset: 0x0000F26D
		private SetupTasksTrace()
		{
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x00011075 File Offset: 0x0000F275
		[Conditional("DEBUG")]
		public static void Assert(bool condition, string format, params object[] parameters)
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x00011077 File Offset: 0x0000F277
		[Conditional("DEBUG")]
		public static void Assert(bool condition)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0001107C File Offset: 0x0000F27C
		internal static void InitializeTracing()
		{
			if (IisTaskTrace.VDirTracer == null)
			{
				lock (SetupTasksTrace.syncLock)
				{
					if (IisTaskTrace.VDirTracer == null)
					{
						IisTaskTrace.InitializeTracing(ExTraceGlobals.TaskTracer, ExTraceGlobals.NonTaskTracer);
					}
				}
			}
		}

		// Token: 0x04000220 RID: 544
		private static object syncLock = new object();
	}
}
