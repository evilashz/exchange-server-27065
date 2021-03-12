using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x020007FD RID: 2045
	public class SetOfflineAddressBookCommand : SyntheticCommandWithPipelineInputNoOutput<OfflineAddressBook>
	{
		// Token: 0x06006569 RID: 25961 RVA: 0x0009AEFF File Offset: 0x000990FF
		private SetOfflineAddressBookCommand() : base("Set-OfflineAddressBook")
		{
		}

		// Token: 0x0600656A RID: 25962 RVA: 0x0009AF0C File Offset: 0x0009910C
		public SetOfflineAddressBookCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600656B RID: 25963 RVA: 0x0009AF1B File Offset: 0x0009911B
		public virtual SetOfflineAddressBookCommand SetParameters(SetOfflineAddressBookCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x0009AF25 File Offset: 0x00099125
		public virtual SetOfflineAddressBookCommand SetParameters(SetOfflineAddressBookCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x020007FE RID: 2046
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x17004128 RID: 16680
			// (set) Token: 0x0600656D RID: 25965 RVA: 0x0009AF2F File Offset: 0x0009912F
			public virtual string GeneratingMailbox
			{
				set
				{
					base.PowerSharpParameters["GeneratingMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17004129 RID: 16681
			// (set) Token: 0x0600656E RID: 25966 RVA: 0x0009AF4D File Offset: 0x0009914D
			public virtual AddressBookBaseIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x1700412A RID: 16682
			// (set) Token: 0x0600656F RID: 25967 RVA: 0x0009AF60 File Offset: 0x00099160
			public virtual VirtualDirectoryIdParameter VirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["VirtualDirectories"] = value;
				}
			}

			// Token: 0x1700412B RID: 16683
			// (set) Token: 0x06006570 RID: 25968 RVA: 0x0009AF73 File Offset: 0x00099173
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x1700412C RID: 16684
			// (set) Token: 0x06006571 RID: 25969 RVA: 0x0009AF8B File Offset: 0x0009918B
			public virtual SwitchParameter UseDefaultAttributes
			{
				set
				{
					base.PowerSharpParameters["UseDefaultAttributes"] = value;
				}
			}

			// Token: 0x1700412D RID: 16685
			// (set) Token: 0x06006572 RID: 25970 RVA: 0x0009AFA3 File Offset: 0x000991A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700412E RID: 16686
			// (set) Token: 0x06006573 RID: 25971 RVA: 0x0009AFB6 File Offset: 0x000991B6
			public virtual MultiValuedProperty<OfflineAddressBookVersion> Versions
			{
				set
				{
					base.PowerSharpParameters["Versions"] = value;
				}
			}

			// Token: 0x1700412F RID: 16687
			// (set) Token: 0x06006574 RID: 25972 RVA: 0x0009AFC9 File Offset: 0x000991C9
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17004130 RID: 16688
			// (set) Token: 0x06006575 RID: 25973 RVA: 0x0009AFE1 File Offset: 0x000991E1
			public virtual bool PublicFolderDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDistributionEnabled"] = value;
				}
			}

			// Token: 0x17004131 RID: 16689
			// (set) Token: 0x06006576 RID: 25974 RVA: 0x0009AFF9 File Offset: 0x000991F9
			public virtual bool GlobalWebDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalWebDistributionEnabled"] = value;
				}
			}

			// Token: 0x17004132 RID: 16690
			// (set) Token: 0x06006577 RID: 25975 RVA: 0x0009B011 File Offset: 0x00099211
			public virtual bool ShadowMailboxDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["ShadowMailboxDistributionEnabled"] = value;
				}
			}

			// Token: 0x17004133 RID: 16691
			// (set) Token: 0x06006578 RID: 25976 RVA: 0x0009B029 File Offset: 0x00099229
			public virtual int MaxBinaryPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxBinaryPropertySize"] = value;
				}
			}

			// Token: 0x17004134 RID: 16692
			// (set) Token: 0x06006579 RID: 25977 RVA: 0x0009B041 File Offset: 0x00099241
			public virtual int MaxMultivaluedBinaryPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxMultivaluedBinaryPropertySize"] = value;
				}
			}

			// Token: 0x17004135 RID: 16693
			// (set) Token: 0x0600657A RID: 25978 RVA: 0x0009B059 File Offset: 0x00099259
			public virtual int MaxStringPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxStringPropertySize"] = value;
				}
			}

			// Token: 0x17004136 RID: 16694
			// (set) Token: 0x0600657B RID: 25979 RVA: 0x0009B071 File Offset: 0x00099271
			public virtual int MaxMultivaluedStringPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxMultivaluedStringPropertySize"] = value;
				}
			}

			// Token: 0x17004137 RID: 16695
			// (set) Token: 0x0600657C RID: 25980 RVA: 0x0009B089 File Offset: 0x00099289
			public virtual MultiValuedProperty<OfflineAddressBookMapiProperty> ConfiguredAttributes
			{
				set
				{
					base.PowerSharpParameters["ConfiguredAttributes"] = value;
				}
			}

			// Token: 0x17004138 RID: 16696
			// (set) Token: 0x0600657D RID: 25981 RVA: 0x0009B09C File Offset: 0x0009929C
			public virtual Unlimited<int>? DiffRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["DiffRetentionPeriod"] = value;
				}
			}

			// Token: 0x17004139 RID: 16697
			// (set) Token: 0x0600657E RID: 25982 RVA: 0x0009B0B4 File Offset: 0x000992B4
			public virtual Schedule Schedule
			{
				set
				{
					base.PowerSharpParameters["Schedule"] = value;
				}
			}

			// Token: 0x1700413A RID: 16698
			// (set) Token: 0x0600657F RID: 25983 RVA: 0x0009B0C7 File Offset: 0x000992C7
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700413B RID: 16699
			// (set) Token: 0x06006580 RID: 25984 RVA: 0x0009B0DA File Offset: 0x000992DA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700413C RID: 16700
			// (set) Token: 0x06006581 RID: 25985 RVA: 0x0009B0F2 File Offset: 0x000992F2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700413D RID: 16701
			// (set) Token: 0x06006582 RID: 25986 RVA: 0x0009B10A File Offset: 0x0009930A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700413E RID: 16702
			// (set) Token: 0x06006583 RID: 25987 RVA: 0x0009B122 File Offset: 0x00099322
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700413F RID: 16703
			// (set) Token: 0x06006584 RID: 25988 RVA: 0x0009B13A File Offset: 0x0009933A
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x020007FF RID: 2047
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x17004140 RID: 16704
			// (set) Token: 0x06006586 RID: 25990 RVA: 0x0009B15A File Offset: 0x0009935A
			public virtual OfflineAddressBookIdParameter Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = value;
				}
			}

			// Token: 0x17004141 RID: 16705
			// (set) Token: 0x06006587 RID: 25991 RVA: 0x0009B16D File Offset: 0x0009936D
			public virtual string GeneratingMailbox
			{
				set
				{
					base.PowerSharpParameters["GeneratingMailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x17004142 RID: 16706
			// (set) Token: 0x06006588 RID: 25992 RVA: 0x0009B18B File Offset: 0x0009938B
			public virtual AddressBookBaseIdParameter AddressLists
			{
				set
				{
					base.PowerSharpParameters["AddressLists"] = value;
				}
			}

			// Token: 0x17004143 RID: 16707
			// (set) Token: 0x06006589 RID: 25993 RVA: 0x0009B19E File Offset: 0x0009939E
			public virtual VirtualDirectoryIdParameter VirtualDirectories
			{
				set
				{
					base.PowerSharpParameters["VirtualDirectories"] = value;
				}
			}

			// Token: 0x17004144 RID: 16708
			// (set) Token: 0x0600658A RID: 25994 RVA: 0x0009B1B1 File Offset: 0x000993B1
			public virtual SwitchParameter ApplyMandatoryProperties
			{
				set
				{
					base.PowerSharpParameters["ApplyMandatoryProperties"] = value;
				}
			}

			// Token: 0x17004145 RID: 16709
			// (set) Token: 0x0600658B RID: 25995 RVA: 0x0009B1C9 File Offset: 0x000993C9
			public virtual SwitchParameter UseDefaultAttributes
			{
				set
				{
					base.PowerSharpParameters["UseDefaultAttributes"] = value;
				}
			}

			// Token: 0x17004146 RID: 16710
			// (set) Token: 0x0600658C RID: 25996 RVA: 0x0009B1E1 File Offset: 0x000993E1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004147 RID: 16711
			// (set) Token: 0x0600658D RID: 25997 RVA: 0x0009B1F4 File Offset: 0x000993F4
			public virtual MultiValuedProperty<OfflineAddressBookVersion> Versions
			{
				set
				{
					base.PowerSharpParameters["Versions"] = value;
				}
			}

			// Token: 0x17004148 RID: 16712
			// (set) Token: 0x0600658E RID: 25998 RVA: 0x0009B207 File Offset: 0x00099407
			public virtual bool IsDefault
			{
				set
				{
					base.PowerSharpParameters["IsDefault"] = value;
				}
			}

			// Token: 0x17004149 RID: 16713
			// (set) Token: 0x0600658F RID: 25999 RVA: 0x0009B21F File Offset: 0x0009941F
			public virtual bool PublicFolderDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["PublicFolderDistributionEnabled"] = value;
				}
			}

			// Token: 0x1700414A RID: 16714
			// (set) Token: 0x06006590 RID: 26000 RVA: 0x0009B237 File Offset: 0x00099437
			public virtual bool GlobalWebDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["GlobalWebDistributionEnabled"] = value;
				}
			}

			// Token: 0x1700414B RID: 16715
			// (set) Token: 0x06006591 RID: 26001 RVA: 0x0009B24F File Offset: 0x0009944F
			public virtual bool ShadowMailboxDistributionEnabled
			{
				set
				{
					base.PowerSharpParameters["ShadowMailboxDistributionEnabled"] = value;
				}
			}

			// Token: 0x1700414C RID: 16716
			// (set) Token: 0x06006592 RID: 26002 RVA: 0x0009B267 File Offset: 0x00099467
			public virtual int MaxBinaryPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxBinaryPropertySize"] = value;
				}
			}

			// Token: 0x1700414D RID: 16717
			// (set) Token: 0x06006593 RID: 26003 RVA: 0x0009B27F File Offset: 0x0009947F
			public virtual int MaxMultivaluedBinaryPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxMultivaluedBinaryPropertySize"] = value;
				}
			}

			// Token: 0x1700414E RID: 16718
			// (set) Token: 0x06006594 RID: 26004 RVA: 0x0009B297 File Offset: 0x00099497
			public virtual int MaxStringPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxStringPropertySize"] = value;
				}
			}

			// Token: 0x1700414F RID: 16719
			// (set) Token: 0x06006595 RID: 26005 RVA: 0x0009B2AF File Offset: 0x000994AF
			public virtual int MaxMultivaluedStringPropertySize
			{
				set
				{
					base.PowerSharpParameters["MaxMultivaluedStringPropertySize"] = value;
				}
			}

			// Token: 0x17004150 RID: 16720
			// (set) Token: 0x06006596 RID: 26006 RVA: 0x0009B2C7 File Offset: 0x000994C7
			public virtual MultiValuedProperty<OfflineAddressBookMapiProperty> ConfiguredAttributes
			{
				set
				{
					base.PowerSharpParameters["ConfiguredAttributes"] = value;
				}
			}

			// Token: 0x17004151 RID: 16721
			// (set) Token: 0x06006597 RID: 26007 RVA: 0x0009B2DA File Offset: 0x000994DA
			public virtual Unlimited<int>? DiffRetentionPeriod
			{
				set
				{
					base.PowerSharpParameters["DiffRetentionPeriod"] = value;
				}
			}

			// Token: 0x17004152 RID: 16722
			// (set) Token: 0x06006598 RID: 26008 RVA: 0x0009B2F2 File Offset: 0x000994F2
			public virtual Schedule Schedule
			{
				set
				{
					base.PowerSharpParameters["Schedule"] = value;
				}
			}

			// Token: 0x17004153 RID: 16723
			// (set) Token: 0x06006599 RID: 26009 RVA: 0x0009B305 File Offset: 0x00099505
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004154 RID: 16724
			// (set) Token: 0x0600659A RID: 26010 RVA: 0x0009B318 File Offset: 0x00099518
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004155 RID: 16725
			// (set) Token: 0x0600659B RID: 26011 RVA: 0x0009B330 File Offset: 0x00099530
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004156 RID: 16726
			// (set) Token: 0x0600659C RID: 26012 RVA: 0x0009B348 File Offset: 0x00099548
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004157 RID: 16727
			// (set) Token: 0x0600659D RID: 26013 RVA: 0x0009B360 File Offset: 0x00099560
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004158 RID: 16728
			// (set) Token: 0x0600659E RID: 26014 RVA: 0x0009B378 File Offset: 0x00099578
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
