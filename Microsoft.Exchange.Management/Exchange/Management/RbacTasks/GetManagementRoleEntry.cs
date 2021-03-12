using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.RbacTasks
{
	// Token: 0x02000682 RID: 1666
	[Cmdlet("Get", "ManagementRoleEntry", DefaultParameterSetName = "Identity")]
	public sealed class GetManagementRoleEntry : GetMultitenancySystemConfigurationObjectTask<RoleEntryIdParameter, ExchangeRoleEntryPresentation>
	{
		// Token: 0x17001188 RID: 4488
		// (get) Token: 0x06003AEE RID: 15086 RVA: 0x000FAC96 File Offset: 0x000F8E96
		// (set) Token: 0x06003AEF RID: 15087 RVA: 0x000FAC9E File Offset: 0x000F8E9E
		private new OrganizationIdParameter Organization { get; set; }

		// Token: 0x17001189 RID: 4489
		// (get) Token: 0x06003AF0 RID: 15088 RVA: 0x000FACA7 File Offset: 0x000F8EA7
		protected override SharedTenantConfigurationMode SharedTenantConfigurationMode
		{
			get
			{
				return SharedTenantConfigurationMode.Static;
			}
		}

		// Token: 0x1700118A RID: 4490
		// (get) Token: 0x06003AF1 RID: 15089 RVA: 0x000FACAA File Offset: 0x000F8EAA
		// (set) Token: 0x06003AF2 RID: 15090 RVA: 0x000FACB2 File Offset: 0x000F8EB2
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override RoleEntryIdParameter Identity
		{
			get
			{
				return base.Identity;
			}
			set
			{
				base.Identity = value;
			}
		}

		// Token: 0x1700118B RID: 4491
		// (get) Token: 0x06003AF3 RID: 15091 RVA: 0x000FACBB File Offset: 0x000F8EBB
		// (set) Token: 0x06003AF4 RID: 15092 RVA: 0x000FACD2 File Offset: 0x000F8ED2
		[Parameter]
		public string[] Parameters
		{
			get
			{
				return (string[])base.Fields[RbacCommonParameters.ParameterParameters];
			}
			set
			{
				RoleEntry.FormatParameters(value);
				base.Fields[RbacCommonParameters.ParameterParameters] = value;
			}
		}

		// Token: 0x1700118C RID: 4492
		// (get) Token: 0x06003AF5 RID: 15093 RVA: 0x000FACEB File Offset: 0x000F8EEB
		// (set) Token: 0x06003AF6 RID: 15094 RVA: 0x000FAD02 File Offset: 0x000F8F02
		[Parameter]
		public string PSSnapinName
		{
			get
			{
				return (string)base.Fields[RbacCommonParameters.ParameterPSSnapinName];
			}
			set
			{
				base.Fields[RbacCommonParameters.ParameterPSSnapinName] = value;
			}
		}

		// Token: 0x1700118D RID: 4493
		// (get) Token: 0x06003AF7 RID: 15095 RVA: 0x000FAD15 File Offset: 0x000F8F15
		// (set) Token: 0x06003AF8 RID: 15096 RVA: 0x000FAD2C File Offset: 0x000F8F2C
		[Parameter]
		public ManagementRoleEntryType[] Type
		{
			get
			{
				return (ManagementRoleEntryType[])base.Fields[RbacCommonParameters.ParameterType];
			}
			set
			{
				base.VerifyValues<ManagementRoleEntryType>(GetManagementRoleEntry.AllowedRoleEntryTypes, value);
				base.Fields[RbacCommonParameters.ParameterType] = value;
			}
		}

		// Token: 0x1700118E RID: 4494
		// (get) Token: 0x06003AF9 RID: 15097 RVA: 0x000FAD4B File Offset: 0x000F8F4B
		protected override bool DeepSearch
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06003AFA RID: 15098 RVA: 0x000FAD50 File Offset: 0x000F8F50
		protected override void InternalValidate()
		{
			TaskLogger.LogEnter(new object[]
			{
				this.Identity
			});
			((IConfigurationSession)base.DataSession).SessionSettings.IsSharedConfigChecked = true;
			this.ConfigurationSession.SessionSettings.IsSharedConfigChecked = true;
			this.Identity.Parameters = this.Parameters;
			this.Identity.PSSnapinName = this.PSSnapinName;
			if (this.Type != null && this.Type.Length > 0)
			{
				ManagementRoleEntryType managementRoleEntryType = (ManagementRoleEntryType)0;
				foreach (ManagementRoleEntryType managementRoleEntryType2 in this.Type)
				{
					managementRoleEntryType |= managementRoleEntryType2;
				}
				this.Identity.Type = managementRoleEntryType;
			}
			base.InternalValidate();
			TaskLogger.LogExit();
		}

		// Token: 0x040026B2 RID: 9906
		private static readonly ManagementRoleEntryType[] AllowedRoleEntryTypes = new ManagementRoleEntryType[]
		{
			ManagementRoleEntryType.Cmdlet,
			ManagementRoleEntryType.Script,
			ManagementRoleEntryType.ApplicationPermission,
			ManagementRoleEntryType.WebService
		};
	}
}
