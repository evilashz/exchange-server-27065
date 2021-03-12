using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.AutoDiscoverProxy
{
	// Token: 0x0200007F RID: 127
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/2010/Autodiscover")]
	[DesignerCategory("code")]
	[XmlInclude(typeof(DomainStringSetting))]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class DomainSetting
	{
		// Token: 0x1700018D RID: 397
		// (get) Token: 0x06000839 RID: 2105 RVA: 0x0001F541 File Offset: 0x0001D741
		// (set) Token: 0x0600083A RID: 2106 RVA: 0x0001F549 File Offset: 0x0001D749
		[XmlElement(IsNullable = true)]
		public string Name
		{
			get
			{
				return this.nameField;
			}
			set
			{
				this.nameField = value;
			}
		}

		// Token: 0x0400030B RID: 779
		private string nameField;
	}
}
