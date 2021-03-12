using System;
using System.Net;
using System.Net.Sockets;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x02000057 RID: 87
	internal class CSocks5Proxy : TransportConnection, IDataConnection
	{
		// Token: 0x0600029D RID: 669 RVA: 0x00011650 File Offset: 0x0000F850
		private static CSocks5Proxy.Socks5ResponseMethod ConvertSocks5ResponseCodeToMethod(byte response)
		{
			CSocks5Proxy.Socks5ResponseMethod result = CSocks5Proxy.Socks5ResponseMethod.NoAcceptableMethod;
			switch (response)
			{
			case 0:
				result = CSocks5Proxy.Socks5ResponseMethod.NoAuthenticationRequired;
				break;
			case 1:
				result = CSocks5Proxy.Socks5ResponseMethod.GSSAPIAuthentication;
				break;
			case 2:
				result = CSocks5Proxy.Socks5ResponseMethod.UserNamePasswordAuth;
				break;
			case 3:
				result = CSocks5Proxy.Socks5ResponseMethod.IANAAssigned;
				break;
			case 4:
				result = CSocks5Proxy.Socks5ResponseMethod.PrivateMethod;
				break;
			}
			return result;
		}

		// Token: 0x0600029E RID: 670 RVA: 0x00011690 File Offset: 0x0000F890
		public CSocks5Proxy(ProxyChain proxyChain) : base(proxyChain)
		{
		}

		// Token: 0x0600029F RID: 671 RVA: 0x000116A0 File Offset: 0x0000F8A0
		private bool UpdateUsernamePassword(string username, string password)
		{
			int num = 0;
			if (username.Length >= 255)
			{
				return false;
			}
			if (password.Length >= 255)
			{
				return false;
			}
			if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
			{
				this.usernamePasswordSize = 3 + username.Length + password.Length;
				this.usernamePasswordRequest = new byte[this.usernamePasswordSize];
				this.usernamePasswordRequest[num++] = 1;
				this.usernamePasswordRequest[num++] = (byte)(username.Length & 255);
				for (int i = 0; i < username.Length; i++)
				{
					this.usernamePasswordRequest[num++] = (byte)username[i];
				}
				this.usernamePasswordRequest[num++] = (byte)(password.Length & 255);
				for (int i = 0; i < password.Length; i++)
				{
					this.usernamePasswordRequest[num++] = (byte)password[i];
				}
			}
			else
			{
				this.usernamePasswordSize = 0;
				this.usernamePasswordRequest = null;
			}
			return true;
		}

		// Token: 0x060002A0 RID: 672 RVA: 0x000117A4 File Offset: 0x0000F9A4
		public override void AsyncConnect(IPEndPoint remoteEndpoint, TcpConnection tcpCxn, NetworkCredential authInfo)
		{
			if (!this.UpdateUsernamePassword(authInfo.UserName, authInfo.Password))
			{
				base.ProxyChain.OnDisconnected();
				return;
			}
			this.tcpCxn = tcpCxn;
			this.state = CSocks5Proxy.Socks5ProxyState.NegotiationSent;
			try
			{
				if (this.usernamePasswordRequest == null)
				{
					this.versionMethodRequestSize--;
				}
				this.versionMethodRequest = new byte[this.versionMethodRequestSize];
				this.versionMethodRequest[0] = 5;
				if (this.usernamePasswordRequest == null)
				{
					this.versionMethodRequest[1] = 1;
					this.versionMethodRequest[2] = 0;
				}
				else
				{
					this.versionMethodRequest[1] = 2;
					this.versionMethodRequest[2] = 0;
					this.versionMethodRequest[3] = 3;
				}
				this.connectRequest = new byte[24];
				this.connectRequest[0] = 5;
				this.connectRequest[1] = 1;
				this.connectRequest[2] = 0;
				this.connectRequest[3] = 1;
				int num = (remoteEndpoint.Address.AddressFamily == AddressFamily.InterNetworkV6) ? 16 : 4;
				byte[] addressBytes = remoteEndpoint.Address.GetAddressBytes();
				Array.Copy(addressBytes, 0, this.connectRequest, 4, num);
				this.connectRequest[4 + num] = (byte)(remoteEndpoint.Port >> 8 & 255);
				this.connectRequest[5 + num] = (byte)(remoteEndpoint.Port & 255);
				this.tcpCxn.SendMessage(this.versionMethodRequest, 0, this.versionMethodRequestSize);
			}
			catch (AtsException)
			{
				this.state = CSocks5Proxy.Socks5ProxyState.Finished;
				base.ProxyChain.OnDisconnected();
			}
		}

		// Token: 0x060002A1 RID: 673 RVA: 0x00011924 File Offset: 0x0000FB24
		public int OnDataReceived(byte[] dataReceived, int offset, int length)
		{
			int num = 0;
			try
			{
				while (length - num > 0)
				{
					switch (this.state)
					{
					case CSocks5Proxy.Socks5ProxyState.NegotiationSent:
						if (length - num < 2)
						{
							return num;
						}
						switch (CSocks5Proxy.ConvertSocks5ResponseCodeToMethod(dataReceived[offset + num + 1]))
						{
						case CSocks5Proxy.Socks5ResponseMethod.NoAuthenticationRequired:
							this.state = CSocks5Proxy.Socks5ProxyState.ConnectSent;
							this.tcpCxn.SendMessage(this.connectRequest, 0, 24);
							break;
						case CSocks5Proxy.Socks5ResponseMethod.GSSAPIAuthentication:
							goto IL_A2;
						case CSocks5Proxy.Socks5ResponseMethod.UserNamePasswordAuth:
							if (this.usernamePasswordRequest == null)
							{
								goto IL_A2;
							}
							this.state = CSocks5Proxy.Socks5ProxyState.UserPwdSent;
							this.tcpCxn.SendMessage(this.usernamePasswordRequest, 0, this.usernamePasswordSize);
							break;
						default:
							goto IL_A2;
						}
						IL_B4:
						num += 2;
						break;
						IL_A2:
						this.state = CSocks5Proxy.Socks5ProxyState.Finished;
						base.ProxyChain.OnDisconnected();
						goto IL_B4;
					case CSocks5Proxy.Socks5ProxyState.UserPwdSent:
						if (length - num < 2)
						{
							return num;
						}
						num += 2;
						if (dataReceived[offset + num + 1] == 0)
						{
							this.state = CSocks5Proxy.Socks5ProxyState.ConnectSent;
							this.tcpCxn.SendMessage(this.connectRequest, 0, 24);
						}
						else
						{
							this.state = CSocks5Proxy.Socks5ProxyState.Finished;
							base.ProxyChain.OnDisconnected();
						}
						break;
					case CSocks5Proxy.Socks5ProxyState.ConnectSent:
					{
						if (length - num < 6)
						{
							return num;
						}
						byte b = dataReceived[offset + num + 1];
						if (b != 0)
						{
							base.ProxyChain.OnDisconnected();
							return num;
						}
						switch (dataReceived[offset + num + 3])
						{
						case 1:
							this.connectResponseSize = 10;
							break;
						case 2:
							goto IL_134;
						case 3:
							this.connectResponseSize = (int)(7 + dataReceived[offset + num + 4]);
							break;
						case 4:
							this.connectResponseSize = 22;
							break;
						default:
							goto IL_134;
						}
						if (length - num < this.connectResponseSize)
						{
							return num;
						}
						num += this.connectResponseSize;
						this.state = CSocks5Proxy.Socks5ProxyState.Finished;
						num += base.ProxyChain.OnConnected(dataReceived, offset + num, length - num);
						break;
						IL_134:
						base.ProxyChain.OnDisconnected();
						return num;
					}
					default:
						return num;
					}
				}
			}
			catch (AtsException)
			{
				this.state = CSocks5Proxy.Socks5ProxyState.Finished;
				base.ProxyChain.OnDisconnected();
			}
			return num;
		}

		// Token: 0x040001F0 RID: 496
		private const int MaxUsernameLength = 255;

		// Token: 0x040001F1 RID: 497
		private const int MaxPasswordLength = 255;

		// Token: 0x040001F2 RID: 498
		private const int IPv4AddressOctets = 4;

		// Token: 0x040001F3 RID: 499
		private const int IPv6AddressOctets = 16;

		// Token: 0x040001F4 RID: 500
		private const byte Socks5RequestGrantCode = 0;

		// Token: 0x040001F5 RID: 501
		private const byte Socks5UserPwdAuthSuccessCode = 0;

		// Token: 0x040001F6 RID: 502
		private const byte IPv4AddressType = 1;

		// Token: 0x040001F7 RID: 503
		private const byte HostnameAddressType = 3;

		// Token: 0x040001F8 RID: 504
		private const byte IPv6AddressType = 4;

		// Token: 0x040001F9 RID: 505
		private const int VersionMethodResponseSize = 2;

		// Token: 0x040001FA RID: 506
		private const int UsernamePasswordResponseSize = 2;

		// Token: 0x040001FB RID: 507
		private const int ConnectRequestSize = 24;

		// Token: 0x040001FC RID: 508
		private const int MinconnectResponseSize = 6;

		// Token: 0x040001FD RID: 509
		private int versionMethodRequestSize = 4;

		// Token: 0x040001FE RID: 510
		private int connectResponseSize;

		// Token: 0x040001FF RID: 511
		private byte[] versionMethodRequest;

		// Token: 0x04000200 RID: 512
		private byte[] connectRequest;

		// Token: 0x04000201 RID: 513
		private TcpConnection tcpCxn;

		// Token: 0x04000202 RID: 514
		private CSocks5Proxy.Socks5ProxyState state;

		// Token: 0x04000203 RID: 515
		private byte[] usernamePasswordRequest;

		// Token: 0x04000204 RID: 516
		private int usernamePasswordSize;

		// Token: 0x02000058 RID: 88
		private enum Socks5ProxyState
		{
			// Token: 0x04000206 RID: 518
			Invalid,
			// Token: 0x04000207 RID: 519
			NegotiationSent,
			// Token: 0x04000208 RID: 520
			UserPwdSent,
			// Token: 0x04000209 RID: 521
			ConnectSent,
			// Token: 0x0400020A RID: 522
			Finished
		}

		// Token: 0x02000059 RID: 89
		private enum Socks5ResponseMethod : byte
		{
			// Token: 0x0400020C RID: 524
			NoAuthenticationRequired,
			// Token: 0x0400020D RID: 525
			GSSAPIAuthentication,
			// Token: 0x0400020E RID: 526
			UserNamePasswordAuth,
			// Token: 0x0400020F RID: 527
			IANAAssigned,
			// Token: 0x04000210 RID: 528
			PrivateMethod,
			// Token: 0x04000211 RID: 529
			NoAcceptableMethod
		}
	}
}
