using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008F1 RID: 2289
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[Serializable]
	public class SettingsAccountSettings
	{
		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x00073519 File Offset: 0x00071719
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x00073521 File Offset: 0x00071721
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

		// Token: 0x17000D44 RID: 3396
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x0007352A File Offset: 0x0007172A
		// (set) Token: 0x06003156 RID: 12630 RVA: 0x00073532 File Offset: 0x00071732
		[XmlIgnore]
		public bool StatusSpecified
		{
			get
			{
				return this.statusFieldSpecified;
			}
			set
			{
				this.statusFieldSpecified = value;
			}
		}

		// Token: 0x17000D45 RID: 3397
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x0007353B File Offset: 0x0007173B
		// (set) Token: 0x06003158 RID: 12632 RVA: 0x00073543 File Offset: 0x00071743
		[XmlElement("Set", typeof(SettingsAccountSettingsSet))]
		[XmlElement("Get", typeof(SettingsAccountSettingsGet))]
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

		// Token: 0x04002A79 RID: 10873
		private int statusField;

		// Token: 0x04002A7A RID: 10874
		private bool statusFieldSpecified;

		// Token: 0x04002A7B RID: 10875
		private object itemField;
	}
}
