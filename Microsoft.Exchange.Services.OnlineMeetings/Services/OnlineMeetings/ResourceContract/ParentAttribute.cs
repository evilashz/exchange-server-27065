using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000046 RID: 70
	[AttributeUsage(AttributeTargets.Class)]
	internal class ParentAttribute : Attribute
	{
		// Token: 0x06000249 RID: 585 RVA: 0x00007E4B File Offset: 0x0000604B
		public ParentAttribute(string parentToken)
		{
			this.parentToken = parentToken;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00007E5A File Offset: 0x0000605A
		public string ParentToken
		{
			get
			{
				return this.parentToken;
			}
		}

		// Token: 0x0400017D RID: 381
		private readonly string parentToken;
	}
}
