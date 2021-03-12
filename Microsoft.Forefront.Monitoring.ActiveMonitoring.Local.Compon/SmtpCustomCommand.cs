using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Forefront.Monitoring.ActiveMonitoring.Smtp.Probes;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x02000253 RID: 595
	public class SmtpCustomCommand
	{
		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001423 RID: 5155 RVA: 0x0003B5CC File Offset: 0x000397CC
		// (set) Token: 0x06001424 RID: 5156 RVA: 0x0003B5D4 File Offset: 0x000397D4
		public string Name
		{
			get
			{
				return this.name;
			}
			internal set
			{
				this.name = value;
			}
		}

		// Token: 0x1700060A RID: 1546
		// (get) Token: 0x06001425 RID: 5157 RVA: 0x0003B5DD File Offset: 0x000397DD
		// (set) Token: 0x06001426 RID: 5158 RVA: 0x0003B5E5 File Offset: 0x000397E5
		public string Arguments
		{
			get
			{
				return this.arguments;
			}
			internal set
			{
				this.arguments = value;
			}
		}

		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001427 RID: 5159 RVA: 0x0003B5EE File Offset: 0x000397EE
		// (set) Token: 0x06001428 RID: 5160 RVA: 0x0003B5F6 File Offset: 0x000397F6
		public CustomCommandRunPoint CustomCommandRunPoint
		{
			get
			{
				return this.runPoint;
			}
			internal set
			{
				this.runPoint = value;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001429 RID: 5161 RVA: 0x0003B5FF File Offset: 0x000397FF
		// (set) Token: 0x0600142A RID: 5162 RVA: 0x0003B607 File Offset: 0x00039807
		public SmtpExpectedResponse ExpectedResponse
		{
			get
			{
				return this.expectedResponse;
			}
			internal set
			{
				this.expectedResponse = value;
			}
		}

		// Token: 0x0600142B RID: 5163 RVA: 0x0003B610 File Offset: 0x00039810
		public static ICollection<SmtpCustomCommand> FromXml(XmlNode workContext)
		{
			List<SmtpCustomCommand> list = new List<SmtpCustomCommand>();
			using (XmlNodeList xmlNodeList = workContext.SelectNodes("//CustomCommand"))
			{
				foreach (object obj in xmlNodeList)
				{
					XmlNode xmlNode = (XmlNode)obj;
					SmtpCustomCommand smtpCustomCommand = new SmtpCustomCommand();
					XmlElement xmlElement = xmlNode as XmlElement;
					smtpCustomCommand.name = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Name"), "CustomCommand Name");
					smtpCustomCommand.runPoint = Utils.GetEnumValue<CustomCommandRunPoint>(xmlElement.GetAttribute("RunPoint"), "CustomCommand RunPoint");
					smtpCustomCommand.expectedResponse = SmtpExpectedResponse.FromXml(xmlNode.SelectSingleNode("ExpectedResponse"), "ExpectedResponse", SimpleSmtpClient.SmtpResponseCode.OK, false);
					string attribute = xmlElement.GetAttribute("Arguments");
					if (!string.IsNullOrWhiteSpace(attribute))
					{
						smtpCustomCommand.arguments = attribute;
					}
					list.Add(smtpCustomCommand);
				}
			}
			return list;
		}

		// Token: 0x04000981 RID: 2433
		private string name;

		// Token: 0x04000982 RID: 2434
		private string arguments;

		// Token: 0x04000983 RID: 2435
		private CustomCommandRunPoint runPoint;

		// Token: 0x04000984 RID: 2436
		private SmtpExpectedResponse expectedResponse;
	}
}
