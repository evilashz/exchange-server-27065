using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200041E RID: 1054
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x0600352A RID: 13610 RVA: 0x000CE458 File Offset: 0x000CC658
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x0600352B RID: 13611 RVA: 0x000CE490 File Offset: 0x000CC690
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = (tags & (EventTags)268435455);
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int prefixSize = 0;
			Statics.EncodeTags((int)this.tags, ref prefixSize, null);
			this.nameMetadata = Statics.MetadataForString(name, prefixSize, 0, typeMetadataSize);
			prefixSize = 2;
			Statics.EncodeTags((int)this.tags, ref prefixSize, this.nameMetadata);
		}

		// Token: 0x0600352C RID: 13612 RVA: 0x000CE4FB File Offset: 0x000CC6FB
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x0600352D RID: 13613 RVA: 0x000CE50F File Offset: 0x000CC70F
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x0600352E RID: 13614 RVA: 0x000CE52C File Offset: 0x000CC72C
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? -1 : 1);
			}
			return num;
		}

		// Token: 0x04001790 RID: 6032
		private static int lastIdentity = 184549376;

		// Token: 0x04001791 RID: 6033
		internal readonly string name;

		// Token: 0x04001792 RID: 6034
		internal readonly EventTags tags;

		// Token: 0x04001793 RID: 6035
		internal readonly int identity;

		// Token: 0x04001794 RID: 6036
		internal readonly byte[] nameMetadata;
	}
}
