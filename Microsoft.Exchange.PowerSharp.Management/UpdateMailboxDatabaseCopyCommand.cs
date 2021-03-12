using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Management.SystemConfigurationTasks;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000586 RID: 1414
	public class UpdateMailboxDatabaseCopyCommand : SyntheticCommandWithPipelineInputNoOutput<DatabaseCopyIdParameter>
	{
		// Token: 0x06004A34 RID: 18996 RVA: 0x000779D5 File Offset: 0x00075BD5
		private UpdateMailboxDatabaseCopyCommand() : base("Update-MailboxDatabaseCopy")
		{
		}

		// Token: 0x06004A35 RID: 18997 RVA: 0x000779E2 File Offset: 0x00075BE2
		public UpdateMailboxDatabaseCopyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06004A36 RID: 18998 RVA: 0x000779F1 File Offset: 0x00075BF1
		public virtual UpdateMailboxDatabaseCopyCommand SetParameters(UpdateMailboxDatabaseCopyCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004A37 RID: 18999 RVA: 0x000779FB File Offset: 0x00075BFB
		public virtual UpdateMailboxDatabaseCopyCommand SetParameters(UpdateMailboxDatabaseCopyCommand.CancelSeedParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004A38 RID: 19000 RVA: 0x00077A05 File Offset: 0x00075C05
		public virtual UpdateMailboxDatabaseCopyCommand SetParameters(UpdateMailboxDatabaseCopyCommand.ExplicitServerParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06004A39 RID: 19001 RVA: 0x00077A0F File Offset: 0x00075C0F
		public virtual UpdateMailboxDatabaseCopyCommand SetParameters(UpdateMailboxDatabaseCopyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000587 RID: 1415
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17002AE1 RID: 10977
			// (set) Token: 0x06004A3A RID: 19002 RVA: 0x00077A19 File Offset: 0x00075C19
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002AE2 RID: 10978
			// (set) Token: 0x06004A3B RID: 19003 RVA: 0x00077A2C File Offset: 0x00075C2C
			public virtual SwitchParameter BeginSeed
			{
				set
				{
					base.PowerSharpParameters["BeginSeed"] = value;
				}
			}

			// Token: 0x17002AE3 RID: 10979
			// (set) Token: 0x06004A3C RID: 19004 RVA: 0x00077A44 File Offset: 0x00075C44
			public virtual SwitchParameter DatabaseOnly
			{
				set
				{
					base.PowerSharpParameters["DatabaseOnly"] = value;
				}
			}

			// Token: 0x17002AE4 RID: 10980
			// (set) Token: 0x06004A3D RID: 19005 RVA: 0x00077A5C File Offset: 0x00075C5C
			public virtual SwitchParameter CatalogOnly
			{
				set
				{
					base.PowerSharpParameters["CatalogOnly"] = value;
				}
			}

			// Token: 0x17002AE5 RID: 10981
			// (set) Token: 0x06004A3E RID: 19006 RVA: 0x00077A74 File Offset: 0x00075C74
			public virtual SwitchParameter ManualResume
			{
				set
				{
					base.PowerSharpParameters["ManualResume"] = value;
				}
			}

			// Token: 0x17002AE6 RID: 10982
			// (set) Token: 0x06004A3F RID: 19007 RVA: 0x00077A8C File Offset: 0x00075C8C
			public virtual SwitchParameter DeleteExistingFiles
			{
				set
				{
					base.PowerSharpParameters["DeleteExistingFiles"] = value;
				}
			}

			// Token: 0x17002AE7 RID: 10983
			// (set) Token: 0x06004A40 RID: 19008 RVA: 0x00077AA4 File Offset: 0x00075CA4
			public virtual SwitchParameter SafeDeleteExistingFiles
			{
				set
				{
					base.PowerSharpParameters["SafeDeleteExistingFiles"] = value;
				}
			}

			// Token: 0x17002AE8 RID: 10984
			// (set) Token: 0x06004A41 RID: 19009 RVA: 0x00077ABC File Offset: 0x00075CBC
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x17002AE9 RID: 10985
			// (set) Token: 0x06004A42 RID: 19010 RVA: 0x00077AD4 File Offset: 0x00075CD4
			public virtual DatabaseAvailabilityGroupNetworkIdParameter Network
			{
				set
				{
					base.PowerSharpParameters["Network"] = value;
				}
			}

			// Token: 0x17002AEA RID: 10986
			// (set) Token: 0x06004A43 RID: 19011 RVA: 0x00077AE7 File Offset: 0x00075CE7
			public virtual UseDagDefaultOnOff NetworkCompressionOverride
			{
				set
				{
					base.PowerSharpParameters["NetworkCompressionOverride"] = value;
				}
			}

			// Token: 0x17002AEB RID: 10987
			// (set) Token: 0x06004A44 RID: 19012 RVA: 0x00077AFF File Offset: 0x00075CFF
			public virtual UseDagDefaultOnOff NetworkEncryptionOverride
			{
				set
				{
					base.PowerSharpParameters["NetworkEncryptionOverride"] = value;
				}
			}

			// Token: 0x17002AEC RID: 10988
			// (set) Token: 0x06004A45 RID: 19013 RVA: 0x00077B17 File Offset: 0x00075D17
			public virtual ServerIdParameter SourceServer
			{
				set
				{
					base.PowerSharpParameters["SourceServer"] = value;
				}
			}

			// Token: 0x17002AED RID: 10989
			// (set) Token: 0x06004A46 RID: 19014 RVA: 0x00077B2A File Offset: 0x00075D2A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AEE RID: 10990
			// (set) Token: 0x06004A47 RID: 19015 RVA: 0x00077B3D File Offset: 0x00075D3D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AEF RID: 10991
			// (set) Token: 0x06004A48 RID: 19016 RVA: 0x00077B55 File Offset: 0x00075D55
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AF0 RID: 10992
			// (set) Token: 0x06004A49 RID: 19017 RVA: 0x00077B6D File Offset: 0x00075D6D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AF1 RID: 10993
			// (set) Token: 0x06004A4A RID: 19018 RVA: 0x00077B85 File Offset: 0x00075D85
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AF2 RID: 10994
			// (set) Token: 0x06004A4B RID: 19019 RVA: 0x00077B9D File Offset: 0x00075D9D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002AF3 RID: 10995
			// (set) Token: 0x06004A4C RID: 19020 RVA: 0x00077BB5 File Offset: 0x00075DB5
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000588 RID: 1416
		public class CancelSeedParameters : ParametersBase
		{
			// Token: 0x17002AF4 RID: 10996
			// (set) Token: 0x06004A4E RID: 19022 RVA: 0x00077BD5 File Offset: 0x00075DD5
			public virtual DatabaseCopyIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17002AF5 RID: 10997
			// (set) Token: 0x06004A4F RID: 19023 RVA: 0x00077BE8 File Offset: 0x00075DE8
			public virtual SwitchParameter CancelSeed
			{
				set
				{
					base.PowerSharpParameters["CancelSeed"] = value;
				}
			}

			// Token: 0x17002AF6 RID: 10998
			// (set) Token: 0x06004A50 RID: 19024 RVA: 0x00077C00 File Offset: 0x00075E00
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002AF7 RID: 10999
			// (set) Token: 0x06004A51 RID: 19025 RVA: 0x00077C13 File Offset: 0x00075E13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002AF8 RID: 11000
			// (set) Token: 0x06004A52 RID: 19026 RVA: 0x00077C2B File Offset: 0x00075E2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002AF9 RID: 11001
			// (set) Token: 0x06004A53 RID: 19027 RVA: 0x00077C43 File Offset: 0x00075E43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002AFA RID: 11002
			// (set) Token: 0x06004A54 RID: 19028 RVA: 0x00077C5B File Offset: 0x00075E5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002AFB RID: 11003
			// (set) Token: 0x06004A55 RID: 19029 RVA: 0x00077C73 File Offset: 0x00075E73
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002AFC RID: 11004
			// (set) Token: 0x06004A56 RID: 19030 RVA: 0x00077C8B File Offset: 0x00075E8B
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000589 RID: 1417
		public class ExplicitServerParameters : ParametersBase
		{
			// Token: 0x17002AFD RID: 11005
			// (set) Token: 0x06004A58 RID: 19032 RVA: 0x00077CAB File Offset: 0x00075EAB
			public virtual MailboxServerIdParameter Server
			{
				set
				{
					base.PowerSharpParameters["Server"] = value;
				}
			}

			// Token: 0x17002AFE RID: 11006
			// (set) Token: 0x06004A59 RID: 19033 RVA: 0x00077CBE File Offset: 0x00075EBE
			public virtual int MaximumSeedsInParallel
			{
				set
				{
					base.PowerSharpParameters["MaximumSeedsInParallel"] = value;
				}
			}

			// Token: 0x17002AFF RID: 11007
			// (set) Token: 0x06004A5A RID: 19034 RVA: 0x00077CD6 File Offset: 0x00075ED6
			public virtual SwitchParameter DatabaseOnly
			{
				set
				{
					base.PowerSharpParameters["DatabaseOnly"] = value;
				}
			}

			// Token: 0x17002B00 RID: 11008
			// (set) Token: 0x06004A5B RID: 19035 RVA: 0x00077CEE File Offset: 0x00075EEE
			public virtual SwitchParameter CatalogOnly
			{
				set
				{
					base.PowerSharpParameters["CatalogOnly"] = value;
				}
			}

			// Token: 0x17002B01 RID: 11009
			// (set) Token: 0x06004A5C RID: 19036 RVA: 0x00077D06 File Offset: 0x00075F06
			public virtual SwitchParameter ManualResume
			{
				set
				{
					base.PowerSharpParameters["ManualResume"] = value;
				}
			}

			// Token: 0x17002B02 RID: 11010
			// (set) Token: 0x06004A5D RID: 19037 RVA: 0x00077D1E File Offset: 0x00075F1E
			public virtual SwitchParameter DeleteExistingFiles
			{
				set
				{
					base.PowerSharpParameters["DeleteExistingFiles"] = value;
				}
			}

			// Token: 0x17002B03 RID: 11011
			// (set) Token: 0x06004A5E RID: 19038 RVA: 0x00077D36 File Offset: 0x00075F36
			public virtual SwitchParameter SafeDeleteExistingFiles
			{
				set
				{
					base.PowerSharpParameters["SafeDeleteExistingFiles"] = value;
				}
			}

			// Token: 0x17002B04 RID: 11012
			// (set) Token: 0x06004A5F RID: 19039 RVA: 0x00077D4E File Offset: 0x00075F4E
			public virtual UseDagDefaultOnOff NetworkCompressionOverride
			{
				set
				{
					base.PowerSharpParameters["NetworkCompressionOverride"] = value;
				}
			}

			// Token: 0x17002B05 RID: 11013
			// (set) Token: 0x06004A60 RID: 19040 RVA: 0x00077D66 File Offset: 0x00075F66
			public virtual UseDagDefaultOnOff NetworkEncryptionOverride
			{
				set
				{
					base.PowerSharpParameters["NetworkEncryptionOverride"] = value;
				}
			}

			// Token: 0x17002B06 RID: 11014
			// (set) Token: 0x06004A61 RID: 19041 RVA: 0x00077D7E File Offset: 0x00075F7E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B07 RID: 11015
			// (set) Token: 0x06004A62 RID: 19042 RVA: 0x00077D91 File Offset: 0x00075F91
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B08 RID: 11016
			// (set) Token: 0x06004A63 RID: 19043 RVA: 0x00077DA9 File Offset: 0x00075FA9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B09 RID: 11017
			// (set) Token: 0x06004A64 RID: 19044 RVA: 0x00077DC1 File Offset: 0x00075FC1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B0A RID: 11018
			// (set) Token: 0x06004A65 RID: 19045 RVA: 0x00077DD9 File Offset: 0x00075FD9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B0B RID: 11019
			// (set) Token: 0x06004A66 RID: 19046 RVA: 0x00077DF1 File Offset: 0x00075FF1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002B0C RID: 11020
			// (set) Token: 0x06004A67 RID: 19047 RVA: 0x00077E09 File Offset: 0x00076009
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x0200058A RID: 1418
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17002B0D RID: 11021
			// (set) Token: 0x06004A69 RID: 19049 RVA: 0x00077E29 File Offset: 0x00076029
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17002B0E RID: 11022
			// (set) Token: 0x06004A6A RID: 19050 RVA: 0x00077E3C File Offset: 0x0007603C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17002B0F RID: 11023
			// (set) Token: 0x06004A6B RID: 19051 RVA: 0x00077E54 File Offset: 0x00076054
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17002B10 RID: 11024
			// (set) Token: 0x06004A6C RID: 19052 RVA: 0x00077E6C File Offset: 0x0007606C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17002B11 RID: 11025
			// (set) Token: 0x06004A6D RID: 19053 RVA: 0x00077E84 File Offset: 0x00076084
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17002B12 RID: 11026
			// (set) Token: 0x06004A6E RID: 19054 RVA: 0x00077E9C File Offset: 0x0007609C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17002B13 RID: 11027
			// (set) Token: 0x06004A6F RID: 19055 RVA: 0x00077EB4 File Offset: 0x000760B4
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}
	}
}
