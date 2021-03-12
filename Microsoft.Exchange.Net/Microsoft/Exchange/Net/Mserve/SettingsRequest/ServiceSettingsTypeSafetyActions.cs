using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008CB RID: 2251
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceSettingsTypeSafetyActions
	{
		// Token: 0x17000CDF RID: 3295
		// (get) Token: 0x06003028 RID: 12328 RVA: 0x0006CBA8 File Offset: 0x0006ADA8
		// (set) Token: 0x06003029 RID: 12329 RVA: 0x0006CBB0 File Offset: 0x0006ADB0
		public object GetVersion
		{
			get
			{
				return this.getVersionField;
			}
			set
			{
				this.getVersionField = value;
			}
		}

		// Token: 0x17000CE0 RID: 3296
		// (get) Token: 0x0600302A RID: 12330 RVA: 0x0006CBB9 File Offset: 0x0006ADB9
		// (set) Token: 0x0600302B RID: 12331 RVA: 0x0006CBC1 File Offset: 0x0006ADC1
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

		// Token: 0x040029A6 RID: 10662
		private object getVersionField;

		// Token: 0x040029A7 RID: 10663
		private object getField;
	}
}
