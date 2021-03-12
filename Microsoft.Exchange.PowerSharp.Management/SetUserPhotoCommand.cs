using System;
using System.IO;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000BA9 RID: 2985
	public class SetUserPhotoCommand : SyntheticCommandWithPipelineInputNoOutput<ADUser>
	{
		// Token: 0x0600909B RID: 37019 RVA: 0x000D36E0 File Offset: 0x000D18E0
		private SetUserPhotoCommand() : base("Set-UserPhoto")
		{
		}

		// Token: 0x0600909C RID: 37020 RVA: 0x000D36ED File Offset: 0x000D18ED
		public SetUserPhotoCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600909D RID: 37021 RVA: 0x000D36FC File Offset: 0x000D18FC
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.SavePhotoParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600909E RID: 37022 RVA: 0x000D3706 File Offset: 0x000D1906
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.UploadPhotoStreamParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600909F RID: 37023 RVA: 0x000D3710 File Offset: 0x000D1910
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060090A0 RID: 37024 RVA: 0x000D371A File Offset: 0x000D191A
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.UploadPhotoDataParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060090A1 RID: 37025 RVA: 0x000D3724 File Offset: 0x000D1924
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.CancelPhotoParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060090A2 RID: 37026 RVA: 0x000D372E File Offset: 0x000D192E
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.UploadPreviewParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x060090A3 RID: 37027 RVA: 0x000D3738 File Offset: 0x000D1938
		public virtual SetUserPhotoCommand SetParameters(SetUserPhotoCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000BAA RID: 2986
		public class SavePhotoParameters : ParametersBase
		{
			// Token: 0x17006502 RID: 25858
			// (set) Token: 0x060090A4 RID: 37028 RVA: 0x000D3742 File Offset: 0x000D1942
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006503 RID: 25859
			// (set) Token: 0x060090A5 RID: 37029 RVA: 0x000D3760 File Offset: 0x000D1960
			public virtual SwitchParameter Save
			{
				set
				{
					base.PowerSharpParameters["Save"] = value;
				}
			}

			// Token: 0x17006504 RID: 25860
			// (set) Token: 0x060090A6 RID: 37030 RVA: 0x000D3778 File Offset: 0x000D1978
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006505 RID: 25861
			// (set) Token: 0x060090A7 RID: 37031 RVA: 0x000D3790 File Offset: 0x000D1990
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006506 RID: 25862
			// (set) Token: 0x060090A8 RID: 37032 RVA: 0x000D37A3 File Offset: 0x000D19A3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006507 RID: 25863
			// (set) Token: 0x060090A9 RID: 37033 RVA: 0x000D37B6 File Offset: 0x000D19B6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006508 RID: 25864
			// (set) Token: 0x060090AA RID: 37034 RVA: 0x000D37CE File Offset: 0x000D19CE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006509 RID: 25865
			// (set) Token: 0x060090AB RID: 37035 RVA: 0x000D37E6 File Offset: 0x000D19E6
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700650A RID: 25866
			// (set) Token: 0x060090AC RID: 37036 RVA: 0x000D37FE File Offset: 0x000D19FE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700650B RID: 25867
			// (set) Token: 0x060090AD RID: 37037 RVA: 0x000D3816 File Offset: 0x000D1A16
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700650C RID: 25868
			// (set) Token: 0x060090AE RID: 37038 RVA: 0x000D382E File Offset: 0x000D1A2E
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BAB RID: 2987
		public class UploadPhotoStreamParameters : ParametersBase
		{
			// Token: 0x1700650D RID: 25869
			// (set) Token: 0x060090B0 RID: 37040 RVA: 0x000D384E File Offset: 0x000D1A4E
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700650E RID: 25870
			// (set) Token: 0x060090B1 RID: 37041 RVA: 0x000D386C File Offset: 0x000D1A6C
			public virtual Stream PictureStream
			{
				set
				{
					base.PowerSharpParameters["PictureStream"] = value;
				}
			}

			// Token: 0x1700650F RID: 25871
			// (set) Token: 0x060090B2 RID: 37042 RVA: 0x000D387F File Offset: 0x000D1A7F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006510 RID: 25872
			// (set) Token: 0x060090B3 RID: 37043 RVA: 0x000D3897 File Offset: 0x000D1A97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006511 RID: 25873
			// (set) Token: 0x060090B4 RID: 37044 RVA: 0x000D38AA File Offset: 0x000D1AAA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006512 RID: 25874
			// (set) Token: 0x060090B5 RID: 37045 RVA: 0x000D38BD File Offset: 0x000D1ABD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006513 RID: 25875
			// (set) Token: 0x060090B6 RID: 37046 RVA: 0x000D38D5 File Offset: 0x000D1AD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006514 RID: 25876
			// (set) Token: 0x060090B7 RID: 37047 RVA: 0x000D38ED File Offset: 0x000D1AED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006515 RID: 25877
			// (set) Token: 0x060090B8 RID: 37048 RVA: 0x000D3905 File Offset: 0x000D1B05
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006516 RID: 25878
			// (set) Token: 0x060090B9 RID: 37049 RVA: 0x000D391D File Offset: 0x000D1B1D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006517 RID: 25879
			// (set) Token: 0x060090BA RID: 37050 RVA: 0x000D3935 File Offset: 0x000D1B35
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BAC RID: 2988
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17006518 RID: 25880
			// (set) Token: 0x060090BC RID: 37052 RVA: 0x000D3955 File Offset: 0x000D1B55
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006519 RID: 25881
			// (set) Token: 0x060090BD RID: 37053 RVA: 0x000D3973 File Offset: 0x000D1B73
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700651A RID: 25882
			// (set) Token: 0x060090BE RID: 37054 RVA: 0x000D398B File Offset: 0x000D1B8B
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700651B RID: 25883
			// (set) Token: 0x060090BF RID: 37055 RVA: 0x000D399E File Offset: 0x000D1B9E
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700651C RID: 25884
			// (set) Token: 0x060090C0 RID: 37056 RVA: 0x000D39B1 File Offset: 0x000D1BB1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700651D RID: 25885
			// (set) Token: 0x060090C1 RID: 37057 RVA: 0x000D39C9 File Offset: 0x000D1BC9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700651E RID: 25886
			// (set) Token: 0x060090C2 RID: 37058 RVA: 0x000D39E1 File Offset: 0x000D1BE1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700651F RID: 25887
			// (set) Token: 0x060090C3 RID: 37059 RVA: 0x000D39F9 File Offset: 0x000D1BF9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006520 RID: 25888
			// (set) Token: 0x060090C4 RID: 37060 RVA: 0x000D3A11 File Offset: 0x000D1C11
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006521 RID: 25889
			// (set) Token: 0x060090C5 RID: 37061 RVA: 0x000D3A29 File Offset: 0x000D1C29
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BAD RID: 2989
		public class UploadPhotoDataParameters : ParametersBase
		{
			// Token: 0x17006522 RID: 25890
			// (set) Token: 0x060090C7 RID: 37063 RVA: 0x000D3A49 File Offset: 0x000D1C49
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006523 RID: 25891
			// (set) Token: 0x060090C8 RID: 37064 RVA: 0x000D3A67 File Offset: 0x000D1C67
			public virtual byte PictureData
			{
				set
				{
					base.PowerSharpParameters["PictureData"] = value;
				}
			}

			// Token: 0x17006524 RID: 25892
			// (set) Token: 0x060090C9 RID: 37065 RVA: 0x000D3A7F File Offset: 0x000D1C7F
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006525 RID: 25893
			// (set) Token: 0x060090CA RID: 37066 RVA: 0x000D3A97 File Offset: 0x000D1C97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006526 RID: 25894
			// (set) Token: 0x060090CB RID: 37067 RVA: 0x000D3AAA File Offset: 0x000D1CAA
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006527 RID: 25895
			// (set) Token: 0x060090CC RID: 37068 RVA: 0x000D3ABD File Offset: 0x000D1CBD
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006528 RID: 25896
			// (set) Token: 0x060090CD RID: 37069 RVA: 0x000D3AD5 File Offset: 0x000D1CD5
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006529 RID: 25897
			// (set) Token: 0x060090CE RID: 37070 RVA: 0x000D3AED File Offset: 0x000D1CED
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700652A RID: 25898
			// (set) Token: 0x060090CF RID: 37071 RVA: 0x000D3B05 File Offset: 0x000D1D05
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700652B RID: 25899
			// (set) Token: 0x060090D0 RID: 37072 RVA: 0x000D3B1D File Offset: 0x000D1D1D
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700652C RID: 25900
			// (set) Token: 0x060090D1 RID: 37073 RVA: 0x000D3B35 File Offset: 0x000D1D35
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BAE RID: 2990
		public class CancelPhotoParameters : ParametersBase
		{
			// Token: 0x1700652D RID: 25901
			// (set) Token: 0x060090D3 RID: 37075 RVA: 0x000D3B55 File Offset: 0x000D1D55
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700652E RID: 25902
			// (set) Token: 0x060090D4 RID: 37076 RVA: 0x000D3B73 File Offset: 0x000D1D73
			public virtual SwitchParameter Cancel
			{
				set
				{
					base.PowerSharpParameters["Cancel"] = value;
				}
			}

			// Token: 0x1700652F RID: 25903
			// (set) Token: 0x060090D5 RID: 37077 RVA: 0x000D3B8B File Offset: 0x000D1D8B
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006530 RID: 25904
			// (set) Token: 0x060090D6 RID: 37078 RVA: 0x000D3BA3 File Offset: 0x000D1DA3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006531 RID: 25905
			// (set) Token: 0x060090D7 RID: 37079 RVA: 0x000D3BB6 File Offset: 0x000D1DB6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006532 RID: 25906
			// (set) Token: 0x060090D8 RID: 37080 RVA: 0x000D3BC9 File Offset: 0x000D1DC9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006533 RID: 25907
			// (set) Token: 0x060090D9 RID: 37081 RVA: 0x000D3BE1 File Offset: 0x000D1DE1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006534 RID: 25908
			// (set) Token: 0x060090DA RID: 37082 RVA: 0x000D3BF9 File Offset: 0x000D1DF9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006535 RID: 25909
			// (set) Token: 0x060090DB RID: 37083 RVA: 0x000D3C11 File Offset: 0x000D1E11
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006536 RID: 25910
			// (set) Token: 0x060090DC RID: 37084 RVA: 0x000D3C29 File Offset: 0x000D1E29
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006537 RID: 25911
			// (set) Token: 0x060090DD RID: 37085 RVA: 0x000D3C41 File Offset: 0x000D1E41
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BAF RID: 2991
		public class UploadPreviewParameters : ParametersBase
		{
			// Token: 0x17006538 RID: 25912
			// (set) Token: 0x060090DF RID: 37087 RVA: 0x000D3C61 File Offset: 0x000D1E61
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17006539 RID: 25913
			// (set) Token: 0x060090E0 RID: 37088 RVA: 0x000D3C7F File Offset: 0x000D1E7F
			public virtual Stream PictureStream
			{
				set
				{
					base.PowerSharpParameters["PictureStream"] = value;
				}
			}

			// Token: 0x1700653A RID: 25914
			// (set) Token: 0x060090E1 RID: 37089 RVA: 0x000D3C92 File Offset: 0x000D1E92
			public virtual byte PictureData
			{
				set
				{
					base.PowerSharpParameters["PictureData"] = value;
				}
			}

			// Token: 0x1700653B RID: 25915
			// (set) Token: 0x060090E2 RID: 37090 RVA: 0x000D3CAA File Offset: 0x000D1EAA
			public virtual SwitchParameter Preview
			{
				set
				{
					base.PowerSharpParameters["Preview"] = value;
				}
			}

			// Token: 0x1700653C RID: 25916
			// (set) Token: 0x060090E3 RID: 37091 RVA: 0x000D3CC2 File Offset: 0x000D1EC2
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x1700653D RID: 25917
			// (set) Token: 0x060090E4 RID: 37092 RVA: 0x000D3CDA File Offset: 0x000D1EDA
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700653E RID: 25918
			// (set) Token: 0x060090E5 RID: 37093 RVA: 0x000D3CED File Offset: 0x000D1EED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700653F RID: 25919
			// (set) Token: 0x060090E6 RID: 37094 RVA: 0x000D3D00 File Offset: 0x000D1F00
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006540 RID: 25920
			// (set) Token: 0x060090E7 RID: 37095 RVA: 0x000D3D18 File Offset: 0x000D1F18
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17006541 RID: 25921
			// (set) Token: 0x060090E8 RID: 37096 RVA: 0x000D3D30 File Offset: 0x000D1F30
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17006542 RID: 25922
			// (set) Token: 0x060090E9 RID: 37097 RVA: 0x000D3D48 File Offset: 0x000D1F48
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17006543 RID: 25923
			// (set) Token: 0x060090EA RID: 37098 RVA: 0x000D3D60 File Offset: 0x000D1F60
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x17006544 RID: 25924
			// (set) Token: 0x060090EB RID: 37099 RVA: 0x000D3D78 File Offset: 0x000D1F78
			public virtual SwitchParameter Confirm
			{
				set
				{
					base.PowerSharpParameters["Confirm"] = value;
				}
			}
		}

		// Token: 0x02000BB0 RID: 2992
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17006545 RID: 25925
			// (set) Token: 0x060090ED RID: 37101 RVA: 0x000D3D98 File Offset: 0x000D1F98
			public virtual SwitchParameter IgnoreDefaultScope
			{
				set
				{
					base.PowerSharpParameters["IgnoreDefaultScope"] = value;
				}
			}

			// Token: 0x17006546 RID: 25926
			// (set) Token: 0x060090EE RID: 37102 RVA: 0x000D3DB0 File Offset: 0x000D1FB0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17006547 RID: 25927
			// (set) Token: 0x060090EF RID: 37103 RVA: 0x000D3DC3 File Offset: 0x000D1FC3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17006548 RID: 25928
			// (set) Token: 0x060090F0 RID: 37104 RVA: 0x000D3DD6 File Offset: 0x000D1FD6
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17006549 RID: 25929
			// (set) Token: 0x060090F1 RID: 37105 RVA: 0x000D3DEE File Offset: 0x000D1FEE
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700654A RID: 25930
			// (set) Token: 0x060090F2 RID: 37106 RVA: 0x000D3E06 File Offset: 0x000D2006
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700654B RID: 25931
			// (set) Token: 0x060090F3 RID: 37107 RVA: 0x000D3E1E File Offset: 0x000D201E
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700654C RID: 25932
			// (set) Token: 0x060090F4 RID: 37108 RVA: 0x000D3E36 File Offset: 0x000D2036
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}

			// Token: 0x1700654D RID: 25933
			// (set) Token: 0x060090F5 RID: 37109 RVA: 0x000D3E4E File Offset: 0x000D204E
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
