using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000747 RID: 1863
	[Serializable]
	internal sealed class DeleteItemsException : ServicePermanentException
	{
		// Token: 0x060037F6 RID: 14326 RVA: 0x000C66C1 File Offset: 0x000C48C1
		public DeleteItemsException(Exception innerException) : base(ResponseCodeType.ErrorCannotDeleteObject, (CoreResources.IDs)3912965805U, innerException)
		{
		}

		// Token: 0x060037F7 RID: 14327 RVA: 0x000C66D6 File Offset: 0x000C48D6
		public DeleteItemsException(Enum messageId) : base(ResponseCodeType.ErrorCannotDeleteObject, messageId)
		{
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060037F8 RID: 14328 RVA: 0x000C66E1 File Offset: 0x000C48E1
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
