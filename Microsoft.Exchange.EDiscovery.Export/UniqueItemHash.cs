using System;
using System.Globalization;
using System.Text;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000029 RID: 41
	internal class UniqueItemHash
	{
		// Token: 0x06000166 RID: 358 RVA: 0x00005637 File Offset: 0x00003837
		public UniqueItemHash(string internetMsgId, string topic, BodyTagInfo btInfo, bool sentItems)
		{
			if (string.IsNullOrEmpty(internetMsgId))
			{
				throw new ArgumentNullException("internetMsgId");
			}
			this.internetMessageId = internetMsgId;
			this.conversationTopic = topic;
			this.bodyTagInfo = btInfo;
			this.isSentItems = sentItems;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x06000167 RID: 359 RVA: 0x0000566F File Offset: 0x0000386F
		public string InternetMessageId
		{
			get
			{
				return this.internetMessageId;
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x06000168 RID: 360 RVA: 0x00005677 File Offset: 0x00003877
		public string ConversationTopic
		{
			get
			{
				return this.conversationTopic;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000567F File Offset: 0x0000387F
		public bool IsSentItems
		{
			get
			{
				return this.isSentItems;
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x0600016A RID: 362 RVA: 0x00005687 File Offset: 0x00003887
		public BodyTagInfo BodyTagInfo
		{
			get
			{
				return this.bodyTagInfo;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00005690 File Offset: 0x00003890
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
			if (num2 == 0)
			{
				throw new ArgumentException("Internet message id value read from serialized item is empty.");
			}
			num += 4;
			string internetMsgId = serializedUniqueItemHash.Substring(num, num2);
			num += num2;
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

		// Token: 0x0600016C RID: 364 RVA: 0x000057E4 File Offset: 0x000039E4
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("{0}", this.isSentItems ? "1" : "0");
			stringBuilder.AppendFormat("{0:X4}", this.internetMessageId.Length);
			stringBuilder.Append(this.internetMessageId);
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

		// Token: 0x0600016D RID: 365 RVA: 0x000058D7 File Offset: 0x00003AD7
		public override int GetHashCode()
		{
			if (string.IsNullOrEmpty(this.conversationTopic))
			{
				return this.internetMessageId.GetHashCode();
			}
			return this.internetMessageId.GetHashCode() ^ this.conversationTopic.GetHashCode();
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000590C File Offset: 0x00003B0C
		public override bool Equals(object obj)
		{
			UniqueItemHash uniqueItemHash = obj as UniqueItemHash;
			if (uniqueItemHash == null)
			{
				return false;
			}
			if (!this.internetMessageId.Equals(uniqueItemHash.internetMessageId, StringComparison.OrdinalIgnoreCase) || !UniqueItemHash.CompareTopics(this.conversationTopic, uniqueItemHash.conversationTopic))
			{
				return false;
			}
			if (this.bodyTagInfo != null && uniqueItemHash.bodyTagInfo != null)
			{
				return this.bodyTagInfo.Equals(uniqueItemHash.bodyTagInfo);
			}
			return (!(this.bodyTagInfo == null) || !(uniqueItemHash.bodyTagInfo == null)) && ((this.bodyTagInfo == null && this.isSentItems) || (uniqueItemHash.bodyTagInfo == null && uniqueItemHash.isSentItems));
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000059CC File Offset: 0x00003BCC
		private static bool CompareTopics(string incomingTopic, string foundTopic)
		{
			return (string.IsNullOrEmpty(foundTopic) && string.IsNullOrEmpty(incomingTopic)) || (foundTopic != null && incomingTopic != null && 0 == string.Compare(incomingTopic, foundTopic, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x06000170 RID: 368 RVA: 0x00005A08 File Offset: 0x00003C08
		private static string SerializeBodyTagInfoToString(BodyTagInfo bodyTagInfo)
		{
			if (bodyTagInfo == null)
			{
				return null;
			}
			byte[] bytes = bodyTagInfo.ToByteArray();
			return UniqueItemHash.asciiEncoding.GetString(bytes);
		}

		// Token: 0x06000171 RID: 369 RVA: 0x00005A34 File Offset: 0x00003C34
		private static BodyTagInfo DeserializeBodyTagInfoFromString(string bodyTagInfoString)
		{
			if (string.IsNullOrEmpty(bodyTagInfoString))
			{
				return null;
			}
			byte[] bytes = UniqueItemHash.asciiEncoding.GetBytes(bodyTagInfoString);
			return BodyTagInfo.FromByteArray(bytes);
		}

		// Token: 0x04000096 RID: 150
		private static readonly Encoding asciiEncoding = new ASCIIEncoding();

		// Token: 0x04000097 RID: 151
		private readonly string internetMessageId;

		// Token: 0x04000098 RID: 152
		private readonly string conversationTopic;

		// Token: 0x04000099 RID: 153
		private readonly BodyTagInfo bodyTagInfo;

		// Token: 0x0400009A RID: 154
		private readonly bool isSentItems;
	}
}
