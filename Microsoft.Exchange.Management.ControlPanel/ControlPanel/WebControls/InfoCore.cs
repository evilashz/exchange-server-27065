using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000615 RID: 1557
	[DataContract]
	public sealed class InfoCore
	{
		// Token: 0x170026BA RID: 9914
		// (get) Token: 0x0600454F RID: 17743 RVA: 0x000D1A05 File Offset: 0x000CFC05
		// (set) Token: 0x06004550 RID: 17744 RVA: 0x000D1A0D File Offset: 0x000CFC0D
		[DataMember(EmitDefaultValue = false)]
		public string JsonTitle { get; set; }

		// Token: 0x170026BB RID: 9915
		// (get) Token: 0x06004551 RID: 17745 RVA: 0x000D1A16 File Offset: 0x000CFC16
		// (set) Token: 0x06004552 RID: 17746 RVA: 0x000D1A1E File Offset: 0x000CFC1E
		[DataMember(EmitDefaultValue = false)]
		public string Message { get; set; }

		// Token: 0x170026BC RID: 9916
		// (get) Token: 0x06004553 RID: 17747 RVA: 0x000D1A27 File Offset: 0x000CFC27
		// (set) Token: 0x06004554 RID: 17748 RVA: 0x000D1A2F File Offset: 0x000CFC2F
		[DataMember(EmitDefaultValue = false)]
		public string Details { get; set; }

		// Token: 0x170026BD RID: 9917
		// (get) Token: 0x06004555 RID: 17749 RVA: 0x000D1A38 File Offset: 0x000CFC38
		// (set) Token: 0x06004556 RID: 17750 RVA: 0x000D1A40 File Offset: 0x000CFC40
		[DataMember(EmitDefaultValue = false)]
		public ModalDialogType MessageBoxType { get; set; }

		// Token: 0x170026BE RID: 9918
		// (get) Token: 0x06004557 RID: 17751 RVA: 0x000D1A49 File Offset: 0x000CFC49
		// (set) Token: 0x06004558 RID: 17752 RVA: 0x000D1A51 File Offset: 0x000CFC51
		[DataMember(EmitDefaultValue = false)]
		public string Help { get; set; }

		// Token: 0x170026BF RID: 9919
		// (get) Token: 0x06004559 RID: 17753 RVA: 0x000D1A5A File Offset: 0x000CFC5A
		// (set) Token: 0x0600455A RID: 17754 RVA: 0x000D1A62 File Offset: 0x000CFC62
		[DataMember(EmitDefaultValue = false)]
		public string HelpUrl { get; set; }

		// Token: 0x170026C0 RID: 9920
		// (get) Token: 0x0600455B RID: 17755 RVA: 0x000D1A6B File Offset: 0x000CFC6B
		// (set) Token: 0x0600455C RID: 17756 RVA: 0x000D1A73 File Offset: 0x000CFC73
		[DataMember(EmitDefaultValue = false)]
		public string StackTrace { get; set; }
	}
}
