using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Search.Core.Abstraction;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.Fast;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x02000205 RID: 517
	internal class NonIndexableItemStatisticsProvider : NonIndexableItemProvider
	{
		// Token: 0x06000DDC RID: 3548 RVA: 0x0003C6F6 File Offset: 0x0003A8F6
		public NonIndexableItemStatisticsProvider(IRecipientSession recipientSession, ExTimeZone timeZone, CallerInfo callerInfo, OrganizationId orgId, string[] mailboxes, bool searchArchiveOnly) : base(recipientSession, timeZone, callerInfo, orgId, mailboxes, searchArchiveOnly)
		{
			this.Results = new List<NonIndexableItemStatisticsInfo>(mailboxes.Length);
		}

		// Token: 0x17000382 RID: 898
		// (get) Token: 0x06000DDD RID: 3549 RVA: 0x0003C716 File Offset: 0x0003A916
		// (set) Token: 0x06000DDE RID: 3550 RVA: 0x0003C71E File Offset: 0x0003A91E
		public List<NonIndexableItemStatisticsInfo> Results { get; private set; }

		// Token: 0x06000DDF RID: 3551 RVA: 0x0003C728 File Offset: 0x0003A928
		protected override void InternalExecuteSearch()
		{
			List<string> list = new List<string>(1);
			List<ADRawEntry> list2 = NonIndexableItemProvider.FindMailboxesInAD(this.recipientSession, this.mailboxes, list);
			foreach (ADRawEntry adrawEntry in list2)
			{
				string text = (string)adrawEntry[ADRecipientSchema.LegacyExchangeDN];
				try
				{
					if (!this.searchArchiveOnly)
					{
						this.RetrieveFailedItemsCount(adrawEntry, MailboxType.Primary);
					}
					if (!this.alreadyProxy && (Guid)adrawEntry[ADUserSchema.ArchiveGuid] != Guid.Empty)
					{
						this.RetrieveFailedItemsCount(adrawEntry, MailboxType.Archive);
					}
				}
				catch (Exception ex)
				{
					Factory.Current.GeneralTracer.TraceDebug<Guid, string>((long)this.GetHashCode(), "Correlation Id:{0}. Failed to retrieve failed items count for {1}", this.callerInfo.QueryCorrelationId, text);
					base.AddFailedMailbox(text, ex.Message);
					this.UpdateResults(text, 0);
				}
			}
			foreach (string text2 in list)
			{
				base.AddFailedMailbox(text2, Strings.SourceMailboxUserNotFoundInAD(text2));
				this.UpdateResults(text2, 0);
			}
		}

		// Token: 0x06000DE0 RID: 3552 RVA: 0x0003C884 File Offset: 0x0003AA84
		protected override void InternalExecuteSearchWebService()
		{
			GetNonIndexableItemStatisticsParameters parameters = new GetNonIndexableItemStatisticsParameters
			{
				Mailboxes = new string[]
				{
					this.mailboxInfo.LegacyExchangeDN
				},
				SearchArchiveOnly = !this.mailboxInfo.IsPrimary
			};
			IAsyncResult result = this.ewsClient.BeginGetNonIndexableItemStatistics(null, null, parameters);
			GetNonIndexableItemStatisticsResponse getNonIndexableItemStatisticsResponse = this.ewsClient.EndGetNonIndexableItemStatistics(result);
			if (getNonIndexableItemStatisticsResponse.NonIndexableStatistics != null && getNonIndexableItemStatisticsResponse.NonIndexableStatistics.Count > 0)
			{
				NonIndexableItemStatistic nonIndexableItemStatistic = getNonIndexableItemStatisticsResponse.NonIndexableStatistics[0];
				if (!string.IsNullOrEmpty(nonIndexableItemStatistic.ErrorMessage))
				{
					base.AddFailedMailbox(nonIndexableItemStatistic.Mailbox, nonIndexableItemStatistic.ErrorMessage);
				}
				this.UpdateResults(nonIndexableItemStatistic.Mailbox, (int)getNonIndexableItemStatisticsResponse.NonIndexableStatistics[0].ItemCount);
			}
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x0003C94C File Offset: 0x0003AB4C
		protected override void HandleExecuteSearchWebServiceFailed(string legacyDn)
		{
			string errorMessage = this.failedMailboxes.ContainsKey(legacyDn) ? this.failedMailboxes[legacyDn] : null;
			bool flag = false;
			foreach (NonIndexableItemStatisticsInfo nonIndexableItemStatisticsInfo in this.Results)
			{
				if (string.Equals(nonIndexableItemStatisticsInfo.Mailbox, legacyDn, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					nonIndexableItemStatisticsInfo.ErrorMessage = errorMessage;
					break;
				}
			}
			if (!flag)
			{
				this.Results.Add(new NonIndexableItemStatisticsInfo(legacyDn, 0, errorMessage));
			}
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x0003C9E8 File Offset: 0x0003ABE8
		private void RetrieveFailedItemsCount(ADRawEntry adRawEntry, MailboxType mailboxType)
		{
			base.PerformMailboxDiscovery(adRawEntry, mailboxType, out this.groupId, out this.mailboxInfo);
			switch (this.groupId.GroupType)
			{
			case GroupType.CrossServer:
			case GroupType.CrossPremise:
				base.ExecuteSearchWebService();
				return;
			case GroupType.SkippedError:
			{
				string error = (this.groupId.Error == null) ? string.Empty : this.groupId.Error.Message;
				base.AddFailedMailbox(this.mailboxInfo.LegacyExchangeDN, error);
				return;
			}
			default:
				if (this.mailboxInfo.MailboxGuid != Guid.Empty)
				{
					Guid guid = (mailboxType == MailboxType.Archive) ? this.mailboxInfo.ArchiveDatabase : this.mailboxInfo.MdbGuid;
					string indexSystemName = FastIndexVersion.GetIndexSystemName(guid);
					using (IFailedItemStorage failedItemStorage = Factory.Current.CreateFailedItemStorage(Factory.Current.CreateSearchServiceConfig(), indexSystemName))
					{
						FailedItemParameters parameters = new FailedItemParameters(FailureMode.All, FieldSet.None)
						{
							MailboxGuid = new Guid?(this.mailboxInfo.MailboxGuid)
						};
						int totalNonIndexableItems = (int)failedItemStorage.GetFailedItemsCount(parameters);
						this.UpdateResults(this.mailboxInfo.LegacyExchangeDN, totalNonIndexableItems);
					}
				}
				return;
			}
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x0003CB20 File Offset: 0x0003AD20
		private void UpdateResults(string legacyDn, int totalNonIndexableItems)
		{
			string errorMessage = this.failedMailboxes.ContainsKey(legacyDn) ? this.failedMailboxes[legacyDn] : null;
			bool flag = false;
			foreach (NonIndexableItemStatisticsInfo nonIndexableItemStatisticsInfo in this.Results)
			{
				if (string.Equals(nonIndexableItemStatisticsInfo.Mailbox, legacyDn, StringComparison.OrdinalIgnoreCase))
				{
					flag = true;
					nonIndexableItemStatisticsInfo.ItemCount += totalNonIndexableItems;
					nonIndexableItemStatisticsInfo.ErrorMessage = errorMessage;
					break;
				}
			}
			if (!flag)
			{
				this.Results.Add(new NonIndexableItemStatisticsInfo(legacyDn, totalNonIndexableItems, errorMessage));
			}
		}
	}
}
