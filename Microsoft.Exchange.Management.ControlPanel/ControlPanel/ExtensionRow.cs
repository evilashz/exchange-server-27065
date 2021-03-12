using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Management.Extension;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001DE RID: 478
	[DataContract]
	public class ExtensionRow : BaseRow
	{
		// Token: 0x06002595 RID: 9621 RVA: 0x00073614 File Offset: 0x00071814
		public ExtensionRow(App extension) : base(((AppId)extension.Identity).ToIdentity(), extension)
		{
			this.Name = extension.DisplayName;
			this.Enabled = extension.Enabled;
			this.Type = extension.Type.ToString();
			this.IsRemovable = (extension.Type == ExtensionType.Private);
			this.Version = extension.AppVersion;
			this.ExtensionId = extension.AppId;
		}

		// Token: 0x17001B9D RID: 7069
		// (get) Token: 0x06002596 RID: 9622 RVA: 0x000736A3 File Offset: 0x000718A3
		// (set) Token: 0x06002597 RID: 9623 RVA: 0x000736AB File Offset: 0x000718AB
		[DataMember]
		public string Name { get; protected set; }

		// Token: 0x17001B9E RID: 7070
		// (get) Token: 0x06002598 RID: 9624 RVA: 0x000736B4 File Offset: 0x000718B4
		// (set) Token: 0x06002599 RID: 9625 RVA: 0x000736BC File Offset: 0x000718BC
		[DataMember]
		public string Type { get; protected set; }

		// Token: 0x17001B9F RID: 7071
		// (get) Token: 0x0600259A RID: 9626 RVA: 0x000736C5 File Offset: 0x000718C5
		// (set) Token: 0x0600259B RID: 9627 RVA: 0x000736CD File Offset: 0x000718CD
		[DataMember]
		public bool Enabled { get; protected set; }

		// Token: 0x17001BA0 RID: 7072
		// (get) Token: 0x0600259C RID: 9628 RVA: 0x000736D6 File Offset: 0x000718D6
		// (set) Token: 0x0600259D RID: 9629 RVA: 0x000736DE File Offset: 0x000718DE
		[DataMember]
		public bool IsRemovable { get; protected set; }

		// Token: 0x17001BA1 RID: 7073
		// (get) Token: 0x0600259E RID: 9630 RVA: 0x000736E7 File Offset: 0x000718E7
		// (set) Token: 0x0600259F RID: 9631 RVA: 0x000736EF File Offset: 0x000718EF
		[DataMember]
		public string Version { get; protected set; }

		// Token: 0x17001BA2 RID: 7074
		// (get) Token: 0x060025A0 RID: 9632 RVA: 0x000736F8 File Offset: 0x000718F8
		// (set) Token: 0x060025A1 RID: 9633 RVA: 0x00073700 File Offset: 0x00071900
		[DataMember]
		public string ExtensionId { get; protected set; }
	}
}
