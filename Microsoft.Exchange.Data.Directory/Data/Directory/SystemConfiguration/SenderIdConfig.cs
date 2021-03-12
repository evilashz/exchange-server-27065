using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004DA RID: 1242
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class SenderIdConfig : MessageHygieneAgentConfig
	{
		// Token: 0x17001109 RID: 4361
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000D91C6 File Offset: 0x000D73C6
		internal override ADObjectSchema Schema
		{
			get
			{
				return SenderIdConfig.schema;
			}
		}

		// Token: 0x1700110A RID: 4362
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000D91CD File Offset: 0x000D73CD
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneSenderIDConfig";
			}
		}

		// Token: 0x1700110B RID: 4363
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000D91D4 File Offset: 0x000D73D4
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000D91E6 File Offset: 0x000D73E6
		[Parameter(Mandatory = false)]
		public SenderIdAction SpoofedDomainAction
		{
			get
			{
				return (SenderIdAction)this[SenderIdConfigSchema.SpoofedDomainAction];
			}
			set
			{
				this[SenderIdConfigSchema.SpoofedDomainAction] = value;
			}
		}

		// Token: 0x1700110C RID: 4364
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000D91F9 File Offset: 0x000D73F9
		// (set) Token: 0x060037C4 RID: 14276 RVA: 0x000D920B File Offset: 0x000D740B
		[Parameter(Mandatory = false)]
		public SenderIdAction TempErrorAction
		{
			get
			{
				return (SenderIdAction)this[SenderIdConfigSchema.TempErrorAction];
			}
			set
			{
				this[SenderIdConfigSchema.TempErrorAction] = value;
			}
		}

		// Token: 0x1700110D RID: 4365
		// (get) Token: 0x060037C5 RID: 14277 RVA: 0x000D921E File Offset: 0x000D741E
		// (set) Token: 0x060037C6 RID: 14278 RVA: 0x000D9230 File Offset: 0x000D7430
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpAddress> BypassedRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[SenderIdConfigSchema.BypassedRecipients];
			}
			set
			{
				this[SenderIdConfigSchema.BypassedRecipients] = value;
			}
		}

		// Token: 0x1700110E RID: 4366
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x000D923E File Offset: 0x000D743E
		// (set) Token: 0x060037C8 RID: 14280 RVA: 0x000D9250 File Offset: 0x000D7450
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpDomain> BypassedSenderDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomain>)this[SenderIdConfigSchema.BypassedSenderDomains];
			}
			set
			{
				this[SenderIdConfigSchema.BypassedSenderDomains] = value;
			}
		}

		// Token: 0x1700110F RID: 4367
		// (get) Token: 0x060037C9 RID: 14281 RVA: 0x000D925E File Offset: 0x000D745E
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000D9268 File Offset: 0x000D7468
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.TempErrorAction == SenderIdAction.Delete)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.InvalidTempErrorSetting, SenderIdConfigSchema.TempErrorAction, this));
			}
			base.ValidateMaximumCollectionCount(errors, this.BypassedRecipients, 800, SenderIdConfigSchema.BypassedRecipients);
			base.ValidateMaximumCollectionCount(errors, this.BypassedSenderDomains, 800, SenderIdConfigSchema.BypassedSenderDomains);
		}

		// Token: 0x040025AD RID: 9645
		public const string CanonicalName = "SenderIdConfig";

		// Token: 0x040025AE RID: 9646
		private const string MostDerivedClass = "msExchMessageHygieneSenderIDConfig";

		// Token: 0x040025AF RID: 9647
		private static SenderIdConfigSchema schema = ObjectSchema.GetInstance<SenderIdConfigSchema>();

		// Token: 0x020004DB RID: 1243
		private struct Validation
		{
			// Token: 0x040025B0 RID: 9648
			public const int MaxBypassedRecipients = 800;

			// Token: 0x040025B1 RID: 9649
			public const int MaxBypassedSenderDomains = 800;
		}
	}
}
