using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.OutlookAddinAdapter
{
	// Token: 0x020000C2 RID: 194
	[DataContract(Name = "CustomInvite")]
	[XmlType("CustomInvite")]
	[KnownType(typeof(CustomInvite))]
	public class CustomInvite
	{
		// Token: 0x170001A1 RID: 417
		// (get) Token: 0x060004B2 RID: 1202 RVA: 0x0000BC2E File Offset: 0x00009E2E
		// (set) Token: 0x060004B3 RID: 1203 RVA: 0x0000BC36 File Offset: 0x00009E36
		[DataMember(Name = "help-url", EmitDefaultValue = false)]
		[XmlElement("help-url")]
		public string HelpUrl { get; set; }

		// Token: 0x170001A2 RID: 418
		// (get) Token: 0x060004B4 RID: 1204 RVA: 0x0000BC3F File Offset: 0x00009E3F
		// (set) Token: 0x060004B5 RID: 1205 RVA: 0x0000BC47 File Offset: 0x00009E47
		[XmlElement("logo-url")]
		[DataMember(Name = "logo-url", EmitDefaultValue = false)]
		public string LogoUrl { get; set; }

		// Token: 0x170001A3 RID: 419
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x0000BC50 File Offset: 0x00009E50
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x0000BC58 File Offset: 0x00009E58
		[XmlElement("legal-url")]
		[DataMember(Name = "legal-url", EmitDefaultValue = false)]
		public string LegalUrl { get; set; }

		// Token: 0x170001A4 RID: 420
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x0000BC61 File Offset: 0x00009E61
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x0000BC69 File Offset: 0x00009E69
		[XmlElement("footer-text")]
		[DataMember(Name = "footer-text", EmitDefaultValue = false)]
		public string FooterText { get; set; }

		// Token: 0x060004BA RID: 1210 RVA: 0x0000BC74 File Offset: 0x00009E74
		internal static CustomInvite ConvertFrom(CustomizationValues customizationValues)
		{
			if (customizationValues != null && (!string.IsNullOrEmpty(customizationValues.InvitationHelpUrl) || !string.IsNullOrEmpty(customizationValues.InvitationLogoUrl) || !string.IsNullOrEmpty(customizationValues.InvitationLegalUrl) || !string.IsNullOrEmpty(customizationValues.InvitationFooterText)))
			{
				return new CustomInvite
				{
					HelpUrl = customizationValues.InvitationHelpUrl,
					LogoUrl = customizationValues.InvitationLogoUrl,
					LegalUrl = customizationValues.InvitationLegalUrl,
					FooterText = customizationValues.InvitationFooterText
				};
			}
			return null;
		}
	}
}
