using System;
using System.Globalization;
using System.Text;
using Microsoft.Exchange.Data.TextConverters;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000866 RID: 2150
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UniqueItemHash
	{
		// Token: 0x060050FC RID: 20732 RVA: 0x00151486 File Offset: 0x0014F686
		public UniqueItemHash(string internetMsgId, string topic, BodyTagInfo btInfo, bool sentItems)
		{
			this.internetMessageId = internetMsgId;
			this.conversationTopic = topic;
			this.bodyTagInfo = btInfo;
			this.isSentItems = sentItems;
		}

		// Token: 0x060050FD RID: 20733 RVA: 0x001514AC File Offset: 0x0014F6AC
		public static UniqueItemHash Create(IStorePropertyBag propertyBag, bool isOnSentItems)
		{
			string internetMsgId = propertyBag.TryGetProperty(ItemSchema.InternetMessageId) as string;
			string text = propertyBag.TryGetProperty(ItemSchema.ConversationTopic) as string;
			byte[] array = propertyBag.TryGetProperty(ItemSchema.BodyTag) as byte[];
			return new UniqueItemHash(internetMsgId, string.IsNullOrEmpty(text) ? string.Empty : text, (array != null) ? BodyTagInfo.FromByteArray(array) : null, isOnSentItems);
		}

		// Token: 0x170016AF RID: 5807
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x0015150F File Offset: 0x0014F70F
		internal string InternetMessageId
		{
			get
			{
				return this.internetMessageId;
			}
		}

		// Token: 0x170016B0 RID: 5808
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x00151517 File Offset: 0x0014F717
		internal string ConversationTopic
		{
			get
			{
				return this.conversationTopic;
			}
		}

		// Token: 0x06005100 RID: 20736 RVA: 0x00151520 File Offset: 0x0014F720
		public static UniqueItemHash Parse(string serializedUniqueItemHash)
		{
			if (string.IsNullOrEmpty(serializedUniqueItemHash))
			{
				return null;
			}
			bool sentItems = false;
			if (serializedUniqueItemHash.Substring(0, 1) == "1")
			{
				sentItems = true;
			}
			else if (serializedUniqueItemHash.Substring(0, 1) != "0")
			{
				throw new ArgumentException("Expected sent items serialized value is not either 1 or 0.");
			}
			int num = 1;
			int num2 = 0;
			if (!int.TryParse(serializedUniqueItemHash.Substring(num, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
			{
				throw new ArgumentException("Cannot read the length indicator value of internet message id part.");
			}
			num += 4;
			string internetMsgId = null;
			if (num2 != 0)
			{
				internetMsgId = serializedUniqueItemHash.Substring(num, num2);
				num += num2;
			}
			if (!int.TryParse(serializedUniqueItemHash.Substring(num, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
			{
				throw new ArgumentException("Cannot read the length indicator value of conversation topic part.");
			}
			num += 4;
			string topic = null;
			if (num2 != 0)
			{
				topic = serializedUniqueItemHash.Substring(num, num2);
				num += num2;
			}
			if (!int.TryParse(serializedUniqueItemHash.Substring(num, 4), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out num2))
			{
				throw new ArgumentException("Cannot read the length indicator value of body tag info part.");
			}
			num += 4;
			BodyTagInfo btInfo = null;
			if (num2 != 0)
			{
				string bodyTagInfoString = serializedUniqueItemHash.Substring(num, num2);
				btInfo = UniqueItemHash.DeserializeBodyTagInfoFromString(bodyTagInfoString);
				num += num2;
			}
			if (num != serializedUniqueItemHash.Length)
			{
				throw new ArgumentException(string.Format("The serialized unique item hash has not been completely parsed. Start index = {0}, Length = {1}", num, serializedUniqueItemHash.Length));
			}
			return new UniqueItemHash(internetMsgId, topic, btInfo, sentItems);
		}

		// Token: 0x06005101 RID: 20737 RVA: 0x0015166C File Offset: 0x0014F86C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}", this.isSentItems ? "1" : "0");
			if (!string.IsNullOrEmpty(this.internetMessageId))
			{
				stringBuilder.AppendFormat("{0:X4}", this.internetMessageId.Length);
				stringBuilder.Append(this.internetMessageId);
			}
			else
			{
				stringBuilder.AppendFormat("{0:X4}", 0);
			}
			if (!string.IsNullOrEmpty(this.conversationTopic))
			{
				stringBuilder.AppendFormat("{0:X4}", this.conversationTopic.Length);
				stringBuilder.Append(this.conversationTopic);
			}
			else
			{
				stringBuilder.AppendFormat("{0:X4}", 0);
			}
			string text = UniqueItemHash.SerializeBodyTagInfoToString(this.bodyTagInfo);
			if (!string.IsNullOrEmpty(text))
			{
				stringBuilder.AppendFormat("{0:X4}", text.Length);
				stringBuilder.Append(text);
			}
			else
			{
				stringBuilder.AppendFormat("{0:X4}", 0);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06005102 RID: 20738 RVA: 0x00151780 File Offset: 0x0014F980
		public override int GetHashCode()
		{
			if (string.IsNullOrEmpty(this.conversationTopic))
			{
				if (!string.IsNullOrEmpty(this.internetMessageId))
				{
					return this.internetMessageId.GetHashCode();
				}
				return 0;
			}
			else
			{
				if (string.IsNullOrEmpty(this.internetMessageId))
				{
					return this.conversationTopic.GetHashCode();
				}
				return this.internetMessageId.GetHashCode() ^ this.conversationTopic.GetHashCode();
			}
		}

		// Token: 0x06005103 RID: 20739 RVA: 0x001517E8 File Offset: 0x0014F9E8
		public override bool Equals(object obj)
		{
			UniqueItemHash uniqueItemHash = obj as UniqueItemHash;
			if (uniqueItemHash == null)
			{
				return false;
			}
			if (!UniqueItemHash.CompareInternetMessageIds(this.internetMessageId, uniqueItemHash.internetMessageId) || !ConversationIndex.CompareTopics(this.conversationTopic, uniqueItemHash.conversationTopic))
			{
				return false;
			}
			if (this.bodyTagInfo != null && uniqueItemHash.bodyTagInfo != null)
			{
				return this.bodyTagInfo.Equals(uniqueItemHash.bodyTagInfo);
			}
			return (!(this.bodyTagInfo == null) || !(uniqueItemHash.bodyTagInfo == null)) && ((this.bodyTagInfo == null && this.isSentItems) || (uniqueItemHash.bodyTagInfo == null && uniqueItemHash.isSentItems));
		}

		// Token: 0x06005104 RID: 20740 RVA: 0x001518A7 File Offset: 0x0014FAA7
		private static bool CompareInternetMessageIds(string id, string otherId)
		{
			return !string.IsNullOrEmpty(id) && !string.IsNullOrEmpty(otherId) && id.Equals(otherId, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06005105 RID: 20741 RVA: 0x001518C4 File Offset: 0x0014FAC4
		private static string SerializeBodyTagInfoToString(BodyTagInfo bodyTagInfo)
		{
			if (bodyTagInfo == null)
			{
				return null;
			}
			byte[] array = bodyTagInfo.ToByteArray();
			return CTSGlobals.AsciiEncoding.GetString(array, 0, array.Length);
		}

		// Token: 0x06005106 RID: 20742 RVA: 0x001518F4 File Offset: 0x0014FAF4
		private static BodyTagInfo DeserializeBodyTagInfoFromString(string bodyTagInfoString)
		{
			if (string.IsNullOrEmpty(bodyTagInfoString))
			{
				return null;
			}
			byte[] bytes = CTSGlobals.AsciiEncoding.GetBytes(bodyTagInfoString);
			return BodyTagInfo.FromByteArray(bytes);
		}

		// Token: 0x04002C38 RID: 11320
		private string internetMessageId;

		// Token: 0x04002C39 RID: 11321
		private string conversationTopic;

		// Token: 0x04002C3A RID: 11322
		private BodyTagInfo bodyTagInfo;

		// Token: 0x04002C3B RID: 11323
		private bool isSentItems;
	}
}
