using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020001C4 RID: 452
	[Serializable]
	internal class DialGroupEntryName : DialGroupEntryField
	{
		// Token: 0x06000FE5 RID: 4069 RVA: 0x0003086F File Offset: 0x0002EA6F
		public DialGroupEntryName(string name) : base((name == null) ? null : name.Trim(), "Name")
		{
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x00030888 File Offset: 0x0002EA88
		public static DialGroupEntryName Parse(string name)
		{
			return new DialGroupEntryName(name);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x00030890 File Offset: 0x0002EA90
		protected override void Validate()
		{
			base.ValidateNullOrEmpty();
			base.ValidateMaxLength(32);
		}

		// Token: 0x0400097A RID: 2426
		public const int MaxLength = 32;
	}
}
