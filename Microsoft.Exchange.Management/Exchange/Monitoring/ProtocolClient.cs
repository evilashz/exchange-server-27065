using System;
using System.IO;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200050C RID: 1292
	internal abstract class ProtocolClient : IDisposable
	{
		// Token: 0x06002E58 RID: 11864 RVA: 0x000B98DC File Offset: 0x000B7ADC
		internal ProtocolClient(string hostName, int portNumber, ProtocolConnectionType connectionMode, bool trustAnySSLCertificate)
		{
			this.server = hostName;
			this.connectionType = connectionMode;
			this.trustAnyCertificate = trustAnySSLCertificate;
			this.communicator = new DataCommunicator(hostName, portNumber, connectionMode, trustAnySSLCertificate);
		}

		// Token: 0x17000DD4 RID: 3540
		// (get) Token: 0x06002E59 RID: 11865 RVA: 0x000B990A File Offset: 0x000B7B0A
		internal bool TrustAnySSLCertificate
		{
			get
			{
				return this.trustAnyCertificate;
			}
		}

		// Token: 0x17000DD5 RID: 3541
		// (get) Token: 0x06002E5A RID: 11866 RVA: 0x000B9912 File Offset: 0x000B7B12
		internal bool HasConnected
		{
			get
			{
				return this.communicator != null && this.communicator.HasConnected;
			}
		}

		// Token: 0x17000DD6 RID: 3542
		// (get) Token: 0x06002E5B RID: 11867 RVA: 0x000B9929 File Offset: 0x000B7B29
		// (set) Token: 0x06002E5C RID: 11868 RVA: 0x000B9931 File Offset: 0x000B7B31
		internal bool HasLoggedIn
		{
			get
			{
				return this.loggedIn;
			}
			set
			{
				this.loggedIn = value;
			}
		}

		// Token: 0x17000DD7 RID: 3543
		// (get) Token: 0x06002E5D RID: 11869 RVA: 0x000B993A File Offset: 0x000B7B3A
		internal ProtocolConnectionType ConnectionType
		{
			get
			{
				return this.connectionType;
			}
		}

		// Token: 0x17000DD8 RID: 3544
		// (get) Token: 0x06002E5E RID: 11870 RVA: 0x000B9942 File Offset: 0x000B7B42
		internal string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06002E5F RID: 11871 RVA: 0x000B994A File Offset: 0x000B7B4A
		// (set) Token: 0x06002E60 RID: 11872 RVA: 0x000B9957 File Offset: 0x000B7B57
		internal DataCommunicator.ExceptionReporterDelegate ExceptionReporter
		{
			get
			{
				return this.communicator.ExceptionReporter;
			}
			set
			{
				this.communicator.ExceptionReporter = value;
			}
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06002E61 RID: 11873 RVA: 0x000B9965 File Offset: 0x000B7B65
		protected DataCommunicator Communicator
		{
			get
			{
				return this.communicator;
			}
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x000B996D File Offset: 0x000B7B6D
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E63 RID: 11875 RVA: 0x000B997C File Offset: 0x000B7B7C
		internal static string GetSubjectOfMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentNullException("message");
			}
			string result = string.Empty;
			if (message.IndexOf("\r\nSubject:", StringComparison.OrdinalIgnoreCase) > -1)
			{
				message = message.Remove(0, message.IndexOf("\r\nSubject:", StringComparison.OrdinalIgnoreCase) + 10);
				message = message.Remove(message.IndexOf('\r'), message.Length - message.IndexOf('\r'));
				result = message;
			}
			return result;
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x000B99EC File Offset: 0x000B7BEC
		internal static string GetDateOfMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				throw new ArgumentNullException("message");
			}
			string result = string.Empty;
			if (message.IndexOf("\r\nDate:", StringComparison.OrdinalIgnoreCase) > -1)
			{
				message = message.Remove(0, message.IndexOf("\r\nDate:", StringComparison.OrdinalIgnoreCase) + 7);
				message = message.Remove(message.IndexOf('\r'), message.Length - message.IndexOf('\r'));
				result = message;
			}
			return result;
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000B9A5A File Offset: 0x000B7C5A
		internal virtual void Connect(DataCommunicator.GetCommandResponseDelegate responseDelegate)
		{
			this.communicator.ConnectAsync(new AsyncCallback(this.SingleLineResponseCallback), responseDelegate, null);
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x000B9A75 File Offset: 0x000B7C75
		internal void SetUpSecureStreamForTls(DataCommunicator.GetCommandResponseDelegate callback)
		{
			this.Communicator.InitializeSecureStreamAsync(callback);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x000B9A83 File Offset: 0x000B7C83
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.loggedIn = false;
				if (this.communicator != null)
				{
					this.communicator.Close();
					this.communicator.Dispose();
					this.communicator = null;
				}
				this.disposed = true;
			}
		}

		// Token: 0x06002E68 RID: 11880 RVA: 0x000B9AC4 File Offset: 0x000B7CC4
		protected void SingleLineResponseCallback(IAsyncResult asyncResult)
		{
			DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
			try
			{
				int num = state.DataStream.EndRead(asyncResult);
				if (num > 0)
				{
					state.AppendReceivedData(num);
					if (!state.Response.EndsWith("\r\n", StringComparison.Ordinal))
					{
						state.BeginRead();
					}
				}
				this.Communicator.StopTimer();
				state.LaunchResponseDelegate();
			}
			catch (InvalidOperationException exception)
			{
				if (this.Communicator.HasTimedOut)
				{
					this.Communicator.HandleException(DataCommunicator.CreateTimeoutException());
				}
				else
				{
					this.Communicator.HandleException(exception);
				}
			}
			catch (IOException exception2)
			{
				this.Communicator.HandleException(exception2);
			}
		}

		// Token: 0x04002128 RID: 8488
		protected const string EndOfLine = "\r\n";

		// Token: 0x04002129 RID: 8489
		private const string DateClause = "\r\nDate:";

		// Token: 0x0400212A RID: 8490
		private const string SubjectClause = "\r\nSubject:";

		// Token: 0x0400212B RID: 8491
		private const char RChar = '\r';

		// Token: 0x0400212C RID: 8492
		private DataCommunicator communicator;

		// Token: 0x0400212D RID: 8493
		private readonly string server;

		// Token: 0x0400212E RID: 8494
		private readonly bool trustAnyCertificate;

		// Token: 0x0400212F RID: 8495
		private ProtocolConnectionType connectionType;

		// Token: 0x04002130 RID: 8496
		private bool disposed;

		// Token: 0x04002131 RID: 8497
		private bool loggedIn;
	}
}
