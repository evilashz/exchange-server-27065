using System;
using System.Management.Automation;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000EA7 RID: 3751
	public class NewPushNotificationAppCommand : SyntheticCommandWithPipelineInput<PushNotificationApp, PushNotificationApp>
	{
		// Token: 0x0600DC2A RID: 56362 RVA: 0x001382B0 File Offset: 0x001364B0
		private NewPushNotificationAppCommand() : base("New-PushNotificationApp")
		{
		}

		// Token: 0x0600DC2B RID: 56363 RVA: 0x001382BD File Offset: 0x001364BD
		public NewPushNotificationAppCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x0600DC2C RID: 56364 RVA: 0x001382CC File Offset: 0x001364CC
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC2D RID: 56365 RVA: 0x001382D6 File Offset: 0x001364D6
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.ApnsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC2E RID: 56366 RVA: 0x001382E0 File Offset: 0x001364E0
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.WnsParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC2F RID: 56367 RVA: 0x001382EA File Offset: 0x001364EA
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.AzureSendParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC30 RID: 56368 RVA: 0x001382F4 File Offset: 0x001364F4
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.GcmParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC31 RID: 56369 RVA: 0x001382FE File Offset: 0x001364FE
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.AzureHubCreationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC32 RID: 56370 RVA: 0x00138308 File Offset: 0x00136508
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.PendingGetParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC33 RID: 56371 RVA: 0x00138312 File Offset: 0x00136512
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.WebAppParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC34 RID: 56372 RVA: 0x0013831C File Offset: 0x0013651C
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.ProxyParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC35 RID: 56373 RVA: 0x00138326 File Offset: 0x00136526
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.AzureChallengeRequestParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x0600DC36 RID: 56374 RVA: 0x00138330 File Offset: 0x00136530
		public virtual NewPushNotificationAppCommand SetParameters(NewPushNotificationAppCommand.AzureDeviceRegistrationParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000EA8 RID: 3752
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700AA95 RID: 43669
			// (set) Token: 0x0600DC37 RID: 56375 RVA: 0x0013833A File Offset: 0x0013653A
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AA96 RID: 43670
			// (set) Token: 0x0600DC38 RID: 56376 RVA: 0x0013834D File Offset: 0x0013654D
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AA97 RID: 43671
			// (set) Token: 0x0600DC39 RID: 56377 RVA: 0x00138365 File Offset: 0x00136565
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AA98 RID: 43672
			// (set) Token: 0x0600DC3A RID: 56378 RVA: 0x00138378 File Offset: 0x00136578
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AA99 RID: 43673
			// (set) Token: 0x0600DC3B RID: 56379 RVA: 0x0013838B File Offset: 0x0013658B
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AA9A RID: 43674
			// (set) Token: 0x0600DC3C RID: 56380 RVA: 0x0013839E File Offset: 0x0013659E
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AA9B RID: 43675
			// (set) Token: 0x0600DC3D RID: 56381 RVA: 0x001383B1 File Offset: 0x001365B1
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AA9C RID: 43676
			// (set) Token: 0x0600DC3E RID: 56382 RVA: 0x001383C9 File Offset: 0x001365C9
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AA9D RID: 43677
			// (set) Token: 0x0600DC3F RID: 56383 RVA: 0x001383E1 File Offset: 0x001365E1
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AA9E RID: 43678
			// (set) Token: 0x0600DC40 RID: 56384 RVA: 0x001383F9 File Offset: 0x001365F9
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AA9F RID: 43679
			// (set) Token: 0x0600DC41 RID: 56385 RVA: 0x00138411 File Offset: 0x00136611
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EA9 RID: 3753
		public class ApnsParameters : ParametersBase
		{
			// Token: 0x1700AAA0 RID: 43680
			// (set) Token: 0x0600DC43 RID: 56387 RVA: 0x00138431 File Offset: 0x00136631
			public virtual SwitchParameter AsApns
			{
				set
				{
					base.PowerSharpParameters["AsApns"] = value;
				}
			}

			// Token: 0x1700AAA1 RID: 43681
			// (set) Token: 0x0600DC44 RID: 56388 RVA: 0x00138449 File Offset: 0x00136649
			public virtual string CertificateThumbprint
			{
				set
				{
					base.PowerSharpParameters["CertificateThumbprint"] = value;
				}
			}

			// Token: 0x1700AAA2 RID: 43682
			// (set) Token: 0x0600DC45 RID: 56389 RVA: 0x0013845C File Offset: 0x0013665C
			public virtual string CertificateThumbprintFallback
			{
				set
				{
					base.PowerSharpParameters["CertificateThumbprintFallback"] = value;
				}
			}

			// Token: 0x1700AAA3 RID: 43683
			// (set) Token: 0x0600DC46 RID: 56390 RVA: 0x0013846F File Offset: 0x0013666F
			public virtual string ApnsHost
			{
				set
				{
					base.PowerSharpParameters["ApnsHost"] = value;
				}
			}

			// Token: 0x1700AAA4 RID: 43684
			// (set) Token: 0x0600DC47 RID: 56391 RVA: 0x00138482 File Offset: 0x00136682
			public virtual int? ApnsPort
			{
				set
				{
					base.PowerSharpParameters["ApnsPort"] = value;
				}
			}

			// Token: 0x1700AAA5 RID: 43685
			// (set) Token: 0x0600DC48 RID: 56392 RVA: 0x0013849A File Offset: 0x0013669A
			public virtual string FeedbackHost
			{
				set
				{
					base.PowerSharpParameters["FeedbackHost"] = value;
				}
			}

			// Token: 0x1700AAA6 RID: 43686
			// (set) Token: 0x0600DC49 RID: 56393 RVA: 0x001384AD File Offset: 0x001366AD
			public virtual int? FeedbackPort
			{
				set
				{
					base.PowerSharpParameters["FeedbackPort"] = value;
				}
			}

			// Token: 0x1700AAA7 RID: 43687
			// (set) Token: 0x0600DC4A RID: 56394 RVA: 0x001384C5 File Offset: 0x001366C5
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAA8 RID: 43688
			// (set) Token: 0x0600DC4B RID: 56395 RVA: 0x001384D8 File Offset: 0x001366D8
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AAA9 RID: 43689
			// (set) Token: 0x0600DC4C RID: 56396 RVA: 0x001384F0 File Offset: 0x001366F0
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AAAA RID: 43690
			// (set) Token: 0x0600DC4D RID: 56397 RVA: 0x00138503 File Offset: 0x00136703
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AAAB RID: 43691
			// (set) Token: 0x0600DC4E RID: 56398 RVA: 0x00138516 File Offset: 0x00136716
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AAAC RID: 43692
			// (set) Token: 0x0600DC4F RID: 56399 RVA: 0x00138529 File Offset: 0x00136729
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AAAD RID: 43693
			// (set) Token: 0x0600DC50 RID: 56400 RVA: 0x0013853C File Offset: 0x0013673C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AAAE RID: 43694
			// (set) Token: 0x0600DC51 RID: 56401 RVA: 0x00138554 File Offset: 0x00136754
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AAAF RID: 43695
			// (set) Token: 0x0600DC52 RID: 56402 RVA: 0x0013856C File Offset: 0x0013676C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAB0 RID: 43696
			// (set) Token: 0x0600DC53 RID: 56403 RVA: 0x00138584 File Offset: 0x00136784
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AAB1 RID: 43697
			// (set) Token: 0x0600DC54 RID: 56404 RVA: 0x0013859C File Offset: 0x0013679C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAA RID: 3754
		public class WnsParameters : ParametersBase
		{
			// Token: 0x1700AAB2 RID: 43698
			// (set) Token: 0x0600DC56 RID: 56406 RVA: 0x001385BC File Offset: 0x001367BC
			public virtual SwitchParameter AsWns
			{
				set
				{
					base.PowerSharpParameters["AsWns"] = value;
				}
			}

			// Token: 0x1700AAB3 RID: 43699
			// (set) Token: 0x0600DC57 RID: 56407 RVA: 0x001385D4 File Offset: 0x001367D4
			public virtual string AppSid
			{
				set
				{
					base.PowerSharpParameters["AppSid"] = value;
				}
			}

			// Token: 0x1700AAB4 RID: 43700
			// (set) Token: 0x0600DC58 RID: 56408 RVA: 0x001385E7 File Offset: 0x001367E7
			public virtual string AppSecret
			{
				set
				{
					base.PowerSharpParameters["AppSecret"] = value;
				}
			}

			// Token: 0x1700AAB5 RID: 43701
			// (set) Token: 0x0600DC59 RID: 56409 RVA: 0x001385FA File Offset: 0x001367FA
			public virtual string AuthenticationUri
			{
				set
				{
					base.PowerSharpParameters["AuthenticationUri"] = value;
				}
			}

			// Token: 0x1700AAB6 RID: 43702
			// (set) Token: 0x0600DC5A RID: 56410 RVA: 0x0013860D File Offset: 0x0013680D
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AAB7 RID: 43703
			// (set) Token: 0x0600DC5B RID: 56411 RVA: 0x00138625 File Offset: 0x00136825
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAB8 RID: 43704
			// (set) Token: 0x0600DC5C RID: 56412 RVA: 0x00138638 File Offset: 0x00136838
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AAB9 RID: 43705
			// (set) Token: 0x0600DC5D RID: 56413 RVA: 0x00138650 File Offset: 0x00136850
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AABA RID: 43706
			// (set) Token: 0x0600DC5E RID: 56414 RVA: 0x00138663 File Offset: 0x00136863
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AABB RID: 43707
			// (set) Token: 0x0600DC5F RID: 56415 RVA: 0x00138676 File Offset: 0x00136876
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AABC RID: 43708
			// (set) Token: 0x0600DC60 RID: 56416 RVA: 0x00138689 File Offset: 0x00136889
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AABD RID: 43709
			// (set) Token: 0x0600DC61 RID: 56417 RVA: 0x0013869C File Offset: 0x0013689C
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AABE RID: 43710
			// (set) Token: 0x0600DC62 RID: 56418 RVA: 0x001386B4 File Offset: 0x001368B4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AABF RID: 43711
			// (set) Token: 0x0600DC63 RID: 56419 RVA: 0x001386CC File Offset: 0x001368CC
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAC0 RID: 43712
			// (set) Token: 0x0600DC64 RID: 56420 RVA: 0x001386E4 File Offset: 0x001368E4
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AAC1 RID: 43713
			// (set) Token: 0x0600DC65 RID: 56421 RVA: 0x001386FC File Offset: 0x001368FC
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAB RID: 3755
		public class AzureSendParameters : ParametersBase
		{
			// Token: 0x1700AAC2 RID: 43714
			// (set) Token: 0x0600DC67 RID: 56423 RVA: 0x0013871C File Offset: 0x0013691C
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AAC3 RID: 43715
			// (set) Token: 0x0600DC68 RID: 56424 RVA: 0x00138734 File Offset: 0x00136934
			public virtual SwitchParameter AsAzureSend
			{
				set
				{
					base.PowerSharpParameters["AsAzureSend"] = value;
				}
			}

			// Token: 0x1700AAC4 RID: 43716
			// (set) Token: 0x0600DC69 RID: 56425 RVA: 0x0013874C File Offset: 0x0013694C
			public virtual string SasKeyName
			{
				set
				{
					base.PowerSharpParameters["SasKeyName"] = value;
				}
			}

			// Token: 0x1700AAC5 RID: 43717
			// (set) Token: 0x0600DC6A RID: 56426 RVA: 0x0013875F File Offset: 0x0013695F
			public virtual string SasKeyValue
			{
				set
				{
					base.PowerSharpParameters["SasKeyValue"] = value;
				}
			}

			// Token: 0x1700AAC6 RID: 43718
			// (set) Token: 0x0600DC6B RID: 56427 RVA: 0x00138772 File Offset: 0x00136972
			public virtual string UriTemplate
			{
				set
				{
					base.PowerSharpParameters["UriTemplate"] = value;
				}
			}

			// Token: 0x1700AAC7 RID: 43719
			// (set) Token: 0x0600DC6C RID: 56428 RVA: 0x00138785 File Offset: 0x00136985
			public virtual string RegistrationTemplate
			{
				set
				{
					base.PowerSharpParameters["RegistrationTemplate"] = value;
				}
			}

			// Token: 0x1700AAC8 RID: 43720
			// (set) Token: 0x0600DC6D RID: 56429 RVA: 0x00138798 File Offset: 0x00136998
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAC9 RID: 43721
			// (set) Token: 0x0600DC6E RID: 56430 RVA: 0x001387AB File Offset: 0x001369AB
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AACA RID: 43722
			// (set) Token: 0x0600DC6F RID: 56431 RVA: 0x001387C3 File Offset: 0x001369C3
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AACB RID: 43723
			// (set) Token: 0x0600DC70 RID: 56432 RVA: 0x001387D6 File Offset: 0x001369D6
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AACC RID: 43724
			// (set) Token: 0x0600DC71 RID: 56433 RVA: 0x001387E9 File Offset: 0x001369E9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AACD RID: 43725
			// (set) Token: 0x0600DC72 RID: 56434 RVA: 0x001387FC File Offset: 0x001369FC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AACE RID: 43726
			// (set) Token: 0x0600DC73 RID: 56435 RVA: 0x0013880F File Offset: 0x00136A0F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AACF RID: 43727
			// (set) Token: 0x0600DC74 RID: 56436 RVA: 0x00138827 File Offset: 0x00136A27
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AAD0 RID: 43728
			// (set) Token: 0x0600DC75 RID: 56437 RVA: 0x0013883F File Offset: 0x00136A3F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAD1 RID: 43729
			// (set) Token: 0x0600DC76 RID: 56438 RVA: 0x00138857 File Offset: 0x00136A57
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AAD2 RID: 43730
			// (set) Token: 0x0600DC77 RID: 56439 RVA: 0x0013886F File Offset: 0x00136A6F
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAC RID: 3756
		public class GcmParameters : ParametersBase
		{
			// Token: 0x1700AAD3 RID: 43731
			// (set) Token: 0x0600DC79 RID: 56441 RVA: 0x0013888F File Offset: 0x00136A8F
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AAD4 RID: 43732
			// (set) Token: 0x0600DC7A RID: 56442 RVA: 0x001388A7 File Offset: 0x00136AA7
			public virtual SwitchParameter AsGcm
			{
				set
				{
					base.PowerSharpParameters["AsGcm"] = value;
				}
			}

			// Token: 0x1700AAD5 RID: 43733
			// (set) Token: 0x0600DC7B RID: 56443 RVA: 0x001388BF File Offset: 0x00136ABF
			public virtual string Sender
			{
				set
				{
					base.PowerSharpParameters["Sender"] = value;
				}
			}

			// Token: 0x1700AAD6 RID: 43734
			// (set) Token: 0x0600DC7C RID: 56444 RVA: 0x001388D2 File Offset: 0x00136AD2
			public virtual string SenderAuthToken
			{
				set
				{
					base.PowerSharpParameters["SenderAuthToken"] = value;
				}
			}

			// Token: 0x1700AAD7 RID: 43735
			// (set) Token: 0x0600DC7D RID: 56445 RVA: 0x001388E5 File Offset: 0x00136AE5
			public virtual string GcmServiceUri
			{
				set
				{
					base.PowerSharpParameters["GcmServiceUri"] = value;
				}
			}

			// Token: 0x1700AAD8 RID: 43736
			// (set) Token: 0x0600DC7E RID: 56446 RVA: 0x001388F8 File Offset: 0x00136AF8
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAD9 RID: 43737
			// (set) Token: 0x0600DC7F RID: 56447 RVA: 0x0013890B File Offset: 0x00136B0B
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AADA RID: 43738
			// (set) Token: 0x0600DC80 RID: 56448 RVA: 0x00138923 File Offset: 0x00136B23
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AADB RID: 43739
			// (set) Token: 0x0600DC81 RID: 56449 RVA: 0x00138936 File Offset: 0x00136B36
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AADC RID: 43740
			// (set) Token: 0x0600DC82 RID: 56450 RVA: 0x00138949 File Offset: 0x00136B49
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AADD RID: 43741
			// (set) Token: 0x0600DC83 RID: 56451 RVA: 0x0013895C File Offset: 0x00136B5C
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AADE RID: 43742
			// (set) Token: 0x0600DC84 RID: 56452 RVA: 0x0013896F File Offset: 0x00136B6F
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AADF RID: 43743
			// (set) Token: 0x0600DC85 RID: 56453 RVA: 0x00138987 File Offset: 0x00136B87
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AAE0 RID: 43744
			// (set) Token: 0x0600DC86 RID: 56454 RVA: 0x0013899F File Offset: 0x00136B9F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAE1 RID: 43745
			// (set) Token: 0x0600DC87 RID: 56455 RVA: 0x001389B7 File Offset: 0x00136BB7
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AAE2 RID: 43746
			// (set) Token: 0x0600DC88 RID: 56456 RVA: 0x001389CF File Offset: 0x00136BCF
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAD RID: 3757
		public class AzureHubCreationParameters : ParametersBase
		{
			// Token: 0x1700AAE3 RID: 43747
			// (set) Token: 0x0600DC8A RID: 56458 RVA: 0x001389EF File Offset: 0x00136BEF
			public virtual SwitchParameter UseClearTextAuthenticationKeys
			{
				set
				{
					base.PowerSharpParameters["UseClearTextAuthenticationKeys"] = value;
				}
			}

			// Token: 0x1700AAE4 RID: 43748
			// (set) Token: 0x0600DC8B RID: 56459 RVA: 0x00138A07 File Offset: 0x00136C07
			public virtual string UriTemplate
			{
				set
				{
					base.PowerSharpParameters["UriTemplate"] = value;
				}
			}

			// Token: 0x1700AAE5 RID: 43749
			// (set) Token: 0x0600DC8C RID: 56460 RVA: 0x00138A1A File Offset: 0x00136C1A
			public virtual SwitchParameter AsAzureHubCreation
			{
				set
				{
					base.PowerSharpParameters["AsAzureHubCreation"] = value;
				}
			}

			// Token: 0x1700AAE6 RID: 43750
			// (set) Token: 0x0600DC8D RID: 56461 RVA: 0x00138A32 File Offset: 0x00136C32
			public virtual string AcsUserName
			{
				set
				{
					base.PowerSharpParameters["AcsUserName"] = value;
				}
			}

			// Token: 0x1700AAE7 RID: 43751
			// (set) Token: 0x0600DC8E RID: 56462 RVA: 0x00138A45 File Offset: 0x00136C45
			public virtual string AcsUserPassword
			{
				set
				{
					base.PowerSharpParameters["AcsUserPassword"] = value;
				}
			}

			// Token: 0x1700AAE8 RID: 43752
			// (set) Token: 0x0600DC8F RID: 56463 RVA: 0x00138A58 File Offset: 0x00136C58
			public virtual string AcsUriTemplate
			{
				set
				{
					base.PowerSharpParameters["AcsUriTemplate"] = value;
				}
			}

			// Token: 0x1700AAE9 RID: 43753
			// (set) Token: 0x0600DC90 RID: 56464 RVA: 0x00138A6B File Offset: 0x00136C6B
			public virtual string AcsScopeUriTemplate
			{
				set
				{
					base.PowerSharpParameters["AcsScopeUriTemplate"] = value;
				}
			}

			// Token: 0x1700AAEA RID: 43754
			// (set) Token: 0x0600DC91 RID: 56465 RVA: 0x00138A7E File Offset: 0x00136C7E
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAEB RID: 43755
			// (set) Token: 0x0600DC92 RID: 56466 RVA: 0x00138A91 File Offset: 0x00136C91
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AAEC RID: 43756
			// (set) Token: 0x0600DC93 RID: 56467 RVA: 0x00138AA9 File Offset: 0x00136CA9
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AAED RID: 43757
			// (set) Token: 0x0600DC94 RID: 56468 RVA: 0x00138ABC File Offset: 0x00136CBC
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AAEE RID: 43758
			// (set) Token: 0x0600DC95 RID: 56469 RVA: 0x00138ACF File Offset: 0x00136CCF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AAEF RID: 43759
			// (set) Token: 0x0600DC96 RID: 56470 RVA: 0x00138AE2 File Offset: 0x00136CE2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AAF0 RID: 43760
			// (set) Token: 0x0600DC97 RID: 56471 RVA: 0x00138AF5 File Offset: 0x00136CF5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AAF1 RID: 43761
			// (set) Token: 0x0600DC98 RID: 56472 RVA: 0x00138B0D File Offset: 0x00136D0D
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AAF2 RID: 43762
			// (set) Token: 0x0600DC99 RID: 56473 RVA: 0x00138B25 File Offset: 0x00136D25
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAF3 RID: 43763
			// (set) Token: 0x0600DC9A RID: 56474 RVA: 0x00138B3D File Offset: 0x00136D3D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AAF4 RID: 43764
			// (set) Token: 0x0600DC9B RID: 56475 RVA: 0x00138B55 File Offset: 0x00136D55
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAE RID: 3758
		public class PendingGetParameters : ParametersBase
		{
			// Token: 0x1700AAF5 RID: 43765
			// (set) Token: 0x0600DC9D RID: 56477 RVA: 0x00138B75 File Offset: 0x00136D75
			public virtual SwitchParameter AsPendingGet
			{
				set
				{
					base.PowerSharpParameters["AsPendingGet"] = value;
				}
			}

			// Token: 0x1700AAF6 RID: 43766
			// (set) Token: 0x0600DC9E RID: 56478 RVA: 0x00138B8D File Offset: 0x00136D8D
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AAF7 RID: 43767
			// (set) Token: 0x0600DC9F RID: 56479 RVA: 0x00138BA0 File Offset: 0x00136DA0
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AAF8 RID: 43768
			// (set) Token: 0x0600DCA0 RID: 56480 RVA: 0x00138BB8 File Offset: 0x00136DB8
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AAF9 RID: 43769
			// (set) Token: 0x0600DCA1 RID: 56481 RVA: 0x00138BCB File Offset: 0x00136DCB
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AAFA RID: 43770
			// (set) Token: 0x0600DCA2 RID: 56482 RVA: 0x00138BDE File Offset: 0x00136DDE
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AAFB RID: 43771
			// (set) Token: 0x0600DCA3 RID: 56483 RVA: 0x00138BF1 File Offset: 0x00136DF1
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AAFC RID: 43772
			// (set) Token: 0x0600DCA4 RID: 56484 RVA: 0x00138C04 File Offset: 0x00136E04
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AAFD RID: 43773
			// (set) Token: 0x0600DCA5 RID: 56485 RVA: 0x00138C1C File Offset: 0x00136E1C
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AAFE RID: 43774
			// (set) Token: 0x0600DCA6 RID: 56486 RVA: 0x00138C34 File Offset: 0x00136E34
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AAFF RID: 43775
			// (set) Token: 0x0600DCA7 RID: 56487 RVA: 0x00138C4C File Offset: 0x00136E4C
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB00 RID: 43776
			// (set) Token: 0x0600DCA8 RID: 56488 RVA: 0x00138C64 File Offset: 0x00136E64
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EAF RID: 3759
		public class WebAppParameters : ParametersBase
		{
			// Token: 0x1700AB01 RID: 43777
			// (set) Token: 0x0600DCAA RID: 56490 RVA: 0x00138C84 File Offset: 0x00136E84
			public virtual SwitchParameter AsWebApp
			{
				set
				{
					base.PowerSharpParameters["AsWebApp"] = value;
				}
			}

			// Token: 0x1700AB02 RID: 43778
			// (set) Token: 0x0600DCAB RID: 56491 RVA: 0x00138C9C File Offset: 0x00136E9C
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB03 RID: 43779
			// (set) Token: 0x0600DCAC RID: 56492 RVA: 0x00138CAF File Offset: 0x00136EAF
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB04 RID: 43780
			// (set) Token: 0x0600DCAD RID: 56493 RVA: 0x00138CC7 File Offset: 0x00136EC7
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB05 RID: 43781
			// (set) Token: 0x0600DCAE RID: 56494 RVA: 0x00138CDA File Offset: 0x00136EDA
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB06 RID: 43782
			// (set) Token: 0x0600DCAF RID: 56495 RVA: 0x00138CED File Offset: 0x00136EED
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AB07 RID: 43783
			// (set) Token: 0x0600DCB0 RID: 56496 RVA: 0x00138D00 File Offset: 0x00136F00
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB08 RID: 43784
			// (set) Token: 0x0600DCB1 RID: 56497 RVA: 0x00138D13 File Offset: 0x00136F13
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB09 RID: 43785
			// (set) Token: 0x0600DCB2 RID: 56498 RVA: 0x00138D2B File Offset: 0x00136F2B
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB0A RID: 43786
			// (set) Token: 0x0600DCB3 RID: 56499 RVA: 0x00138D43 File Offset: 0x00136F43
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB0B RID: 43787
			// (set) Token: 0x0600DCB4 RID: 56500 RVA: 0x00138D5B File Offset: 0x00136F5B
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB0C RID: 43788
			// (set) Token: 0x0600DCB5 RID: 56501 RVA: 0x00138D73 File Offset: 0x00136F73
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EB0 RID: 3760
		public class ProxyParameters : ParametersBase
		{
			// Token: 0x1700AB0D RID: 43789
			// (set) Token: 0x0600DCB7 RID: 56503 RVA: 0x00138D93 File Offset: 0x00136F93
			public virtual SwitchParameter AsProxy
			{
				set
				{
					base.PowerSharpParameters["AsProxy"] = value;
				}
			}

			// Token: 0x1700AB0E RID: 43790
			// (set) Token: 0x0600DCB8 RID: 56504 RVA: 0x00138DAB File Offset: 0x00136FAB
			public virtual string Uri
			{
				set
				{
					base.PowerSharpParameters["Uri"] = value;
				}
			}

			// Token: 0x1700AB0F RID: 43791
			// (set) Token: 0x0600DCB9 RID: 56505 RVA: 0x00138DBE File Offset: 0x00136FBE
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = value;
				}
			}

			// Token: 0x1700AB10 RID: 43792
			// (set) Token: 0x0600DCBA RID: 56506 RVA: 0x00138DD1 File Offset: 0x00136FD1
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB11 RID: 43793
			// (set) Token: 0x0600DCBB RID: 56507 RVA: 0x00138DE4 File Offset: 0x00136FE4
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB12 RID: 43794
			// (set) Token: 0x0600DCBC RID: 56508 RVA: 0x00138DFC File Offset: 0x00136FFC
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB13 RID: 43795
			// (set) Token: 0x0600DCBD RID: 56509 RVA: 0x00138E0F File Offset: 0x0013700F
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB14 RID: 43796
			// (set) Token: 0x0600DCBE RID: 56510 RVA: 0x00138E22 File Offset: 0x00137022
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AB15 RID: 43797
			// (set) Token: 0x0600DCBF RID: 56511 RVA: 0x00138E35 File Offset: 0x00137035
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB16 RID: 43798
			// (set) Token: 0x0600DCC0 RID: 56512 RVA: 0x00138E48 File Offset: 0x00137048
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB17 RID: 43799
			// (set) Token: 0x0600DCC1 RID: 56513 RVA: 0x00138E60 File Offset: 0x00137060
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB18 RID: 43800
			// (set) Token: 0x0600DCC2 RID: 56514 RVA: 0x00138E78 File Offset: 0x00137078
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB19 RID: 43801
			// (set) Token: 0x0600DCC3 RID: 56515 RVA: 0x00138E90 File Offset: 0x00137090
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB1A RID: 43802
			// (set) Token: 0x0600DCC4 RID: 56516 RVA: 0x00138EA8 File Offset: 0x001370A8
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EB1 RID: 3761
		public class AzureChallengeRequestParameters : ParametersBase
		{
			// Token: 0x1700AB1B RID: 43803
			// (set) Token: 0x0600DCC6 RID: 56518 RVA: 0x00138EC8 File Offset: 0x001370C8
			public virtual SwitchParameter AsAzureChallengeRequest
			{
				set
				{
					base.PowerSharpParameters["AsAzureChallengeRequest"] = value;
				}
			}

			// Token: 0x1700AB1C RID: 43804
			// (set) Token: 0x0600DCC7 RID: 56519 RVA: 0x00138EE0 File Offset: 0x001370E0
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB1D RID: 43805
			// (set) Token: 0x0600DCC8 RID: 56520 RVA: 0x00138EF3 File Offset: 0x001370F3
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB1E RID: 43806
			// (set) Token: 0x0600DCC9 RID: 56521 RVA: 0x00138F0B File Offset: 0x0013710B
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB1F RID: 43807
			// (set) Token: 0x0600DCCA RID: 56522 RVA: 0x00138F1E File Offset: 0x0013711E
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB20 RID: 43808
			// (set) Token: 0x0600DCCB RID: 56523 RVA: 0x00138F31 File Offset: 0x00137131
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AB21 RID: 43809
			// (set) Token: 0x0600DCCC RID: 56524 RVA: 0x00138F44 File Offset: 0x00137144
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB22 RID: 43810
			// (set) Token: 0x0600DCCD RID: 56525 RVA: 0x00138F57 File Offset: 0x00137157
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB23 RID: 43811
			// (set) Token: 0x0600DCCE RID: 56526 RVA: 0x00138F6F File Offset: 0x0013716F
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB24 RID: 43812
			// (set) Token: 0x0600DCCF RID: 56527 RVA: 0x00138F87 File Offset: 0x00137187
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB25 RID: 43813
			// (set) Token: 0x0600DCD0 RID: 56528 RVA: 0x00138F9F File Offset: 0x0013719F
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB26 RID: 43814
			// (set) Token: 0x0600DCD1 RID: 56529 RVA: 0x00138FB7 File Offset: 0x001371B7
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000EB2 RID: 3762
		public class AzureDeviceRegistrationParameters : ParametersBase
		{
			// Token: 0x1700AB27 RID: 43815
			// (set) Token: 0x0600DCD3 RID: 56531 RVA: 0x00138FD7 File Offset: 0x001371D7
			public virtual SwitchParameter AsAzureDeviceRegistration
			{
				set
				{
					base.PowerSharpParameters["AsAzureDeviceRegistration"] = value;
				}
			}

			// Token: 0x1700AB28 RID: 43816
			// (set) Token: 0x0600DCD4 RID: 56532 RVA: 0x00138FEF File Offset: 0x001371EF
			public virtual string DisplayName
			{
				set
				{
					base.PowerSharpParameters["DisplayName"] = value;
				}
			}

			// Token: 0x1700AB29 RID: 43817
			// (set) Token: 0x0600DCD5 RID: 56533 RVA: 0x00139002 File Offset: 0x00137202
			public virtual bool? Enabled
			{
				set
				{
					base.PowerSharpParameters["Enabled"] = value;
				}
			}

			// Token: 0x1700AB2A RID: 43818
			// (set) Token: 0x0600DCD6 RID: 56534 RVA: 0x0013901A File Offset: 0x0013721A
			public virtual Version ExchangeMinimumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMinimumVersion"] = value;
				}
			}

			// Token: 0x1700AB2B RID: 43819
			// (set) Token: 0x0600DCD7 RID: 56535 RVA: 0x0013902D File Offset: 0x0013722D
			public virtual Version ExchangeMaximumVersion
			{
				set
				{
					base.PowerSharpParameters["ExchangeMaximumVersion"] = value;
				}
			}

			// Token: 0x1700AB2C RID: 43820
			// (set) Token: 0x0600DCD8 RID: 56536 RVA: 0x00139040 File Offset: 0x00137240
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700AB2D RID: 43821
			// (set) Token: 0x0600DCD9 RID: 56537 RVA: 0x00139053 File Offset: 0x00137253
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700AB2E RID: 43822
			// (set) Token: 0x0600DCDA RID: 56538 RVA: 0x00139066 File Offset: 0x00137266
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700AB2F RID: 43823
			// (set) Token: 0x0600DCDB RID: 56539 RVA: 0x0013907E File Offset: 0x0013727E
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700AB30 RID: 43824
			// (set) Token: 0x0600DCDC RID: 56540 RVA: 0x00139096 File Offset: 0x00137296
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700AB31 RID: 43825
			// (set) Token: 0x0600DCDD RID: 56541 RVA: 0x001390AE File Offset: 0x001372AE
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700AB32 RID: 43826
			// (set) Token: 0x0600DCDE RID: 56542 RVA: 0x001390C6 File Offset: 0x001372C6
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
