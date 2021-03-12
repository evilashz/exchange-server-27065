using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OnlineMeetings.ResourceContract
{
	// Token: 0x02000078 RID: 120
	internal class LinksCollection : ICollection<Link>, IEnumerable<Link>, IEnumerable
	{
		// Token: 0x06000358 RID: 856 RVA: 0x00009BC0 File Offset: 0x00007DC0
		internal LinksCollection(List<Link> links)
		{
			this.links = links;
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000359 RID: 857 RVA: 0x00009BCF File Offset: 0x00007DCF
		public int Count
		{
			get
			{
				return this.links.Count;
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600035A RID: 858 RVA: 0x00009BDC File Offset: 0x00007DDC
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600035B RID: 859 RVA: 0x00009BF4 File Offset: 0x00007DF4
		public void Add(Link item)
		{
			string relationship = item.Relationship;
			if (relationship == "related")
			{
				item = new Link(item)
				{
					Relationship = item.Token
				};
			}
			if (relationship == "self")
			{
				this.links.RemoveAll((Link link) => link.Relationship == "self");
				item = new Link(item)
				{
					Token = "self"
				};
			}
			this.links.Add(item);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x00009C82 File Offset: 0x00007E82
		public void Clear()
		{
			this.links.Clear();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x00009C8F File Offset: 0x00007E8F
		public bool Contains(Link item)
		{
			return this.links.Contains(item);
		}

		// Token: 0x0600035E RID: 862 RVA: 0x00009C9D File Offset: 0x00007E9D
		public void CopyTo(Link[] array, int arrayIndex)
		{
			this.links.CopyTo(array, arrayIndex);
		}

		// Token: 0x0600035F RID: 863 RVA: 0x00009CAC File Offset: 0x00007EAC
		public bool Remove(Link item)
		{
			return this.links.Remove(item);
		}

		// Token: 0x06000360 RID: 864 RVA: 0x00009CBA File Offset: 0x00007EBA
		public IEnumerator<Link> GetEnumerator()
		{
			return this.links.GetEnumerator();
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00009CCC File Offset: 0x00007ECC
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04000218 RID: 536
		private readonly List<Link> links;
	}
}
