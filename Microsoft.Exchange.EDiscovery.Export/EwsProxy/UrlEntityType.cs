using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000184 RID: 388
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[Serializable]
	public class UrlEntityType : EntityType
	{
		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x060010CE RID: 4302 RVA: 0x00023D9F File Offset: 0x00021F9F
		// (set) Token: 0x060010CF RID: 4303 RVA: 0x00023DA7 File Offset: 0x00021FA7
		public string Url
		{
			get
			{
				return this.urlField;
			}
			set
			{
				this.urlField = value;
			}
		}

		// Token: 0x04000B6D RID: 2925
		private string urlField;
	}
}
