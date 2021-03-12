using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.Sync;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02000371 RID: 881
	[Cmdlet("Remove", "FailedMSOSyncObject", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High, DefaultParameterSetName = "Identity")]
	public sealed class RemoveFailedMSOSyncObject : RemoveTaskBase<FailedMSOSyncObjectIdParameter, FailedMSOSyncObject>
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x06001EED RID: 7917 RVA: 0x00085C76 File Offset: 0x00083E76
		protected override ObjectId RootId
		{
			get
			{
				return ForwardSyncDataAccessHelper.GetRootId(this.Identity);
			}
		}

		// Token: 0x06001EEE RID: 7918 RVA: 0x00085C83 File Offset: 0x00083E83
		protected override IConfigDataProvider CreateSession()
		{
			return ForwardSyncDataAccessHelper.CreateSession(false);
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00085C8B File Offset: 0x00083E8B
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveFailedMSOSyncObject(this.Identity.ToString());
			}
		}

		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x06001EF0 RID: 7920 RVA: 0x00085C9D File Offset: 0x00083E9D
		// (set) Token: 0x06001EF1 RID: 7921 RVA: 0x00085CA5 File Offset: 0x00083EA5
		[Parameter(Mandatory = false)]
		public SwitchParameter Force { get; set; }

		// Token: 0x06001EF2 RID: 7922 RVA: 0x00085CB0 File Offset: 0x00083EB0
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (base.DataObject.IsTenantWideDivergence && !this.Force)
			{
				base.WriteError(new CannotRemoveTenantWideDivergenceException(base.DataObject.Identity.ToString()), ExchangeErrorCategory.Client, base.DataObject);
			}
		}

		// Token: 0x06001EF3 RID: 7923 RVA: 0x00085D04 File Offset: 0x00083F04
		protected override void InternalProcessRecord()
		{
			ForwardSyncDataAccessHelper.CleanUpDivergenceIds((IConfigurationSession)base.DataSession, base.DataObject);
			if (base.DataObject.IsTenantWideDivergence)
			{
				this.MarkDivergenceHaltingForContext(base.DataObject.ObjectId.ContextId);
			}
			base.InternalProcessRecord();
		}

		// Token: 0x06001EF4 RID: 7924 RVA: 0x00085D50 File Offset: 0x00083F50
		private void MarkDivergenceHaltingForContext(string contextId)
		{
			lock (this)
			{
				QueryFilter filter = new AndFilter(new QueryFilter[]
				{
					new ComparisonFilter(ComparisonOperator.Equal, FailedMSOSyncObjectSchema.ContextId, contextId),
					new ComparisonFilter(ComparisonOperator.Equal, FailedMSOSyncObjectSchema.IsTenantWideDivergence, false),
					new ComparisonFilter(ComparisonOperator.NotEqual, FailedMSOSyncObjectSchema.ExternalDirectoryObjectClass, DirectoryObjectClass.Company)
				});
				this.MarkDivergencesHalting(filter);
			}
		}

		// Token: 0x06001EF5 RID: 7925 RVA: 0x00085DD4 File Offset: 0x00083FD4
		private void MarkDivergencesHalting(QueryFilter filter)
		{
			ForwardSyncDataAccessHelper forwardSyncDataAccessHelper = new ForwardSyncDataAccessHelper(base.DataObject.ServiceInstanceId);
			IEnumerable<FailedMSOSyncObject> enumerable = forwardSyncDataAccessHelper.FindDivergence(filter);
			IConfigurationSession configurationSession = ForwardSyncDataAccessHelper.CreateSession(false);
			foreach (FailedMSOSyncObject failedMSOSyncObject in enumerable)
			{
				failedMSOSyncObject.IsIgnoredInHaltCondition = false;
				configurationSession.Save(failedMSOSyncObject);
			}
		}
	}
}
