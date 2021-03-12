using System;

namespace Microsoft.Exchange.InfoWorker.Common.OOF
{
	// Token: 0x02000032 RID: 50
	internal class InvalidUserOofSettings : InvalidParameterException
	{
		// Token: 0x060000FE RID: 254 RVA: 0x00006B05 File Offset: 0x00004D05
		public InvalidUserOofSettings() : base(Strings.descInvalidUserOofSettings)
		{
		}
	}
}
