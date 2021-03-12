using System;
using System.Text;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.PopImap.Core;

namespace Microsoft.Exchange.Pop3
{
	// Token: 0x02000020 RID: 32
	internal class Pop3ProxySession : ProxySession
	{
		// Token: 0x06000100 RID: 256 RVA: 0x0000653E File Offset: 0x0000473E
		public Pop3ProxySession(ResponseFactory responseFactory, NetworkConnection networkConnection) : base(responseFactory, networkConnection)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00006548 File Offset: 0x00004748
		public override string GetUserConfigurationName()
		{
			return "POP3.UserConfiguration";
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00006550 File Offset: 0x00004750
		protected override bool ForwardToClient(byte[] buf, int offset, int size)
		{
			if (!base.IncomingSession.ProxyToLegacyServer)
			{
				return true;
			}
			if (size > Pop3ProxySession.E2k3QuitResponse.Length && BaseSession.CompareArg(Pop3ProxySession.E2k3QuitResponse, buf, offset, Pop3ProxySession.E2k3QuitResponse.Length))
			{
				base.IncomingSession.SendToClient(new BufferResponseItem(Pop3ProxySession.QuitResponse, 0, Pop3ProxySession.QuitResponse.Length));
				return false;
			}
			return true;
		}

		// Token: 0x06000103 RID: 259 RVA: 0x000065AC File Offset: 0x000047AC
		protected override void BeginRead(NetworkConnection networkConnection)
		{
			if (networkConnection == null)
			{
				return;
			}
			if (base.IncomingSession.ProxyToLegacyServer || base.State != ProxySession.ProxyState.completed)
			{
				networkConnection.BeginReadLine(new AsyncCallback(this.ReadLineCompleteCallback), networkConnection);
				return;
			}
			networkConnection.BeginRead(new AsyncCallback(this.ReadCompleteCallback), networkConnection);
		}

		// Token: 0x040000A1 RID: 161
		internal static readonly byte[] E2k3QuitResponse = Encoding.ASCII.GetBytes("+ok microsoft exchange server 2003 pop3 ");

		// Token: 0x040000A2 RID: 162
		internal static readonly byte[] QuitResponse = Encoding.ASCII.GetBytes("+OK" + " " + "Microsoft Exchange Server 2013 POP3 server signing off.\r\n");
	}
}
