using System;
using System.Net;

namespace Microsoft.Exchange.Transport.Logging.MessageTracking
{
	// Token: 0x0200008C RID: 140
	internal class MsgTrackPoisonInfo
	{
		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060004B6 RID: 1206 RVA: 0x000147C0 File Offset: 0x000129C0
		// (set) Token: 0x060004B7 RID: 1207 RVA: 0x000147C8 File Offset: 0x000129C8
		internal IPAddress ClientIPAddress { get; private set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060004B8 RID: 1208 RVA: 0x000147D1 File Offset: 0x000129D1
		// (set) Token: 0x060004B9 RID: 1209 RVA: 0x000147D9 File Offset: 0x000129D9
		internal string ClientHostName { get; private set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060004BA RID: 1210 RVA: 0x000147E2 File Offset: 0x000129E2
		// (set) Token: 0x060004BB RID: 1211 RVA: 0x000147EA File Offset: 0x000129EA
		internal IPAddress ServerIPAddress { get; private set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060004BC RID: 1212 RVA: 0x000147F3 File Offset: 0x000129F3
		// (set) Token: 0x060004BD RID: 1213 RVA: 0x000147FB File Offset: 0x000129FB
		internal string SourceContext { get; private set; }

		// Token: 0x060004BE RID: 1214 RVA: 0x00014804 File Offset: 0x00012A04
		public MsgTrackPoisonInfo(IPAddress clientIPAddress, string clientHostName, IPAddress serverIPAddress, string sourceContext)
		{
			this.ClientIPAddress = clientIPAddress;
			this.ClientHostName = clientHostName;
			this.ServerIPAddress = serverIPAddress;
			this.SourceContext = sourceContext;
		}
	}
}
