using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C7 RID: 455
	[Serializable]
	internal class DialGroupEntryComment : DialGroupEntryField
	{
		// Token: 0x06000FF0 RID: 4080 RVA: 0x0003092E File Offset: 0x0002EB2E
		public DialGroupEntryComment(string comment) : base(comment, "Comment")
		{
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x0003093C File Offset: 0x0002EB3C
		public static DialGroupEntryComment Parse(string comment)
		{
			return new DialGroupEntryComment(comment);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x00030944 File Offset: 0x0002EB44
		protected override void Validate()
		{
			if (!string.IsNullOrEmpty(base.Data))
			{
				base.ValidateMaxLength(96);
			}
		}

		// Token: 0x04000981 RID: 2433
		public const int MaxLength = 96;
	}
}
