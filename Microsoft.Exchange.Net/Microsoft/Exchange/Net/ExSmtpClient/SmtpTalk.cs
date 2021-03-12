using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Net;

namespace Microsoft.Exchange.Net.ExSmtpClient
{
	// Token: 0x0200070D RID: 1805
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SmtpTalk : DisposeTrackableBase
	{
		// Token: 0x060021F2 RID: 8690 RVA: 0x000455C4 File Offset: 0x000437C4
		internal SmtpTalk(ISmtpClientDebugOutput smtpClientDebugOutput)
		{
			this.smtpClientDebugOutput = smtpClientDebugOutput;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x000455FF File Offset: 0x000437FF
		// (set) Token: 0x060021F4 RID: 8692 RVA: 0x00045607 File Offset: 0x00043807
		private Stream TcpStream
		{
			get
			{
				return this.tcpStream;
			}
			set
			{
				this.tcpStream = value;
			}
		}

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060021F5 RID: 8693 RVA: 0x00045610 File Offset: 0x00043810
		// (set) Token: 0x060021F6 RID: 8694 RVA: 0x00045618 File Offset: 0x00043818
		private TcpClient TcpClient
		{
			get
			{
				return this.tcpClient;
			}
			set
			{
				this.tcpClient = value;
			}
		}

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060021F7 RID: 8695 RVA: 0x00045621 File Offset: 0x00043821
		private bool Connected
		{
			get
			{
				return this.tcpClient != null && this.TcpClient.Connected;
			}
		}

		// Token: 0x170008C7 RID: 2247
		// (get) Token: 0x060021F8 RID: 8696 RVA: 0x00045638 File Offset: 0x00043838
		public string EhloResponseText
		{
			get
			{
				return this.ehloResponseText;
			}
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00045640 File Offset: 0x00043840
		internal void Connect(string server, int port)
		{
			base.CheckDisposed();
			if (this.Connected)
			{
				throw new AlreadyConnectedToSMTPServerException(this.serverName);
			}
			this.clientFqdn = ComputerInformation.DnsPhysicalFullyQualifiedDomainName;
			this.serverName = server;
			IPHostEntry hostEntry = Dns.GetHostEntry(server);
			IPAddress[] addressList = hostEntry.AddressList;
			IPAddress[] array = addressList;
			int i = 0;
			while (i < array.Length)
			{
				IPAddress ipaddress = array[i];
				IPEndPoint remoteEP = new IPEndPoint(ipaddress, port);
				this.TcpClient = new TcpClient();
				this.TcpClient.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
				this.TcpClient.SendTimeout = SmtpConstants.NetworkingTimeout;
				this.TcpClient.ReceiveTimeout = SmtpConstants.NetworkingTimeout;
				try
				{
					this.TcpClient.Connect(remoteEP);
				}
				catch (SocketException)
				{
					this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Could not connect to {0} at {1}::{2}, will try other IP bindings", new object[]
					{
						server,
						ipaddress.ToString(),
						port.ToString(CultureInfo.InvariantCulture)
					});
					goto IL_184;
				}
				goto IL_F2;
				IL_184:
				i++;
				continue;
				IL_F2:
				this.TcpStream = this.TcpClient.GetStream();
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), string.Concat(new string[]
				{
					"Connected to ",
					server,
					" at ",
					ipaddress.ToString(),
					"::",
					port.ToString(CultureInfo.InvariantCulture)
				}), new object[0]);
				SmtpTalk.ServerResponseInfo serverResponse = this.GetServerResponse();
				this.CheckResponse(serverResponse, 220);
				return;
			}
			throw new FailedToConnectToSMTPServerException(server);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x000457FC File Offset: 0x000439FC
		internal void Ehlo()
		{
			base.CheckDisposed();
			SmtpTalk.ServerResponseInfo serverResponseInfo = this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.EhloCommand + this.clientFqdn), SmtpCommandType.Ehlo, 250);
			this.ehloResponseText = serverResponseInfo.StatusString;
			this.advertisedFqdn = serverResponseInfo.StatusString.Substring(4).Split(new char[]
			{
				' '
			})[0];
			if (!SmtpClientDomainUtility.IsValidDomain(this.advertisedFqdn))
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "The SMTP server returned '{0}' as FQDN. This value is invalid so bailing out.", new object[]
				{
					this.advertisedFqdn
				});
				throw new AuthFailureException();
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000458A8 File Offset: 0x00043AA8
		internal void StartTls(bool useAnonymousTls = true)
		{
			base.CheckDisposed();
			if (useAnonymousTls)
			{
				this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.XAnonymousTlsCommand), SmtpCommandType.XAnonymousTls, 220);
			}
			else
			{
				this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.StartTlsCommand), SmtpCommandType.STARTTLS, 220);
			}
			SmtpSslStream smtpSslStream = new SmtpSslStream(this.TcpClient.GetStream(), this.smtpClientDebugOutput);
			smtpSslStream.Handshake();
			this.TcpStream = smtpSslStream;
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00045914 File Offset: 0x00043B14
		internal void Authenticate(NetworkCredential networkCredential, SmtpSspiMechanism authType = SmtpSspiMechanism.Kerberos)
		{
			base.CheckDisposed();
			SmtpSslStream smtpSslStream = this.TcpStream as SmtpSslStream;
			if (smtpSslStream == null)
			{
				throw new MustBeTlsForAuthException();
			}
			using (SmtpAuth smtpAuth = new SmtpAuth(this.smtpClientDebugOutput))
			{
				if (authType != SmtpSspiMechanism.Login)
				{
					if (authType != SmtpSspiMechanism.Kerberos)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), authType.ToString() + " is not an expected SMTP authentication mechanism", new object[0]);
						throw new UnsupportedAuthMechanismException(authType.ToString());
					}
					string targetSPN = string.Format(CultureInfo.InvariantCulture, SmtpConstants.TargetSpn, new object[]
					{
						this.advertisedFqdn
					});
					string text = smtpAuth.HandleOutboundAuth(null, targetSPN, smtpSslStream.CertificatePublicKey, smtpSslStream.SessionKey, true);
					SmtpTalk.ServerResponseInfo serverResponseInfo = this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.AuthCommand + text), SmtpCommandType.Custom, 235);
					string text2 = ((string)serverResponseInfo.StatusString.Split(new char[]
					{
						' '
					}).GetValue(1)).Trim();
					smtpAuth.HandleOutboundAuth(text2, targetSPN, smtpSslStream.CertificatePublicKey, smtpSslStream.SessionKey, false);
				}
				else
				{
					string text = smtpAuth.GetInitialBlob(networkCredential, authType);
					SmtpTalk.ServerResponseInfo serverResponseInfo = this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.AuthLoginCommand + text), SmtpCommandType.Custom, 334);
					string text2 = ((string)serverResponseInfo.StatusString.Split(new char[]
					{
						' '
					}).GetValue(1)).Trim();
					text = smtpAuth.GetNextBlob(Encoding.Default.GetBytes(text2), networkCredential, SmtpSspiMechanism.Login);
					this.Command(SmtpUtils.ProperlyTerminatedCommand(text), SmtpCommandType.Custom, 235);
				}
			}
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00045AEC File Offset: 0x00043CEC
		internal void MailFrom(string sender, IEnumerable<KeyValuePair<string, string>> parameters = null)
		{
			base.CheckDisposed();
			StringBuilder stringBuilder = new StringBuilder(SmtpConstants.MailFromCommand + sender);
			if (parameters != null)
			{
				foreach (KeyValuePair<string, string> keyValuePair in parameters)
				{
					stringBuilder.AppendFormat(" {0}={1}", keyValuePair.Key, SmtpUtils.ToXtextString(keyValuePair.Value, false));
				}
			}
			string protocolText = SmtpUtils.ProperlyTerminatedCommand(stringBuilder.ToString());
			this.Command(protocolText, SmtpCommandType.Mail, 250);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00045B84 File Offset: 0x00043D84
		internal void RcptTo(string recipient, bool? ndrForFailure = null)
		{
			base.CheckDisposed();
			string str = string.Empty;
			if (ndrForFailure != null)
			{
				str = (ndrForFailure.Value ? SmtpConstants.NDRForFailure : SmtpConstants.NoNDR);
			}
			this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.RcptToCommand + recipient + str), SmtpCommandType.Recipient, 250);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00045BDC File Offset: 0x00043DDC
		internal void Chunking(MemoryStream stream)
		{
			base.CheckDisposed();
			string s = string.Format(CultureInfo.InvariantCulture, SmtpConstants.BdatCommand, new object[]
			{
				stream.Length
			});
			SmtpChunk[] array = new SmtpChunk[2];
			array[0].Data = Encoding.Default.GetBytes(s);
			array[0].Length = Encoding.Default.GetByteCount(s);
			array[1].Data = stream.GetBuffer();
			array[1].Length = (int)stream.Length;
			this.Command(array, SmtpCommandType.BDAT, 250);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00045C7D File Offset: 0x00043E7D
		internal void Quit()
		{
			base.CheckDisposed();
			this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.QuitCommand), SmtpCommandType.Quit, 221);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00045C9C File Offset: 0x00043E9C
		internal void DisableDelayedAck()
		{
			base.CheckDisposed();
			this.Command(SmtpUtils.ProperlyTerminatedCommand(SmtpConstants.XSHADOW + this.clientFqdn), SmtpCommandType.XSHADOW, 250);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00045CC7 File Offset: 0x00043EC7
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.Connected)
			{
				this.TcpStream.Flush();
				this.TcpStream.Dispose();
				this.TcpStream = null;
				this.TcpClient.Close();
				this.TcpClient = null;
			}
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00045D03 File Offset: 0x00043F03
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<SmtpTalk>(this);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00045D0B File Offset: 0x00043F0B
		private void Send(byte[] data, int length)
		{
			this.TcpStream.Write(data, 0, length);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00045D1C File Offset: 0x00043F1C
		private SmtpTalk.ServerResponseInfo GetServerResponse()
		{
			string currentResponse = string.Empty;
			SmtpTalk.ServerResponseInfo serverResponseInfo;
			do
			{
				serverResponseInfo = this.Receive(currentResponse);
				this.ProcessServerResponse(serverResponseInfo);
				currentResponse = serverResponseInfo.StatusString;
			}
			while (!serverResponseInfo.CommandResponseCompleted);
			return serverResponseInfo;
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00045D50 File Offset: 0x00043F50
		private SmtpTalk.ServerResponseInfo Receive(string currentResponse)
		{
			byte[] array = new byte[SmtpConstants.BufferSize];
			StringBuilder stringBuilder = new StringBuilder(currentResponse);
			int count = this.TcpStream.Read(array, 0, array.Length);
			stringBuilder.Append(Encoding.Default.GetString(array, 0, count));
			SmtpTalk.ServerResponseInfo serverResponseInfo = new SmtpTalk.ServerResponseInfo();
			serverResponseInfo.StatusStream = new MemoryStream();
			serverResponseInfo.StatusStream.Write(Encoding.Default.GetBytes(stringBuilder.ToString()), 0, stringBuilder.Length);
			return serverResponseInfo;
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00045DC8 File Offset: 0x00043FC8
		private void ProcessServerResponse(SmtpTalk.ServerResponseInfo response)
		{
			string @string = Encoding.Default.GetString(response.StatusStream.ToArray());
			if (@string.Length == 0)
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Received 0 bytes as a response => the connection was closed.", new object[0]);
				response.CommandResponseCompleted = true;
				return;
			}
			if (@string.Length < 5)
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "The SMTP server's response (" + @string + ") is too short to be a complete line.  Will read more from the server.", new object[0]);
				response.CommandResponseCompleted = false;
				return;
			}
			response.Status = SmtpUtils.StatusFromResponseString(@string);
			bool flag = @string.Length >= 4 && @string[3] == '-';
			int num = @string.LastIndexOf(SmtpConstants.CrLf, StringComparison.Ordinal);
			if (num != @string.Length - SmtpConstants.CrLf.Length)
			{
				response.CommandResponseCompleted = false;
				return;
			}
			if (flag)
			{
				int i = 0;
				while (i < @string.Length)
				{
					int num2 = @string.IndexOf(SmtpConstants.CrLf, i, StringComparison.Ordinal) - i + SmtpConstants.CrLf.Length;
					if (num2 < 5)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "This line of the SMTP server's multiline response (" + @string.Substring(i, num2) + ") is too short to be a complete line.  Will read more from the server.", new object[0]);
						response.CommandResponseCompleted = false;
						return;
					}
					int num3 = SmtpUtils.StatusFromResponseString(@string.Substring(i));
					if (response.Status != num3)
					{
						this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), string.Concat(new string[]
						{
							"This line of the SMTP server's multiline response (",
							@string.Substring(i, num2),
							") does not begin with the same three-digit status code as the first line(",
							response.Status.ToString(CultureInfo.InvariantCulture),
							"), in violation of the SMTP protocol."
						}), new object[0]);
						throw new InvalidSmtpServerResponseException(@string);
					}
					char c = @string[i + 3];
					bool flag2;
					if (c != ' ')
					{
						if (c != '-')
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "The charcter after the status code (" + @string[i + 3].ToString() + ") is neither a ' ' or a '-', in violation of the SMTP protocol.", new object[0]);
							throw new InvalidSmtpServerResponseException(@string);
						}
						flag2 = false;
					}
					else
					{
						flag2 = true;
					}
					i += num2;
					if (-1 == @string.IndexOf(SmtpConstants.CrLf, i, StringComparison.Ordinal))
					{
						if (flag2)
						{
							response.CommandResponseCompleted = true;
						}
						else
						{
							this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "The last line of the multiline response does not contain a ' ' after the status code. The multiline response must not be complete, will read more from the server.", new object[0]);
							response.CommandResponseCompleted = false;
						}
					}
				}
				return;
			}
			response.CommandResponseCompleted = true;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x00046080 File Offset: 0x00044280
		private void CheckResponse(SmtpTalk.ServerResponseInfo response, int expectedCode)
		{
			if (response.Status != expectedCode)
			{
				this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), string.Format(CultureInfo.InvariantCulture, "The SMTP server returned '{0}' instead of the expected '{1}' status", new object[]
				{
					response.Status,
					expectedCode
				}), new object[0]);
				throw new UnexpectedSmtpServerResponseException(expectedCode, response.Status, response.StatusString);
			}
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x000460F8 File Offset: 0x000442F8
		private SmtpTalk.ServerResponseInfo Command(string protocolText, SmtpCommandType command, int expectedCode)
		{
			SmtpChunk[] array = new SmtpChunk[1];
			array[0].Data = Encoding.Default.GetBytes(protocolText);
			array[0].Length = Encoding.Default.GetByteCount(protocolText);
			return this.Command(array, command, expectedCode);
		}

		// Token: 0x0600220A RID: 8714 RVA: 0x00046144 File Offset: 0x00044344
		private SmtpTalk.ServerResponseInfo Command(SmtpChunk[] chunks, SmtpCommandType command, int expectedCode)
		{
			this.smtpClientDebugOutput.Output(ExTraceGlobals.SmtpClientTracer, this.GetHashCode(), "Processing command: {0}", new object[]
			{
				command
			});
			if (!this.Connected)
			{
				throw new NotConnectedToSMTPServerException(this.serverName);
			}
			foreach (SmtpChunk smtpChunk in chunks)
			{
				this.Send(smtpChunk.Data, smtpChunk.Length);
			}
			SmtpTalk.ServerResponseInfo serverResponse = this.GetServerResponse();
			this.CheckResponse(serverResponse, expectedCode);
			return serverResponse;
		}

		// Token: 0x040020A8 RID: 8360
		private string clientFqdn = string.Empty;

		// Token: 0x040020A9 RID: 8361
		private string advertisedFqdn = string.Empty;

		// Token: 0x040020AA RID: 8362
		private string serverName = string.Empty;

		// Token: 0x040020AB RID: 8363
		private TcpClient tcpClient;

		// Token: 0x040020AC RID: 8364
		private Stream tcpStream;

		// Token: 0x040020AD RID: 8365
		private ISmtpClientDebugOutput smtpClientDebugOutput;

		// Token: 0x040020AE RID: 8366
		private string ehloResponseText = string.Empty;

		// Token: 0x0200070E RID: 1806
		[Serializable]
		internal class ServerResponseInfo
		{
			// Token: 0x170008C8 RID: 2248
			// (get) Token: 0x0600220B RID: 8715 RVA: 0x000461DC File Offset: 0x000443DC
			// (set) Token: 0x0600220C RID: 8716 RVA: 0x000461E4 File Offset: 0x000443E4
			internal int Status
			{
				get
				{
					return this.status;
				}
				set
				{
					this.status = value;
				}
			}

			// Token: 0x170008C9 RID: 2249
			// (get) Token: 0x0600220D RID: 8717 RVA: 0x000461ED File Offset: 0x000443ED
			// (set) Token: 0x0600220E RID: 8718 RVA: 0x000461F5 File Offset: 0x000443F5
			internal bool CommandResponseCompleted
			{
				get
				{
					return this.commandResponseCompleted;
				}
				set
				{
					this.commandResponseCompleted = value;
				}
			}

			// Token: 0x170008CA RID: 2250
			// (get) Token: 0x0600220F RID: 8719 RVA: 0x000461FE File Offset: 0x000443FE
			// (set) Token: 0x06002210 RID: 8720 RVA: 0x00046206 File Offset: 0x00044406
			internal MemoryStream StatusStream
			{
				get
				{
					return this.statusStream;
				}
				set
				{
					this.statusStream = value;
				}
			}

			// Token: 0x170008CB RID: 2251
			// (get) Token: 0x06002211 RID: 8721 RVA: 0x0004620F File Offset: 0x0004440F
			internal string StatusString
			{
				get
				{
					return Encoding.Default.GetString(this.statusStream.ToArray());
				}
			}

			// Token: 0x040020AF RID: 8367
			private MemoryStream statusStream = new MemoryStream();

			// Token: 0x040020B0 RID: 8368
			private int status = -1;

			// Token: 0x040020B1 RID: 8369
			private bool commandResponseCompleted;
		}
	}
}
