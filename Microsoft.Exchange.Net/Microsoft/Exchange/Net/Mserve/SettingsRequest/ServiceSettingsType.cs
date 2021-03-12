using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Net.Mserve.SettingsRequest
{
	// Token: 0x020008B0 RID: 2224
	[GeneratedCode("xsd", "2.0.50727.1318")]
	[XmlType(Namespace = "HMSETTINGS:")]
	[DebuggerStepThrough]
	[DesignerCategory("code")]
	[Serializable]
	public class ServiceSettingsType
	{
		// Token: 0x17000CAB RID: 3243
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x0006C7B4 File Offset: 0x0006A9B4
		// (set) Token: 0x06002FB1 RID: 12209 RVA: 0x0006C7BC File Offset: 0x0006A9BC
		public string SafetySchemaVersion
		{
			get
			{
				return this.safetySchemaVersionField;
			}
			set
			{
				this.safetySchemaVersionField = value;
			}
		}

		// Token: 0x17000CAC RID: 3244
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x0006C7C5 File Offset: 0x0006A9C5
		// (set) Token: 0x06002FB3 RID: 12211 RVA: 0x0006C7CD File Offset: 0x0006A9CD
		public ServiceSettingsTypeSafetyLevelRules SafetyLevelRules
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

		// Token: 0x17000CAD RID: 3245
		// (get) Token: 0x06002FB4 RID: 12212 RVA: 0x0006C7D6 File Offset: 0x0006A9D6
		// (set) Token: 0x06002FB5 RID: 12213 RVA: 0x0006C7DE File Offset: 0x0006A9DE
		public ServiceSettingsTypeSafetyActions SafetyActions
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

		// Token: 0x17000CAE RID: 3246
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x0006C7E7 File Offset: 0x0006A9E7
		// (set) Token: 0x06002FB7 RID: 12215 RVA: 0x0006C7EF File Offset: 0x0006A9EF
		public ServiceSettingsTypeProperties Properties
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

		// Token: 0x17000CAF RID: 3247
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x0006C7F8 File Offset: 0x0006A9F8
		// (set) Token: 0x06002FB9 RID: 12217 RVA: 0x0006C800 File Offset: 0x0006AA00
		public ServiceSettingsTypeLists Lists
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

		// Token: 0x04002943 RID: 10563
		private string safetySchemaVersionField;

		// Token: 0x04002944 RID: 10564
		private ServiceSettingsTypeSafetyLevelRules safetyLevelRulesField;

		// Token: 0x04002945 RID: 10565
		private ServiceSettingsTypeSafetyActions safetyActionsField;

		// Token: 0x04002946 RID: 10566
		private ServiceSettingsTypeProperties propertiesField;

		// Token: 0x04002947 RID: 10567
		private ServiceSettingsTypeLists listsField;
	}
}
