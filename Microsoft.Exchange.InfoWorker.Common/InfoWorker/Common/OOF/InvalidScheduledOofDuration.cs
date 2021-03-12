using System;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000031 RID: 49
	internal class InvalidScheduledOofDuration : InvalidParameterException
	{
		// Token: 0x060000FD RID: 253 RVA: 0x00006AF8 File Offset: 0x00004CF8
		public InvalidScheduledOofDuration() : base(Strings.descInvalidScheduledOofDuration)
		{
		}
	}
}
