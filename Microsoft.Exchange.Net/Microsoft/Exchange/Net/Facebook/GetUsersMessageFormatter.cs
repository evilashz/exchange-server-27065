using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Json;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.Facebook
{
	// Token: 0x0200072C RID: 1836
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class GetUsersMessageFormatter : IClientMessageFormatter
	{
		// Token: 0x06002310 RID: 8976 RVA: 0x00047A58 File Offset: 0x00045C58
		internal GetUsersMessageFormatter(IClientMessageFormatter previousFormatter)
		{
			ArgumentValidator.ThrowIfNull("previousFormatter", previousFormatter);
			this.previousFormatter = previousFormatter;
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x00047A74 File Offset: 0x00045C74
		public object DeserializeReply(Message message, object[] parameters)
		{
			if (message.IsEmpty || message.IsFault)
			{
				return this.previousFormatter.DeserializeReply(message, parameters);
			}
			object result;
			using (XmlDictionaryReader readerAtBodyContents = message.GetReaderAtBodyContents())
			{
				List<FacebookUser> list = new List<FacebookUser>();
				if (!"root".Equals(readerAtBodyContents.LocalName, StringComparison.Ordinal) && !readerAtBodyContents.ReadToFollowing("root"))
				{
					result = new FacebookUsersList
					{
						Users = new List<FacebookUser>()
					};
				}
				else
				{
					readerAtBodyContents.Read();
					DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(FacebookUser), "item");
					while ("item".Equals(readerAtBodyContents.LocalName, StringComparison.Ordinal))
					{
						list.Add((FacebookUser)dataContractJsonSerializer.ReadObject(readerAtBodyContents, false));
					}
					readerAtBodyContents.ReadEndElement();
					result = new FacebookUsersList
					{
						Users = list
					};
				}
			}
			return result;
		}

		// Token: 0x06002312 RID: 8978 RVA: 0x00047B60 File Offset: 0x00045D60
		public Message SerializeRequest(MessageVersion messageVersion, object[] parameters)
		{
			return this.previousFormatter.SerializeRequest(messageVersion, parameters);
		}

		// Token: 0x0400213A RID: 8506
		private IClientMessageFormatter previousFormatter;
	}
}
