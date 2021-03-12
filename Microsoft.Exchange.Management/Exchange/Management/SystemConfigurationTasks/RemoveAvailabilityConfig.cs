using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020007F5 RID: 2037
	[Cmdlet("Remove", "AvailabilityConfig", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
	public sealed class RemoveAvailabilityConfig : RemoveSystemConfigurationObjectTask<AvailabilityConfigIdParameter, AvailabilityConfig>
	{
		// Token: 0x17001584 RID: 5508
		// (get) Token: 0x06004715 RID: 18197 RVA: 0x00123E40 File Offset: 0x00122040
		// (set) Token: 0x06004716 RID: 18198 RVA: 0x00123E57 File Offset: 0x00122057
		[Parameter(Mandatory = false, ParameterSetName = "Identity", ValueFromPipeline = true, ValueFromPipelineByPropertyName = true, Position = 0)]
		public override AvailabilityConfigIdParameter Identity
		{
			get
			{
				return (AvailabilityConfigIdParameter)base.Fields["Identity"];
			}
			set
			{
				base.Fields["Identity"] = value;
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x00123E6C File Offset: 0x0012206C
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			SharedConfigurationTaskHelper.VerifyIsNotTinyTenant(base.CurrentOrgState, new Task.ErrorLoggerDelegate(base.WriteError));
			try
			{
				if (this.Identity == null)
				{
					this.Identity = AvailabilityConfigIdParameter.Parse(AvailabilityConfig.ContainerName);
				}
				base.InternalValidate();
			}
			finally
			{
				TaskLogger.LogExit();
			}
		}

		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x06004718 RID: 18200 RVA: 0x00123ECC File Offset: 0x001220CC
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageRemoveAvailabilityConfig(base.DataObject.Name);
			}
		}
	}
}
