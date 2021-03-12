using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000009 RID: 9
	internal class GroupExpansionRecipients
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000284B File Offset: 0x00000A4B
		public List<RecipientToIndex> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002853 File Offset: 0x00000A53
		public string ToRecipients
		{
			get
			{
				return GroupExpansionRecipients.GetRecipientsByType(RecipientItemType.To, this.recipients);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600003B RID: 59 RVA: 0x00002861 File Offset: 0x00000A61
		public string CcRecipients
		{
			get
			{
				return GroupExpansionRecipients.GetRecipientsByType(RecipientItemType.Cc, this.recipients);
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600003C RID: 60 RVA: 0x0000286F File Offset: 0x00000A6F
		public string BccRecipients
		{
			get
			{
				return GroupExpansionRecipients.GetRecipientsByType(RecipientItemType.Bcc, this.recipients);
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002880 File Offset: 0x00000A80
		public static GroupExpansionRecipients Parse(string data)
		{
			if (string.IsNullOrEmpty(data))
			{
				throw new ArgumentNullException("data");
			}
			GroupExpansionRecipients groupExpansionRecipients = new GroupExpansionRecipients();
			string[] array = data.Split(new string[]
			{
				"|"
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string data2 in array)
			{
				groupExpansionRecipients.Recipients.Add(RecipientToIndex.Parse(data2));
			}
			return groupExpansionRecipients;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000028EC File Offset: 0x00000AEC
		public override string ToString()
		{
			if (this.recipients.Count == 0)
			{
				return string.Empty;
			}
			StringBuilder stringBuilder = new StringBuilder();
			foreach (RecipientToIndex recipientToIndex in this.recipients)
			{
				stringBuilder.Append(recipientToIndex.ToString());
				stringBuilder.Append("|");
			}
			if (stringBuilder.Length > 0)
			{
				stringBuilder.Length -= "|".Length;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002990 File Offset: 0x00000B90
		private static string GetRecipientsByType(RecipientItemType itemType, List<RecipientToIndex> recipients)
		{
			string result = string.Empty;
			if (recipients != null && recipients.Count != 0)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int count = recipients.Count;
				for (int i = 0; i < count; i++)
				{
					if (recipients[i].RecipientType.Equals(itemType))
					{
						stringBuilder.Append(string.Format("{1}{0}{2}", ";", recipients[i].DisplayName, recipients[i].EmailAddress));
						if (i < count - 1)
						{
							stringBuilder.Append(",");
						}
					}
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x04000037 RID: 55
		public const string RecipientsSeparatorToken = ",";

		// Token: 0x04000038 RID: 56
		internal const string ItemSeparatorToken = "|";

		// Token: 0x04000039 RID: 57
		private List<RecipientToIndex> recipients = new List<RecipientToIndex>();
	}
}
