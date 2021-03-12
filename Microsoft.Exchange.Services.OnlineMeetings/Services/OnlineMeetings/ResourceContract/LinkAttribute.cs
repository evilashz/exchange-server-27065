using System;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000044 RID: 68
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal class LinkAttribute : Attribute
	{
		// Token: 0x06000239 RID: 569 RVA: 0x00007D83 File Offset: 0x00005F83
		public LinkAttribute()
		{
		}

		// Token: 0x0600023A RID: 570 RVA: 0x00007D96 File Offset: 0x00005F96
		public LinkAttribute(string token) : this(token, false)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00007DA0 File Offset: 0x00005FA0
		public LinkAttribute(string token, bool isRequired)
		{
			this.token = token;
			this.isRequired = isRequired;
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x0600023C RID: 572 RVA: 0x00007DC1 File Offset: 0x00005FC1
		public bool IsRequired
		{
			get
			{
				return this.isRequired;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600023D RID: 573 RVA: 0x00007DC9 File Offset: 0x00005FC9
		public string Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00007DD1 File Offset: 0x00005FD1
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00007DD9 File Offset: 0x00005FD9
		public string ContextToken
		{
			get
			{
				return this.contextToken;
			}
			set
			{
				this.contextToken = value;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x06000240 RID: 576 RVA: 0x00007DE2 File Offset: 0x00005FE2
		// (set) Token: 0x06000241 RID: 577 RVA: 0x00007DEA File Offset: 0x00005FEA
		public string SampleHrefContext
		{
			get
			{
				return this.sampleHrefContext;
			}
			set
			{
				this.sampleHrefContext = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00007DF3 File Offset: 0x00005FF3
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00007DFB File Offset: 0x00005FFB
		public string Rel
		{
			get
			{
				return this.rel;
			}
			set
			{
				this.rel = value;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00007E04 File Offset: 0x00006004
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00007E0C File Offset: 0x0000600C
		public string OrGroupName
		{
			get
			{
				return this.orGroupName;
			}
			set
			{
				this.orGroupName = value;
			}
		}

		// Token: 0x06000246 RID: 582 RVA: 0x00007E15 File Offset: 0x00006015
		public bool AppliesToToken(string token)
		{
			return this.contextToken == null || token == null || string.Compare(this.contextToken, token, StringComparison.CurrentCultureIgnoreCase) == 0;
		}

		// Token: 0x04000176 RID: 374
		private readonly bool isRequired;

		// Token: 0x04000177 RID: 375
		private readonly string token;

		// Token: 0x04000178 RID: 376
		private string rel = "related";

		// Token: 0x04000179 RID: 377
		private string contextToken;

		// Token: 0x0400017A RID: 378
		private string orGroupName;

		// Token: 0x0400017B RID: 379
		private string sampleHrefContext;
	}
}
