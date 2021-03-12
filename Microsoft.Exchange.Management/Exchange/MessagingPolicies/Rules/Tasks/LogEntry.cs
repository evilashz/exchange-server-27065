using System;

namespace Microsoft.Exchange.MessagingPolicies.Rules.Tasks
{
	// Token: 0x02000B5B RID: 2907
	internal class LogEntry
	{
		// Token: 0x0600697E RID: 27006 RVA: 0x001B3B2A File Offset: 0x001B1D2A
		public LogEntry(DateTime timestamp, string id, string name, string details, string action, string from, string recipients)
		{
			this.timeStamp = timestamp;
			this.messageID = id;
			this.ruleName = name;
			this.details = details;
			this.action = action;
			this.fromAddress = from;
			this.recipientAddress = recipients;
		}

		// Token: 0x17002083 RID: 8323
		// (get) Token: 0x0600697F RID: 27007 RVA: 0x001B3B67 File Offset: 0x001B1D67
		public string RuleName
		{
			get
			{
				return this.ruleName;
			}
		}

		// Token: 0x17002084 RID: 8324
		// (get) Token: 0x06006980 RID: 27008 RVA: 0x001B3B6F File Offset: 0x001B1D6F
		public string MessageID
		{
			get
			{
				return this.messageID;
			}
		}

		// Token: 0x17002085 RID: 8325
		// (get) Token: 0x06006981 RID: 27009 RVA: 0x001B3B77 File Offset: 0x001B1D77
		public string Details
		{
			get
			{
				return this.details;
			}
		}

		// Token: 0x17002086 RID: 8326
		// (get) Token: 0x06006982 RID: 27010 RVA: 0x001B3B7F File Offset: 0x001B1D7F
		public DateTime TimeStamp
		{
			get
			{
				return this.timeStamp;
			}
		}

		// Token: 0x17002087 RID: 8327
		// (get) Token: 0x06006983 RID: 27011 RVA: 0x001B3B87 File Offset: 0x001B1D87
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17002088 RID: 8328
		// (get) Token: 0x06006984 RID: 27012 RVA: 0x001B3B8F File Offset: 0x001B1D8F
		public string FromAddress
		{
			get
			{
				return this.fromAddress;
			}
		}

		// Token: 0x17002089 RID: 8329
		// (get) Token: 0x06006985 RID: 27013 RVA: 0x001B3B97 File Offset: 0x001B1D97
		public string RecipientAddress
		{
			get
			{
				return this.recipientAddress;
			}
		}

		// Token: 0x040036CB RID: 14027
		private readonly DateTime timeStamp;

		// Token: 0x040036CC RID: 14028
		private readonly string messageID;

		// Token: 0x040036CD RID: 14029
		private readonly string ruleName;

		// Token: 0x040036CE RID: 14030
		private readonly string details;

		// Token: 0x040036CF RID: 14031
		private readonly string action;

		// Token: 0x040036D0 RID: 14032
		private readonly string fromAddress;

		// Token: 0x040036D1 RID: 14033
		private readonly string recipientAddress;
	}
}
