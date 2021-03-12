using System;
using System.Management.Automation;
using Microsoft.Exchange.Data.Directory.Provisioning;
using Microsoft.Exchange.Data.Directory.Recipient;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FB RID: 1787
	[ProvisioningObjectTag("DynamicDistributionGroup")]
	[Serializable]
	public class DynamicDistributionGroup : DistributionGroupBase, ISupportRecipientFilter
	{
		// Token: 0x17001BCC RID: 7116
		// (get) Token: 0x060053F7 RID: 21495 RVA: 0x001315F7 File Offset: 0x0012F7F7
		internal override ADPresentationSchema PresentationSchema
		{
			get
			{
				return DynamicDistributionGroup.schema;
			}
		}

		// Token: 0x060053F8 RID: 21496 RVA: 0x001315FE File Offset: 0x0012F7FE
		public DynamicDistributionGroup()
		{
		}

		// Token: 0x060053F9 RID: 21497 RVA: 0x00131606 File Offset: 0x0012F806
		public DynamicDistributionGroup(ADDynamicGroup dataObject) : base(dataObject)
		{
		}

		// Token: 0x060053FA RID: 21498 RVA: 0x0013160F File Offset: 0x0012F80F
		internal static DynamicDistributionGroup FromDataObject(ADDynamicGroup dataObject)
		{
			if (dataObject == null)
			{
				return null;
			}
			return new DynamicDistributionGroup(dataObject);
		}

		// Token: 0x17001BCD RID: 7117
		// (get) Token: 0x060053FB RID: 21499 RVA: 0x0013161C File Offset: 0x0012F81C
		// (set) Token: 0x060053FC RID: 21500 RVA: 0x0013162E File Offset: 0x0012F82E
		public ADObjectId RecipientContainer
		{
			get
			{
				return (ADObjectId)this[DynamicDistributionGroupSchema.RecipientContainer];
			}
			set
			{
				this[DynamicDistributionGroupSchema.RecipientContainer] = value;
			}
		}

		// Token: 0x17001BCE RID: 7118
		// (get) Token: 0x060053FD RID: 21501 RVA: 0x0013163C File Offset: 0x0012F83C
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2010;
			}
		}

		// Token: 0x17001BCF RID: 7119
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x00131643 File Offset: 0x0012F843
		public string RecipientFilter
		{
			get
			{
				return (string)this[DynamicDistributionGroupSchema.RecipientFilter];
			}
		}

		// Token: 0x17001BD0 RID: 7120
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x00131655 File Offset: 0x0012F855
		public string LdapRecipientFilter
		{
			get
			{
				return (string)this[DynamicDistributionGroupSchema.LdapRecipientFilter];
			}
		}

		// Token: 0x17001BD1 RID: 7121
		// (get) Token: 0x06005400 RID: 21504 RVA: 0x00131667 File Offset: 0x0012F867
		// (set) Token: 0x06005401 RID: 21505 RVA: 0x00131679 File Offset: 0x0012F879
		[Parameter]
		public WellKnownRecipientType? IncludedRecipients
		{
			get
			{
				return (WellKnownRecipientType?)this[DynamicDistributionGroupSchema.IncludedRecipients];
			}
			set
			{
				this[DynamicDistributionGroupSchema.IncludedRecipients] = value;
			}
		}

		// Token: 0x17001BD2 RID: 7122
		// (get) Token: 0x06005402 RID: 21506 RVA: 0x0013168C File Offset: 0x0012F88C
		// (set) Token: 0x06005403 RID: 21507 RVA: 0x0013169E File Offset: 0x0012F89E
		[Parameter]
		public MultiValuedProperty<string> ConditionalDepartment
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalDepartment];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalDepartment] = value;
			}
		}

		// Token: 0x17001BD3 RID: 7123
		// (get) Token: 0x06005404 RID: 21508 RVA: 0x001316AC File Offset: 0x0012F8AC
		// (set) Token: 0x06005405 RID: 21509 RVA: 0x001316BE File Offset: 0x0012F8BE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCompany
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCompany];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCompany] = value;
			}
		}

		// Token: 0x17001BD4 RID: 7124
		// (get) Token: 0x06005406 RID: 21510 RVA: 0x001316CC File Offset: 0x0012F8CC
		// (set) Token: 0x06005407 RID: 21511 RVA: 0x001316DE File Offset: 0x0012F8DE
		[Parameter]
		public MultiValuedProperty<string> ConditionalStateOrProvince
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalStateOrProvince];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalStateOrProvince] = value;
			}
		}

		// Token: 0x17001BD5 RID: 7125
		// (get) Token: 0x06005408 RID: 21512 RVA: 0x001316EC File Offset: 0x0012F8EC
		// (set) Token: 0x06005409 RID: 21513 RVA: 0x001316FE File Offset: 0x0012F8FE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute1
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute1];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute1] = value;
			}
		}

		// Token: 0x17001BD6 RID: 7126
		// (get) Token: 0x0600540A RID: 21514 RVA: 0x0013170C File Offset: 0x0012F90C
		// (set) Token: 0x0600540B RID: 21515 RVA: 0x0013171E File Offset: 0x0012F91E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute2
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute2];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute2] = value;
			}
		}

		// Token: 0x17001BD7 RID: 7127
		// (get) Token: 0x0600540C RID: 21516 RVA: 0x0013172C File Offset: 0x0012F92C
		// (set) Token: 0x0600540D RID: 21517 RVA: 0x0013173E File Offset: 0x0012F93E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute3
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute3];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute3] = value;
			}
		}

		// Token: 0x17001BD8 RID: 7128
		// (get) Token: 0x0600540E RID: 21518 RVA: 0x0013174C File Offset: 0x0012F94C
		// (set) Token: 0x0600540F RID: 21519 RVA: 0x0013175E File Offset: 0x0012F95E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute4
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute4];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute4] = value;
			}
		}

		// Token: 0x17001BD9 RID: 7129
		// (get) Token: 0x06005410 RID: 21520 RVA: 0x0013176C File Offset: 0x0012F96C
		// (set) Token: 0x06005411 RID: 21521 RVA: 0x0013177E File Offset: 0x0012F97E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute5
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute5];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute5] = value;
			}
		}

		// Token: 0x17001BDA RID: 7130
		// (get) Token: 0x06005412 RID: 21522 RVA: 0x0013178C File Offset: 0x0012F98C
		// (set) Token: 0x06005413 RID: 21523 RVA: 0x0013179E File Offset: 0x0012F99E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute6
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute6];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute6] = value;
			}
		}

		// Token: 0x17001BDB RID: 7131
		// (get) Token: 0x06005414 RID: 21524 RVA: 0x001317AC File Offset: 0x0012F9AC
		// (set) Token: 0x06005415 RID: 21525 RVA: 0x001317BE File Offset: 0x0012F9BE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute7
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute7];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute7] = value;
			}
		}

		// Token: 0x17001BDC RID: 7132
		// (get) Token: 0x06005416 RID: 21526 RVA: 0x001317CC File Offset: 0x0012F9CC
		// (set) Token: 0x06005417 RID: 21527 RVA: 0x001317DE File Offset: 0x0012F9DE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute8
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute8];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute8] = value;
			}
		}

		// Token: 0x17001BDD RID: 7133
		// (get) Token: 0x06005418 RID: 21528 RVA: 0x001317EC File Offset: 0x0012F9EC
		// (set) Token: 0x06005419 RID: 21529 RVA: 0x001317FE File Offset: 0x0012F9FE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute9
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute9];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute9] = value;
			}
		}

		// Token: 0x17001BDE RID: 7134
		// (get) Token: 0x0600541A RID: 21530 RVA: 0x0013180C File Offset: 0x0012FA0C
		// (set) Token: 0x0600541B RID: 21531 RVA: 0x0013181E File Offset: 0x0012FA1E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute10
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute10];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute10] = value;
			}
		}

		// Token: 0x17001BDF RID: 7135
		// (get) Token: 0x0600541C RID: 21532 RVA: 0x0013182C File Offset: 0x0012FA2C
		// (set) Token: 0x0600541D RID: 21533 RVA: 0x0013183E File Offset: 0x0012FA3E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute11
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute11];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute11] = value;
			}
		}

		// Token: 0x17001BE0 RID: 7136
		// (get) Token: 0x0600541E RID: 21534 RVA: 0x0013184C File Offset: 0x0012FA4C
		// (set) Token: 0x0600541F RID: 21535 RVA: 0x0013185E File Offset: 0x0012FA5E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute12
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute12];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute12] = value;
			}
		}

		// Token: 0x17001BE1 RID: 7137
		// (get) Token: 0x06005420 RID: 21536 RVA: 0x0013186C File Offset: 0x0012FA6C
		// (set) Token: 0x06005421 RID: 21537 RVA: 0x0013187E File Offset: 0x0012FA7E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute13
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute13];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute13] = value;
			}
		}

		// Token: 0x17001BE2 RID: 7138
		// (get) Token: 0x06005422 RID: 21538 RVA: 0x0013188C File Offset: 0x0012FA8C
		// (set) Token: 0x06005423 RID: 21539 RVA: 0x0013189E File Offset: 0x0012FA9E
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute14
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute14];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute14] = value;
			}
		}

		// Token: 0x17001BE3 RID: 7139
		// (get) Token: 0x06005424 RID: 21540 RVA: 0x001318AC File Offset: 0x0012FAAC
		// (set) Token: 0x06005425 RID: 21541 RVA: 0x001318BE File Offset: 0x0012FABE
		[Parameter]
		public MultiValuedProperty<string> ConditionalCustomAttribute15
		{
			get
			{
				return (MultiValuedProperty<string>)this[DynamicDistributionGroupSchema.ConditionalCustomAttribute15];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ConditionalCustomAttribute15] = value;
			}
		}

		// Token: 0x17001BE4 RID: 7140
		// (get) Token: 0x06005426 RID: 21542 RVA: 0x001318CC File Offset: 0x0012FACC
		public WellKnownRecipientFilterType RecipientFilterType
		{
			get
			{
				return (WellKnownRecipientFilterType)this[DynamicDistributionGroupSchema.RecipientFilterType];
			}
		}

		// Token: 0x17001BE5 RID: 7141
		// (get) Token: 0x06005427 RID: 21543 RVA: 0x001318DE File Offset: 0x0012FADE
		// (set) Token: 0x06005428 RID: 21544 RVA: 0x001318F0 File Offset: 0x0012FAF0
		[Parameter(Mandatory = false)]
		public string Notes
		{
			get
			{
				return (string)this[DynamicDistributionGroupSchema.Notes];
			}
			set
			{
				this[DynamicDistributionGroupSchema.Notes] = value;
			}
		}

		// Token: 0x17001BE6 RID: 7142
		// (get) Token: 0x06005429 RID: 21545 RVA: 0x001318FE File Offset: 0x0012FAFE
		// (set) Token: 0x0600542A RID: 21546 RVA: 0x00131910 File Offset: 0x0012FB10
		[Parameter(Mandatory = false)]
		public string PhoneticDisplayName
		{
			get
			{
				return (string)this[DynamicDistributionGroupSchema.PhoneticDisplayName];
			}
			set
			{
				this[DynamicDistributionGroupSchema.PhoneticDisplayName] = value;
			}
		}

		// Token: 0x17001BE7 RID: 7143
		// (get) Token: 0x0600542B RID: 21547 RVA: 0x0013191E File Offset: 0x0012FB1E
		// (set) Token: 0x0600542C RID: 21548 RVA: 0x00131930 File Offset: 0x0012FB30
		public ADObjectId ManagedBy
		{
			get
			{
				return (ADObjectId)this[DynamicDistributionGroupSchema.ManagedBy];
			}
			set
			{
				this[DynamicDistributionGroupSchema.ManagedBy] = value;
			}
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x0600542D RID: 21549 RVA: 0x0013193E File Offset: 0x0012FB3E
		ADPropertyDefinition ISupportRecipientFilter.RecipientFilterSchema
		{
			get
			{
				return DynamicDistributionGroupSchema.RecipientFilter;
			}
		}

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x0600542E RID: 21550 RVA: 0x00131945 File Offset: 0x0012FB45
		ADPropertyDefinition ISupportRecipientFilter.LdapRecipientFilterSchema
		{
			get
			{
				return DynamicDistributionGroupSchema.LdapRecipientFilter;
			}
		}

		// Token: 0x17001BEA RID: 7146
		// (get) Token: 0x0600542F RID: 21551 RVA: 0x0013194C File Offset: 0x0012FB4C
		ADPropertyDefinition ISupportRecipientFilter.IncludedRecipientsSchema
		{
			get
			{
				return DynamicDistributionGroupSchema.IncludedRecipients;
			}
		}

		// Token: 0x17001BEB RID: 7147
		// (get) Token: 0x06005430 RID: 21552 RVA: 0x00131953 File Offset: 0x0012FB53
		ADPropertyDefinition ISupportRecipientFilter.ConditionalDepartmentSchema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalDepartment;
			}
		}

		// Token: 0x17001BEC RID: 7148
		// (get) Token: 0x06005431 RID: 21553 RVA: 0x0013195A File Offset: 0x0012FB5A
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCompanySchema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCompany;
			}
		}

		// Token: 0x17001BED RID: 7149
		// (get) Token: 0x06005432 RID: 21554 RVA: 0x00131961 File Offset: 0x0012FB61
		ADPropertyDefinition ISupportRecipientFilter.ConditionalStateOrProvinceSchema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalStateOrProvince;
			}
		}

		// Token: 0x17001BEE RID: 7150
		// (get) Token: 0x06005433 RID: 21555 RVA: 0x00131968 File Offset: 0x0012FB68
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute1Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute1;
			}
		}

		// Token: 0x17001BEF RID: 7151
		// (get) Token: 0x06005434 RID: 21556 RVA: 0x0013196F File Offset: 0x0012FB6F
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute2Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute2;
			}
		}

		// Token: 0x17001BF0 RID: 7152
		// (get) Token: 0x06005435 RID: 21557 RVA: 0x00131976 File Offset: 0x0012FB76
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute3Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute3;
			}
		}

		// Token: 0x17001BF1 RID: 7153
		// (get) Token: 0x06005436 RID: 21558 RVA: 0x0013197D File Offset: 0x0012FB7D
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute4Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute4;
			}
		}

		// Token: 0x17001BF2 RID: 7154
		// (get) Token: 0x06005437 RID: 21559 RVA: 0x00131984 File Offset: 0x0012FB84
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute5Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute5;
			}
		}

		// Token: 0x17001BF3 RID: 7155
		// (get) Token: 0x06005438 RID: 21560 RVA: 0x0013198B File Offset: 0x0012FB8B
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute6Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute6;
			}
		}

		// Token: 0x17001BF4 RID: 7156
		// (get) Token: 0x06005439 RID: 21561 RVA: 0x00131992 File Offset: 0x0012FB92
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute7Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute7;
			}
		}

		// Token: 0x17001BF5 RID: 7157
		// (get) Token: 0x0600543A RID: 21562 RVA: 0x00131999 File Offset: 0x0012FB99
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute8Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute8;
			}
		}

		// Token: 0x17001BF6 RID: 7158
		// (get) Token: 0x0600543B RID: 21563 RVA: 0x001319A0 File Offset: 0x0012FBA0
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute9Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute9;
			}
		}

		// Token: 0x17001BF7 RID: 7159
		// (get) Token: 0x0600543C RID: 21564 RVA: 0x001319A7 File Offset: 0x0012FBA7
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute10Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute10;
			}
		}

		// Token: 0x17001BF8 RID: 7160
		// (get) Token: 0x0600543D RID: 21565 RVA: 0x001319AE File Offset: 0x0012FBAE
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute11Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute11;
			}
		}

		// Token: 0x17001BF9 RID: 7161
		// (get) Token: 0x0600543E RID: 21566 RVA: 0x001319B5 File Offset: 0x0012FBB5
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute12Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute12;
			}
		}

		// Token: 0x17001BFA RID: 7162
		// (get) Token: 0x0600543F RID: 21567 RVA: 0x001319BC File Offset: 0x0012FBBC
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute13Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute13;
			}
		}

		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x06005440 RID: 21568 RVA: 0x001319C3 File Offset: 0x0012FBC3
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute14Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute14;
			}
		}

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x06005441 RID: 21569 RVA: 0x001319CA File Offset: 0x0012FBCA
		ADPropertyDefinition ISupportRecipientFilter.ConditionalCustomAttribute15Schema
		{
			get
			{
				return DynamicDistributionGroupSchema.ConditionalCustomAttribute15;
			}
		}

		// Token: 0x04003899 RID: 14489
		private static DynamicDistributionGroupSchema schema = ObjectSchema.GetInstance<DynamicDistributionGroupSchema>();
	}
}
