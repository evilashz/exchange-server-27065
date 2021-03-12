using System;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Transport.Email;

namespace Microsoft.Exchange.Transport.Agent.Search
{
	// Token: 0x02000002 RID: 2
	internal sealed class EmailMessageHash
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
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

		// Token: 0x06000002 RID: 2 RVA: 0x000021DC File Offset: 0x000003DC
		private EmailMessageHash(string hashString)
		{
			this.cachedHashString = hashString;
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000021EB File Offset: 0x000003EB
		public override string ToString()
		{
			return this.cachedHashString;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000021F4 File Offset: 0x000003F4
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

		// Token: 0x06000005 RID: 5 RVA: 0x00002223 File Offset: 0x00000423
		internal void SetToHeader(HeaderList mimeHeader)
		{
			XHeaderUtils.SetProperty(mimeHeader, "X-MS-Exchange-Forest-EmailMessageHash", this.cachedHashString);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002238 File Offset: 0x00000438
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

		// Token: 0x04000001 RID: 1
		private const int HexHashLength = 8;

		// Token: 0x04000002 RID: 2
		private const string BodyHashFormat = "{0:X8}";

		// Token: 0x04000003 RID: 3
		private const string AttachmentHashFormat = ",{0:X8}";

		// Token: 0x04000004 RID: 4
		private const string XHeaderName = "X-MS-Exchange-Forest-EmailMessageHash";

		// Token: 0x04000005 RID: 5
		private readonly string cachedHashString;
	}
}
