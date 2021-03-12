using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200009D RID: 157
	[DataContract]
	internal sealed class CategorySettings
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x0600062A RID: 1578 RVA: 0x0000AFBA File Offset: 0x000091BA
		// (set) Token: 0x0600062B RID: 1579 RVA: 0x0000AFC2 File Offset: 0x000091C2
		[DataMember]
		public ushort Id { get; set; }

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0000AFCB File Offset: 0x000091CB
		// (set) Token: 0x0600062D RID: 1581 RVA: 0x0000AFD3 File Offset: 0x000091D3
		[DataMember]
		public string Name { get; set; }

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600062E RID: 1582 RVA: 0x0000AFDC File Offset: 0x000091DC
		// (set) Token: 0x0600062F RID: 1583 RVA: 0x0000AFE4 File Offset: 0x000091E4
		[DataMember]
		public string ColorCode { get; set; }

		// Token: 0x17000203 RID: 515
		// (get) Token: 0x06000630 RID: 1584 RVA: 0x0000AFED File Offset: 0x000091ED
		// (set) Token: 0x06000631 RID: 1585 RVA: 0x0000AFF5 File Offset: 0x000091F5
		[DataMember]
		public int AttributeFlagsInt { get; set; }

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x06000632 RID: 1586 RVA: 0x0000AFFE File Offset: 0x000091FE
		// (set) Token: 0x06000633 RID: 1587 RVA: 0x0000B006 File Offset: 0x00009206
		public OlcCategoryAttributeFlags AttributeFlags
		{
			get
			{
				return (OlcCategoryAttributeFlags)this.AttributeFlagsInt;
			}
			set
			{
				this.AttributeFlagsInt = (int)value;
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x06000634 RID: 1588 RVA: 0x0000B00F File Offset: 0x0000920F
		// (set) Token: 0x06000635 RID: 1589 RVA: 0x0000B017 File Offset: 0x00009217
		[DataMember]
		public byte ViewTypeByte { get; set; }

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x06000636 RID: 1590 RVA: 0x0000B020 File Offset: 0x00009220
		// (set) Token: 0x06000637 RID: 1591 RVA: 0x0000B028 File Offset: 0x00009228
		[DataMember]
		public DateTime LastWrite { get; set; }
	}
}
