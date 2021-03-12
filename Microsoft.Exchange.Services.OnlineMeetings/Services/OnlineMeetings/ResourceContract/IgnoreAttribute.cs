using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000041 RID: 65
	[AttributeUsage(AttributeTargets.Property)]
	internal class IgnoreAttribute : Attribute
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00007D34 File Offset: 0x00005F34
		public IgnoreAttribute()
		{
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007D3C File Offset: 0x00005F3C
		public IgnoreAttribute(string tokenContext)
		{
			this.tokenContext = tokenContext;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x06000233 RID: 563 RVA: 0x00007D4B File Offset: 0x00005F4B
		public string ContextToken
		{
			get
			{
				return this.tokenContext;
			}
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000234 RID: 564 RVA: 0x00007D53 File Offset: 0x00005F53
		// (set) Token: 0x06000235 RID: 565 RVA: 0x00007D5B File Offset: 0x00005F5B
		public int MaximumRecursionLevel { get; set; }

		// Token: 0x04000173 RID: 371
		private readonly string tokenContext;
	}
}
