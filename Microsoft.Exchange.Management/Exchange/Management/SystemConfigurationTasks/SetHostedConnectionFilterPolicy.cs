using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000A3F RID: 2623
	[Cmdlet("Set", "HostedConnectionFilterPolicy", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetHostedConnectionFilterPolicy : SetSystemConfigurationObjectTask<HostedConnectionFilterPolicyIdParameter, HostedConnectionFilterPolicy>
	{
		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x06005DA6 RID: 23974 RVA: 0x00189F8B File Offset: 0x0018818B
		// (set) Token: 0x06005DA7 RID: 23975 RVA: 0x00189F93 File Offset: 0x00188193
		[Parameter]
		public SwitchParameter MakeDefault { get; set; }

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x06005DA8 RID: 23976 RVA: 0x00189F9C File Offset: 0x0018819C
		// (set) Token: 0x06005DA9 RID: 23977 RVA: 0x00189FA4 File Offset: 0x001881A4
		[Parameter]
		public SwitchParameter IgnoreDehydratedFlag { get; set; }

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x06005DAA RID: 23978 RVA: 0x00189FAD File Offset: 0x001881AD
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveHostedConnectionFilterPolicy(this.Identity.ToString());
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x06005DAB RID: 23979 RVA: 0x00189FBF File Offset: 0x001881BF
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				if (!this.IgnoreDehydratedFlag)
				{
					return SharedTenantConfigurationMode.Dehydrateable;
				}
				return SharedTenantConfigurationMode.NotShared;
			}
		}

		// Token: 0x06005DAC RID: 23980 RVA: 0x00189FD4 File Offset: 0x001881D4
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			if (!this.IgnoreDehydratedFlag)
			{
				SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			}
			TaskLogger.LogEnter();
		}

		// Token: 0x06005DAD RID: 23981 RVA: 0x0018A02C File Offset: 0x0018822C
		protected override void StampChangesOn(IConfigurable dataObject)
		{
			ADObject adobject = dataObject as ADObject;
			if (adobject != null)
			{
				this.dualWriter = new FfoDualWriter(adobject.Name);
			}
			base.StampChangesOn(dataObject);
		}

		// Token: 0x06005DAE RID: 23982 RVA: 0x0018A05C File Offset: 0x0018825C
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (SharedConfiguration.IsSharedConfiguration(this.DataObject.OrganizationId) && !base.ShouldContinue(Strings.ConfirmSharedConfiguration(this.DataObject.OrganizationId.OrganizationalUnit.Name)))
			{
				TaskLogger.LogExit();
				return;
			}
			HostedConnectionFilterPolicy hostedConnectionFilterPolicy = null;
			if (this.MakeDefault && !this.DataObject.IsDefault)
			{
				this.DataObject.IsDefault = true;
				hostedConnectionFilterPolicy = ((ITenantConfigurationSession)base.DataSession).GetDefaultFilteringConfiguration<HostedConnectionFilterPolicy>();
				if (hostedConnectionFilterPolicy != null && hostedConnectionFilterPolicy.IsDefault)
				{
					hostedConnectionFilterPolicy.IsDefault = false;
					base.DataSession.Save(hostedConnectionFilterPolicy);
				}
			}
			else if (base.Fields.Contains("MakeDefault") && !this.MakeDefault && this.DataObject.IsDefault)
			{
				base.WriteError(new OperationNotAllowedException(Strings.OperationNotAllowed), ErrorCategory.InvalidOperation, this.MakeDefault);
			}
			try
			{
				base.InternalProcessRecord();
				hostedConnectionFilterPolicy = null;
				this.dualWriter.Save<HostedConnectionFilterPolicy>(this, this.DataObject);
			}
			finally
			{
				if (hostedConnectionFilterPolicy != null)
				{
					hostedConnectionFilterPolicy.IsDefault = true;
					base.DataSession.Save(hostedConnectionFilterPolicy);
				}
			}
			TaskLogger.LogExit();
		}

		// Token: 0x040034C0 RID: 13504
		private FfoDualWriter dualWriter;
	}
}
