using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000076 RID: 118
	[DataContract(Name = "Link")]
	internal class Link
	{
		// Token: 0x06000349 RID: 841 RVA: 0x00009A8D File Offset: 0x00007C8D
		public Link()
		{
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00009A95 File Offset: 0x00007C95
		public Link(string token, string href) : this(token, href, "related")
		{
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00009AA4 File Offset: 0x00007CA4
		public Link(string token, string href, string rel)
		{
			if (string.IsNullOrEmpty(token))
			{
				throw new ArgumentException("token");
			}
			if (string.IsNullOrEmpty(href))
			{
				throw new ArgumentException("href");
			}
			this.Relationship = rel;
			this.Href = href;
			this.Token = token;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00009AF4 File Offset: 0x00007CF4
		public Link(Link copy)
		{
			if (copy == null)
			{
				throw new ArgumentNullException("copy");
			}
			this.Cid = copy.Cid;
			this.Href = copy.Href;
			this.Relationship = copy.Relationship;
			this.Token = copy.Token;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600034D RID: 845 RVA: 0x00009B45 File Offset: 0x00007D45
		// (set) Token: 0x0600034E RID: 846 RVA: 0x00009B4D File Offset: 0x00007D4D
		[IgnoreDataMember]
		public string Token { get; set; }

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x0600034F RID: 847 RVA: 0x00009B56 File Offset: 0x00007D56
		// (set) Token: 0x06000350 RID: 848 RVA: 0x00009B5E File Offset: 0x00007D5E
		[DataMember(Name = "cid", EmitDefaultValue = false)]
		public string Cid { get; set; }

		// Token: 0x17000120 RID: 288
		// (get) Token: 0x06000351 RID: 849 RVA: 0x00009B67 File Offset: 0x00007D67
		// (set) Token: 0x06000352 RID: 850 RVA: 0x00009B6F File Offset: 0x00007D6F
		[DataMember(Name = "rel", EmitDefaultValue = false)]
		public string Relationship { get; set; }

		// Token: 0x17000121 RID: 289
		// (get) Token: 0x06000353 RID: 851 RVA: 0x00009B78 File Offset: 0x00007D78
		// (set) Token: 0x06000354 RID: 852 RVA: 0x00009B80 File Offset: 0x00007D80
		[DataMember(Name = "href", EmitDefaultValue = false)]
		public string Href { get; set; }

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00009B89 File Offset: 0x00007D89
		// (set) Token: 0x06000356 RID: 854 RVA: 0x00009B91 File Offset: 0x00007D91
		[IgnoreDataMember]
		public object Target { get; set; }

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000357 RID: 855 RVA: 0x00009B9C File Offset: 0x00007D9C
		public bool CanBeEmbedded
		{
			get
			{
				Resource resource = this.Target as Resource;
				return resource != null && resource.CanBeEmbedded;
			}
		}

		// Token: 0x02000077 RID: 119
		internal static class Relationships
		{
			// Token: 0x0400020E RID: 526
			public const string Added = "added";

			// Token: 0x0400020F RID: 527
			public const string Creator = "creator";

			// Token: 0x04000210 RID: 528
			public const string Deleted = "deleted";

			// Token: 0x04000211 RID: 529
			public const string Completed = "completed";

			// Token: 0x04000212 RID: 530
			public const string Next = "next";

			// Token: 0x04000213 RID: 531
			public const string Public = "public";

			// Token: 0x04000214 RID: 532
			public const string Related = "related";

			// Token: 0x04000215 RID: 533
			public const string Self = "self";

			// Token: 0x04000216 RID: 534
			public const string Sender = "sender";

			// Token: 0x04000217 RID: 535
			public const string Updated = "updated";
		}
	}
}
