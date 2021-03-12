using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000043 RID: 67
	internal sealed class CompositeLogger : ILogger, IEnumerable<ILogger>, IEnumerable
	{
		// Token: 0x060001A3 RID: 419 RVA: 0x0000604F File Offset: 0x0000424F
		public void Add(ILogger logger)
		{
			ArgumentValidator.ThrowIfNull("logger", logger);
			this.innerLoggers.Add(logger);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x00006068 File Offset: 0x00004268
		public void TaskStarted(ITaskDescriptor task)
		{
			foreach (ILogger logger in this.innerLoggers)
			{
				logger.TaskStarted(task);
			}
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000060BC File Offset: 0x000042BC
		public void TaskCompleted(ITaskDescriptor task, TaskResult result)
		{
			foreach (ILogger logger in this.innerLoggers)
			{
				logger.TaskCompleted(task, result);
			}
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x00006110 File Offset: 0x00004310
		public IEnumerator<ILogger> GetEnumerator()
		{
			return this.innerLoggers.GetEnumerator();
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00006122 File Offset: 0x00004322
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040000CC RID: 204
		private readonly List<ILogger> innerLoggers = new List<ILogger>();
	}
}
