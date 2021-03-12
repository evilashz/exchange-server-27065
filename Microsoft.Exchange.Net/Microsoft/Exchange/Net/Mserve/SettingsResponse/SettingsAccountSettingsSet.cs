using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008FB RID: 2299
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DesignerCategory("code")]
	[DebuggerStepThrough]
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[Serializable]
	public class SettingsAccountSettingsSet
	{
		// Token: 0x17000D56 RID: 3414
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x00073694 File Offset: 0x00071894
		// (set) Token: 0x06003181 RID: 12673 RVA: 0x0007369C File Offset: 0x0007189C
		public SettingsCategoryResponseType Filters
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

		// Token: 0x17000D57 RID: 3415
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000736A5 File Offset: 0x000718A5
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x000736AD File Offset: 0x000718AD
		public ListsSetResponseType Lists
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

		// Token: 0x17000D58 RID: 3416
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000736B6 File Offset: 0x000718B6
		// (set) Token: 0x06003185 RID: 12677 RVA: 0x000736BE File Offset: 0x000718BE
		public SettingsCategoryResponseType Options
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

		// Token: 0x17000D59 RID: 3417
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000736C7 File Offset: 0x000718C7
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x000736CF File Offset: 0x000718CF
		public SettingsCategoryResponseType UserSignature
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

		// Token: 0x04002A9B RID: 10907
		private SettingsCategoryResponseType filtersField;

		// Token: 0x04002A9C RID: 10908
		private ListsSetResponseType listsField;

		// Token: 0x04002A9D RID: 10909
		private SettingsCategoryResponseType optionsField;

		// Token: 0x04002A9E RID: 10910
		private SettingsCategoryResponseType userSignatureField;
	}
}
