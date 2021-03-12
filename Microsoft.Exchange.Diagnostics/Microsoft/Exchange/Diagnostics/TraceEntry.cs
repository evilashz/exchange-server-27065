using System;

namespace Microsoft.Exchange.Diagnostics
{
	// Token: 0x020000B0 RID: 176
	public struct TraceEntry
	{
		// Token: 0x060004A0 RID: 1184 RVA: 0x00012528 File Offset: 0x00010728
		public TraceEntry(TraceType traceType, Guid componentGuid, int traceTag, long id, string formatString, int startIndex, int nativeThreadId)
		{
			this.traceType = traceType;
			this.traceTag = traceTag;
			this.componentGuid = componentGuid;
			this.formatString = formatString;
			this.startIndex = startIndex;
			this.id = id;
			this.length = 0;
			this.timeStamp = DateTime.UtcNow.Ticks;
			this.nativeThreadId = nativeThreadId;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060004A1 RID: 1185 RVA: 0x00012584 File Offset: 0x00010784
		public Guid ComponentGuid
		{
			get
			{
				return this.componentGuid;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0001258C File Offset: 0x0001078C
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x00012594 File Offset: 0x00010794
		public string FormatString
		{
			get
			{
				return this.formatString;
			}
			internal set
			{
				this.formatString = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001259D File Offset: 0x0001079D
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x000125A5 File Offset: 0x000107A5
		public int StartIndex
		{
			get
			{
				return this.startIndex;
			}
			set
			{
				this.startIndex = value;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x000125AE File Offset: 0x000107AE
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x000125B6 File Offset: 0x000107B6
		public int Length
		{
			get
			{
				return this.length;
			}
			set
			{
				this.length = value;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x000125BF File Offset: 0x000107BF
		public DateTime Timestamp
		{
			get
			{
				return new DateTime(this.timeStamp, DateTimeKind.Utc);
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060004A9 RID: 1193 RVA: 0x000125CD File Offset: 0x000107CD
		public TraceType TraceType
		{
			get
			{
				return this.traceType;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x000125D5 File Offset: 0x000107D5
		public int TraceTag
		{
			get
			{
				return this.traceTag;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004AB RID: 1195 RVA: 0x000125DD File Offset: 0x000107DD
		public long Id
		{
			get
			{
				return this.id;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x000125E5 File Offset: 0x000107E5
		public int NativeThreadId
		{
			get
			{
				return this.nativeThreadId;
			}
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x000125ED File Offset: 0x000107ED
		public void Clear()
		{
			this.formatString = null;
			this.startIndex = 0;
			this.length = 0;
		}

		// Token: 0x04000365 RID: 869
		private string formatString;

		// Token: 0x04000366 RID: 870
		private int startIndex;

		// Token: 0x04000367 RID: 871
		private int length;

		// Token: 0x04000368 RID: 872
		private long timeStamp;

		// Token: 0x04000369 RID: 873
		private Guid componentGuid;

		// Token: 0x0400036A RID: 874
		private TraceType traceType;

		// Token: 0x0400036B RID: 875
		private int traceTag;

		// Token: 0x0400036C RID: 876
		private long id;

		// Token: 0x0400036D RID: 877
		private int nativeThreadId;
	}
}
