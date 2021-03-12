using System;
using System.Diagnostics;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Management.RecipientTasks;
using Microsoft.Exchange.UnifiedGroups;

namespace Microsoft.Exchange.FederatedDirectory
{
	// Token: 0x02000007 RID: 7
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class CreateSiteCollectionTask : UnifiedGroupsTask
	{
		// Token: 0x0600002C RID: 44 RVA: 0x000026F8 File Offset: 0x000008F8
		public CreateSiteCollectionTask(ADUser accessingUser, IRecipientSession adSession, Guid activityId) : base(accessingUser, adSession, activityId)
		{
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600002D RID: 45 RVA: 0x00002703 File Offset: 0x00000903
		// (set) Token: 0x0600002E RID: 46 RVA: 0x0000270B File Offset: 0x0000090B
		public string Name { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002714 File Offset: 0x00000914
		// (set) Token: 0x06000030 RID: 48 RVA: 0x0000271C File Offset: 0x0000091C
		public string Alias { get; set; }

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002725 File Offset: 0x00000925
		// (set) Token: 0x06000032 RID: 50 RVA: 0x0000272D File Offset: 0x0000092D
		public string Description { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00002736 File Offset: 0x00000936
		// (set) Token: 0x06000034 RID: 52 RVA: 0x0000273E File Offset: 0x0000093E
		public ModernGroupTypeInfo Type { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000035 RID: 53 RVA: 0x00002747 File Offset: 0x00000947
		// (set) Token: 0x06000036 RID: 54 RVA: 0x0000274F File Offset: 0x0000094F
		public string ExternalDirectoryObjectId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002758 File Offset: 0x00000958
		protected override string TaskName
		{
			get
			{
				return "CreateSiteCollectionTask";
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002760 File Offset: 0x00000960
		protected override void RunInternal()
		{
			Stopwatch stopwatch = Stopwatch.StartNew();
			base.CurrentAction = UnifiedGroupsTask.UnifiedGroupsAction.SharePointCreate;
			UnifiedGroupsTask.Tracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "ActivityId={0}. CreateSiteCollectionTask.RunInternal: Notifying SharePoint about group creation: {1}", base.ActivityId, this.ExternalDirectoryObjectId);
			this.CreateSiteCollection();
			UnifiedGroupsTask.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ActivityId={0}. CreateSiteCollectionTask.RunInternal: Finished notifying SharePoint about group creation", base.ActivityId);
			string value = string.Format("Notified SharePoint about group creation;Group={0};ElapsedTime={1}", this.ExternalDirectoryObjectId, stopwatch.ElapsedMilliseconds);
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

		// Token: 0x06000039 RID: 57 RVA: 0x00002828 File Offset: 0x00000A28
		private void CreateSiteCollection()
		{
			using (SharePointNotification sharePointNotification = new SharePointNotification(SharePointNotification.NotificationType.Create, this.ExternalDirectoryObjectId, base.AccessingUser.OrganizationId, base.ActAsUserCredentials, base.ActivityId))
			{
				sharePointNotification.SetPropertyValue("Alias", this.Alias, false);
				sharePointNotification.SetPropertyValue("DisplayName", this.Name, false);
				sharePointNotification.SetPropertyValue("IsPublic", this.Type == ModernGroupTypeInfo.Public, false);
				if (!string.IsNullOrEmpty(this.Description))
				{
					sharePointNotification.SetPropertyValue("Description", this.Description, false);
				}
				sharePointNotification.SetAllowAccessTo(this.Type == ModernGroupTypeInfo.Public);
				sharePointNotification.Execute();
			}
		}
	}
}
