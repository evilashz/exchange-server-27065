using System;
using System.IO;
using System.IO.Compression;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x02000027 RID: 39
	internal class ApnsFeedbackFileIO
	{
		// Token: 0x0600018C RID: 396 RVA: 0x0000661E File Offset: 0x0000481E
		public virtual bool Exists(string path)
		{
			return Directory.Exists(path);
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00006626 File Offset: 0x00004826
		public virtual void ExtractFileToDirectory(string sourceArchiveFileName, string destinationDirectoryName)
		{
			ZipFile.ExtractToDirectory(sourceArchiveFileName, destinationDirectoryName);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000662F File Offset: 0x0000482F
		public virtual string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
		{
			return Directory.GetFiles(path, searchPattern, searchOption);
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000663C File Offset: 0x0000483C
		public virtual StreamReader GetFileReader(string path)
		{
			FileStream stream = new FileStream(path, FileMode.Open);
			return new StreamReader(stream);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x00006657 File Offset: 0x00004857
		public virtual void DeleteFile(string path)
		{
			File.Delete(path);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000665F File Offset: 0x0000485F
		public virtual void DeleteFolder(string path)
		{
			Directory.Delete(path, true);
		}

		// Token: 0x0400009A RID: 154
		public static readonly ApnsFeedbackFileIO DefaultFileIO = new ApnsFeedbackFileIO();
	}
}
