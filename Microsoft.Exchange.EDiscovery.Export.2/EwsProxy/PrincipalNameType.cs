using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020003AB RID: 939
	[DebuggerStepThrough]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PrincipalNameType
	{
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06001D44 RID: 7492 RVA: 0x0002A6A8 File Offset: 0x000288A8
		// (set) Token: 0x06001D45 RID: 7493 RVA: 0x0002A6B0 File Offset: 0x000288B0
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

		// Token: 0x0400135F RID: 4959
		private string valueField;
	}
}
