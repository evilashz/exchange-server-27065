using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.SoftDeletedRemoval
{
	// Token: 0x02000104 RID: 260
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SoftDeleteMailboxRemovalCheckRemoval : IExtensibleDataObject
	{
		// Token: 0x060007B9 RID: 1977 RVA: 0x00016048 File Offset: 0x00014248
		private SoftDeleteMailboxRemovalCheckRemoval(bool canRemove, string reason)
		{
			this.CanRemove = canRemove;
			this.Reason = reason;
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060007BA RID: 1978 RVA: 0x0001605E File Offset: 0x0001425E
		// (set) Token: 0x060007BB RID: 1979 RVA: 0x00016066 File Offset: 0x00014266
		[DataMember]
		public bool CanRemove { get; set; }

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x060007BC RID: 1980 RVA: 0x0001606F File Offset: 0x0001426F
		// (set) Token: 0x060007BD RID: 1981 RVA: 0x00016077 File Offset: 0x00014277
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x060007BE RID: 1982 RVA: 0x00016080 File Offset: 0x00014280
		// (set) Token: 0x060007BF RID: 1983 RVA: 0x00016088 File Offset: 0x00014288
		[DataMember]
		public string Reason { get; set; }

		// Token: 0x060007C0 RID: 1984 RVA: 0x00016091 File Offset: 0x00014291
		public static SoftDeleteMailboxRemovalCheckRemoval AllowRemoval()
		{
			return new SoftDeleteMailboxRemovalCheckRemoval(true, string.Empty);
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001609E File Offset: 0x0001429E
		public static SoftDeleteMailboxRemovalCheckRemoval DisallowRemoval(string reasonMessage, params object[] formatArgs)
		{
			return new SoftDeleteMailboxRemovalCheckRemoval(false, string.Format(reasonMessage, formatArgs));
		}
	}
}
