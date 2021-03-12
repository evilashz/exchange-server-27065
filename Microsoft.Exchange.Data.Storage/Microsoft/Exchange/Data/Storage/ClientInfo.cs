using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001C6 RID: 454
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class ClientInfo
	{
		// Token: 0x06001866 RID: 6246 RVA: 0x00076E34 File Offset: 0x00075034
		private ClientInfo(string clientInfoStringWithoutAction)
		{
			this.clientInfoStringWithoutAction = clientInfoStringWithoutAction;
			this.clientInfoStringWithAnyAction = this.clientInfoStringWithoutAction + ";";
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x00076E59 File Offset: 0x00075059
		public bool IsMatch(string clientInfoString)
		{
			return clientInfoString.Equals(this.clientInfoStringWithoutAction, StringComparison.OrdinalIgnoreCase) || clientInfoString.StartsWith(this.clientInfoStringWithAnyAction, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000CCA RID: 3274
		public static readonly ClientInfo OWA = new ClientInfo("Client=OWA");

		// Token: 0x04000CCB RID: 3275
		public static readonly ClientInfo MOMT = new ClientInfo("Client=MSExchangeRPC");

		// Token: 0x04000CCC RID: 3276
		public static readonly ClientInfo HubTransport = new ClientInfo("Client=Hub Transport");

		// Token: 0x04000CCD RID: 3277
		private readonly string clientInfoStringWithoutAction;

		// Token: 0x04000CCE RID: 3278
		private readonly string clientInfoStringWithAnyAction;
	}
}
