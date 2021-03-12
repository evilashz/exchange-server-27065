using System;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.DNSPlusPlus
{
	// Token: 0x0200000A RID: 10
	internal class Response
	{
		// Token: 0x0600004E RID: 78 RVA: 0x00003A24 File Offset: 0x00001C24
		public Response(byte[] message)
		{
			if (message == null || message.Length < 12 || message.Length > 512)
			{
				throw new FormatException(string.Format("Invalid message length, expected >=12 and <=512, actual ={0}", (message == null) ? 0 : message.Length));
			}
			this.ResponseId = DnsHelper.GetUShort(message, 0);
			byte @byte = DnsHelper.GetByte(message, 2);
			this.IsResponse = ((@byte & 128) > 0);
			if (!this.IsResponse)
			{
				throw new FormatException("Invalid value for IsResponse expected:True, actual:False");
			}
			this.IsAuthoritativeAnswer = ((@byte & 4) > 0);
			this.IsTruncated = ((@byte & 2) > 0);
			this.IsRecursionDesired = ((@byte & 1) > 0);
			byte byte2 = DnsHelper.GetByte(message, 3);
			this.IsRecursionAvailable = ((byte2 & 128) > 0);
			this.Z = (byte)(byte2 >> 4 & 7);
			this.ResponseCode = (QueryResponseCode)(byte2 & 15);
			this.QuestionCount = (int)DnsHelper.GetUShort(message, 4);
			this.AnswerCount = (int)DnsHelper.GetUShort(message, 6);
			this.NameserverCount = (int)DnsHelper.GetUShort(message, 8);
			this.AdditionalRecordCount = (int)DnsHelper.GetUShort(message, 10);
			int position = 12;
			this.Questions = new Question[this.QuestionCount];
			for (int i = 0; i < this.QuestionCount; i++)
			{
				this.Questions[i] = new Question();
				position = this.Questions[i].ProcessResponse(message, position);
			}
			this.Answers = new Answer[this.AnswerCount];
			for (int j = 0; j < this.AnswerCount; j++)
			{
				this.Answers[j] = new Answer();
				position = this.Answers[j].ProcessMessage(message, position);
			}
			this.NSServers = new Answer[this.NameserverCount];
			for (int k = 0; k < this.NameserverCount; k++)
			{
				this.NSServers[k] = new Answer();
				position = this.NSServers[k].ProcessMessage(message, position);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600004F RID: 79 RVA: 0x00003BF6 File Offset: 0x00001DF6
		// (set) Token: 0x06000050 RID: 80 RVA: 0x00003BFE File Offset: 0x00001DFE
		public QueryResponseCode ResponseCode { get; private set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000051 RID: 81 RVA: 0x00003C07 File Offset: 0x00001E07
		// (set) Token: 0x06000052 RID: 82 RVA: 0x00003C0F File Offset: 0x00001E0F
		public bool IsAuthoritativeAnswer { get; private set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000053 RID: 83 RVA: 0x00003C18 File Offset: 0x00001E18
		// (set) Token: 0x06000054 RID: 84 RVA: 0x00003C20 File Offset: 0x00001E20
		public bool IsRecursionAvailable { get; private set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000055 RID: 85 RVA: 0x00003C29 File Offset: 0x00001E29
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00003C31 File Offset: 0x00001E31
		public bool IsRecursionDesired { get; private set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00003C3A File Offset: 0x00001E3A
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00003C42 File Offset: 0x00001E42
		public bool IsTruncated { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00003C4B File Offset: 0x00001E4B
		// (set) Token: 0x0600005A RID: 90 RVA: 0x00003C53 File Offset: 0x00001E53
		public ushort ResponseId { get; private set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600005B RID: 91 RVA: 0x00003C5C File Offset: 0x00001E5C
		// (set) Token: 0x0600005C RID: 92 RVA: 0x00003C64 File Offset: 0x00001E64
		public bool IsResponse { get; private set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600005D RID: 93 RVA: 0x00003C6D File Offset: 0x00001E6D
		// (set) Token: 0x0600005E RID: 94 RVA: 0x00003C75 File Offset: 0x00001E75
		public byte Z { get; private set; }

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00003C7E File Offset: 0x00001E7E
		// (set) Token: 0x06000060 RID: 96 RVA: 0x00003C86 File Offset: 0x00001E86
		public int QuestionCount { get; private set; }

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000061 RID: 97 RVA: 0x00003C8F File Offset: 0x00001E8F
		// (set) Token: 0x06000062 RID: 98 RVA: 0x00003C97 File Offset: 0x00001E97
		public int AnswerCount { get; private set; }

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003CA0 File Offset: 0x00001EA0
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003CA8 File Offset: 0x00001EA8
		public int NameserverCount { get; private set; }

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003CB1 File Offset: 0x00001EB1
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003CB9 File Offset: 0x00001EB9
		public int AdditionalRecordCount { get; private set; }

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003CC2 File Offset: 0x00001EC2
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003CCA File Offset: 0x00001ECA
		public Question[] Questions { get; private set; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000069 RID: 105 RVA: 0x00003CD3 File Offset: 0x00001ED3
		// (set) Token: 0x0600006A RID: 106 RVA: 0x00003CDB File Offset: 0x00001EDB
		public Answer[] Answers { get; private set; }

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600006B RID: 107 RVA: 0x00003CE4 File Offset: 0x00001EE4
		// (set) Token: 0x0600006C RID: 108 RVA: 0x00003CEC File Offset: 0x00001EEC
		public Answer[] NSServers { get; private set; }
	}
}
