using System;
using System.Collections.Generic;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Shared.Smtp
{
	// Token: 0x02000027 RID: 39
	internal class SmtpMailItemResult
	{
		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000118 RID: 280 RVA: 0x0000753C File Offset: 0x0000573C
		// (set) Token: 0x06000119 RID: 281 RVA: 0x00007544 File Offset: 0x00005744
		public AckStatusAndResponse ConnectionResponse
		{
			get
			{
				return this.connectionResponse;
			}
			set
			{
				this.connectionResponse = value;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011A RID: 282 RVA: 0x0000754D File Offset: 0x0000574D
		// (set) Token: 0x0600011B RID: 283 RVA: 0x00007555 File Offset: 0x00005755
		public AckStatusAndResponse MessageResponse
		{
			get
			{
				return this.messageResponse;
			}
			set
			{
				this.messageResponse = value;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000755E File Offset: 0x0000575E
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00007566 File Offset: 0x00005766
		public string RemoteHostName
		{
			get
			{
				return this.remoteHostName;
			}
			set
			{
				this.remoteHostName = value;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000756F File Offset: 0x0000576F
		// (set) Token: 0x0600011F RID: 287 RVA: 0x00007577 File Offset: 0x00005777
		public Dictionary<MailRecipient, AckStatusAndResponse> RecipientResponses
		{
			get
			{
				return this.recipientResponses;
			}
			set
			{
				this.recipientResponses = value;
			}
		}

		// Token: 0x04000081 RID: 129
		private AckStatusAndResponse connectionResponse;

		// Token: 0x04000082 RID: 130
		private AckStatusAndResponse messageResponse;

		// Token: 0x04000083 RID: 131
		private string remoteHostName;

		// Token: 0x04000084 RID: 132
		private Dictionary<MailRecipient, AckStatusAndResponse> recipientResponses;
	}
}
