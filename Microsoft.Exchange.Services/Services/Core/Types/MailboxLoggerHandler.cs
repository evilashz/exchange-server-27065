using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003EC RID: 1004
	internal sealed class MailboxLoggerHandler : DisposeTrackableBase
	{
		// Token: 0x06001C1E RID: 7198 RVA: 0x0009DD54 File Offset: 0x0009BF54
		public MailboxLoggerHandler(MailboxSession mailboxSession, string protocol, string clientName, bool clearOldLogs)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNullOrEmpty("protocol", protocol);
			ArgumentValidator.ThrowIfNullOrEmpty("clientName", clientName);
			this.mailboxLoggerHelper = new MailboxLogger(mailboxSession, protocol, clientName);
			if (this.Enabled)
			{
				this.currentDataTable = new Dictionary<MailboxLogDataName, string>();
				if (clearOldLogs)
				{
					this.mailboxLoggerHelper.ClearOldLogs(5000, 10485760L);
				}
			}
		}

		// Token: 0x1700036B RID: 875
		// (get) Token: 0x06001C1F RID: 7199 RVA: 0x0009DDC3 File Offset: 0x0009BFC3
		public LocalizedException LastError
		{
			get
			{
				return this.mailboxLoggerHelper.LastError;
			}
		}

		// Token: 0x1700036C RID: 876
		// (get) Token: 0x06001C20 RID: 7200 RVA: 0x0009DDD0 File Offset: 0x0009BFD0
		public bool Enabled
		{
			get
			{
				return this.LastError == null;
			}
		}

		// Token: 0x1700036D RID: 877
		// (get) Token: 0x06001C21 RID: 7201 RVA: 0x0009DDDB File Offset: 0x0009BFDB
		public bool LogsExist
		{
			get
			{
				return this.Enabled && this.mailboxLoggerHelper.LogsExist;
			}
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06001C22 RID: 7202 RVA: 0x0009DDF2 File Offset: 0x0009BFF2
		// (set) Token: 0x06001C23 RID: 7203 RVA: 0x0009DE09 File Offset: 0x0009C009
		public MailboxSession MailboxSession
		{
			get
			{
				if (!this.Enabled)
				{
					return null;
				}
				return this.mailboxLoggerHelper.Mailbox;
			}
			set
			{
				if (!this.Enabled)
				{
					return;
				}
				this.mailboxLoggerHelper.Mailbox = value;
			}
		}

		// Token: 0x06001C24 RID: 7204 RVA: 0x0009DE20 File Offset: 0x0009C020
		public bool DataExists(MailboxLogDataName name)
		{
			return this.Enabled && this.currentDataTable.ContainsKey(name);
		}

		// Token: 0x06001C25 RID: 7205 RVA: 0x0009DE38 File Offset: 0x0009C038
		public string GenerateReport()
		{
			if (!this.Enabled || this.MailboxSession == null)
			{
				return null;
			}
			StringBuilder stringBuilder = new StringBuilder(1024);
			char[] array = new char[1024];
			int num = 0;
			foreach (TextReader textReader in this.mailboxLoggerHelper)
			{
				stringBuilder.Append(string.Format(CultureInfo.InvariantCulture, "\r\n-----------------\r\n Log Entry: {0}\r\n-----------------\r\n\r\n", new object[]
				{
					num.ToString(CultureInfo.InvariantCulture)
				}));
				int charCount;
				while ((charCount = textReader.Read(array, 0, array.Length)) > 0)
				{
					stringBuilder.Append(array, 0, charCount);
				}
				num++;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001C26 RID: 7206 RVA: 0x0009DF08 File Offset: 0x0009C108
		public void GenerateReport(Stream outputStream)
		{
			if (outputStream == null)
			{
				throw new ArgumentNullException("outputStream");
			}
			if (!this.Enabled || this.MailboxSession == null)
			{
				return;
			}
			char[] array = new char[1024];
			byte[] array2 = new byte[Encoding.UTF8.GetMaxByteCount(array.Length)];
			int num = 0;
			foreach (TextReader textReader in this.mailboxLoggerHelper)
			{
				string text = string.Format(CultureInfo.InvariantCulture, "\r\n-----------------\r\n Log Entry: {0}\r\n-----------------\r\n\r\n", new object[]
				{
					num.ToString(CultureInfo.InvariantCulture)
				});
				int bytes = Encoding.UTF8.GetBytes(text, 0, text.Length, array2, 0);
				outputStream.Write(array2, 0, bytes);
				int charCount;
				while ((charCount = textReader.Read(array, 0, array.Length)) > 0)
				{
					bytes = Encoding.UTF8.GetBytes(array, 0, charCount, array2, 0);
					outputStream.Write(array2, 0, bytes);
				}
				num++;
			}
		}

		// Token: 0x06001C27 RID: 7207 RVA: 0x0009E018 File Offset: 0x0009C218
		public void SaveLogToMailbox()
		{
			if (!this.Enabled)
			{
				return;
			}
			this.mailboxLoggerHelper.WriteLog(this.GenerateLog());
		}

		// Token: 0x06001C28 RID: 7208 RVA: 0x0009E034 File Offset: 0x0009C234
		public void AppendToLogInMailbox()
		{
			if (!this.Enabled)
			{
				return;
			}
			this.mailboxLoggerHelper.AppendLog(this.GenerateLog());
			this.mailboxLoggerHelper.Flush();
		}

		// Token: 0x06001C29 RID: 7209 RVA: 0x0009E05B File Offset: 0x0009C25B
		public void SetData(MailboxLogDataName name, object data)
		{
			if (!this.Enabled)
			{
				return;
			}
			this.currentDataTable[name] = data.ToString();
		}

		// Token: 0x06001C2A RID: 7210 RVA: 0x0009E078 File Offset: 0x0009C278
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.mailboxLoggerHelper != null)
			{
				this.mailboxLoggerHelper.Dispose();
				this.mailboxLoggerHelper = null;
			}
		}

		// Token: 0x06001C2B RID: 7211 RVA: 0x0009E097 File Offset: 0x0009C297
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxLoggerHandler>(this);
		}

		// Token: 0x06001C2C RID: 7212 RVA: 0x0009E0A0 File Offset: 0x0009C2A0
		private string GenerateLog()
		{
			StringBuilder stringBuilder = new StringBuilder(1024);
			foreach (KeyValuePair<MailboxLogDataName, string> keyValuePair in this.currentDataTable)
			{
				stringBuilder.Append(keyValuePair.Key).Append(" : ").AppendLine();
				stringBuilder.Append(keyValuePair.Value).AppendLine().AppendLine();
			}
			this.currentDataTable = new Dictionary<MailboxLogDataName, string>();
			return stringBuilder.ToString();
		}

		// Token: 0x040012A1 RID: 4769
		private const int MaxNumberOfMailboxLogs = 5000;

		// Token: 0x040012A2 RID: 4770
		private const long MaxTotalLogSize = 10485760L;

		// Token: 0x040012A3 RID: 4771
		private const string LogHeader = "\r\n-----------------\r\n Log Entry: {0}\r\n-----------------\r\n\r\n";

		// Token: 0x040012A4 RID: 4772
		private Dictionary<MailboxLogDataName, string> currentDataTable;

		// Token: 0x040012A5 RID: 4773
		private MailboxLogger mailboxLoggerHelper;
	}
}
