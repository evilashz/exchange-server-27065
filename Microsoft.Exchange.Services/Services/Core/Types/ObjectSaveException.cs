using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200082F RID: 2095
	internal sealed class ObjectSaveException : ServicePermanentException
	{
		// Token: 0x06003C73 RID: 15475 RVA: 0x000D5D3C File Offset: 0x000D3F3C
		public ObjectSaveException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorItemSave, messageId, innerException)
		{
		}

		// Token: 0x06003C74 RID: 15476 RVA: 0x000D5D4B File Offset: 0x000D3F4B
		public ObjectSaveException(Exception innerException, bool useItemError) : base(useItemError ? ((CoreResources.IDs)2339310738U) : ((CoreResources.IDs)3867216855U), innerException)
		{
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000D5D68 File Offset: 0x000D3F68
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
