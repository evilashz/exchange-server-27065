using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics.CmdletInfra;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020000A7 RID: 167
	internal class TaskIOPipeline : ITaskIOPipeline
	{
		// Token: 0x060006BA RID: 1722 RVA: 0x00018A00 File Offset: 0x00016C00
		internal TaskIOPipeline(TaskContext context)
		{
			this.context = context;
		}

		// Token: 0x060006BB RID: 1723 RVA: 0x00018A1A File Offset: 0x00016C1A
		internal void PrependTaskIOPipelineHandler(ITaskIOPipeline pipeline)
		{
			this.pipelines.Insert(0, pipeline);
		}

		// Token: 0x060006BC RID: 1724 RVA: 0x00018A33 File Offset: 0x00016C33
		public bool WriteVerbose(LocalizedString input, out LocalizedString output)
		{
			return this.ExecutePipeline<LocalizedString>(delegate(ITaskIOPipeline p, LocalizedString i, out LocalizedString o)
			{
				return p.WriteVerbose(i, out o);
			}, "WriteVerbose", input, out output);
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00018A69 File Offset: 0x00016C69
		public bool WriteDebug(LocalizedString input, out LocalizedString output)
		{
			return this.ExecutePipeline<LocalizedString>(delegate(ITaskIOPipeline p, LocalizedString i, out LocalizedString o)
			{
				return p.WriteDebug(i, out o);
			}, "WriteDebug", input, out output);
		}

		// Token: 0x060006BE RID: 1726 RVA: 0x00018AB0 File Offset: 0x00016CB0
		public bool WriteWarning(LocalizedString input, string helperUrl, out LocalizedString output)
		{
			return this.ExecutePipeline<LocalizedString>(delegate(ITaskIOPipeline p, LocalizedString i, out LocalizedString o)
			{
				return p.WriteWarning(i, helperUrl, out o);
			}, "WriteWarning", input, out output);
		}

		// Token: 0x060006BF RID: 1727 RVA: 0x00018AED File Offset: 0x00016CED
		public bool WriteError(TaskErrorInfo input, out TaskErrorInfo output)
		{
			return this.ExecutePipeline<TaskErrorInfo>(delegate(ITaskIOPipeline p, TaskErrorInfo i, out TaskErrorInfo o)
			{
				return p.WriteError(i, out o);
			}, "WriteError", input, out output);
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00018B23 File Offset: 0x00016D23
		public bool WriteObject(object input, out object output)
		{
			return this.ExecutePipeline<object>(delegate(ITaskIOPipeline p, object i, out object o)
			{
				return p.WriteObject(i, out o);
			}, "WriteObject", input, out output);
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00018B59 File Offset: 0x00016D59
		public bool WriteProgress(ExProgressRecord input, out ExProgressRecord output)
		{
			return this.ExecutePipeline<ExProgressRecord>(delegate(ITaskIOPipeline p, ExProgressRecord i, out ExProgressRecord o)
			{
				return p.WriteProgress(i, out o);
			}, "WriteProgress", input, out output);
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00018BF0 File Offset: 0x00016DF0
		public bool ShouldContinue(string query, string caption, ref bool yesToAll, ref bool noToAll, out bool? output)
		{
			bool result = true;
			output = null;
			using (List<ITaskIOPipeline>.Enumerator enumerator = this.pipelines.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					ITaskIOPipeline pipeline = enumerator.Current;
					string text = base.GetType().Name + ".ShouldContinue";
					using (new CmdletMonitoredScope(this.context.UniqueId, "TaskModuleLatency", text, LoggerHelper.CmdletPerfMonitors))
					{
						ICriticalFeature feature = pipeline as ICriticalFeature;
						bool yesToAllFromDelegate = yesToAll;
						bool noToAllFromDelegate = noToAll;
						bool? outputFromDelegate = output;
						feature.Execute(delegate
						{
							result = pipeline.ShouldContinue(query, caption, ref yesToAllFromDelegate, ref noToAllFromDelegate, out outputFromDelegate);
						}, this.context, text);
						yesToAll = yesToAllFromDelegate;
						noToAll = noToAllFromDelegate;
						output = outputFromDelegate;
					}
					if (!result)
					{
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00018D6C File Offset: 0x00016F6C
		public bool ShouldProcess(string verboseDescription, string verboseWarning, string caption, out bool? output)
		{
			return this.ExecutePipeline<bool?>(delegate(ITaskIOPipeline p, bool? i, out bool? o)
			{
				return p.ShouldProcess(verboseDescription, verboseWarning, caption, out o);
			}, "ShouldProcess", null, out output);
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00018E08 File Offset: 0x00017008
		private bool ExecutePipeline<T>(TaskIOPipeline.PipelineExecuter<T> pipelineExecuter, string methodName, T input, out T output)
		{
			bool result = true;
			output = input;
			using (List<ITaskIOPipeline>.Enumerator enumerator = this.pipelines.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					TaskIOPipeline.<>c__DisplayClass1a<T> CS$<>8__locals2 = new TaskIOPipeline.<>c__DisplayClass1a<T>();
					CS$<>8__locals2.pipeline = enumerator.Current;
					ICriticalFeature feature = CS$<>8__locals2.pipeline as ICriticalFeature;
					T outputFromExecuter = input;
					string text = base.GetType().Name + "." + methodName;
					using (new CmdletMonitoredScope(this.context.UniqueId, "TaskModuleLatency", text, LoggerHelper.CmdletPerfMonitors))
					{
						feature.Execute(delegate
						{
							result = pipelineExecuter(CS$<>8__locals2.pipeline, input, out outputFromExecuter);
						}, this.context, text);
					}
					output = outputFromExecuter;
					if (!result)
					{
						break;
					}
					input = output;
				}
			}
			return result;
		}

		// Token: 0x04000177 RID: 375
		private readonly List<ITaskIOPipeline> pipelines = new List<ITaskIOPipeline>();

		// Token: 0x04000178 RID: 376
		private readonly TaskContext context;

		// Token: 0x020000A8 RID: 168
		// (Invoke) Token: 0x060006CB RID: 1739
		private delegate bool PipelineExecuter<T>(ITaskIOPipeline pipeline, T input, out T output);
	}
}
