using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x0200000B RID: 11
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class DeleteSiteCollectionTask : UnifiedGroupsTask
	{
		// Token: 0x06000074 RID: 116 RVA: 0x00003734 File Offset: 0x00001934
		public DeleteSiteCollectionTask(ADUser accessingUser, IRecipientSession adSession, Guid activityId) : base(accessingUser, adSession, activityId)
		{
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000373F File Offset: 0x0000193F
		// (set) Token: 0x06000076 RID: 118 RVA: 0x00003747 File Offset: 0x00001947
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00003750 File Offset: 0x00001950
		// (set) Token: 0x06000078 RID: 120 RVA: 0x00003758 File Offset: 0x00001958
		public string SmtpAddress { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00003761 File Offset: 0x00001961
		protected override string TaskName
		{
			get
			{
				return "DeleteSiteCollectionTask";
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003768 File Offset: 0x00001968
		protected override void RunInternal()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.SharePointDelete;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. DeleteSiteCollectionTask.RunInternal: Notifying SharePoint about group deletion: {1}", base.ActivityId, this.ExternalDirectoryObjectId ?? this.SmtpAddress);
			this.DeleteSiteCollection();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. DeleteSiteCollectionTask.RunInternal: Finished notifying SharePoint about group deletion", base.ActivityId);
			string value = string.Format("Notified SharePoint about group deletion;Group={0};ElapsedTime={1}", this.ExternalDirectoryObjectId ?? this.SmtpAddress, stopwatch.ElapsedMilliseconds);
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
					value
				}
			});
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.Completed;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003844 File Offset: 0x00001A44
		private void DeleteSiteCollection()
		{
			if (string.IsNullOrEmpty(this.ExternalDirectoryObjectId))
			{
				ObjectIdMapping objectIdMapping = new ObjectIdMapping(base.ADSession);
				objectIdMapping.Prefetch(new string[]
				{
					this.SmtpAddress
				});
				this.ExternalDirectoryObjectId = objectIdMapping.GetIdentityFromSmtpAddress(this.SmtpAddress);
			}
			using (SharePointNotification sharePointNotification = new SharePointNotification(SharePointNotification.NotificationType.Delete, this.ExternalDirectoryObjectId, base.AccessingUser.OrganizationId, base.ActAsUserCredentials, base.ActivityId))
			{
				sharePointNotification.Execute();
			}
		}
	}
}
