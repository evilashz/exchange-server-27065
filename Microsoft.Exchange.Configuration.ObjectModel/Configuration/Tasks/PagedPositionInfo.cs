using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x0200007F RID: 127
	[Serializable]
	public class PagedPositionInfo : IConfigurable
	{
		// Token: 0x060004F2 RID: 1266 RVA: 0x00011B67 File Offset: 0x0000FD67
		public PagedPositionInfo(int offset, int totalCount)
		{
			if (0 > offset)
			{
				throw new ArgumentException("Offset cannot be less than 0", "offset");
			}
			if (0 > totalCount)
			{
				throw new ArgumentException("Total count cannot be less than 0", "totalCount");
			}
			this.pageOffset = offset;
			this.totalCount = totalCount;
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x00011BA5 File Offset: 0x0000FDA5
		public int PageOffset
		{
			get
			{
				return this.pageOffset;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00011BAD File Offset: 0x0000FDAD
		public int TotalCount
		{
			get
			{
				return this.totalCount;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060004F5 RID: 1269 RVA: 0x00011BB5 File Offset: 0x0000FDB5
		ObjectId IConfigurable.Identity
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x00011BBC File Offset: 0x0000FDBC
		ValidationError[] IConfigurable.Validate()
		{
			throw new NotImplementedException();
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060004F7 RID: 1271 RVA: 0x00011BC3 File Offset: 0x0000FDC3
		bool IConfigurable.IsValid
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060004F8 RID: 1272 RVA: 0x00011BCA File Offset: 0x0000FDCA
		ObjectState IConfigurable.ObjectState
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x00011BD1 File Offset: 0x0000FDD1
		void IConfigurable.CopyChangesFrom(IConfigurable source)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060004FA RID: 1274 RVA: 0x00011BD8 File Offset: 0x0000FDD8
		void IConfigurable.ResetChangeTracking()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400011D RID: 285
		private int pageOffset;

		// Token: 0x0400011E RID: 286
		private int totalCount;
	}
}
