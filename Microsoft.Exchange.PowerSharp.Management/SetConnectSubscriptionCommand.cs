using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Transport.Sync.Common.Subscription.Connect;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000E7F RID: 3711
	public class SetConnectSubscriptionCommand : SyntheticCommandWithPipelineInputNoOutput<ConnectSubscriptionProxy>
	{
		// Token: 0x0600DAC4 RID: 56004 RVA: 0x0013657B File Offset: 0x0013477B
		private SetConnectSubscriptionCommand() : base("Set-ConnectSubscription")
		{
		}

		// Token: 0x0600DAC5 RID: 56005 RVA: 0x00136588 File Offset: 0x00134788
		public SetConnectSubscriptionCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DAC6 RID: 56006 RVA: 0x00136597 File Offset: 0x00134797
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DAC7 RID: 56007 RVA: 0x001365A1 File Offset: 0x001347A1
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.FacebookParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DAC8 RID: 56008 RVA: 0x001365AB File Offset: 0x001347AB
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.LinkedInParameterSetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DAC9 RID: 56009 RVA: 0x001365B5 File Offset: 0x001347B5
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.DisableSubscriptionAsPoisonParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DACA RID: 56010 RVA: 0x001365BF File Offset: 0x001347BF
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.SubscriptionModificationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DACB RID: 56011 RVA: 0x001365C9 File Offset: 0x001347C9
		public virtual SetConnectSubscriptionCommand SetParameters(SetConnectSubscriptionCommand.IdentityParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000E80 RID: 3712
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700A97F RID: 43391
			// (set) Token: 0x0600DACC RID: 56012 RVA: 0x001365D3 File Offset: 0x001347D3
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A980 RID: 43392
			// (set) Token: 0x0600DACD RID: 56013 RVA: 0x001365F1 File Offset: 0x001347F1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A981 RID: 43393
			// (set) Token: 0x0600DACE RID: 56014 RVA: 0x00136604 File Offset: 0x00134804
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A982 RID: 43394
			// (set) Token: 0x0600DACF RID: 56015 RVA: 0x0013661C File Offset: 0x0013481C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A983 RID: 43395
			// (set) Token: 0x0600DAD0 RID: 56016 RVA: 0x00136634 File Offset: 0x00134834
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A984 RID: 43396
			// (set) Token: 0x0600DAD1 RID: 56017 RVA: 0x0013664C File Offset: 0x0013484C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A985 RID: 43397
			// (set) Token: 0x0600DAD2 RID: 56018 RVA: 0x00136664 File Offset: 0x00134864
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E81 RID: 3713
		public class FacebookParameterSetParameters : ParametersBase
		{
			// Token: 0x1700A986 RID: 43398
			// (set) Token: 0x0600DAD4 RID: 56020 RVA: 0x00136684 File Offset: 0x00134884
			public virtual SwitchParameter Facebook
			{
				set
				{
					base.PowerSharpParameters["Facebook"] = value;
				}
			}

			// Token: 0x1700A987 RID: 43399
			// (set) Token: 0x0600DAD5 RID: 56021 RVA: 0x0013669C File Offset: 0x0013489C
			public virtual string AppAuthorizationCode
			{
				set
				{
					base.PowerSharpParameters["AppAuthorizationCode"] = value;
				}
			}

			// Token: 0x1700A988 RID: 43400
			// (set) Token: 0x0600DAD6 RID: 56022 RVA: 0x001366AF File Offset: 0x001348AF
			public virtual string RedirectUri
			{
				set
				{
					base.PowerSharpParameters["RedirectUri"] = value;
				}
			}

			// Token: 0x1700A989 RID: 43401
			// (set) Token: 0x0600DAD7 RID: 56023 RVA: 0x001366C2 File Offset: 0x001348C2
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A98A RID: 43402
			// (set) Token: 0x0600DAD8 RID: 56024 RVA: 0x001366E0 File Offset: 0x001348E0
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A98B RID: 43403
			// (set) Token: 0x0600DAD9 RID: 56025 RVA: 0x001366F3 File Offset: 0x001348F3
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A98C RID: 43404
			// (set) Token: 0x0600DADA RID: 56026 RVA: 0x0013670B File Offset: 0x0013490B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A98D RID: 43405
			// (set) Token: 0x0600DADB RID: 56027 RVA: 0x00136723 File Offset: 0x00134923
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A98E RID: 43406
			// (set) Token: 0x0600DADC RID: 56028 RVA: 0x0013673B File Offset: 0x0013493B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A98F RID: 43407
			// (set) Token: 0x0600DADD RID: 56029 RVA: 0x00136753 File Offset: 0x00134953
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E82 RID: 3714
		public class LinkedInParameterSetParameters : ParametersBase
		{
			// Token: 0x1700A990 RID: 43408
			// (set) Token: 0x0600DADF RID: 56031 RVA: 0x00136773 File Offset: 0x00134973
			public virtual SwitchParameter LinkedIn
			{
				set
				{
					base.PowerSharpParameters["LinkedIn"] = value;
				}
			}

			// Token: 0x1700A991 RID: 43409
			// (set) Token: 0x0600DAE0 RID: 56032 RVA: 0x0013678B File Offset: 0x0013498B
			public virtual string RequestToken
			{
				set
				{
					base.PowerSharpParameters["RequestToken"] = value;
				}
			}

			// Token: 0x1700A992 RID: 43410
			// (set) Token: 0x0600DAE1 RID: 56033 RVA: 0x0013679E File Offset: 0x0013499E
			public virtual string RequestSecret
			{
				set
				{
					base.PowerSharpParameters["RequestSecret"] = value;
				}
			}

			// Token: 0x1700A993 RID: 43411
			// (set) Token: 0x0600DAE2 RID: 56034 RVA: 0x001367B1 File Offset: 0x001349B1
			public virtual string OAuthVerifier
			{
				set
				{
					base.PowerSharpParameters["OAuthVerifier"] = value;
				}
			}

			// Token: 0x1700A994 RID: 43412
			// (set) Token: 0x0600DAE3 RID: 56035 RVA: 0x001367C4 File Offset: 0x001349C4
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A995 RID: 43413
			// (set) Token: 0x0600DAE4 RID: 56036 RVA: 0x001367E2 File Offset: 0x001349E2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A996 RID: 43414
			// (set) Token: 0x0600DAE5 RID: 56037 RVA: 0x001367F5 File Offset: 0x001349F5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A997 RID: 43415
			// (set) Token: 0x0600DAE6 RID: 56038 RVA: 0x0013680D File Offset: 0x00134A0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A998 RID: 43416
			// (set) Token: 0x0600DAE7 RID: 56039 RVA: 0x00136825 File Offset: 0x00134A25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A999 RID: 43417
			// (set) Token: 0x0600DAE8 RID: 56040 RVA: 0x0013683D File Offset: 0x00134A3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A99A RID: 43418
			// (set) Token: 0x0600DAE9 RID: 56041 RVA: 0x00136855 File Offset: 0x00134A55
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E83 RID: 3715
		public class DisableSubscriptionAsPoisonParameters : ParametersBase
		{
			// Token: 0x1700A99B RID: 43419
			// (set) Token: 0x0600DAEB RID: 56043 RVA: 0x00136875 File Offset: 0x00134A75
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A99C RID: 43420
			// (set) Token: 0x0600DAEC RID: 56044 RVA: 0x00136893 File Offset: 0x00134A93
			public virtual SwitchParameter DisableAsPoison
			{
				set
				{
					base.PowerSharpParameters["DisableAsPoison"] = value;
				}
			}

			// Token: 0x1700A99D RID: 43421
			// (set) Token: 0x0600DAED RID: 56045 RVA: 0x001368AB File Offset: 0x00134AAB
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A99E RID: 43422
			// (set) Token: 0x0600DAEE RID: 56046 RVA: 0x001368C9 File Offset: 0x00134AC9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A99F RID: 43423
			// (set) Token: 0x0600DAEF RID: 56047 RVA: 0x001368DC File Offset: 0x00134ADC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9A0 RID: 43424
			// (set) Token: 0x0600DAF0 RID: 56048 RVA: 0x001368F4 File Offset: 0x00134AF4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9A1 RID: 43425
			// (set) Token: 0x0600DAF1 RID: 56049 RVA: 0x0013690C File Offset: 0x00134B0C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9A2 RID: 43426
			// (set) Token: 0x0600DAF2 RID: 56050 RVA: 0x00136924 File Offset: 0x00134B24
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9A3 RID: 43427
			// (set) Token: 0x0600DAF3 RID: 56051 RVA: 0x0013693C File Offset: 0x00134B3C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E84 RID: 3716
		public class SubscriptionModificationParameters : ParametersBase
		{
			// Token: 0x1700A9A4 RID: 43428
			// (set) Token: 0x0600DAF5 RID: 56053 RVA: 0x0013695C File Offset: 0x00134B5C
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9A5 RID: 43429
			// (set) Token: 0x0600DAF6 RID: 56054 RVA: 0x0013697A File Offset: 0x00134B7A
			public virtual SwitchParameter EnablePoisonSubscription
			{
				set
				{
					base.PowerSharpParameters["EnablePoisonSubscription"] = value;
				}
			}

			// Token: 0x1700A9A6 RID: 43430
			// (set) Token: 0x0600DAF7 RID: 56055 RVA: 0x00136992 File Offset: 0x00134B92
			public virtual bool Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700A9A7 RID: 43431
			// (set) Token: 0x0600DAF8 RID: 56056 RVA: 0x001369AA File Offset: 0x00134BAA
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9A8 RID: 43432
			// (set) Token: 0x0600DAF9 RID: 56057 RVA: 0x001369C8 File Offset: 0x00134BC8
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9A9 RID: 43433
			// (set) Token: 0x0600DAFA RID: 56058 RVA: 0x001369DB File Offset: 0x00134BDB
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9AA RID: 43434
			// (set) Token: 0x0600DAFB RID: 56059 RVA: 0x001369F3 File Offset: 0x00134BF3
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9AB RID: 43435
			// (set) Token: 0x0600DAFC RID: 56060 RVA: 0x00136A0B File Offset: 0x00134C0B
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9AC RID: 43436
			// (set) Token: 0x0600DAFD RID: 56061 RVA: 0x00136A23 File Offset: 0x00134C23
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9AD RID: 43437
			// (set) Token: 0x0600DAFE RID: 56062 RVA: 0x00136A3B File Offset: 0x00134C3B
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000E85 RID: 3717
		public class IdentityParameters : ParametersBase
		{
			// Token: 0x1700A9AE RID: 43438
			// (set) Token: 0x0600DB00 RID: 56064 RVA: 0x00136A5B File Offset: 0x00134C5B
			public virtual string Mailbox
			{
				set
				{
					base.PowerSharpParameters["Mailbox"] = ((value != null) ? new MailboxIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9AF RID: 43439
			// (set) Token: 0x0600DB01 RID: 56065 RVA: 0x00136A79 File Offset: 0x00134C79
			public virtual string Identity
			{
				set
				{
					base.PowerSharpParameters["Identity"] = ((value != null) ? new AggregationSubscriptionIdParameter(value) : null);
				}
			}

			// Token: 0x1700A9B0 RID: 43440
			// (set) Token: 0x0600DB02 RID: 56066 RVA: 0x00136A97 File Offset: 0x00134C97
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700A9B1 RID: 43441
			// (set) Token: 0x0600DB03 RID: 56067 RVA: 0x00136AAA File Offset: 0x00134CAA
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700A9B2 RID: 43442
			// (set) Token: 0x0600DB04 RID: 56068 RVA: 0x00136AC2 File Offset: 0x00134CC2
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700A9B3 RID: 43443
			// (set) Token: 0x0600DB05 RID: 56069 RVA: 0x00136ADA File Offset: 0x00134CDA
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700A9B4 RID: 43444
			// (set) Token: 0x0600DB06 RID: 56070 RVA: 0x00136AF2 File Offset: 0x00134CF2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700A9B5 RID: 43445
			// (set) Token: 0x0600DB07 RID: 56071 RVA: 0x00136B0A File Offset: 0x00134D0A
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
