using System;
using System.Text;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200003A RID: 58
	[Serializable]
	internal abstract class ContentFilter : SinglePropertyFilter
	{
		// Token: 0x060001D2 RID: 466 RVA: 0x00007BA7 File Offset: 0x00005DA7
		public ContentFilter(PropertyDefinition property, MatchOptions matchOptions, MatchFlags matchFlags) : base(property)
		{
			this.matchOptions = matchOptions;
			this.matchFlags = matchFlags;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00007BBE File Offset: 0x00005DBE
		public MatchOptions MatchOptions
		{
			get
			{
				return this.matchOptions;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00007BC6 File Offset: 0x00005DC6
		public MatchFlags MatchFlags
		{
			get
			{
				return this.matchFlags;
			}
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x00007BD0 File Offset: 0x00005DD0
		public override void ToString(StringBuilder sb)
		{
			sb.Append("(");
			sb.Append(this.matchOptions.ToString());
			sb.Append(" ");
			sb.Append(this.matchFlags.ToString());
			sb.Append("(");
			sb.Append(base.Property.Name);
			sb.Append(")=");
			sb.Append(this.StringValue);
			sb.Append(")");
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060001D6 RID: 470
		protected abstract string StringValue { get; }

		// Token: 0x04000099 RID: 153
		private readonly MatchFlags matchFlags;

		// Token: 0x0400009A RID: 154
		private readonly MatchOptions matchOptions;
	}
}
