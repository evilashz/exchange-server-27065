using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Monitoring;

namespace Microsoft.Exchange.Management.Search
{
	// Token: 0x02000160 RID: 352
	[Serializable]
	public class SearchTestResult : ConfigurableObject
	{
		// Token: 0x17000390 RID: 912
		// (get) Token: 0x06000CB6 RID: 3254 RVA: 0x0003A9CB File Offset: 0x00038BCB
		internal override ObjectSchema ObjectSchema
		{
			get
			{
				return SearchTestResult.schema;
			}
		}

		// Token: 0x17000391 RID: 913
		// (get) Token: 0x06000CB7 RID: 3255 RVA: 0x0003A9D2 File Offset: 0x00038BD2
		// (set) Token: 0x06000CB8 RID: 3256 RVA: 0x0003A9E4 File Offset: 0x00038BE4
		public string Mailbox
		{
			get
			{
				return (string)this[SearchTestResultSchema.Mailbox];
			}
			set
			{
				this[SearchTestResultSchema.Mailbox] = value;
			}
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000CB9 RID: 3257 RVA: 0x0003A9F2 File Offset: 0x00038BF2
		// (set) Token: 0x06000CBA RID: 3258 RVA: 0x0003AA04 File Offset: 0x00038C04
		public Guid MailboxGuid
		{
			get
			{
				return (Guid)this[SearchTestResultSchema.MailboxGuid];
			}
			set
			{
				this[SearchTestResultSchema.MailboxGuid] = value;
			}
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000CBB RID: 3259 RVA: 0x0003AA17 File Offset: 0x00038C17
		// (set) Token: 0x06000CBC RID: 3260 RVA: 0x0003AA29 File Offset: 0x00038C29
		public string UserLegacyExchangeDN
		{
			get
			{
				return (string)this[SearchTestResultSchema.UserLegacyExchangeDN];
			}
			set
			{
				this[SearchTestResultSchema.UserLegacyExchangeDN] = value;
			}
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000CBD RID: 3261 RVA: 0x0003AA37 File Offset: 0x00038C37
		// (set) Token: 0x06000CBE RID: 3262 RVA: 0x0003AA49 File Offset: 0x00038C49
		public string Database
		{
			get
			{
				return (string)this[SearchTestResultSchema.Database];
			}
			set
			{
				this[SearchTestResultSchema.Database] = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x0003AA57 File Offset: 0x00038C57
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x0003AA69 File Offset: 0x00038C69
		public Guid DatabaseGuid
		{
			get
			{
				return (Guid)this[SearchTestResultSchema.DatabaseGuid];
			}
			set
			{
				this[SearchTestResultSchema.DatabaseGuid] = value;
			}
		}

		// Token: 0x17000396 RID: 918
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x0003AA7C File Offset: 0x00038C7C
		// (set) Token: 0x06000CC2 RID: 3266 RVA: 0x0003AA8E File Offset: 0x00038C8E
		public Guid ServerGuid
		{
			get
			{
				return (Guid)this[SearchTestResultSchema.ServerGuid];
			}
			set
			{
				this[SearchTestResultSchema.ServerGuid] = value;
			}
		}

		// Token: 0x17000397 RID: 919
		// (get) Token: 0x06000CC3 RID: 3267 RVA: 0x0003AAA1 File Offset: 0x00038CA1
		// (set) Token: 0x06000CC4 RID: 3268 RVA: 0x0003AAB3 File Offset: 0x00038CB3
		public bool ResultFound
		{
			get
			{
				return (bool)this[SearchTestResultSchema.ResultFound];
			}
			set
			{
				this[SearchTestResultSchema.ResultFound] = value;
			}
		}

		// Token: 0x17000398 RID: 920
		// (get) Token: 0x06000CC5 RID: 3269 RVA: 0x0003AAC6 File Offset: 0x00038CC6
		// (set) Token: 0x06000CC6 RID: 3270 RVA: 0x0003AADD File Offset: 0x00038CDD
		public double SearchTimeInSeconds
		{
			get
			{
				return double.Parse((string)this[SearchTestResultSchema.SearchTimeInSeconds]);
			}
			set
			{
				this[SearchTestResultSchema.SearchTimeInSeconds] = value.ToString();
			}
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x0003AAF1 File Offset: 0x00038CF1
		// (set) Token: 0x06000CC8 RID: 3272 RVA: 0x0003AB03 File Offset: 0x00038D03
		public List<MonitoringEvent> DetailEvents
		{
			get
			{
				return (List<MonitoringEvent>)this[SearchTestResultSchema.DetailEvents];
			}
			set
			{
				this[SearchTestResultSchema.DetailEvents] = value;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x0003AB11 File Offset: 0x00038D11
		// (set) Token: 0x06000CCA RID: 3274 RVA: 0x0003AB23 File Offset: 0x00038D23
		public string Server
		{
			get
			{
				return (string)this[SearchTestResultSchema.Server];
			}
			set
			{
				this[SearchTestResultSchema.Server] = value;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x0003AB31 File Offset: 0x00038D31
		// (set) Token: 0x06000CCC RID: 3276 RVA: 0x0003AB43 File Offset: 0x00038D43
		public string Error
		{
			get
			{
				return (string)this[SearchTestResultSchema.Error];
			}
			set
			{
				this[SearchTestResultSchema.Error] = value;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x06000CCD RID: 3277 RVA: 0x0003AB51 File Offset: 0x00038D51
		// (set) Token: 0x06000CCE RID: 3278 RVA: 0x0003AB59 File Offset: 0x00038D59
		public uint DocumentId
		{
			get
			{
				return this.documentId;
			}
			set
			{
				this.documentId = value;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0003AB62 File Offset: 0x00038D62
		// (set) Token: 0x06000CD0 RID: 3280 RVA: 0x0003AB6A File Offset: 0x00038D6A
		public byte[] EntryId
		{
			get
			{
				return this.entryId;
			}
			set
			{
				this.entryId = value;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000CD1 RID: 3281 RVA: 0x0003AB73 File Offset: 0x00038D73
		public static SearchTestResult DefaultSearchTestResult
		{
			get
			{
				return new SearchTestResult();
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0003AB7A File Offset: 0x00038D7A
		public SearchTestResult() : base(new SimpleProviderPropertyBag())
		{
			this.Reset();
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0003AB90 File Offset: 0x00038D90
		internal void Reset()
		{
			this[SearchTestResultSchema.ResultFound] = false;
			this[SearchTestResultSchema.SearchTimeInSeconds] = "0";
			this[SearchTestResultSchema.Mailbox] = null;
			this[SearchTestResultSchema.UserLegacyExchangeDN] = null;
			this[SearchTestResultSchema.Database] = null;
			this[SearchTestResultSchema.Server] = null;
			this[SearchTestResultSchema.Error] = null;
			this[SearchTestResultSchema.MailboxGuid] = Guid.Empty;
			this[SearchTestResultSchema.DatabaseGuid] = Guid.Empty;
			this[SearchTestResultSchema.ServerGuid] = Guid.Empty;
			this[SearchTestResultSchema.DetailEvents] = new List<MonitoringEvent>(1);
			this.resultTimeout = false;
			this.documentId = 0U;
		}

		// Token: 0x06000CD4 RID: 3284 RVA: 0x0003AC58 File Offset: 0x00038E58
		internal void SetResult(bool bResult, double SearchTimeInSeconds)
		{
			this[SearchTestResultSchema.ResultFound] = bResult;
			this[SearchTestResultSchema.SearchTimeInSeconds] = SearchTimeInSeconds.ToString();
		}

		// Token: 0x06000CD5 RID: 3285 RVA: 0x0003AC80 File Offset: 0x00038E80
		internal void SetErrorTestResult(EventId eventId, LocalizedString strMessage)
		{
			this.SetResult(false, -1.0);
			this.Error = strMessage;
			this.DetailEvents.Add(new MonitoringEvent("MSExchange Monitoring ExchangeSearch", (int)eventId, EventTypeEnumeration.Error, strMessage, this.Database));
		}

		// Token: 0x06000CD6 RID: 3286 RVA: 0x0003ACCC File Offset: 0x00038ECC
		internal void SetErrorTestResult(EventId eventId, string strMessage)
		{
			this.SetResult(false, -1.0);
			MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring ExchangeSearch", (int)eventId, EventTypeEnumeration.Error, strMessage, this.Database);
			this.Error = strMessage;
			this.DetailEvents.Add(item);
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x0003AD10 File Offset: 0x00038F10
		internal void SetErrorTestResultWithTestThreadTimeOut()
		{
			this.SetResult(false, 0.0);
			this.resultTimeout = true;
			MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring ExchangeSearch", 1020, EventTypeEnumeration.Error, Strings.TestSearchTestThreadTimeOut, this.Database);
			this.Error = Strings.TestSearchTestThreadTimeOut;
			this.DetailEvents.Add(item);
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x0003AD74 File Offset: 0x00038F74
		internal void SetTestResult(bool bResult, double SearchTimeInSeconds)
		{
			if (bResult)
			{
				MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring ExchangeSearch", 1000, EventTypeEnumeration.Success, Strings.TestSearchSucceeded(this.Database), this.Database);
				this.DetailEvents.Add(item);
			}
			else if (!this.resultTimeout)
			{
				MonitoringEvent item = new MonitoringEvent("MSExchange Monitoring ExchangeSearch", 1001, EventTypeEnumeration.Error, Strings.TestSearchFailed(this.Mailbox, this.Database, this.Server), this.Database);
				this.Error = Strings.TestSearchFailed(this.Mailbox, this.Database, this.Server);
				this.DetailEvents.Add(item);
			}
			this.SetResult(bResult, SearchTimeInSeconds);
		}

		// Token: 0x0400063E RID: 1598
		internal const string MOMEventSource = "MSExchange Monitoring ExchangeSearch";

		// Token: 0x0400063F RID: 1599
		private static ObjectSchema schema = ObjectSchema.GetInstance<SearchTestResultSchema>();

		// Token: 0x04000640 RID: 1600
		private bool resultTimeout;

		// Token: 0x04000641 RID: 1601
		private uint documentId;

		// Token: 0x04000642 RID: 1602
		private byte[] entryId;
	}
}
