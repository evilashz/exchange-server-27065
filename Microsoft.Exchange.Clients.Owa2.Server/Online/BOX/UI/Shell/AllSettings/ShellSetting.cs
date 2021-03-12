using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.UI.Shell.AllSettings
{
	// Token: 0x02000085 RID: 133
	[DataContract(Name = "ShellSetting", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.UI.Shell.AllSettings")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class ShellSetting : IExtensibleDataObject
	{
		// Token: 0x1700019D RID: 413
		// (get) Token: 0x060004E6 RID: 1254 RVA: 0x0000E851 File Offset: 0x0000CA51
		// (set) Token: 0x060004E7 RID: 1255 RVA: 0x0000E859 File Offset: 0x0000CA59
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

		// Token: 0x1700019E RID: 414
		// (get) Token: 0x060004E8 RID: 1256 RVA: 0x0000E862 File Offset: 0x0000CA62
		// (set) Token: 0x060004E9 RID: 1257 RVA: 0x0000E86A File Offset: 0x0000CA6A
		[DataMember]
		public NavBarLinkData SettingEntry
		{
			get
			{
				return this.SettingEntryField;
			}
			set
			{
				this.SettingEntryField = value;
			}
		}

		// Token: 0x040002AC RID: 684
		private ExtensionDataObject extensionDataField;

		// Token: 0x040002AD RID: 685
		private NavBarLinkData SettingEntryField;
	}
}
