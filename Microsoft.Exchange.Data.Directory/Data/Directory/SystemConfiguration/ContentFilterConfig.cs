using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Collections;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020004BC RID: 1212
	[ObjectScope(new ConfigScopes[]
	{
		ConfigScopes.TenantLocal,
		ConfigScopes.TenantSubTree
	})]
	[Serializable]
	public sealed class ContentFilterConfig : MessageHygieneAgentConfig
	{
		// Token: 0x170010CA RID: 4298
		// (get) Token: 0x06003721 RID: 14113 RVA: 0x000D7CBB File Offset: 0x000D5EBB
		internal override ADObjectSchema Schema
		{
			get
			{
				return ContentFilterConfig.schema;
			}
		}

		// Token: 0x170010CB RID: 4299
		// (get) Token: 0x06003722 RID: 14114 RVA: 0x000D7CC2 File Offset: 0x000D5EC2
		internal override string MostDerivedObjectClass
		{
			get
			{
				return "msExchMessageHygieneContentFilterConfig";
			}
		}

		// Token: 0x170010CC RID: 4300
		// (get) Token: 0x06003723 RID: 14115 RVA: 0x000D7CC9 File Offset: 0x000D5EC9
		public new string Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x170010CD RID: 4301
		// (get) Token: 0x06003724 RID: 14116 RVA: 0x000D7CD1 File Offset: 0x000D5ED1
		// (set) Token: 0x06003725 RID: 14117 RVA: 0x000D7CE3 File Offset: 0x000D5EE3
		[Parameter(Mandatory = false)]
		public AsciiString RejectionResponse
		{
			get
			{
				return (AsciiString)this[ContentFilterConfigSchema.RejectionResponse];
			}
			set
			{
				this[ContentFilterConfigSchema.RejectionResponse] = value;
			}
		}

		// Token: 0x170010CE RID: 4302
		// (get) Token: 0x06003726 RID: 14118 RVA: 0x000D7CF1 File Offset: 0x000D5EF1
		// (set) Token: 0x06003727 RID: 14119 RVA: 0x000D7D03 File Offset: 0x000D5F03
		[Parameter(Mandatory = false)]
		public bool OutlookEmailPostmarkValidationEnabled
		{
			get
			{
				return (bool)this[ContentFilterConfigSchema.OutlookEmailPostmarkValidationEnabled];
			}
			set
			{
				this[ContentFilterConfigSchema.OutlookEmailPostmarkValidationEnabled] = value;
			}
		}

		// Token: 0x170010CF RID: 4303
		// (get) Token: 0x06003728 RID: 14120 RVA: 0x000D7D16 File Offset: 0x000D5F16
		// (set) Token: 0x06003729 RID: 14121 RVA: 0x000D7D28 File Offset: 0x000D5F28
		[ValidateCount(0, 800)]
		[Parameter(Mandatory = false)]
		public MultiValuedProperty<SmtpAddress> BypassedRecipients
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[ContentFilterConfigSchema.BypassedRecipients];
			}
			set
			{
				this[ContentFilterConfigSchema.BypassedRecipients] = value;
			}
		}

		// Token: 0x170010D0 RID: 4304
		// (get) Token: 0x0600372A RID: 14122 RVA: 0x000D7D36 File Offset: 0x000D5F36
		// (set) Token: 0x0600372B RID: 14123 RVA: 0x000D7D48 File Offset: 0x000D5F48
		[Parameter(Mandatory = false)]
		public SmtpAddress? QuarantineMailbox
		{
			get
			{
				return (SmtpAddress?)this[ContentFilterConfigSchema.QuarantineMailbox];
			}
			set
			{
				this[ContentFilterConfigSchema.QuarantineMailbox] = value;
			}
		}

		// Token: 0x170010D1 RID: 4305
		// (get) Token: 0x0600372C RID: 14124 RVA: 0x000D7D5B File Offset: 0x000D5F5B
		// (set) Token: 0x0600372D RID: 14125 RVA: 0x000D7D6D File Offset: 0x000D5F6D
		[Parameter(Mandatory = false)]
		public int SCLRejectThreshold
		{
			get
			{
				return (int)this[ContentFilterConfigSchema.SCLRejectThreshold];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLRejectThreshold] = value;
			}
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x0600372E RID: 14126 RVA: 0x000D7D80 File Offset: 0x000D5F80
		// (set) Token: 0x0600372F RID: 14127 RVA: 0x000D7D92 File Offset: 0x000D5F92
		[Parameter(Mandatory = false)]
		public bool SCLRejectEnabled
		{
			get
			{
				return (bool)this[ContentFilterConfigSchema.SCLRejectEnabled];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLRejectEnabled] = value;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06003730 RID: 14128 RVA: 0x000D7DA5 File Offset: 0x000D5FA5
		// (set) Token: 0x06003731 RID: 14129 RVA: 0x000D7DB7 File Offset: 0x000D5FB7
		[Parameter(Mandatory = false)]
		public int SCLDeleteThreshold
		{
			get
			{
				return (int)this[ContentFilterConfigSchema.SCLDeleteThreshold];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLDeleteThreshold] = value;
			}
		}

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06003732 RID: 14130 RVA: 0x000D7DCA File Offset: 0x000D5FCA
		// (set) Token: 0x06003733 RID: 14131 RVA: 0x000D7DDC File Offset: 0x000D5FDC
		[Parameter(Mandatory = false)]
		public bool SCLDeleteEnabled
		{
			get
			{
				return (bool)this[ContentFilterConfigSchema.SCLDeleteEnabled];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLDeleteEnabled] = value;
			}
		}

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06003734 RID: 14132 RVA: 0x000D7DEF File Offset: 0x000D5FEF
		// (set) Token: 0x06003735 RID: 14133 RVA: 0x000D7E01 File Offset: 0x000D6001
		[Parameter(Mandatory = false)]
		public int SCLQuarantineThreshold
		{
			get
			{
				return (int)this[ContentFilterConfigSchema.SCLQuarantineThreshold];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLQuarantineThreshold] = value;
			}
		}

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06003736 RID: 14134 RVA: 0x000D7E14 File Offset: 0x000D6014
		// (set) Token: 0x06003737 RID: 14135 RVA: 0x000D7E26 File Offset: 0x000D6026
		[Parameter(Mandatory = false)]
		public bool SCLQuarantineEnabled
		{
			get
			{
				return (bool)this[ContentFilterConfigSchema.SCLQuarantineEnabled];
			}
			set
			{
				this[ContentFilterConfigSchema.SCLQuarantineEnabled] = value;
			}
		}

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000D7E39 File Offset: 0x000D6039
		// (set) Token: 0x06003739 RID: 14137 RVA: 0x000D7E4B File Offset: 0x000D604B
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpAddress> BypassedSenders
		{
			get
			{
				return (MultiValuedProperty<SmtpAddress>)this[ContentFilterConfigSchema.BypassedSenders];
			}
			set
			{
				this[ContentFilterConfigSchema.BypassedSenders] = value;
			}
		}

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x0600373A RID: 14138 RVA: 0x000D7E59 File Offset: 0x000D6059
		// (set) Token: 0x0600373B RID: 14139 RVA: 0x000D7E6B File Offset: 0x000D606B
		[Parameter(Mandatory = false)]
		[ValidateCount(0, 800)]
		public MultiValuedProperty<SmtpDomainWithSubdomains> BypassedSenderDomains
		{
			get
			{
				return (MultiValuedProperty<SmtpDomainWithSubdomains>)this[ContentFilterConfigSchema.BypassedSenderDomains];
			}
			set
			{
				this[ContentFilterConfigSchema.BypassedSenderDomains] = value;
			}
		}

		// Token: 0x0600373C RID: 14140 RVA: 0x000D7E7C File Offset: 0x000D607C
		internal ReadOnlyCollection<ContentFilterPhrase> GetPhrases()
		{
			List<ContentFilterPhrase> list = new List<ContentFilterPhrase>();
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[ContentFilterConfigSchema.EncodedPhrases];
			if (multiValuedProperty != null)
			{
				foreach (string encoded in multiValuedProperty)
				{
					try
					{
						list.Add(ContentFilterPhrase.Decode(encoded));
					}
					catch (FormatException)
					{
					}
				}
			}
			return new ReadOnlyCollection<ContentFilterPhrase>(list);
		}

		// Token: 0x0600373D RID: 14141 RVA: 0x000D7F00 File Offset: 0x000D6100
		internal void AddPhrase(ContentFilterPhrase phrase)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[ContentFilterConfigSchema.EncodedPhrases];
			if (multiValuedProperty == null)
			{
				multiValuedProperty = new MultiValuedProperty<string>();
			}
			multiValuedProperty.Add(phrase.Encode());
			this[ContentFilterConfigSchema.EncodedPhrases] = multiValuedProperty;
		}

		// Token: 0x0600373E RID: 14142 RVA: 0x000D7F40 File Offset: 0x000D6140
		internal void RemovePhrase(ContentFilterPhrase phrase)
		{
			MultiValuedProperty<string> multiValuedProperty = (MultiValuedProperty<string>)this[ContentFilterConfigSchema.EncodedPhrases];
			if (multiValuedProperty == null)
			{
				multiValuedProperty = new MultiValuedProperty<string>();
			}
			multiValuedProperty.Remove(phrase.Encode());
			this[ContentFilterConfigSchema.EncodedPhrases] = multiValuedProperty;
		}

		// Token: 0x0600373F RID: 14143 RVA: 0x000D7F80 File Offset: 0x000D6180
		protected override void ValidateWrite(List<ValidationError> errors)
		{
			base.ValidateWrite(errors);
			if (this.SCLDeleteEnabled)
			{
				if (this.SCLRejectEnabled && this.SCLDeleteThreshold <= this.SCLRejectThreshold)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DeleteAndRejectThreshold, ContentFilterConfigSchema.SCLDeleteThreshold, this));
				}
				if (this.SCLQuarantineEnabled && this.SCLDeleteThreshold <= this.SCLQuarantineThreshold)
				{
					errors.Add(new PropertyValidationError(DirectoryStrings.DeleteAndQuarantineThreshold, ContentFilterConfigSchema.SCLDeleteThreshold, this));
				}
			}
			if (this.SCLRejectEnabled && this.SCLQuarantineEnabled && this.SCLRejectThreshold <= this.SCLQuarantineThreshold)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.RejectAndQuarantineThreshold, ContentFilterConfigSchema.SCLRejectThreshold, this));
			}
			bool flag = this.QuarantineMailbox != null && this.QuarantineMailbox != SmtpAddress.Empty;
			bool flag2 = flag && this.QuarantineMailbox != SmtpAddress.NullReversePath && this.QuarantineMailbox.Value.IsValidAddress;
			if ((flag || this.SCLQuarantineEnabled) && !flag2)
			{
				errors.Add(new PropertyValidationError(DirectoryStrings.QuarantineMailboxIsInvalid, ContentFilterConfigSchema.QuarantineMailbox, this));
			}
			base.ValidateMaximumCollectionCount(errors, this.BypassedRecipients, 800, ContentFilterConfigSchema.BypassedRecipients);
			base.ValidateMaximumCollectionCount(errors, this.BypassedSenderDomains, 800, ContentFilterConfigSchema.BypassedSenderDomains);
			base.ValidateMaximumCollectionCount(errors, this.BypassedSenders, 800, ContentFilterConfigSchema.BypassedSenders);
		}

		// Token: 0x0400255B RID: 9563
		public const string CanonicalName = "ContentFilterConfig";

		// Token: 0x0400255C RID: 9564
		private const string MostDerivedClass = "msExchMessageHygieneContentFilterConfig";

		// Token: 0x0400255D RID: 9565
		private static readonly ContentFilterConfigSchema schema = ObjectSchema.GetInstance<ContentFilterConfigSchema>();

		// Token: 0x020004BD RID: 1213
		private static class Validation
		{
			// Token: 0x0400255E RID: 9566
			public const int MaxBypassedRecipients = 800;

			// Token: 0x0400255F RID: 9567
			public const int MaxBypassedSenders = 800;

			// Token: 0x04002560 RID: 9568
			public const int MaxBypassedSenderDomains = 800;
		}
	}
}
