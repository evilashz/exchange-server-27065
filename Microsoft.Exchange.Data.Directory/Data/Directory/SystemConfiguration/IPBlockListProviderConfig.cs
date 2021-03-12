using System;
using System.Collections.Generic;
using System.Management.Automation;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004CD RID: 1229
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class IPBlockListProviderConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010F6 RID: 4342
		// (get) Token: 0x06003791 RID: 14225 RVA: 0x000D8980 File Offset: 0x000D6B80
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010F7 RID: 4343
		// (get) Token: 0x06003792 RID: 14226 RVA: 0x000D8988 File Offset: 0x000D6B88
		internal override ADObjectSchema Schema
		{
			get
			{
				return IPBlockListProviderConfig.schema;
			}
		}

		// Token: 0x170010F8 RID: 4344
		// (get) Token: 0x06003793 RID: 14227 RVA: 0x000D898F File Offset: 0x000D6B8F
		internal override string MostDerivedObjectClass
		{
			get
			{
				return IPBlockListProviderConfig.mostDerivedClass;
			}
		}

		// Token: 0x170010F9 RID: 4345
		// (get) Token: 0x06003794 RID: 14228 RVA: 0x000D8996 File Offset: 0x000D6B96
		// (set) Token: 0x06003795 RID: 14229 RVA: 0x000D89A8 File Offset: 0x000D6BA8
		[ValidateCount(0, 800)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> BypassedRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[IPBlockListProviderConfigSchema.BypassedRecipients];
			}
			set
			{
				this[IPBlockListProviderConfigSchema.BypassedRecipients] = value;
			}
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000D89B6 File Offset: 0x000D6BB6
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			base.ValidateMaximumCollectionCount(errors, this.BypassedRecipients, 800, IPBlockListProviderConfigSchema.BypassedRecipients);
		}

		// Token: 0x0400257E RID: 9598
		public const string CanonicalName = "IPBlockListProviderConfig";

		// Token: 0x0400257F RID: 9599
		private static IPBlockListProviderConfigSchema schema = ObjectSchema.GetInstance<IPBlockListProviderConfigSchema>();

		// Token: 0x04002580 RID: 9600
		private static string mostDerivedClass = "msExchMessageHygieneIPBlockListProviderConfig";

		// Token: 0x020004CE RID: 1230
		private struct Validation
		{
			// Token: 0x04002581 RID: 9601
			public const int MaxBypassedRecipientsLength = 800;
		}
	}
}
