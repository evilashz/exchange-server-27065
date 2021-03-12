using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008E8 RID: 2280
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[Serializable]
	public class RulesResponseType
	{
		// Token: 0x17000D28 RID: 3368
		// (get) Token: 0x06003114 RID: 12564 RVA: 0x00073306 File Offset: 0x00071506
		// (set) Token: 0x06003115 RID: 12565 RVA: 0x0007330E File Offset: 0x0007150E
		public int Status
		{
			get
			{
				return this.statusField;
			}
			set
			{
				this.statusField = value;
			}
		}

		// Token: 0x17000D29 RID: 3369
		// (get) Token: 0x06003116 RID: 12566 RVA: 0x00073317 File Offset: 0x00071517
		// (set) Token: 0x06003117 RID: 12567 RVA: 0x0007331F File Offset: 0x0007151F
		public XmlElement Get
		{
			get
			{
				return this.getField;
			}
			set
			{
				this.getField = value;
			}
		}

		// Token: 0x17000D2A RID: 3370
		// (get) Token: 0x06003118 RID: 12568 RVA: 0x00073328 File Offset: 0x00071528
		// (set) Token: 0x06003119 RID: 12569 RVA: 0x00073330 File Offset: 0x00071530
		public string Version
		{
			get
			{
				return this.versionField;
			}
			set
			{
				this.versionField = value;
			}
		}

		// Token: 0x04002A5E RID: 10846
		private int statusField;

		// Token: 0x04002A5F RID: 10847
		private XmlElement getField;

		// Token: 0x04002A60 RID: 10848
		private string versionField;
	}
}
