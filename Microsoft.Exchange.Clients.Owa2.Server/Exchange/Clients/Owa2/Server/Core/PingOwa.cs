using System;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000345 RID: 837
	internal class PingOwa : ServiceCommand<int>
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x00069653 File Offset: 0x00067853
		public PingOwa(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001B81 RID: 7041 RVA: 0x0006965C File Offset: 0x0006785C
		protected override int InternalExecute()
		{
			return 0;
		}
	}
}
