using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000474 RID: 1140
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public class HostedOutboundSpamFilterPolicy : ADConfigurationObject
	{
		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x060032CC RID: 13004 RVA: 0x000CD3B2 File Offset: 0x000CB5B2
		internal override ADObjectSchema Schema
		{
			get
			{
				return HostedOutboundSpamFilterPolicy.schema;
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x060032CD RID: 13005 RVA: 0x000CD3B9 File Offset: 0x000CB5B9
		internal override ADObjectId ParentPath
		{
			get
			{
				return HostedOutboundSpamFilterPolicy.parentPath;
			}
		}

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060032CE RID: 13006 RVA: 0x000CD3C0 File Offset: 0x000CB5C0
		internal override string MostDerivedObjectClass
		{
			get
			{
				return HostedOutboundSpamFilterPolicy.ldapName;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060032CF RID: 13007 RVA: 0x000CD3C7 File Offset: 0x000CB5C7
		internal override ExchangeObjectVersion MaximumSupportedExchangeObjectVersion
		{
			get
			{
				return ExchangeObjectVersion.Exchange2012;
			}
		}

		// Token: 0x060032D0 RID: 13008 RVA: 0x000CD3D0 File Offset: 0x000CB5D0
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.NotifyOutboundSpam && (this.NotifyOutboundSpamRecipients == null || this.NotifyOutboundSpamRecipients.Count == 0))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.NotifyOutboundSpamRecipientsRequired, HostedOutboundSpamFilterPolicySchema.NotifyOutboundSpamRecipients, this.NotifyOutboundSpamRecipients));
			}
			if (this.BccSuspiciousOutboundMail && (this.BccSuspiciousOutboundAdditionalRecipients == null || this.BccSuspiciousOutboundAdditionalRecipients.Count == 0))
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.BccSuspiciousOutboundAdditionalRecipientsRequired, HostedOutboundSpamFilterPolicySchema.BccSuspiciousOutboundAdditionalRecipients, this.BccSuspiciousOutboundAdditionalRecipients));
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060032D1 RID: 13009 RVA: 0x000CD454 File Offset: 0x000CB654
		// (set) Token: 0x060032D2 RID: 13010 RVA: 0x000CD466 File Offset: 0x000CB666
		[Parameter]
		public new string AdminDisplayName
		{
			get
			{
				return (string)this[ADConfigurationObjectSchema.AdminDisplayName];
			}
			set
			{
				this[ADConfigurationObjectSchema.AdminDisplayName] = value;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x060032D3 RID: 13011 RVA: 0x000CD474 File Offset: 0x000CB674
		// (set) Token: 0x060032D4 RID: 13012 RVA: 0x000CD486 File Offset: 0x000CB686
		[Parameter]
		public MultiValuedProperty<SmtpAddress> NotifyOutboundSpamRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedOutboundSpamFilterPolicySchema.NotifyOutboundSpamRecipients];
			}
			set
			{
				this[HostedOutboundSpamFilterPolicySchema.NotifyOutboundSpamRecipients] = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x060032D5 RID: 13013 RVA: 0x000CD494 File Offset: 0x000CB694
		// (set) Token: 0x060032D6 RID: 13014 RVA: 0x000CD4A6 File Offset: 0x000CB6A6
		[Parameter]
		public MultiValuedProperty<SmtpAddress> BccSuspiciousOutboundAdditionalRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[HostedOutboundSpamFilterPolicySchema.BccSuspiciousOutboundAdditionalRecipients];
			}
			set
			{
				this[HostedOutboundSpamFilterPolicySchema.BccSuspiciousOutboundAdditionalRecipients] = value;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x060032D7 RID: 13015 RVA: 0x000CD4B4 File Offset: 0x000CB6B4
		// (set) Token: 0x060032D8 RID: 13016 RVA: 0x000CD4C6 File Offset: 0x000CB6C6
		[Parameter]
		public bool BccSuspiciousOutboundMail
		{
			get
			{
				return (bool)this[HostedOutboundSpamFilterPolicySchema.BccSuspiciousOutboundMail];
			}
			set
			{
				this[HostedOutboundSpamFilterPolicySchema.BccSuspiciousOutboundMail] = value;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x000CD4D9 File Offset: 0x000CB6D9
		// (set) Token: 0x060032DA RID: 13018 RVA: 0x000CD4EB File Offset: 0x000CB6EB
		[Parameter]
		public bool NotifyOutboundSpam
		{
			get
			{
				return (bool)this[HostedOutboundSpamFilterPolicySchema.NotifyOutboundSpam];
			}
			set
			{
				this[HostedOutboundSpamFilterPolicySchema.NotifyOutboundSpam] = value;
			}
		}

		// Token: 0x0400233B RID: 9019
		private static readonly string ldapName = "msExchHygieneConfiguration";

		// Token: 0x0400233C RID: 9020
		private static readonly ADObjectId parentPath = new ADObjectId("CN=Outbound Spam Filter,CN=Transport Settings");

		// Token: 0x0400233D RID: 9021
		private static readonly HostedOutboundSpamFilterPolicySchema schema = ObjectSchema.GetInstance<HostedOutboundSpamFilterPolicySchema>();
	}
}
