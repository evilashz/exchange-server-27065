using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000760 RID: 1888
	[Serializable]
	internal sealed class EmptyFolderException : ServicePermanentException
	{
		// Token: 0x06003870 RID: 14448 RVA: 0x000C76E6 File Offset: 0x000C58E6
		public EmptyFolderException(Exception innerException) : base(ResponseCodeType.ErrorCannotEmptyFolder, (CoreResources.IDs)2838198776U, innerException)
		{
		}

		// Token: 0x06003871 RID: 14449 RVA: 0x000C76FE File Offset: 0x000C58FE
		public EmptyFolderException(Enum messageId) : base(ResponseCodeType.ErrorCannotEmptyFolder, messageId)
		{
		}

		// Token: 0x17000D5A RID: 3418
		// (get) Token: 0x06003872 RID: 14450 RVA: 0x000C770C File Offset: 0x000C590C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010SP1;
			}
		}
	}
}
