using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery.Agents
{
	// Token: 0x02000083 RID: 131
	internal sealed class EmailMessageHash
	{
		// Token: 0x06000492 RID: 1170 RVA: 0x00017CF4 File Offset: 0x00015EF4
		internal EmailMessageHash(EmailMessage emailMessage)
		{
			if (emailMessage == null)
			{
				throw new ArgumentNullException("emailMessage");
			}
			int num = 0;
			if (emailMessage.Attachments != null)
			{
				num = emailMessage.Attachments.Count;
			}
			int capacity = 8 + 9 * num;
			StringBuilder stringBuilder = new StringBuilder(capacity);
			using (Stream contentReadStream = emailMessage.Body.GetContentReadStream())
			{
				stringBuilder.AppendFormat("{0:X8}", EmailMessageHash.ComputeCRC(contentReadStream));
			}
			if (num > 0)
			{
				foreach (Attachment attachment in emailMessage.Attachments)
				{
					using (Stream contentReadStream2 = attachment.GetContentReadStream())
					{
						stringBuilder.AppendFormat(",{0:X8}", EmailMessageHash.ComputeCRC(contentReadStream2));
					}
				}
			}
			this.cachedHashString = stringBuilder.ToString();
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00017E00 File Offset: 0x00016000
		private EmailMessageHash(string hashString)
		{
			this.cachedHashString = hashString;
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x00017E10 File Offset: 0x00016010
		internal static bool TryGetFromHeader(HeaderList mimeHeader, out EmailMessageHash result)
		{
			result = null;
			string property = XHeaderUtils.GetProperty(mimeHeader, "X-MS-Exchange-Forest-EmailMessageHash");
			if (string.IsNullOrEmpty(property))
			{
				return false;
			}
			result = new EmailMessageHash(property);
			return true;
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x00017E40 File Offset: 0x00016040
		private static uint ComputeCRC(Stream stream)
		{
			uint num = 0U;
			int num2 = 131072;
			byte[] array = new byte[num2];
			for (;;)
			{
				int num3 = stream.Read(array, 0, num2);
				if (num3 == 0)
				{
					break;
				}
				num = Microsoft.Exchange.Data.Storage.ComputeCRC.Compute(num, array, 0, num3);
			}
			return num;
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x00017E76 File Offset: 0x00016076
		public override string ToString()
		{
			return this.cachedHashString;
		}

		// Token: 0x06000497 RID: 1175 RVA: 0x00017E7E File Offset: 0x0001607E
		internal void SetToHeader(HeaderList mimeHeader)
		{
			XHeaderUtils.SetProperty(mimeHeader, "X-MS-Exchange-Forest-EmailMessageHash", this.cachedHashString);
		}

		// Token: 0x04000296 RID: 662
		private const int HexHashLength = 8;

		// Token: 0x04000297 RID: 663
		private const string BodyHashFormat = "{0:X8}";

		// Token: 0x04000298 RID: 664
		private const string AttachmentHashFormat = ",{0:X8}";

		// Token: 0x04000299 RID: 665
		private const string XHeaderName = "X-MS-Exchange-Forest-EmailMessageHash";

		// Token: 0x0400029A RID: 666
		private readonly string cachedHashString;
	}
}
