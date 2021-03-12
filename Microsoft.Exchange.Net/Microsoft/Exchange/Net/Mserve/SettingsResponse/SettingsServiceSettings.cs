using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsResponse
{
	// Token: 0x020008EC RID: 2284
	[XmlType(AnonymousType = true, Namespace = "HMSETTINGS:")]
	[DesignerCategory("code")]
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[DebuggerStepThrough]
	[Serializable]
	public class SettingsServiceSettings
	{
		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x06003132 RID: 12594 RVA: 0x00073403 File Offset: 0x00071603
		// (set) Token: 0x06003133 RID: 12595 RVA: 0x0007340B File Offset: 0x0007160B
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

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x06003134 RID: 12596 RVA: 0x00073414 File Offset: 0x00071614
		// (set) Token: 0x06003135 RID: 12597 RVA: 0x0007341C File Offset: 0x0007161C
		public RulesResponseType SafetyLevelRules
		{
			get
			{
				return this.safetyLevelRulesField;
			}
			set
			{
				this.safetyLevelRulesField = value;
			}
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x06003136 RID: 12598 RVA: 0x00073425 File Offset: 0x00071625
		// (set) Token: 0x06003137 RID: 12599 RVA: 0x0007342D File Offset: 0x0007162D
		public RulesResponseType SafetyActions
		{
			get
			{
				return this.safetyActionsField;
			}
			set
			{
				this.safetyActionsField = value;
			}
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x06003138 RID: 12600 RVA: 0x00073436 File Offset: 0x00071636
		// (set) Token: 0x06003139 RID: 12601 RVA: 0x0007343E File Offset: 0x0007163E
		public SettingsServiceSettingsProperties Properties
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

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x0600313A RID: 12602 RVA: 0x00073447 File Offset: 0x00071647
		// (set) Token: 0x0600313B RID: 12603 RVA: 0x0007344F File Offset: 0x0007164F
		public SettingsServiceSettingsLists Lists
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

		// Token: 0x04002A6B RID: 10859
		private int statusField;

		// Token: 0x04002A6C RID: 10860
		private RulesResponseType safetyLevelRulesField;

		// Token: 0x04002A6D RID: 10861
		private RulesResponseType safetyActionsField;

		// Token: 0x04002A6E RID: 10862
		private SettingsServiceSettingsProperties propertiesField;

		// Token: 0x04002A6F RID: 10863
		private SettingsServiceSettingsLists listsField;
	}
}
