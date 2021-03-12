using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EBA RID: 3770
	public class SetPushNotificationAppCommand : SyntheticCommandWithPipelineInputNoOutput<PushNotificationApp>
	{
		// Token: 0x0600DD15 RID: 56597 RVA: 0x00139523 File Offset: 0x00137723
		private SetPushNotificationAppCommand() : base("Set-PushNotificationApp")
		{
		}

		// Token: 0x0600DD16 RID: 56598 RVA: 0x00139530 File Offset: 0x00137730
		public SetPushNotificationAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DD17 RID: 56599 RVA: 0x0013953F File Offset: 0x0013773F
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD18 RID: 56600 RVA: 0x00139549 File Offset: 0x00137749
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.ApnsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD19 RID: 56601 RVA: 0x00139553 File Offset: 0x00137753
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.WnsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD1A RID: 56602 RVA: 0x0013955D File Offset: 0x0013775D
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.AzureHubCreationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD1B RID: 56603 RVA: 0x00139567 File Offset: 0x00137767
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.GcmParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD1C RID: 56604 RVA: 0x00139571 File Offset: 0x00137771
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.AzureSendParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DD1D RID: 56605 RVA: 0x0013957B File Offset: 0x0013777B
		public virtual SetPushNotificationAppCommand SetParameters(SetPushNotificationAppCommand.ProxyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EBB RID: 3771
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AB5A RID: 43866
			// (set) Token: 0x0600DD1E RID: 56606 RVA: 0x00139585 File Offset: 0x00137785
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB5B RID: 43867
			// (set) Token: 0x0600DD1F RID: 56607 RVA: 0x001395A3 File Offset: 0x001377A3
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB5C RID: 43868
			// (set) Token: 0x0600DD20 RID: 56608 RVA: 0x001395B6 File Offset: 0x001377B6
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB5D RID: 43869
			// (set) Token: 0x0600DD21 RID: 56609 RVA: 0x001395C9 File Offset: 0x001377C9
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB5E RID: 43870
			// (set) Token: 0x0600DD22 RID: 56610 RVA: 0x001395E1 File Offset: 0x001377E1
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB5F RID: 43871
			// (set) Token: 0x0600DD23 RID: 56611 RVA: 0x001395F4 File Offset: 0x001377F4
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB60 RID: 43872
			// (set) Token: 0x0600DD24 RID: 56612 RVA: 0x00139607 File Offset: 0x00137807
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700AB61 RID: 43873
			// (set) Token: 0x0600DD25 RID: 56613 RVA: 0x0013961F File Offset: 0x0013781F
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700AB62 RID: 43874
			// (set) Token: 0x0600DD26 RID: 56614 RVA: 0x00139637 File Offset: 0x00137837
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700AB63 RID: 43875
			// (set) Token: 0x0600DD27 RID: 56615 RVA: 0x0013964F File Offset: 0x0013784F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB64 RID: 43876
			// (set) Token: 0x0600DD28 RID: 56616 RVA: 0x00139667 File Offset: 0x00137867
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB65 RID: 43877
			// (set) Token: 0x0600DD29 RID: 56617 RVA: 0x0013967F File Offset: 0x0013787F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB66 RID: 43878
			// (set) Token: 0x0600DD2A RID: 56618 RVA: 0x00139697 File Offset: 0x00137897
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB67 RID: 43879
			// (set) Token: 0x0600DD2B RID: 56619 RVA: 0x001396AF File Offset: 0x001378AF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EBC RID: 3772
		public class ApnsParameters : ParametersBase
		{
			// Token: 0x1700AB68 RID: 43880
			// (set) Token: 0x0600DD2D RID: 56621 RVA: 0x001396CF File Offset: 0x001378CF
			public virtual string CertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["CertificateThumbprint"] = value;
				}
			}

			// Token: 0x1700AB69 RID: 43881
			// (set) Token: 0x0600DD2E RID: 56622 RVA: 0x001396E2 File Offset: 0x001378E2
			public virtual string CertificateThumbprintFallback
			{
				set
				{
					base.PowerSharpParameters["CertificateThumbprintFallback"] = value;
				}
			}

			// Token: 0x1700AB6A RID: 43882
			// (set) Token: 0x0600DD2F RID: 56623 RVA: 0x001396F5 File Offset: 0x001378F5
			public virtual string ApnsHost
			{
				set
				{
					base.PowerSharpParameters["ApnsHost"] = value;
				}
			}

			// Token: 0x1700AB6B RID: 43883
			// (set) Token: 0x0600DD30 RID: 56624 RVA: 0x00139708 File Offset: 0x00137908
			public virtual int? ApnsPort
			{
				set
				{
					base.PowerSharpParameters["ApnsPort"] = value;
				}
			}

			// Token: 0x1700AB6C RID: 43884
			// (set) Token: 0x0600DD31 RID: 56625 RVA: 0x00139720 File Offset: 0x00137920
			public virtual string FeedbackHost
			{
				set
				{
					base.PowerSharpParameters["FeedbackHost"] = value;
				}
			}

			// Token: 0x1700AB6D RID: 43885
			// (set) Token: 0x0600DD32 RID: 56626 RVA: 0x00139733 File Offset: 0x00137933
			public virtual int? FeedbackPort
			{
				set
				{
					base.PowerSharpParameters["FeedbackPort"] = value;
				}
			}

			// Token: 0x1700AB6E RID: 43886
			// (set) Token: 0x0600DD33 RID: 56627 RVA: 0x0013974B File Offset: 0x0013794B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB6F RID: 43887
			// (set) Token: 0x0600DD34 RID: 56628 RVA: 0x00139769 File Offset: 0x00137969
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB70 RID: 43888
			// (set) Token: 0x0600DD35 RID: 56629 RVA: 0x0013977C File Offset: 0x0013797C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB71 RID: 43889
			// (set) Token: 0x0600DD36 RID: 56630 RVA: 0x0013978F File Offset: 0x0013798F
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB72 RID: 43890
			// (set) Token: 0x0600DD37 RID: 56631 RVA: 0x001397A7 File Offset: 0x001379A7
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB73 RID: 43891
			// (set) Token: 0x0600DD38 RID: 56632 RVA: 0x001397BA File Offset: 0x001379BA
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB74 RID: 43892
			// (set) Token: 0x0600DD39 RID: 56633 RVA: 0x001397CD File Offset: 0x001379CD
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700AB75 RID: 43893
			// (set) Token: 0x0600DD3A RID: 56634 RVA: 0x001397E5 File Offset: 0x001379E5
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700AB76 RID: 43894
			// (set) Token: 0x0600DD3B RID: 56635 RVA: 0x001397FD File Offset: 0x001379FD
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700AB77 RID: 43895
			// (set) Token: 0x0600DD3C RID: 56636 RVA: 0x00139815 File Offset: 0x00137A15
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB78 RID: 43896
			// (set) Token: 0x0600DD3D RID: 56637 RVA: 0x0013982D File Offset: 0x00137A2D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB79 RID: 43897
			// (set) Token: 0x0600DD3E RID: 56638 RVA: 0x00139845 File Offset: 0x00137A45
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB7A RID: 43898
			// (set) Token: 0x0600DD3F RID: 56639 RVA: 0x0013985D File Offset: 0x00137A5D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB7B RID: 43899
			// (set) Token: 0x0600DD40 RID: 56640 RVA: 0x00139875 File Offset: 0x00137A75
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EBD RID: 3773
		public class WnsParameters : ParametersBase
		{
			// Token: 0x1700AB7C RID: 43900
			// (set) Token: 0x0600DD42 RID: 56642 RVA: 0x00139895 File Offset: 0x00137A95
			public virtual string AppSid
			{
				set
				{
					base.PowerSharpParameters["AppSid"] = value;
				}
			}

			// Token: 0x1700AB7D RID: 43901
			// (set) Token: 0x0600DD43 RID: 56643 RVA: 0x001398A8 File Offset: 0x00137AA8
			public virtual string AppSecret
			{
				set
				{
					base.PowerSharpParameters["AppSecret"] = value;
				}
			}

			// Token: 0x1700AB7E RID: 43902
			// (set) Token: 0x0600DD44 RID: 56644 RVA: 0x001398BB File Offset: 0x00137ABB
			public virtual string AuthenticationUri
			{
				set
				{
					base.PowerSharpParameters["AuthenticationUri"] = value;
				}
			}

			// Token: 0x1700AB7F RID: 43903
			// (set) Token: 0x0600DD45 RID: 56645 RVA: 0x001398CE File Offset: 0x00137ACE
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AB80 RID: 43904
			// (set) Token: 0x0600DD46 RID: 56646 RVA: 0x001398E6 File Offset: 0x00137AE6
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB81 RID: 43905
			// (set) Token: 0x0600DD47 RID: 56647 RVA: 0x00139904 File Offset: 0x00137B04
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB82 RID: 43906
			// (set) Token: 0x0600DD48 RID: 56648 RVA: 0x00139917 File Offset: 0x00137B17
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB83 RID: 43907
			// (set) Token: 0x0600DD49 RID: 56649 RVA: 0x0013992A File Offset: 0x00137B2A
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB84 RID: 43908
			// (set) Token: 0x0600DD4A RID: 56650 RVA: 0x00139942 File Offset: 0x00137B42
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB85 RID: 43909
			// (set) Token: 0x0600DD4B RID: 56651 RVA: 0x00139955 File Offset: 0x00137B55
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB86 RID: 43910
			// (set) Token: 0x0600DD4C RID: 56652 RVA: 0x00139968 File Offset: 0x00137B68
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700AB87 RID: 43911
			// (set) Token: 0x0600DD4D RID: 56653 RVA: 0x00139980 File Offset: 0x00137B80
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700AB88 RID: 43912
			// (set) Token: 0x0600DD4E RID: 56654 RVA: 0x00139998 File Offset: 0x00137B98
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700AB89 RID: 43913
			// (set) Token: 0x0600DD4F RID: 56655 RVA: 0x001399B0 File Offset: 0x00137BB0
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB8A RID: 43914
			// (set) Token: 0x0600DD50 RID: 56656 RVA: 0x001399C8 File Offset: 0x00137BC8
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB8B RID: 43915
			// (set) Token: 0x0600DD51 RID: 56657 RVA: 0x001399E0 File Offset: 0x00137BE0
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB8C RID: 43916
			// (set) Token: 0x0600DD52 RID: 56658 RVA: 0x001399F8 File Offset: 0x00137BF8
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB8D RID: 43917
			// (set) Token: 0x0600DD53 RID: 56659 RVA: 0x00139A10 File Offset: 0x00137C10
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EBE RID: 3774
		public class AzureHubCreationParameters : ParametersBase
		{
			// Token: 0x1700AB8E RID: 43918
			// (set) Token: 0x0600DD55 RID: 56661 RVA: 0x00139A30 File Offset: 0x00137C30
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AB8F RID: 43919
			// (set) Token: 0x0600DD56 RID: 56662 RVA: 0x00139A48 File Offset: 0x00137C48
			public virtual string UriTemplate
			{
				set
				{
					base.PowerSharpParameters["UriTemplate"] = value;
				}
			}

			// Token: 0x1700AB90 RID: 43920
			// (set) Token: 0x0600DD57 RID: 56663 RVA: 0x00139A5B File Offset: 0x00137C5B
			public virtual string AcsUserName
			{
				set
				{
					base.PowerSharpParameters["AcsUserName"] = value;
				}
			}

			// Token: 0x1700AB91 RID: 43921
			// (set) Token: 0x0600DD58 RID: 56664 RVA: 0x00139A6E File Offset: 0x00137C6E
			public virtual string AcsUserPassword
			{
				set
				{
					base.PowerSharpParameters["AcsUserPassword"] = value;
				}
			}

			// Token: 0x1700AB92 RID: 43922
			// (set) Token: 0x0600DD59 RID: 56665 RVA: 0x00139A81 File Offset: 0x00137C81
			public virtual string AcsUriTemplate
			{
				set
				{
					base.PowerSharpParameters["AcsUriTemplate"] = value;
				}
			}

			// Token: 0x1700AB93 RID: 43923
			// (set) Token: 0x0600DD5A RID: 56666 RVA: 0x00139A94 File Offset: 0x00137C94
			public virtual string AcsScopeUriTemplate
			{
				set
				{
					base.PowerSharpParameters["AcsScopeUriTemplate"] = value;
				}
			}

			// Token: 0x1700AB94 RID: 43924
			// (set) Token: 0x0600DD5B RID: 56667 RVA: 0x00139AA7 File Offset: 0x00137CA7
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700AB95 RID: 43925
			// (set) Token: 0x0600DD5C RID: 56668 RVA: 0x00139AC5 File Offset: 0x00137CC5
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB96 RID: 43926
			// (set) Token: 0x0600DD5D RID: 56669 RVA: 0x00139AD8 File Offset: 0x00137CD8
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB97 RID: 43927
			// (set) Token: 0x0600DD5E RID: 56670 RVA: 0x00139AEB File Offset: 0x00137CEB
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB98 RID: 43928
			// (set) Token: 0x0600DD5F RID: 56671 RVA: 0x00139B03 File Offset: 0x00137D03
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB99 RID: 43929
			// (set) Token: 0x0600DD60 RID: 56672 RVA: 0x00139B16 File Offset: 0x00137D16
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB9A RID: 43930
			// (set) Token: 0x0600DD61 RID: 56673 RVA: 0x00139B29 File Offset: 0x00137D29
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700AB9B RID: 43931
			// (set) Token: 0x0600DD62 RID: 56674 RVA: 0x00139B41 File Offset: 0x00137D41
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700AB9C RID: 43932
			// (set) Token: 0x0600DD63 RID: 56675 RVA: 0x00139B59 File Offset: 0x00137D59
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700AB9D RID: 43933
			// (set) Token: 0x0600DD64 RID: 56676 RVA: 0x00139B71 File Offset: 0x00137D71
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB9E RID: 43934
			// (set) Token: 0x0600DD65 RID: 56677 RVA: 0x00139B89 File Offset: 0x00137D89
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB9F RID: 43935
			// (set) Token: 0x0600DD66 RID: 56678 RVA: 0x00139BA1 File Offset: 0x00137DA1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700ABA0 RID: 43936
			// (set) Token: 0x0600DD67 RID: 56679 RVA: 0x00139BB9 File Offset: 0x00137DB9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700ABA1 RID: 43937
			// (set) Token: 0x0600DD68 RID: 56680 RVA: 0x00139BD1 File Offset: 0x00137DD1
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EBF RID: 3775
		public class GcmParameters : ParametersBase
		{
			// Token: 0x1700ABA2 RID: 43938
			// (set) Token: 0x0600DD6A RID: 56682 RVA: 0x00139BF1 File Offset: 0x00137DF1
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700ABA3 RID: 43939
			// (set) Token: 0x0600DD6B RID: 56683 RVA: 0x00139C09 File Offset: 0x00137E09
			public virtual string Sender
			{
				set
				{
					base.PowerSharpParameters["Sender"] = value;
				}
			}

			// Token: 0x1700ABA4 RID: 43940
			// (set) Token: 0x0600DD6C RID: 56684 RVA: 0x00139C1C File Offset: 0x00137E1C
			public virtual string SenderAuthToken
			{
				set
				{
					base.PowerSharpParameters["SenderAuthToken"] = value;
				}
			}

			// Token: 0x1700ABA5 RID: 43941
			// (set) Token: 0x0600DD6D RID: 56685 RVA: 0x00139C2F File Offset: 0x00137E2F
			public virtual string GcmServiceUri
			{
				set
				{
					base.PowerSharpParameters["GcmServiceUri"] = value;
				}
			}

			// Token: 0x1700ABA6 RID: 43942
			// (set) Token: 0x0600DD6E RID: 56686 RVA: 0x00139C42 File Offset: 0x00137E42
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700ABA7 RID: 43943
			// (set) Token: 0x0600DD6F RID: 56687 RVA: 0x00139C60 File Offset: 0x00137E60
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700ABA8 RID: 43944
			// (set) Token: 0x0600DD70 RID: 56688 RVA: 0x00139C73 File Offset: 0x00137E73
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700ABA9 RID: 43945
			// (set) Token: 0x0600DD71 RID: 56689 RVA: 0x00139C86 File Offset: 0x00137E86
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700ABAA RID: 43946
			// (set) Token: 0x0600DD72 RID: 56690 RVA: 0x00139C9E File Offset: 0x00137E9E
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700ABAB RID: 43947
			// (set) Token: 0x0600DD73 RID: 56691 RVA: 0x00139CB1 File Offset: 0x00137EB1
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700ABAC RID: 43948
			// (set) Token: 0x0600DD74 RID: 56692 RVA: 0x00139CC4 File Offset: 0x00137EC4
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700ABAD RID: 43949
			// (set) Token: 0x0600DD75 RID: 56693 RVA: 0x00139CDC File Offset: 0x00137EDC
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700ABAE RID: 43950
			// (set) Token: 0x0600DD76 RID: 56694 RVA: 0x00139CF4 File Offset: 0x00137EF4
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700ABAF RID: 43951
			// (set) Token: 0x0600DD77 RID: 56695 RVA: 0x00139D0C File Offset: 0x00137F0C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700ABB0 RID: 43952
			// (set) Token: 0x0600DD78 RID: 56696 RVA: 0x00139D24 File Offset: 0x00137F24
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700ABB1 RID: 43953
			// (set) Token: 0x0600DD79 RID: 56697 RVA: 0x00139D3C File Offset: 0x00137F3C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700ABB2 RID: 43954
			// (set) Token: 0x0600DD7A RID: 56698 RVA: 0x00139D54 File Offset: 0x00137F54
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700ABB3 RID: 43955
			// (set) Token: 0x0600DD7B RID: 56699 RVA: 0x00139D6C File Offset: 0x00137F6C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EC0 RID: 3776
		public class AzureSendParameters : ParametersBase
		{
			// Token: 0x1700ABB4 RID: 43956
			// (set) Token: 0x0600DD7D RID: 56701 RVA: 0x00139D8C File Offset: 0x00137F8C
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700ABB5 RID: 43957
			// (set) Token: 0x0600DD7E RID: 56702 RVA: 0x00139DA4 File Offset: 0x00137FA4
			public virtual string SasKeyName
			{
				set
				{
					base.PowerSharpParameters["SasKeyName"] = value;
				}
			}

			// Token: 0x1700ABB6 RID: 43958
			// (set) Token: 0x0600DD7F RID: 56703 RVA: 0x00139DB7 File Offset: 0x00137FB7
			public virtual string SasKeyValue
			{
				set
				{
					base.PowerSharpParameters["SasKeyValue"] = value;
				}
			}

			// Token: 0x1700ABB7 RID: 43959
			// (set) Token: 0x0600DD80 RID: 56704 RVA: 0x00139DCA File Offset: 0x00137FCA
			public virtual string UriTemplate
			{
				set
				{
					base.PowerSharpParameters["UriTemplate"] = value;
				}
			}

			// Token: 0x1700ABB8 RID: 43960
			// (set) Token: 0x0600DD81 RID: 56705 RVA: 0x00139DDD File Offset: 0x00137FDD
			public virtual string RegistrationTemplate
			{
				set
				{
					base.PowerSharpParameters["RegistrationTemplate"] = value;
				}
			}

			// Token: 0x1700ABB9 RID: 43961
			// (set) Token: 0x0600DD82 RID: 56706 RVA: 0x00139DF0 File Offset: 0x00137FF0
			public virtual bool? RegistrationEnabled
			{
				set
				{
					base.PowerSharpParameters["RegistrationEnabled"] = value;
				}
			}

			// Token: 0x1700ABBA RID: 43962
			// (set) Token: 0x0600DD83 RID: 56707 RVA: 0x00139E08 File Offset: 0x00138008
			public virtual bool? MultifactorRegistrationEnabled
			{
				set
				{
					base.PowerSharpParameters["MultifactorRegistrationEnabled"] = value;
				}
			}

			// Token: 0x1700ABBB RID: 43963
			// (set) Token: 0x0600DD84 RID: 56708 RVA: 0x00139E20 File Offset: 0x00138020
			public virtual string PartitionName
			{
				set
				{
					base.PowerSharpParameters["PartitionName"] = value;
				}
			}

			// Token: 0x1700ABBC RID: 43964
			// (set) Token: 0x0600DD85 RID: 56709 RVA: 0x00139E33 File Offset: 0x00138033
			public virtual bool? IsDefaultPartitionName
			{
				set
				{
					base.PowerSharpParameters["IsDefaultPartitionName"] = value;
				}
			}

			// Token: 0x1700ABBD RID: 43965
			// (set) Token: 0x0600DD86 RID: 56710 RVA: 0x00139E4B File Offset: 0x0013804B
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700ABBE RID: 43966
			// (set) Token: 0x0600DD87 RID: 56711 RVA: 0x00139E69 File Offset: 0x00138069
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700ABBF RID: 43967
			// (set) Token: 0x0600DD88 RID: 56712 RVA: 0x00139E7C File Offset: 0x0013807C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700ABC0 RID: 43968
			// (set) Token: 0x0600DD89 RID: 56713 RVA: 0x00139E8F File Offset: 0x0013808F
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700ABC1 RID: 43969
			// (set) Token: 0x0600DD8A RID: 56714 RVA: 0x00139EA7 File Offset: 0x001380A7
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700ABC2 RID: 43970
			// (set) Token: 0x0600DD8B RID: 56715 RVA: 0x00139EBA File Offset: 0x001380BA
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700ABC3 RID: 43971
			// (set) Token: 0x0600DD8C RID: 56716 RVA: 0x00139ECD File Offset: 0x001380CD
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700ABC4 RID: 43972
			// (set) Token: 0x0600DD8D RID: 56717 RVA: 0x00139EE5 File Offset: 0x001380E5
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700ABC5 RID: 43973
			// (set) Token: 0x0600DD8E RID: 56718 RVA: 0x00139EFD File Offset: 0x001380FD
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700ABC6 RID: 43974
			// (set) Token: 0x0600DD8F RID: 56719 RVA: 0x00139F15 File Offset: 0x00138115
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700ABC7 RID: 43975
			// (set) Token: 0x0600DD90 RID: 56720 RVA: 0x00139F2D File Offset: 0x0013812D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700ABC8 RID: 43976
			// (set) Token: 0x0600DD91 RID: 56721 RVA: 0x00139F45 File Offset: 0x00138145
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700ABC9 RID: 43977
			// (set) Token: 0x0600DD92 RID: 56722 RVA: 0x00139F5D File Offset: 0x0013815D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700ABCA RID: 43978
			// (set) Token: 0x0600DD93 RID: 56723 RVA: 0x00139F75 File Offset: 0x00138175
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EC1 RID: 3777
		public class ProxyParameters : ParametersBase
		{
			// Token: 0x1700ABCB RID: 43979
			// (set) Token: 0x0600DD95 RID: 56725 RVA: 0x00139F95 File Offset: 0x00138195
			public virtual string Uri
			{
				set
				{
					base.PowerSharpParameters["Uri"] = value;
				}
			}

			// Token: 0x1700ABCC RID: 43980
			// (set) Token: 0x0600DD96 RID: 56726 RVA: 0x00139FA8 File Offset: 0x001381A8
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = value;
				}
			}

			// Token: 0x1700ABCD RID: 43981
			// (set) Token: 0x0600DD97 RID: 56727 RVA: 0x00139FBB File Offset: 0x001381BB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new PushNotificationAppIdParameter(value) : null);
				}
			}

			// Token: 0x1700ABCE RID: 43982
			// (set) Token: 0x0600DD98 RID: 56728 RVA: 0x00139FD9 File Offset: 0x001381D9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700ABCF RID: 43983
			// (set) Token: 0x0600DD99 RID: 56729 RVA: 0x00139FEC File Offset: 0x001381EC
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700ABD0 RID: 43984
			// (set) Token: 0x0600DD9A RID: 56730 RVA: 0x00139FFF File Offset: 0x001381FF
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700ABD1 RID: 43985
			// (set) Token: 0x0600DD9B RID: 56731 RVA: 0x0013A017 File Offset: 0x00138217
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700ABD2 RID: 43986
			// (set) Token: 0x0600DD9C RID: 56732 RVA: 0x0013A02A File Offset: 0x0013822A
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700ABD3 RID: 43987
			// (set) Token: 0x0600DD9D RID: 56733 RVA: 0x0013A03D File Offset: 0x0013823D
			public virtual int? QueueSize
			{
				set
				{
					base.PowerSharpParameters["QueueSize"] = value;
				}
			}

			// Token: 0x1700ABD4 RID: 43988
			// (set) Token: 0x0600DD9E RID: 56734 RVA: 0x0013A055 File Offset: 0x00138255
			public virtual int? NumberOfChannels
			{
				set
				{
					base.PowerSharpParameters["NumberOfChannels"] = value;
				}
			}

			// Token: 0x1700ABD5 RID: 43989
			// (set) Token: 0x0600DD9F RID: 56735 RVA: 0x0013A06D File Offset: 0x0013826D
			public virtual int? BackOffTimeInSeconds
			{
				set
				{
					base.PowerSharpParameters["BackOffTimeInSeconds"] = value;
				}
			}

			// Token: 0x1700ABD6 RID: 43990
			// (set) Token: 0x0600DDA0 RID: 56736 RVA: 0x0013A085 File Offset: 0x00138285
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700ABD7 RID: 43991
			// (set) Token: 0x0600DDA1 RID: 56737 RVA: 0x0013A09D File Offset: 0x0013829D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700ABD8 RID: 43992
			// (set) Token: 0x0600DDA2 RID: 56738 RVA: 0x0013A0B5 File Offset: 0x001382B5
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700ABD9 RID: 43993
			// (set) Token: 0x0600DDA3 RID: 56739 RVA: 0x0013A0CD File Offset: 0x001382CD
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700ABDA RID: 43994
			// (set) Token: 0x0600DDA4 RID: 56740 RVA: 0x0013A0E5 File Offset: 0x001382E5
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
