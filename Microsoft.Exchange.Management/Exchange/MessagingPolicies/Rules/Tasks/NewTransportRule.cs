using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.RightsManagement;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Management.RightsManagement;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.MessagingPolicies.CompliancePrograms.Tasks;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5C RID: 2908
	[Cmdlet("New", "TransportRule", SupportsShouldProcess = true)]
	public sealed class NewTransportRule : NewMultitenancyFixedNameSystemConfigurationObjectTask<TransportRule>
	{
		// Token: 0x1700208A RID: 8330
		// (get) Token: 0x06006986 RID: 27014 RVA: 0x001B3B9F File Offset: 0x001B1D9F
		protected override LocalizedString ConfirmationMessage
		{
			get
			{
				return Strings.ConfirmationMessageNewTransportRule(this.Name);
			}
		}

		// Token: 0x06006987 RID: 27015 RVA: 0x001B3BAC File Offset: 0x001B1DAC
		public NewTransportRule()
		{
			this.ruleCollectionName = Utils.RuleCollectionNameFromRole();
			this.Priority = 0;
			this.Enabled = true;
			this.ruleMode = RuleMode.Enforce;
			this.ActivationDate = null;
			this.ExpiryDate = null;
			base.Fields.ResetChangeTracking();
		}

		// Token: 0x1700208B RID: 8331
		// (get) Token: 0x06006988 RID: 27016 RVA: 0x001B3C08 File Offset: 0x001B1E08
		// (set) Token: 0x06006989 RID: 27017 RVA: 0x001B3C1F File Offset: 0x001B1E1F
		[Parameter(Mandatory = true, Position = 0)]
		public string Name
		{
			get
			{
				return (string)base.Fields["Name"];
			}
			set
			{
				base.Fields["Name"] = value;
			}
		}

		// Token: 0x1700208C RID: 8332
		// (get) Token: 0x0600698A RID: 27018 RVA: 0x001B3C32 File Offset: 0x001B1E32
		// (set) Token: 0x0600698B RID: 27019 RVA: 0x001B3C49 File Offset: 0x001B1E49
		[Parameter(Mandatory = false)]
		public int Priority
		{
			get
			{
				return (int)base.Fields["Priority"];
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(Strings.NegativePriority);
				}
				base.Fields["Priority"] = value;
			}
		}

		// Token: 0x1700208D RID: 8333
		// (get) Token: 0x0600698C RID: 27020 RVA: 0x001B3C75 File Offset: 0x001B1E75
		// (set) Token: 0x0600698D RID: 27021 RVA: 0x001B3C8C File Offset: 0x001B1E8C
		[Parameter(Mandatory = false)]
		public string Comments
		{
			get
			{
				return (string)base.Fields["Comments"];
			}
			set
			{
				base.Fields["Comments"] = value;
			}
		}

		// Token: 0x1700208E RID: 8334
		// (get) Token: 0x0600698E RID: 27022 RVA: 0x001B3C9F File Offset: 0x001B1E9F
		// (set) Token: 0x0600698F RID: 27023 RVA: 0x001B3CB6 File Offset: 0x001B1EB6
		[Parameter(Mandatory = false)]
		public bool UseLegacyRegex
		{
			get
			{
				return (bool)base.Fields["UseLegacyRegex"];
			}
			set
			{
				base.Fields["UseLegacyRegex"] = value;
			}
		}

		// Token: 0x1700208F RID: 8335
		// (get) Token: 0x06006990 RID: 27024 RVA: 0x001B3CCE File Offset: 0x001B1ECE
		// (set) Token: 0x06006991 RID: 27025 RVA: 0x001B3CE5 File Offset: 0x001B1EE5
		[Parameter(Mandatory = false)]
		public string DlpPolicy
		{
			get
			{
				return (string)base.Fields["DlpPolicy"];
			}
			set
			{
				base.Fields["DlpPolicy"] = value;
			}
		}

		// Token: 0x17002090 RID: 8336
		// (get) Token: 0x06006992 RID: 27026 RVA: 0x001B3CF8 File Offset: 0x001B1EF8
		// (set) Token: 0x06006993 RID: 27027 RVA: 0x001B3D0F File Offset: 0x001B1F0F
		public TransportRulePredicate[] Conditions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Conditions"];
			}
			set
			{
				base.Fields["Conditions"] = value;
			}
		}

		// Token: 0x17002091 RID: 8337
		// (get) Token: 0x06006994 RID: 27028 RVA: 0x001B3D22 File Offset: 0x001B1F22
		// (set) Token: 0x06006995 RID: 27029 RVA: 0x001B3D39 File Offset: 0x001B1F39
		public TransportRulePredicate[] Exceptions
		{
			get
			{
				return (TransportRulePredicate[])base.Fields["Exceptions"];
			}
			set
			{
				base.Fields["Exceptions"] = value;
			}
		}

		// Token: 0x17002092 RID: 8338
		// (get) Token: 0x06006996 RID: 27030 RVA: 0x001B3D4C File Offset: 0x001B1F4C
		// (set) Token: 0x06006997 RID: 27031 RVA: 0x001B3D63 File Offset: 0x001B1F63
		public TransportRuleAction[] Actions
		{
			get
			{
				return (TransportRuleAction[])base.Fields["Actions"];
			}
			set
			{
				base.Fields["Actions"] = value;
			}
		}

		// Token: 0x17002093 RID: 8339
		// (get) Token: 0x06006998 RID: 27032 RVA: 0x001B3D76 File Offset: 0x001B1F76
		// (set) Token: 0x06006999 RID: 27033 RVA: 0x001B3D8D File Offset: 0x001B1F8D
		[Parameter(Mandatory = false)]
		public bool Enabled
		{
			get
			{
				return (bool)base.Fields["Enabled"];
			}
			set
			{
				base.Fields["Enabled"] = value;
			}
		}

		// Token: 0x17002094 RID: 8340
		// (get) Token: 0x0600699A RID: 27034 RVA: 0x001B3DA5 File Offset: 0x001B1FA5
		// (set) Token: 0x0600699B RID: 27035 RVA: 0x001B3DBC File Offset: 0x001B1FBC
		[Parameter(Mandatory = false)]
		public RuleMode Mode
		{
			get
			{
				return (RuleMode)base.Fields["Mode"];
			}
			set
			{
				base.Fields["Mode"] = value;
			}
		}

		// Token: 0x17002095 RID: 8341
		// (get) Token: 0x0600699C RID: 27036 RVA: 0x001B3DD4 File Offset: 0x001B1FD4
		// (set) Token: 0x0600699D RID: 27037 RVA: 0x001B3DEB File Offset: 0x001B1FEB
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] From
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["From"];
			}
			set
			{
				base.Fields["From"] = value;
			}
		}

		// Token: 0x17002096 RID: 8342
		// (get) Token: 0x0600699E RID: 27038 RVA: 0x001B3DFE File Offset: 0x001B1FFE
		// (set) Token: 0x0600699F RID: 27039 RVA: 0x001B3E15 File Offset: 0x001B2015
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] FromMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["FromMemberOf"];
			}
			set
			{
				base.Fields["FromMemberOf"] = value;
			}
		}

		// Token: 0x17002097 RID: 8343
		// (get) Token: 0x060069A0 RID: 27040 RVA: 0x001B3E28 File Offset: 0x001B2028
		// (set) Token: 0x060069A1 RID: 27041 RVA: 0x001B3E3F File Offset: 0x001B203F
		[Parameter(Mandatory = false)]
		public FromUserScope FromScope
		{
			get
			{
				return (FromUserScope)base.Fields["FromScope"];
			}
			set
			{
				base.Fields["FromScope"] = value;
			}
		}

		// Token: 0x17002098 RID: 8344
		// (get) Token: 0x060069A2 RID: 27042 RVA: 0x001B3E57 File Offset: 0x001B2057
		// (set) Token: 0x060069A3 RID: 27043 RVA: 0x001B3E6E File Offset: 0x001B206E
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentTo"];
			}
			set
			{
				base.Fields["SentTo"] = value;
			}
		}

		// Token: 0x17002099 RID: 8345
		// (get) Token: 0x060069A4 RID: 27044 RVA: 0x001B3E81 File Offset: 0x001B2081
		// (set) Token: 0x060069A5 RID: 27045 RVA: 0x001B3E98 File Offset: 0x001B2098
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] SentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["SentToMemberOf"];
			}
			set
			{
				base.Fields["SentToMemberOf"] = value;
			}
		}

		// Token: 0x1700209A RID: 8346
		// (get) Token: 0x060069A6 RID: 27046 RVA: 0x001B3EAB File Offset: 0x001B20AB
		// (set) Token: 0x060069A7 RID: 27047 RVA: 0x001B3EC2 File Offset: 0x001B20C2
		[Parameter(Mandatory = false)]
		public ToUserScope SentToScope
		{
			get
			{
				return (ToUserScope)base.Fields["SentToScope"];
			}
			set
			{
				base.Fields["SentToScope"] = value;
			}
		}

		// Token: 0x1700209B RID: 8347
		// (get) Token: 0x060069A8 RID: 27048 RVA: 0x001B3EDA File Offset: 0x001B20DA
		// (set) Token: 0x060069A9 RID: 27049 RVA: 0x001B3EF1 File Offset: 0x001B20F1
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BetweenMemberOf1
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BetweenMemberOf1"];
			}
			set
			{
				base.Fields["BetweenMemberOf1"] = value;
			}
		}

		// Token: 0x1700209C RID: 8348
		// (get) Token: 0x060069AA RID: 27050 RVA: 0x001B3F04 File Offset: 0x001B2104
		// (set) Token: 0x060069AB RID: 27051 RVA: 0x001B3F1B File Offset: 0x001B211B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BetweenMemberOf2
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BetweenMemberOf2"];
			}
			set
			{
				base.Fields["BetweenMemberOf2"] = value;
			}
		}

		// Token: 0x1700209D RID: 8349
		// (get) Token: 0x060069AC RID: 27052 RVA: 0x001B3F2E File Offset: 0x001B212E
		// (set) Token: 0x060069AD RID: 27053 RVA: 0x001B3F45 File Offset: 0x001B2145
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ManagerAddresses
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ManagerAddresses"];
			}
			set
			{
				base.Fields["ManagerAddresses"] = value;
			}
		}

		// Token: 0x1700209E RID: 8350
		// (get) Token: 0x060069AE RID: 27054 RVA: 0x001B3F58 File Offset: 0x001B2158
		// (set) Token: 0x060069AF RID: 27055 RVA: 0x001B3F6F File Offset: 0x001B216F
		[Parameter(Mandatory = false)]
		public EvaluatedUser ManagerForEvaluatedUser
		{
			get
			{
				return (EvaluatedUser)base.Fields["ManagerForEvaluatedUser"];
			}
			set
			{
				base.Fields["ManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x1700209F RID: 8351
		// (get) Token: 0x060069B0 RID: 27056 RVA: 0x001B3F87 File Offset: 0x001B2187
		// (set) Token: 0x060069B1 RID: 27057 RVA: 0x001B3F9E File Offset: 0x001B219E
		[Parameter(Mandatory = false)]
		public ManagementRelationship SenderManagementRelationship
		{
			get
			{
				return (ManagementRelationship)base.Fields["SenderManagementRelationship"];
			}
			set
			{
				base.Fields["SenderManagementRelationship"] = value;
			}
		}

		// Token: 0x170020A0 RID: 8352
		// (get) Token: 0x060069B2 RID: 27058 RVA: 0x001B3FB6 File Offset: 0x001B21B6
		// (set) Token: 0x060069B3 RID: 27059 RVA: 0x001B3FCD File Offset: 0x001B21CD
		[Parameter(Mandatory = false)]
		public ADAttribute ADComparisonAttribute
		{
			get
			{
				return (ADAttribute)base.Fields["ADComparisonAttribute"];
			}
			set
			{
				base.Fields["ADComparisonAttribute"] = value;
			}
		}

		// Token: 0x170020A1 RID: 8353
		// (get) Token: 0x060069B4 RID: 27060 RVA: 0x001B3FE5 File Offset: 0x001B21E5
		// (set) Token: 0x060069B5 RID: 27061 RVA: 0x001B3FFC File Offset: 0x001B21FC
		[Parameter(Mandatory = false)]
		public Evaluation ADComparisonOperator
		{
			get
			{
				return (Evaluation)base.Fields["ADComparisonOperator"];
			}
			set
			{
				base.Fields["ADComparisonOperator"] = value;
			}
		}

		// Token: 0x170020A2 RID: 8354
		// (get) Token: 0x060069B6 RID: 27062 RVA: 0x001B4014 File Offset: 0x001B2214
		// (set) Token: 0x060069B7 RID: 27063 RVA: 0x001B402B File Offset: 0x001B222B
		[Parameter(Mandatory = false)]
		public Word[] SenderADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["SenderADAttributeContainsWords"];
			}
			set
			{
				base.Fields["SenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x170020A3 RID: 8355
		// (get) Token: 0x060069B8 RID: 27064 RVA: 0x001B403E File Offset: 0x001B223E
		// (set) Token: 0x060069B9 RID: 27065 RVA: 0x001B4055 File Offset: 0x001B2255
		[Parameter(Mandatory = false)]
		public Pattern[] SenderADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SenderADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["SenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020A4 RID: 8356
		// (get) Token: 0x060069BA RID: 27066 RVA: 0x001B4068 File Offset: 0x001B2268
		// (set) Token: 0x060069BB RID: 27067 RVA: 0x001B407F File Offset: 0x001B227F
		[Parameter(Mandatory = false)]
		public Word[] RecipientADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["RecipientADAttributeContainsWords"];
			}
			set
			{
				base.Fields["RecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x170020A5 RID: 8357
		// (get) Token: 0x060069BC RID: 27068 RVA: 0x001B4092 File Offset: 0x001B2292
		// (set) Token: 0x060069BD RID: 27069 RVA: 0x001B40A9 File Offset: 0x001B22A9
		[Parameter(Mandatory = false)]
		public Pattern[] RecipientADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["RecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["RecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020A6 RID: 8358
		// (get) Token: 0x060069BE RID: 27070 RVA: 0x001B40BC File Offset: 0x001B22BC
		// (set) Token: 0x060069BF RID: 27071 RVA: 0x001B40D3 File Offset: 0x001B22D3
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToHeader"];
			}
			set
			{
				base.Fields["AnyOfToHeader"] = value;
			}
		}

		// Token: 0x170020A7 RID: 8359
		// (get) Token: 0x060069C0 RID: 27072 RVA: 0x001B40E6 File Offset: 0x001B22E6
		// (set) Token: 0x060069C1 RID: 27073 RVA: 0x001B40FD File Offset: 0x001B22FD
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfToHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020A8 RID: 8360
		// (get) Token: 0x060069C2 RID: 27074 RVA: 0x001B4110 File Offset: 0x001B2310
		// (set) Token: 0x060069C3 RID: 27075 RVA: 0x001B4127 File Offset: 0x001B2327
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfCcHeader"];
			}
			set
			{
				base.Fields["AnyOfCcHeader"] = value;
			}
		}

		// Token: 0x170020A9 RID: 8361
		// (get) Token: 0x060069C4 RID: 27076 RVA: 0x001B413A File Offset: 0x001B233A
		// (set) Token: 0x060069C5 RID: 27077 RVA: 0x001B4151 File Offset: 0x001B2351
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020AA RID: 8362
		// (get) Token: 0x060069C6 RID: 27078 RVA: 0x001B4164 File Offset: 0x001B2364
		// (set) Token: 0x060069C7 RID: 27079 RVA: 0x001B417B File Offset: 0x001B237B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToCcHeader"];
			}
			set
			{
				base.Fields["AnyOfToCcHeader"] = value;
			}
		}

		// Token: 0x170020AB RID: 8363
		// (get) Token: 0x060069C8 RID: 27080 RVA: 0x001B418E File Offset: 0x001B238E
		// (set) Token: 0x060069C9 RID: 27081 RVA: 0x001B41A5 File Offset: 0x001B23A5
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AnyOfToCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AnyOfToCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["AnyOfToCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020AC RID: 8364
		// (get) Token: 0x060069CA RID: 27082 RVA: 0x001B41B8 File Offset: 0x001B23B8
		// (set) Token: 0x060069CB RID: 27083 RVA: 0x001B41CF File Offset: 0x001B23CF
		[Parameter(Mandatory = false)]
		public string HasClassification
		{
			get
			{
				return (string)base.Fields["HasClassification"];
			}
			set
			{
				base.Fields["HasClassification"] = value;
			}
		}

		// Token: 0x170020AD RID: 8365
		// (get) Token: 0x060069CC RID: 27084 RVA: 0x001B41E2 File Offset: 0x001B23E2
		// (set) Token: 0x060069CD RID: 27085 RVA: 0x001B41F9 File Offset: 0x001B23F9
		[Parameter(Mandatory = false)]
		public bool HasNoClassification
		{
			get
			{
				return (bool)base.Fields["HasNoClassification"];
			}
			set
			{
				base.Fields["HasNoClassification"] = value;
			}
		}

		// Token: 0x170020AE RID: 8366
		// (get) Token: 0x060069CE RID: 27086 RVA: 0x001B4211 File Offset: 0x001B2411
		// (set) Token: 0x060069CF RID: 27087 RVA: 0x001B4228 File Offset: 0x001B2428
		[Parameter(Mandatory = false)]
		public Word[] SubjectContainsWords
		{
			get
			{
				return (Word[])base.Fields["SubjectContainsWords"];
			}
			set
			{
				base.Fields["SubjectContainsWords"] = value;
			}
		}

		// Token: 0x170020AF RID: 8367
		// (get) Token: 0x060069D0 RID: 27088 RVA: 0x001B423B File Offset: 0x001B243B
		// (set) Token: 0x060069D1 RID: 27089 RVA: 0x001B4252 File Offset: 0x001B2452
		[Parameter(Mandatory = false)]
		public Word[] SubjectOrBodyContainsWords
		{
			get
			{
				return (Word[])base.Fields["SubjectOrBodyContainsWords"];
			}
			set
			{
				base.Fields["SubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x170020B0 RID: 8368
		// (get) Token: 0x060069D2 RID: 27090 RVA: 0x001B4265 File Offset: 0x001B2465
		// (set) Token: 0x060069D3 RID: 27091 RVA: 0x001B427C File Offset: 0x001B247C
		[Parameter(Mandatory = false)]
		public HeaderName HeaderContainsMessageHeader
		{
			get
			{
				return (HeaderName)base.Fields["HeaderContainsMessageHeader"];
			}
			set
			{
				base.Fields["HeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x170020B1 RID: 8369
		// (get) Token: 0x060069D4 RID: 27092 RVA: 0x001B4294 File Offset: 0x001B2494
		// (set) Token: 0x060069D5 RID: 27093 RVA: 0x001B42AB File Offset: 0x001B24AB
		[Parameter(Mandatory = false)]
		public Word[] HeaderContainsWords
		{
			get
			{
				return (Word[])base.Fields["HeaderContainsWords"];
			}
			set
			{
				base.Fields["HeaderContainsWords"] = value;
			}
		}

		// Token: 0x170020B2 RID: 8370
		// (get) Token: 0x060069D6 RID: 27094 RVA: 0x001B42BE File Offset: 0x001B24BE
		// (set) Token: 0x060069D7 RID: 27095 RVA: 0x001B42D5 File Offset: 0x001B24D5
		[Parameter(Mandatory = false)]
		public Word[] FromAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["FromAddressContainsWords"];
			}
			set
			{
				base.Fields["FromAddressContainsWords"] = value;
			}
		}

		// Token: 0x170020B3 RID: 8371
		// (get) Token: 0x060069D8 RID: 27096 RVA: 0x001B42E8 File Offset: 0x001B24E8
		// (set) Token: 0x060069D9 RID: 27097 RVA: 0x001B42FF File Offset: 0x001B24FF
		[Parameter(Mandatory = false)]
		public Word[] SenderDomainIs
		{
			get
			{
				return (Word[])base.Fields["SenderDomainIs"];
			}
			set
			{
				base.Fields["SenderDomainIs"] = value;
			}
		}

		// Token: 0x170020B4 RID: 8372
		// (get) Token: 0x060069DA RID: 27098 RVA: 0x001B4312 File Offset: 0x001B2512
		// (set) Token: 0x060069DB RID: 27099 RVA: 0x001B4329 File Offset: 0x001B2529
		[Parameter(Mandatory = false)]
		public Word[] RecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["RecipientDomainIs"];
			}
			set
			{
				base.Fields["RecipientDomainIs"] = value;
			}
		}

		// Token: 0x170020B5 RID: 8373
		// (get) Token: 0x060069DC RID: 27100 RVA: 0x001B433C File Offset: 0x001B253C
		// (set) Token: 0x060069DD RID: 27101 RVA: 0x001B4353 File Offset: 0x001B2553
		[Parameter(Mandatory = false)]
		public Pattern[] SubjectMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SubjectMatchesPatterns"];
			}
			set
			{
				base.Fields["SubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020B6 RID: 8374
		// (get) Token: 0x060069DE RID: 27102 RVA: 0x001B4366 File Offset: 0x001B2566
		// (set) Token: 0x060069DF RID: 27103 RVA: 0x001B437D File Offset: 0x001B257D
		[Parameter(Mandatory = false)]
		public Pattern[] SubjectOrBodyMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["SubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base.Fields["SubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020B7 RID: 8375
		// (get) Token: 0x060069E0 RID: 27104 RVA: 0x001B4390 File Offset: 0x001B2590
		// (set) Token: 0x060069E1 RID: 27105 RVA: 0x001B43A7 File Offset: 0x001B25A7
		[Parameter(Mandatory = false)]
		public HeaderName HeaderMatchesMessageHeader
		{
			get
			{
				return (HeaderName)base.Fields["HeaderMatchesMessageHeader"];
			}
			set
			{
				base.Fields["HeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x060069E2 RID: 27106 RVA: 0x001B43BF File Offset: 0x001B25BF
		// (set) Token: 0x060069E3 RID: 27107 RVA: 0x001B43D6 File Offset: 0x001B25D6
		[Parameter(Mandatory = false)]
		public Pattern[] HeaderMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["HeaderMatchesPatterns"];
			}
			set
			{
				base.Fields["HeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x060069E4 RID: 27108 RVA: 0x001B43E9 File Offset: 0x001B25E9
		// (set) Token: 0x060069E5 RID: 27109 RVA: 0x001B4400 File Offset: 0x001B2600
		[Parameter(Mandatory = false)]
		public Pattern[] FromAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["FromAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["FromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x060069E6 RID: 27110 RVA: 0x001B4413 File Offset: 0x001B2613
		// (set) Token: 0x060069E7 RID: 27111 RVA: 0x001B442A File Offset: 0x001B262A
		[Parameter(Mandatory = false)]
		public Pattern[] AttachmentNameMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AttachmentNameMatchesPatterns"];
			}
			set
			{
				base.Fields["AttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x060069E8 RID: 27112 RVA: 0x001B443D File Offset: 0x001B263D
		// (set) Token: 0x060069E9 RID: 27113 RVA: 0x001B4454 File Offset: 0x001B2654
		[Parameter(Mandatory = false)]
		public Word[] AttachmentExtensionMatchesWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentExtensionMatchesWords"];
			}
			set
			{
				base.Fields["AttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x060069EA RID: 27114 RVA: 0x001B4467 File Offset: 0x001B2667
		// (set) Token: 0x060069EB RID: 27115 RVA: 0x001B447E File Offset: 0x001B267E
		[Parameter(Mandatory = false)]
		public Word[] AttachmentPropertyContainsWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentPropertyContainsWords"];
			}
			set
			{
				base.Fields["AttachmentPropertyContainsWords"] = value;
			}
		}

		// Token: 0x170020BD RID: 8381
		// (get) Token: 0x060069EC RID: 27116 RVA: 0x001B4491 File Offset: 0x001B2691
		// (set) Token: 0x060069ED RID: 27117 RVA: 0x001B44A8 File Offset: 0x001B26A8
		[Parameter(Mandatory = false)]
		public Word[] ContentCharacterSetContainsWords
		{
			get
			{
				return (Word[])base.Fields["ContentCharacterSetContainsWords"];
			}
			set
			{
				base.Fields["ContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x170020BE RID: 8382
		// (get) Token: 0x060069EE RID: 27118 RVA: 0x001B44BB File Offset: 0x001B26BB
		// (set) Token: 0x060069EF RID: 27119 RVA: 0x001B44D2 File Offset: 0x001B26D2
		[Parameter(Mandatory = false)]
		public SclValue SCLOver
		{
			get
			{
				return (SclValue)base.Fields["SCLOver"];
			}
			set
			{
				base.Fields["SCLOver"] = value;
			}
		}

		// Token: 0x170020BF RID: 8383
		// (get) Token: 0x060069F0 RID: 27120 RVA: 0x001B44EA File Offset: 0x001B26EA
		// (set) Token: 0x060069F1 RID: 27121 RVA: 0x001B4501 File Offset: 0x001B2701
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize AttachmentSizeOver
		{
			get
			{
				return (ByteQuantifiedSize)base.Fields["AttachmentSizeOver"];
			}
			set
			{
				base.Fields["AttachmentSizeOver"] = value;
			}
		}

		// Token: 0x170020C0 RID: 8384
		// (get) Token: 0x060069F2 RID: 27122 RVA: 0x001B4519 File Offset: 0x001B2719
		// (set) Token: 0x060069F3 RID: 27123 RVA: 0x001B4530 File Offset: 0x001B2730
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize MessageSizeOver
		{
			get
			{
				return (ByteQuantifiedSize)base.Fields["MessageSizeOver"];
			}
			set
			{
				base.Fields["MessageSizeOver"] = value;
			}
		}

		// Token: 0x170020C1 RID: 8385
		// (get) Token: 0x060069F4 RID: 27124 RVA: 0x001B4548 File Offset: 0x001B2748
		// (set) Token: 0x060069F5 RID: 27125 RVA: 0x001B455F File Offset: 0x001B275F
		[Parameter(Mandatory = false)]
		public Importance WithImportance
		{
			get
			{
				return (Importance)base.Fields["WithImportance"];
			}
			set
			{
				base.Fields["WithImportance"] = value;
			}
		}

		// Token: 0x170020C2 RID: 8386
		// (get) Token: 0x060069F6 RID: 27126 RVA: 0x001B4577 File Offset: 0x001B2777
		// (set) Token: 0x060069F7 RID: 27127 RVA: 0x001B458E File Offset: 0x001B278E
		[Parameter(Mandatory = false)]
		public MessageType MessageTypeMatches
		{
			get
			{
				return (MessageType)base.Fields["MessageTypeMatches"];
			}
			set
			{
				base.Fields["MessageTypeMatches"] = value;
			}
		}

		// Token: 0x170020C3 RID: 8387
		// (get) Token: 0x060069F8 RID: 27128 RVA: 0x001B45A6 File Offset: 0x001B27A6
		// (set) Token: 0x060069F9 RID: 27129 RVA: 0x001B45BD File Offset: 0x001B27BD
		[Parameter(Mandatory = false)]
		public Word[] RecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["RecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["RecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x170020C4 RID: 8388
		// (get) Token: 0x060069FA RID: 27130 RVA: 0x001B45D0 File Offset: 0x001B27D0
		// (set) Token: 0x060069FB RID: 27131 RVA: 0x001B45E7 File Offset: 0x001B27E7
		[Parameter(Mandatory = false)]
		public Pattern[] RecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["RecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["RecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020C5 RID: 8389
		// (get) Token: 0x060069FC RID: 27132 RVA: 0x001B45FA File Offset: 0x001B27FA
		// (set) Token: 0x060069FD RID: 27133 RVA: 0x001B4611 File Offset: 0x001B2811
		[Parameter(Mandatory = false)]
		public Word[] SenderInRecipientList
		{
			get
			{
				return (Word[])base.Fields["SenderInRecipientList"];
			}
			set
			{
				base.Fields["SenderInRecipientList"] = value;
			}
		}

		// Token: 0x170020C6 RID: 8390
		// (get) Token: 0x060069FE RID: 27134 RVA: 0x001B4624 File Offset: 0x001B2824
		// (set) Token: 0x060069FF RID: 27135 RVA: 0x001B463B File Offset: 0x001B283B
		[Parameter(Mandatory = false)]
		public Word[] RecipientInSenderList
		{
			get
			{
				return (Word[])base.Fields["RecipientInSenderList"];
			}
			set
			{
				base.Fields["RecipientInSenderList"] = value;
			}
		}

		// Token: 0x170020C7 RID: 8391
		// (get) Token: 0x06006A00 RID: 27136 RVA: 0x001B464E File Offset: 0x001B284E
		// (set) Token: 0x06006A01 RID: 27137 RVA: 0x001B4665 File Offset: 0x001B2865
		[Parameter(Mandatory = false)]
		public Word[] AttachmentContainsWords
		{
			get
			{
				return (Word[])base.Fields["AttachmentContainsWords"];
			}
			set
			{
				base.Fields["AttachmentContainsWords"] = value;
			}
		}

		// Token: 0x170020C8 RID: 8392
		// (get) Token: 0x06006A02 RID: 27138 RVA: 0x001B4678 File Offset: 0x001B2878
		// (set) Token: 0x06006A03 RID: 27139 RVA: 0x001B468F File Offset: 0x001B288F
		[Parameter(Mandatory = false)]
		public Pattern[] AttachmentMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AttachmentMatchesPatterns"];
			}
			set
			{
				base.Fields["AttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020C9 RID: 8393
		// (get) Token: 0x06006A04 RID: 27140 RVA: 0x001B46A2 File Offset: 0x001B28A2
		// (set) Token: 0x06006A05 RID: 27141 RVA: 0x001B46B9 File Offset: 0x001B28B9
		[Parameter(Mandatory = false)]
		public bool AttachmentIsUnsupported
		{
			get
			{
				return (bool)base.Fields["AttachmentIsUnsupported"];
			}
			set
			{
				base.Fields["AttachmentIsUnsupported"] = value;
			}
		}

		// Token: 0x170020CA RID: 8394
		// (get) Token: 0x06006A06 RID: 27142 RVA: 0x001B46D1 File Offset: 0x001B28D1
		// (set) Token: 0x06006A07 RID: 27143 RVA: 0x001B46E8 File Offset: 0x001B28E8
		[Parameter(Mandatory = false)]
		public bool AttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool)base.Fields["AttachmentProcessingLimitExceeded"];
			}
			set
			{
				base.Fields["AttachmentProcessingLimitExceeded"] = value;
			}
		}

		// Token: 0x170020CB RID: 8395
		// (get) Token: 0x06006A08 RID: 27144 RVA: 0x001B4700 File Offset: 0x001B2900
		// (set) Token: 0x06006A09 RID: 27145 RVA: 0x001B4717 File Offset: 0x001B2917
		[Parameter(Mandatory = false)]
		public bool AttachmentHasExecutableContent
		{
			get
			{
				return (bool)base.Fields["AttachmentHasExecutableContent"];
			}
			set
			{
				base.Fields["AttachmentHasExecutableContent"] = value;
			}
		}

		// Token: 0x170020CC RID: 8396
		// (get) Token: 0x06006A0A RID: 27146 RVA: 0x001B472F File Offset: 0x001B292F
		// (set) Token: 0x06006A0B RID: 27147 RVA: 0x001B4746 File Offset: 0x001B2946
		[Parameter(Mandatory = false)]
		public bool AttachmentIsPasswordProtected
		{
			get
			{
				return (bool)base.Fields["AttachmentIsPasswordProtected"];
			}
			set
			{
				base.Fields["AttachmentIsPasswordProtected"] = value;
			}
		}

		// Token: 0x170020CD RID: 8397
		// (get) Token: 0x06006A0C RID: 27148 RVA: 0x001B475E File Offset: 0x001B295E
		// (set) Token: 0x06006A0D RID: 27149 RVA: 0x001B4775 File Offset: 0x001B2975
		[Parameter(Mandatory = false)]
		public Word[] AnyOfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["AnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["AnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x170020CE RID: 8398
		// (get) Token: 0x06006A0E RID: 27150 RVA: 0x001B4788 File Offset: 0x001B2988
		// (set) Token: 0x06006A0F RID: 27151 RVA: 0x001B479F File Offset: 0x001B299F
		[Parameter(Mandatory = false)]
		public Pattern[] AnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["AnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["AnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020CF RID: 8399
		// (get) Token: 0x06006A10 RID: 27152 RVA: 0x001B47B2 File Offset: 0x001B29B2
		// (set) Token: 0x06006A11 RID: 27153 RVA: 0x001B47C9 File Offset: 0x001B29C9
		[Parameter(Mandatory = false)]
		public bool HasSenderOverride
		{
			get
			{
				return (bool)base.Fields["HasSenderOverride"];
			}
			set
			{
				base.Fields["HasSenderOverride"] = value;
			}
		}

		// Token: 0x170020D0 RID: 8400
		// (get) Token: 0x06006A12 RID: 27154 RVA: 0x001B47E1 File Offset: 0x001B29E1
		// (set) Token: 0x06006A13 RID: 27155 RVA: 0x001B47F8 File Offset: 0x001B29F8
		[Parameter(Mandatory = false)]
		public bool ExceptIfHasSenderOverride
		{
			get
			{
				return (bool)base.Fields["ExceptIfHasSenderOverride"];
			}
			set
			{
				base.Fields["ExceptIfHasSenderOverride"] = value;
			}
		}

		// Token: 0x170020D1 RID: 8401
		// (get) Token: 0x06006A14 RID: 27156 RVA: 0x001B4810 File Offset: 0x001B2A10
		// (set) Token: 0x06006A15 RID: 27157 RVA: 0x001B4827 File Offset: 0x001B2A27
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfFrom
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfFrom"];
			}
			set
			{
				base.Fields["ExceptIfFrom"] = value;
			}
		}

		// Token: 0x170020D2 RID: 8402
		// (get) Token: 0x06006A16 RID: 27158 RVA: 0x001B483A File Offset: 0x001B2A3A
		// (set) Token: 0x06006A17 RID: 27159 RVA: 0x001B4851 File Offset: 0x001B2A51
		[Parameter(Mandatory = false)]
		public Hashtable[] MessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base.Fields["MessageContainsDataClassifications"];
			}
			set
			{
				base.Fields["MessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x06006A18 RID: 27160 RVA: 0x001B4864 File Offset: 0x001B2A64
		// (set) Token: 0x06006A19 RID: 27161 RVA: 0x001B487B File Offset: 0x001B2A7B
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> SenderIpRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["SenderIpRanges"];
			}
			set
			{
				base.Fields["SenderIpRanges"] = value;
			}
		}

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x06006A1A RID: 27162 RVA: 0x001B488E File Offset: 0x001B2A8E
		// (set) Token: 0x06006A1B RID: 27163 RVA: 0x001B48A5 File Offset: 0x001B2AA5
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfFromMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfFromMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfFromMemberOf"] = value;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x06006A1C RID: 27164 RVA: 0x001B48B8 File Offset: 0x001B2AB8
		// (set) Token: 0x06006A1D RID: 27165 RVA: 0x001B48CF File Offset: 0x001B2ACF
		[Parameter(Mandatory = false)]
		public FromUserScope ExceptIfFromScope
		{
			get
			{
				return (FromUserScope)base.Fields["ExceptIfFromScope"];
			}
			set
			{
				base.Fields["ExceptIfFromScope"] = value;
			}
		}

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x06006A1E RID: 27166 RVA: 0x001B48E7 File Offset: 0x001B2AE7
		// (set) Token: 0x06006A1F RID: 27167 RVA: 0x001B48FE File Offset: 0x001B2AFE
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentTo"];
			}
			set
			{
				base.Fields["ExceptIfSentTo"] = value;
			}
		}

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x06006A20 RID: 27168 RVA: 0x001B4911 File Offset: 0x001B2B11
		// (set) Token: 0x06006A21 RID: 27169 RVA: 0x001B4928 File Offset: 0x001B2B28
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfSentToMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfSentToMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfSentToMemberOf"] = value;
			}
		}

		// Token: 0x170020D8 RID: 8408
		// (get) Token: 0x06006A22 RID: 27170 RVA: 0x001B493B File Offset: 0x001B2B3B
		// (set) Token: 0x06006A23 RID: 27171 RVA: 0x001B4952 File Offset: 0x001B2B52
		[Parameter(Mandatory = false)]
		public ToUserScope ExceptIfSentToScope
		{
			get
			{
				return (ToUserScope)base.Fields["ExceptIfSentToScope"];
			}
			set
			{
				base.Fields["ExceptIfSentToScope"] = value;
			}
		}

		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06006A24 RID: 27172 RVA: 0x001B496A File Offset: 0x001B2B6A
		// (set) Token: 0x06006A25 RID: 27173 RVA: 0x001B4981 File Offset: 0x001B2B81
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf1
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfBetweenMemberOf1"];
			}
			set
			{
				base.Fields["ExceptIfBetweenMemberOf1"] = value;
			}
		}

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06006A26 RID: 27174 RVA: 0x001B4994 File Offset: 0x001B2B94
		// (set) Token: 0x06006A27 RID: 27175 RVA: 0x001B49AB File Offset: 0x001B2BAB
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfBetweenMemberOf2
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfBetweenMemberOf2"];
			}
			set
			{
				base.Fields["ExceptIfBetweenMemberOf2"] = value;
			}
		}

		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06006A28 RID: 27176 RVA: 0x001B49BE File Offset: 0x001B2BBE
		// (set) Token: 0x06006A29 RID: 27177 RVA: 0x001B49D5 File Offset: 0x001B2BD5
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfManagerAddresses
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfManagerAddresses"];
			}
			set
			{
				base.Fields["ExceptIfManagerAddresses"] = value;
			}
		}

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06006A2A RID: 27178 RVA: 0x001B49E8 File Offset: 0x001B2BE8
		// (set) Token: 0x06006A2B RID: 27179 RVA: 0x001B49FF File Offset: 0x001B2BFF
		[Parameter(Mandatory = false)]
		public EvaluatedUser ExceptIfManagerForEvaluatedUser
		{
			get
			{
				return (EvaluatedUser)base.Fields["ExceptIfManagerForEvaluatedUser"];
			}
			set
			{
				base.Fields["ExceptIfManagerForEvaluatedUser"] = value;
			}
		}

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06006A2C RID: 27180 RVA: 0x001B4A17 File Offset: 0x001B2C17
		// (set) Token: 0x06006A2D RID: 27181 RVA: 0x001B4A2E File Offset: 0x001B2C2E
		[Parameter(Mandatory = false)]
		public ManagementRelationship ExceptIfSenderManagementRelationship
		{
			get
			{
				return (ManagementRelationship)base.Fields["ExceptIfSenderManagementRelationship"];
			}
			set
			{
				base.Fields["ExceptIfSenderManagementRelationship"] = value;
			}
		}

		// Token: 0x170020DE RID: 8414
		// (get) Token: 0x06006A2E RID: 27182 RVA: 0x001B4A46 File Offset: 0x001B2C46
		// (set) Token: 0x06006A2F RID: 27183 RVA: 0x001B4A5D File Offset: 0x001B2C5D
		[Parameter(Mandatory = false)]
		public ADAttribute ExceptIfADComparisonAttribute
		{
			get
			{
				return (ADAttribute)base.Fields["ExceptIfADComparisonAttribute"];
			}
			set
			{
				base.Fields["ExceptIfADComparisonAttribute"] = value;
			}
		}

		// Token: 0x170020DF RID: 8415
		// (get) Token: 0x06006A30 RID: 27184 RVA: 0x001B4A75 File Offset: 0x001B2C75
		// (set) Token: 0x06006A31 RID: 27185 RVA: 0x001B4A8C File Offset: 0x001B2C8C
		[Parameter(Mandatory = false)]
		public Evaluation ExceptIfADComparisonOperator
		{
			get
			{
				return (Evaluation)base.Fields["ExceptIfADComparisonOperator"];
			}
			set
			{
				base.Fields["ExceptIfADComparisonOperator"] = value;
			}
		}

		// Token: 0x170020E0 RID: 8416
		// (get) Token: 0x06006A32 RID: 27186 RVA: 0x001B4AA4 File Offset: 0x001B2CA4
		// (set) Token: 0x06006A33 RID: 27187 RVA: 0x001B4ABB File Offset: 0x001B2CBB
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderADAttributeContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSenderADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x170020E1 RID: 8417
		// (get) Token: 0x06006A34 RID: 27188 RVA: 0x001B4ACE File Offset: 0x001B2CCE
		// (set) Token: 0x06006A35 RID: 27189 RVA: 0x001B4AE5 File Offset: 0x001B2CE5
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSenderADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSenderADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSenderADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020E2 RID: 8418
		// (get) Token: 0x06006A36 RID: 27190 RVA: 0x001B4AF8 File Offset: 0x001B2CF8
		// (set) Token: 0x06006A37 RID: 27191 RVA: 0x001B4B0F File Offset: 0x001B2D0F
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientADAttributeContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientADAttributeContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfRecipientADAttributeContainsWords"] = value;
			}
		}

		// Token: 0x170020E3 RID: 8419
		// (get) Token: 0x06006A38 RID: 27192 RVA: 0x001B4B22 File Offset: 0x001B2D22
		// (set) Token: 0x06006A39 RID: 27193 RVA: 0x001B4B39 File Offset: 0x001B2D39
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfRecipientADAttributeMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfRecipientADAttributeMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfRecipientADAttributeMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020E4 RID: 8420
		// (get) Token: 0x06006A3A RID: 27194 RVA: 0x001B4B4C File Offset: 0x001B2D4C
		// (set) Token: 0x06006A3B RID: 27195 RVA: 0x001B4B63 File Offset: 0x001B2D63
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToHeader"] = value;
			}
		}

		// Token: 0x170020E5 RID: 8421
		// (get) Token: 0x06006A3C RID: 27196 RVA: 0x001B4B76 File Offset: 0x001B2D76
		// (set) Token: 0x06006A3D RID: 27197 RVA: 0x001B4B8D File Offset: 0x001B2D8D
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020E6 RID: 8422
		// (get) Token: 0x06006A3E RID: 27198 RVA: 0x001B4BA0 File Offset: 0x001B2DA0
		// (set) Token: 0x06006A3F RID: 27199 RVA: 0x001B4BB7 File Offset: 0x001B2DB7
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfCcHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfCcHeader"] = value;
			}
		}

		// Token: 0x170020E7 RID: 8423
		// (get) Token: 0x06006A40 RID: 27200 RVA: 0x001B4BCA File Offset: 0x001B2DCA
		// (set) Token: 0x06006A41 RID: 27201 RVA: 0x001B4BE1 File Offset: 0x001B2DE1
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020E8 RID: 8424
		// (get) Token: 0x06006A42 RID: 27202 RVA: 0x001B4BF4 File Offset: 0x001B2DF4
		// (set) Token: 0x06006A43 RID: 27203 RVA: 0x001B4C0B File Offset: 0x001B2E0B
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeader
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToCcHeader"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToCcHeader"] = value;
			}
		}

		// Token: 0x170020E9 RID: 8425
		// (get) Token: 0x06006A44 RID: 27204 RVA: 0x001B4C1E File Offset: 0x001B2E1E
		// (set) Token: 0x06006A45 RID: 27205 RVA: 0x001B4C35 File Offset: 0x001B2E35
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ExceptIfAnyOfToCcHeaderMemberOf
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ExceptIfAnyOfToCcHeaderMemberOf"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfToCcHeaderMemberOf"] = value;
			}
		}

		// Token: 0x170020EA RID: 8426
		// (get) Token: 0x06006A46 RID: 27206 RVA: 0x001B4C48 File Offset: 0x001B2E48
		// (set) Token: 0x06006A47 RID: 27207 RVA: 0x001B4C5F File Offset: 0x001B2E5F
		[Parameter(Mandatory = false)]
		public string ExceptIfHasClassification
		{
			get
			{
				return (string)base.Fields["ExceptIfHasClassification"];
			}
			set
			{
				base.Fields["ExceptIfHasClassification"] = value;
			}
		}

		// Token: 0x170020EB RID: 8427
		// (get) Token: 0x06006A48 RID: 27208 RVA: 0x001B4C72 File Offset: 0x001B2E72
		// (set) Token: 0x06006A49 RID: 27209 RVA: 0x001B4C89 File Offset: 0x001B2E89
		[Parameter(Mandatory = false)]
		public bool ExceptIfHasNoClassification
		{
			get
			{
				return (bool)base.Fields["ExceptIfHasNoClassification"];
			}
			set
			{
				base.Fields["ExceptIfHasNoClassification"] = value;
			}
		}

		// Token: 0x170020EC RID: 8428
		// (get) Token: 0x06006A4A RID: 27210 RVA: 0x001B4CA1 File Offset: 0x001B2EA1
		// (set) Token: 0x06006A4B RID: 27211 RVA: 0x001B4CB8 File Offset: 0x001B2EB8
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSubjectContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSubjectContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSubjectContainsWords"] = value;
			}
		}

		// Token: 0x170020ED RID: 8429
		// (get) Token: 0x06006A4C RID: 27212 RVA: 0x001B4CCB File Offset: 0x001B2ECB
		// (set) Token: 0x06006A4D RID: 27213 RVA: 0x001B4CE2 File Offset: 0x001B2EE2
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSubjectOrBodyContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSubjectOrBodyContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfSubjectOrBodyContainsWords"] = value;
			}
		}

		// Token: 0x170020EE RID: 8430
		// (get) Token: 0x06006A4E RID: 27214 RVA: 0x001B4CF5 File Offset: 0x001B2EF5
		// (set) Token: 0x06006A4F RID: 27215 RVA: 0x001B4D0C File Offset: 0x001B2F0C
		[Parameter(Mandatory = false)]
		public HeaderName ExceptIfHeaderContainsMessageHeader
		{
			get
			{
				return (HeaderName)base.Fields["ExceptIfHeaderContainsMessageHeader"];
			}
			set
			{
				base.Fields["ExceptIfHeaderContainsMessageHeader"] = value;
			}
		}

		// Token: 0x170020EF RID: 8431
		// (get) Token: 0x06006A50 RID: 27216 RVA: 0x001B4D24 File Offset: 0x001B2F24
		// (set) Token: 0x06006A51 RID: 27217 RVA: 0x001B4D3B File Offset: 0x001B2F3B
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfHeaderContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfHeaderContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfHeaderContainsWords"] = value;
			}
		}

		// Token: 0x170020F0 RID: 8432
		// (get) Token: 0x06006A52 RID: 27218 RVA: 0x001B4D4E File Offset: 0x001B2F4E
		// (set) Token: 0x06006A53 RID: 27219 RVA: 0x001B4D65 File Offset: 0x001B2F65
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfFromAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfFromAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfFromAddressContainsWords"] = value;
			}
		}

		// Token: 0x170020F1 RID: 8433
		// (get) Token: 0x06006A54 RID: 27220 RVA: 0x001B4D78 File Offset: 0x001B2F78
		// (set) Token: 0x06006A55 RID: 27221 RVA: 0x001B4D8F File Offset: 0x001B2F8F
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderDomainIs
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderDomainIs"];
			}
			set
			{
				base.Fields["ExceptIfSenderDomainIs"] = value;
			}
		}

		// Token: 0x170020F2 RID: 8434
		// (get) Token: 0x06006A56 RID: 27222 RVA: 0x001B4DA2 File Offset: 0x001B2FA2
		// (set) Token: 0x06006A57 RID: 27223 RVA: 0x001B4DB9 File Offset: 0x001B2FB9
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientDomainIs
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientDomainIs"];
			}
			set
			{
				base.Fields["ExceptIfRecipientDomainIs"] = value;
			}
		}

		// Token: 0x170020F3 RID: 8435
		// (get) Token: 0x06006A58 RID: 27224 RVA: 0x001B4DCC File Offset: 0x001B2FCC
		// (set) Token: 0x06006A59 RID: 27225 RVA: 0x001B4DE3 File Offset: 0x001B2FE3
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSubjectMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSubjectMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSubjectMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020F4 RID: 8436
		// (get) Token: 0x06006A5A RID: 27226 RVA: 0x001B4DF6 File Offset: 0x001B2FF6
		// (set) Token: 0x06006A5B RID: 27227 RVA: 0x001B4E0D File Offset: 0x001B300D
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfSubjectOrBodyMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfSubjectOrBodyMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfSubjectOrBodyMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020F5 RID: 8437
		// (get) Token: 0x06006A5C RID: 27228 RVA: 0x001B4E20 File Offset: 0x001B3020
		// (set) Token: 0x06006A5D RID: 27229 RVA: 0x001B4E37 File Offset: 0x001B3037
		[Parameter(Mandatory = false)]
		public HeaderName ExceptIfHeaderMatchesMessageHeader
		{
			get
			{
				return (HeaderName)base.Fields["ExceptIfHeaderMatchesMessageHeader"];
			}
			set
			{
				base.Fields["ExceptIfHeaderMatchesMessageHeader"] = value;
			}
		}

		// Token: 0x170020F6 RID: 8438
		// (get) Token: 0x06006A5E RID: 27230 RVA: 0x001B4E4F File Offset: 0x001B304F
		// (set) Token: 0x06006A5F RID: 27231 RVA: 0x001B4E66 File Offset: 0x001B3066
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfHeaderMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfHeaderMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfHeaderMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020F7 RID: 8439
		// (get) Token: 0x06006A60 RID: 27232 RVA: 0x001B4E79 File Offset: 0x001B3079
		// (set) Token: 0x06006A61 RID: 27233 RVA: 0x001B4E90 File Offset: 0x001B3090
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfFromAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfFromAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfFromAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020F8 RID: 8440
		// (get) Token: 0x06006A62 RID: 27234 RVA: 0x001B4EA3 File Offset: 0x001B30A3
		// (set) Token: 0x06006A63 RID: 27235 RVA: 0x001B4EBA File Offset: 0x001B30BA
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAttachmentNameMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAttachmentNameMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentNameMatchesPatterns"] = value;
			}
		}

		// Token: 0x170020F9 RID: 8441
		// (get) Token: 0x06006A64 RID: 27236 RVA: 0x001B4ECD File Offset: 0x001B30CD
		// (set) Token: 0x06006A65 RID: 27237 RVA: 0x001B4EE4 File Offset: 0x001B30E4
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentExtensionMatchesWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentExtensionMatchesWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentExtensionMatchesWords"] = value;
			}
		}

		// Token: 0x170020FA RID: 8442
		// (get) Token: 0x06006A66 RID: 27238 RVA: 0x001B4EF7 File Offset: 0x001B30F7
		// (set) Token: 0x06006A67 RID: 27239 RVA: 0x001B4F0E File Offset: 0x001B310E
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentPropertyContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentPropertyContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentPropertyContainsWords"] = value;
			}
		}

		// Token: 0x170020FB RID: 8443
		// (get) Token: 0x06006A68 RID: 27240 RVA: 0x001B4F21 File Offset: 0x001B3121
		// (set) Token: 0x06006A69 RID: 27241 RVA: 0x001B4F38 File Offset: 0x001B3138
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfContentCharacterSetContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfContentCharacterSetContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfContentCharacterSetContainsWords"] = value;
			}
		}

		// Token: 0x170020FC RID: 8444
		// (get) Token: 0x06006A6A RID: 27242 RVA: 0x001B4F4B File Offset: 0x001B314B
		// (set) Token: 0x06006A6B RID: 27243 RVA: 0x001B4F62 File Offset: 0x001B3162
		[Parameter(Mandatory = false)]
		public SclValue ExceptIfSCLOver
		{
			get
			{
				return (SclValue)base.Fields["ExceptIfSCLOver"];
			}
			set
			{
				base.Fields["ExceptIfSCLOver"] = value;
			}
		}

		// Token: 0x170020FD RID: 8445
		// (get) Token: 0x06006A6C RID: 27244 RVA: 0x001B4F7A File Offset: 0x001B317A
		// (set) Token: 0x06006A6D RID: 27245 RVA: 0x001B4F91 File Offset: 0x001B3191
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ExceptIfAttachmentSizeOver
		{
			get
			{
				return (ByteQuantifiedSize)base.Fields["ExceptIfAttachmentSizeOver"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentSizeOver"] = value;
			}
		}

		// Token: 0x170020FE RID: 8446
		// (get) Token: 0x06006A6E RID: 27246 RVA: 0x001B4FA9 File Offset: 0x001B31A9
		// (set) Token: 0x06006A6F RID: 27247 RVA: 0x001B4FC0 File Offset: 0x001B31C0
		[Parameter(Mandatory = false)]
		public ByteQuantifiedSize ExceptIfMessageSizeOver
		{
			get
			{
				return (ByteQuantifiedSize)base.Fields["ExceptIfMessageSizeOver"];
			}
			set
			{
				base.Fields["ExceptIfMessageSizeOver"] = value;
			}
		}

		// Token: 0x170020FF RID: 8447
		// (get) Token: 0x06006A70 RID: 27248 RVA: 0x001B4FD8 File Offset: 0x001B31D8
		// (set) Token: 0x06006A71 RID: 27249 RVA: 0x001B4FEF File Offset: 0x001B31EF
		[Parameter(Mandatory = false)]
		public Importance ExceptIfWithImportance
		{
			get
			{
				return (Importance)base.Fields["ExceptIfWithImportance"];
			}
			set
			{
				base.Fields["ExceptIfWithImportance"] = value;
			}
		}

		// Token: 0x17002100 RID: 8448
		// (get) Token: 0x06006A72 RID: 27250 RVA: 0x001B5007 File Offset: 0x001B3207
		// (set) Token: 0x06006A73 RID: 27251 RVA: 0x001B501E File Offset: 0x001B321E
		[Parameter(Mandatory = false)]
		public MessageType ExceptIfMessageTypeMatches
		{
			get
			{
				return (MessageType)base.Fields["ExceptIfMessageTypeMatches"];
			}
			set
			{
				base.Fields["ExceptIfMessageTypeMatches"] = value;
			}
		}

		// Token: 0x17002101 RID: 8449
		// (get) Token: 0x06006A74 RID: 27252 RVA: 0x001B5036 File Offset: 0x001B3236
		// (set) Token: 0x06006A75 RID: 27253 RVA: 0x001B504D File Offset: 0x001B324D
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x17002102 RID: 8450
		// (get) Token: 0x06006A76 RID: 27254 RVA: 0x001B5060 File Offset: 0x001B3260
		// (set) Token: 0x06006A77 RID: 27255 RVA: 0x001B5077 File Offset: 0x001B3277
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002103 RID: 8451
		// (get) Token: 0x06006A78 RID: 27256 RVA: 0x001B508A File Offset: 0x001B328A
		// (set) Token: 0x06006A79 RID: 27257 RVA: 0x001B50A1 File Offset: 0x001B32A1
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfSenderInRecipientList
		{
			get
			{
				return (Word[])base.Fields["ExceptIfSenderInRecipientList"];
			}
			set
			{
				base.Fields["ExceptIfSenderInRecipientList"] = value;
			}
		}

		// Token: 0x17002104 RID: 8452
		// (get) Token: 0x06006A7A RID: 27258 RVA: 0x001B50B4 File Offset: 0x001B32B4
		// (set) Token: 0x06006A7B RID: 27259 RVA: 0x001B50CB File Offset: 0x001B32CB
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfRecipientInSenderList
		{
			get
			{
				return (Word[])base.Fields["ExceptIfRecipientInSenderList"];
			}
			set
			{
				base.Fields["ExceptIfRecipientInSenderList"] = value;
			}
		}

		// Token: 0x17002105 RID: 8453
		// (get) Token: 0x06006A7C RID: 27260 RVA: 0x001B50DE File Offset: 0x001B32DE
		// (set) Token: 0x06006A7D RID: 27261 RVA: 0x001B50F5 File Offset: 0x001B32F5
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAttachmentContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAttachmentContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentContainsWords"] = value;
			}
		}

		// Token: 0x17002106 RID: 8454
		// (get) Token: 0x06006A7E RID: 27262 RVA: 0x001B5108 File Offset: 0x001B3308
		// (set) Token: 0x06006A7F RID: 27263 RVA: 0x001B511F File Offset: 0x001B331F
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAttachmentMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAttachmentMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentMatchesPatterns"] = value;
			}
		}

		// Token: 0x17002107 RID: 8455
		// (get) Token: 0x06006A80 RID: 27264 RVA: 0x001B5132 File Offset: 0x001B3332
		// (set) Token: 0x06006A81 RID: 27265 RVA: 0x001B5149 File Offset: 0x001B3349
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentIsUnsupported
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentIsUnsupported"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentIsUnsupported"] = value;
			}
		}

		// Token: 0x17002108 RID: 8456
		// (get) Token: 0x06006A82 RID: 27266 RVA: 0x001B5161 File Offset: 0x001B3361
		// (set) Token: 0x06006A83 RID: 27267 RVA: 0x001B5178 File Offset: 0x001B3378
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentProcessingLimitExceeded
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentProcessingLimitExceeded"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentProcessingLimitExceeded"] = value;
			}
		}

		// Token: 0x17002109 RID: 8457
		// (get) Token: 0x06006A84 RID: 27268 RVA: 0x001B5190 File Offset: 0x001B3390
		// (set) Token: 0x06006A85 RID: 27269 RVA: 0x001B51A7 File Offset: 0x001B33A7
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentHasExecutableContent
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentHasExecutableContent"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentHasExecutableContent"] = value;
			}
		}

		// Token: 0x1700210A RID: 8458
		// (get) Token: 0x06006A86 RID: 27270 RVA: 0x001B51BF File Offset: 0x001B33BF
		// (set) Token: 0x06006A87 RID: 27271 RVA: 0x001B51D6 File Offset: 0x001B33D6
		[Parameter(Mandatory = false)]
		public bool ExceptIfAttachmentIsPasswordProtected
		{
			get
			{
				return (bool)base.Fields["ExceptIfAttachmentIsPasswordProtected"];
			}
			set
			{
				base.Fields["ExceptIfAttachmentIsPasswordProtected"] = value;
			}
		}

		// Token: 0x1700210B RID: 8459
		// (get) Token: 0x06006A88 RID: 27272 RVA: 0x001B51EE File Offset: 0x001B33EE
		// (set) Token: 0x06006A89 RID: 27273 RVA: 0x001B5205 File Offset: 0x001B3405
		[Parameter(Mandatory = false)]
		public Word[] ExceptIfAnyOfRecipientAddressContainsWords
		{
			get
			{
				return (Word[])base.Fields["ExceptIfAnyOfRecipientAddressContainsWords"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfRecipientAddressContainsWords"] = value;
			}
		}

		// Token: 0x1700210C RID: 8460
		// (get) Token: 0x06006A8A RID: 27274 RVA: 0x001B5218 File Offset: 0x001B3418
		// (set) Token: 0x06006A8B RID: 27275 RVA: 0x001B522F File Offset: 0x001B342F
		[Parameter(Mandatory = false)]
		public Pattern[] ExceptIfAnyOfRecipientAddressMatchesPatterns
		{
			get
			{
				return (Pattern[])base.Fields["ExceptIfAnyOfRecipientAddressMatchesPatterns"];
			}
			set
			{
				base.Fields["ExceptIfAnyOfRecipientAddressMatchesPatterns"] = value;
			}
		}

		// Token: 0x1700210D RID: 8461
		// (get) Token: 0x06006A8C RID: 27276 RVA: 0x001B5242 File Offset: 0x001B3442
		// (set) Token: 0x06006A8D RID: 27277 RVA: 0x001B5259 File Offset: 0x001B3459
		[Parameter(Mandatory = false)]
		public Hashtable[] ExceptIfMessageContainsDataClassifications
		{
			get
			{
				return (Hashtable[])base.Fields["ExceptIfMessageContainsDataClassifications"];
			}
			set
			{
				base.Fields["ExceptIfMessageContainsDataClassifications"] = value;
			}
		}

		// Token: 0x1700210E RID: 8462
		// (get) Token: 0x06006A8E RID: 27278 RVA: 0x001B526C File Offset: 0x001B346C
		// (set) Token: 0x06006A8F RID: 27279 RVA: 0x001B5283 File Offset: 0x001B3483
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<IPRange> ExceptIfSenderIpRanges
		{
			get
			{
				return (MultiValuedProperty<IPRange>)base.Fields["ExceptIfSenderIpRanges"];
			}
			set
			{
				base.Fields["ExceptIfSenderIpRanges"] = value;
			}
		}

		// Token: 0x1700210F RID: 8463
		// (get) Token: 0x06006A90 RID: 27280 RVA: 0x001B5296 File Offset: 0x001B3496
		// (set) Token: 0x06006A91 RID: 27281 RVA: 0x001B52AD File Offset: 0x001B34AD
		[Parameter(Mandatory = false)]
		public SubjectPrefix? PrependSubject
		{
			get
			{
				return (SubjectPrefix?)base.Fields["PrependSubject"];
			}
			set
			{
				base.Fields["PrependSubject"] = value;
			}
		}

		// Token: 0x17002110 RID: 8464
		// (get) Token: 0x06006A92 RID: 27282 RVA: 0x001B52C5 File Offset: 0x001B34C5
		// (set) Token: 0x06006A93 RID: 27283 RVA: 0x001B52DC File Offset: 0x001B34DC
		[Parameter(Mandatory = false)]
		public string SetAuditSeverity
		{
			get
			{
				return (string)base.Fields["SetAuditSeverity"];
			}
			set
			{
				base.Fields["SetAuditSeverity"] = value;
			}
		}

		// Token: 0x17002111 RID: 8465
		// (get) Token: 0x06006A94 RID: 27284 RVA: 0x001B52EF File Offset: 0x001B34EF
		// (set) Token: 0x06006A95 RID: 27285 RVA: 0x001B5306 File Offset: 0x001B3506
		[Parameter(Mandatory = false)]
		public string ApplyClassification
		{
			get
			{
				return (string)base.Fields["ApplyClassification"];
			}
			set
			{
				base.Fields["ApplyClassification"] = value;
			}
		}

		// Token: 0x17002112 RID: 8466
		// (get) Token: 0x06006A96 RID: 27286 RVA: 0x001B5319 File Offset: 0x001B3519
		// (set) Token: 0x06006A97 RID: 27287 RVA: 0x001B5330 File Offset: 0x001B3530
		[Parameter(Mandatory = false)]
		public DisclaimerLocation ApplyHtmlDisclaimerLocation
		{
			get
			{
				return (DisclaimerLocation)base.Fields["ApplyHtmlDisclaimerLocation"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerLocation"] = value;
			}
		}

		// Token: 0x17002113 RID: 8467
		// (get) Token: 0x06006A98 RID: 27288 RVA: 0x001B5348 File Offset: 0x001B3548
		// (set) Token: 0x06006A99 RID: 27289 RVA: 0x001B535F File Offset: 0x001B355F
		[Parameter(Mandatory = false)]
		public DisclaimerText ApplyHtmlDisclaimerText
		{
			get
			{
				return (DisclaimerText)base.Fields["ApplyHtmlDisclaimerText"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerText"] = value;
			}
		}

		// Token: 0x17002114 RID: 8468
		// (get) Token: 0x06006A9A RID: 27290 RVA: 0x001B5377 File Offset: 0x001B3577
		// (set) Token: 0x06006A9B RID: 27291 RVA: 0x001B538E File Offset: 0x001B358E
		[Parameter(Mandatory = false)]
		public DisclaimerFallbackAction ApplyHtmlDisclaimerFallbackAction
		{
			get
			{
				return (DisclaimerFallbackAction)base.Fields["ApplyHtmlDisclaimerFallbackAction"];
			}
			set
			{
				base.Fields["ApplyHtmlDisclaimerFallbackAction"] = value;
			}
		}

		// Token: 0x17002115 RID: 8469
		// (get) Token: 0x06006A9C RID: 27292 RVA: 0x001B53A6 File Offset: 0x001B35A6
		// (set) Token: 0x06006A9D RID: 27293 RVA: 0x001B53BD File Offset: 0x001B35BD
		[Parameter(Mandatory = false)]
		public RmsTemplateIdParameter ApplyRightsProtectionTemplate
		{
			get
			{
				return (RmsTemplateIdParameter)base.Fields["ApplyRightsProtectionTemplate"];
			}
			set
			{
				base.Fields["ApplyRightsProtectionTemplate"] = value;
			}
		}

		// Token: 0x17002116 RID: 8470
		// (get) Token: 0x06006A9E RID: 27294 RVA: 0x001B53D0 File Offset: 0x001B35D0
		// (set) Token: 0x06006A9F RID: 27295 RVA: 0x001B53E7 File Offset: 0x001B35E7
		[Parameter(Mandatory = false)]
		public SclValue SetSCL
		{
			get
			{
				return (SclValue)base.Fields["SetSCL"];
			}
			set
			{
				base.Fields["SetSCL"] = value;
			}
		}

		// Token: 0x17002117 RID: 8471
		// (get) Token: 0x06006AA0 RID: 27296 RVA: 0x001B53FF File Offset: 0x001B35FF
		// (set) Token: 0x06006AA1 RID: 27297 RVA: 0x001B5416 File Offset: 0x001B3616
		[Parameter(Mandatory = false)]
		public HeaderName SetHeaderName
		{
			get
			{
				return (HeaderName)base.Fields["SetHeaderName"];
			}
			set
			{
				base.Fields["SetHeaderName"] = value;
			}
		}

		// Token: 0x17002118 RID: 8472
		// (get) Token: 0x06006AA2 RID: 27298 RVA: 0x001B542E File Offset: 0x001B362E
		// (set) Token: 0x06006AA3 RID: 27299 RVA: 0x001B5445 File Offset: 0x001B3645
		[Parameter(Mandatory = false)]
		public HeaderValue SetHeaderValue
		{
			get
			{
				return (HeaderValue)base.Fields["SetHeaderValue"];
			}
			set
			{
				base.Fields["SetHeaderValue"] = value;
			}
		}

		// Token: 0x17002119 RID: 8473
		// (get) Token: 0x06006AA4 RID: 27300 RVA: 0x001B545D File Offset: 0x001B365D
		// (set) Token: 0x06006AA5 RID: 27301 RVA: 0x001B5474 File Offset: 0x001B3674
		[Parameter(Mandatory = false)]
		public HeaderName RemoveHeader
		{
			get
			{
				return (HeaderName)base.Fields["RemoveHeader"];
			}
			set
			{
				base.Fields["RemoveHeader"] = value;
			}
		}

		// Token: 0x1700211A RID: 8474
		// (get) Token: 0x06006AA6 RID: 27302 RVA: 0x001B548C File Offset: 0x001B368C
		// (set) Token: 0x06006AA7 RID: 27303 RVA: 0x001B54A3 File Offset: 0x001B36A3
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] AddToRecipients
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["AddToRecipients"];
			}
			set
			{
				base.Fields["AddToRecipients"] = value;
			}
		}

		// Token: 0x1700211B RID: 8475
		// (get) Token: 0x06006AA8 RID: 27304 RVA: 0x001B54B6 File Offset: 0x001B36B6
		// (set) Token: 0x06006AA9 RID: 27305 RVA: 0x001B54CD File Offset: 0x001B36CD
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] CopyTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["CopyTo"];
			}
			set
			{
				base.Fields["CopyTo"] = value;
			}
		}

		// Token: 0x1700211C RID: 8476
		// (get) Token: 0x06006AAA RID: 27306 RVA: 0x001B54E0 File Offset: 0x001B36E0
		// (set) Token: 0x06006AAB RID: 27307 RVA: 0x001B54F7 File Offset: 0x001B36F7
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] BlindCopyTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["BlindCopyTo"];
			}
			set
			{
				base.Fields["BlindCopyTo"] = value;
			}
		}

		// Token: 0x1700211D RID: 8477
		// (get) Token: 0x06006AAC RID: 27308 RVA: 0x001B550A File Offset: 0x001B370A
		// (set) Token: 0x06006AAD RID: 27309 RVA: 0x001B5521 File Offset: 0x001B3721
		[Parameter(Mandatory = false)]
		public AddedRecipientType AddManagerAsRecipientType
		{
			get
			{
				return (AddedRecipientType)base.Fields["AddManagerAsRecipientType"];
			}
			set
			{
				base.Fields["AddManagerAsRecipientType"] = value;
			}
		}

		// Token: 0x1700211E RID: 8478
		// (get) Token: 0x06006AAE RID: 27310 RVA: 0x001B5539 File Offset: 0x001B3739
		// (set) Token: 0x06006AAF RID: 27311 RVA: 0x001B5550 File Offset: 0x001B3750
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] ModerateMessageByUser
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["ModerateMessageByUser"];
			}
			set
			{
				base.Fields["ModerateMessageByUser"] = value;
			}
		}

		// Token: 0x1700211F RID: 8479
		// (get) Token: 0x06006AB0 RID: 27312 RVA: 0x001B5563 File Offset: 0x001B3763
		// (set) Token: 0x06006AB1 RID: 27313 RVA: 0x001B557A File Offset: 0x001B377A
		[Parameter(Mandatory = false)]
		public bool ModerateMessageByManager
		{
			get
			{
				return (bool)base.Fields["ModerateMessageByManager"];
			}
			set
			{
				base.Fields["ModerateMessageByManager"] = value;
			}
		}

		// Token: 0x17002120 RID: 8480
		// (get) Token: 0x06006AB2 RID: 27314 RVA: 0x001B5592 File Offset: 0x001B3792
		// (set) Token: 0x06006AB3 RID: 27315 RVA: 0x001B55A9 File Offset: 0x001B37A9
		[Parameter(Mandatory = false)]
		public RecipientIdParameter[] RedirectMessageTo
		{
			get
			{
				return (RecipientIdParameter[])base.Fields["RedirectMessageTo"];
			}
			set
			{
				base.Fields["RedirectMessageTo"] = value;
			}
		}

		// Token: 0x17002121 RID: 8481
		// (get) Token: 0x06006AB4 RID: 27316 RVA: 0x001B55BC File Offset: 0x001B37BC
		// (set) Token: 0x06006AB5 RID: 27317 RVA: 0x001B55D3 File Offset: 0x001B37D3
		[Parameter(Mandatory = false)]
		public NotifySenderType NotifySender
		{
			get
			{
				return (NotifySenderType)base.Fields["NotifySender"];
			}
			set
			{
				base.Fields["NotifySender"] = value;
			}
		}

		// Token: 0x17002122 RID: 8482
		// (get) Token: 0x06006AB6 RID: 27318 RVA: 0x001B55EB File Offset: 0x001B37EB
		// (set) Token: 0x06006AB7 RID: 27319 RVA: 0x001B5607 File Offset: 0x001B3807
		[Parameter(Mandatory = false)]
		public RejectEnhancedStatus? RejectMessageEnhancedStatusCode
		{
			get
			{
				return new RejectEnhancedStatus?((RejectEnhancedStatus)base.Fields["RejectMessageEnhancedStatusCode"]);
			}
			set
			{
				base.Fields["RejectMessageEnhancedStatusCode"] = value;
			}
		}

		// Token: 0x17002123 RID: 8483
		// (get) Token: 0x06006AB8 RID: 27320 RVA: 0x001B561F File Offset: 0x001B381F
		// (set) Token: 0x06006AB9 RID: 27321 RVA: 0x001B563B File Offset: 0x001B383B
		[Parameter(Mandatory = false)]
		public DsnText? RejectMessageReasonText
		{
			get
			{
				return new DsnText?((DsnText)base.Fields["RejectMessageReasonText"]);
			}
			set
			{
				base.Fields["RejectMessageReasonText"] = value;
			}
		}

		// Token: 0x17002124 RID: 8484
		// (get) Token: 0x06006ABA RID: 27322 RVA: 0x001B5653 File Offset: 0x001B3853
		// (set) Token: 0x06006ABB RID: 27323 RVA: 0x001B566A File Offset: 0x001B386A
		[Parameter(Mandatory = false)]
		public bool DeleteMessage
		{
			get
			{
				return (bool)base.Fields["DeleteMessage"];
			}
			set
			{
				base.Fields["DeleteMessage"] = value;
			}
		}

		// Token: 0x17002125 RID: 8485
		// (get) Token: 0x06006ABC RID: 27324 RVA: 0x001B5682 File Offset: 0x001B3882
		// (set) Token: 0x06006ABD RID: 27325 RVA: 0x001B5699 File Offset: 0x001B3899
		[Parameter(Mandatory = false)]
		public bool Disconnect
		{
			get
			{
				return (bool)base.Fields["Disconnect"];
			}
			set
			{
				base.Fields["Disconnect"] = value;
			}
		}

		// Token: 0x17002126 RID: 8486
		// (get) Token: 0x06006ABE RID: 27326 RVA: 0x001B56B1 File Offset: 0x001B38B1
		// (set) Token: 0x06006ABF RID: 27327 RVA: 0x001B56C8 File Offset: 0x001B38C8
		[Parameter(Mandatory = false)]
		public bool Quarantine
		{
			get
			{
				return (bool)base.Fields["Quarantine"];
			}
			set
			{
				base.Fields["Quarantine"] = value;
			}
		}

		// Token: 0x17002127 RID: 8487
		// (get) Token: 0x06006AC0 RID: 27328 RVA: 0x001B56E0 File Offset: 0x001B38E0
		// (set) Token: 0x06006AC1 RID: 27329 RVA: 0x001B56F7 File Offset: 0x001B38F7
		[Parameter(Mandatory = false)]
		public RejectText? SmtpRejectMessageRejectText
		{
			get
			{
				return (RejectText?)base.Fields["SmtpRejectMessageRejectText"];
			}
			set
			{
				base.Fields["SmtpRejectMessageRejectText"] = value;
			}
		}

		// Token: 0x17002128 RID: 8488
		// (get) Token: 0x06006AC2 RID: 27330 RVA: 0x001B570F File Offset: 0x001B390F
		// (set) Token: 0x06006AC3 RID: 27331 RVA: 0x001B5726 File Offset: 0x001B3926
		[Parameter(Mandatory = false)]
		public RejectStatusCode? SmtpRejectMessageRejectStatusCode
		{
			get
			{
				return (RejectStatusCode?)base.Fields["SmtpRejectMessageRejectStatusCode"];
			}
			set
			{
				base.Fields["SmtpRejectMessageRejectStatusCode"] = value;
			}
		}

		// Token: 0x17002129 RID: 8489
		// (get) Token: 0x06006AC4 RID: 27332 RVA: 0x001B573E File Offset: 0x001B393E
		// (set) Token: 0x06006AC5 RID: 27333 RVA: 0x001B5755 File Offset: 0x001B3955
		[Parameter(Mandatory = false)]
		public EventLogText? LogEventText
		{
			get
			{
				return (EventLogText?)base.Fields["LogEventText"];
			}
			set
			{
				base.Fields["LogEventText"] = value;
			}
		}

		// Token: 0x1700212A RID: 8490
		// (get) Token: 0x06006AC6 RID: 27334 RVA: 0x001B576D File Offset: 0x001B396D
		// (set) Token: 0x06006AC7 RID: 27335 RVA: 0x001B5784 File Offset: 0x001B3984
		[Parameter(Mandatory = false)]
		public bool StopRuleProcessing
		{
			get
			{
				return (bool)base.Fields["StopRuleProcessing"];
			}
			set
			{
				base.Fields["StopRuleProcessing"] = value;
			}
		}

		// Token: 0x1700212B RID: 8491
		// (get) Token: 0x06006AC8 RID: 27336 RVA: 0x001B579C File Offset: 0x001B399C
		// (set) Token: 0x06006AC9 RID: 27337 RVA: 0x001B57B3 File Offset: 0x001B39B3
		[Parameter(Mandatory = false)]
		public DateTime? ActivationDate
		{
			get
			{
				return (DateTime?)base.Fields["ActivationDate"];
			}
			set
			{
				base.Fields["ActivationDate"] = value;
			}
		}

		// Token: 0x1700212C RID: 8492
		// (get) Token: 0x06006ACA RID: 27338 RVA: 0x001B57CB File Offset: 0x001B39CB
		// (set) Token: 0x06006ACB RID: 27339 RVA: 0x001B57E2 File Offset: 0x001B39E2
		[Parameter(Mandatory = false)]
		public DateTime? ExpiryDate
		{
			get
			{
				return (DateTime?)base.Fields["ExpiryDate"];
			}
			set
			{
				base.Fields["ExpiryDate"] = value;
			}
		}

		// Token: 0x1700212D RID: 8493
		// (get) Token: 0x06006ACC RID: 27340 RVA: 0x001B57FA File Offset: 0x001B39FA
		// (set) Token: 0x06006ACD RID: 27341 RVA: 0x001B5811 File Offset: 0x001B3A11
		[Parameter(Mandatory = false)]
		public OutboundConnectorIdParameter RouteMessageOutboundConnector
		{
			get
			{
				return (OutboundConnectorIdParameter)base.Fields["RouteMessageOutboundConnector"];
			}
			set
			{
				base.Fields["RouteMessageOutboundConnector"] = value;
			}
		}

		// Token: 0x1700212E RID: 8494
		// (get) Token: 0x06006ACE RID: 27342 RVA: 0x001B5824 File Offset: 0x001B3A24
		// (set) Token: 0x06006ACF RID: 27343 RVA: 0x001B583B File Offset: 0x001B3A3B
		[Parameter(Mandatory = false)]
		public bool RouteMessageOutboundRequireTls
		{
			get
			{
				return (bool)base.Fields["RouteMessageOutboundRequireTls"];
			}
			set
			{
				base.Fields["RouteMessageOutboundRequireTls"] = value;
			}
		}

		// Token: 0x1700212F RID: 8495
		// (get) Token: 0x06006AD0 RID: 27344 RVA: 0x001B5853 File Offset: 0x001B3A53
		// (set) Token: 0x06006AD1 RID: 27345 RVA: 0x001B586A File Offset: 0x001B3A6A
		[Parameter(Mandatory = false)]
		public bool ApplyOME
		{
			get
			{
				return (bool)base.Fields["ApplyOME"];
			}
			set
			{
				base.Fields["ApplyOME"] = value;
			}
		}

		// Token: 0x17002130 RID: 8496
		// (get) Token: 0x06006AD2 RID: 27346 RVA: 0x001B5882 File Offset: 0x001B3A82
		// (set) Token: 0x06006AD3 RID: 27347 RVA: 0x001B5899 File Offset: 0x001B3A99
		[Parameter(Mandatory = false)]
		public bool RemoveOME
		{
			get
			{
				return (bool)base.Fields["RemoveOME"];
			}
			set
			{
				base.Fields["RemoveOME"] = value;
			}
		}

		// Token: 0x17002131 RID: 8497
		// (get) Token: 0x06006AD4 RID: 27348 RVA: 0x001B58B1 File Offset: 0x001B3AB1
		// (set) Token: 0x06006AD5 RID: 27349 RVA: 0x001B58C8 File Offset: 0x001B3AC8
		[Parameter(Mandatory = false)]
		public RuleSubType RuleSubType
		{
			get
			{
				return (RuleSubType)base.Fields["RuleSubType"];
			}
			set
			{
				base.Fields["RuleSubType"] = value;
			}
		}

		// Token: 0x17002132 RID: 8498
		// (get) Token: 0x06006AD6 RID: 27350 RVA: 0x001B58E0 File Offset: 0x001B3AE0
		// (set) Token: 0x06006AD7 RID: 27351 RVA: 0x001B58F7 File Offset: 0x001B3AF7
		[Parameter(Mandatory = false)]
		public RuleErrorAction RuleErrorAction
		{
			get
			{
				return (RuleErrorAction)base.Fields["RuleErrorAction"];
			}
			set
			{
				base.Fields["RuleErrorAction"] = value;
			}
		}

		// Token: 0x17002133 RID: 8499
		// (get) Token: 0x06006AD8 RID: 27352 RVA: 0x001B590F File Offset: 0x001B3B0F
		// (set) Token: 0x06006AD9 RID: 27353 RVA: 0x001B5926 File Offset: 0x001B3B26
		[Parameter(Mandatory = false)]
		public SenderAddressLocation SenderAddressLocation
		{
			get
			{
				return (SenderAddressLocation)base.Fields["SenderAddressLocation"];
			}
			set
			{
				base.Fields["SenderAddressLocation"] = value;
			}
		}

		// Token: 0x17002134 RID: 8500
		// (get) Token: 0x06006ADA RID: 27354 RVA: 0x001B593E File Offset: 0x001B3B3E
		// (set) Token: 0x06006ADB RID: 27355 RVA: 0x001B5955 File Offset: 0x001B3B55
		[Parameter(Mandatory = false)]
		public RecipientIdParameter GenerateIncidentReport
		{
			get
			{
				return (RecipientIdParameter)base.Fields["GenerateIncidentReport"];
			}
			set
			{
				base.Fields["GenerateIncidentReport"] = value;
			}
		}

		// Token: 0x17002135 RID: 8501
		// (get) Token: 0x06006ADC RID: 27356 RVA: 0x001B5968 File Offset: 0x001B3B68
		// (set) Token: 0x06006ADD RID: 27357 RVA: 0x001B5984 File Offset: 0x001B3B84
		[Parameter(Mandatory = false)]
		public IncidentReportOriginalMail? IncidentReportOriginalMail
		{
			get
			{
				return new IncidentReportOriginalMail?((IncidentReportOriginalMail)base.Fields["IncidentReportOriginalMail"]);
			}
			set
			{
				base.Fields["IncidentReportOriginalMail"] = value;
			}
		}

		// Token: 0x17002136 RID: 8502
		// (get) Token: 0x06006ADE RID: 27358 RVA: 0x001B599C File Offset: 0x001B3B9C
		// (set) Token: 0x06006ADF RID: 27359 RVA: 0x001B59B3 File Offset: 0x001B3BB3
		[Parameter(Mandatory = false)]
		public IncidentReportContent[] IncidentReportContent
		{
			get
			{
				return (IncidentReportContent[])base.Fields["IncidentReportContent"];
			}
			set
			{
				base.Fields["IncidentReportContent"] = value;
			}
		}

		// Token: 0x17002137 RID: 8503
		// (get) Token: 0x06006AE0 RID: 27360 RVA: 0x001B59C6 File Offset: 0x001B3BC6
		// (set) Token: 0x06006AE1 RID: 27361 RVA: 0x001B59DD File Offset: 0x001B3BDD
		[Parameter(Mandatory = false)]
		public DisclaimerText? GenerateNotification
		{
			get
			{
				return (DisclaimerText?)base.Fields["GenerateNotification"];
			}
			set
			{
				base.Fields["GenerateNotification"] = value;
			}
		}

		// Token: 0x06006AE2 RID: 27362 RVA: 0x001B59F8 File Offset: 0x001B3BF8
		protected override void InternalValidate()
		{
			this.DataObject = (TransportRule)this.PrepareDataObject();
			if (this.Name == null)
			{
				base.WriteError(new ArgumentException(Strings.InvalidRuleName, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			Exception exception;
			string target;
			if (!Utils.ValidateParametersForRole(base.Fields, out exception, out target))
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, target);
				return;
			}
			ArgumentException ex;
			if (!Utils.ValidateRuleComments(this.Comments, out ex))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, this.Comments);
				return;
			}
			if (!Utils.ValidateRestrictedHeaders(base.Fields, true, out ex, out target))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target);
				return;
			}
			string target2;
			if (!Utils.ValidateParameterGroups(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateMessageClassification(base.Fields, out ex, out target2, base.DataSession))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateContainsWordsPredicate(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateMatchesPatternsPredicate(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateAdAttributePredicate(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidatePropertyContainsWordsPredicates(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			Exception exception2;
			if (!Utils.ValidateRecipientIdParameters(base.Fields, base.TenantGlobalCatalogSession, new DataAccessHelper.GetDataObjectDelegate(base.GetDataObject<ADRecipient>), out exception2, out target2))
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateMessageDataClassification(base.Fields, this.ResolveCurrentOrganization(), out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateAuditSeverityLevel(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateDlpPolicy(base.DataSession, base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
			}
			if (base.Fields.IsModified("DlpPolicy"))
			{
				if (this.DlpPolicy.Equals(string.Empty))
				{
					this.DlpPolicy = null;
					this.dlpPolicyId = Guid.Empty;
				}
				else
				{
					ADComplianceProgram adcomplianceProgram = DlpUtils.GetInstalledTenantDlpPolicies(base.DataSession, this.DlpPolicy).First<ADComplianceProgram>();
					this.dlpPolicyId = adcomplianceProgram.ImmutableId;
					this.DlpPolicy = adcomplianceProgram.Name;
					Tuple<RuleState, RuleMode> tuple = DlpUtils.DlpStateToRuleState(adcomplianceProgram.State);
					if ((base.Fields.IsModified("Enabled") && this.Enabled && tuple.Item1 == RuleState.Disabled) || (!this.Enabled && tuple.Item1 == RuleState.Enabled))
					{
						base.WriteError(new ArgumentException(Strings.RuleStateParameterValueIsInconsistentWithDlpPolicyState("Enabled")), ErrorCategory.InvalidArgument, "Enabled");
						return;
					}
					if (!base.Fields.IsModified("Enabled"))
					{
						this.Enabled = (tuple.Item1 == RuleState.Enabled);
					}
					if (base.Fields.IsModified("Mode") && this.Mode != tuple.Item2)
					{
						this.WriteWarning(Strings.DlpPolicyModeIsOverridenByModeParameter(this.Mode.ToString(), tuple.Item2.ToString()));
					}
					if (!base.Fields.IsModified("Mode"))
					{
						this.ruleMode = DlpUtils.DlpStateToRuleState(adcomplianceProgram.State).Item2;
					}
				}
			}
			if (base.Fields.IsModified("Mode"))
			{
				this.ruleMode = this.Mode;
			}
			if (base.Fields.IsModified("DlpPolicy") && this.DlpPolicy != null && !Utils.ValidateRuleAndDlpPolicyStateConsistency(base.DataSession, base.Fields, this.Enabled, out ex, out target2))
			{
				base.WriteWarning(ex.Message);
			}
			if (base.Fields.IsModified("ApplyRightsProtectionTemplate") && this.ApplyRightsProtectionTemplate != null)
			{
				RmsTemplateDataProvider session = new RmsTemplateDataProvider((IConfigurationSession)base.DataSession);
				string name = (this.ApplyRightsProtectionTemplate != null) ? this.ApplyRightsProtectionTemplate.ToString() : string.Empty;
				RmsTemplatePresentation rmsTemplatePresentation = (RmsTemplatePresentation)base.GetDataObject<RmsTemplatePresentation>(this.ApplyRightsProtectionTemplate, session, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(name)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(name)));
				base.Fields["ResolvedRmsTemplateIdentity"] = rmsTemplatePresentation.Identity;
			}
			if ((base.Fields.IsModified("ApplyOME") && this.ApplyOME) || (base.Fields.IsModified("RemoveOME") && this.RemoveOME))
			{
				IRMConfiguration irmconfiguration = IRMConfiguration.Read((IConfigurationSession)base.DataSession);
				if (irmconfiguration == null || !irmconfiguration.InternalLicensingEnabled)
				{
					base.WriteError(new E4eLicensingIsDisabledExceptionCreateRule(), ErrorCategory.InvalidArgument, null);
				}
				if (RmsClientManager.IRMConfig.GetRmsTemplate(base.CurrentOrganizationId, RmsTemplate.InternetConfidential.Id) == null)
				{
					base.WriteError(new E4eRuleRmsTemplateNotFoundException(RmsTemplate.InternetConfidential.Name), ErrorCategory.InvalidArgument, null);
				}
			}
			if (base.Fields.IsModified("ApplyClassification") && string.IsNullOrWhiteSpace(this.ApplyClassification))
			{
				base.WriteError(new ArgumentException(Strings.ErrorClassificationNameCannotBeBlank("ApplyClassification")), ErrorCategory.InvalidArgument, null);
			}
			Utils.ValidateGenerateIncidentReportParameters(this, this.Name);
			if (base.Fields.IsModified("IncidentReportOriginalMail"))
			{
				this.WriteWarning(Strings.LegacyIncludeOriginalMailParameterWillBeUpgraded);
			}
			bool useLegacyRegex = false;
			if (base.Fields.IsModified("UseLegacyRegex"))
			{
				useLegacyRegex = (bool)base.Fields["UseLegacyRegex"];
			}
			if (base.Fields.IsModified("RedirectMessageTo") && this.RedirectMessageTo != null)
			{
				int num;
				this.RedirectMessageTo = Utils.RemoveDuplicateRecipients(this.RedirectMessageTo, out num);
				if (num > 0)
				{
					this.WriteWarning(Strings.RemovedDuplicateRecipients(num, "RedirectMessageTo"));
				}
			}
			if (!Utils.ValidateConnectorParameter(base.Fields, base.DataSession, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateSentToScope(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateDomainIsPredicates(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			if (!Utils.ValidateAttachmentExtensionMatchesWordParameter(base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
				return;
			}
			TransportRulePredicate[] array;
			TransportRulePredicate[] array2;
			TransportRuleAction[] array3;
			try
			{
				List<Type> list;
				Utils.BuildConditionsAndExceptionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, useLegacyRegex, out list, out list, out array, out array2);
				array3 = Utils.BuildActionsFromParameters(base.Fields, base.TenantGlobalCatalogSession, base.DataSession, out list);
			}
			catch (TransientException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			catch (DataValidationException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			catch (ArgumentException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			if (array.Length > 0)
			{
				this.Conditions = array;
			}
			if (array2.Length > 0)
			{
				this.Exceptions = array2;
			}
			if (array3.Length > 0)
			{
				this.Actions = array3;
			}
			if (!base.Fields.IsModified("RuleErrorAction"))
			{
				this.RuleErrorAction = RuleErrorAction.Ignore;
			}
			if (!base.Fields.IsModified("SenderAddressLocation"))
			{
				this.SenderAddressLocation = SenderAddressLocation.Header;
			}
			if (!base.Fields.IsModified("RuleSubType"))
			{
				this.RuleSubType = RuleSubType.None;
			}
			ArgumentException exception6;
			if (!Utils.ValidateSubtypes(this.RuleSubType, this.Conditions, this.Exceptions, this.Actions, out exception6))
			{
				base.WriteError(exception6, ErrorCategory.InvalidArgument, this.Name);
			}
			if (!Utils.ValidateNotifySender(this.Conditions, this.Exceptions, this.Actions, new Action<LocalizedString>(this.WriteWarning), out exception6))
			{
				base.WriteError(exception6, ErrorCategory.InvalidArgument, this.Name);
			}
			if (Utils.IsNotifySenderIgnoringRejectParameters(base.Fields))
			{
				this.RejectMessageEnhancedStatusCode = null;
				this.RejectMessageReasonText = null;
				this.WriteWarning(Strings.RejectMessageParameterWillBeIgnored);
			}
			if (this.Actions != null)
			{
				bool flag = Utils.ValidateSingletonAction(this.Actions);
				foreach (TransportRuleAction transportRuleAction in this.Actions)
				{
					if (transportRuleAction is RejectMessageAction)
					{
						RejectMessageAction rejectMessageAction = (RejectMessageAction)transportRuleAction;
						string text = rejectMessageAction.EnhancedStatusCode.ToString();
						if (!Utils.IsCustomizedDsnConfigured(text))
						{
							this.WriteWarning(Strings.CustomizedDsnNotConfigured(text));
						}
					}
					if (transportRuleAction is ApplyHtmlDisclaimerAction)
					{
						ApplyHtmlDisclaimerAction applyHtmlDisclaimerAction = (ApplyHtmlDisclaimerAction)transportRuleAction;
						string disclaimerText = applyHtmlDisclaimerAction.Text.ToString();
						string text2 = TransportUtils.CheckForInvalidMacroName(disclaimerText);
						if (!string.IsNullOrEmpty(text2))
						{
							base.WriteError(new ArgumentException(Strings.InvalidDisclaimerMacroName(text2)), ErrorCategory.InvalidArgument, this.Name);
							return;
						}
					}
					if (transportRuleAction is RightsProtectMessageAction)
					{
						RmsTemplateDataProvider session2 = new RmsTemplateDataProvider((IConfigurationSession)base.DataSession);
						RightsProtectMessageAction rightsProtectMessageAction = (RightsProtectMessageAction)transportRuleAction;
						RmsTemplateIdentity template = rightsProtectMessageAction.Template;
						base.GetDataObject<RmsTemplatePresentation>(new RmsTemplateIdParameter(template), session2, null, new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotFound(template.TemplateName)), new LocalizedString?(Strings.OutlookProtectionRuleRmsTemplateNotUnique(template.TemplateName)));
						base.Fields["ResolvedRmsTemplateIdentity"] = template;
					}
					if (!flag && Utils.ActionWhichMustBeSingleton.ContainsKey(transportRuleAction.GetType()))
					{
						base.WriteError(new ArgumentException(Utils.ActionWhichMustBeSingleton[transportRuleAction.GetType()]), ErrorCategory.InvalidArgument, this.Name);
						return;
					}
				}
			}
			Utils.ValidateTransportRuleRegexCpuTimeLimit((IConfigurationSession)base.DataSession, base.Fields);
			if (!Utils.ValidateActivationAndExpiryDates(new Action<LocalizedString>(this.WriteWarning), null, base.Fields, out ex, out target2))
			{
				base.WriteError(ex, ErrorCategory.InvalidArgument, target2);
			}
		}

		// Token: 0x06006AE3 RID: 27363 RVA: 0x001B6384 File Offset: 0x001B4584
		protected override void InternalProcessRecord()
		{
			Rule taskRuleRepresentation = this.GetTaskRuleRepresentation();
			TransportRule transportRule;
			try
			{
				transportRule = taskRuleRepresentation.ToInternalRule();
			}
			catch (ArgumentException exception)
			{
				base.WriteError(exception, ErrorCategory.InvalidArgument, null);
				return;
			}
			catch (RulesValidationException exception2)
			{
				base.WriteError(exception2, ErrorCategory.InvalidArgument, null);
				return;
			}
			int priority = base.Fields.IsModified("Priority") ? taskRuleRepresentation.Priority : -1;
			ADRuleStorageManager adruleStorageManager;
			try
			{
				IConfigDataProvider session = new MessagingPoliciesSyncLogDataSession(base.DataSession, null, null);
				adruleStorageManager = new ADRuleStorageManager(this.ruleCollectionName, session);
			}
			catch (RuleCollectionNotInAdException exception3)
			{
				base.WriteError(exception3, ErrorCategory.InvalidOperation, null);
				return;
			}
			TransportRule transportRule2;
			try
			{
				OrganizationId organizationId = this.ResolveCurrentOrganization();
				if (organizationId != OrganizationId.ForestWideOrgId)
				{
					adruleStorageManager.LoadRuleCollection();
					InvalidOperationException ex = Utils.CheckRuleForOrganizationLimits((IConfigurationSession)base.DataSession, base.TenantGlobalCatalogSession, adruleStorageManager, organizationId, transportRule, true);
					if (ex != null)
					{
						base.WriteError(ex, ErrorCategory.InvalidOperation, this.Name);
						return;
					}
				}
				adruleStorageManager.NewRule(transportRule, organizationId, ref priority, out transportRule2);
			}
			catch (RulesValidationException)
			{
				base.WriteError(new ArgumentException(Strings.RuleNameAlreadyExist, "Name"), ErrorCategory.InvalidArgument, this.Name);
				return;
			}
			catch (InvalidPriorityException exception4)
			{
				base.WriteError(exception4, ErrorCategory.InvalidArgument, null);
				return;
			}
			catch (ParserException exception5)
			{
				base.WriteError(exception5, ErrorCategory.InvalidData, null);
				return;
			}
			if (Utils.Exchange12HubServersExist(this))
			{
				this.WriteWarning(Strings.NewRuleSyncAcrossDifferentVersionsNeeded);
			}
			taskRuleRepresentation.Priority = priority;
			taskRuleRepresentation.SetTransportRule(transportRule2);
			base.WriteObject(taskRuleRepresentation);
		}

		// Token: 0x06006AE4 RID: 27364 RVA: 0x001B6524 File Offset: 0x001B4724
		internal Rule GetTaskRuleRepresentation()
		{
			RuleState state = this.Enabled ? RuleState.Enabled : RuleState.Disabled;
			return new Rule(null, this.Name, this.Priority, this.DlpPolicy, this.dlpPolicyId, this.Comments, false, (this.ActivationDate != null && this.ActivationDate.Value.ToUniversalTime() < DateTime.UtcNow) ? null : this.ActivationDate, this.ExpiryDate, this.Conditions, this.Exceptions, this.Actions, state, this.ruleMode, this.RuleSubType, this.RuleErrorAction, this.SenderAddressLocation);
		}

		// Token: 0x06006AE5 RID: 27365 RVA: 0x001B65D8 File Offset: 0x001B47D8
		protected override bool IsKnownException(Exception exception)
		{
			return base.IsKnownException(exception) || RmsUtil.IsKnownException(exception) || exception is ValidationArgumentException;
		}

		// Token: 0x040036D2 RID: 14034
		private readonly string ruleCollectionName;

		// Token: 0x040036D3 RID: 14035
		private RuleMode ruleMode;

		// Token: 0x040036D4 RID: 14036
		private Guid dlpPolicyId;
	}
}
