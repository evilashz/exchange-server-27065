using System;

namespace Microsoft.Exchange.Net
{
	// Token: 0x02000C01 RID: 3073
	internal class DnsQuery
	{
		// Token: 0x06004347 RID: 17223 RVA: 0x000B4510 File Offset: 0x000B2710
		internal DnsQuery(DnsRecordType type, string question)
		{
			this.type = type;
			this.question = question.ToLowerInvariant();
		}

		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06004348 RID: 17224 RVA: 0x000B452B File Offset: 0x000B272B
		internal DnsRecordType Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06004349 RID: 17225 RVA: 0x000B4533 File Offset: 0x000B2733
		internal string Question
		{
			get
			{
				return this.question;
			}
		}

		// Token: 0x0600434A RID: 17226 RVA: 0x000B453B File Offset: 0x000B273B
		public override int GetHashCode()
		{
			return this.type.GetHashCode() ^ this.question.GetHashCode();
		}

		// Token: 0x0600434B RID: 17227 RVA: 0x000B455C File Offset: 0x000B275C
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DnsQuery dnsQuery = obj as DnsQuery;
			return dnsQuery != null && this.type == dnsQuery.type && this.question == dnsQuery.question;
		}

		// Token: 0x0600434C RID: 17228 RVA: 0x000B459C File Offset: 0x000B279C
		public override string ToString()
		{
			return this.type + " " + this.question;
		}

		// Token: 0x04003943 RID: 14659
		private DnsRecordType type;

		// Token: 0x04003944 RID: 14660
		private string question;
	}
}
