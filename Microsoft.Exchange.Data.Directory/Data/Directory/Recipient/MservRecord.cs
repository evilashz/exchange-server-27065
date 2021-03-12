using System;
using System.Globalization;
using System.Text.RegularExpressions;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200015E RID: 350
	internal class MservRecord
	{
		// Token: 0x1700028A RID: 650
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x00048B79 File Offset: 0x00046D79
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x00048B81 File Offset: 0x00046D81
		public string Key { get; private set; }

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x00048B8A File Offset: 0x00046D8A
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x00048B92 File Offset: 0x00046D92
		public byte ResourceId { get; private set; }

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x00048B9B File Offset: 0x00046D9B
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x00048BA3 File Offset: 0x00046DA3
		public byte Flags { get; private set; }

		// Token: 0x1700028D RID: 653
		// (get) Token: 0x06000F0C RID: 3852 RVA: 0x00048BAC File Offset: 0x00046DAC
		// (set) Token: 0x06000F0D RID: 3853 RVA: 0x00048BB4 File Offset: 0x00046DB4
		public byte UpdatedFlagsMask { get; private set; }

		// Token: 0x1700028E RID: 654
		// (get) Token: 0x06000F0E RID: 3854 RVA: 0x00048BBD File Offset: 0x00046DBD
		// (set) Token: 0x06000F0F RID: 3855 RVA: 0x00048BC5 File Offset: 0x00046DC5
		public string SourceKey { get; private set; }

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x06000F10 RID: 3856 RVA: 0x00048BCE File Offset: 0x00046DCE
		// (set) Token: 0x06000F11 RID: 3857 RVA: 0x00048BD6 File Offset: 0x00046DD6
		public string Value { get; private set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x06000F12 RID: 3858 RVA: 0x00048BDF File Offset: 0x00046DDF
		// (set) Token: 0x06000F13 RID: 3859 RVA: 0x00048BE7 File Offset: 0x00046DE7
		public MservValueFormat ValueFormat { get; private set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000F14 RID: 3860 RVA: 0x00048BF0 File Offset: 0x00046DF0
		// (set) Token: 0x06000F15 RID: 3861 RVA: 0x00048BF8 File Offset: 0x00046DF8
		public string ExoForestFqdn { get; private set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000F16 RID: 3862 RVA: 0x00048C01 File Offset: 0x00046E01
		// (set) Token: 0x06000F17 RID: 3863 RVA: 0x00048C09 File Offset: 0x00046E09
		public string ExoDatabaseId { get; private set; }

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000F18 RID: 3864 RVA: 0x00048C12 File Offset: 0x00046E12
		// (set) Token: 0x06000F19 RID: 3865 RVA: 0x00048C1A File Offset: 0x00046E1A
		public string HotmailClusterIp { get; private set; }

		// Token: 0x17000294 RID: 660
		// (get) Token: 0x06000F1A RID: 3866 RVA: 0x00048C23 File Offset: 0x00046E23
		// (set) Token: 0x06000F1B RID: 3867 RVA: 0x00048C2B File Offset: 0x00046E2B
		public string HotmailDGroupId { get; private set; }

		// Token: 0x06000F1C RID: 3868 RVA: 0x00048C34 File Offset: 0x00046E34
		public MservRecord(string key, byte resourceId, string value, string sourceKey, byte flags)
		{
			this.Key = key;
			this.ResourceId = resourceId;
			this.SourceKey = sourceKey;
			this.Flags = flags;
			if (value == null)
			{
				this.ValueFormat = (string.IsNullOrEmpty(sourceKey) ? MservValueFormat.Undefined : MservValueFormat.Alias);
				return;
			}
			Match match = MservRecord.MservExoValueRegex.Match(value);
			if (match.Success)
			{
				this.ValueFormat = MservValueFormat.Exo;
				this.ExoForestFqdn = match.Result("${resourceForestFqdn}");
				this.ExoDatabaseId = match.Result("${guidString}");
				this.Value = value;
				return;
			}
			match = MservRecord.MservHotmailValueRegex.Match(value);
			if (match.Success)
			{
				this.ValueFormat = MservValueFormat.Hotmail;
				this.HotmailClusterIp = match.Result("${clusterIp}");
				this.HotmailDGroupId = match.Result("${dGroup}");
				this.Value = value;
				return;
			}
			this.ValueFormat = MservValueFormat.Unknown;
			this.Value = value;
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x00048D14 File Offset: 0x00046F14
		public MservRecord GetUpdatedExoRecord(string exoForestFqdn, string exoDatabaseId)
		{
			if (this.ValueFormat == MservValueFormat.Hotmail)
			{
				throw new MservOperationException(DirectoryStrings.RecordValueFormatChange(this.Key, this.Value));
			}
			string value = string.Format("{0}{1} {2}", "EXO:", exoForestFqdn, exoDatabaseId);
			return new MservRecord(this.Key, this.ResourceId, value, this.SourceKey, this.Flags);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x00048D74 File Offset: 0x00046F74
		public MservRecord GetUpdatedHotmailRecord(string hotmailClusterIp, string hotmailDGroupId)
		{
			if (this.ValueFormat == MservValueFormat.Exo)
			{
				throw new MservOperationException(DirectoryStrings.RecordValueFormatChange(this.Key, this.Value));
			}
			string value = string.Format("{0} {1}", hotmailClusterIp, hotmailDGroupId);
			return new MservRecord(this.Key, this.ResourceId, value, this.SourceKey, this.Flags)
			{
				ValueFormat = MservValueFormat.Hotmail,
				HotmailClusterIp = hotmailClusterIp,
				HotmailDGroupId = hotmailDGroupId
			};
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x00048DE4 File Offset: 0x00046FE4
		public MservRecord GetUpdatedRecord(byte newResourceId)
		{
			return new MservRecord(this.Key, newResourceId, this.Value, this.SourceKey, this.Flags);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x00048E14 File Offset: 0x00047014
		public MservRecord GetUpdatedRecordFlag(bool flagValue, byte mask)
		{
			byte flags = flagValue ? (this.Flags | mask) : (this.Flags & ~mask);
			MservRecord mservRecord = new MservRecord(this.Key, this.ResourceId, this.Value, this.SourceKey, flags);
			MservRecord mservRecord2 = mservRecord;
			mservRecord2.UpdatedFlagsMask |= mask;
			return mservRecord;
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000F21 RID: 3873 RVA: 0x00048E68 File Offset: 0x00047068
		public bool IsEmpty
		{
			get
			{
				switch (this.ValueFormat)
				{
				case MservValueFormat.Undefined:
					return true;
				case MservValueFormat.Exo:
				case MservValueFormat.Hotmail:
				case MservValueFormat.Unknown:
					return string.IsNullOrWhiteSpace(this.Value);
				case MservValueFormat.Alias:
					return string.IsNullOrEmpty(this.SourceKey);
				default:
					throw new ArgumentException("ValueFormat");
				}
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000F22 RID: 3874 RVA: 0x00048EBC File Offset: 0x000470BC
		public bool IsXmr
		{
			get
			{
				return this.HotmailClusterIp == "65.54.241.216" && this.HotmailDGroupId == "51999";
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x00048EE4 File Offset: 0x000470E4
		public override string ToString()
		{
			return string.Format("Key:{0}, ResourceId:{1}, Value:{2}, SourceKey:{3}", new object[]
			{
				this.Key,
				this.ResourceId,
				this.Value,
				this.SourceKey
			});
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x00048F2C File Offset: 0x0004712C
		public static string KeyFromPuid(ulong puid)
		{
			return string.Format("({0:X16})", puid);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x00048F40 File Offset: 0x00047140
		public bool TryGetPuid(out ulong puid)
		{
			puid = 0UL;
			return this.Key.StartsWith("(") && this.Key.EndsWith(")") && ulong.TryParse(this.Key.Substring(1, this.Key.Length - 2), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out puid);
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x00048FA0 File Offset: 0x000471A0
		public bool SameRecord(MservRecord record)
		{
			return record != null && string.Equals(this.Key, record.Key, StringComparison.OrdinalIgnoreCase) && this.ResourceId == record.ResourceId;
		}

		// Token: 0x040008BD RID: 2237
		private const string ExoPrefix = "EXO:";

		// Token: 0x040008BE RID: 2238
		private static readonly Regex MservExoValueRegex = new Regex("EXO:(?<resourceForestFqdn>\\S+)\\s+(?<guidString>\\S+)", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));

		// Token: 0x040008BF RID: 2239
		private static readonly Regex MservHotmailValueRegex = new Regex("(?<clusterIp>\\S+)\\s+(?<dGroup>\\S+)", RegexOptions.IgnoreCase | RegexOptions.Compiled, TimeSpan.FromSeconds(1.0));
	}
}
