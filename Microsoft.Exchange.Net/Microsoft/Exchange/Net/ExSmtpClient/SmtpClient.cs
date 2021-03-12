using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x02000703 RID: 1795
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SmtpClient : DisposeTrackableBase
	{
		// Token: 0x060021B5 RID: 8629 RVA: 0x000437B0 File Offset: 0x000419B0
		internal SmtpClient(string host, int port, ISmtpClientDebugOutput smtpClientDebugOutput)
		{
			if (string.IsNullOrEmpty(host))
			{
				throw new ArgumentNullException("host");
			}
			if (smtpClientDebugOutput == null)
			{
				throw new ArgumentNullException("smtpClientDebugOutput");
			}
			this.serverName = host;
			this.serverPort = port;
			this.smtpClientDebugOutput = smtpClientDebugOutput;
			this.smtpTalk = new SmtpTalk(this.smtpClientDebugOutput);
		}

		// Token: 0x170008B3 RID: 2227
		// (get) Token: 0x060021B6 RID: 8630 RVA: 0x00043812 File Offset: 0x00041A12
		// (set) Token: 0x060021B7 RID: 8631 RVA: 0x00043820 File Offset: 0x00041A20
		internal string From
		{
			get
			{
				base.CheckDisposed();
				return this.from;
			}
			set
			{
				base.CheckDisposed();
				this.from = value;
			}
		}

		// Token: 0x170008B4 RID: 2228
		// (get) Token: 0x060021B8 RID: 8632 RVA: 0x00043830 File Offset: 0x00041A30
		internal IList<KeyValuePair<string, string>> FromParameters
		{
			get
			{
				IList<KeyValuePair<string, string>> result;
				if ((result = this.fromParameters) == null)
				{
					result = (this.fromParameters = new List<KeyValuePair<string, string>>());
				}
				return result;
			}
		}

		// Token: 0x170008B5 RID: 2229
		// (get) Token: 0x060021B9 RID: 8633 RVA: 0x00043855 File Offset: 0x00041A55
		// (set) Token: 0x060021BA RID: 8634 RVA: 0x0004386D File Offset: 0x00041A6D
		internal string[] To
		{
			get
			{
				base.CheckDisposed();
				return (string[])this.recips.Clone();
			}
			set
			{
				base.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.recips = (string[])value.Clone();
			}
		}

		// Token: 0x170008B6 RID: 2230
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0004388F File Offset: 0x00041A8F
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0004389D File Offset: 0x00041A9D
		internal bool NDRRequired
		{
			get
			{
				base.CheckDisposed();
				return this.ndrRequired;
			}
			set
			{
				base.CheckDisposed();
				this.ndrRequired = value;
			}
		}

		// Token: 0x170008B7 RID: 2231
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x000438AC File Offset: 0x00041AAC
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x000438BA File Offset: 0x00041ABA
		internal MemoryStream DataStream
		{
			get
			{
				base.CheckDisposed();
				return this.dataStream;
			}
			set
			{
				base.CheckDisposed();
				if (value == null)
				{
					throw new ArgumentNullException();
				}
				this.dataStream = value;
			}
		}

		// Token: 0x170008B8 RID: 2232
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x000438D2 File Offset: 0x00041AD2
		private SmtpTalk Talk
		{
			get
			{
				return this.smtpTalk;
			}
		}

		// Token: 0x060021C0 RID: 8640 RVA: 0x000438DA File Offset: 0x00041ADA
		internal void AuthCredentials(NetworkCredential credentials)
		{
			base.CheckDisposed();
			this.authCredentials = credentials;
		}

		// Token: 0x060021C1 RID: 8641 RVA: 0x000438EC File Offset: 0x00041AEC
		internal void Submit()
		{
			base.CheckDisposed();
			if (this.dataStream == null)
			{
				throw new ArgumentNullException("DataStream");
			}
			if (this.From == null)
			{
				throw new ArgumentNullException("From");
			}
			if (this.To == null)
			{
				throw new ArgumentNullException("To");
			}
			this.Talk.Connect(this.serverName, this.serverPort);
			this.Talk.Ehlo();
			this.Talk.StartTls(true);
			this.Talk.Ehlo();
			this.Talk.Authenticate(this.authCredentials, SmtpSspiMechanism.Kerberos);
			this.Talk.MailFrom(this.from, this.fromParameters);
			for (int i = 0; i < this.recips.Length; i++)
			{
				this.Talk.RcptTo(this.recips[i], new bool?(this.ndrRequired));
			}
			this.dataStream.Position = 0L;
			this.Talk.Chunking(this.dataStream);
			try
			{
				this.Talk.Quit();
			}
			catch (SocketException ex)
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, 0, "Failed to QUIT. '{0}'", new object[]
				{
					ex
				});
			}
			catch (IOException ex2)
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, 0, "Failed to QUIT '{0}'", new object[]
				{
					ex2
				});
			}
		}

		// Token: 0x060021C2 RID: 8642 RVA: 0x00043A6C File Offset: 0x00041C6C
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.dataStream != null)
				{
					this.dataStream.Flush();
					this.dataStream.Dispose();
					this.dataStream = null;
				}
				if (this.tempfile != null)
				{
					this.tempfile.Delete();
					this.tempfile = null;
				}
				this.smtpTalk.Dispose();
			}
		}

		// Token: 0x060021C3 RID: 8643 RVA: 0x00043AC6 File Offset: 0x00041CC6
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpClient>(this);
		}

		// Token: 0x0400205D RID: 8285
		private SmtpTalk smtpTalk;

		// Token: 0x0400205E RID: 8286
		private string serverName;

		// Token: 0x0400205F RID: 8287
		private int serverPort = 25;

		// Token: 0x04002060 RID: 8288
		private NetworkCredential authCredentials;

		// Token: 0x04002061 RID: 8289
		private string[] recips;

		// Token: 0x04002062 RID: 8290
		private string from;

		// Token: 0x04002063 RID: 8291
		private bool ndrRequired;

		// Token: 0x04002064 RID: 8292
		private MemoryStream dataStream;

		// Token: 0x04002065 RID: 8293
		private FileInfo tempfile;

		// Token: 0x04002066 RID: 8294
		private ISmtpClientDebugOutput smtpClientDebugOutput;

		// Token: 0x04002067 RID: 8295
		private IList<KeyValuePair<string, string>> fromParameters;
	}
}
