using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003A2 RID: 930
	[KnownType(typeof(FileAttachmentDataProviderItem))]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[KnownType(typeof(GroupAttachmentDataProviderItem))]
	[KnownType(typeof(FolderAttachmentDataProviderItem))]
	public class AttachmentDataProviderItem
	{
		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x0007627E File Offset: 0x0007447E
		// (set) Token: 0x06001DBE RID: 7614 RVA: 0x00076286 File Offset: 0x00074486
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001DBF RID: 7615 RVA: 0x0007628F File Offset: 0x0007448F
		// (set) Token: 0x06001DC0 RID: 7616 RVA: 0x00076297 File Offset: 0x00074497
		[DataMember]
		public virtual long Size { get; set; }

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001DC1 RID: 7617 RVA: 0x000762A0 File Offset: 0x000744A0
		// (set) Token: 0x06001DC2 RID: 7618 RVA: 0x000762A8 File Offset: 0x000744A8
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001DC3 RID: 7619 RVA: 0x000762B1 File Offset: 0x000744B1
		// (set) Token: 0x06001DC4 RID: 7620 RVA: 0x000762B9 File Offset: 0x000744B9
		[DataMember]
		public string ParentId { get; set; }

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x000762C2 File Offset: 0x000744C2
		// (set) Token: 0x06001DC6 RID: 7622 RVA: 0x000762CA File Offset: 0x000744CA
		[DataMember]
		public string Location { get; set; }

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001DC7 RID: 7623 RVA: 0x000762D3 File Offset: 0x000744D3
		// (set) Token: 0x06001DC8 RID: 7624 RVA: 0x000762DB File Offset: 0x000744DB
		[DataMember]
		public AttachmentDataProviderType ProviderType { get; set; }

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001DC9 RID: 7625 RVA: 0x000762E4 File Offset: 0x000744E4
		// (set) Token: 0x06001DCA RID: 7626 RVA: 0x000762EC File Offset: 0x000744EC
		[DataMember]
		public string ProviderEndpointUrl { get; set; }

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001DCB RID: 7627 RVA: 0x000762F5 File Offset: 0x000744F5
		// (set) Token: 0x06001DCC RID: 7628 RVA: 0x000762FD File Offset: 0x000744FD
		[DateTimeString]
		[DataMember]
		public string Modified { get; set; }

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001DCD RID: 7629 RVA: 0x00076306 File Offset: 0x00074506
		// (set) Token: 0x06001DCE RID: 7630 RVA: 0x0007630E File Offset: 0x0007450E
		[DataMember]
		public string ModifiedBy { get; set; }

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001DCF RID: 7631 RVA: 0x00076317 File Offset: 0x00074517
		// (set) Token: 0x06001DD0 RID: 7632 RVA: 0x0007631F File Offset: 0x0007451F
		[DataMember]
		public virtual int ChildCount { get; set; }

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001DD1 RID: 7633 RVA: 0x00076328 File Offset: 0x00074528
		// (set) Token: 0x06001DD2 RID: 7634 RVA: 0x00076330 File Offset: 0x00074530
		[DataMember]
		public AttachmentDataProviderItemType Type { get; set; }

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001DD3 RID: 7635 RVA: 0x00076339 File Offset: 0x00074539
		// (set) Token: 0x06001DD4 RID: 7636 RVA: 0x00076341 File Offset: 0x00074541
		[DataMember]
		public string AttachmentProviderId { get; set; }
	}
}
