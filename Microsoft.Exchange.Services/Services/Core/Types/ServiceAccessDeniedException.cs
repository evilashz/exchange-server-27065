using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200087D RID: 2173
	internal sealed class ServiceAccessDeniedException : ServicePermanentException
	{
		// Token: 0x06003E55 RID: 15957 RVA: 0x000D8554 File Offset: 0x000D6754
		public ServiceAccessDeniedException() : base(ResponseCodeType.ErrorAccessDenied, (CoreResources.IDs)3579904699U)
		{
		}

		// Token: 0x06003E56 RID: 15958 RVA: 0x000D8567 File Offset: 0x000D6767
		public ServiceAccessDeniedException(Enum messageId) : base(ResponseCodeType.ErrorAccessDenied, messageId)
		{
		}

		// Token: 0x06003E57 RID: 15959 RVA: 0x000D8571 File Offset: 0x000D6771
		public ServiceAccessDeniedException(Exception innerException) : base(ResponseCodeType.ErrorAccessDenied, (CoreResources.IDs)3579904699U, innerException)
		{
		}

		// Token: 0x06003E58 RID: 15960 RVA: 0x000D8585 File Offset: 0x000D6785
		public ServiceAccessDeniedException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorAccessDenied, messageId, innerException)
		{
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06003E59 RID: 15961 RVA: 0x000D8590 File Offset: 0x000D6790
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
