using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x02000099 RID: 153
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class GetDomainSettingsRequest : AutodiscoverRequest
	{
		// Token: 0x170001C0 RID: 448
		// (get) Token: 0x060008B8 RID: 2232 RVA: 0x0001F96C File Offset: 0x0001DB6C
		// (set) Token: 0x060008B9 RID: 2233 RVA: 0x0001F974 File Offset: 0x0001DB74
		[XmlArray(IsNullable = true)]
		[XmlArrayItem("Domain")]
		public string[] Domains
		{
			get
			{
				return this.domainsField;
			}
			set
			{
				this.domainsField = value;
			}
		}

		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x060008BA RID: 2234 RVA: 0x0001F97D File Offset: 0x0001DB7D
		// (set) Token: 0x060008BB RID: 2235 RVA: 0x0001F985 File Offset: 0x0001DB85
		[XmlArrayItem("Setting")]
		[XmlArray(IsNullable = true)]
		public string[] RequestedSettings
		{
			get
			{
				return this.requestedSettingsField;
			}
			set
			{
				this.requestedSettingsField = value;
			}
		}

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x060008BC RID: 2236 RVA: 0x0001F98E File Offset: 0x0001DB8E
		// (set) Token: 0x060008BD RID: 2237 RVA: 0x0001F996 File Offset: 0x0001DB96
		[XmlElement(IsNullable = true)]
		public ExchangeVersion? RequestedVersion
		{
			get
			{
				return this.requestedVersionField;
			}
			set
			{
				this.requestedVersionField = value;
			}
		}

		// Token: 0x0400034A RID: 842
		private string[] domainsField;

		// Token: 0x0400034B RID: 843
		private string[] requestedSettingsField;

		// Token: 0x0400034C RID: 844
		private ExchangeVersion? requestedVersionField;
	}
}
