using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DF6 RID: 3574
	[Serializable]
	public sealed class SharingMessageAction
	{
		// Token: 0x170020D9 RID: 8409
		// (get) Token: 0x06007AD4 RID: 31444 RVA: 0x0021F421 File Offset: 0x0021D621
		// (set) Token: 0x06007AD5 RID: 31445 RVA: 0x0021F429 File Offset: 0x0021D629
		[XmlElement(IsNullable = false)]
		public string Title { get; set; }

		// Token: 0x170020DA RID: 8410
		// (get) Token: 0x06007AD6 RID: 31446 RVA: 0x0021F432 File Offset: 0x0021D632
		// (set) Token: 0x06007AD7 RID: 31447 RVA: 0x0021F43A File Offset: 0x0021D63A
		[XmlArrayItem(ElementName = "Provider")]
		[XmlArray]
		public SharingMessageProvider[] Providers { get; set; }

		// Token: 0x06007AD8 RID: 31448 RVA: 0x0021F444 File Offset: 0x0021D644
		internal ValidationResults Validate(SharingMessageKind sharingMessageKind)
		{
			if (this.Providers == null || this.Providers.Length == 0)
			{
				return new ValidationResults(ValidationResult.Failure, "There should be at least one provider");
			}
			int num = 0;
			int num2 = 0;
			foreach (SharingMessageProvider sharingMessageProvider in this.Providers)
			{
				ValidationResults validationResults = sharingMessageProvider.Validate(sharingMessageKind);
				switch (validationResults.Result)
				{
				case ValidationResult.Success:
					if (sharingMessageProvider.IsExchangeInternalProvider)
					{
						num++;
						if (num > 1)
						{
							return new ValidationResults(ValidationResult.Failure, "There should be at most one Exchange internal provider");
						}
					}
					else if (sharingMessageProvider.IsExchangeExternalProvider)
					{
						num2++;
						if (num2 > 1)
						{
							return new ValidationResults(ValidationResult.Failure, "There should be at most one Exchange internal provider");
						}
					}
					break;
				case ValidationResult.Failure:
					return validationResults;
				}
			}
			return ValidationResults.Success;
		}
	}
}
