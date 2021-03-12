using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B0D RID: 2829
	[Cmdlet("Set", "ResourceConfig", SupportsShouldProcess = true, DefaultParameterSetName = "Identity")]
	public sealed class SetResourceConfig : SetMultitenancySingletonSystemConfigurationObjectTask<ResourceBookingConfig>
	{
		// Token: 0x17001E9A RID: 7834
		// (get) Token: 0x060064AA RID: 25770 RVA: 0x001A4486 File Offset: 0x001A2686
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageSetResourceConfig;
			}
		}

		// Token: 0x17001E9B RID: 7835
		// (get) Token: 0x060064AB RID: 25771 RVA: 0x001A448D File Offset: 0x001A268D
		protected override ObjectId RootId
		{
			get
			{
				return ResourceBookingConfig.GetWellKnownParentLocation(base.CurrentOrgContainerId);
			}
		}

		// Token: 0x17001E9C RID: 7836
		// (get) Token: 0x060064AC RID: 25772 RVA: 0x001A449A File Offset: 0x001A269A
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x060064AD RID: 25773 RVA: 0x001A44A0 File Offset: 0x001A26A0
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (this.DataObject.ResourcePropertySchema != null)
			{
				foreach (string text in this.DataObject.ResourcePropertySchema)
				{
					if (!text.StartsWith("Room/", StringComparison.OrdinalIgnoreCase) && !text.StartsWith("Equipment/", StringComparison.OrdinalIgnoreCase))
					{
						string[] array = text.Split(new char[]
						{
							'/'
						});
						base.WriteError(new ErrorResourceRoomOrEquipmentOnlyException("Room", "Equipment", text, (array.Length > 0) ? array[0] : text), ErrorCategory.InvalidData, this.DataObject);
					}
				}
			}
			TaskLogger.LogExit();
		}
	}
}
