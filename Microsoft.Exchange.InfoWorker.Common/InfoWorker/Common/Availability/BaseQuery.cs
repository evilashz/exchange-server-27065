using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200006E RID: 110
	internal abstract class BaseQuery
	{
		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002AB RID: 683 RVA: 0x0000D05A File Offset: 0x0000B25A
		public EmailAddress Email
		{
			get
			{
				return this.recipientData.EmailAddress;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000D067 File Offset: 0x0000B267
		public ExchangePrincipal ExchangePrincipal
		{
			get
			{
				return this.recipientData.ExchangePrincipal;
			}
		}

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002AD RID: 685 RVA: 0x0000D074 File Offset: 0x0000B274
		public RecipientData RecipientData
		{
			get
			{
				return this.recipientData;
			}
		}

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002AE RID: 686 RVA: 0x0000D07C File Offset: 0x0000B27C
		public long ExchangePrincipalLatency
		{
			get
			{
				return this.recipientData.ExchangePrincipalLatency;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002AF RID: 687 RVA: 0x0000D089 File Offset: 0x0000B289
		// (set) Token: 0x060002B0 RID: 688 RVA: 0x0000D091 File Offset: 0x0000B291
		public long ServiceDiscoveryLatency { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x0000D09A File Offset: 0x0000B29A
		public BaseQueryResult Result
		{
			get
			{
				return this.result;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002B2 RID: 690 RVA: 0x0000D0A2 File Offset: 0x0000B2A2
		// (set) Token: 0x060002B3 RID: 691 RVA: 0x0000D0AA File Offset: 0x0000B2AA
		public RequestType? Type { get; set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002B4 RID: 692 RVA: 0x0000D0B3 File Offset: 0x0000B2B3
		// (set) Token: 0x060002B5 RID: 693 RVA: 0x0000D0BB File Offset: 0x0000B2BB
		public string Target { get; set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002B6 RID: 694 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
		public Dictionary<string, string> LogData
		{
			get
			{
				return this.logData;
			}
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0000D0CC File Offset: 0x0000B2CC
		protected BaseQuery(RecipientData recipientData, BaseQueryResult result)
		{
			this.recipientData = recipientData;
			this.result = result;
			this.logData = new Dictionary<string, string>();
		}

		// Token: 0x060002B8 RID: 696 RVA: 0x0000D0F0 File Offset: 0x0000B2F0
		public bool SetResultOnFirstCall(BaseQueryResult result)
		{
			BaseQueryResult baseQueryResult = Interlocked.CompareExchange<BaseQueryResult>(ref this.result, result, null);
			return baseQueryResult == null;
		}

		// Token: 0x060002B9 RID: 697 RVA: 0x0000D110 File Offset: 0x0000B310
		public void LogAutoDiscRequestDetails(string frontEndServer, string backEndServer, string redirectAddress = null)
		{
			if (string.IsNullOrEmpty(frontEndServer) && string.IsNullOrEmpty(backEndServer) && string.IsNullOrEmpty(redirectAddress))
			{
				return;
			}
			string empty = string.Empty;
			string value = string.Empty;
			this.logData.TryGetValue("AutoDInfo", out empty);
			if (string.IsNullOrEmpty(redirectAddress))
			{
				value = string.Format("{0}<FE-{1}|BE-{2}|>", empty, frontEndServer, backEndServer);
			}
			else
			{
				value = string.Format("{0}<FE-{1}|BE-{2}|Redirect-{3}|>", new object[]
				{
					empty,
					frontEndServer,
					backEndServer,
					redirectAddress
				});
			}
			this.logData["AutoDInfo"] = value;
		}

		// Token: 0x060002BA RID: 698 RVA: 0x0000D1A1 File Offset: 0x0000B3A1
		internal void LogLatency(string key, long value)
		{
			this.logData.Add(key, value.ToString());
		}

		// Token: 0x040001B6 RID: 438
		private RecipientData recipientData;

		// Token: 0x040001B7 RID: 439
		private BaseQueryResult result;

		// Token: 0x040001B8 RID: 440
		private Dictionary<string, string> logData;
	}
}
