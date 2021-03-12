using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.PowerSharp.Management
{
	// Token: 0x02000840 RID: 2112
	public class NewEmailAddressPolicyCommand : SyntheticCommandWithPipelineInput<EmailAddressPolicy, EmailAddressPolicy>
	{
		// Token: 0x0600690E RID: 26894 RVA: 0x0009FCBB File Offset: 0x0009DEBB
		private NewEmailAddressPolicyCommand() : base("New-EmailAddressPolicy")
		{
		}

		// Token: 0x0600690F RID: 26895 RVA: 0x0009FCC8 File Offset: 0x0009DEC8
		public NewEmailAddressPolicyCommand(ExchangeManagementSession session) : this()
		{
			base.Session = session;
		}

		// Token: 0x06006910 RID: 26896 RVA: 0x0009FCD7 File Offset: 0x0009DED7
		public virtual NewEmailAddressPolicyCommand SetParameters(NewEmailAddressPolicyCommand.SMTPTemplateWithCustomFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006911 RID: 26897 RVA: 0x0009FCE1 File Offset: 0x0009DEE1
		public virtual NewEmailAddressPolicyCommand SetParameters(NewEmailAddressPolicyCommand.AllTemplatesWithCustomFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006912 RID: 26898 RVA: 0x0009FCEB File Offset: 0x0009DEEB
		public virtual NewEmailAddressPolicyCommand SetParameters(NewEmailAddressPolicyCommand.AllTemplatesWithPrecannedFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006913 RID: 26899 RVA: 0x0009FCF5 File Offset: 0x0009DEF5
		public virtual NewEmailAddressPolicyCommand SetParameters(NewEmailAddressPolicyCommand.SMTPTemplateWithPrecannedFilterParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x06006914 RID: 26900 RVA: 0x0009FCFF File Offset: 0x0009DEFF
		public virtual NewEmailAddressPolicyCommand SetParameters(NewEmailAddressPolicyCommand.DefaultParameters parameters)
		{
			base.SetParameters(parameters);
			return this;
		}

		// Token: 0x02000841 RID: 2113
		public class SMTPTemplateWithCustomFilterParameters : ParametersBase
		{
			// Token: 0x17004447 RID: 17479
			// (set) Token: 0x06006915 RID: 26901 RVA: 0x0009FD09 File Offset: 0x0009DF09
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17004448 RID: 17480
			// (set) Token: 0x06006916 RID: 26902 RVA: 0x0009FD1C File Offset: 0x0009DF1C
			public virtual string EnabledPrimarySMTPAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["EnabledPrimarySMTPAddressTemplate"] = value;
				}
			}

			// Token: 0x17004449 RID: 17481
			// (set) Token: 0x06006917 RID: 26903 RVA: 0x0009FD2F File Offset: 0x0009DF2F
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700444A RID: 17482
			// (set) Token: 0x06006918 RID: 26904 RVA: 0x0009FD4D File Offset: 0x0009DF4D
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700444B RID: 17483
			// (set) Token: 0x06006919 RID: 26905 RVA: 0x0009FD65 File Offset: 0x0009DF65
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x1700444C RID: 17484
			// (set) Token: 0x0600691A RID: 26906 RVA: 0x0009FD83 File Offset: 0x0009DF83
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700444D RID: 17485
			// (set) Token: 0x0600691B RID: 26907 RVA: 0x0009FD96 File Offset: 0x0009DF96
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700444E RID: 17486
			// (set) Token: 0x0600691C RID: 26908 RVA: 0x0009FDA9 File Offset: 0x0009DFA9
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700444F RID: 17487
			// (set) Token: 0x0600691D RID: 26909 RVA: 0x0009FDC1 File Offset: 0x0009DFC1
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x17004450 RID: 17488
			// (set) Token: 0x0600691E RID: 26910 RVA: 0x0009FDD9 File Offset: 0x0009DFD9
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x17004451 RID: 17489
			// (set) Token: 0x0600691F RID: 26911 RVA: 0x0009FDF1 File Offset: 0x0009DFF1
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x17004452 RID: 17490
			// (set) Token: 0x06006920 RID: 26912 RVA: 0x0009FE09 File Offset: 0x0009E009
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000842 RID: 2114
		public class AllTemplatesWithCustomFilterParameters : ParametersBase
		{
			// Token: 0x17004453 RID: 17491
			// (set) Token: 0x06006922 RID: 26914 RVA: 0x0009FE29 File Offset: 0x0009E029
			public virtual string RecipientFilter
			{
				set
				{
					base.PowerSharpParameters["RecipientFilter"] = value;
				}
			}

			// Token: 0x17004454 RID: 17492
			// (set) Token: 0x06006923 RID: 26915 RVA: 0x0009FE3C File Offset: 0x0009E03C
			public virtual ProxyAddressTemplateCollection EnabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["EnabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x17004455 RID: 17493
			// (set) Token: 0x06006924 RID: 26916 RVA: 0x0009FE4F File Offset: 0x0009E04F
			public virtual ProxyAddressTemplateCollection DisabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["DisabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x17004456 RID: 17494
			// (set) Token: 0x06006925 RID: 26917 RVA: 0x0009FE62 File Offset: 0x0009E062
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17004457 RID: 17495
			// (set) Token: 0x06006926 RID: 26918 RVA: 0x0009FE80 File Offset: 0x0009E080
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004458 RID: 17496
			// (set) Token: 0x06006927 RID: 26919 RVA: 0x0009FE98 File Offset: 0x0009E098
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004459 RID: 17497
			// (set) Token: 0x06006928 RID: 26920 RVA: 0x0009FEB6 File Offset: 0x0009E0B6
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x1700445A RID: 17498
			// (set) Token: 0x06006929 RID: 26921 RVA: 0x0009FEC9 File Offset: 0x0009E0C9
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700445B RID: 17499
			// (set) Token: 0x0600692A RID: 26922 RVA: 0x0009FEDC File Offset: 0x0009E0DC
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700445C RID: 17500
			// (set) Token: 0x0600692B RID: 26923 RVA: 0x0009FEF4 File Offset: 0x0009E0F4
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700445D RID: 17501
			// (set) Token: 0x0600692C RID: 26924 RVA: 0x0009FF0C File Offset: 0x0009E10C
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700445E RID: 17502
			// (set) Token: 0x0600692D RID: 26925 RVA: 0x0009FF24 File Offset: 0x0009E124
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700445F RID: 17503
			// (set) Token: 0x0600692E RID: 26926 RVA: 0x0009FF3C File Offset: 0x0009E13C
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000843 RID: 2115
		public class AllTemplatesWithPrecannedFilterParameters : ParametersBase
		{
			// Token: 0x17004460 RID: 17504
			// (set) Token: 0x06006930 RID: 26928 RVA: 0x0009FF5C File Offset: 0x0009E15C
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17004461 RID: 17505
			// (set) Token: 0x06006931 RID: 26929 RVA: 0x0009FF74 File Offset: 0x0009E174
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17004462 RID: 17506
			// (set) Token: 0x06006932 RID: 26930 RVA: 0x0009FF87 File Offset: 0x0009E187
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17004463 RID: 17507
			// (set) Token: 0x06006933 RID: 26931 RVA: 0x0009FF9A File Offset: 0x0009E19A
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17004464 RID: 17508
			// (set) Token: 0x06006934 RID: 26932 RVA: 0x0009FFAD File Offset: 0x0009E1AD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17004465 RID: 17509
			// (set) Token: 0x06006935 RID: 26933 RVA: 0x0009FFC0 File Offset: 0x0009E1C0
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17004466 RID: 17510
			// (set) Token: 0x06006936 RID: 26934 RVA: 0x0009FFD3 File Offset: 0x0009E1D3
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17004467 RID: 17511
			// (set) Token: 0x06006937 RID: 26935 RVA: 0x0009FFE6 File Offset: 0x0009E1E6
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17004468 RID: 17512
			// (set) Token: 0x06006938 RID: 26936 RVA: 0x0009FFF9 File Offset: 0x0009E1F9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17004469 RID: 17513
			// (set) Token: 0x06006939 RID: 26937 RVA: 0x000A000C File Offset: 0x0009E20C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x1700446A RID: 17514
			// (set) Token: 0x0600693A RID: 26938 RVA: 0x000A001F File Offset: 0x0009E21F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x1700446B RID: 17515
			// (set) Token: 0x0600693B RID: 26939 RVA: 0x000A0032 File Offset: 0x0009E232
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x1700446C RID: 17516
			// (set) Token: 0x0600693C RID: 26940 RVA: 0x000A0045 File Offset: 0x0009E245
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x1700446D RID: 17517
			// (set) Token: 0x0600693D RID: 26941 RVA: 0x000A0058 File Offset: 0x0009E258
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x1700446E RID: 17518
			// (set) Token: 0x0600693E RID: 26942 RVA: 0x000A006B File Offset: 0x0009E26B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x1700446F RID: 17519
			// (set) Token: 0x0600693F RID: 26943 RVA: 0x000A007E File Offset: 0x0009E27E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x17004470 RID: 17520
			// (set) Token: 0x06006940 RID: 26944 RVA: 0x000A0091 File Offset: 0x0009E291
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17004471 RID: 17521
			// (set) Token: 0x06006941 RID: 26945 RVA: 0x000A00A4 File Offset: 0x0009E2A4
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17004472 RID: 17522
			// (set) Token: 0x06006942 RID: 26946 RVA: 0x000A00B7 File Offset: 0x0009E2B7
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17004473 RID: 17523
			// (set) Token: 0x06006943 RID: 26947 RVA: 0x000A00CA File Offset: 0x0009E2CA
			public virtual ProxyAddressTemplateCollection EnabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["EnabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x17004474 RID: 17524
			// (set) Token: 0x06006944 RID: 26948 RVA: 0x000A00DD File Offset: 0x0009E2DD
			public virtual ProxyAddressTemplateCollection DisabledEmailAddressTemplates
			{
				set
				{
					base.PowerSharpParameters["DisabledEmailAddressTemplates"] = value;
				}
			}

			// Token: 0x17004475 RID: 17525
			// (set) Token: 0x06006945 RID: 26949 RVA: 0x000A00F0 File Offset: 0x0009E2F0
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17004476 RID: 17526
			// (set) Token: 0x06006946 RID: 26950 RVA: 0x000A010E File Offset: 0x0009E30E
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004477 RID: 17527
			// (set) Token: 0x06006947 RID: 26951 RVA: 0x000A0126 File Offset: 0x0009E326
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004478 RID: 17528
			// (set) Token: 0x06006948 RID: 26952 RVA: 0x000A0144 File Offset: 0x0009E344
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004479 RID: 17529
			// (set) Token: 0x06006949 RID: 26953 RVA: 0x000A0157 File Offset: 0x0009E357
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x1700447A RID: 17530
			// (set) Token: 0x0600694A RID: 26954 RVA: 0x000A016A File Offset: 0x0009E36A
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x1700447B RID: 17531
			// (set) Token: 0x0600694B RID: 26955 RVA: 0x000A0182 File Offset: 0x0009E382
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700447C RID: 17532
			// (set) Token: 0x0600694C RID: 26956 RVA: 0x000A019A File Offset: 0x0009E39A
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700447D RID: 17533
			// (set) Token: 0x0600694D RID: 26957 RVA: 0x000A01B2 File Offset: 0x0009E3B2
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700447E RID: 17534
			// (set) Token: 0x0600694E RID: 26958 RVA: 0x000A01CA File Offset: 0x0009E3CA
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000844 RID: 2116
		public class SMTPTemplateWithPrecannedFilterParameters : ParametersBase
		{
			// Token: 0x1700447F RID: 17535
			// (set) Token: 0x06006950 RID: 26960 RVA: 0x000A01EA File Offset: 0x0009E3EA
			public virtual WellKnownRecipientType? IncludedRecipients
			{
				set
				{
					base.PowerSharpParameters["IncludedRecipients"] = value;
				}
			}

			// Token: 0x17004480 RID: 17536
			// (set) Token: 0x06006951 RID: 26961 RVA: 0x000A0202 File Offset: 0x0009E402
			public virtual MultiValuedProperty<string> ConditionalDepartment
			{
				set
				{
					base.PowerSharpParameters["ConditionalDepartment"] = value;
				}
			}

			// Token: 0x17004481 RID: 17537
			// (set) Token: 0x06006952 RID: 26962 RVA: 0x000A0215 File Offset: 0x0009E415
			public virtual MultiValuedProperty<string> ConditionalCompany
			{
				set
				{
					base.PowerSharpParameters["ConditionalCompany"] = value;
				}
			}

			// Token: 0x17004482 RID: 17538
			// (set) Token: 0x06006953 RID: 26963 RVA: 0x000A0228 File Offset: 0x0009E428
			public virtual MultiValuedProperty<string> ConditionalStateOrProvince
			{
				set
				{
					base.PowerSharpParameters["ConditionalStateOrProvince"] = value;
				}
			}

			// Token: 0x17004483 RID: 17539
			// (set) Token: 0x06006954 RID: 26964 RVA: 0x000A023B File Offset: 0x0009E43B
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute1
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute1"] = value;
				}
			}

			// Token: 0x17004484 RID: 17540
			// (set) Token: 0x06006955 RID: 26965 RVA: 0x000A024E File Offset: 0x0009E44E
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute2
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute2"] = value;
				}
			}

			// Token: 0x17004485 RID: 17541
			// (set) Token: 0x06006956 RID: 26966 RVA: 0x000A0261 File Offset: 0x0009E461
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute3
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute3"] = value;
				}
			}

			// Token: 0x17004486 RID: 17542
			// (set) Token: 0x06006957 RID: 26967 RVA: 0x000A0274 File Offset: 0x0009E474
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute4
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute4"] = value;
				}
			}

			// Token: 0x17004487 RID: 17543
			// (set) Token: 0x06006958 RID: 26968 RVA: 0x000A0287 File Offset: 0x0009E487
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute5
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute5"] = value;
				}
			}

			// Token: 0x17004488 RID: 17544
			// (set) Token: 0x06006959 RID: 26969 RVA: 0x000A029A File Offset: 0x0009E49A
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute6
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute6"] = value;
				}
			}

			// Token: 0x17004489 RID: 17545
			// (set) Token: 0x0600695A RID: 26970 RVA: 0x000A02AD File Offset: 0x0009E4AD
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute7
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute7"] = value;
				}
			}

			// Token: 0x1700448A RID: 17546
			// (set) Token: 0x0600695B RID: 26971 RVA: 0x000A02C0 File Offset: 0x0009E4C0
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute8
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute8"] = value;
				}
			}

			// Token: 0x1700448B RID: 17547
			// (set) Token: 0x0600695C RID: 26972 RVA: 0x000A02D3 File Offset: 0x0009E4D3
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute9
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute9"] = value;
				}
			}

			// Token: 0x1700448C RID: 17548
			// (set) Token: 0x0600695D RID: 26973 RVA: 0x000A02E6 File Offset: 0x0009E4E6
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute10
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute10"] = value;
				}
			}

			// Token: 0x1700448D RID: 17549
			// (set) Token: 0x0600695E RID: 26974 RVA: 0x000A02F9 File Offset: 0x0009E4F9
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute11
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute11"] = value;
				}
			}

			// Token: 0x1700448E RID: 17550
			// (set) Token: 0x0600695F RID: 26975 RVA: 0x000A030C File Offset: 0x0009E50C
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute12
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute12"] = value;
				}
			}

			// Token: 0x1700448F RID: 17551
			// (set) Token: 0x06006960 RID: 26976 RVA: 0x000A031F File Offset: 0x0009E51F
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute13
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute13"] = value;
				}
			}

			// Token: 0x17004490 RID: 17552
			// (set) Token: 0x06006961 RID: 26977 RVA: 0x000A0332 File Offset: 0x0009E532
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute14
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute14"] = value;
				}
			}

			// Token: 0x17004491 RID: 17553
			// (set) Token: 0x06006962 RID: 26978 RVA: 0x000A0345 File Offset: 0x0009E545
			public virtual MultiValuedProperty<string> ConditionalCustomAttribute15
			{
				set
				{
					base.PowerSharpParameters["ConditionalCustomAttribute15"] = value;
				}
			}

			// Token: 0x17004492 RID: 17554
			// (set) Token: 0x06006963 RID: 26979 RVA: 0x000A0358 File Offset: 0x0009E558
			public virtual string EnabledPrimarySMTPAddressTemplate
			{
				set
				{
					base.PowerSharpParameters["EnabledPrimarySMTPAddressTemplate"] = value;
				}
			}

			// Token: 0x17004493 RID: 17555
			// (set) Token: 0x06006964 RID: 26980 RVA: 0x000A036B File Offset: 0x0009E56B
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x17004494 RID: 17556
			// (set) Token: 0x06006965 RID: 26981 RVA: 0x000A0389 File Offset: 0x0009E589
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x17004495 RID: 17557
			// (set) Token: 0x06006966 RID: 26982 RVA: 0x000A03A1 File Offset: 0x0009E5A1
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x17004496 RID: 17558
			// (set) Token: 0x06006967 RID: 26983 RVA: 0x000A03BF File Offset: 0x0009E5BF
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x17004497 RID: 17559
			// (set) Token: 0x06006968 RID: 26984 RVA: 0x000A03D2 File Offset: 0x0009E5D2
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x17004498 RID: 17560
			// (set) Token: 0x06006969 RID: 26985 RVA: 0x000A03E5 File Offset: 0x0009E5E5
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x17004499 RID: 17561
			// (set) Token: 0x0600696A RID: 26986 RVA: 0x000A03FD File Offset: 0x0009E5FD
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x1700449A RID: 17562
			// (set) Token: 0x0600696B RID: 26987 RVA: 0x000A0415 File Offset: 0x0009E615
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x1700449B RID: 17563
			// (set) Token: 0x0600696C RID: 26988 RVA: 0x000A042D File Offset: 0x0009E62D
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x1700449C RID: 17564
			// (set) Token: 0x0600696D RID: 26989 RVA: 0x000A0445 File Offset: 0x0009E645
			public virtual SwitchParameter WhatIf
			{
				set
				{
					base.PowerSharpParameters["WhatIf"] = value;
				}
			}
		}

		// Token: 0x02000845 RID: 2117
		public class DefaultParameters : ParametersBase
		{
			// Token: 0x1700449D RID: 17565
			// (set) Token: 0x0600696F RID: 26991 RVA: 0x000A0465 File Offset: 0x0009E665
			public virtual string RecipientContainer
			{
				set
				{
					base.PowerSharpParameters["RecipientContainer"] = ((value != null) ? new OrganizationalUnitIdParameter(value) : null);
				}
			}

			// Token: 0x1700449E RID: 17566
			// (set) Token: 0x06006970 RID: 26992 RVA: 0x000A0483 File Offset: 0x0009E683
			public virtual EmailAddressPolicyPriority Priority
			{
				set
				{
					base.PowerSharpParameters["Priority"] = value;
				}
			}

			// Token: 0x1700449F RID: 17567
			// (set) Token: 0x06006971 RID: 26993 RVA: 0x000A049B File Offset: 0x0009E69B
			public virtual string Organization
			{
				set
				{
					base.PowerSharpParameters["Organization"] = ((value != null) ? new OrganizationIdParameter(value) : null);
				}
			}

			// Token: 0x170044A0 RID: 17568
			// (set) Token: 0x06006972 RID: 26994 RVA: 0x000A04B9 File Offset: 0x0009E6B9
			public virtual string Name
			{
				set
				{
					base.PowerSharpParameters["Name"] = value;
				}
			}

			// Token: 0x170044A1 RID: 17569
			// (set) Token: 0x06006973 RID: 26995 RVA: 0x000A04CC File Offset: 0x0009E6CC
			public virtual Fqdn DomainController
			{
				set
				{
					base.PowerSharpParameters["DomainController"] = value;
				}
			}

			// Token: 0x170044A2 RID: 17570
			// (set) Token: 0x06006974 RID: 26996 RVA: 0x000A04DF File Offset: 0x0009E6DF
			public virtual SwitchParameter Verbose
			{
				set
				{
					base.PowerSharpParameters["Verbose"] = value;
				}
			}

			// Token: 0x170044A3 RID: 17571
			// (set) Token: 0x06006975 RID: 26997 RVA: 0x000A04F7 File Offset: 0x0009E6F7
			public virtual SwitchParameter Debug
			{
				set
				{
					base.PowerSharpParameters["Debug"] = value;
				}
			}

			// Token: 0x170044A4 RID: 17572
			// (set) Token: 0x06006976 RID: 26998 RVA: 0x000A050F File Offset: 0x0009E70F
			public virtual ActionPreference ErrorAction
			{
				set
				{
					base.PowerSharpParameters["ErrorAction"] = value;
				}
			}

			// Token: 0x170044A5 RID: 17573
			// (set) Token: 0x06006977 RID: 26999 RVA: 0x000A0527 File Offset: 0x0009E727
			public virtual ActionPreference WarningAction
			{
				set
				{
					base.PowerSharpParameters["WarningAction"] = value;
				}
			}

			// Token: 0x170044A6 RID: 17574
			// (set) Token: 0x06006978 RID: 27000 RVA: 0x000A053F File Offset: 0x0009E73F
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
