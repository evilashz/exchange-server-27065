using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004D1 RID: 1233
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class RecipientFilterConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010FA RID: 4346
		// (get) Token: 0x0600379D RID: 14237 RVA: 0x000D8D2B File Offset: 0x000D6F2B
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010FB RID: 4347
		// (get) Token: 0x0600379E RID: 14238 RVA: 0x000D8D33 File Offset: 0x000D6F33
		internal override ADObjectSchema Schema
		{
			get
			{
				return RecipientFilterConfig.schema;
			}
		}

		// Token: 0x170010FC RID: 4348
		// (get) Token: 0x0600379F RID: 14239 RVA: 0x000D8D3A File Offset: 0x000D6F3A
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneRecipientFilterConfig";
			}
		}

		// Token: 0x170010FD RID: 4349
		// (get) Token: 0x060037A0 RID: 14240 RVA: 0x000D8D41 File Offset: 0x000D6F41
		// (set) Token: 0x060037A1 RID: 14241 RVA: 0x000D8D53 File Offset: 0x000D6F53
		[ValidateCount(0, 800)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> BlockedRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[RecipientFilterConfigSchema.BlockedRecipients];
			}
			set
			{
				this[RecipientFilterConfigSchema.BlockedRecipients] = value;
			}
		}

		// Token: 0x170010FE RID: 4350
		// (get) Token: 0x060037A2 RID: 14242 RVA: 0x000D8D61 File Offset: 0x000D6F61
		// (set) Token: 0x060037A3 RID: 14243 RVA: 0x000D8D73 File Offset: 0x000D6F73
		[Parameter(Mandatory = false)]
		public bool RecipientValidationEnabled
		{
			get
			{
				return (bool)this[RecipientFilterConfigSchema.RecipientValidationEnabled];
			}
			set
			{
				this[RecipientFilterConfigSchema.RecipientValidationEnabled] = value;
			}
		}

		// Token: 0x170010FF RID: 4351
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000D8D86 File Offset: 0x000D6F86
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000D8D98 File Offset: 0x000D6F98
		[Parameter(Mandatory = false)]
		public bool BlockListEnabled
		{
			get
			{
				return (bool)this[RecipientFilterConfigSchema.BlockListEnabled];
			}
			set
			{
				this[RecipientFilterConfigSchema.BlockListEnabled] = value;
			}
		}

		// Token: 0x060037A6 RID: 14246 RVA: 0x000D8DAB File Offset: 0x000D6FAB
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			base.ValidateMaximumCollectionCount(errors, this.BlockedRecipients, 800, RecipientFilterConfigSchema.BlockedRecipients);
		}

		// Token: 0x0400258F RID: 9615
		public const string CanonicalName = "RecipientFilterConfig";

		// Token: 0x04002590 RID: 9616
		private const string MostDerivedClass = "msExchMessageHygieneRecipientFilterConfig";

		// Token: 0x04002591 RID: 9617
		private static readonly RecipientFilterConfigSchema schema = ObjectSchema.GetInstance<RecipientFilterConfigSchema>();

		// Token: 0x020004D2 RID: 1234
		private struct Validation
		{
			// Token: 0x04002592 RID: 9618
			public const int MaxBlockedRecipientsLength = 800;
		}
	}
}
