using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B1 RID: 2225
	[DebuggerStepThrough]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceSettingsTypeSafetyLevelRules
	{
		// Token: 0x17000CB0 RID: 3248
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x0006C811 File Offset: 0x0006AA11
		// (set) Token: 0x06002FBC RID: 12220 RVA: 0x0006C819 File Offset: 0x0006AA19
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

		// Token: 0x17000CB1 RID: 3249
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x0006C822 File Offset: 0x0006AA22
		// (set) Token: 0x06002FBE RID: 12222 RVA: 0x0006C82A File Offset: 0x0006AA2A
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

		// Token: 0x04002948 RID: 10568
		private object getVersionField;

		// Token: 0x04002949 RID: 10569
		private object getField;
	}
}
