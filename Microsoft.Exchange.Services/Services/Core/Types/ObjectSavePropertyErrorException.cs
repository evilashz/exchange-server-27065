using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000830 RID: 2096
	internal sealed class ObjectSavePropertyErrorException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003C76 RID: 15478 RVA: 0x000D5D6F File Offset: 0x000D3F6F
		public ObjectSavePropertyErrorException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorItemSavePropertyError, messageId, innerException)
		{
		}

		// Token: 0x06003C77 RID: 15479 RVA: 0x000D5D7E File Offset: 0x000D3F7E
		public ObjectSavePropertyErrorException(PropertyPath[] properties, Exception innerException, bool useItemError) : base(useItemError ? CoreResources.IDs.ErrorItemSavePropertyError : CoreResources.IDs.ErrorFolderSavePropertyError, properties, innerException)
		{
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06003C78 RID: 15480 RVA: 0x000D5D9C File Offset: 0x000D3F9C
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
