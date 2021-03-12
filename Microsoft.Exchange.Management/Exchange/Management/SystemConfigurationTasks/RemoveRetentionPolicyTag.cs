using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.InfoWorker.Common.Search;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x0200031D RID: 797
	[Cmdlet("Remove", "RetentionPolicyTag", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveRetentionPolicyTag : RemoveSystemConfigurationObjectTask<RetentionPolicyTagIdParameter, RetentionPolicyTag>
	{
		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06001AF9 RID: 6905 RVA: 0x00077A04 File Offset: 0x00075C04
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveRetentionPolicyTag(this.Identity.ToString());
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06001AFA RID: 6906 RVA: 0x00077A16 File Offset: 0x00075C16
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Dehydrateable;
			}
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x00077A19 File Offset: 0x00075C19
		protected override void InternalValidate()
		{
			base.InternalValidate();
			if (SharedConfiguration.IsDehydratedConfiguration(base.CurrentOrganizationId))
			{
				base.WriteError(new ArgumentException(Strings.ErrorWriteOpOnDehydratedTenant), ErrorCategory.InvalidArgument, base.DataObject.Identity);
			}
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x00077A60 File Offset: 0x00075C60
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (base.DataObject != null && SharedConfiguration.IsSharedConfiguration(base.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(base.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			base.DataObject.GetELCContentSettings().ForEach(delegate(ElcContentSettings x)
			{
				base.DataSession.Delete(x);
			});
			base.InternalProcessRecord();
			TaskLogger.LogExit();
		}
	}
}
