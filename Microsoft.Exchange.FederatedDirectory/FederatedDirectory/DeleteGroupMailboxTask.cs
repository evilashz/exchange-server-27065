using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.PSDirectInvoke;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200000A RID: 10
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeleteGroupMailboxTask : UnifiedGroupsTask
	{
		// Token: 0x0600006C RID: 108 RVA: 0x000034E1 File Offset: 0x000016E1
		public DeleteGroupMailboxTask(ADUser accessingUser, ExchangePrincipal accessingPrincipal, IRecipientSession adSession) : base(accessingUser, adSession)
		{
			this.accessingPrincipal = accessingPrincipal;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000034F2 File Offset: 0x000016F2
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000034FA File Offset: 0x000016FA
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003503 File Offset: 0x00001703
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000350B File Offset: 0x0000170B
		public string SmtpAddress { get; set; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003514 File Offset: 0x00001714
		protected override string TaskName
		{
			get
			{
				return "DeleteGroupMailboxTask";
			}
		}

		// Token: 0x06000072 RID: 114 RVA: 0x0000351C File Offset: 0x0000171C
		protected override void RunInternal()
		{
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.ExchangeDelete;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. DeleteUnifiedGroupTask.RunInternal: Deleting group in Exchange: {1}", base.ActivityId, this.ExternalDirectoryObjectId ?? this.SmtpAddress);
			this.DeleteGroupMailbox();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. DeleteUnifiedGroupTask.RunInternal: Finished deleting group in Exchange", base.ActivityId);
			FederatedDirectoryLogger.AppendToLog(new SchemaBasedLogEvent<FederatedDirectoryLogSchema.TraceTag>
			{
				{
					FederatedDirectoryLogSchema.TraceTag.TaskName,
					this.TaskName
				},
				{
					FederatedDirectoryLogSchema.TraceTag.ActivityId,
					base.ActivityId
				},
				{
					FederatedDirectoryLogSchema.TraceTag.CurrentAction,
					base.CurrentAction
				},
				{
					FederatedDirectoryLogSchema.TraceTag.Message,
					string.Format("Deleted group mailbox in Exchange. {0}", this.ExternalDirectoryObjectId ?? this.SmtpAddress)
				}
			});
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.Completed;
		}

		// Token: 0x06000073 RID: 115 RVA: 0x000035E8 File Offset: 0x000017E8
		private void DeleteGroupMailbox()
		{
			using (PSLocalTask<RemoveGroupMailbox, object> pslocalTask = CmdletTaskFactory.Instance.CreateRemoveGroupMailboxTask(this.accessingPrincipal))
			{
				pslocalTask.CaptureAdditionalIO = true;
				pslocalTask.Task.ExecutingUser = new RecipientIdParameter(this.accessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString());
				pslocalTask.Task.Identity = new MailboxIdParameter(this.ExternalDirectoryObjectId ?? this.SmtpAddress);
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.RemoveGroupMailboxToString(pslocalTask.Task).ToString());
				pslocalTask.Task.Execute();
				UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. {1}", base.ActivityId, new PSLocalTaskLogging.TaskOutputToString(pslocalTask.AdditionalIO).ToString());
				if (pslocalTask.Error != null)
				{
					UnifiedGroupsTask.Tracer.TraceError<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. DeleteUnifiedGroupTask.DeleteGroupMailbox() failed: {1}", base.ActivityId, pslocalTask.ErrorMessage);
					throw new ExchangeAdaptorException(Strings.GroupMailboxFailedDelete(this.ExternalDirectoryObjectId ?? this.SmtpAddress, pslocalTask.ErrorMessage));
				}
			}
		}

		// Token: 0x04000045 RID: 69
		private readonly ExchangePrincipal accessingPrincipal;
	}
}
