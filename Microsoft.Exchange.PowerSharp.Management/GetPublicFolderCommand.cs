using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000237 RID: 567
	public class GetPublicFolderCommand : SyntheticCommandWithPipelineInput<PublicFolder, PublicFolder>
	{
		// Token: 0x06002B06 RID: 11014 RVA: 0x0004F96A File Offset: 0x0004DB6A
		private GetPublicFolderCommand() : base("Get-PublicFolder")
		{
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x0004F977 File Offset: 0x0004DB77
		public GetPublicFolderCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x0004F986 File Offset: 0x0004DB86
		public virtual GetPublicFolderCommand SetParameters(GetPublicFolderCommand.RecurseParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x0004F990 File Offset: 0x0004DB90
		public virtual GetPublicFolderCommand SetParameters(GetPublicFolderCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x0004F99A File Offset: 0x0004DB9A
		public virtual GetPublicFolderCommand SetParameters(GetPublicFolderCommand.GetChildrenParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x0004F9A4 File Offset: 0x0004DBA4
		public virtual GetPublicFolderCommand SetParameters(GetPublicFolderCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000238 RID: 568
		public class RecurseParameters : ParametersBase
		{
			// Token: 0x17001251 RID: 4689
			// (set) Token: 0x06002B0C RID: 11020 RVA: 0x0004F9AE File Offset: 0x0004DBAE
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001252 RID: 4690
			// (set) Token: 0x06002B0D RID: 11021 RVA: 0x0004F9CC File Offset: 0x0004DBCC
			public virtual SwitchParameter Recurse
			{
				set
				{
					base.PowerSharpParameters["Recurse"] = value;
				}
			}

			// Token: 0x17001253 RID: 4691
			// (set) Token: 0x06002B0E RID: 11022 RVA: 0x0004F9E4 File Offset: 0x0004DBE4
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001254 RID: 4692
			// (set) Token: 0x06002B0F RID: 11023 RVA: 0x0004F9FC File Offset: 0x0004DBFC
			public virtual SwitchParameter ResidentFolders
			{
				set
				{
					base.PowerSharpParameters["ResidentFolders"] = value;
				}
			}

			// Token: 0x17001255 RID: 4693
			// (set) Token: 0x06002B10 RID: 11024 RVA: 0x0004FA14 File Offset: 0x0004DC14
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001256 RID: 4694
			// (set) Token: 0x06002B11 RID: 11025 RVA: 0x0004FA32 File Offset: 0x0004DC32
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001257 RID: 4695
			// (set) Token: 0x06002B12 RID: 11026 RVA: 0x0004FA50 File Offset: 0x0004DC50
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001258 RID: 4696
			// (set) Token: 0x06002B13 RID: 11027 RVA: 0x0004FA63 File Offset: 0x0004DC63
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001259 RID: 4697
			// (set) Token: 0x06002B14 RID: 11028 RVA: 0x0004FA7B File Offset: 0x0004DC7B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700125A RID: 4698
			// (set) Token: 0x06002B15 RID: 11029 RVA: 0x0004FA93 File Offset: 0x0004DC93
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700125B RID: 4699
			// (set) Token: 0x06002B16 RID: 11030 RVA: 0x0004FAAB File Offset: 0x0004DCAB
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x02000239 RID: 569
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700125C RID: 4700
			// (set) Token: 0x06002B18 RID: 11032 RVA: 0x0004FACB File Offset: 0x0004DCCB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x1700125D RID: 4701
			// (set) Token: 0x06002B19 RID: 11033 RVA: 0x0004FAE9 File Offset: 0x0004DCE9
			public virtual SwitchParameter ResidentFolders
			{
				set
				{
					base.PowerSharpParameters["ResidentFolders"] = value;
				}
			}

			// Token: 0x1700125E RID: 4702
			// (set) Token: 0x06002B1A RID: 11034 RVA: 0x0004FB01 File Offset: 0x0004DD01
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700125F RID: 4703
			// (set) Token: 0x06002B1B RID: 11035 RVA: 0x0004FB1F File Offset: 0x0004DD1F
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001260 RID: 4704
			// (set) Token: 0x06002B1C RID: 11036 RVA: 0x0004FB3D File Offset: 0x0004DD3D
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001261 RID: 4705
			// (set) Token: 0x06002B1D RID: 11037 RVA: 0x0004FB50 File Offset: 0x0004DD50
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001262 RID: 4706
			// (set) Token: 0x06002B1E RID: 11038 RVA: 0x0004FB68 File Offset: 0x0004DD68
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001263 RID: 4707
			// (set) Token: 0x06002B1F RID: 11039 RVA: 0x0004FB80 File Offset: 0x0004DD80
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001264 RID: 4708
			// (set) Token: 0x06002B20 RID: 11040 RVA: 0x0004FB98 File Offset: 0x0004DD98
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200023A RID: 570
		public class GetChildrenParameters : ParametersBase
		{
			// Token: 0x17001265 RID: 4709
			// (set) Token: 0x06002B22 RID: 11042 RVA: 0x0004FBB8 File Offset: 0x0004DDB8
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PublicFolderIdParameter(value) : null);
				}
			}

			// Token: 0x17001266 RID: 4710
			// (set) Token: 0x06002B23 RID: 11043 RVA: 0x0004FBD6 File Offset: 0x0004DDD6
			public virtual SwitchParameter GetChildren
			{
				set
				{
					base.PowerSharpParameters["GetChildren"] = value;
				}
			}

			// Token: 0x17001267 RID: 4711
			// (set) Token: 0x06002B24 RID: 11044 RVA: 0x0004FBEE File Offset: 0x0004DDEE
			public virtual Unlimited<uint> ResultSize
			{
				set
				{
					base.PowerSharpParameters["ResultSize"] = value;
				}
			}

			// Token: 0x17001268 RID: 4712
			// (set) Token: 0x06002B25 RID: 11045 RVA: 0x0004FC06 File Offset: 0x0004DE06
			public virtual SwitchParameter ResidentFolders
			{
				set
				{
					base.PowerSharpParameters["ResidentFolders"] = value;
				}
			}

			// Token: 0x17001269 RID: 4713
			// (set) Token: 0x06002B26 RID: 11046 RVA: 0x0004FC1E File Offset: 0x0004DE1E
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700126A RID: 4714
			// (set) Token: 0x06002B27 RID: 11047 RVA: 0x0004FC3C File Offset: 0x0004DE3C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700126B RID: 4715
			// (set) Token: 0x06002B28 RID: 11048 RVA: 0x0004FC5A File Offset: 0x0004DE5A
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700126C RID: 4716
			// (set) Token: 0x06002B29 RID: 11049 RVA: 0x0004FC6D File Offset: 0x0004DE6D
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700126D RID: 4717
			// (set) Token: 0x06002B2A RID: 11050 RVA: 0x0004FC85 File Offset: 0x0004DE85
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700126E RID: 4718
			// (set) Token: 0x06002B2B RID: 11051 RVA: 0x0004FC9D File Offset: 0x0004DE9D
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700126F RID: 4719
			// (set) Token: 0x06002B2C RID: 11052 RVA: 0x0004FCB5 File Offset: 0x0004DEB5
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}

		// Token: 0x0200023B RID: 571
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17001270 RID: 4720
			// (set) Token: 0x06002B2E RID: 11054 RVA: 0x0004FCD5 File Offset: 0x0004DED5
			public virtual SwitchParameter ResidentFolders
			{
				set
				{
					base.PowerSharpParameters["ResidentFolders"] = value;
				}
			}

			// Token: 0x17001271 RID: 4721
			// (set) Token: 0x06002B2F RID: 11055 RVA: 0x0004FCED File Offset: 0x0004DEED
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17001272 RID: 4722
			// (set) Token: 0x06002B30 RID: 11056 RVA: 0x0004FD0B File Offset: 0x0004DF0B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17001273 RID: 4723
			// (set) Token: 0x06002B31 RID: 11057 RVA: 0x0004FD29 File Offset: 0x0004DF29
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17001274 RID: 4724
			// (set) Token: 0x06002B32 RID: 11058 RVA: 0x0004FD3C File Offset: 0x0004DF3C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17001275 RID: 4725
			// (set) Token: 0x06002B33 RID: 11059 RVA: 0x0004FD54 File Offset: 0x0004DF54
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17001276 RID: 4726
			// (set) Token: 0x06002B34 RID: 11060 RVA: 0x0004FD6C File Offset: 0x0004DF6C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17001277 RID: 4727
			// (set) Token: 0x06002B35 RID: 11061 RVA: 0x0004FD84 File Offset: 0x0004DF84
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}
		}
	}
}
