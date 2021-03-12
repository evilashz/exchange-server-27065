using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004D6 RID: 1238
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class SenderFilterConfig : MessageHygieneAgentConfig
	{
		// Token: 0x17001100 RID: 4352
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000D8F92 File Offset: 0x000D7192
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001101 RID: 4353
		// (get) Token: 0x060037AD RID: 14253 RVA: 0x000D8F9A File Offset: 0x000D719A
		internal override ADObjectSchema Schema
		{
			get
			{
				return SenderFilterConfig.schema;
			}
		}

		// Token: 0x17001102 RID: 4354
		// (get) Token: 0x060037AE RID: 14254 RVA: 0x000D8FA1 File Offset: 0x000D71A1
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneSenderFilterConfig";
			}
		}

		// Token: 0x17001103 RID: 4355
		// (get) Token: 0x060037AF RID: 14255 RVA: 0x000D8FA8 File Offset: 0x000D71A8
		// (set) Token: 0x060037B0 RID: 14256 RVA: 0x000D8FBA File Offset: 0x000D71BA
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpAddress> BlockedSenders
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[SenderFilterConfigSchema.BlockedSenders];
			}
			set
			{
				this[SenderFilterConfigSchema.BlockedSenders] = value;
			}
		}

		// Token: 0x17001104 RID: 4356
		// (get) Token: 0x060037B1 RID: 14257 RVA: 0x000D8FC8 File Offset: 0x000D71C8
		// (set) Token: 0x060037B2 RID: 14258 RVA: 0x000D8FDA File Offset: 0x000D71DA
		[ValidateCount(0, 800)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpDomain> BlockedDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[SenderFilterConfigSchema.BlockedDomains];
			}
			set
			{
				this[SenderFilterConfigSchema.BlockedDomains] = value;
			}
		}

		// Token: 0x17001105 RID: 4357
		// (get) Token: 0x060037B3 RID: 14259 RVA: 0x000D8FE8 File Offset: 0x000D71E8
		// (set) Token: 0x060037B4 RID: 14260 RVA: 0x000D8FFA File Offset: 0x000D71FA
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpDomain> BlockedDomainsAndSubdomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[SenderFilterConfigSchema.BlockedDomainAndSubdomains];
			}
			set
			{
				this[SenderFilterConfigSchema.BlockedDomainAndSubdomains] = value;
			}
		}

		// Token: 0x17001106 RID: 4358
		// (get) Token: 0x060037B5 RID: 14261 RVA: 0x000D9008 File Offset: 0x000D7208
		// (set) Token: 0x060037B6 RID: 14262 RVA: 0x000D901A File Offset: 0x000D721A
		[Parameter(Mandatory = false)]
		public BlockedSenderAction Action
		{
			get
			{
				return (BlockedSenderAction)this[SenderFilterConfigSchema.BlockedSenderAction];
			}
			set
			{
				this[SenderFilterConfigSchema.BlockedSenderAction] = value;
			}
		}

		// Token: 0x17001107 RID: 4359
		// (get) Token: 0x060037B7 RID: 14263 RVA: 0x000D902D File Offset: 0x000D722D
		// (set) Token: 0x060037B8 RID: 14264 RVA: 0x000D903F File Offset: 0x000D723F
		[Parameter(Mandatory = false)]
		public bool BlankSenderBlockingEnabled
		{
			get
			{
				return (bool)this[SenderFilterConfigSchema.BlockBlankSenders];
			}
			set
			{
				this[SenderFilterConfigSchema.BlockBlankSenders] = value;
			}
		}

		// Token: 0x17001108 RID: 4360
		// (get) Token: 0x060037B9 RID: 14265 RVA: 0x000D9052 File Offset: 0x000D7252
		// (set) Token: 0x060037BA RID: 14266 RVA: 0x000D9064 File Offset: 0x000D7264
		[Parameter(Mandatory = false)]
		public RecipientBlockedSenderAction RecipientBlockedSenderAction
		{
			get
			{
				return (RecipientBlockedSenderAction)this[SenderFilterConfigSchema.RecipientBlockedSenderAction];
			}
			set
			{
				this[SenderFilterConfigSchema.RecipientBlockedSenderAction] = value;
			}
		}

		// Token: 0x060037BB RID: 14267 RVA: 0x000D9078 File Offset: 0x000D7278
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			base.ValidateMaximumCollectionCount(errors, this.BlockedSenders, 800, SenderFilterConfigSchema.BlockedSenders);
			base.ValidateMaximumCollectionCount(errors, this.BlockedDomains, 800, SenderFilterConfigSchema.BlockedDomains);
			base.ValidateMaximumCollectionCount(errors, this.BlockedDomainsAndSubdomains, 800, SenderFilterConfigSchema.BlockedDomainAndSubdomains);
		}

		// Token: 0x0400259F RID: 9631
		public const string CanonicalName = "SenderFilterConfig";

		// Token: 0x040025A0 RID: 9632
		private const string MostDerivedClass = "msExchMessageHygieneSenderFilterConfig";

		// Token: 0x040025A1 RID: 9633
		private static readonly SenderFilterConfigSchema schema = ObjectSchema.GetInstance<SenderFilterConfigSchema>();

		// Token: 0x020004D7 RID: 1239
		private static class Validation
		{
			// Token: 0x040025A2 RID: 9634
			public const int MaxBlockedSendersLength = 800;

			// Token: 0x040025A3 RID: 9635
			public const int MaxBlockedDomainsLength = 800;

			// Token: 0x040025A4 RID: 9636
			public const int MaxBlockedDomainsAndSubdomainsLength = 800;
		}
	}
}
