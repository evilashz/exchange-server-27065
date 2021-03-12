using System;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Principal;
using Microsoft.Exchange.Cluster.Common;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.EseRepl;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200026B RID: 619
	internal class TcpClientChannel : TcpChannel
	{
		// Token: 0x0600184B RID: 6219 RVA: 0x00063EDD File Offset: 0x000620DD
		protected TcpClientChannel(string serverNodeName, Socket channel, NegotiateStream s, int timeout) : base(channel, s, timeout, TimeSpan.FromSeconds((double)RegistryParameters.TcpChannelIdleLimitInSec))
		{
			base.PartnerNodeName = serverNodeName;
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x00063EFC File Offset: 0x000620FC
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

		// Token: 0x0600184D RID: 6221 RVA: 0x00063F20 File Offset: 0x00062120
		internal static bool TryOpenChannel(NetworkPath netPath, int timeoutInMs, out TcpClientChannel channel, out NetworkTransportException networkEx)
		{
			channel = null;
			networkEx = null;
			Exception ex = null;
			Socket socket = null;
			Stream stream = null;
			NegotiateStream negotiateStream = null;
			ReplayStopwatch replayStopwatch = new ReplayStopwatch();
			replayStopwatch.Start();
			try
			{
				socket = new Socket(netPath.TargetEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				if (netPath.Purpose == NetworkPath.ConnectionPurpose.Seeding)
				{
					socket.ReceiveBufferSize = RegistryParameters.SeedingNetworkTransferSize;
					socket.SendBufferSize = RegistryParameters.SeedingNetworkTransferSize;
				}
				else
				{
					socket.ReceiveBufferSize = RegistryParameters.LogCopyNetworkTransferSize;
					socket.SendBufferSize = RegistryParameters.LogCopyNetworkTransferSize;
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
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, ReplayStrings.NetworkConnectionTimeout(timeoutInMs / 1000));
				}
				socket.EndConnect(asyncResult);
				long elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
				ExTraceGlobals.TcpClientTracer.TraceDebug<long>(0L, "Connection took {0}ms", elapsedMilliseconds);
				socket.LingerState = new LingerOption(true, 0);
				if (!netPath.UseSocketStream || RegistryParameters.DisableSocketStream)
				{
					stream = new NetworkStream(socket, false);
				}
				else
				{
					stream = new SocketStream(socket, netPath.SocketStreamBufferPool, netPath.SocketStreamAsyncArgPool, netPath.SocketStreamPerfCounters);
				}
				negotiateStream = new NegotiateStream(stream, false);
				stream = null;
				elapsedMilliseconds = replayStopwatch.ElapsedMilliseconds;
				if (elapsedMilliseconds >= (long)timeoutInMs)
				{
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, ReplayStrings.NetworkConnectionTimeout(timeoutInMs / 1000));
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
				else if (RegistryParameters.DisableNetworkSigning)
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
					TcpChannel.ThrowTimeoutException(netPath.TargetNodeName, ReplayStrings.NetworkConnectionTimeout(timeoutInMs / 1000));
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
					NetworkManager.ThrowException(new NetworkCommunicationException(netPath.TargetNodeName, ReplayStrings.NetworkSecurityFailed));
				}
				ExTraceGlobals.TcpClientTracer.TraceDebug<long, bool, ProtectionLevel>(0L, "Authenticated Connection took {0}ms. Encrypt={1} ProtRequested={2}", replayStopwatch.ElapsedMilliseconds, negotiateStream.IsEncrypted, protectionLevel);
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

		// Token: 0x0200026C RID: 620
		public class ConnectAbandon : AbandonAsyncBase
		{
			// Token: 0x0600184E RID: 6222 RVA: 0x00064384 File Offset: 0x00062584
			public ConnectAbandon(Socket socket)
			{
				this.m_connection = socket;
			}

			// Token: 0x0600184F RID: 6223 RVA: 0x00064394 File Offset: 0x00062594
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

			// Token: 0x06001850 RID: 6224 RVA: 0x000643CC File Offset: 0x000625CC
			public void Cancel(IAsyncResult ar)
			{
				base.Abandon(ar);
			}

			// Token: 0x040009AC RID: 2476
			private Socket m_connection;
		}

		// Token: 0x0200026D RID: 621
		public class AuthAbandon : AbandonAsyncBase
		{
			// Token: 0x06001851 RID: 6225 RVA: 0x000643D5 File Offset: 0x000625D5
			public AuthAbandon(Socket socket, NegotiateStream stream)
			{
				this.m_connection = socket;
				this.m_authStream = stream;
			}

			// Token: 0x06001852 RID: 6226 RVA: 0x000643EC File Offset: 0x000625EC
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

			// Token: 0x040009AD RID: 2477
			private Socket m_connection;

			// Token: 0x040009AE RID: 2478
			private NegotiateStream m_authStream;
		}
	}
}
