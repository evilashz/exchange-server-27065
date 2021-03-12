using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000C8 RID: 200
	internal class ExFolderSearchComplianceItemPagedReader : ComplianceItemPagedReader
	{
		// Token: 0x06000876 RID: 2166 RVA: 0x00022053 File Offset: 0x00020253
		public ExFolderSearchComplianceItemPagedReader(ExFolderComplianceItemContainer folderContainer) : base(20, null)
		{
			this.folderContainer = folderContainer;
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x06000877 RID: 2167 RVA: 0x00022065 File Offset: 0x00020265
		public override string PageCookie
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0002206C File Offset: 0x0002026C
		public override IEnumerable<ComplianceItem> GetNextPage()
		{
			if (this.reachedEnd)
			{
				return null;
			}
			List<ComplianceItem> list = new List<ComplianceItem>();
			QueryResult queryResult = this.folderContainer.Folder.ItemQuery(ItemQueryType.None, null, null, ExMailComplianceItem.MailDataColumns);
			queryResult.SeekToOffset(SeekReference.OriginBeginning, this.currentPage++ * base.PageSize);
			object[][] rows = queryResult.GetRows(base.PageSize);
			if (rows.Length != base.PageSize)
			{
				this.reachedEnd = true;
			}
			for (int i = 0; i < rows.Length; i++)
			{
				list.Add(new ExMailComplianceItem(this.folderContainer.Session, rows[i]));
			}
			return list;
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0002210B File Offset: 0x0002030B
		protected override void Dispose(bool isDisposing)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00022112 File Offset: 0x00020312
		protected override string GenerateQuery()
		{
			return string.Empty;
		}

		// Token: 0x040003BE RID: 958
		private const int DefaultPageSize = 20;

		// Token: 0x040003BF RID: 959
		private int currentPage;

		// Token: 0x040003C0 RID: 960
		private bool reachedEnd;

		// Token: 0x040003C1 RID: 961
		private ExFolderComplianceItemContainer folderContainer;
	}
}
