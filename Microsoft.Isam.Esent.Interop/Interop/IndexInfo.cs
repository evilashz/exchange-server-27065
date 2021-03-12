using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace Microsoft.Isam.Esent.Interop
{
	// Token: 0x02000260 RID: 608
	[Serializable]
	public class IndexInfo
	{
		// Token: 0x06000A80 RID: 2688 RVA: 0x00015364 File Offset: 0x00013564
		internal IndexInfo(string name, CultureInfo cultureInfo, CompareOptions compareOptions, IndexSegment[] indexSegments, CreateIndexGrbit grbit, int keys, int entries, int pages)
		{
			this.name = name;
			this.cultureInfo = cultureInfo;
			this.compareOptions = compareOptions;
			this.indexSegments = new ReadOnlyCollection<IndexSegment>(indexSegments);
			this.grbit = grbit;
			this.keys = keys;
			this.entries = entries;
			this.pages = pages;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000A81 RID: 2689 RVA: 0x000153B9 File Offset: 0x000135B9
		public string Name
		{
			[DebuggerStepThrough]
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000A82 RID: 2690 RVA: 0x000153C1 File Offset: 0x000135C1
		public CultureInfo CultureInfo
		{
			[DebuggerStepThrough]
			get
			{
				return this.cultureInfo;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000A83 RID: 2691 RVA: 0x000153C9 File Offset: 0x000135C9
		public CompareOptions CompareOptions
		{
			[DebuggerStepThrough]
			get
			{
				return this.compareOptions;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000A84 RID: 2692 RVA: 0x000153D1 File Offset: 0x000135D1
		public IList<IndexSegment> IndexSegments
		{
			[DebuggerStepThrough]
			get
			{
				return this.indexSegments;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000A85 RID: 2693 RVA: 0x000153D9 File Offset: 0x000135D9
		public CreateIndexGrbit Grbit
		{
			[DebuggerStepThrough]
			get
			{
				return this.grbit;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x06000A86 RID: 2694 RVA: 0x000153E1 File Offset: 0x000135E1
		public int Keys
		{
			[DebuggerStepThrough]
			get
			{
				return this.keys;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x06000A87 RID: 2695 RVA: 0x000153E9 File Offset: 0x000135E9
		public int Entries
		{
			[DebuggerStepThrough]
			get
			{
				return this.entries;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x06000A88 RID: 2696 RVA: 0x000153F1 File Offset: 0x000135F1
		public int Pages
		{
			[DebuggerStepThrough]
			get
			{
				return this.pages;
			}
		}

		// Token: 0x06000A89 RID: 2697 RVA: 0x000153FC File Offset: 0x000135FC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.Name);
			stringBuilder.Append(" (");
			foreach (IndexSegment indexSegment in this.IndexSegments)
			{
				stringBuilder.Append(indexSegment.ToString());
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x04000443 RID: 1091
		private readonly string name;

		// Token: 0x04000444 RID: 1092
		private readonly CultureInfo cultureInfo;

		// Token: 0x04000445 RID: 1093
		private readonly CompareOptions compareOptions;

		// Token: 0x04000446 RID: 1094
		private readonly ReadOnlyCollection<IndexSegment> indexSegments;

		// Token: 0x04000447 RID: 1095
		private readonly CreateIndexGrbit grbit;

		// Token: 0x04000448 RID: 1096
		private readonly int keys;

		// Token: 0x04000449 RID: 1097
		private readonly int entries;

		// Token: 0x0400044A RID: 1098
		private readonly int pages;
	}
}
