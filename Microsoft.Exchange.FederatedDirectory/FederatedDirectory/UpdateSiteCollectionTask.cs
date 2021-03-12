using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000014 RID: 20
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class UpdateSiteCollectionTask : UnifiedGroupsTask
	{
		// Token: 0x0600009F RID: 159 RVA: 0x00004684 File Offset: 0x00002884
		public UpdateSiteCollectionTask(ADUser accessingUser, IRecipientSession adSession, Guid activityId) : base(accessingUser, adSession, activityId)
		{
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000A0 RID: 160 RVA: 0x0000468F File Offset: 0x0000288F
		// (set) Token: 0x060000A1 RID: 161 RVA: 0x00004697 File Offset: 0x00002897
		public string Description { get; set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000A2 RID: 162 RVA: 0x000046A0 File Offset: 0x000028A0
		// (set) Token: 0x060000A3 RID: 163 RVA: 0x000046A8 File Offset: 0x000028A8
		public string DisplayName { get; set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000A4 RID: 164 RVA: 0x000046B1 File Offset: 0x000028B1
		// (set) Token: 0x060000A5 RID: 165 RVA: 0x000046B9 File Offset: 0x000028B9
		public string[] AddedOwners { get; set; }

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A6 RID: 166 RVA: 0x000046C2 File Offset: 0x000028C2
		// (set) Token: 0x060000A7 RID: 167 RVA: 0x000046CA File Offset: 0x000028CA
		public string[] RemovedOwners { get; set; }

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000A8 RID: 168 RVA: 0x000046D3 File Offset: 0x000028D3
		// (set) Token: 0x060000A9 RID: 169 RVA: 0x000046DB File Offset: 0x000028DB
		public string[] AddedMembers { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000AA RID: 170 RVA: 0x000046E4 File Offset: 0x000028E4
		// (set) Token: 0x060000AB RID: 171 RVA: 0x000046EC File Offset: 0x000028EC
		public string[] RemovedMembers { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000AC RID: 172 RVA: 0x000046F5 File Offset: 0x000028F5
		// (set) Token: 0x060000AD RID: 173 RVA: 0x000046FD File Offset: 0x000028FD
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00004706 File Offset: 0x00002906
		protected override string TaskName
		{
			get
			{
				return "UpdateSiteCollectionTask";
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004710 File Offset: 0x00002910
		protected override void RunInternal()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.SharePointUpdate;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. UpdateSiteCollectionTask.RunInternal: Notifying SharePoint about group update: {1}", base.ActivityId, this.ExternalDirectoryObjectId);
			this.UpdateSiteCollection();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. UpdateSiteCollectionTask.RunInternal: Finished notifying SharePoint about group update", base.ActivityId);
			string value = string.Format("Notified SharePoint about group update;Group={0};ElapsedTime={1}", this.ExternalDirectoryObjectId, stopwatch.ElapsedMilliseconds);
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

		// Token: 0x060000B0 RID: 176 RVA: 0x000047D8 File Offset: 0x000029D8
		private void UpdateSiteCollection()
		{
			using (SharePointNotification sharePointNotification = new SharePointNotification(SharePointNotification.NotificationType.Update, this.ExternalDirectoryObjectId, base.AccessingUser.OrganizationId, base.ActAsUserCredentials, base.ActivityId))
			{
				if (this.Description != null)
				{
					sharePointNotification.SetPropertyValue("Description", this.Description, false);
				}
				if (!string.IsNullOrEmpty(this.DisplayName))
				{
					sharePointNotification.SetPropertyValue("DisplayName", this.DisplayName, false);
				}
				if (this.AddedOwners != null && this.AddedOwners.Length != 0)
				{
					sharePointNotification.AddOwners(this.AddedOwners);
				}
				if (this.RemovedOwners != null && this.RemovedOwners.Length != 0)
				{
					sharePointNotification.RemoveOwners(this.RemovedOwners);
				}
				if (this.AddedMembers != null && this.AddedMembers.Length != 0)
				{
					sharePointNotification.AddMembers(this.AddedMembers);
				}
				if (this.RemovedMembers != null && this.RemovedMembers.Length != 0)
				{
					sharePointNotification.RemoveMembers(this.RemovedMembers);
				}
				sharePointNotification.Execute();
			}
		}
	}
}
