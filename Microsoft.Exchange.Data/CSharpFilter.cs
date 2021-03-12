using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x02000029 RID: 41
	internal sealed class CSharpFilter<T> : QueryFilter
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00006B0D File Offset: 0x00004D0D
		internal CSharpFilter(CSharpFilter<T>.MatchDelegate matchDelegate)
		{
			if (matchDelegate == null)
			{
				throw new ArgumentNullException("matchDelegate");
			}
			this.matchDelegate = matchDelegate;
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00006B2A File Offset: 0x00004D2A
		internal bool Match(T item)
		{
			return this.matchDelegate(item);
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006B38 File Offset: 0x00004D38
		public override bool Equals(object obj)
		{
			CSharpFilter<T> csharpFilter = obj as CSharpFilter<T>;
			return csharpFilter != null && !(csharpFilter.GetType() != base.GetType()) && object.ReferenceEquals(this.matchDelegate, csharpFilter.matchDelegate);
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00006B75 File Offset: 0x00004D75
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode() ^ this.matchDelegate.GetHashCode();
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00006B8E File Offset: 0x00004D8E
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(CSharpFilter)");
		}

		// Token: 0x04000088 RID: 136
		private CSharpFilter<T>.MatchDelegate matchDelegate;

		// Token: 0x0200002A RID: 42
		// (Invoke) Token: 0x06000176 RID: 374
		internal delegate bool MatchDelegate(T item);
	}
}
