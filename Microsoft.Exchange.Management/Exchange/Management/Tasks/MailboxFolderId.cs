using System;
using System.Text;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200042F RID: 1071
	[Serializable]
	public class MailboxFolderId : ObjectId
	{
		// Token: 0x0600258A RID: 9610 RVA: 0x0009754C File Offset: 0x0009574C
		public MailboxFolderId(string mailboxName, string url)
		{
			if (string.IsNullOrEmpty(mailboxName))
			{
				throw new ArgumentNullException("mailboxName");
			}
			if (url == null)
			{
				throw new ArgumentNullException("url");
			}
			this.mailboxName = mailboxName;
			this.path = url.Replace('/', '\\');
		}

		// Token: 0x0600258B RID: 9611 RVA: 0x0009758C File Offset: 0x0009578C
		public override byte[] GetBytes()
		{
			return Encoding.Unicode.GetBytes(this.ToString());
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x000975A0 File Offset: 0x000957A0
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(this.mailboxName);
			stringBuilder.Append(this.path);
			return stringBuilder.ToString();
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x000975D4 File Offset: 0x000957D4
		public static MailboxFolderId Parse(string folderIdentity)
		{
			if (string.IsNullOrEmpty(folderIdentity))
			{
				throw new ArgumentNullException("folderIdentity");
			}
			int num = folderIdentity.IndexOf('\\');
			if (num <= 0 || num >= folderIdentity.Length - 1)
			{
				throw new ArgumentException(Strings.InvalidMailboxFolderIdentity(folderIdentity), "folderIdentity");
			}
			string text = folderIdentity.Substring(0, num);
			string url = folderIdentity.Substring(num);
			return new MailboxFolderId(text, url);
		}

		// Token: 0x04001D75 RID: 7541
		private readonly string mailboxName;

		// Token: 0x04001D76 RID: 7542
		private readonly string path;
	}
}
