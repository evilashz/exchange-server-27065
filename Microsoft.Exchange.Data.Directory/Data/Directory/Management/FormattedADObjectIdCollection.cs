using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.Data.Directory.Management
{
	// Token: 0x020006FE RID: 1790
	[Serializable]
	public class FormattedADObjectIdCollection
	{
		// Token: 0x0600544B RID: 21579 RVA: 0x00131A45 File Offset: 0x0012FC45
		internal FormattedADObjectIdCollection(IEnumerable<ADObjectId> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this.objs = collection;
		}

		// Token: 0x17001C00 RID: 7168
		// (get) Token: 0x0600544C RID: 21580 RVA: 0x00131A62 File Offset: 0x0012FC62
		public IEnumerable<ADObjectId> AssignmentChains
		{
			get
			{
				return this.objs;
			}
		}

		// Token: 0x0600544D RID: 21581 RVA: 0x00131A6C File Offset: 0x0012FC6C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (ADObjectId adobjectId in this.objs)
			{
				if (!flag)
				{
					stringBuilder.Append('\\');
				}
				else
				{
					flag = false;
				}
				stringBuilder.Append(adobjectId.Name);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0400389C RID: 14492
		private IEnumerable<ADObjectId> objs;
	}
}
