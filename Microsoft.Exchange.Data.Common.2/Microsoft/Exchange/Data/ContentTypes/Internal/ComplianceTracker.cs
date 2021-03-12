using System;
using Microsoft.Exchange.Data.ContentTypes.iCalendar;
using Microsoft.Exchange.Data.ContentTypes.vCard;

namespace Microsoft.Exchange.Data.ContentTypes.Internal
{
	// Token: 0x020000B7 RID: 183
	internal class ComplianceTracker
	{
		// Token: 0x06000773 RID: 1907 RVA: 0x000295B0 File Offset: 0x000277B0
		public ComplianceTracker(FormatType format, ComplianceMode complianceMode)
		{
			this.format = format;
			this.complianceMode = complianceMode;
		}

		// Token: 0x06000774 RID: 1908 RVA: 0x000295C6 File Offset: 0x000277C6
		public void SetComplianceStatus(ComplianceStatus status, string message)
		{
			this.complianceStatus |= status;
			if (ComplianceMode.Strict == this.complianceMode)
			{
				if (this.format == FormatType.Calendar)
				{
					throw new InvalidCalendarDataException(message);
				}
				if (FormatType.VCard == this.format)
				{
					throw new InvalidContactDataException(message);
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000775 RID: 1909 RVA: 0x000295FE File Offset: 0x000277FE
		public ComplianceStatus Status
		{
			get
			{
				return this.complianceStatus;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000776 RID: 1910 RVA: 0x00029606 File Offset: 0x00027806
		public ComplianceMode Mode
		{
			get
			{
				return this.complianceMode;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000777 RID: 1911 RVA: 0x0002960E File Offset: 0x0002780E
		public FormatType Format
		{
			get
			{
				return this.format;
			}
		}

		// Token: 0x06000778 RID: 1912 RVA: 0x00029616 File Offset: 0x00027816
		public void Reset()
		{
			this.complianceStatus = ComplianceStatus.Compliant;
		}

		// Token: 0x04000600 RID: 1536
		private FormatType format;

		// Token: 0x04000601 RID: 1537
		private ComplianceMode complianceMode;

		// Token: 0x04000602 RID: 1538
		private ComplianceStatus complianceStatus;
	}
}
