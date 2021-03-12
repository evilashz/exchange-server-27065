using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AA RID: 938
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class SIDType
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x0002A68F File Offset: 0x0002888F
		// (set) Token: 0x06001D42 RID: 7490 RVA: 0x0002A697 File Offset: 0x00028897
		[XmlText]
		public string Value
		{
			get
			{
				return this.valueField;
			}
			set
			{
				this.valueField = value;
			}
		}

		// Token: 0x0400135E RID: 4958
		private string valueField;
	}
}
