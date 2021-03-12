using System;

namespace Microsoft.Forefront.Reporting.Common
{
	// Token: 0x02000011 RID: 17
	internal abstract class QueryField
	{
		// Token: 0x06000028 RID: 40 RVA: 0x0000244C File Offset: 0x0000064C
		protected internal QueryField(QueryGroupField parent, int startPos, int endPos)
		{
			this.Parent = parent;
			this.StartPosition = startPos;
			this.EndPosition = endPos;
			if (parent != null)
			{
				this.QueryString = parent.QueryString;
				this.Level = parent.Level + 1;
				this.Compiler = parent.Compiler;
				return;
			}
			this.Level = 1;
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000029 RID: 41 RVA: 0x000024A5 File Offset: 0x000006A5
		// (set) Token: 0x0600002A RID: 42 RVA: 0x000024AD File Offset: 0x000006AD
		internal string QueryString { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600002B RID: 43 RVA: 0x000024B6 File Offset: 0x000006B6
		// (set) Token: 0x0600002C RID: 44 RVA: 0x000024BE File Offset: 0x000006BE
		internal int StartPosition { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002D RID: 45 RVA: 0x000024C7 File Offset: 0x000006C7
		// (set) Token: 0x0600002E RID: 46 RVA: 0x000024CF File Offset: 0x000006CF
		internal int EndPosition { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000024D8 File Offset: 0x000006D8
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000024E0 File Offset: 0x000006E0
		internal int Level { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000031 RID: 49 RVA: 0x000024E9 File Offset: 0x000006E9
		// (set) Token: 0x06000032 RID: 50 RVA: 0x000024F1 File Offset: 0x000006F1
		internal bool HasOptionalCriterion { get; set; }

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000024FA File Offset: 0x000006FA
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00002502 File Offset: 0x00000702
		internal QueryGroupField Parent { get; set; }

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000035 RID: 53 RVA: 0x0000250B File Offset: 0x0000070B
		// (set) Token: 0x06000036 RID: 54 RVA: 0x00002513 File Offset: 0x00000713
		internal QueryCompiler Compiler { get; set; }

		// Token: 0x06000037 RID: 55
		internal abstract string Compile();

		// Token: 0x06000038 RID: 56 RVA: 0x0000251C File Offset: 0x0000071C
		internal string GetField()
		{
			return this.QueryString.Substring(this.StartPosition, this.EndPosition - this.StartPosition);
		}
	}
}
