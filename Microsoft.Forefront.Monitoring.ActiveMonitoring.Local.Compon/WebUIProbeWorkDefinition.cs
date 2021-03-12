using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.Exchange.Compliance.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x020002A1 RID: 673
	public class WebUIProbeWorkDefinition
	{
		// Token: 0x06001677 RID: 5751 RVA: 0x000487B0 File Offset: 0x000469B0
		public WebUIProbeWorkDefinition(string xml)
		{
			this.LoadFromContext(xml);
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001678 RID: 5752 RVA: 0x000487BF File Offset: 0x000469BF
		// (set) Token: 0x06001679 RID: 5753 RVA: 0x000487C7 File Offset: 0x000469C7
		public ProcessCookies ProcessCookies
		{
			get
			{
				return this.processCookies;
			}
			set
			{
				this.processCookies = value;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x0600167A RID: 5754 RVA: 0x000487D0 File Offset: 0x000469D0
		// (set) Token: 0x0600167B RID: 5755 RVA: 0x000487D8 File Offset: 0x000469D8
		public ICollection<EndPoint> EndPoints
		{
			get
			{
				return this.endPoints;
			}
			set
			{
				this.endPoints = value;
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x0600167C RID: 5756 RVA: 0x000487E1 File Offset: 0x000469E1
		// (set) Token: 0x0600167D RID: 5757 RVA: 0x000487E9 File Offset: 0x000469E9
		public TimeSpan PageLoadTimeout
		{
			get
			{
				return this.pageLoadTimeout;
			}
			set
			{
				this.pageLoadTimeout = value;
			}
		}

		// Token: 0x0600167E RID: 5758 RVA: 0x000487F4 File Offset: 0x000469F4
		private void LoadFromContext(string xml)
		{
			if (string.IsNullOrWhiteSpace(xml))
			{
				throw new ArgumentException("Work Definition XML is not valid.");
			}
			XmlDocument xmlDocument = new SafeXmlDocument();
			xmlDocument.LoadXml(xml);
			XmlNode xmlNode = xmlDocument.SelectSingleNode("//ProcessCookies");
			this.ProcessCookies = ProcessCookies.All;
			if (xmlNode != null)
			{
				this.ProcessCookies = Utils.GetEnumValue<ProcessCookies>(xmlNode.InnerText, "ProcessCookies");
			}
			xmlNode = xmlDocument.SelectSingleNode("//PageLoadTimeout");
			this.PageLoadTimeout = TimeSpan.MaxValue;
			if (xmlNode != null)
			{
				this.PageLoadTimeout = TimeSpan.FromMilliseconds((double)Utils.GetPositiveInteger(xmlNode.InnerText, "PageLoadTimeout"));
			}
			this.EndPoints = EndPoint.FromXml(xmlDocument, this.PageLoadTimeout);
		}

		// Token: 0x04000AF9 RID: 2809
		private ProcessCookies processCookies;

		// Token: 0x04000AFA RID: 2810
		private ICollection<EndPoint> endPoints;

		// Token: 0x04000AFB RID: 2811
		private TimeSpan pageLoadTimeout;
	}
}
