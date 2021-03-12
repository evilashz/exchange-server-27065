using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008CD RID: 2253
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class ServiceSettingsTypeLists
	{
		// Token: 0x17000CE2 RID: 3298
		// (get) Token: 0x06003030 RID: 12336 RVA: 0x0006CBEB File Offset: 0x0006ADEB
		// (set) Token: 0x06003031 RID: 12337 RVA: 0x0006CBF3 File Offset: 0x0006ADF3
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

		// Token: 0x040029A9 RID: 10665
		private object getField;
	}
}
