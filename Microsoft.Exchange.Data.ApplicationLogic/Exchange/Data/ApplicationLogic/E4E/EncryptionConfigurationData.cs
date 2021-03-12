using System;
using System.Globalization;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Exchange.Data.ApplicationLogic.E4E
{
	// Token: 0x020000E8 RID: 232
	[Serializable]
	public class EncryptionConfigurationData
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x00025D89 File Offset: 0x00023F89
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x00025D91 File Offset: 0x00023F91
		[XmlElement]
		public string ImageBase64 { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x060009B5 RID: 2485 RVA: 0x00025D9A File Offset: 0x00023F9A
		// (set) Token: 0x060009B6 RID: 2486 RVA: 0x00025DA2 File Offset: 0x00023FA2
		[XmlElement]
		public string EmailText { get; set; }

		// Token: 0x17000284 RID: 644
		// (get) Token: 0x060009B7 RID: 2487 RVA: 0x00025DAB File Offset: 0x00023FAB
		// (set) Token: 0x060009B8 RID: 2488 RVA: 0x00025DB3 File Offset: 0x00023FB3
		[XmlElement]
		public string PortalText { get; set; }

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x00025DBC File Offset: 0x00023FBC
		// (set) Token: 0x060009BA RID: 2490 RVA: 0x00025DC4 File Offset: 0x00023FC4
		[XmlElement]
		public string DisclaimerText { get; set; }

		// Token: 0x17000286 RID: 646
		// (get) Token: 0x060009BB RID: 2491 RVA: 0x00025DCD File Offset: 0x00023FCD
		// (set) Token: 0x060009BC RID: 2492 RVA: 0x00025DD5 File Offset: 0x00023FD5
		[XmlElement]
		public bool OTPEnabled { get; set; }

		// Token: 0x060009BD RID: 2493 RVA: 0x00025DDE File Offset: 0x00023FDE
		public EncryptionConfigurationData()
		{
			this.ImageBase64 = string.Empty;
			this.EmailText = string.Empty;
			this.PortalText = string.Empty;
			this.DisclaimerText = string.Empty;
			this.OTPEnabled = true;
		}

		// Token: 0x060009BE RID: 2494 RVA: 0x00025E19 File Offset: 0x00024019
		public EncryptionConfigurationData(string imageBase64, string emailText, string portalText, string disclaimerText, bool otpEnabled)
		{
			this.ImageBase64 = imageBase64;
			this.EmailText = emailText;
			this.PortalText = portalText;
			this.DisclaimerText = disclaimerText;
			this.OTPEnabled = otpEnabled;
			this.OTPEnabled = true;
		}

		// Token: 0x060009BF RID: 2495 RVA: 0x00025E50 File Offset: 0x00024050
		internal string Serialize()
		{
			if (this.ImageBase64 == null)
			{
				this.ImageBase64 = string.Empty;
			}
			if (this.EmailText == null)
			{
				this.EmailText = string.Empty;
			}
			if (this.PortalText == null)
			{
				this.PortalText = string.Empty;
			}
			if (this.DisclaimerText == null)
			{
				this.DisclaimerText = string.Empty;
			}
			string result;
			using (StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture))
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(base.GetType());
				safeXmlSerializer.Serialize(stringWriter, this);
				stringWriter.Flush();
				result = stringWriter.ToString();
			}
			return result;
		}

		// Token: 0x060009C0 RID: 2496 RVA: 0x00025EF4 File Offset: 0x000240F4
		internal static EncryptionConfigurationData Deserialize(string serializedXML)
		{
			EncryptionConfigurationData result;
			using (StringReader stringReader = new StringReader(serializedXML))
			{
				SafeXmlSerializer safeXmlSerializer = new SafeXmlSerializer(typeof(EncryptionConfigurationData));
				EncryptionConfigurationData encryptionConfigurationData = (EncryptionConfigurationData)safeXmlSerializer.Deserialize(stringReader);
				result = encryptionConfigurationData;
			}
			return result;
		}
	}
}
