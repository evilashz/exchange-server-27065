using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B3 RID: 2227
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class AccountSettingsTypeGet
	{
		// Token: 0x17000CB3 RID: 3251
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x0006C854 File Offset: 0x0006AA54
		// (set) Token: 0x06002FC4 RID: 12228 RVA: 0x0006C85C File Offset: 0x0006AA5C
		public object Filters
		{
			get
			{
				return this.filtersField;
			}
			set
			{
				this.filtersField = value;
			}
		}

		// Token: 0x17000CB4 RID: 3252
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x0006C865 File Offset: 0x0006AA65
		// (set) Token: 0x06002FC6 RID: 12230 RVA: 0x0006C86D File Offset: 0x0006AA6D
		public object Lists
		{
			get
			{
				return this.listsField;
			}
			set
			{
				this.listsField = value;
			}
		}

		// Token: 0x17000CB5 RID: 3253
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x0006C876 File Offset: 0x0006AA76
		// (set) Token: 0x06002FC8 RID: 12232 RVA: 0x0006C87E File Offset: 0x0006AA7E
		public object Options
		{
			get
			{
				return this.optionsField;
			}
			set
			{
				this.optionsField = value;
			}
		}

		// Token: 0x17000CB6 RID: 3254
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x0006C887 File Offset: 0x0006AA87
		// (set) Token: 0x06002FCA RID: 12234 RVA: 0x0006C88F File Offset: 0x0006AA8F
		public object Properties
		{
			get
			{
				return this.propertiesField;
			}
			set
			{
				this.propertiesField = value;
			}
		}

		// Token: 0x17000CB7 RID: 3255
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x0006C898 File Offset: 0x0006AA98
		// (set) Token: 0x06002FCC RID: 12236 RVA: 0x0006C8A0 File Offset: 0x0006AAA0
		public object UserSignature
		{
			get
			{
				return this.userSignatureField;
			}
			set
			{
				this.userSignatureField = value;
			}
		}

		// Token: 0x0400294B RID: 10571
		private object filtersField;

		// Token: 0x0400294C RID: 10572
		private object listsField;

		// Token: 0x0400294D RID: 10573
		private object optionsField;

		// Token: 0x0400294E RID: 10574
		private object propertiesField;

		// Token: 0x0400294F RID: 10575
		private object userSignatureField;
	}
}
