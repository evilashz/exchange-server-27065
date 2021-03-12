using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007AF RID: 1967
	[Serializable]
	internal sealed class InvalidFolderTypeForOperationException : ServicePermanentException
	{
		// Token: 0x06003AAD RID: 15021 RVA: 0x000CF3EA File Offset: 0x000CD5EA
		public InvalidFolderTypeForOperationException(Enum messageId) : base(ResponseCodeType.ErrorInvalidFolderTypeForOperation, messageId)
		{
		}

		// Token: 0x06003AAE RID: 15022 RVA: 0x000CF3F8 File Offset: 0x000CD5F8
		public InvalidFolderTypeForOperationException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidFolderTypeForOperation, messageId, innerException)
		{
		}

		// Token: 0x17000DCE RID: 3534
		// (get) Token: 0x06003AAF RID: 15023 RVA: 0x000CF407 File Offset: 0x000CD607
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
