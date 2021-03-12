using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.CompliancePolicy.ComplianceData;

namespace Microsoft.Exchange.Data.ApplicationLogic.Compliance
{
	// Token: 0x020000CC RID: 204
	internal class ExMailboxSearchComplianceItemPagedReader : ComplianceItemPagedReader
	{
		// Token: 0x060008A0 RID: 2208 RVA: 0x000227A3 File Offset: 0x000209A3
		public ExMailboxSearchComplianceItemPagedReader(ExMailboxComplianceItemContainer mailboxContainer) : base(20, null)
		{
			this.mailboxContainer = mailboxContainer;
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x000227B5 File Offset: 0x000209B5
		public override string PageCookie
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x0002285C File Offset: 0x00020A5C
		public override IEnumerable<ComplianceItem> GetNextPage()
		{
			if (this.reachedEnd)
			{
				return null;
			}
			List<ComplianceItem> items = new List<ComplianceItem>();
			AllItemsFolderHelper.RunQueryOnAllItemsFolder<bool>(this.mailboxContainer.Session, AllItemsFolderHelper.SupportedSortBy.ReceivedTime, delegate(QueryResult queryResults)
			{
				queryResults.SeekToOffset(SeekReference.OriginBeginning, this.currentPage++ * this.PageSize);
				object[][] rows = queryResults.GetRows(this.PageSize);
				if (rows.Length != this.PageSize)
				{
					this.reachedEnd = true;
				}
				for (int i = 0; i < rows.Length; i++)
				{
					items.Add(new ExMailComplianceItem(this.mailboxContainer.Session, rows[i]));
				}
				return true;
			}, ExMailComplianceItem.MailDataColumns);
			return items;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x000228B4 File Offset: 0x00020AB4
		protected override string GenerateQuery()
		{
			return string.Empty;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x000228BB File Offset: 0x00020ABB
		protected override void Dispose(bool disposing)
		{
		}

		// Token: 0x040003CF RID: 975
		private const int DefaultPageSize = 20;

		// Token: 0x040003D0 RID: 976
		private int currentPage;

		// Token: 0x040003D1 RID: 977
		private bool reachedEnd;

		// Token: 0x040003D2 RID: 978
		private ExMailboxComplianceItemContainer mailboxContainer;
	}
}
