using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class BandData : Band
	{
		// Token: 0x060000B2 RID: 178 RVA: 0x00005208 File Offset: 0x00003408
		public BandData(Band band) : base(band.Profile, band.MinSize, band.MaxSize, band.MailboxSizeWeightFactor, band.IncludeOnlyPhysicalMailboxes, band.MinLastLogonAge, band.MaxLastLogonAge)
		{
			this.Band = band;
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x060000B3 RID: 179 RVA: 0x00005241 File Offset: 0x00003441
		// (set) Token: 0x060000B4 RID: 180 RVA: 0x00005249 File Offset: 0x00003449
		[DataMember]
		public Band Band { get; private set; }

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x060000B5 RID: 181 RVA: 0x00005252 File Offset: 0x00003452
		// (set) Token: 0x060000B6 RID: 182 RVA: 0x0000525A File Offset: 0x0000345A
		public LoadContainer Database { get; set; }

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x060000B7 RID: 183 RVA: 0x00005263 File Offset: 0x00003463
		// (set) Token: 0x060000B8 RID: 184 RVA: 0x0000526B File Offset: 0x0000346B
		[DataMember]
		public int TotalWeight { get; set; }

		// Token: 0x060000B9 RID: 185 RVA: 0x00005274 File Offset: 0x00003474
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((BandData)obj)));
		}

		// Token: 0x060000BA RID: 186 RVA: 0x000052AD File Offset: 0x000034AD
		public override int GetHashCode()
		{
			return base.GetHashCode() * 397 ^ ((this.Band != null) ? this.Band.GetHashCode() : 0);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x000052D8 File Offset: 0x000034D8
		private bool Equals(BandData other)
		{
			return base.Equals(other) && object.Equals(this.Band, other.Band);
		}
	}
}
