using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.CertificateAuthentication;
using Microsoft.Exchange.Diagnostics.FaultInjection;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200028E RID: 654
	internal class TaskFaultInjectionModule : ITaskModule, ICriticalFeature
	{
		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x06001685 RID: 5765 RVA: 0x0005547C File Offset: 0x0005367C
		// (set) Token: 0x06001686 RID: 5766 RVA: 0x00055484 File Offset: 0x00053684
		private protected TaskContext CurrentTaskContext { protected get; private set; }

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00055490 File Offset: 0x00053690
		private static FaultInjectionTrace FaultInjectionTracer
		{
			get
			{
				if (TaskFaultInjectionModule.faultInjectionTracer == null)
				{
					lock (TaskFaultInjectionModule.lockObject)
					{
						if (TaskFaultInjectionModule.faultInjectionTracer == null)
						{
							FaultInjectionTrace faultInjectionTrace = ExTraceGlobals.FaultInjectionTracer;
							faultInjectionTrace.RegisterExceptionInjectionCallback(new ExceptionInjectionCallback(TaskFaultInjectionModule.Callback));
							TaskFaultInjectionModule.faultInjectionTracer = faultInjectionTrace;
						}
					}
				}
				return TaskFaultInjectionModule.faultInjectionTracer;
			}
		}

		// Token: 0x06001688 RID: 5768 RVA: 0x000554FC File Offset: 0x000536FC
		public TaskFaultInjectionModule(TaskContext context)
		{
			this.CurrentTaskContext = context;
		}

		// Token: 0x06001689 RID: 5769 RVA: 0x0005550B File Offset: 0x0005370B
		bool ICriticalFeature.IsCriticalException(Exception ex)
		{
			return typeof(ADTransientException).IsInstanceOfType(ex);
		}

		// Token: 0x0600168A RID: 5770 RVA: 0x00055522 File Offset: 0x00053722
		public virtual void Init(ITaskEvent task)
		{
			task.PreInit += this.InitFaultInjection;
		}

		// Token: 0x0600168B RID: 5771 RVA: 0x00055536 File Offset: 0x00053736
		public void Dispose()
		{
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x00055538 File Offset: 0x00053738
		private static Exception Callback(string exceptionType)
		{
			Exception result = null;
			if (exceptionType != null)
			{
				if (typeof(ADTransientException).FullName.Equals(exceptionType))
				{
					return new ADTransientException(new LocalizedString("fault injection! ADTransientException"));
				}
				if (typeof(ApplicationException).FullName.Equals(exceptionType))
				{
					return new ApplicationException(new LocalizedString("fault injection!ApplicationException"));
				}
			}
			return result;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0005559E File Offset: 0x0005379E
		private void InitFaultInjection(object sender, EventArgs e)
		{
			TaskFaultInjectionModule.FaultInjectionTracer.TraceTest(3859164477U);
		}

		// Token: 0x040006E1 RID: 1761
		private static object lockObject = new object();

		// Token: 0x040006E2 RID: 1762
		private static FaultInjectionTrace faultInjectionTracer = null;
	}
}
