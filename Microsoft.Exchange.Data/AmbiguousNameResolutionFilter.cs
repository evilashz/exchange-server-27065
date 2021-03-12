using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000031 RID: 49
	internal sealed class AmbiguousNameResolutionFilter : QueryFilter
	{
		// Token: 0x060001A5 RID: 421 RVA: 0x000072F4 File Offset: 0x000054F4
		public AmbiguousNameResolutionFilter(string valueToMatch)
		{
			if (valueToMatch == null)
			{
				throw new ArgumentNullException("valueToMatch");
			}
			this.valueToMatch = valueToMatch;
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001A6 RID: 422 RVA: 0x00007311 File Offset: 0x00005511
		public string ValueToMatch
		{
			get
			{
				return this.valueToMatch;
			}
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x00007319 File Offset: 0x00005519
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(ANR=");
			sb.Append(this.valueToMatch);
			sb.Append(")");
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x00007340 File Offset: 0x00005540
		public override bool Equals(object obj)
		{
			AmbiguousNameResolutionFilter ambiguousNameResolutionFilter = obj as AmbiguousNameResolutionFilter;
			return ambiguousNameResolutionFilter != null && ambiguousNameResolutionFilter.GetType() == base.GetType() && this.valueToMatch.Equals(ambiguousNameResolutionFilter.valueToMatch);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000737D File Offset: 0x0000557D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.valueToMatch.GetHashCode();
		}

		// Token: 0x04000092 RID: 146
		private readonly string valueToMatch;
	}
}
