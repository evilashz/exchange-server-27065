using System;
using System.Xml;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000254 RID: 596
	public class SmtpExpectedResponse
	{
		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x0600142D RID: 5165 RVA: 0x0003B72C File Offset: 0x0003992C
		// (set) Token: 0x0600142E RID: 5166 RVA: 0x0003B734 File Offset: 0x00039934
		public ExpectedResponseType Type
		{
			get
			{
				return this.type;
			}
			internal set
			{
				this.type = value;
			}
		}

		// Token: 0x1700060E RID: 1550
		// (get) Token: 0x0600142F RID: 5167 RVA: 0x0003B73D File Offset: 0x0003993D
		// (set) Token: 0x06001430 RID: 5168 RVA: 0x0003B745 File Offset: 0x00039945
		public SimpleSmtpClient.SmtpResponseCode ResponseCode
		{
			get
			{
				return this.responseCode;
			}
			internal set
			{
				this.responseCode = value;
			}
		}

		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001431 RID: 5169 RVA: 0x0003B74E File Offset: 0x0003994E
		// (set) Token: 0x06001432 RID: 5170 RVA: 0x0003B756 File Offset: 0x00039956
		public string ResponseText
		{
			get
			{
				return this.responseText;
			}
			internal set
			{
				this.responseText = value;
			}
		}

		// Token: 0x06001433 RID: 5171 RVA: 0x0003B760 File Offset: 0x00039960
		public static SmtpExpectedResponse FromXml(XmlNode workContext, string nodePath, SimpleSmtpClient.SmtpResponseCode defaultValue, bool isRequired = false)
		{
			XmlElement xmlElement = workContext as XmlElement;
			SmtpExpectedResponse smtpExpectedResponse = new SmtpExpectedResponse
			{
				type = ExpectedResponseType.ResponseCode,
				responseCode = defaultValue
			};
			if (xmlElement != null)
			{
				string attribute = xmlElement.GetAttribute("Type");
				if (!string.IsNullOrEmpty(attribute))
				{
					smtpExpectedResponse.type = Utils.GetEnumValue<ExpectedResponseType>(attribute, string.Format("{0} Type", nodePath));
				}
				switch (smtpExpectedResponse.type)
				{
				case ExpectedResponseType.ResponseCode:
					smtpExpectedResponse.responseCode = Utils.GetEnumValue<SimpleSmtpClient.SmtpResponseCode>(xmlElement.InnerText, nodePath);
					break;
				case ExpectedResponseType.ResponseText:
					smtpExpectedResponse.responseText = Utils.CheckNullOrWhiteSpace(xmlElement.InnerText, nodePath);
					break;
				default:
					throw new ArgumentException(string.Format("Expected Response Type {0} is not supported.", smtpExpectedResponse.Type));
				}
			}
			else if (isRequired)
			{
				throw new ArgumentException(string.Format("The ExpectedResponse node ({0}) is required.", nodePath));
			}
			return smtpExpectedResponse;
		}

		// Token: 0x04000985 RID: 2437
		private ExpectedResponseType type;

		// Token: 0x04000986 RID: 2438
		private SimpleSmtpClient.SmtpResponseCode responseCode;

		// Token: 0x04000987 RID: 2439
		private string responseText;
	}
}
