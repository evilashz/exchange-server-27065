using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Diagnostics
{
	// Token: 0x02000021 RID: 33
	public sealed class DiagnosticQueryResults
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00009607 File Offset: 0x00007807
		private DiagnosticQueryResults(IList<string> names, IList<Type> types, IList<uint> widths, IList<object[]> values, bool truncated, bool interrupted)
		{
			this.names = names;
			this.types = types;
			this.widths = widths;
			this.values = values;
			this.truncated = truncated;
			this.interrupted = interrupted;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060000F5 RID: 245 RVA: 0x0000963C File Offset: 0x0000783C
		public IList<string> Names
		{
			get
			{
				return this.names;
			}
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060000F6 RID: 246 RVA: 0x00009644 File Offset: 0x00007844
		public IList<Type> Types
		{
			get
			{
				return this.types;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x0000964C File Offset: 0x0000784C
		public IList<uint> Widths
		{
			get
			{
				return this.widths;
			}
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00009654 File Offset: 0x00007854
		public IList<object[]> Values
		{
			get
			{
				return this.values;
			}
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x0000965C File Offset: 0x0000785C
		public bool IsTruncated
		{
			get
			{
				return this.truncated;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060000FA RID: 250 RVA: 0x00009664 File Offset: 0x00007864
		public bool IsInterrupted
		{
			get
			{
				return this.interrupted;
			}
		}

		// Token: 0x060000FB RID: 251 RVA: 0x0000966C File Offset: 0x0000786C
		public static DiagnosticQueryResults Create(IList<string> names, IList<Type> types, IList<uint> widths, IList<object[]> values, bool truncated, bool interrupted)
		{
			return new DiagnosticQueryResults(names, types, widths, values, truncated, interrupted);
		}

		// Token: 0x040000D5 RID: 213
		private readonly IList<string> names;

		// Token: 0x040000D6 RID: 214
		private readonly IList<Type> types;

		// Token: 0x040000D7 RID: 215
		private readonly IList<uint> widths;

		// Token: 0x040000D8 RID: 216
		private readonly IList<object[]> values;

		// Token: 0x040000D9 RID: 217
		private readonly bool truncated;

		// Token: 0x040000DA RID: 218
		private readonly bool interrupted;
	}
}
