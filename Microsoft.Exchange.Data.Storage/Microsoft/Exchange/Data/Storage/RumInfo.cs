using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000402 RID: 1026
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class RumInfo
	{
		// Token: 0x06002EC9 RID: 11977 RVA: 0x000C0E1C File Offset: 0x000BF01C
		private RumInfo() : this(RumType.None, null)
		{
			this.SendTime = null;
		}

		// Token: 0x06002ECA RID: 11978 RVA: 0x000C0E48 File Offset: 0x000BF048
		protected RumInfo(RumType type, ExDateTime? originalStartTime)
		{
			this.Type = type;
			this.occurrenceOriginalStartTime = originalStartTime;
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x000C0E5E File Offset: 0x000BF05E
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x000C0E66 File Offset: 0x000BF066
		public RumType Type { get; private set; }

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x000C0E6F File Offset: 0x000BF06F
		public ExDateTime? OccurrenceOriginalStartTime
		{
			get
			{
				return this.occurrenceOriginalStartTime;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06002ECE RID: 11982 RVA: 0x000C0E78 File Offset: 0x000BF078
		public bool IsSuccessfullySent
		{
			get
			{
				return this.SendTime != null;
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06002ECF RID: 11983 RVA: 0x000C0E93 File Offset: 0x000BF093
		// (set) Token: 0x06002ED0 RID: 11984 RVA: 0x000C0E9B File Offset: 0x000BF09B
		public ExDateTime? SendTime { get; internal set; }

		// Token: 0x06002ED1 RID: 11985 RVA: 0x000C0EA4 File Offset: 0x000BF0A4
		protected virtual void Merge(RumInfo infoToMerge)
		{
			if (infoToMerge == null)
			{
				throw new ArgumentNullException("infoToMerge");
			}
			if (!infoToMerge.IsNullOp)
			{
				if (this.OccurrenceOriginalStartTime == null)
				{
					if (infoToMerge.OccurrenceOriginalStartTime != null)
					{
						throw new ArgumentException("Cannot merge a master RUM info with an occurrence RUM info.", "infoToMerge");
					}
				}
				else
				{
					if (infoToMerge.OccurrenceOriginalStartTime == null)
					{
						throw new ArgumentException("Cannot merge an occurrence RUM info with a master RUM info.", "infoToMerge");
					}
					if (!this.OccurrenceOriginalStartTime.Equals(infoToMerge.OccurrenceOriginalStartTime))
					{
						throw new ArgumentOutOfRangeException("infoToMerge", "Two RUMs for different occurrences cannot be merged with each other.");
					}
				}
				if (this.IsNullOp)
				{
					throw new ArgumentException("Cannot merge a NullOp RUM info with a non-NullOp RUM info.", "infoToMerge");
				}
				if (this.Type != infoToMerge.Type)
				{
					throw new ArgumentException("Two RUMs of different types cannot be merged with each other.", "infoToMerge");
				}
			}
		}

		// Token: 0x06002ED2 RID: 11986 RVA: 0x000C0F81 File Offset: 0x000BF181
		public static RumInfo Merge(RumInfo info1, RumInfo info2)
		{
			if (info1 == null)
			{
				throw new ArgumentNullException("info1");
			}
			if (info2 == null)
			{
				throw new ArgumentNullException("info2");
			}
			if (info1.IsNullOp)
			{
				return info2;
			}
			if (info2.IsNullOp)
			{
				return info1;
			}
			info1.Merge(info2);
			return info1;
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x000C0FBB File Offset: 0x000BF1BB
		public bool IsNullOp
		{
			get
			{
				return this.Type == RumType.None;
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06002ED4 RID: 11988 RVA: 0x000C0FC8 File Offset: 0x000BF1C8
		public bool IsOccurrenceRum
		{
			get
			{
				return this.OccurrenceOriginalStartTime != null;
			}
		}

		// Token: 0x0400199A RID: 6554
		private readonly ExDateTime? occurrenceOriginalStartTime;
	}
}
