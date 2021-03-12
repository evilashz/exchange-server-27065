using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000246 RID: 582
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[Serializable]
	public class PhoneCallIdType
	{
		// Token: 0x1700079A RID: 1946
		// (get) Token: 0x060015D4 RID: 5588 RVA: 0x00026802 File Offset: 0x00024A02
		// (set) Token: 0x060015D5 RID: 5589 RVA: 0x0002680A File Offset: 0x00024A0A
		[XmlAttribute]
		public string Id
		{
			get
			{
				return this.idField;
			}
			set
			{
				this.idField = value;
			}
		}

		// Token: 0x04000F02 RID: 3842
		private string idField;
	}
}
