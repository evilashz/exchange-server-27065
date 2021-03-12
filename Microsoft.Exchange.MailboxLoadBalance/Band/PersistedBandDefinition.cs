using System;
using System.Xml.Serialization;
using Microsoft.Exchange.AnchorService;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.MailboxLoadBalance.Band
{
	// Token: 0x02000020 RID: 32
	public class PersistedBandDefinition : XMLSerializableBase
	{
		// Token: 0x06000105 RID: 261 RVA: 0x000063B8 File Offset: 0x000045B8
		public PersistedBandDefinition()
		{
			this.BandProfile = Band.BandProfile.CountBased;
			this.MaxSize = 0UL;
			this.MinSize = 0UL;
			this.IsEnabled = false;
			this.IncludeOnlyPhysicalMailboxes = true;
			this.MinLastLogonAgeTicks = null;
			this.MaxLastLogonAgeTicks = null;
			this.WeightFactor = 30.0;
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00006420 File Offset: 0x00004620
		internal PersistedBandDefinition(Band band, bool isEnabled = false)
		{
			AnchorUtil.ThrowOnNullArgument(band, "band");
			this.BandProfile = band.Profile;
			this.MaxSize = band.MaxSize.ToBytes();
			this.MinSize = band.MinSize.ToBytes();
			this.IsEnabled = isEnabled;
			this.IncludeOnlyPhysicalMailboxes = band.IncludeOnlyPhysicalMailboxes;
			this.MinLastLogonAgeTicks = PersistedBandDefinition.GetPersistableLogonAge(band.MinLastLogonAge);
			this.MaxLastLogonAgeTicks = PersistedBandDefinition.GetPersistableLogonAge(band.MaxLastLogonAge);
			this.WeightFactor = band.MailboxSizeWeightFactor;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000107 RID: 263 RVA: 0x000064B3 File Offset: 0x000046B3
		// (set) Token: 0x06000108 RID: 264 RVA: 0x000064BB File Offset: 0x000046BB
		[XmlElement(ElementName = "MinLogonAge")]
		public long? MinLastLogonAgeTicks { get; set; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000109 RID: 265 RVA: 0x000064C4 File Offset: 0x000046C4
		// (set) Token: 0x0600010A RID: 266 RVA: 0x000064CC File Offset: 0x000046CC
		[XmlElement(ElementName = "MaxLogonAge")]
		public long? MaxLastLogonAgeTicks { get; set; }

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000064D5 File Offset: 0x000046D5
		// (set) Token: 0x0600010C RID: 268 RVA: 0x000064DD File Offset: 0x000046DD
		[XmlElement(ElementName = "IncludeOnlyPhysicalMailboxes")]
		public bool IncludeOnlyPhysicalMailboxes { get; set; }

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600010D RID: 269 RVA: 0x000064E6 File Offset: 0x000046E6
		// (set) Token: 0x0600010E RID: 270 RVA: 0x000064EE File Offset: 0x000046EE
		[XmlElement(ElementName = "MinSize")]
		public ulong MinSize { get; set; }

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x0600010F RID: 271 RVA: 0x000064F7 File Offset: 0x000046F7
		// (set) Token: 0x06000110 RID: 272 RVA: 0x000064FF File Offset: 0x000046FF
		[XmlElement(ElementName = "MaxSize")]
		public ulong MaxSize { get; set; }

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000111 RID: 273 RVA: 0x00006508 File Offset: 0x00004708
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006510 File Offset: 0x00004710
		[XmlElement(ElementName = "BandProfileInt")]
		public int BandProfileInt
		{
			get
			{
				return (int)this.BandProfile;
			}
			set
			{
				this.BandProfile = (Band.BandProfile)value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00006519 File Offset: 0x00004719
		// (set) Token: 0x06000114 RID: 276 RVA: 0x00006521 File Offset: 0x00004721
		[XmlElement(ElementName = "IsEnabled")]
		public bool IsEnabled { get; set; }

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000115 RID: 277 RVA: 0x0000652A File Offset: 0x0000472A
		// (set) Token: 0x06000116 RID: 278 RVA: 0x00006532 File Offset: 0x00004732
		[XmlElement(ElementName = "WeightFactor")]
		public double WeightFactor { get; set; }

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000117 RID: 279 RVA: 0x0000653B File Offset: 0x0000473B
		// (set) Token: 0x06000118 RID: 280 RVA: 0x00006543 File Offset: 0x00004743
		[XmlIgnore]
		internal Band.BandProfile BandProfile { get; set; }

		// Token: 0x06000119 RID: 281 RVA: 0x0000654C File Offset: 0x0000474C
		internal Band ToBand()
		{
			return new Band(this.BandProfile, ByteQuantifiedSize.FromBytes(this.MinSize), ByteQuantifiedSize.FromBytes(this.MaxSize), this.WeightFactor, this.IncludeOnlyPhysicalMailboxes, PersistedBandDefinition.FromPersistableLogonAge(this.MinLastLogonAgeTicks), PersistedBandDefinition.FromPersistableLogonAge(this.MaxLastLogonAgeTicks));
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000659C File Offset: 0x0000479C
		internal bool Matches(Band band)
		{
			return object.Equals(this.ToBand(), band);
		}

		// Token: 0x0600011B RID: 283 RVA: 0x000065AC File Offset: 0x000047AC
		private static TimeSpan? FromPersistableLogonAge(long? logonAgeTicks)
		{
			if (logonAgeTicks == null)
			{
				return null;
			}
			return new TimeSpan?(TimeSpan.FromTicks(logonAgeTicks.Value));
		}

		// Token: 0x0600011C RID: 284 RVA: 0x000065E0 File Offset: 0x000047E0
		private static long? GetPersistableLogonAge(TimeSpan? logonAge)
		{
			if (logonAge == null)
			{
				return null;
			}
			return new long?(logonAge.Value.Ticks);
		}
	}
}
