using System;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x0200018B RID: 395
	internal class NoOpOwaCallback : IOwaCallback
	{
		// Token: 0x06000E23 RID: 3619 RVA: 0x000359E0 File Offset: 0x00033BE0
		public void ProcessCallback(object owaContext)
		{
		}

		// Token: 0x04000880 RID: 2176
		public static readonly NoOpOwaCallback Prototype = new NoOpOwaCallback();
	}
}
