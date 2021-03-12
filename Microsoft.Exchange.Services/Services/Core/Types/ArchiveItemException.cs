using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006B9 RID: 1721
	[Serializable]
	internal sealed class ArchiveItemException : ServicePermanentException
	{
		// Token: 0x060034FD RID: 13565 RVA: 0x000BF39B File Offset: 0x000BD59B
		public ArchiveItemException(Enum messageId) : base(messageId)
		{
		}

		// Token: 0x17000C2C RID: 3116
		// (get) Token: 0x060034FE RID: 13566 RVA: 0x000BF3A4 File Offset: 0x000BD5A4
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
