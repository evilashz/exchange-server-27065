using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x0200021F RID: 543
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class ExcludesValueType
	{
		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06001539 RID: 5433 RVA: 0x000262ED File Offset: 0x000244ED
		// (set) Token: 0x0600153A RID: 5434 RVA: 0x000262F5 File Offset: 0x000244F5
		[XmlAttribute]
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

		// Token: 0x04000EA3 RID: 3747
		private string valueField;
	}
}
