using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000D1A RID: 3354
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GroupExpansionRecipients
	{
		// Token: 0x17001EF0 RID: 7920
		// (get) Token: 0x060073E9 RID: 29673 RVA: 0x002024A1 File Offset: 0x002006A1
		public List<RecipientToIndex> Recipients
		{
			get
			{
				return this.recipients;
			}
		}

		// Token: 0x060073EA RID: 29674 RVA: 0x002024AC File Offset: 0x002006AC
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

		// Token: 0x060073EB RID: 29675 RVA: 0x00202518 File Offset: 0x00200718
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

		// Token: 0x060073EC RID: 29676 RVA: 0x002025BC File Offset: 0x002007BC
		internal static GroupExpansionRecipients RetrieveFromStore(MessageItem messageItem, StorePropertyDefinition propertyDefinition)
		{
			if (messageItem == null)
			{
				return null;
			}
			try
			{
				object propertyValue = messageItem.TryGetProperty(propertyDefinition);
				if (PropertyError.IsPropertyNotFound(propertyValue))
				{
					return null;
				}
				if (PropertyError.IsPropertyValueTooBig(propertyValue))
				{
					using (Stream stream = messageItem.OpenPropertyStream(propertyDefinition, PropertyOpenMode.ReadOnly))
					{
						Encoding encoding = new UnicodeEncoding(false, false);
						using (StreamReader streamReader = new StreamReader(stream, encoding))
						{
							string data = streamReader.ReadToEnd();
							return GroupExpansionRecipients.Parse(data);
						}
					}
				}
				if (PropertyError.IsPropertyError(propertyValue))
				{
					return null;
				}
				string text = messageItem[propertyDefinition] as string;
				if (!string.IsNullOrEmpty(text))
				{
					return GroupExpansionRecipients.Parse(text);
				}
			}
			catch (PropertyErrorException)
			{
				return null;
			}
			return null;
		}

		// Token: 0x060073ED RID: 29677 RVA: 0x0020269C File Offset: 0x0020089C
		internal void SaveToStore(MessageItem messageItem, StorePropertyDefinition propertyDefinition)
		{
			string value = this.ToString();
			if (string.IsNullOrEmpty(value))
			{
				messageItem[propertyDefinition] = null;
				return;
			}
			using (Stream stream = messageItem.OpenPropertyStream(propertyDefinition, PropertyOpenMode.Create))
			{
				Encoding encoding = new UnicodeEncoding(false, false);
				using (StreamWriter streamWriter = new StreamWriter(stream, encoding))
				{
					streamWriter.Write(value);
					streamWriter.Flush();
				}
			}
		}

		// Token: 0x040050F9 RID: 20729
		internal const string ItemSeparatorToken = "|";

		// Token: 0x040050FA RID: 20730
		private List<RecipientToIndex> recipients = new List<RecipientToIndex>();
	}
}
