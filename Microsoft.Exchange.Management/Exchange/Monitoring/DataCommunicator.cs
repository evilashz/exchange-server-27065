using System;
using System.Globalization;
using System.IO;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000508 RID: 1288
	internal class DataCommunicator : IDisposable
	{
		// Token: 0x06002E28 RID: 11816 RVA: 0x000B8FD8 File Offset: 0x000B71D8
		internal DataCommunicator(string hostName, int portNumber, ProtocolConnectionType connectionMode, bool trustAnySSLCertificate)
		{
			if (string.IsNullOrEmpty(hostName))
			{
				throw new ArgumentNullException("hostName");
			}
			if (portNumber <= 0)
			{
				throw new ArgumentNullException("portNumber");
			}
			this.serverName = hostName;
			this.port = portNumber;
			this.connectionType = connectionMode;
			this.trustAnyCertificate = trustAnySSLCertificate;
		}

		// Token: 0x17000DCA RID: 3530
		// (get) Token: 0x06002E29 RID: 11817 RVA: 0x000B902A File Offset: 0x000B722A
		internal bool HasConnected
		{
			get
			{
				return this.tcpClient != null && this.tcpClient.Connected;
			}
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06002E2A RID: 11818 RVA: 0x000B9041 File Offset: 0x000B7241
		internal bool HasTimedOut
		{
			get
			{
				return this.hasTimedOut;
			}
		}

		// Token: 0x17000DCC RID: 3532
		// (get) Token: 0x06002E2B RID: 11819 RVA: 0x000B9049 File Offset: 0x000B7249
		internal Stream DataStream
		{
			get
			{
				return this.dataStream;
			}
		}

		// Token: 0x17000DCD RID: 3533
		// (get) Token: 0x06002E2C RID: 11820 RVA: 0x000B9051 File Offset: 0x000B7251
		// (set) Token: 0x06002E2D RID: 11821 RVA: 0x000B9059 File Offset: 0x000B7259
		internal DataCommunicator.ExceptionReporterDelegate ExceptionReporter
		{
			get
			{
				return this.exceptionReporter;
			}
			set
			{
				this.exceptionReporter = value;
			}
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x000B9062 File Offset: 0x000B7262
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x000B9071 File Offset: 0x000B7271
		internal static TimeoutException CreateTimeoutException()
		{
			return new TimeoutException(Strings.PopImapErrorIOTimeout(60));
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x000B9084 File Offset: 0x000B7284
		internal void ConnectAsync(AsyncCallback readCallback, DataCommunicator.GetCommandResponseDelegate responseDelegate, object callerArguments)
		{
			DataCommunicator.State state = new DataCommunicator.State();
			state.ResponseDelegate = responseDelegate;
			state.ReadDataCallback = readCallback;
			state.CallerArguments = callerArguments;
			try
			{
				this.tcpClient = new TcpClient();
			}
			catch (SocketException exception)
			{
				this.HandleException(exception);
			}
			this.StartTimer();
			this.tcpClient.BeginConnect(this.serverName, this.port, new AsyncCallback(this.ConnectAsyncCallback), state);
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x000B9100 File Offset: 0x000B7300
		internal void SendCommandAsync(string command, AsyncCallback readCallback, DataCommunicator.GetCommandResponseDelegate responseDelegate, object callerArguments)
		{
			if (this.dataStream == null)
			{
				return;
			}
			DataCommunicator.State state = new DataCommunicator.State();
			state.ReadDataCallback = readCallback;
			state.ResponseDelegate = responseDelegate;
			state.CallerArguments = callerArguments;
			state.DataStream = this.dataStream;
			try
			{
				this.StartTimer();
				byte[] bytes = Encoding.ASCII.GetBytes(command);
				this.dataStream.BeginWrite(bytes, 0, bytes.Length, new AsyncCallback(this.SendDataCallback), state);
			}
			catch (IOException exception)
			{
				this.HandleException(exception);
			}
			catch (InvalidOperationException exception2)
			{
				this.HandleException(exception2);
			}
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x000B91A0 File Offset: 0x000B73A0
		internal void Close()
		{
			this.StopTimer();
			this.CloseStream();
			if (this.tcpClient != null)
			{
				this.tcpClient.Close();
				this.tcpClient = null;
			}
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x000B91C8 File Offset: 0x000B73C8
		internal void InitializeSecureStreamAsync(DataCommunicator.GetCommandResponseDelegate responseDelegate)
		{
			if (this.dataStream == null)
			{
				return;
			}
			this.InitializeSecureStreamAsync(new DataCommunicator.State
			{
				ResponseDelegate = responseDelegate,
				DataStream = this.DataStream
			});
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x000B9200 File Offset: 0x000B7400
		internal void ReadResponseAsync(AsyncCallback readCallback, DataCommunicator.GetCommandResponseDelegate responseDelegate, object callerArguments)
		{
			if (this.dataStream == null)
			{
				return;
			}
			DataCommunicator.State state = new DataCommunicator.State();
			state.ReadDataCallback = readCallback;
			state.ResponseDelegate = responseDelegate;
			state.CallerArguments = callerArguments;
			state.DataStream = this.dataStream;
			this.StartTimer();
			this.ReadResponseAsync(state);
		}

		// Token: 0x06002E35 RID: 11829 RVA: 0x000B924A File Offset: 0x000B744A
		internal void StartTimer()
		{
			this.StopTimer();
			this.timeOutTimer = new Timer(new TimerCallback(this.TimeoutEventHandler), null, 60000, -1);
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x000B9270 File Offset: 0x000B7470
		internal void StopTimer()
		{
			if (this.timeOutTimer != null)
			{
				this.timeOutTimer.Dispose();
				this.timeOutTimer = null;
			}
		}

		// Token: 0x06002E37 RID: 11831 RVA: 0x000B928C File Offset: 0x000B748C
		internal void HandleException(Exception exception)
		{
			this.Close();
			if (this.ExceptionReporter == null)
			{
				throw new NullReferenceException("Handled exception reporter not found.", exception);
			}
			if (this.hasTimedOut)
			{
				this.exceptionReporter(DataCommunicator.CreateTimeoutException());
				return;
			}
			this.exceptionReporter(exception);
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000B92D8 File Offset: 0x000B74D8
		protected virtual void Dispose(bool disposing)
		{
			if (!this.disposed && disposing)
			{
				this.Close();
			}
			this.disposed = true;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000B92F4 File Offset: 0x000B74F4
		private void ConnectAsyncCallback(IAsyncResult asyncResult)
		{
			try
			{
				this.tcpClient.EndConnect(asyncResult);
				DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
				this.dataStream = this.tcpClient.GetStream();
				state.DataStream = this.dataStream;
				if (this.connectionType == ProtocolConnectionType.Ssl)
				{
					this.InitializeSecureStreamAsync(state);
				}
				else
				{
					this.ReadResponseAsync(state);
				}
			}
			catch (InvalidOperationException exception)
			{
				this.HandleException(exception);
			}
			catch (SocketException exception2)
			{
				this.HandleException(exception2);
			}
			catch (IOException exception3)
			{
				this.HandleException(exception3);
			}
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000B9398 File Offset: 0x000B7598
		private void SendDataCallback(IAsyncResult asyncResult)
		{
			DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
			try
			{
				state.DataStream.EndWrite(asyncResult);
				if (this.dataStream != null)
				{
					this.ReadResponseAsync(state);
				}
			}
			catch (InvalidOperationException exception)
			{
				this.HandleException(exception);
			}
			catch (IOException exception2)
			{
				this.HandleException(exception2);
			}
		}

		// Token: 0x06002E3B RID: 11835 RVA: 0x000B9400 File Offset: 0x000B7600
		private void ReadResponseAsync(DataCommunicator.State state)
		{
			try
			{
				state.BeginRead();
			}
			catch (InvalidOperationException exception)
			{
				this.HandleException(exception);
			}
			catch (IOException exception2)
			{
				this.HandleException(exception2);
			}
		}

		// Token: 0x06002E3C RID: 11836 RVA: 0x000B9448 File Offset: 0x000B7648
		private void InitializeSecureStreamAsync(DataCommunicator.State state)
		{
			if (this.dataStream == null)
			{
				return;
			}
			try
			{
				SslStream sslStream = new SslStream(this.dataStream, false, new RemoteCertificateValidationCallback(this.ValidateServerCertificate), null);
				state.DataStream = sslStream;
				this.dataStream = sslStream;
				this.StartTimer();
				sslStream.BeginAuthenticateAsClient(this.serverName, new AsyncCallback(this.InitializeSecureStreamAsyncCallback), state);
			}
			catch (InvalidOperationException exception)
			{
				this.HandleException(exception);
			}
			catch (IOException ex)
			{
				this.HandleException(new ProtocolException(Strings.ErrorAuthenticationFailed(ex.Message) + Strings.InitializeServerResponse(string.IsNullOrEmpty(state.Response) ? string.Empty : state.Response), ex));
			}
			catch (AuthenticationException innerException)
			{
				this.HandleException(new ProtocolException(Strings.ErrorAuthenticationFailed(this.certificateError) + Strings.InitializeServerResponse(string.IsNullOrEmpty(state.Response) ? string.Empty : state.Response), innerException));
			}
		}

		// Token: 0x06002E3D RID: 11837 RVA: 0x000B9570 File Offset: 0x000B7770
		private void InitializeSecureStreamAsyncCallback(IAsyncResult asyncResult)
		{
			DataCommunicator.State state = (DataCommunicator.State)asyncResult.AsyncState;
			try
			{
				SslStream sslStream = (SslStream)state.DataStream;
				sslStream.EndAuthenticateAsClient(asyncResult);
				if (this.connectionType == ProtocolConnectionType.Ssl)
				{
					state.BeginRead();
				}
				else
				{
					state.LaunchResponseDelegate();
				}
			}
			catch (IOException ex)
			{
				this.HandleException(new ProtocolException(Strings.ErrorAuthenticationFailed(ex.Message) + Strings.InitializeServerResponse(string.IsNullOrEmpty(state.Response) ? string.Empty : state.Response), ex));
			}
			catch (InvalidOperationException innerException)
			{
				this.HandleException(new ProtocolException(Strings.ErrorAuthentication + Strings.InitializeServerResponse(string.IsNullOrEmpty(state.Response) ? string.Empty : state.Response), innerException));
			}
			catch (AuthenticationException innerException2)
			{
				this.HandleException(new ProtocolException(Strings.ErrorAuthenticationFailed(this.certificateError) + Strings.InitializeServerResponse(string.IsNullOrEmpty(state.Response) ? string.Empty : state.Response), innerException2));
			}
		}

		// Token: 0x06002E3E RID: 11838 RVA: 0x000B96B8 File Offset: 0x000B78B8
		private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (sslPolicyErrors == SslPolicyErrors.None || this.trustAnyCertificate)
			{
				return true;
			}
			if (certificate != null)
			{
				this.certificateError = string.Format(CultureInfo.InvariantCulture, " {0}\r\n", new object[]
				{
					certificate.Subject
				});
			}
			this.certificateError = string.Format(CultureInfo.InvariantCulture, "Name:{0} SslPolicyErrors:{1}", new object[]
			{
				this.certificateError,
				sslPolicyErrors.ToString()
			});
			if (chain != null)
			{
				foreach (X509ChainStatus x509ChainStatus in chain.ChainStatus)
				{
					this.certificateError = string.Format(CultureInfo.InvariantCulture, "{0} {1} {2} {3} {4}", new object[]
					{
						this.certificateError,
						Environment.NewLine,
						x509ChainStatus.Status.ToString(),
						Environment.NewLine,
						x509ChainStatus.StatusInformation
					});
				}
			}
			return false;
		}

		// Token: 0x06002E3F RID: 11839 RVA: 0x000B97BA File Offset: 0x000B79BA
		private void TimeoutEventHandler(object stateInfo)
		{
			this.StopTimer();
			this.hasTimedOut = true;
			this.CloseStream();
		}

		// Token: 0x06002E40 RID: 11840 RVA: 0x000B97CF File Offset: 0x000B79CF
		private void CloseStream()
		{
			if (this.dataStream != null)
			{
				this.dataStream.Close();
				this.dataStream = null;
			}
		}

		// Token: 0x04002114 RID: 8468
		private const string EndOfLine = "\r\n";

		// Token: 0x04002115 RID: 8469
		private const int READTIMEOUT = 60000;

		// Token: 0x04002116 RID: 8470
		private TcpClient tcpClient;

		// Token: 0x04002117 RID: 8471
		private ProtocolConnectionType connectionType;

		// Token: 0x04002118 RID: 8472
		private readonly string serverName;

		// Token: 0x04002119 RID: 8473
		private readonly int port;

		// Token: 0x0400211A RID: 8474
		private readonly bool trustAnyCertificate;

		// Token: 0x0400211B RID: 8475
		private bool disposed;

		// Token: 0x0400211C RID: 8476
		private Stream dataStream;

		// Token: 0x0400211D RID: 8477
		private string certificateError;

		// Token: 0x0400211E RID: 8478
		private Timer timeOutTimer;

		// Token: 0x0400211F RID: 8479
		private bool hasTimedOut;

		// Token: 0x04002120 RID: 8480
		private DataCommunicator.ExceptionReporterDelegate exceptionReporter;

		// Token: 0x02000509 RID: 1289
		// (Invoke) Token: 0x06002E42 RID: 11842
		internal delegate void GetCommandResponseDelegate(string response, object callerArguments);

		// Token: 0x0200050A RID: 1290
		// (Invoke) Token: 0x06002E46 RID: 11846
		internal delegate void ExceptionReporterDelegate(Exception exception);

		// Token: 0x0200050B RID: 1291
		internal class State
		{
			// Token: 0x06002E49 RID: 11849 RVA: 0x000B97EB File Offset: 0x000B79EB
			internal State()
			{
				this.response = new StringBuilder(1024);
				this.buffer = new byte[1024];
			}

			// Token: 0x17000DCE RID: 3534
			// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000B9813 File Offset: 0x000B7A13
			// (set) Token: 0x06002E4B RID: 11851 RVA: 0x000B981B File Offset: 0x000B7A1B
			internal Stream DataStream
			{
				get
				{
					return this.stream;
				}
				set
				{
					this.stream = value;
				}
			}

			// Token: 0x17000DCF RID: 3535
			// (get) Token: 0x06002E4C RID: 11852 RVA: 0x000B9824 File Offset: 0x000B7A24
			// (set) Token: 0x06002E4D RID: 11853 RVA: 0x000B9831 File Offset: 0x000B7A31
			internal string Response
			{
				get
				{
					return this.response.ToString();
				}
				set
				{
					this.response = new StringBuilder(value);
				}
			}

			// Token: 0x17000DD0 RID: 3536
			// (get) Token: 0x06002E4E RID: 11854 RVA: 0x000B983F File Offset: 0x000B7A3F
			internal byte[] Buffer
			{
				get
				{
					return this.buffer;
				}
			}

			// Token: 0x17000DD1 RID: 3537
			// (get) Token: 0x06002E4F RID: 11855 RVA: 0x000B9847 File Offset: 0x000B7A47
			// (set) Token: 0x06002E50 RID: 11856 RVA: 0x000B984F File Offset: 0x000B7A4F
			internal DataCommunicator.GetCommandResponseDelegate ResponseDelegate
			{
				get
				{
					return this.responseDelegate;
				}
				set
				{
					this.responseDelegate = value;
				}
			}

			// Token: 0x17000DD2 RID: 3538
			// (get) Token: 0x06002E51 RID: 11857 RVA: 0x000B9858 File Offset: 0x000B7A58
			// (set) Token: 0x06002E52 RID: 11858 RVA: 0x000B9860 File Offset: 0x000B7A60
			internal object CallerArguments
			{
				get
				{
					return this.callerArguments;
				}
				set
				{
					this.callerArguments = value;
				}
			}

			// Token: 0x17000DD3 RID: 3539
			// (get) Token: 0x06002E53 RID: 11859 RVA: 0x000B9869 File Offset: 0x000B7A69
			// (set) Token: 0x06002E54 RID: 11860 RVA: 0x000B9871 File Offset: 0x000B7A71
			internal AsyncCallback ReadDataCallback
			{
				get
				{
					return this.readCallback;
				}
				set
				{
					this.readCallback = value;
				}
			}

			// Token: 0x06002E55 RID: 11861 RVA: 0x000B987A File Offset: 0x000B7A7A
			internal void AppendReceivedData(int length)
			{
				if (length != 0)
				{
					this.response.Append(Encoding.ASCII.GetString(this.buffer, 0, length));
				}
			}

			// Token: 0x06002E56 RID: 11862 RVA: 0x000B989D File Offset: 0x000B7A9D
			internal void LaunchResponseDelegate()
			{
				this.responseDelegate(this.response.ToString(), this.callerArguments);
			}

			// Token: 0x06002E57 RID: 11863 RVA: 0x000B98BB File Offset: 0x000B7ABB
			internal void BeginRead()
			{
				this.stream.BeginRead(this.buffer, 0, 1024, this.readCallback, this);
			}

			// Token: 0x04002121 RID: 8481
			internal const int BUFFSIZE = 1024;

			// Token: 0x04002122 RID: 8482
			private Stream stream;

			// Token: 0x04002123 RID: 8483
			private StringBuilder response;

			// Token: 0x04002124 RID: 8484
			private byte[] buffer;

			// Token: 0x04002125 RID: 8485
			private DataCommunicator.GetCommandResponseDelegate responseDelegate;

			// Token: 0x04002126 RID: 8486
			private object callerArguments;

			// Token: 0x04002127 RID: 8487
			private AsyncCallback readCallback;
		}
	}
}
