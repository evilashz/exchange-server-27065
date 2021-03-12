using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078F RID: 1935
	[Serializable]
	internal sealed class FolderSaveException : ServicePermanentException
	{
		// Token: 0x060039AD RID: 14765 RVA: 0x000CB829 File Offset: 0x000C9A29
		public FolderSaveException() : base(CoreResources.IDs.ErrorFolderSaveFailed)
		{
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x060039AE RID: 14766 RVA: 0x000CB83B File Offset: 0x000C9A3B
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
