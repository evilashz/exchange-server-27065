using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.MessageSecurity.MessageClassifications;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x02000160 RID: 352
	internal sealed class MessageClassificationReader
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00054794 File Offset: 0x00052994
		internal MessageClassificationReader(OrganizationId organizationId)
		{
			this.organizationId = organizationId;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000547A4 File Offset: 0x000529A4
		public List<ClassificationSummary> GetClassificationsForLocale(CultureInfo locale)
		{
			ComplianceReader.ThrowOnNullArgument(locale, "locale");
			List<ClassificationSummary> list = new List<ClassificationSummary>();
			foreach (ClassificationSummary classificationSummary in MessageClassificationReader.classificationConfig.GetClassifications(this.organizationId, locale))
			{
				if (classificationSummary.PermissionMenuVisible)
				{
					list.Add(classificationSummary);
				}
			}
			return list;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x0005481C File Offset: 0x00052A1C
		public ClassificationSummary LookupMessageClassification(Guid guid, CultureInfo locale)
		{
			ComplianceReader.ThrowOnNullArgument(locale, "locale");
			if (guid == Guid.Empty)
			{
				return null;
			}
			return MessageClassificationReader.classificationConfig.GetClassification(this.organizationId, guid, locale);
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x0005484C File Offset: 0x00052A4C
		public string GetDescription(Guid guid, CultureInfo locale, bool checkForPermissionMenuVisible)
		{
			ClassificationSummary classificationSummary = this.LookupMessageClassification(guid, locale);
			if (checkForPermissionMenuVisible && !classificationSummary.PermissionMenuVisible)
			{
				return string.Empty;
			}
			return this.GetDescription(classificationSummary);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x0005487C File Offset: 0x00052A7C
		private string GetDescription(ClassificationSummary classification)
		{
			string result = string.Empty;
			if (classification != null)
			{
				if (classification.SenderDescription != null && !string.IsNullOrEmpty(classification.SenderDescription.Trim()))
				{
					result = (string.IsNullOrEmpty(classification.DisplayName) ? string.Empty : classification.DisplayName) + " - " + classification.SenderDescription;
				}
				else if (classification.DisplayName != null && !string.IsNullOrEmpty(classification.DisplayName.Trim()))
				{
					result = classification.DisplayName;
				}
			}
			return result;
		}

		// Token: 0x040008A0 RID: 2208
		private static readonly ClassificationConfig classificationConfig = new ClassificationConfig();

		// Token: 0x040008A1 RID: 2209
		private readonly OrganizationId organizationId;
	}
}
