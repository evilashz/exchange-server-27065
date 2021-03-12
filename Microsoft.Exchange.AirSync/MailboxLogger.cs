using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AirSync;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x020000D6 RID: 214
	internal class MailboxLogger : DisposeTrackableBase
	{
		// Token: 0x06000C54 RID: 3156 RVA: 0x00041287 File Offset: 0x0003F487
		public MailboxLogger(MailboxSession mailboxSession, DeviceIdentity deviceIdentity) : this(mailboxSession, deviceIdentity, false)
		{
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x00041294 File Offset: 0x0003F494
		public MailboxLogger(MailboxSession mailboxSession, DeviceIdentity deviceIdentity, bool clearOldLogs)
		{
			ArgumentValidator.ThrowIfNull("mailboxSession", mailboxSession);
			ArgumentValidator.ThrowIfNull("deviceIdentity", deviceIdentity);
			string clientName = deviceIdentity.DeviceType + "_" + deviceIdentity.DeviceId;
			this.mailboxLoggerHelper = new MailboxLogger(mailboxSession, deviceIdentity.Protocol, clientName);
			if (this.Enabled)
			{
				this.currentDataTable = new Dictionary<MailboxLogDataName, string>();
				if (clearOldLogs)
				{
					this.mailboxLoggerHelper.ClearOldLogs(5000, 10485760L);
				}
			}
		}

		// Token: 0x170004E5 RID: 1253
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00041313 File Offset: 0x0003F513
		public LocalizedException LastError
		{
			get
			{
				return this.mailboxLoggerHelper.LastError;
			}
		}

		// Token: 0x170004E6 RID: 1254
		// (get) Token: 0x06000C57 RID: 3159 RVA: 0x00041320 File Offset: 0x0003F520
		public bool Enabled
		{
			get
			{
				return this.LastError == null;
			}
		}

		// Token: 0x170004E7 RID: 1255
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x0004132B File Offset: 0x0003F52B
		public bool LogsExist
		{
			get
			{
				return this.Enabled && this.mailboxLoggerHelper.LogsExist;
			}
		}

		// Token: 0x170004E8 RID: 1256
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00041342 File Offset: 0x0003F542
		// (set) Token: 0x06000C5A RID: 3162 RVA: 0x00041359 File Offset: 0x0003F559
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
				AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Setting MailboxLogger.MailboxSession to {0}", (value == null) ? "null" : "a non-null value");
				this.mailboxLoggerHelper.Mailbox = value;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x0004138F File Offset: 0x0003F58F
		public bool DataExists(MailboxLogDataName name)
		{
			return this.Enabled && this.currentDataTable.ContainsKey(name);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000413A8 File Offset: 0x0003F5A8
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

		// Token: 0x06000C5D RID: 3165 RVA: 0x00041478 File Offset: 0x0003F678
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

		// Token: 0x06000C5E RID: 3166 RVA: 0x00041588 File Offset: 0x0003F788
		public void LogRequestHeader(IAirSyncRequest request)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			this.SetData(MailboxLogDataName.RequestHeader, request.GetHeadersAsString());
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000415AE File Offset: 0x0003F7AE
		public void LogResponseHead(IAirSyncResponse response)
		{
			if (!this.Enabled)
			{
				return;
			}
			if (response == null)
			{
				throw new ArgumentNullException("response");
			}
			this.SetData(MailboxLogDataName.ResponseHeader, response.GetHeadersAsString());
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000415D4 File Offset: 0x0003F7D4
		public void SaveLogToMailbox()
		{
			if (!this.Enabled)
			{
				return;
			}
			this.mailboxLoggerHelper.WriteLog(this.GenerateLog());
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000415F0 File Offset: 0x0003F7F0
		public void AppendToLogInMailbox()
		{
			if (!this.Enabled)
			{
				return;
			}
			this.mailboxLoggerHelper.AppendLog(this.GenerateLog());
			this.mailboxLoggerHelper.Flush();
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x00041617 File Offset: 0x0003F817
		public void SetData(MailboxLogDataName name, object data)
		{
			if (!this.Enabled)
			{
				return;
			}
			this.currentDataTable[name] = data.ToString();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x00041634 File Offset: 0x0003F834
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing && this.mailboxLoggerHelper != null)
			{
				this.mailboxLoggerHelper.Dispose();
				this.mailboxLoggerHelper = null;
			}
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x00041653 File Offset: 0x0003F853
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<MailboxLogger>(this);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0004165C File Offset: 0x0003F85C
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

		// Token: 0x04000790 RID: 1936
		private const int MaxNumberOfMailboxLogs = 5000;

		// Token: 0x04000791 RID: 1937
		private const long MaxTotalLogSize = 10485760L;

		// Token: 0x04000792 RID: 1938
		private const string LogHeader = "\r\n-----------------\r\n Log Entry: {0}\r\n-----------------\r\n\r\n";

		// Token: 0x04000793 RID: 1939
		private Dictionary<MailboxLogDataName, string> currentDataTable;

		// Token: 0x04000794 RID: 1940
		private MailboxLogger mailboxLoggerHelper;
	}
}
