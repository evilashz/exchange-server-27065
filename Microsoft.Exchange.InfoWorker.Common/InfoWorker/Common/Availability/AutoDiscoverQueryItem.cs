using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.InfoWorker.EventLog;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200006A RID: 106
	internal sealed class AutoDiscoverQueryItem
	{
		// Token: 0x0600029A RID: 666 RVA: 0x0000CEC3 File Offset: 0x0000B0C3
		public AutoDiscoverQueryItem(RecipientData recipientData, LocalizedString applicationName, BaseQuery sourceQuery)
		{
			this.EmailAddress = recipientData.EmailAddress;
			this.initialEmailAddress = recipientData.EmailAddress;
			this.recipientData = recipientData;
			this.applicationName = applicationName;
			this.sourceQuery = sourceQuery;
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600029B RID: 667 RVA: 0x0000CEF8 File Offset: 0x0000B0F8
		// (set) Token: 0x0600029C RID: 668 RVA: 0x0000CF00 File Offset: 0x0000B100
		public EmailAddress EmailAddress { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600029D RID: 669 RVA: 0x0000CF09 File Offset: 0x0000B109
		public EmailAddress InitialEmailAddress
		{
			get
			{
				return this.initialEmailAddress;
			}
		}

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600029E RID: 670 RVA: 0x0000CF11 File Offset: 0x0000B111
		public AutoDiscoverResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x0600029F RID: 671 RVA: 0x0000CF19 File Offset: 0x0000B119
		public RecipientData RecipientData
		{
			get
			{
				return this.recipientData;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x0000CF21 File Offset: 0x0000B121
		public BaseQuery SourceQuery
		{
			get
			{
				return this.sourceQuery;
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x0000CF2C File Offset: 0x0000B12C
		public static AutoDiscoverQueryItem[] CreateAutoDiscoverQueryItems(Application application, QueryList queryList, Uri autoDiscoverUrl)
		{
			AutoDiscoverQueryItem[] array = new AutoDiscoverQueryItem[queryList.Count];
			string target = autoDiscoverUrl.ToString();
			for (int i = 0; i < queryList.Count; i++)
			{
				queryList[i].Target = target;
				array[i] = new AutoDiscoverQueryItem(queryList[i].RecipientData, application.Name, queryList[i]);
			}
			return array;
		}

		// Token: 0x060002A2 RID: 674 RVA: 0x0000CF8C File Offset: 0x0000B18C
		public void SetResult(AutoDiscoverResult result)
		{
			if (Interlocked.CompareExchange<AutoDiscoverResult>(ref this.result, result, null) == null && result.Exception != null)
			{
				Globals.AvailabilityLogger.LogEvent(InfoWorkerEventLogConstants.Tuple_AutoDiscoverFailed, this.EmailAddress.Domain, new object[]
				{
					Globals.ProcessId,
					this.EmailAddress,
					this.applicationName,
					result.Exception.ToString()
				});
			}
		}

		// Token: 0x040001A5 RID: 421
		private AutoDiscoverResult result;

		// Token: 0x040001A6 RID: 422
		private EmailAddress initialEmailAddress;

		// Token: 0x040001A7 RID: 423
		private RecipientData recipientData;

		// Token: 0x040001A8 RID: 424
		private LocalizedString applicationName;

		// Token: 0x040001A9 RID: 425
		private BaseQuery sourceQuery;
	}
}
