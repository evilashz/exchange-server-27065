using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OData.Model
{
	// Token: 0x02000EC3 RID: 3779
	public class FindEntitiesResult<T> : IFindEntitiesResult<T>, IFindEntitiesResult, IEnumerable<T>, IEnumerable
	{
		// Token: 0x06006230 RID: 25136 RVA: 0x001337FB File Offset: 0x001319FB
		public FindEntitiesResult(IEnumerable<T> entities, int totalCount)
		{
			this.entities = entities;
			this.totalCount = totalCount;
		}

		// Token: 0x1700167E RID: 5758
		// (get) Token: 0x06006231 RID: 25137 RVA: 0x00133811 File Offset: 0x00131A11
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x06006232 RID: 25138 RVA: 0x00133819 File Offset: 0x00131A19
		public IEnumerator<T> GetEnumerator()
		{
			return this.entities.GetEnumerator();
		}

		// Token: 0x06006233 RID: 25139 RVA: 0x00133826 File Offset: 0x00131A26
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x040034F2 RID: 13554
		private readonly IEnumerable<T> entities;

		// Token: 0x040034F3 RID: 13555
		private readonly int totalCount;
	}
}
