using System;
using System.Globalization;
using System.Xml;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000071 RID: 113
	internal abstract class EvmTranscriptWriter
	{
		// Token: 0x0600042C RID: 1068 RVA: 0x0000EA36 File Offset: 0x0000CC36
		protected EvmTranscriptWriter(CultureInfo language)
		{
			this.Language = language;
		}

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600042D RID: 1069 RVA: 0x0000EA45 File Offset: 0x0000CC45
		// (set) Token: 0x0600042E RID: 1070 RVA: 0x0000EA4D File Offset: 0x0000CC4D
		private protected CultureInfo Language { protected get; private set; }

		// Token: 0x0600042F RID: 1071 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public void WriteTranscript(XmlNode asrData)
		{
			bool flag = false;
			XmlElement xmlElement = null;
			XmlElement xmlElement2 = null;
			if (asrData.NodeType == XmlNodeType.Element && string.Equals(asrData.LocalName, "ASR", StringComparison.OrdinalIgnoreCase))
			{
				foreach (object obj in asrData.ChildNodes)
				{
					XmlElement xmlElement3 = (XmlElement)obj;
					if (string.Equals(xmlElement3.LocalName, "Information", StringComparison.OrdinalIgnoreCase))
					{
						xmlElement = xmlElement3;
					}
					else if (string.Equals(xmlElement3.LocalName, "ErrorInformation", StringComparison.OrdinalIgnoreCase))
					{
						xmlElement2 = xmlElement3;
					}
					else
					{
						this.Write(xmlElement3);
						flag = true;
					}
				}
			}
			if (flag && AppConfig.Instance.Service.EnableTranscriptionWhitespace)
			{
				this.WriteEndOfParagraph();
			}
			if (xmlElement != null)
			{
				this.WriteInformation(xmlElement);
			}
			if (xmlElement2 != null)
			{
				this.WriteInformation(xmlElement2);
			}
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x0000EB3C File Offset: 0x0000CD3C
		public void WriteErrorInformationOnly(XmlNode asrData)
		{
			if (asrData.NodeType == XmlNodeType.Element && string.Equals(asrData.LocalName, "ASR", StringComparison.OrdinalIgnoreCase))
			{
				foreach (object obj in asrData.ChildNodes)
				{
					XmlElement xmlElement = (XmlElement)obj;
					if (string.Equals(xmlElement.LocalName, "ErrorInformation", StringComparison.OrdinalIgnoreCase))
					{
						this.WriteInformation(xmlElement);
						break;
					}
				}
			}
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x0000EBC8 File Offset: 0x0000CDC8
		protected static bool IgnoreLeadingOrTrailingCharInTelAnchor(char c)
		{
			return '.' == c || ',' == c || ';' == c || ':' == c || char.IsWhiteSpace(c);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x0000EBE8 File Offset: 0x0000CDE8
		protected void Write(XmlElement element)
		{
			if (string.Equals(element.LocalName, "Feature", StringComparison.OrdinalIgnoreCase))
			{
				string value = element.Attributes["class"].Value;
				if (string.Equals(value, "PhoneNumber", StringComparison.OrdinalIgnoreCase))
				{
					this.WritePhoneNumber(element);
					return;
				}
				this.WriteGenericFeature(element);
				return;
			}
			else
			{
				if (string.Equals(element.LocalName, "Text", StringComparison.OrdinalIgnoreCase))
				{
					this.WriteGenericTextElement(element);
					return;
				}
				if (string.Equals(element.LocalName, "Break", StringComparison.OrdinalIgnoreCase))
				{
					this.WriteBreakElement(element);
					return;
				}
				throw new ArgumentException(element.LocalName);
			}
		}

		// Token: 0x06000433 RID: 1075
		protected abstract void WriteEndOfParagraph();

		// Token: 0x06000434 RID: 1076
		protected abstract void WriteInformation(XmlElement element);

		// Token: 0x06000435 RID: 1077
		protected abstract void WritePhoneNumber(XmlElement element);

		// Token: 0x06000436 RID: 1078
		protected abstract void WriteGenericFeature(XmlElement element);

		// Token: 0x06000437 RID: 1079
		protected abstract void WriteGenericTextElement(XmlElement element);

		// Token: 0x06000438 RID: 1080
		protected abstract void WriteBreakElement(XmlElement element);
	}
}
