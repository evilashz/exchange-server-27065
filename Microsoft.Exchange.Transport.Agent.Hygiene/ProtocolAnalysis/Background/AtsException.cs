using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background
{
	// Token: 0x0200004B RID: 75
	internal class AtsException : LocalizedException
	{
		// Token: 0x06000243 RID: 579 RVA: 0x0000F5E8 File Offset: 0x0000D7E8
		public AtsException(LocalizedString errorMessage) : base(errorMessage)
		{
		}
	}
}
