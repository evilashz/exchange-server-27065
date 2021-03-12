using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000DF7 RID: 3575
	[Serializable]
	public sealed class SharingMessageInitiator
	{
		// Token: 0x170020DB RID: 8411
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x0021F508 File Offset: 0x0021D708
		// (set) Token: 0x06007ADB RID: 31451 RVA: 0x0021F510 File Offset: 0x0021D710
		[XmlElement]
		public string Name { get; set; }

		// Token: 0x170020DC RID: 8412
		// (get) Token: 0x06007ADC RID: 31452 RVA: 0x0021F519 File Offset: 0x0021D719
		// (set) Token: 0x06007ADD RID: 31453 RVA: 0x0021F521 File Offset: 0x0021D721
		[XmlElement]
		public string SmtpAddress { get; set; }

		// Token: 0x170020DD RID: 8413
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x0021F52A File Offset: 0x0021D72A
		// (set) Token: 0x06007ADF RID: 31455 RVA: 0x0021F532 File Offset: 0x0021D732
		[XmlElement(IsNullable = false)]
		public string EntryId { get; set; }

		// Token: 0x06007AE0 RID: 31456 RVA: 0x0021F53B File Offset: 0x0021D73B
		internal ValidationResults Validate()
		{
			if (string.IsNullOrEmpty(this.SmtpAddress))
			{
				return new ValidationResults(ValidationResult.Failure, "SmtpAddress is required");
			}
			if (!Microsoft.Exchange.Data.SmtpAddress.IsValidSmtpAddress(this.SmtpAddress))
			{
				return new ValidationResults(ValidationResult.Failure, "SmtpAddress is not valid value");
			}
			return ValidationResults.Success;
		}
	}
}
