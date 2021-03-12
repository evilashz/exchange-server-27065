using System;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200003E RID: 62
	internal class ApnsTcpClient : IDisposable
	{
		// Token: 0x06000259 RID: 601 RVA: 0x00008A94 File Offset: 0x00006C94
		public ApnsTcpClient(TcpClient tcpClient)
		{
			if (tcpClient == null)
			{
				throw new ArgumentNullException("tcpClient");
			}
			this.TcpClient = tcpClient;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00008AB1 File Offset: 0x00006CB1
		public virtual bool Connected
		{
			get
			{
				return this.TcpClient.Connected;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00008ABE File Offset: 0x00006CBE
		// (set) Token: 0x0600025C RID: 604 RVA: 0x00008AC6 File Offset: 0x00006CC6
		private TcpClient TcpClient { get; set; }

		// Token: 0x0600025D RID: 605 RVA: 0x00008ACF File Offset: 0x00006CCF
		public virtual IAsyncResult BeginConnect(string host, int port, AsyncCallback requestCallback, object state)
		{
			return this.TcpClient.BeginConnect(host, port, requestCallback, state);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x00008AE4 File Offset: 0x00006CE4
		public virtual void EndConnect(IAsyncResult asyncResult)
		{
			this.TcpClient.EndConnect(asyncResult);
			this.TcpClient.SendBufferSize = 320;
			this.TcpClient.ReceiveBufferSize = 128;
			this.TcpClient.Client.IOControl((IOControlCode)((ulong)-1744830460), ApnsTcpClient.KeepAliveValues, null);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00008B3A File Offset: 0x00006D3A
		public virtual void Connect(string hostname, int port)
		{
			this.TcpClient.Connect(hostname, port);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00008B49 File Offset: 0x00006D49
		public virtual NetworkStream GetStream()
		{
			return this.TcpClient.GetStream();
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00008B56 File Offset: 0x00006D56
		public virtual void Dispose()
		{
			this.TcpClient.Close();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00008B64 File Offset: 0x00006D64
		private static byte[] CreateKeepAliveValues()
		{
			uint num = 0U;
			int num2 = Marshal.SizeOf(num);
			byte[] array = new byte[num2 * 3];
			BitConverter.GetBytes(1U).CopyTo(array, 0);
			BitConverter.GetBytes(240000U).CopyTo(array, num2);
			BitConverter.GetBytes(1000U).CopyTo(array, num2 * 2);
			return array;
		}

		// Token: 0x040000F7 RID: 247
		private const int SendBufferSize = 320;

		// Token: 0x040000F8 RID: 248
		private const int ReceiveBufferSize = 128;

		// Token: 0x040000F9 RID: 249
		private static readonly byte[] KeepAliveValues = ApnsTcpClient.CreateKeepAliveValues();
	}
}
