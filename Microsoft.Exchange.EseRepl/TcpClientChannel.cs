using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.EseRepl
{
	// Token: 0x0200003B RID: 59
	internal class TcpClientChannel : TcpChannel
	{
		// Token: 0x06000207 RID: 519 RVA: 0x00007F41 File Offset: 0x00006141
		protected TcpClientChannel(string serverNodeName, Socket channel, NegotiateStream s, int timeout) : base(channel, s, timeout)
		{
			base.PartnerNodeName = serverNodeName;
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00007F54 File Offset: 0x00006154
		internal static TcpClientChannel OpenChannel(NetworkPath netPath, int timeoutInMs)
		{
			NetworkTransportException ex = null;
			TcpClientChannel result = null;
			if (TcpClientChannel.TryOpenChannel(netPath, timeoutInMs, out result, out ex))
			{
				return result;
			}
			throw ex;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x00007F78 File Offset: 0x00006178
		internal static bool TryOpenChannel(NetworkPath netPath, int timeoutInMs, out TcpClientChannel channel, out NetworkTransportException networkEx)
		{
			channel = null;
			networkEx = null;
			Exception ex = null;
			Socket socket = null;
			Stream stream = null;
			NegotiateStream negotiateStream = null;
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				socket = new Socket(netPath.TargetEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				if (netPath.Purpose == NetworkPath.ConnectionPurpose.Seeding)
				{
					socket.ReceiveBufferSize = Parameters.CurrentValues.SeedingNetworkTransferSize;
					socket.SendBufferSize = Parameters.CurrentValues.SeedingNetworkTransferSize;
				}
				else
				{
					socket.ReceiveBufferSize = Parameters.CurrentValues.LogCopyNetworkTransferSize;
					socket.SendBufferSize = Parameters.CurrentValues.LogCopyNetworkTransferSize;
				}
				if (netPath.HasSourceEndpoint())
				{
					socket.Bind(netPath.SourceEndPoint);
				}
				TcpClientChannel.ConnectAbandon connectAbandon = new TcpClientChannel.ConnectAbandon(socket);
				IAsyncResult asyncResult = socket.BeginConnect(netPath.TargetEndPoint.Address, netPath.TargetEndPoint.Port, null, connectAbandon);
				if (!asyncResult.AsyncWaitHandle.WaitOne(timeoutInMs, false))
				{
					socket = null;
					connectAbandon.Cancel(asyncResult);
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, Strings.NetworkConnectionTimeout(timeoutInMs / 1000));
				}
				socket.EndConnect(asyncResult);
				long elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				ExTraceGlobals.TcpClientTracer.TraceDebug<long>(0L, "Connection took {0}ms", elapsedMilliseconds);
				socket.LingerState = new LingerOption(true, 0);
				if (!netPath.UseSocketStream || Parameters.CurrentValues.DisableSocketStream)
				{
					stream = new NetworkStream(socket, false);
				}
				else
				{
					stream = new SocketStream(socket, netPath.SocketStreamBufferPool, netPath.SocketStreamAsyncArgPool, netPath.SocketStreamPerfCounters);
				}
				negotiateStream = new NegotiateStream(stream, false);
				stream = null;
				elapsedMilliseconds = stopwatch.ElapsedMilliseconds;
				if (elapsedMilliseconds >= (long)timeoutInMs)
				{
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, Strings.NetworkConnectionTimeout(timeoutInMs / 1000));
				}
				int num = timeoutInMs - (int)elapsedMilliseconds;
				negotiateStream.WriteTimeout = num;
				negotiateStream.ReadTimeout = num;
				TcpClientChannel.AuthAbandon authAbandon = new TcpClientChannel.AuthAbandon(socket, negotiateStream);
				string targetName;
				if (netPath.UseNullSpn)
				{
					targetName = "";
				}
				else
				{
					targetName = "HOST/" + netPath.TargetNodeName;
				}
				bool encrypt = netPath.Encrypt;
				ProtectionLevel protectionLevel;
				if (encrypt)
				{
					protectionLevel = ProtectionLevel.EncryptAndSign;
				}
				else if (Parameters.CurrentValues.DisableNetworkSigning)
				{
					protectionLevel = ProtectionLevel.None;
				}
				else
				{
					protectionLevel = ProtectionLevel.Sign;
				}
				asyncResult = negotiateStream.BeginAuthenticateAsClient(CredentialCache.DefaultNetworkCredentials, targetName, protectionLevel, TokenImpersonationLevel.Identification, null, authAbandon);
				if (!asyncResult.AsyncWaitHandle.WaitOne(num, false))
				{
					negotiateStream = null;
					socket = null;
					authAbandon.Abandon(asyncResult);
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, Strings.NetworkConnectionTimeout(timeoutInMs / 1000));
				}
				negotiateStream.EndAuthenticateAsClient(asyncResult);
				bool flag = false;
				if (!negotiateStream.IsAuthenticated)
				{
					flag = true;
				}
				else if (protectionLevel != ProtectionLevel.None && !negotiateStream.IsMutuallyAuthenticated)
				{
					if (netPath.IgnoreMutualAuth || MachineName.Comparer.Equals(netPath.TargetNodeName, Environment.MachineName))
					{
						ExTraceGlobals.TcpClientTracer.TraceDebug(0L, "Ignoring mutual auth since we are local");
					}
					else
					{
						flag = true;
					}
				}
				if (!flag && encrypt && !negotiateStream.IsEncrypted)
				{
					ExTraceGlobals.TcpClientTracer.TraceError(0L, "Encryption requested, but could not be negotiated");
					flag = true;
				}
				if (flag)
				{
					ExTraceGlobals.TcpClientTracer.TraceError<bool, bool, bool>(0L, "Security Negotiation failed. Auth={0},MAuth={1},Encrypt={2}", negotiateStream.IsAuthenticated, negotiateStream.IsMutuallyAuthenticated, negotiateStream.IsEncrypted);
					throw new NetworkCommunicationException(netPath.TargetNodeName, Strings.NetworkSecurityFailed);
				}
				ExTraceGlobals.TcpClientTracer.TraceDebug<long, bool, ProtectionLevel>(0L, "Authenticated Connection took {0}ms. Encrypt={1} ProtRequested={2}", stopwatch.ElapsedMilliseconds, negotiateStream.IsEncrypted, protectionLevel);
				channel = new TcpClientChannel(netPath.TargetNodeName, socket, negotiateStream, timeoutInMs);
				return true;
			}
			catch (SocketException ex2)
			{
				ex = ex2;
			}
			catch (IOException ex3)
			{
				ex = ex3;
			}
			catch (AuthenticationException ex4)
			{
				ex = ex4;
			}
			catch (NetworkTransportException ex5)
			{
				ex = ex5;
			}
			finally
			{
				if (channel == null)
				{
					if (negotiateStream != null)
					{
						negotiateStream.Dispose();
					}
					else if (stream != null)
					{
						stream.Dispose();
					}
					if (socket != null)
					{
						socket.Close();
					}
				}
				else
				{
					ReplayCrimsonEvents.NetworkConnectionSuccess.Log<string, IPEndPoint, IPEndPoint>(netPath.TargetNodeName, netPath.TargetEndPoint, channel.LocalEndpoint);
				}
			}
			ExTraceGlobals.TcpClientTracer.TraceError<Exception>(0L, "TryOpenChannel failed. Ex={0}", ex);
			ReplayCrimsonEvents.NetworkConnectionFailure.Log<string, IPEndPoint, IPEndPoint, string>(netPath.TargetNodeName, netPath.TargetEndPoint, netPath.SourceEndPoint, ex.ToString());
			if (ex is NetworkTransportException)
			{
				networkEx = (NetworkTransportException)ex;
			}
			else
			{
				networkEx = new NetworkCommunicationException(netPath.TargetNodeName, ex.Message, ex);
			}
			return false;
		}

		// Token: 0x0200003C RID: 60
		public class ConnectAbandon : AbandonAsyncBase
		{
			// Token: 0x0600020A RID: 522 RVA: 0x000083F8 File Offset: 0x000065F8
			public ConnectAbandon(Socket socket)
			{
				this.m_connection = socket;
			}

			// Token: 0x0600020B RID: 523 RVA: 0x00008408 File Offset: 0x00006608
			protected override void EndProcessing(IAsyncResult ar)
			{
				try
				{
					this.m_connection.EndConnect(ar);
				}
				finally
				{
					this.m_connection.Close();
				}
			}

			// Token: 0x0600020C RID: 524 RVA: 0x00008440 File Offset: 0x00006640
			public void Cancel(IAsyncResult ar)
			{
				base.Abandon(ar);
			}

			// Token: 0x04000133 RID: 307
			private Socket m_connection;
		}

		// Token: 0x0200003D RID: 61
		public class AuthAbandon : AbandonAsyncBase
		{
			// Token: 0x0600020D RID: 525 RVA: 0x00008449 File Offset: 0x00006649
			public AuthAbandon(Socket socket, NegotiateStream stream)
			{
				this.m_connection = socket;
				this.m_authStream = stream;
			}

			// Token: 0x0600020E RID: 526 RVA: 0x00008460 File Offset: 0x00006660
			protected override void EndProcessing(IAsyncResult ar)
			{
				try
				{
					this.m_authStream.EndAuthenticateAsClient(ar);
				}
				finally
				{
					this.m_authStream.Close();
					this.m_connection.Close();
				}
			}

			// Token: 0x04000134 RID: 308
			private Socket m_connection;

			// Token: 0x04000135 RID: 309
			private NegotiateStream m_authStream;
		}
	}
}
