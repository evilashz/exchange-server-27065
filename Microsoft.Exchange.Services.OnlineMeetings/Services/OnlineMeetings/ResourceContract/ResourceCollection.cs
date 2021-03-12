using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x0200007A RID: 122
	internal class ResourceCollection<T> : Resource, IEnumerable<T>, IEnumerable, IResourceCollection where T : Resource
	{
		// Token: 0x06000364 RID: 868 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public ResourceCollection() : base("collection")
		{
		}

		// Token: 0x06000365 RID: 869 RVA: 0x00009CEC File Offset: 0x00007EEC
		public ResourceCollection(IEnumerable<Resource> items) : this()
		{
			foreach (Resource target in items)
			{
				this.AddItem(target);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000366 RID: 870 RVA: 0x00009D44 File Offset: 0x00007F44
		public override bool CanBeEmbedded
		{
			get
			{
				return this.items.All((Link link) => link.CanBeEmbedded);
			}
		}

		// Token: 0x17000127 RID: 295
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00009D6E File Offset: 0x00007F6E
		public int Count
		{
			get
			{
				return this.items.Count;
			}
		}

		// Token: 0x06000368 RID: 872 RVA: 0x00009D7C File Offset: 0x00007F7C
		public void AddItem(object target = null)
		{
			this.items.Add(new Link("ignore-this-token", "ignore-this-relationship", "ignore")
			{
				Target = target
			});
		}

		// Token: 0x06000369 RID: 873 RVA: 0x00009DB1 File Offset: 0x00007FB1
		public void Add(object target = null)
		{
			this.AddItem(target);
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00009E04 File Offset: 0x00008004
		public override object ToDictionary(List<EmbeddedPart> mimeParts)
		{
			if (this.CanBeEmbedded)
			{
				return (from link in this.items
				select ((Resource)link.Target).ToDictionary(mimeParts)).ToArray<object>();
			}
			return (from link in this.items
			select new Dictionary<string, string>
			{
				{
					"href",
					link.Href
				}
			}).ToArray<Dictionary<string, string>>();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00009E83 File Offset: 0x00008083
		public IEnumerator<T> GetEnumerator()
		{
			return (from link in this.items
			select link.Target).Cast<T>().GetEnumerator();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00009EB7 File Offset: 0x000080B7
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x0400021A RID: 538
		private readonly List<Link> items = new List<Link>();
	}
}
