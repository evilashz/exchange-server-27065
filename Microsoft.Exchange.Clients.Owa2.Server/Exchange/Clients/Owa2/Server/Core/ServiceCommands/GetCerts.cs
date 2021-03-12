using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core.ServiceCommands
{
	// Token: 0x0200036B RID: 875
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetCerts : ServiceCommand<GetCertsResponse>
	{
		// Token: 0x06001C0E RID: 7182 RVA: 0x0006DF50 File Offset: 0x0006C150
		public GetCerts(CallContext callContext, GetCertsRequest request) : base(callContext)
		{
			this.getEncryptionCertsCommand = new GetEncryptionCerts(callContext, request);
		}

		// Token: 0x06001C0F RID: 7183 RVA: 0x0006DF68 File Offset: 0x0006C168
		protected override GetCertsResponse InternalExecute()
		{
			GetCertsResponse getCertsResponse = this.getEncryptionCertsCommand.Execute();
			List<string> list = new List<string>();
			foreach (string[] array in getCertsResponse.ValidRecipients)
			{
				if (array != null && array.Length > 0)
				{
					list.AddRange(array);
				}
			}
			getCertsResponse.ValidRecipients = null;
			getCertsResponse.CertsRawData = list.ToArray();
			return getCertsResponse;
		}

		// Token: 0x04000FE4 RID: 4068
		private readonly GetEncryptionCerts getEncryptionCertsCommand;
	}
}
