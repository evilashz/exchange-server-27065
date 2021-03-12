using System;
using System.Net;
using System.Net.Sockets;
using System.Security;
using Microsoft.Exchange.Data.Directory.EventLog;

namespace Microsoft.Exchange.Data.Directory.ProvisioningCache
{
	// Token: 0x020007A7 RID: 1959
	internal class InvalidationRecvActivity : Activity
	{
		// Token: 0x06006157 RID: 24919 RVA: 0x0014B5F0 File Offset: 0x001497F0
		public InvalidationRecvActivity(ProvisioningCache cache, uint recvPort) : base(cache)
		{
			if (!CacheBroadcaster.IsIPv6Only())
			{
				this.msgReceiveSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
				this.msgReceiveEndPoint = new IPEndPoint(IPAddress.Any, (int)recvPort);
			}
			else
			{
				this.msgReceiveSocket = new Socket(AddressFamily.InterNetworkV6, SocketType.Dgram, ProtocolType.Udp);
				this.msgReceiveEndPoint = new IPEndPoint(IPAddress.IPv6Any, (int)recvPort);
				this.msgReceiveSocket.SetSocketOption(SocketOptionLevel.IPv6, SocketOptionName.AddMembership, new IPv6MulticastOption(IPAddress.Parse("ff02::1")));
			}
			this.msgReceiveSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
			this.msgReceiveSocket.Bind(this.msgReceiveEndPoint);
			this.recvBuffer = new byte[5000];
		}

		// Token: 0x170022CB RID: 8907
		// (get) Token: 0x06006158 RID: 24920 RVA: 0x0014B69F File Offset: 0x0014989F
		public override string Name
		{
			get
			{
				return "Invalidation message receiver";
			}
		}

		// Token: 0x06006159 RID: 24921 RVA: 0x0014B6A6 File Offset: 0x001498A6
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				this.msgReceiveSocket.Close();
			}
		}

		// Token: 0x0600615A RID: 24922 RVA: 0x0014B6B8 File Offset: 0x001498B8
		protected override void InternalExecute()
		{
			EndPoint endPoint = this.msgReceiveEndPoint;
			Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCStartingToReceiveInvalidationMessage, this.msgReceiveEndPoint.Address.ToString(), new object[]
			{
				this.msgReceiveEndPoint.Port
			});
			while (!base.GotStopSignalFromTestCode)
			{
				try
				{
					Array.Clear(this.recvBuffer, 0, this.recvBuffer.Length);
					int bufLen = this.msgReceiveSocket.ReceiveFrom(this.recvBuffer, ref endPoint);
					Exception ex = null;
					InvalidationMessage invalidationMessage = InvalidationMessage.TryFromReceivedData(this.recvBuffer, bufLen, out ex);
					if (ex != null)
					{
						Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCInvalidInvalidationMessageReceived, this.msgReceiveEndPoint.Address.ToString(), new object[]
						{
							this.msgReceiveEndPoint.Port,
							ex.Message
						});
					}
					else
					{
						if (invalidationMessage.IsCacheClearMessage)
						{
							base.ProvisioningCache.Reset();
						}
						else if (invalidationMessage.IsGlobal)
						{
							base.ProvisioningCache.RemoveGlobalDatas(invalidationMessage.CacheKeys);
						}
						else
						{
							base.ProvisioningCache.RemoveOrganizationDatas(invalidationMessage.OrganizationId, invalidationMessage.CacheKeys);
						}
						ProvisioningCache.IncrementReceivedInvalidationMsgNum();
					}
				}
				catch (SocketException ex2)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveInvalidationMessage, this.msgReceiveEndPoint.Address.ToString(), new object[]
					{
						this.msgReceiveEndPoint.Port,
						ex2.Message
					});
				}
				catch (ObjectDisposedException ex3)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveInvalidationMessage, this.msgReceiveEndPoint.Address.ToString(), new object[]
					{
						this.msgReceiveEndPoint.Port,
						ex3.Message
					});
					throw ex3;
				}
				catch (SecurityException ex4)
				{
					Globals.LogEvent(DirectoryEventLogConstants.Tuple_PCFailedToReceiveInvalidationMessage, this.msgReceiveEndPoint.Address.ToString(), new object[]
					{
						this.msgReceiveEndPoint.Port,
						ex4.Message
					});
					throw ex4;
				}
			}
		}

		// Token: 0x0600615B RID: 24923 RVA: 0x0014B8F0 File Offset: 0x00149AF0
		internal override void StopExecute()
		{
			base.StopExecute();
			if (base.GotStopSignalFromTestCode)
			{
				CacheBroadcaster cacheBroadcaster = new CacheBroadcaster(9050U);
				cacheBroadcaster.BroadcastInvalidationMessage(null, new Guid[]
				{
					Guid.NewGuid()
				});
				base.AsyncThread.Join();
			}
		}

		// Token: 0x0400415B RID: 16731
		private Socket msgReceiveSocket;

		// Token: 0x0400415C RID: 16732
		private IPEndPoint msgReceiveEndPoint;

		// Token: 0x0400415D RID: 16733
		private byte[] recvBuffer;
	}
}
