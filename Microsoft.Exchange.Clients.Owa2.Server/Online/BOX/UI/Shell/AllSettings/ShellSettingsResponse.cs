using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell.AllSettings
{
	// Token: 0x02000084 RID: 132
	[DebuggerStepThrough]
	[DataContract(Name = "ShellSettingsResponse", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell.AllSettings")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class ShellSettingsResponse : IExtensibleDataObject
	{
		// Token: 0x17000198 RID: 408
		// (get) Token: 0x060004DB RID: 1243 RVA: 0x0000E7F4 File Offset: 0x0000C9F4
		// (set) Token: 0x060004DC RID: 1244 RVA: 0x0000E7FC File Offset: 0x0000C9FC
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x17000199 RID: 409
		// (get) Token: 0x060004DD RID: 1245 RVA: 0x0000E805 File Offset: 0x0000CA05
		// (set) Token: 0x060004DE RID: 1246 RVA: 0x0000E80D File Offset: 0x0000CA0D
		[DataMember]
		public string AllSettingsControlCSS
		{
			get
			{
				return this.AllSettingsControlCSSField;
			}
			set
			{
				this.AllSettingsControlCSSField = value;
			}
		}

		// Token: 0x1700019A RID: 410
		// (get) Token: 0x060004DF RID: 1247 RVA: 0x0000E816 File Offset: 0x0000CA16
		// (set) Token: 0x060004E0 RID: 1248 RVA: 0x0000E81E File Offset: 0x0000CA1E
		[DataMember]
		public string AllSettingsControlJS
		{
			get
			{
				return this.AllSettingsControlJSField;
			}
			set
			{
				this.AllSettingsControlJSField = value;
			}
		}

		// Token: 0x1700019B RID: 411
		// (get) Token: 0x060004E1 RID: 1249 RVA: 0x0000E827 File Offset: 0x0000CA27
		// (set) Token: 0x060004E2 RID: 1250 RVA: 0x0000E82F File Offset: 0x0000CA2F
		[DataMember]
		public string ClientData
		{
			get
			{
				return this.ClientDataField;
			}
			set
			{
				this.ClientDataField = value;
			}
		}

		// Token: 0x1700019C RID: 412
		// (get) Token: 0x060004E3 RID: 1251 RVA: 0x0000E838 File Offset: 0x0000CA38
		// (set) Token: 0x060004E4 RID: 1252 RVA: 0x0000E840 File Offset: 0x0000CA40
		[DataMember]
		public ShellSetting[] ShellSettings
		{
			get
			{
				return this.ShellSettingsField;
			}
			set
			{
				this.ShellSettingsField = value;
			}
		}

		// Token: 0x040002A7 RID: 679
		private ExtensionDataObject extensionDataField;

		// Token: 0x040002A8 RID: 680
		private string AllSettingsControlCSSField;

		// Token: 0x040002A9 RID: 681
		private string AllSettingsControlJSField;

		// Token: 0x040002AA RID: 682
		private string ClientDataField;

		// Token: 0x040002AB RID: 683
		private ShellSetting[] ShellSettingsField;
	}
}
