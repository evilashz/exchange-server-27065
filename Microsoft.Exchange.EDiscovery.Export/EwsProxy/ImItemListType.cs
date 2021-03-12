using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.EDiscovery.Export.EwsProxy
{
	// Token: 0x020000D9 RID: 217
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ImItemListType
	{
		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A1D RID: 2589 RVA: 0x00020502 File Offset: 0x0001E702
		// (set) Token: 0x06000A1E RID: 2590 RVA: 0x0002050A File Offset: 0x0001E70A
		[XmlArrayItem("ImGroup", IsNullable = false)]
		public ImGroupType[] Groups
		{
			get
			{
				return this.groupsField;
			}
			set
			{
				this.groupsField = value;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A1F RID: 2591 RVA: 0x00020513 File Offset: 0x0001E713
		// (set) Token: 0x06000A20 RID: 2592 RVA: 0x0002051B File Offset: 0x0001E71B
		[XmlArrayItem("Persona", IsNullable = false)]
		public PersonaType[] Personas
		{
			get
			{
				return this.personasField;
			}
			set
			{
				this.personasField = value;
			}
		}

		// Token: 0x040005DC RID: 1500
		private ImGroupType[] groupsField;

		// Token: 0x040005DD RID: 1501
		private PersonaType[] personasField;
	}
}
