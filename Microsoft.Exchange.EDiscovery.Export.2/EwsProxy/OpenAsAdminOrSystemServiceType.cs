using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Web.Services.Protocols;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x02000018 RID: 24
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.1")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DebuggerStepThrough]
	[XmlRoot("OpenAsAdminOrSystemService", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types", IsNullable = false)]
	[Serializable]
	public class OpenAsAdminOrSystemServiceType : SoapHeader
	{
		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00003729 File Offset: 0x00001929
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00003731 File Offset: 0x00001931
		public ConnectingSIDType ConnectingSID { get; set; }

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000BE RID: 190 RVA: 0x0000373A File Offset: 0x0000193A
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00003742 File Offset: 0x00001942
		[XmlAttribute]
		public SpecialLogonType LogonType { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000C0 RID: 192 RVA: 0x0000374B File Offset: 0x0000194B
		// (set) Token: 0x060000C1 RID: 193 RVA: 0x00003753 File Offset: 0x00001953
		[XmlAttribute]
		public int BudgetType { get; set; }

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000C2 RID: 194 RVA: 0x0000375C File Offset: 0x0000195C
		// (set) Token: 0x060000C3 RID: 195 RVA: 0x00003764 File Offset: 0x00001964
		[XmlIgnore]
		public bool BudgetTypeSpecified { get; set; }

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x0000376D File Offset: 0x0000196D
		// (set) Token: 0x060000C5 RID: 197 RVA: 0x00003775 File Offset: 0x00001975
		[XmlAnyAttribute]
		public XmlAttribute[] AnyAttr { get; set; }
	}
}
