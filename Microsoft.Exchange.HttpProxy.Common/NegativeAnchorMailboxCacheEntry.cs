using System;
using System.Net;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000010 RID: 16
	internal class NegativeAnchorMailboxCacheEntry
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600004E RID: 78 RVA: 0x00003721 File Offset: 0x00001921
		// (set) Token: 0x0600004F RID: 79 RVA: 0x00003729 File Offset: 0x00001929
		public NegativeAnchorMailboxCacheEntry.CacheGeneration Generation { get; set; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000050 RID: 80 RVA: 0x00003732 File Offset: 0x00001932
		// (set) Token: 0x06000051 RID: 81 RVA: 0x0000373A File Offset: 0x0000193A
		public DateTime StartTime { get; set; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000052 RID: 82 RVA: 0x00003743 File Offset: 0x00001943
		// (set) Token: 0x06000053 RID: 83 RVA: 0x0000374B File Offset: 0x0000194B
		public HttpStatusCode ErrorCode { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000054 RID: 84 RVA: 0x00003754 File Offset: 0x00001954
		// (set) Token: 0x06000055 RID: 85 RVA: 0x0000375C File Offset: 0x0000195C
		public HttpProxySubErrorCode SubErrorCode { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00003765 File Offset: 0x00001965
		// (set) Token: 0x06000057 RID: 87 RVA: 0x0000376D File Offset: 0x0000196D
		public string SourceObject { get; set; }

		// Token: 0x06000058 RID: 88 RVA: 0x00003778 File Offset: 0x00001978
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Generation,
				'~',
				this.StartTime.ToString("s"),
				'~',
				this.ErrorCode,
				'~',
				this.SubErrorCode,
				'~',
				this.SourceObject
			});
		}

		// Token: 0x06000059 RID: 89 RVA: 0x00003804 File Offset: 0x00001A04
		public override int GetHashCode()
		{
			int num = 0;
			if (this.Generation != (NegativeAnchorMailboxCacheEntry.CacheGeneration)0)
			{
				num ^= this.Generation.GetHashCode();
			}
			num ^= this.StartTime.ToString("s").GetHashCode();
			if (this.ErrorCode != (HttpStatusCode)0)
			{
				num ^= this.ErrorCode.GetHashCode();
			}
			if (this.SubErrorCode != (HttpProxySubErrorCode)0)
			{
				num ^= this.SubErrorCode.GetHashCode();
			}
			if (!string.IsNullOrEmpty(this.SourceObject))
			{
				num ^= this.SourceObject.GetHashCode();
			}
			return num;
		}

		// Token: 0x0600005A RID: 90 RVA: 0x0000389C File Offset: 0x00001A9C
		public override bool Equals(object obj)
		{
			NegativeAnchorMailboxCacheEntry negativeAnchorMailboxCacheEntry = obj as NegativeAnchorMailboxCacheEntry;
			return negativeAnchorMailboxCacheEntry != null && this.Generation == negativeAnchorMailboxCacheEntry.Generation && this.ErrorCode == negativeAnchorMailboxCacheEntry.ErrorCode && this.SubErrorCode == negativeAnchorMailboxCacheEntry.SubErrorCode && this.StartTime == negativeAnchorMailboxCacheEntry.StartTime && ((string.IsNullOrEmpty(this.SourceObject) && string.IsNullOrEmpty(negativeAnchorMailboxCacheEntry.SourceObject)) || string.Equals(this.SourceObject, negativeAnchorMailboxCacheEntry.SourceObject));
		}

		// Token: 0x0400005E RID: 94
		private const char Separator = '~';

		// Token: 0x02000011 RID: 17
		public enum CacheGeneration : ushort
		{
			// Token: 0x04000065 RID: 101
			One = 1,
			// Token: 0x04000066 RID: 102
			Two,
			// Token: 0x04000067 RID: 103
			Max = 65535
		}
	}
}
