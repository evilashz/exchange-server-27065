using System;
using System.Runtime.Serialization;
using System.Text;

namespace Microsoft.Exchange.Inference.PeopleRelevance
{
	// Token: 0x02000024 RID: 36
	[DataContract]
	[Serializable]
	internal sealed class MaskedRecipient
	{
		// Token: 0x0600013D RID: 317 RVA: 0x00006EE4 File Offset: 0x000050E4
		public MaskedRecipient(string emailAddress)
		{
			if (string.IsNullOrEmpty(emailAddress))
			{
				throw new ArgumentException("emailAddress");
			}
			this.EmailAddress = emailAddress;
			this.LastMaskedFromAutoCompleteTimeUtc = DateTime.UtcNow;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600013E RID: 318 RVA: 0x00006F11 File Offset: 0x00005111
		// (set) Token: 0x0600013F RID: 319 RVA: 0x00006F19 File Offset: 0x00005119
		[DataMember]
		public string EmailAddress { get; set; }

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000140 RID: 320 RVA: 0x00006F22 File Offset: 0x00005122
		// (set) Token: 0x06000141 RID: 321 RVA: 0x00006F2A File Offset: 0x0000512A
		[DataMember]
		public DateTime LastMaskedFromAutoCompleteTimeUtc { get; set; }

		// Token: 0x06000142 RID: 322 RVA: 0x00006F34 File Offset: 0x00005134
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("[Email:{0},", this.EmailAddress);
			stringBuilder.AppendFormat("LastDeletedUtc:{0}]", this.LastMaskedFromAutoCompleteTimeUtc);
			return stringBuilder.ToString();
		}
	}
}
