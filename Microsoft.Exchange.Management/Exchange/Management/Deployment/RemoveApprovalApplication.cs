using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Deployment
{
	// Token: 0x0200021C RID: 540
	[Cmdlet("Remove", "ApprovalApplication")]
	public sealed class RemoveApprovalApplication : RemoveSystemConfigurationObjectTask<ApprovalApplicationIdParameter, ApprovalApplication>
	{
		// Token: 0x170005B0 RID: 1456
		// (get) Token: 0x06001274 RID: 4724 RVA: 0x00051422 File Offset: 0x0004F622
		// (set) Token: 0x06001275 RID: 4725 RVA: 0x00051448 File Offset: 0x0004F648
		[Parameter(Mandatory = true, ParameterSetName = "AutoGroup")]
		public SwitchParameter AutoGroup
		{
			get
			{
				return (SwitchParameter)(base.Fields["AutoGroup"] ?? false);
			}
			set
			{
				base.Fields["AutoGroup"] = value;
			}
		}

		// Token: 0x170005B1 RID: 1457
		// (get) Token: 0x06001276 RID: 4726 RVA: 0x00051460 File Offset: 0x0004F660
		// (set) Token: 0x06001277 RID: 4727 RVA: 0x00051486 File Offset: 0x0004F686
		[Parameter(Mandatory = true, ParameterSetName = "ModeratedRecipients")]
		public SwitchParameter ModeratedRecipients
		{
			get
			{
				return (SwitchParameter)(base.Fields["ModeratedRecipients"] ?? false);
			}
			set
			{
				base.Fields["ModeratedRecipients"] = value;
			}
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000514A0 File Offset: 0x0004F6A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			try
			{
				if (this.Identity == null)
				{
					ADObjectId orgContainerId = this.ConfigurationSession.GetOrgContainerId();
					this.Identity = new ApprovalApplicationIdParameter(orgContainerId.GetDescendantId(new ADObjectId(string.Concat(new object[]
					{
						"CN=",
						base.ParameterSetName,
						",",
						ApprovalApplication.ParentPathInternal
					}), Guid.Empty)));
				}
				else
				{
					base.WriteError(new ArgumentException(Strings.ErrorApprovalApplicationIdentityUnsupported), ErrorCategory.InvalidArgument, this.Identity);
				}
				base.InternalValidate();
				ExistsFilter filter = new ExistsFilter(ApprovalApplicationSchema.ArbitrationMailboxesBacklink);
				ApprovalApplication[] array = this.ConfigurationSession.Find<ApprovalApplication>((ADObjectId)base.DataObject.Identity, QueryScope.Base, filter, null, 0);
				if (array.Length > 0)
				{
					base.WriteError(new CannotRemoveApprovalApplicationWithMailboxes(), ErrorCategory.PermissionDenied, this.Identity);
				}
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}
	}
}
