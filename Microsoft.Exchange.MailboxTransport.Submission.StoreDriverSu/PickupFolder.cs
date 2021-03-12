using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission
{
	// Token: 0x0200003C RID: 60
	internal class PickupFolder
	{
		// Token: 0x06000242 RID: 578 RVA: 0x0000CEB4 File Offset: 0x0000B0B4
		public bool WriteMessage(TransportMailItem mailItem, IEnumerable<MailRecipient> recipients, out SmtpResponse smtpResponse, out string exceptionMessage)
		{
			Stream stream = null;
			bool result = false;
			exceptionMessage = null;
			smtpResponse = SmtpResponse.NoopOk;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("{0}-{1}-{2}.{3}.eml", new object[]
				{
					Environment.MachineName,
					mailItem.RecordId,
					DateTime.UtcNow.ToString("yyyyMMddHHmmssZ", DateTimeFormatInfo.InvariantInfo),
					((IQueueItem)mailItem).Priority
				});
				string path = Path.Combine(this.dropDirectory, stringBuilder.ToString());
				if (!ExportStream.TryCreate(mailItem, recipients, true, out stream) || stream == null)
				{
					throw new InvalidOperationException("Failed to create an export stream because there were no ready recipients");
				}
				using (stream)
				{
					using (FileStream fileStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
					{
						stream.Position = 0L;
						for (;;)
						{
							int num = stream.Read(this.buffer, 0, 65536);
							if (num == 0)
							{
								break;
							}
							fileStream.Write(this.buffer, 0, num);
						}
					}
				}
				result = true;
			}
			catch (PathTooLongException)
			{
				smtpResponse = AckReason.GWPathTooLongException;
			}
			catch (IOException ex)
			{
				exceptionMessage = ex.Message;
				smtpResponse = AckReason.GWIOException;
			}
			catch (UnauthorizedAccessException)
			{
				smtpResponse = AckReason.GWUnauthorizedAccess;
			}
			return result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000D038 File Offset: 0x0000B238
		private bool DropDirectoryExists()
		{
			string rootDropDirectoryPath = Components.Configuration.LocalServer.TransportServer.RootDropDirectoryPath;
			this.dropDirectory = rootDropDirectoryPath;
			this.directoryInfo = new DirectoryInfo(this.dropDirectory);
			return this.directoryInfo.Exists;
		}

		// Token: 0x04000135 RID: 309
		private const int BlockSize = 65536;

		// Token: 0x04000136 RID: 310
		private DirectoryInfo directoryInfo;

		// Token: 0x04000137 RID: 311
		private string dropDirectory = "C:\\root\\";

		// Token: 0x04000138 RID: 312
		private byte[] buffer = new byte[65536];
	}
}
