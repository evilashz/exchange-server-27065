using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal static class WarmupGroupManagementDependency
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x0000582B File Offset: 0x00003A2B
		internal static bool HasWarmUpAttempted
		{
			get
			{
				return WarmupGroupManagementDependency.hasWarmUpAttempted;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00005834 File Offset: 0x00003A34
		// (set) Token: 0x060000EB RID: 235 RVA: 0x0000583B File Offset: 0x00003A3B
		internal static Action<object, string> OnAttemptCompletionCallBack { get; set; }

		// Token: 0x060000EC RID: 236 RVA: 0x00005844 File Offset: 0x00003A44
		internal static void Reset()
		{
			lock (WarmupGroupManagementDependency.syncObject)
			{
				WarmupGroupManagementDependency.hasWarmUpAttempted = false;
			}
		}

		// Token: 0x060000ED RID: 237 RVA: 0x000059CC File Offset: 0x00003BCC
		internal static void WarmUpAsyncIfRequired(ExchangePrincipal currentUser)
		{
			if (WarmupGroupManagementDependency.HasWarmUpAttempted)
			{
				return;
			}
			lock (WarmupGroupManagementDependency.syncObject)
			{
				if (!WarmupGroupManagementDependency.HasWarmUpAttempted)
				{
					WarmupGroupManagementDependency.hasWarmUpAttempted = true;
					WarmupGroupManagementDependency.LogEntry("Scheduling Group management warmup call.");
					System.Threading.Tasks.Task.Factory.StartNew(delegate()
					{
						Stopwatch stopwatch = new Stopwatch();
						stopwatch.Start();
						using (PSLocalTask<NewGroupMailbox, GroupMailbox> pslocalTask = CmdletTaskFactory.Instance.CreateNewGroupMailboxTask(currentUser))
						{
							pslocalTask.Task.Name = "WarmUpRequest";
							pslocalTask.Task.ExecutingUser = new RecipientIdParameter(currentUser.MailboxInfo.PrimarySmtpAddress.ToString());
							pslocalTask.WhatIfMode = true;
							WarmupGroupManagementDependency.LogEntry("Execute warm up call");
							pslocalTask.Task.Execute();
							WarmupGroupManagementDependency.LogEntry(string.Format("Executed new group mailbox warm call. output = {0}, error = {1}, total seconds = {2}", (pslocalTask.Result == null) ? "null" : pslocalTask.Result.ToString(), pslocalTask.Error, stopwatch.Elapsed.TotalSeconds.ToString("n2")));
							if (WarmupGroupManagementDependency.OnAttemptCompletionCallBack != null)
							{
								WarmupGroupManagementDependency.OnAttemptCompletionCallBack(pslocalTask.Result, pslocalTask.ErrorMessage);
							}
						}
					}).ContinueWith(delegate(System.Threading.Tasks.Task t)
					{
						WarmupGroupManagementDependency.LogEntry("UnExpected exception. error =" + t.Exception);
						if (WarmupGroupManagementDependency.OnAttemptCompletionCallBack != null)
						{
							WarmupGroupManagementDependency.OnAttemptCompletionCallBack(null, t.Exception.ToString());
						}
					}, TaskContinuationOptions.OnlyOnFaulted);
				}
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00005A7C File Offset: 0x00003C7C
		private static void LogEntry(string message)
		{
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
			{
				{
					FederatedDirectoryLogSchema.TraceTag.TaskName,
					"WarmupGroupManagementDependency"
				},
				{
					FederatedDirectoryLogSchema.TraceTag.ActivityId,
					"3ca7a0ab-9404-497f-b691-000000000000"
				},
				{
					FederatedDirectoryLogSchema.TraceTag.CurrentAction,
					"WarmupGroupManagementDependency"
				},
				{
					FederatedDirectoryLogSchema.TraceTag.Message,
					message
				}
			});
		}

		// Token: 0x04000081 RID: 129
		private const string ActivityId = "3ca7a0ab-9404-497f-b691-000000000000";

		// Token: 0x04000082 RID: 130
		private static readonly object syncObject = new object();

		// Token: 0x04000083 RID: 131
		private static volatile bool hasWarmUpAttempted;
	}
}
