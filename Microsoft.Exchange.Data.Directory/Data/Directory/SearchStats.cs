using System;
using System.DirectoryServices.Protocols;
using System.Text;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	internal class SearchStats
	{
		// Token: 0x0600109D RID: 4253 RVA: 0x0005026D File Offset: 0x0004E46D
		private SearchStats()
		{
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x00050278 File Offset: 0x0004E478
		public static SearchStats Parse(byte[] value)
		{
			SearchStats searchStats = null;
			try
			{
				object[] array = BerConverter.Decode("{iiiiiiiiiaia}", value);
				searchStats = new SearchStats();
				searchStats.callTime = (int)array[3];
				searchStats.entriesReturned = (int)array[5];
				searchStats.entriesVisited = (int)array[7];
				searchStats.filter = (string)array[9];
				searchStats.index = (string)array[11];
			}
			catch (BerConversionException)
			{
			}
			catch (InvalidCastException)
			{
			}
			catch (DecoderFallbackException)
			{
			}
			if (searchStats != null)
			{
				if (!string.IsNullOrEmpty(searchStats.filter))
				{
					SearchStats searchStats2 = searchStats;
					string text = searchStats.filter;
					char[] trimChars = new char[1];
					searchStats2.filter = text.TrimEnd(trimChars);
				}
				if (string.IsNullOrEmpty(searchStats.filter))
				{
					searchStats.filter = "<null>";
				}
				if (!string.IsNullOrEmpty(searchStats.index))
				{
					SearchStats searchStats3 = searchStats;
					string text2 = searchStats.index;
					char[] trimChars2 = new char[1];
					searchStats3.index = text2.TrimEnd(trimChars2);
				}
				if (string.IsNullOrEmpty(searchStats.index))
				{
					searchStats.index = "<null>";
				}
			}
			return searchStats;
		}

		// Token: 0x170002C3 RID: 707
		// (get) Token: 0x0600109F RID: 4255 RVA: 0x00050394 File Offset: 0x0004E594
		public int EntriesReturned
		{
			get
			{
				return this.entriesReturned;
			}
		}

		// Token: 0x170002C4 RID: 708
		// (get) Token: 0x060010A0 RID: 4256 RVA: 0x0005039C File Offset: 0x0004E59C
		public int EntriesVisited
		{
			get
			{
				return this.entriesVisited;
			}
		}

		// Token: 0x170002C5 RID: 709
		// (get) Token: 0x060010A1 RID: 4257 RVA: 0x000503A4 File Offset: 0x0004E5A4
		public string Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170002C6 RID: 710
		// (get) Token: 0x060010A2 RID: 4258 RVA: 0x000503AC File Offset: 0x0004E5AC
		public string Filter
		{
			get
			{
				return this.filter;
			}
		}

		// Token: 0x170002C7 RID: 711
		// (get) Token: 0x060010A3 RID: 4259 RVA: 0x000503B4 File Offset: 0x0004E5B4
		public int CallTime
		{
			get
			{
				return this.callTime;
			}
		}

		// Token: 0x04000973 RID: 2419
		private int entriesReturned;

		// Token: 0x04000974 RID: 2420
		private int entriesVisited;

		// Token: 0x04000975 RID: 2421
		private string filter;

		// Token: 0x04000976 RID: 2422
		private string index;

		// Token: 0x04000977 RID: 2423
		private int callTime;
	}
}
