using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.MailboxLoadBalance.Data;
using Microsoft.Exchange.MailboxLoadBalance.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x0200000F RID: 15
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[DataContract]
	internal class Band : LoadMetric
	{
		// Token: 0x0600008E RID: 142 RVA: 0x00004338 File Offset: 0x00002538
		public Band(Band.BandProfile bandProfile, ulong minSizeMb, ulong maxSizeMb, double mailboxSizeWeightFactor, bool includeOnlyPhysicalMailboxes = false, TimeSpan? minLastLogonAge = null, TimeSpan? maxLastLogonAge = null) : base(Band.CreateBandName(bandProfile, minSizeMb, maxSizeMb, minLastLogonAge, maxLastLogonAge), true)
		{
			this.IncludeOnlyPhysicalMailboxes = includeOnlyPhysicalMailboxes;
			this.Profile = bandProfile;
			this.MailboxSizeWeightFactor = mailboxSizeWeightFactor;
			this.MaxSize = ByteQuantifiedSize.FromMB(maxSizeMb);
			this.MinSize = ByteQuantifiedSize.FromMB(minSizeMb);
			this.MinLastLogonAge = minLastLogonAge;
			this.MaxLastLogonAge = maxLastLogonAge;
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00004397 File Offset: 0x00002597
		public Band(Band.BandProfile bandProfile, ByteQuantifiedSize minSize, ByteQuantifiedSize maxSize, double mailboxSizeWeightFactor, bool includeOnlyPhysicalMailboxes = false, TimeSpan? minLastLogonAge = null, TimeSpan? maxLastLogonAge = null) : this(bandProfile, minSize.ToMB(), maxSize.ToMB(), mailboxSizeWeightFactor, includeOnlyPhysicalMailboxes, minLastLogonAge, maxLastLogonAge)
		{
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000090 RID: 144 RVA: 0x000043B6 File Offset: 0x000025B6
		// (set) Token: 0x06000091 RID: 145 RVA: 0x000043BE File Offset: 0x000025BE
		[DataMember]
		public bool IncludeOnlyPhysicalMailboxes { get; private set; }

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000092 RID: 146 RVA: 0x000043C7 File Offset: 0x000025C7
		// (set) Token: 0x06000093 RID: 147 RVA: 0x000043CF File Offset: 0x000025CF
		[DataMember]
		public double MailboxSizeWeightFactor { get; private set; }

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000094 RID: 148 RVA: 0x000043D8 File Offset: 0x000025D8
		// (set) Token: 0x06000095 RID: 149 RVA: 0x000043E0 File Offset: 0x000025E0
		[DataMember]
		public TimeSpan? MaxLastLogonAge { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x06000096 RID: 150 RVA: 0x000043E9 File Offset: 0x000025E9
		// (set) Token: 0x06000097 RID: 151 RVA: 0x000043F1 File Offset: 0x000025F1
		[DataMember]
		public ByteQuantifiedSize MaxSize { get; private set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000098 RID: 152 RVA: 0x000043FA File Offset: 0x000025FA
		// (set) Token: 0x06000099 RID: 153 RVA: 0x00004402 File Offset: 0x00002602
		[DataMember]
		public TimeSpan? MinLastLogonAge { get; private set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600009A RID: 154 RVA: 0x0000440B File Offset: 0x0000260B
		// (set) Token: 0x0600009B RID: 155 RVA: 0x00004413 File Offset: 0x00002613
		[DataMember]
		public ByteQuantifiedSize MinSize { get; private set; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600009C RID: 156 RVA: 0x0000441C File Offset: 0x0000261C
		public override string Name
		{
			get
			{
				return string.Format("Band-{0}-{1}-{2}-{3}-{4}-{5}", new object[]
				{
					this.Profile,
					this.MinSize,
					this.MaxSize,
					this.MinLastLogonAge,
					this.MaxLastLogonAge,
					this.IncludeOnlyPhysicalMailboxes
				});
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600009D RID: 157 RVA: 0x0000448F File Offset: 0x0000268F
		// (set) Token: 0x0600009E RID: 158 RVA: 0x00004497 File Offset: 0x00002697
		[DataMember]
		public Band.BandProfile Profile { get; private set; }

		// Token: 0x0600009F RID: 159 RVA: 0x000044A8 File Offset: 0x000026A8
		public bool ContainsMailbox(DirectoryMailbox mailbox)
		{
			if (mailbox is NonConnectedMailbox)
			{
				return false;
			}
			if (mailbox.PhysicalSize < this.MinSize)
			{
				return false;
			}
			if (mailbox.PhysicalSize >= this.MaxSize)
			{
				return false;
			}
			if (!mailbox.PhysicalMailboxes.Any<IPhysicalMailbox>())
			{
				return !this.IncludeOnlyPhysicalMailboxes;
			}
			TimeSpan t = mailbox.PhysicalMailboxes.Min((IPhysicalMailbox pm) => pm.LastLogonAge);
			return (this.MinLastLogonAge == null || !(t < this.MinLastLogonAge.Value)) && (this.MaxLastLogonAge == null || !(t >= this.MaxLastLogonAge.Value));
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000457B File Offset: 0x0000277B
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && (object.ReferenceEquals(this, obj) || (!(obj.GetType() != base.GetType()) && this.Equals((Band)obj)));
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000045B4 File Offset: 0x000027B4
		public override int GetHashCode()
		{
			int num = this.MaxSize.GetHashCode();
			num = (num * 397 ^ this.MinSize.GetHashCode());
			num = (num * 397 ^ (int)this.Profile);
			num = (num * 397 ^ (int)this.MailboxSizeWeightFactor);
			num = (num * 397 ^ (this.IncludeOnlyPhysicalMailboxes ? 397 : 0));
			num = (num * 397 ^ ((this.MaxLastLogonAge == null) ? 0 : this.MaxLastLogonAge.GetHashCode()));
			return num * 397 ^ ((this.MinLastLogonAge == null) ? 0 : this.MinLastLogonAge.GetHashCode());
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x00004690 File Offset: 0x00002890
		public override EntitySelector GetSelector(LoadContainer container, string constraintSetIdentity, long units)
		{
			if (this.Profile == Band.BandProfile.CountBased)
			{
				return new NumberOfEntitiesSelector(this, units, container, constraintSetIdentity);
			}
			double num = (double)units * this.MailboxSizeWeightFactor;
			return new TotalSizeEntitySelector(this, ByteQuantifiedSize.FromMB((ulong)num), container, constraintSetIdentity);
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000046C9 File Offset: 0x000028C9
		public override long GetUnitsForMailbox(DirectoryMailbox mailbox)
		{
			if (!this.ContainsMailbox(mailbox))
			{
				return 0L;
			}
			return this.CalculateMailboxWeight(mailbox);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000046E0 File Offset: 0x000028E0
		public bool IsOverlap(Band band)
		{
			return !(this.MaxSize <= band.MinSize) && !(this.MinSize >= band.MaxSize) && !(this.MaxLastLogonAge <= band.MinLastLogonAge) && !(this.MinLastLogonAge >= band.MaxLastLogonAge);
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000478A File Offset: 0x0000298A
		public override ByteQuantifiedSize ToByteQuantifiedSize(long value)
		{
			return ByteQuantifiedSize.FromMB((ulong)((double)value * this.MailboxSizeWeightFactor));
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000479C File Offset: 0x0000299C
		public override string ToString()
		{
			return string.Format("Band-{0}-(Size {1} to {2} Mb)-(LogonAge {3} to {4})-{5}-{6}", new object[]
			{
				this.Profile,
				this.MinSize.ToMB(),
				this.MaxSize.ToMB(),
				this.MinLastLogonAge,
				this.MaxLastLogonAge,
				this.IncludeOnlyPhysicalMailboxes,
				this.MailboxSizeWeightFactor
			});
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x00004830 File Offset: 0x00002A30
		protected bool Equals(Band other)
		{
			return this.MaxSize.Equals(other.MaxSize) && this.MinSize.Equals(other.MinSize) && this.Profile == other.Profile && Math.Abs(this.MailboxSizeWeightFactor - other.MailboxSizeWeightFactor) < 0.5 && this.IncludeOnlyPhysicalMailboxes == other.IncludeOnlyPhysicalMailboxes && object.Equals(this.MaxLastLogonAge, other.MaxLastLogonAge) && object.Equals(this.MinLastLogonAge, other.MinLastLogonAge);
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000048E0 File Offset: 0x00002AE0
		private static string CreateBandName(Band.BandProfile bandProfile, ulong minSizeMb, ulong maxSizeMb, TimeSpan? minLastLogonAge, TimeSpan? maxLastLogonAge)
		{
			return string.Format("Band-{0}-{1}-{2}-{3}-{4}", new object[]
			{
				(bandProfile == Band.BandProfile.SizeBased) ? "Size" : "Count",
				minSizeMb,
				maxSizeMb,
				minLastLogonAge,
				maxLastLogonAge
			});
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00004938 File Offset: 0x00002B38
		private long CalculateMailboxWeight(DirectoryMailbox mailbox)
		{
			if (this.Profile == Band.BandProfile.CountBased)
			{
				return 1L;
			}
			return (long)(mailbox.PhysicalSize.ToMB() / this.MailboxSizeWeightFactor);
		}

		// Token: 0x02000010 RID: 16
		public enum BandProfile
		{
			// Token: 0x0400003C RID: 60
			SizeBased,
			// Token: 0x0400003D RID: 61
			CountBased
		}
	}
}
