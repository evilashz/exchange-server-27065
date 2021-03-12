using System;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;
using Microsoft.Exchange.Security.RightsManagement;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000F2 RID: 242
	internal sealed class ComplianceReader
	{
		// Token: 0x1700022E RID: 558
		// (get) Token: 0x060007F0 RID: 2032 RVA: 0x0003B817 File Offset: 0x00039A17
		public MessageClassificationReader MessageClassificationReader
		{
			get
			{
				return this.messageClassificationReader;
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x060007F1 RID: 2033 RVA: 0x0003B81F File Offset: 0x00039A1F
		public RmsTemplateReader RmsTemplateReader
		{
			get
			{
				return this.rmsTemplateReader;
			}
		}

		// Token: 0x060007F2 RID: 2034 RVA: 0x0003B827 File Offset: 0x00039A27
		internal ComplianceReader(OrganizationId organizationId)
		{
			this.messageClassificationReader = new MessageClassificationReader(organizationId);
			this.rmsTemplateReader = new RmsTemplateReader(organizationId);
		}

		// Token: 0x060007F3 RID: 2035 RVA: 0x0003B848 File Offset: 0x00039A48
		public ComplianceType GetComplianceType(Guid guid, CultureInfo locale)
		{
			ClassificationSummary classificationSummary = this.messageClassificationReader.LookupMessageClassification(guid, locale);
			if (classificationSummary != null)
			{
				return ComplianceType.MessageClassification;
			}
			RmsTemplate rmsTemplate = this.rmsTemplateReader.LookupRmsTemplate(guid);
			if (rmsTemplate != null)
			{
				return ComplianceType.RmsTemplate;
			}
			return ComplianceType.Unknown;
		}

		// Token: 0x060007F4 RID: 2036 RVA: 0x0003B87B File Offset: 0x00039A7B
		public bool IsComplianceFeatureEnabled(bool isIrmEnabled, bool isIrmProtected, CultureInfo locale)
		{
			return (isIrmEnabled && (this.rmsTemplateReader.IsInternalLicensingEnabled || (isIrmProtected && this.rmsTemplateReader.IsExternalLicensingEnabled))) || this.messageClassificationReader.GetClassificationsForLocale(locale).Count > 0;
		}

		// Token: 0x060007F5 RID: 2037 RVA: 0x0003B8B4 File Offset: 0x00039AB4
		public string GetDescription(Guid guid, CultureInfo locale)
		{
			switch (this.GetComplianceType(guid, locale))
			{
			case ComplianceType.MessageClassification:
				return this.messageClassificationReader.GetDescription(guid, locale, false);
			case ComplianceType.RmsTemplate:
				return this.rmsTemplateReader.GetDescription(guid, locale);
			default:
				return string.Empty;
			}
		}

		// Token: 0x060007F6 RID: 2038 RVA: 0x0003B900 File Offset: 0x00039B00
		internal static void ThrowOnNullArgument(object argument, string argumentName)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(argumentName);
			}
		}

		// Token: 0x040005B4 RID: 1460
		private MessageClassificationReader messageClassificationReader;

		// Token: 0x040005B5 RID: 1461
		private RmsTemplateReader rmsTemplateReader;
	}
}
