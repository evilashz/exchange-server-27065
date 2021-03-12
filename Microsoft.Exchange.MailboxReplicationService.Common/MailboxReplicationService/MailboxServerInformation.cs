using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200003D RID: 61
	[DataContract]
	internal sealed class MailboxServerInformation
	{
		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x000053F5 File Offset: 0x000035F5
		// (set) Token: 0x060002F3 RID: 755 RVA: 0x000053FD File Offset: 0x000035FD
		[DataMember]
		public string MailboxServerName { get; set; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x00005406 File Offset: 0x00003606
		// (set) Token: 0x060002F5 RID: 757 RVA: 0x0000540E File Offset: 0x0000360E
		[DataMember]
		public int MailboxServerVersion { get; set; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00005417 File Offset: 0x00003617
		// (set) Token: 0x060002F7 RID: 759 RVA: 0x0000541F File Offset: 0x0000361F
		[DataMember]
		public string ProxyServerName { get; set; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00005428 File Offset: 0x00003628
		// (set) Token: 0x060002F9 RID: 761 RVA: 0x00005430 File Offset: 0x00003630
		[DataMember]
		public VersionInformation ProxyServerVersion { get; set; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x060002FA RID: 762 RVA: 0x00005439 File Offset: 0x00003639
		// (set) Token: 0x060002FB RID: 763 RVA: 0x00005441 File Offset: 0x00003641
		[DataMember]
		public Guid MailboxServerGuid { get; set; }

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0000544A File Offset: 0x0000364A
		// (set) Token: 0x060002FD RID: 765 RVA: 0x00005452 File Offset: 0x00003652
		[DataMember]
		public uint MailboxSignatureVersion { get; set; }

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x060002FE RID: 766 RVA: 0x0000545B File Offset: 0x0000365B
		// (set) Token: 0x060002FF RID: 767 RVA: 0x00005463 File Offset: 0x00003663
		[DataMember]
		public uint DeleteMailboxVersion { get; set; }

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000546C File Offset: 0x0000366C
		// (set) Token: 0x06000301 RID: 769 RVA: 0x00005474 File Offset: 0x00003674
		[DataMember]
		public uint InTransitStatusVersion { get; set; }

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000547D File Offset: 0x0000367D
		// (set) Token: 0x06000303 RID: 771 RVA: 0x00005485 File Offset: 0x00003685
		[DataMember]
		public uint MailboxShapeVersion { get; set; }

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00005490 File Offset: 0x00003690
		public LocalizedString ServerInfoString
		{
			get
			{
				ServerVersion serverVersion = new ServerVersion(this.MailboxServerVersion);
				if (string.IsNullOrEmpty(this.ProxyServerName))
				{
					return MrsStrings.MailboxServerInformation(this.MailboxServerName, serverVersion.ToString());
				}
				return MrsStrings.RemoteMailboxServerInformation(this.MailboxServerName, serverVersion.ToString(), this.ProxyServerName, this.ProxyServerVersion.ToString());
			}
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000054EC File Offset: 0x000036EC
		public override string ToString()
		{
			return this.ServerInfoString.ToString();
		}
	}
}
