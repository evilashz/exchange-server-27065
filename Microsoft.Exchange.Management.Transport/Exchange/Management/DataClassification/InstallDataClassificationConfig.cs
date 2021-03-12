using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.DataClassification
{
	// Token: 0x02000022 RID: 34
	[Cmdlet("Install", "DataClassificationConfig")]
	public sealed class InstallDataClassificationConfig : NewMultitenancyFixedNameSystemConfigurationObjectTask<DataClassificationConfig>
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000C6 RID: 198 RVA: 0x00005242 File Offset: 0x00003442
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005248 File Offset: 0x00003448
		protected override IConfigurable PrepareDataObject()
		{
			TaskLogger.LogEnter();
			DataClassificationConfig dataClassificationConfig = (DataClassificationConfig)base.PrepareDataObject();
			dataClassificationConfig.Name = "Default Data Config";
			dataClassificationConfig.SetId(base.DataSession as IConfigurationSession, dataClassificationConfig.Name);
			TaskLogger.LogEnter();
			return dataClassificationConfig;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00005290 File Offset: 0x00003490
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter();
			base.InternalValidate();
			if (OrganizationId.ForestWideOrgId.Equals(base.ExecutingUserOrganizationId) && base.Organization == null)
			{
				base.WriteError(new InvalidOperationException(Strings.DataClassificationConfigFirstOrgNotSupported), ErrorCategory.InvalidOperation, null);
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x000052DE File Offset: 0x000034DE
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			if (!InstallDataClassificationConfig.DataClassificationConfigExists(base.DataSession as IConfigurationSession))
			{
				base.CreateParentContainerIfNeeded(this.DataObject);
				base.InternalProcessRecord();
			}
			TaskLogger.LogExit();
		}

		// Token: 0x060000CA RID: 202 RVA: 0x00005310 File Offset: 0x00003510
		private static bool DataClassificationConfigExists(IConfigurationSession session)
		{
			DataClassificationConfig[] array = session.Find<DataClassificationConfig>(null, QueryScope.SubTree, null, null, 1);
			return array.Length != 0;
		}
	}
}
