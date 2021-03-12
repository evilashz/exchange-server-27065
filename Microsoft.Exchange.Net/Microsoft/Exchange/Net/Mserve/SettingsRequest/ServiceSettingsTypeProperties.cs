using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008CC RID: 2252
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceSettingsTypeProperties
	{
		// Token: 0x17000CE1 RID: 3297
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x0006CBD2 File Offset: 0x0006ADD2
		// (set) Token: 0x0600302E RID: 12334 RVA: 0x0006CBDA File Offset: 0x0006ADDA
		public object Get
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

		// Token: 0x040029A8 RID: 10664
		private object getField;
	}
}
