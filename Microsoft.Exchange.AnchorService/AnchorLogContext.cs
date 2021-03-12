using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AnchorService
{
	// Token: 0x02000011 RID: 17
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class AnchorLogContext
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003A7C File Offset: 0x00001C7C
		private AnchorLogContext()
		{
			this.summarizables = new SortedDictionary<string, ISummarizable>();
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003AA1 File Offset: 0x00001CA1
		public static AnchorLogContext Current
		{
			get
			{
				AnchorLogContext result;
				if ((result = AnchorLogContext.context) == null)
				{
					result = (AnchorLogContext.context = new AnchorLogContext());
				}
				return result;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000BF RID: 191 RVA: 0x00003AB7 File Offset: 0x00001CB7
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00003AD2 File Offset: 0x00001CD2
		public string Source
		{
			get
			{
				if (string.IsNullOrEmpty(this.source))
				{
					return "Default";
				}
				return this.source;
			}
			set
			{
				this.source = value;
				this.isDirty = true;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x00003AE2 File Offset: 0x00001CE2
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00003AEA File Offset: 0x00001CEA
		public OrganizationId OrganizationId
		{
			get
			{
				return this.organizationId;
			}
			set
			{
				this.organizationId = value;
				this.summarizables.Clear();
				this.isDirty = true;
			}
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00003B05 File Offset: 0x00001D05
		public static void Clear()
		{
			AnchorLogContext.context = null;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00003B0D File Offset: 0x00001D0D
		public void SetSummarizable(ISummarizable summarizable)
		{
			AnchorUtil.ThrowOnNullArgument(summarizable, "summarizable");
			this.summarizables[summarizable.SummaryName] = summarizable;
			this.isDirty = true;
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00003B33 File Offset: 0x00001D33
		public void ClearSummarizable(ISummarizable summarizable)
		{
			AnchorUtil.ThrowOnNullArgument(summarizable, "summarizable");
			this.summarizables.Remove(summarizable.SummaryName);
			this.isDirty = true;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00003B5C File Offset: 0x00001D5C
		public override string ToString()
		{
			if (!this.isDirty)
			{
				return this.cachedSummary;
			}
			this.isDirty = false;
			StringBuilder stringBuilder = new StringBuilder();
			if (this.OrganizationId != null)
			{
				stringBuilder.Append(this.OrganizationId.ToString());
				stringBuilder.Append(";");
			}
			if (this.summarizables.Count > 0)
			{
				stringBuilder.Append(string.Join(";", this.SummarizablesToString()));
			}
			this.cachedSummary = stringBuilder.ToString();
			return this.cachedSummary;
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00003BE8 File Offset: 0x00001DE8
		public override int GetHashCode()
		{
			int num = 0;
			foreach (ISummarizable summarizable in this.summarizables.Values)
			{
				num += summarizable.GetHashCode();
			}
			return num;
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x00003E18 File Offset: 0x00002018
		private IEnumerable<string> SummarizablesToString()
		{
			foreach (string key in this.summarizables.Keys)
			{
				ISummarizable summarizable = this.summarizables[key];
				yield return summarizable.SummaryName + '=' + string.Join(",", summarizable.SummaryTokens);
			}
			yield break;
		}

		// Token: 0x0400004C RID: 76
		internal const string SeparatorLevelTwo = ",";

		// Token: 0x0400004D RID: 77
		internal const string SeparatorLevelOne = ";";

		// Token: 0x0400004E RID: 78
		private const string DefaultSource = "Default";

		// Token: 0x0400004F RID: 79
		[ThreadStatic]
		private static AnchorLogContext context;

		// Token: 0x04000050 RID: 80
		private string source;

		// Token: 0x04000051 RID: 81
		private OrganizationId organizationId;

		// Token: 0x04000052 RID: 82
		private SortedDictionary<string, ISummarizable> summarizables;

		// Token: 0x04000053 RID: 83
		private string cachedSummary = string.Empty;

		// Token: 0x04000054 RID: 84
		private bool isDirty = true;
	}
}
