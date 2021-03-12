using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000213 RID: 531
	internal class SortedResultPage
	{
		// Token: 0x06000E72 RID: 3698 RVA: 0x0003F060 File Offset: 0x0003D260
		public SortedResultPage(PreviewItem[] resultRows, PagingInfo pagingInfo)
		{
			Util.ThrowOnNull(resultRows, "resultRows");
			Util.ThrowOnNull(pagingInfo, "pagingInfo");
			for (int i = 0; i < resultRows.Length; i++)
			{
				if (resultRows[i] == null)
				{
					throw new ArgumentException(Strings.InvalidPreviewItemInResultRows);
				}
			}
			this.resultRows = resultRows;
			this.pagingInfo = pagingInfo;
			this.AreResultsSorted();
			this.AreResultsConsistentWithReferenceItem();
			if (this.pagingInfo.ExcludeDuplicates && !this.AreResultsDeDuped())
			{
				throw new ArgumentException(Strings.ResultsNotDeduped);
			}
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x0003F0EF File Offset: 0x0003D2EF
		internal SortedResultPage(PreviewItem[] sortedResults)
		{
			Util.ThrowOnNull(sortedResults, "sortedResults");
			this.resultRows = sortedResults;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06000E74 RID: 3700 RVA: 0x0003F109 File Offset: 0x0003D309
		public PreviewItem[] ResultRows
		{
			get
			{
				return this.resultRows;
			}
		}

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06000E75 RID: 3701 RVA: 0x0003F111 File Offset: 0x0003D311
		public int ResultCount
		{
			get
			{
				return this.resultRows.Length;
			}
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x0003F11C File Offset: 0x0003D31C
		public void Merge(SortedResultPage newPage)
		{
			if (newPage == null)
			{
				return;
			}
			if (this.pagingInfo == null)
			{
				throw new ArgumentException(Strings.SortedResultNullParameters);
			}
			if (!this.pagingInfo.Equals(newPage.pagingInfo))
			{
				throw new ArgumentException(Strings.InvalidResultMerge);
			}
			int num = 0;
			int num2 = 0;
			int i = 0;
			int num3 = this.ResultCount + newPage.ResultCount;
			PreviewItem[] array = new PreviewItem[num3];
			while (i < num3)
			{
				bool flag = false;
				if (num == this.ResultCount)
				{
					flag = false;
				}
				else if (num2 == newPage.ResultCount)
				{
					flag = true;
				}
				else if (this.resultRows[num].CompareTo(newPage.resultRows[num2]) <= 0)
				{
					if (this.pagingInfo.AscendingSort)
					{
						flag = true;
					}
				}
				else if (this.resultRows[num].CompareTo(newPage.resultRows[num2]) > 0 && !this.pagingInfo.AscendingSort)
				{
					flag = true;
				}
				if (flag)
				{
					array[i] = this.resultRows[num];
					num++;
				}
				else
				{
					array[i] = newPage.resultRows[num2];
					num2++;
				}
				i++;
			}
			if (this.pagingInfo.ExcludeDuplicates)
			{
				array = Util.ExcludeDuplicateItems(array);
				num3 = array.Length;
			}
			this.resultRows = new PreviewItem[Math.Min(this.pagingInfo.PageSize, num3)];
			if (this.pagingInfo.Direction == PageDirection.Next || num3 < this.pagingInfo.PageSize)
			{
				num = 0;
			}
			else
			{
				num = num3 - this.pagingInfo.PageSize;
			}
			i = 0;
			while (i < Math.Min(this.pagingInfo.PageSize, num3))
			{
				this.resultRows[i] = array[num];
				i++;
				num++;
			}
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x0003F2BC File Offset: 0x0003D4BC
		private bool AreResultsDeDuped()
		{
			HashSet<UniqueItemHash> hashSet = new HashSet<UniqueItemHash>();
			for (int i = 0; i < this.resultRows.Length; i++)
			{
				if (this.resultRows[i].ItemHash != null && hashSet.Contains(this.resultRows[i].ItemHash))
				{
					return false;
				}
				if (this.resultRows[i].ItemHash != null)
				{
					hashSet.Add(this.resultRows[i].ItemHash);
				}
			}
			return true;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x0003F32C File Offset: 0x0003D52C
		private void AreResultsSorted()
		{
			for (int i = 1; i < this.resultRows.Length; i++)
			{
				int num = this.resultRows[i - 1].CompareTo(this.resultRows[i]);
				if (this.pagingInfo.AscendingSort && num > 0)
				{
					throw new ArgumentException(Strings.InvalidSortedResultParameter(this.resultRows[i - i].SortValue.ToString(), this.resultRows[i].SortValue.ToString()));
				}
				if (!this.pagingInfo.AscendingSort && num < 0)
				{
					throw new ArgumentException(Strings.InvalidSortedResultParameter(this.resultRows[i - i].SortValue.ToString(), this.resultRows[i].SortValue.ToString()));
				}
			}
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x0003F3F8 File Offset: 0x0003D5F8
		private void AreResultsConsistentWithReferenceItem()
		{
			if (this.pagingInfo == null || this.pagingInfo.SortValue == null || this.resultRows.Length == 0)
			{
				return;
			}
			Guid mailboxGuid = this.resultRows[0].MailboxGuid;
			ReferenceItem referenceItem = (this.pagingInfo.Direction == PageDirection.Previous) ? this.resultRows[this.resultRows.Length - 1].SortValue : this.resultRows[0].SortValue;
			int num = referenceItem.CompareTo(this.pagingInfo.SortValue);
			if (num > 0 && (this.pagingInfo.Direction == PageDirection.Next || this.pagingInfo.AscendingSort) && (this.pagingInfo.Direction == PageDirection.Previous || !this.pagingInfo.AscendingSort))
			{
				this.PublishToLogAndThrowArgumentException(referenceItem, mailboxGuid);
			}
			if (num < 0 && (this.pagingInfo.Direction == PageDirection.Previous || this.pagingInfo.AscendingSort) && (this.pagingInfo.Direction == PageDirection.Next || !this.pagingInfo.AscendingSort))
			{
				this.PublishToLogAndThrowArgumentException(referenceItem, mailboxGuid);
			}
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x0003F4FC File Offset: 0x0003D6FC
		private void PublishToLogAndThrowArgumentException(ReferenceItem sortValue, Guid mailboxGuid)
		{
			string text = Strings.InconsistentSortedResults(mailboxGuid.ToString(), this.pagingInfo.SortValue.ToString(), sortValue.ToString());
			EventNotificationItem.Publish(ExchangeComponent.EdiscoveryProtocol.Name, "MailboxSearch.InconsistentItemReferenceErrorMonitor", null, text, ResultSeverityLevel.Error, false);
			throw new ArgumentException(text)
			{
				Data = 
				{
					{
						"SortOrder",
						this.pagingInfo.AscendingSort ? "Ascending" : "Descending"
					},
					{
						"PageDirection",
						(this.pagingInfo.Direction == PageDirection.Next) ? "Next" : "Previous"
					}
				}
			};
		}

		// Token: 0x040009EC RID: 2540
		private readonly PagingInfo pagingInfo;

		// Token: 0x040009ED RID: 2541
		private PreviewItem[] resultRows;
	}
}
