using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000DDE RID: 3550
	public class NewSyncUserCommand : SyntheticCommandWithPipelineInput<ADUser, ADUser>
	{
		// Token: 0x0600D376 RID: 54134 RVA: 0x0012CCB8 File Offset: 0x0012AEB8
		private NewSyncUserCommand() : base("New-SyncUser")
		{
		}

		// Token: 0x0600D377 RID: 54135 RVA: 0x0012CCC5 File Offset: 0x0012AEC5
		public NewSyncUserCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D378 RID: 54136 RVA: 0x0012CCD4 File Offset: 0x0012AED4
		public virtual NewSyncUserCommand SetParameters(NewSyncUserCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D379 RID: 54137 RVA: 0x0012CCDE File Offset: 0x0012AEDE
		public virtual NewSyncUserCommand SetParameters(NewSyncUserCommand.WindowsLiveIDParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000DDF RID: 3551
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A373 RID: 41843
			// (set) Token: 0x0600D37A RID: 54138 RVA: 0x0012CCE8 File Offset: 0x0012AEE8
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A374 RID: 41844
			// (set) Token: 0x0600D37B RID: 54139 RVA: 0x0012CCFB File Offset: 0x0012AEFB
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A375 RID: 41845
			// (set) Token: 0x0600D37C RID: 54140 RVA: 0x0012CD13 File Offset: 0x0012AF13
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A376 RID: 41846
			// (set) Token: 0x0600D37D RID: 54141 RVA: 0x0012CD26 File Offset: 0x0012AF26
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A377 RID: 41847
			// (set) Token: 0x0600D37E RID: 54142 RVA: 0x0012CD39 File Offset: 0x0012AF39
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A378 RID: 41848
			// (set) Token: 0x0600D37F RID: 54143 RVA: 0x0012CD51 File Offset: 0x0012AF51
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A379 RID: 41849
			// (set) Token: 0x0600D380 RID: 54144 RVA: 0x0012CD69 File Offset: 0x0012AF69
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A37A RID: 41850
			// (set) Token: 0x0600D381 RID: 54145 RVA: 0x0012CD7C File Offset: 0x0012AF7C
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A37B RID: 41851
			// (set) Token: 0x0600D382 RID: 54146 RVA: 0x0012CD94 File Offset: 0x0012AF94
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A37C RID: 41852
			// (set) Token: 0x0600D383 RID: 54147 RVA: 0x0012CDAC File Offset: 0x0012AFAC
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A37D RID: 41853
			// (set) Token: 0x0600D384 RID: 54148 RVA: 0x0012CDBF File Offset: 0x0012AFBF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A37E RID: 41854
			// (set) Token: 0x0600D385 RID: 54149 RVA: 0x0012CDD2 File Offset: 0x0012AFD2
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A37F RID: 41855
			// (set) Token: 0x0600D386 RID: 54150 RVA: 0x0012CDF0 File Offset: 0x0012AFF0
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A380 RID: 41856
			// (set) Token: 0x0600D387 RID: 54151 RVA: 0x0012CE03 File Offset: 0x0012B003
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A381 RID: 41857
			// (set) Token: 0x0600D388 RID: 54152 RVA: 0x0012CE21 File Offset: 0x0012B021
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A382 RID: 41858
			// (set) Token: 0x0600D389 RID: 54153 RVA: 0x0012CE34 File Offset: 0x0012B034
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A383 RID: 41859
			// (set) Token: 0x0600D38A RID: 54154 RVA: 0x0012CE4C File Offset: 0x0012B04C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A384 RID: 41860
			// (set) Token: 0x0600D38B RID: 54155 RVA: 0x0012CE64 File Offset: 0x0012B064
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A385 RID: 41861
			// (set) Token: 0x0600D38C RID: 54156 RVA: 0x0012CE7C File Offset: 0x0012B07C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A386 RID: 41862
			// (set) Token: 0x0600D38D RID: 54157 RVA: 0x0012CE94 File Offset: 0x0012B094
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000DE0 RID: 3552
		public class WindowsLiveIDParameters : ParametersBase
		{
			// Token: 0x1700A387 RID: 41863
			// (set) Token: 0x0600D38F RID: 54159 RVA: 0x0012CEB4 File Offset: 0x0012B0B4
			public virtual SmtpAddress WindowsLiveID
			{
				set
				{
					base.PowerSharpParameters["WindowsLiveID"] = value;
				}
			}

			// Token: 0x1700A388 RID: 41864
			// (set) Token: 0x0600D390 RID: 54160 RVA: 0x0012CECC File Offset: 0x0012B0CC
			public virtual NetID NetID
			{
				set
				{
					base.PowerSharpParameters["NetID"] = value;
				}
			}

			// Token: 0x1700A389 RID: 41865
			// (set) Token: 0x0600D391 RID: 54161 RVA: 0x0012CEDF File Offset: 0x0012B0DF
			public virtual string OnPremisesObjectId
			{
				set
				{
					base.PowerSharpParameters["OnPremisesObjectId"] = value;
				}
			}

			// Token: 0x1700A38A RID: 41866
			// (set) Token: 0x0600D392 RID: 54162 RVA: 0x0012CEF2 File Offset: 0x0012B0F2
			public virtual bool IsDirSynced
			{
				set
				{
					base.PowerSharpParameters["IsDirSynced"] = value;
				}
			}

			// Token: 0x1700A38B RID: 41867
			// (set) Token: 0x0600D393 RID: 54163 RVA: 0x0012CF0A File Offset: 0x0012B10A
			public virtual MultiValuedProperty<string> DirSyncAuthorityMetadata
			{
				set
				{
					base.PowerSharpParameters["DirSyncAuthorityMetadata"] = value;
				}
			}

			// Token: 0x1700A38C RID: 41868
			// (set) Token: 0x0600D394 RID: 54164 RVA: 0x0012CF1D File Offset: 0x0012B11D
			public virtual CountryInfo UsageLocation
			{
				set
				{
					base.PowerSharpParameters["UsageLocation"] = value;
				}
			}

			// Token: 0x1700A38D RID: 41869
			// (set) Token: 0x0600D395 RID: 54165 RVA: 0x0012CF30 File Offset: 0x0012B130
			public virtual ReleaseTrack? ReleaseTrack
			{
				set
				{
					base.PowerSharpParameters["ReleaseTrack"] = value;
				}
			}

			// Token: 0x1700A38E RID: 41870
			// (set) Token: 0x0600D396 RID: 54166 RVA: 0x0012CF48 File Offset: 0x0012B148
			public virtual RemoteRecipientType RemoteRecipientType
			{
				set
				{
					base.PowerSharpParameters["RemoteRecipientType"] = value;
				}
			}

			// Token: 0x1700A38F RID: 41871
			// (set) Token: 0x0600D397 RID: 54167 RVA: 0x0012CF60 File Offset: 0x0012B160
			public virtual string ValidationOrganization
			{
				set
				{
					base.PowerSharpParameters["ValidationOrganization"] = value;
				}
			}

			// Token: 0x1700A390 RID: 41872
			// (set) Token: 0x0600D398 RID: 54168 RVA: 0x0012CF73 File Offset: 0x0012B173
			public virtual SwitchParameter AccountDisabled
			{
				set
				{
					base.PowerSharpParameters["AccountDisabled"] = value;
				}
			}

			// Token: 0x1700A391 RID: 41873
			// (set) Token: 0x0600D399 RID: 54169 RVA: 0x0012CF8B File Offset: 0x0012B18B
			public virtual DateTime? StsRefreshTokensValidFrom
			{
				set
				{
					base.PowerSharpParameters["StsRefreshTokensValidFrom"] = value;
				}
			}

			// Token: 0x1700A392 RID: 41874
			// (set) Token: 0x0600D39A RID: 54170 RVA: 0x0012CFA3 File Offset: 0x0012B1A3
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700A393 RID: 41875
			// (set) Token: 0x0600D39B RID: 54171 RVA: 0x0012CFB6 File Offset: 0x0012B1B6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A394 RID: 41876
			// (set) Token: 0x0600D39C RID: 54172 RVA: 0x0012CFC9 File Offset: 0x0012B1C9
			public virtual string OrganizationalUnit
			{
				set
				{
					base.PowerSharpParameters["OrganizationalUnit"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700A395 RID: 41877
			// (set) Token: 0x0600D39D RID: 54173 RVA: 0x0012CFE7 File Offset: 0x0012B1E7
			public virtual string ExternalDirectoryObjectId
			{
				set
				{
					base.PowerSharpParameters["ExternalDirectoryObjectId"] = value;
				}
			}

			// Token: 0x1700A396 RID: 41878
			// (set) Token: 0x0600D39E RID: 54174 RVA: 0x0012CFFA File Offset: 0x0012B1FA
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700A397 RID: 41879
			// (set) Token: 0x0600D39F RID: 54175 RVA: 0x0012D018 File Offset: 0x0012B218
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A398 RID: 41880
			// (set) Token: 0x0600D3A0 RID: 54176 RVA: 0x0012D02B File Offset: 0x0012B22B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A399 RID: 41881
			// (set) Token: 0x0600D3A1 RID: 54177 RVA: 0x0012D043 File Offset: 0x0012B243
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A39A RID: 41882
			// (set) Token: 0x0600D3A2 RID: 54178 RVA: 0x0012D05B File Offset: 0x0012B25B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A39B RID: 41883
			// (set) Token: 0x0600D3A3 RID: 54179 RVA: 0x0012D073 File Offset: 0x0012B273
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A39C RID: 41884
			// (set) Token: 0x0600D3A4 RID: 54180 RVA: 0x0012D08B File Offset: 0x0012B28B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}
	}
}
