using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Exchange.Assistants;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MailboxAssistants.Assistants.DirectoryProcessorAssistant;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.GroupMetricsGenerator;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMDtmfMapGenerator;
using Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant.UMGrammarGenerator;
using Microsoft.Exchange.UM;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.DirectoryProcessorAssistant
{
	// Token: 0x0200019A RID: 410
	internal sealed class DirectoryProcessorAssistant : TimeBasedAssistant, ITimeBasedAssistant, IAssistantBase
	{
		// Token: 0x06001019 RID: 4121 RVA: 0x0005E078 File Offset: 0x0005C278
		public DirectoryProcessorAssistant(DatabaseInfo databaseInfo, LocalizedString name, string nonLocalizedName) : base(databaseInfo, name, nonLocalizedName)
		{
		}

		// Token: 0x0600101A RID: 4122 RVA: 0x0005E08E File Offset: 0x0005C28E
		public void OnWorkCycleCheckpoint()
		{
		}

		// Token: 0x0600101B RID: 4123 RVA: 0x0005E090 File Offset: 0x0005C290
		public override List<MailboxData> GetMailboxesToProcess()
		{
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Entering DirectoryProcessorAssistant.GetMailboxesToProcess", new object[0]);
			Guid guid = base.DatabaseInfo.Guid;
			return this.GetMailboxesToProcess(guid);
		}

		// Token: 0x0600101C RID: 4124 RVA: 0x0005E0C8 File Offset: 0x0005C2C8
		public override AssistantTaskContext InitialStep(AssistantTaskContext context)
		{
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Entering DirectoryProcessorAssistant.InitialStep", new object[0]);
			DirectoryProcessorMailboxData directoryProcessorMailboxData = context.MailboxData as DirectoryProcessorMailboxData;
			AssistantTaskContext result;
			try
			{
				Queue<TaskQueueItem> queue = new Queue<TaskQueueItem>();
				RunData runData = this.CreateRunData(directoryProcessorMailboxData);
				GrammarGenerator.CleanUpOldGrammarRuns(runData, DirectoryProcessorAssistant.Tracer);
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DirectoryProcessorStarted, null, new object[]
				{
					runData.TenantId,
					runData.MailboxGuid,
					runData.RunId
				});
				List<DirectoryProcessorBaseTask> generators = this.GetGenerators(runData);
				List<DirectoryProcessorBaseTask> list = this.FilterGenerators(generators, runData, RecipientType.User);
				List<DirectoryProcessorBaseTask> list2 = this.FilterGenerators(generators, runData, RecipientType.Group);
				if (list.Count > 0)
				{
					queue.Enqueue(new TaskQueueItem(ADCrawler.Create(runData, RecipientType.User), RecipientType.User));
				}
				foreach (DirectoryProcessorBaseTask task in list)
				{
					queue.Enqueue(new TaskQueueItem(task, RecipientType.User));
				}
				if (list2.Count > 0)
				{
					queue.Enqueue(new TaskQueueItem(ADCrawler.Create(runData, RecipientType.Group), RecipientType.Group));
				}
				foreach (DirectoryProcessorBaseTask task2 in list2)
				{
					queue.Enqueue(new TaskQueueItem(task2, RecipientType.Group));
				}
				DirectoryProcessorBaseTaskContext directoryProcessorBaseTaskContext = new DirectoryProcessorBaseTaskContext(context.MailboxData, context.Job, queue, new AssistantStep(this.DoTask), TaskStatus.NoError, runData, new List<DirectoryProcessorBaseTask>());
				result = directoryProcessorBaseTaskContext;
			}
			catch (Exception ex)
			{
				string tenantIdentifiableDN = RunData.GetTenantIdentifiableDN(directoryProcessorMailboxData.OrgId);
				Utilities.ErrorTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.InitialStep, Exception='{0}' for '{1}' in database '{2}'", new object[]
				{
					ex,
					tenantIdentifiableDN,
					directoryProcessorMailboxData.DatabaseGuid
				});
				UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DirectoryProcessorInitialStepEncounteredException, null, new object[]
				{
					tenantIdentifiableDN,
					directoryProcessorMailboxData.DatabaseGuid,
					CommonUtil.ToEventLogString(ex)
				});
				if (ex is IOException)
				{
					throw new SkipException(ex);
				}
				throw;
			}
			return result;
		}

		// Token: 0x0600101D RID: 4125 RVA: 0x0005E330 File Offset: 0x0005C530
		public AssistantTaskContext DoTask(AssistantTaskContext context)
		{
			DirectoryProcessorBaseTaskContext directoryProcessorBaseTaskContext = context as DirectoryProcessorBaseTaskContext;
			ExAssert.RetailAssert(null != directoryProcessorBaseTaskContext, "Check InitialStep that it returns DirectoryProcessorBaseTaskContext. ");
			ExAssert.RetailAssert(null != directoryProcessorBaseTaskContext.TaskQueue, "Check InitialStep that it assigns TaskQueue to DirectoryProcessorBaseTaskContext. ");
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Entering DirectoryProcessorAssistant.DoTask. taskContext='{0}' taskQueue size='{1}'", new object[]
			{
				directoryProcessorBaseTaskContext.ClassName,
				directoryProcessorBaseTaskContext.TaskQueue.Count
			});
			if (directoryProcessorBaseTaskContext.TaskQueue.Count > 0)
			{
				TaskQueueItem taskQueueItem = directoryProcessorBaseTaskContext.TaskQueue.Peek();
				ExAssert.RetailAssert(null != taskQueueItem, "Should not have added null taskQueueItem into queue. Check enqueue logic. ");
				ExAssert.RetailAssert(null != taskQueueItem.Task, "Should not have added null task into queue. Check enqueue logic. ");
				ExAssert.RetailAssert(taskQueueItem.Task.RunData == directoryProcessorBaseTaskContext.RunData, "They should reference to same RunData object. Check DirectoryProcessorAssistant.InitialStep. ");
				AssistantTaskContext assistantTaskContext = null;
				try
				{
					Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.DoTask calls Task.DoChunkWork Task='{0}' TaskRecipientType='{1}'", new object[]
					{
						taskQueueItem.Task.ClassName,
						taskQueueItem.TaskRecipientType.ToString()
					});
					assistantTaskContext = taskQueueItem.Task.DoChunk(directoryProcessorBaseTaskContext, taskQueueItem.TaskRecipientType);
				}
				catch (Exception e)
				{
					this.LogException(taskQueueItem.Task.RunData, e, taskQueueItem.Task, directoryProcessorBaseTaskContext);
					if (taskQueueItem.Task.ShouldWatson(e))
					{
						throw;
					}
					assistantTaskContext = null;
				}
				if (assistantTaskContext == null)
				{
					Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.DoTask Task='{0}' TaskRecipientType='{1}' is done. ", new object[]
					{
						taskQueueItem.Task.ClassName,
						taskQueueItem.TaskRecipientType.ToString()
					});
					if (taskQueueItem.Task.ShouldDeferFinalize)
					{
						if (!directoryProcessorBaseTaskContext.DeferredFinalizeTasks.Contains(taskQueueItem.Task))
						{
							directoryProcessorBaseTaskContext.DeferredFinalizeTasks.Add(taskQueueItem.Task);
							Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Task {0} TaskRecipientType='{1}' is added to deferred finalize list. ", new object[]
							{
								taskQueueItem.Task.ClassName,
								taskQueueItem.TaskRecipientType.ToString()
							});
						}
					}
					else
					{
						this.FinalizeTask(taskQueueItem.Task, directoryProcessorBaseTaskContext);
					}
					directoryProcessorBaseTaskContext.TaskQueue.Dequeue();
					assistantTaskContext = new DirectoryProcessorBaseTaskContext(directoryProcessorBaseTaskContext.MailboxData, directoryProcessorBaseTaskContext.Job, directoryProcessorBaseTaskContext.TaskQueue, directoryProcessorBaseTaskContext.Step, directoryProcessorBaseTaskContext.TaskStatus, directoryProcessorBaseTaskContext.RunData, directoryProcessorBaseTaskContext.DeferredFinalizeTasks);
				}
				return assistantTaskContext;
			}
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant has {0} deferred finalize tasks. Finalize them. ", new object[]
			{
				directoryProcessorBaseTaskContext.DeferredFinalizeTasks.Count
			});
			foreach (DirectoryProcessorBaseTask task in directoryProcessorBaseTaskContext.DeferredFinalizeTasks)
			{
				this.FinalizeTask(task, directoryProcessorBaseTaskContext);
			}
			directoryProcessorBaseTaskContext.DeferredFinalizeTasks.Clear();
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant has done all tasks. ", new object[0]);
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DirectoryProcessorCompleted, null, new object[]
			{
				directoryProcessorBaseTaskContext.RunData.TenantId,
				directoryProcessorBaseTaskContext.RunData.MailboxGuid,
				directoryProcessorBaseTaskContext.RunData.RunId
			});
			return null;
		}

		// Token: 0x0600101E RID: 4126 RVA: 0x0005E674 File Offset: 0x0005C874
		protected override void InvokeInternal(InvokeArgs invokeArgs, List<KeyValuePair<string, object>> customDataToLog)
		{
		}

		// Token: 0x0600101F RID: 4127 RVA: 0x0005E678 File Offset: 0x0005C878
		private List<DirectoryProcessorBaseTask> GetGenerators(RunData runData)
		{
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Entering DirectoryProcessorAssistant.GetGenerators", new object[0]);
			List<DirectoryProcessorBaseTask> list = new List<DirectoryProcessorBaseTask>();
			if (Utilities.TestFlag == TestFlag.Off || TestFlag.GroupMetrics == Utilities.TestFlag)
			{
				list.Add(new GroupMetricsGenerator(runData, this.groupMetricsMailboxes));
			}
			if (Utilities.TestFlag == TestFlag.Off || TestFlag.UnifiedMessaging == Utilities.TestFlag)
			{
				list.Add(new GrammarGenerator(runData, this.grammarGeneratorMailboxes));
				list.Add(new DtmfMapGenerator(runData, this.grammarGeneratorMailboxes));
			}
			return list;
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x0005E6F8 File Offset: 0x0005C8F8
		private List<DirectoryProcessorBaseTask> FilterGenerators(List<DirectoryProcessorBaseTask> generators, RunData runData, RecipientType type)
		{
			Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Entering DirectoryProcessorAssistant.FilterGenerators", new object[0]);
			List<DirectoryProcessorBaseTask> list = new List<DirectoryProcessorBaseTask>(generators.Count);
			foreach (DirectoryProcessorBaseTask directoryProcessorBaseTask in generators)
			{
				if (directoryProcessorBaseTask.ShouldRun(type))
				{
					list.Add(directoryProcessorBaseTask);
				}
			}
			return list;
		}

		// Token: 0x06001021 RID: 4129 RVA: 0x0005E774 File Offset: 0x0005C974
		private RunData CreateRunData(DirectoryProcessorMailboxData mailboxData)
		{
			ValidateArgument.NotNull(mailboxData, "mailboxData");
			RunData runData = new RunData(Guid.NewGuid(), mailboxData, new ThrowIfShuttingDown(this.ThrowIfShuttingDown));
			runData.CreateRunFolder();
			return runData;
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0005E7AC File Offset: 0x0005C9AC
		private void LogException(RunData runData, Exception e, DirectoryProcessorBaseTask task, DirectoryProcessorBaseTaskContext taskContext)
		{
			Utilities.ErrorTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.DoTask, Exception='{0}' for Org '{1}' Run '{2}' Task name '{3}' Error message '{4}'", new object[]
			{
				e,
				runData.OrgId,
				runData.RunId,
				task.ClassName,
				e.Message
			});
			UmGlobals.ExEvent.LogEvent(UMEventLogConstants.Tuple_DirectoryProcessorTaskThrewException, null, new object[]
			{
				runData.TenantId,
				runData.RunId,
				task.ClassName,
				CommonUtil.ToEventLogString(e)
			});
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0005E840 File Offset: 0x0005CA40
		private void FinalizeTask(DirectoryProcessorBaseTask task, DirectoryProcessorBaseTaskContext taskContext)
		{
			try
			{
				task.FinalizeMe(taskContext);
				Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Task {0} is finalized. ", new object[]
				{
					task.ClassName
				});
			}
			catch (Exception e)
			{
				this.LogException(task.RunData, e, task, taskContext);
			}
		}

		// Token: 0x06001024 RID: 4132 RVA: 0x0005E898 File Offset: 0x0005CA98
		private List<MailboxData> GetMailboxesToProcess(Guid databaseGuid)
		{
			HashSet<DirectoryProcessorMailboxData> hashSet = new HashSet<DirectoryProcessorMailboxData>();
			lock (this.instanceLock)
			{
				this.grammarGeneratorMailboxes = UMGrammarTenantCache.Instance.GetMailboxesNeedingGrammars(databaseGuid);
				foreach (DirectoryProcessorMailboxData directoryProcessorMailboxData in this.grammarGeneratorMailboxes)
				{
					Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.GetMailboxesToProcess - Adding mailbox '{0}', orgId '{1}' for UM Grammar Generation", new object[]
					{
						directoryProcessorMailboxData.MailboxGuid,
						directoryProcessorMailboxData.OrgId
					});
					hashSet.Add(directoryProcessorMailboxData);
				}
				this.groupMetricsMailboxes = GroupMetricsTenantCache.Instance.GetMailboxesNeedingGroupMetrics(databaseGuid);
				foreach (DirectoryProcessorMailboxData directoryProcessorMailboxData2 in this.groupMetricsMailboxes)
				{
					Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "DirectoryProcessorAssistant.GetMailboxesToProcess - Adding mailbox '{0}', orgId '{1}' for Group Metrics", new object[]
					{
						directoryProcessorMailboxData2.MailboxGuid,
						directoryProcessorMailboxData2.OrgId
					});
					hashSet.Add(directoryProcessorMailboxData2);
				}
			}
			return hashSet.ToList<MailboxData>();
		}

		// Token: 0x06001025 RID: 4133 RVA: 0x0005EA10 File Offset: 0x0005CC10
		private void ThrowIfShuttingDown(RunData runData)
		{
			if (base.Shutdown)
			{
				Utilities.DebugTrace(DirectoryProcessorAssistant.Tracer, "Shutdown called during processing of mailbox guid='{0}'", new object[]
				{
					runData.MailboxGuid
				});
				throw new ShutdownException();
			}
		}

		// Token: 0x06001026 RID: 4134 RVA: 0x0005EA5C File Offset: 0x0005CC5C
		void IAssistantBase.OnShutdown()
		{
			base.OnShutdown();
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x0005EA64 File Offset: 0x0005CC64
		LocalizedString IAssistantBase.get_Name()
		{
			return base.Name;
		}

		// Token: 0x06001028 RID: 4136 RVA: 0x0005EA6C File Offset: 0x0005CC6C
		string IAssistantBase.get_NonLocalizedName()
		{
			return base.NonLocalizedName;
		}

		// Token: 0x04000A32 RID: 2610
		public const string FactoryDefaultSystemMailboxName = "SystemMailbox{bb558c35-97f1-4cb9-8ff7-d53741dc928c}";

		// Token: 0x04000A33 RID: 2611
		private object instanceLock = new object();

		// Token: 0x04000A34 RID: 2612
		private static readonly Trace Tracer = ExTraceGlobals.GeneralTracer;

		// Token: 0x04000A35 RID: 2613
		private ICollection<DirectoryProcessorMailboxData> groupMetricsMailboxes;

		// Token: 0x04000A36 RID: 2614
		private ICollection<DirectoryProcessorMailboxData> grammarGeneratorMailboxes;
	}
}
