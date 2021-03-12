using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x020001F2 RID: 498
	internal class ResolverLog
	{
		// Token: 0x0600164A RID: 5706 RVA: 0x0005ADD0 File Offset: 0x00058FD0
		public ResolverLog(string path, long correlator, bool logContent)
		{
			this.correlator = correlator;
			this.logContent = logContent;
			this.path = path;
			this.unique = Guid.NewGuid();
			string path2 = string.Format("resolver-{0}-{1}.log", correlator, this.unique);
			try
			{
				this.log = new StreamWriter(Path.Combine(path, path2), false, Encoding.UTF8);
			}
			catch (DirectoryNotFoundException)
			{
				Microsoft.Exchange.Diagnostics.Log.CreateLogDirectory(path);
				this.log = new StreamWriter(Path.Combine(path, path2), false, Encoding.UTF8);
			}
		}

		// Token: 0x170005F4 RID: 1524
		// (get) Token: 0x0600164B RID: 5707 RVA: 0x0005AE6C File Offset: 0x0005906C
		public StreamWriter Log
		{
			get
			{
				return this.log;
			}
		}

		// Token: 0x0600164C RID: 5708 RVA: 0x0005AE74 File Offset: 0x00059074
		public void LogMailItem(string title, TransportMailItem mailItem)
		{
			if (this.logContent)
			{
				string contentFileName = string.Format("content-{0}-{1}-{2}.eml", this.correlator, this.unique, this.counter);
				this.LogMailItem(title, mailItem, this.path, contentFileName);
			}
			else
			{
				this.LogMailItem(title, mailItem, null, null);
			}
			this.log.Flush();
			this.counter++;
		}

		// Token: 0x0600164D RID: 5709 RVA: 0x0005AEE9 File Offset: 0x000590E9
		public void Close()
		{
			this.log.Close();
		}

		// Token: 0x0600164E RID: 5710 RVA: 0x0005AEF8 File Offset: 0x000590F8
		private static void LogMailItemContent(TransportMailItem mailItem, string filename)
		{
			Stream stream = mailItem.OpenMimeReadStream();
			Stream stream2 = File.Open(filename, FileMode.Create, FileAccess.Write, FileShare.Read);
			byte[] array = new byte[65536];
			for (int i = stream.Read(array, 0, array.Length); i > 0; i = stream.Read(array, 0, array.Length))
			{
				stream2.Write(array, 0, i);
			}
			stream.Close();
			stream2.Close();
		}

		// Token: 0x0600164F RID: 5711 RVA: 0x0005AF54 File Offset: 0x00059154
		private void WriteLinesIndented(string indent, string data)
		{
			if (data == null)
			{
				this.log.WriteLine();
				return;
			}
			using (StringReader stringReader = new StringReader(data))
			{
				for (string value = stringReader.ReadLine(); value != null; value = stringReader.ReadLine())
				{
					this.log.Write(indent);
					this.log.WriteLine(value);
				}
			}
		}

		// Token: 0x06001650 RID: 5712 RVA: 0x0005AFC0 File Offset: 0x000591C0
		private void WriteBytesAsAscii(byte[] data, int start, int count)
		{
			for (int i = start; i < start + count; i++)
			{
				if (data[i] < 32 || data[i] > 127)
				{
					this.log.Write('.');
				}
				else
				{
					this.log.Write((char)data[i]);
				}
			}
		}

		// Token: 0x06001651 RID: 5713 RVA: 0x0005B008 File Offset: 0x00059208
		private void WriteBytesIndented(string indent, byte[] data)
		{
			int i;
			for (i = 0; i < data.Length - 16; i += 16)
			{
				this.log.Write(indent);
				this.log.Write(HexConverter.ByteArrayToHexString(data, i, 16));
				this.log.Write(' ');
				this.WriteBytesAsAscii(data, i, 16);
				this.log.WriteLine();
			}
			if (i < data.Length)
			{
				this.log.Write(indent);
				this.log.Write(HexConverter.ByteArrayToHexString(data, i, data.Length - i));
				for (int j = data.Length - i; j < 16; j++)
				{
					this.log.Write("   ");
				}
				this.log.Write(' ');
				this.WriteBytesAsAscii(data, i, data.Length - i);
				this.log.WriteLine();
			}
		}

		// Token: 0x06001652 RID: 5714 RVA: 0x0005B0D8 File Offset: 0x000592D8
		private void LogMailItem(string title, TransportMailItem mailItem, string contentFilePath, string contentFileName)
		{
			this.log.WriteLine(title);
			this.log.WriteLine("  Mail item ({0})", contentFileName ?? "content not logged");
			this.log.WriteLine("    Id: {0}", mailItem.RecordId);
			this.log.WriteLine("    InternetMessageId: {0}", mailItem.InternetMessageId);
			this.log.WriteLine("    ReversePath: {0}", mailItem.From);
			this.log.WriteLine("    Subject: {0}", mailItem.Subject);
			this.log.WriteLine("    MimeSize: {0}", mailItem.MimeSize);
			this.LogP1Recipients(mailItem);
			this.log.WriteLine("---------------------------------------------------------------------");
			if (contentFileName != null)
			{
				ResolverLog.LogMailItemContent(mailItem, Path.Combine(contentFilePath, contentFileName));
			}
		}

		// Token: 0x06001653 RID: 5715 RVA: 0x0005B1B4 File Offset: 0x000593B4
		private void WriteValuesIndented<T>(string indent, IEnumerable<T> values)
		{
			foreach (T t in values)
			{
				this.log.WriteLine("{0}{1}", indent, t);
			}
		}

		// Token: 0x06001654 RID: 5716 RVA: 0x0005B20C File Offset: 0x0005940C
		private void LogP1Recipients(TransportMailItem mailItem)
		{
			this.log.WriteLine("    Recipients:");
			foreach (MailRecipient mailRecipient in mailItem.Recipients)
			{
				this.log.WriteLine("      P1 recipient:");
				this.log.WriteLine("        Email: {0}", mailRecipient.Email);
				this.log.WriteLine("        Id: {0}", mailRecipient.MsgRecordId);
				this.log.WriteLine("        AckStatus: {0}", mailRecipient.AckStatus);
				this.log.WriteLine("        Status: {0}", mailRecipient.Status);
				this.log.WriteLine("        SmtpResponse:");
				this.WriteLinesIndented("            ", mailRecipient.SmtpResponse.ToString());
				this.log.WriteLine("        DsnRequested: {0}", mailRecipient.DsnRequested);
				this.log.WriteLine("        DsnMessageId: {0}", mailRecipient.DsnMessageId);
				this.log.WriteLine("        ORcpt: {0}", mailRecipient.ORcpt);
			}
		}

		// Token: 0x04000B05 RID: 2821
		private StreamWriter log;

		// Token: 0x04000B06 RID: 2822
		private long correlator;

		// Token: 0x04000B07 RID: 2823
		private Guid unique;

		// Token: 0x04000B08 RID: 2824
		private string path;

		// Token: 0x04000B09 RID: 2825
		private bool logContent;

		// Token: 0x04000B0A RID: 2826
		private int counter;
	}
}
