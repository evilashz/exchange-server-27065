using System;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x0200022E RID: 558
	public class KeywordHit
	{
		// Token: 0x170005E5 RID: 1509
		// (get) Token: 0x0600135B RID: 4955 RVA: 0x0003B2CE File Offset: 0x000394CE
		// (set) Token: 0x0600135C RID: 4956 RVA: 0x0003B2D6 File Offset: 0x000394D6
		public string Phrase { get; set; }

		// Token: 0x170005E6 RID: 1510
		// (get) Token: 0x0600135D RID: 4957 RVA: 0x0003B2DF File Offset: 0x000394DF
		// (set) Token: 0x0600135E RID: 4958 RVA: 0x0003B2E7 File Offset: 0x000394E7
		public int Count { get; set; }

		// Token: 0x170005E7 RID: 1511
		// (get) Token: 0x0600135F RID: 4959 RVA: 0x0003B2F0 File Offset: 0x000394F0
		// (set) Token: 0x06001360 RID: 4960 RVA: 0x0003B2F8 File Offset: 0x000394F8
		public ByteQuantifiedSize Size { get; set; }

		// Token: 0x170005E8 RID: 1512
		// (get) Token: 0x06001361 RID: 4961 RVA: 0x0003B301 File Offset: 0x00039501
		// (set) Token: 0x06001362 RID: 4962 RVA: 0x0003B309 File Offset: 0x00039509
		public int MailboxCount { get; set; }

		// Token: 0x06001363 RID: 4963 RVA: 0x0003B314 File Offset: 0x00039514
		public static KeywordHit Parse(string value)
		{
			string[] array = value.Split(new char[]
			{
				'\t'
			});
			int count = int.Parse(array[array.Length - 3]);
			ByteQuantifiedSize size = ByteQuantifiedSize.Parse(array[array.Length - 2]);
			int mailboxCount = int.Parse(array[array.Length - 1]);
			return new KeywordHit
			{
				Phrase = string.Join("\t", array, 0, array.Length - 3),
				Count = count,
				Size = size,
				MailboxCount = mailboxCount
			};
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x0003B398 File Offset: 0x00039598
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				this.Phrase,
				'\t',
				this.Count,
				'\t',
				this.Size,
				'\t',
				this.MailboxCount
			});
		}

		// Token: 0x04000B4F RID: 2895
		public const string UnsearchablePhrase = "652beee2-75f7-4ca0-8a02-0698a3919cb9";
	}
}
