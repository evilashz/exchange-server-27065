using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Metering
{
	// Token: 0x0200000F RID: 15
	internal class CountWrapper<TEntityType, TCountType> : ICount<TEntityType, TCountType> where TEntityType : struct, IConvertible where TCountType : struct, IConvertible
	{
		// Token: 0x060000CC RID: 204 RVA: 0x00004EE1 File Offset: 0x000030E1
		public CountWrapper(Count<TEntityType, TCountType> count)
		{
			ArgumentValidator.ThrowIfNull("count", count);
			this.count = count;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000CD RID: 205 RVA: 0x00004EFB File Offset: 0x000030FB
		public ICountedEntity<TEntityType> Entity
		{
			get
			{
				return this.count.Entity;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000CE RID: 206 RVA: 0x00004F08 File Offset: 0x00003108
		public ICountedConfig Config
		{
			get
			{
				return this.count.Config;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00004F15 File Offset: 0x00003115
		public TCountType Measure
		{
			get
			{
				return this.count.Measure;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00004F22 File Offset: 0x00003122
		public bool IsPromoted
		{
			get
			{
				return this.count.IsPromoted;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000D1 RID: 209 RVA: 0x00004F2F File Offset: 0x0000312F
		public long Total
		{
			get
			{
				return this.count.Total;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004F3C File Offset: 0x0000313C
		public long Average
		{
			get
			{
				return this.count.Average;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000D3 RID: 211 RVA: 0x00004F49 File Offset: 0x00003149
		public ITrendline Trend
		{
			get
			{
				return this.count.Trend;
			}
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00004F56 File Offset: 0x00003156
		public bool TryGetObject(string key, out object value)
		{
			return this.count.TryGetObject(key, out value);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00004F65 File Offset: 0x00003165
		public void SetObject(string key, object value)
		{
			this.count.SetObject(key, value);
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00004F74 File Offset: 0x00003174
		public override string ToString()
		{
			return this.count.ToString();
		}

		// Token: 0x0400005A RID: 90
		private Count<TEntityType, TCountType> count;
	}
}
