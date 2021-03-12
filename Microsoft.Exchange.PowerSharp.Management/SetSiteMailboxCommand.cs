using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Management;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E02 RID: 3586
	public class SetSiteMailboxCommand : SyntheticCommandWithPipelineInputNoOutput<TeamMailbox>
	{
		// Token: 0x0600D57B RID: 54651 RVA: 0x0012F6C9 File Offset: 0x0012D8C9
		private SetSiteMailboxCommand() : base("Set-SiteMailbox")
		{
		}

		// Token: 0x0600D57C RID: 54652 RVA: 0x0012F6D6 File Offset: 0x0012D8D6
		public SetSiteMailboxCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600D57D RID: 54653 RVA: 0x0012F6E5 File Offset: 0x0012D8E5
		public virtual SetSiteMailboxCommand SetParameters(SetSiteMailboxCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600D57E RID: 54654 RVA: 0x0012F6EF File Offset: 0x0012D8EF
		public virtual SetSiteMailboxCommand SetParameters(SetSiteMailboxCommand.TeamMailboxIWParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E03 RID: 3587
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A530 RID: 42288
			// (set) Token: 0x0600D57F RID: 54655 RVA: 0x0012F6F9 File Offset: 0x0012D8F9
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A531 RID: 42289
			// (set) Token: 0x0600D580 RID: 54656 RVA: 0x0012F717 File Offset: 0x0012D917
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A532 RID: 42290
			// (set) Token: 0x0600D581 RID: 54657 RVA: 0x0012F72A File Offset: 0x0012D92A
			public virtual bool Active
			{
				set
				{
					base.PowerSharpParameters["Active"] = value;
				}
			}

			// Token: 0x1700A533 RID: 42291
			// (set) Token: 0x0600D582 RID: 54658 RVA: 0x0012F742 File Offset: 0x0012D942
			public virtual bool RemoveDuplicateMessages
			{
				set
				{
					base.PowerSharpParameters["RemoveDuplicateMessages"] = value;
				}
			}

			// Token: 0x1700A534 RID: 42292
			// (set) Token: 0x0600D583 RID: 54659 RVA: 0x0012F75A File Offset: 0x0012D95A
			public virtual RecipientIdParameter Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x1700A535 RID: 42293
			// (set) Token: 0x0600D584 RID: 54660 RVA: 0x0012F76D File Offset: 0x0012D96D
			public virtual RecipientIdParameter Owners
			{
				set
				{
					base.PowerSharpParameters["Owners"] = value;
				}
			}

			// Token: 0x1700A536 RID: 42294
			// (set) Token: 0x0600D585 RID: 54661 RVA: 0x0012F780 File Offset: 0x0012D980
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A537 RID: 42295
			// (set) Token: 0x0600D586 RID: 54662 RVA: 0x0012F793 File Offset: 0x0012D993
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A538 RID: 42296
			// (set) Token: 0x0600D587 RID: 54663 RVA: 0x0012F7AB File Offset: 0x0012D9AB
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A539 RID: 42297
			// (set) Token: 0x0600D588 RID: 54664 RVA: 0x0012F7BE File Offset: 0x0012D9BE
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A53A RID: 42298
			// (set) Token: 0x0600D589 RID: 54665 RVA: 0x0012F7D6 File Offset: 0x0012D9D6
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A53B RID: 42299
			// (set) Token: 0x0600D58A RID: 54666 RVA: 0x0012F7EE File Offset: 0x0012D9EE
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A53C RID: 42300
			// (set) Token: 0x0600D58B RID: 54667 RVA: 0x0012F806 File Offset: 0x0012DA06
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A53D RID: 42301
			// (set) Token: 0x0600D58C RID: 54668 RVA: 0x0012F81E File Offset: 0x0012DA1E
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E04 RID: 3588
		public class TeamMailboxIWParameters : ParametersBase
		{
			// Token: 0x1700A53E RID: 42302
			// (set) Token: 0x0600D58E RID: 54670 RVA: 0x0012F83E File Offset: 0x0012DA3E
			public virtual bool ShowInMyClient
			{
				set
				{
					base.PowerSharpParameters["ShowInMyClient"] = value;
				}
			}

			// Token: 0x1700A53F RID: 42303
			// (set) Token: 0x0600D58F RID: 54671 RVA: 0x0012F856 File Offset: 0x0012DA56
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new RecipientIdParameter(value) : null);
				}
			}

			// Token: 0x1700A540 RID: 42304
			// (set) Token: 0x0600D590 RID: 54672 RVA: 0x0012F874 File Offset: 0x0012DA74
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700A541 RID: 42305
			// (set) Token: 0x0600D591 RID: 54673 RVA: 0x0012F887 File Offset: 0x0012DA87
			public virtual bool Active
			{
				set
				{
					base.PowerSharpParameters["Active"] = value;
				}
			}

			// Token: 0x1700A542 RID: 42306
			// (set) Token: 0x0600D592 RID: 54674 RVA: 0x0012F89F File Offset: 0x0012DA9F
			public virtual bool RemoveDuplicateMessages
			{
				set
				{
					base.PowerSharpParameters["RemoveDuplicateMessages"] = value;
				}
			}

			// Token: 0x1700A543 RID: 42307
			// (set) Token: 0x0600D593 RID: 54675 RVA: 0x0012F8B7 File Offset: 0x0012DAB7
			public virtual RecipientIdParameter Members
			{
				set
				{
					base.PowerSharpParameters["Members"] = value;
				}
			}

			// Token: 0x1700A544 RID: 42308
			// (set) Token: 0x0600D594 RID: 54676 RVA: 0x0012F8CA File Offset: 0x0012DACA
			public virtual RecipientIdParameter Owners
			{
				set
				{
					base.PowerSharpParameters["Owners"] = value;
				}
			}

			// Token: 0x1700A545 RID: 42309
			// (set) Token: 0x0600D595 RID: 54677 RVA: 0x0012F8DD File Offset: 0x0012DADD
			public virtual Uri SharePointUrl
			{
				set
				{
					base.PowerSharpParameters["SharePointUrl"] = value;
				}
			}

			// Token: 0x1700A546 RID: 42310
			// (set) Token: 0x0600D596 RID: 54678 RVA: 0x0012F8F0 File Offset: 0x0012DAF0
			public virtual SwitchParameter Force
			{
				set
				{
					base.PowerSharpParameters["Force"] = value;
				}
			}

			// Token: 0x1700A547 RID: 42311
			// (set) Token: 0x0600D597 RID: 54679 RVA: 0x0012F908 File Offset: 0x0012DB08
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A548 RID: 42312
			// (set) Token: 0x0600D598 RID: 54680 RVA: 0x0012F91B File Offset: 0x0012DB1B
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A549 RID: 42313
			// (set) Token: 0x0600D599 RID: 54681 RVA: 0x0012F933 File Offset: 0x0012DB33
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A54A RID: 42314
			// (set) Token: 0x0600D59A RID: 54682 RVA: 0x0012F94B File Offset: 0x0012DB4B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A54B RID: 42315
			// (set) Token: 0x0600D59B RID: 54683 RVA: 0x0012F963 File Offset: 0x0012DB63
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A54C RID: 42316
			// (set) Token: 0x0600D59C RID: 54684 RVA: 0x0012F97B File Offset: 0x0012DB7B
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
