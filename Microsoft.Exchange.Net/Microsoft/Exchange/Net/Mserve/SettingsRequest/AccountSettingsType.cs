using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B2 RID: 2226
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AccountSettingsType
	{
		// Token: 0x17000CB2 RID: 3250
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x0006C83B File Offset: 0x0006AA3B
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x0006C843 File Offset: 0x0006AA43
		[XmlElement("Get", typeof(AccountSettingsTypeGet))]
		[XmlElement("Set", typeof(AccountSettingsTypeSet))]
		public object Item
		{
			get
			{
				return this.itemField;
			}
			set
			{
				this.itemField = value;
			}
		}

		// Token: 0x0400294A RID: 10570
		private object itemField;
	}
}
