using System;
using System.Runtime.Serialization;

namespace Microsoft.WindowsAzure.ActiveDirectory.GraphHelper
{
	// Token: 0x02000589 RID: 1417
	[DataContract]
	[CLSCompliant(false)]
	public class AADJWTToken
	{
		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x060012CF RID: 4815 RVA: 0x0002AA8B File Offset: 0x00028C8B
		// (set) Token: 0x060012D0 RID: 4816 RVA: 0x0002AA93 File Offset: 0x00028C93
		[DataMember(Name = "token_type")]
		public string TokenType { get; set; }

		// Token: 0x170003B8 RID: 952
		// (get) Token: 0x060012D1 RID: 4817 RVA: 0x0002AA9C File Offset: 0x00028C9C
		// (set) Token: 0x060012D2 RID: 4818 RVA: 0x0002AAA4 File Offset: 0x00028CA4
		[DataMember(Name = "access_token")]
		public string AccessToken { get; set; }

		// Token: 0x170003B9 RID: 953
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x0002AAAD File Offset: 0x00028CAD
		// (set) Token: 0x060012D4 RID: 4820 RVA: 0x0002AAB5 File Offset: 0x00028CB5
		[DataMember(Name = "not_before")]
		public ulong NotBefore { get; set; }

		// Token: 0x170003BA RID: 954
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x0002AABE File Offset: 0x00028CBE
		// (set) Token: 0x060012D6 RID: 4822 RVA: 0x0002AAC6 File Offset: 0x00028CC6
		[DataMember(Name = "expires_on")]
		public ulong ExpiresOn { get; set; }

		// Token: 0x170003BB RID: 955
		// (get) Token: 0x060012D7 RID: 4823 RVA: 0x0002AACF File Offset: 0x00028CCF
		// (set) Token: 0x060012D8 RID: 4824 RVA: 0x0002AAD7 File Offset: 0x00028CD7
		[DataMember(Name = "expires_in")]
		public ulong ExpiresIn { get; set; }

		// Token: 0x170003BC RID: 956
		// (get) Token: 0x060012D9 RID: 4825 RVA: 0x0002AAE0 File Offset: 0x00028CE0
		public bool IsExpired
		{
			get
			{
				return this.WillExpireIn(0);
			}
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x0002AAE9 File Offset: 0x00028CE9
		public bool WillExpireIn(int minutes)
		{
			return AADJWTToken.GenerateTimeStamp(minutes) > this.ExpiresOn;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x0002AAFC File Offset: 0x00028CFC
		private static ulong GenerateTimeStamp(int minutes)
		{
			return Convert.ToUInt64((DateTime.UtcNow.AddMinutes((double)minutes) - new DateTime(1970, 1, 1, 0, 0, 0, 0)).TotalSeconds);
		}
	}
}
