using System;
using System.Configuration;
using Microsoft.Exchange.Data.ConfigurationSettings;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000171 RID: 369
	[Serializable]
	internal class MRSRecurrentOperationConfigSchema : ConfigSchemaBase
	{
		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000E34 RID: 3636 RVA: 0x00020687 File Offset: 0x0001E887
		public override string Name
		{
			get
			{
				return "MRSScripts";
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000E35 RID: 3637 RVA: 0x0002068E File Offset: 0x0001E88E
		// (set) Token: 0x06000E36 RID: 3638 RVA: 0x0002069B File Offset: 0x0001E89B
		[ConfigurationProperty("TargetAddressOnMailboxRecoveryWorkflowIsEnabled", DefaultValue = false)]
		public bool TargetAddressRecoveryEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("TargetAddressOnMailboxRecoveryWorkflowIsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "TargetAddressOnMailboxRecoveryWorkflowIsEnabled");
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06000E37 RID: 3639 RVA: 0x000206A9 File Offset: 0x0001E8A9
		// (set) Token: 0x06000E38 RID: 3640 RVA: 0x000206B6 File Offset: 0x0001E8B6
		[ConfigurationProperty("MidsetDeletedRecoveryE15WorkflowIsEnabled", DefaultValue = false)]
		public bool MidsetRecoveryEnabled
		{
			get
			{
				return this.InternalGetConfig<bool>("MidsetDeletedRecoveryE15WorkflowIsEnabled");
			}
			set
			{
				this.InternalSetConfig<bool>(value, "MidsetDeletedRecoveryE15WorkflowIsEnabled");
			}
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000206C4 File Offset: 0x0001E8C4
		public MRSRecurrentOperationConfigSchema()
		{
			if (CommonUtils.IsMultiTenantEnabled())
			{
				base.SetDefaultConfigValue<bool>("TargetAddressOnMailboxRecoveryWorkflowIsEnabled", true);
				base.SetDefaultConfigValue<bool>("MidsetDeletedRecoveryE15WorkflowIsEnabled", true);
			}
		}

		// Token: 0x02000172 RID: 370
		[Serializable]
		public static class Setting
		{
			// Token: 0x04000808 RID: 2056
			public const string TargetAddressRecoveryIsEnabled = "TargetAddressOnMailboxRecoveryWorkflowIsEnabled";

			// Token: 0x04000809 RID: 2057
			public const string MidsetRecoveryIsEnabled = "MidsetDeletedRecoveryE15WorkflowIsEnabled";
		}
	}
}
