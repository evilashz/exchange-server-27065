using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000881 RID: 2177
	internal sealed class ServiceInvalidOperationException : ServicePermanentException
	{
		// Token: 0x06003E79 RID: 15993 RVA: 0x000D8B8B File Offset: 0x000D6D8B
		public ServiceInvalidOperationException(Enum messageId) : base(ResponseCodeType.ErrorInvalidOperation, messageId)
		{
		}

		// Token: 0x06003E7A RID: 15994 RVA: 0x000D8B99 File Offset: 0x000D6D99
		public ServiceInvalidOperationException(LocalizedString message, Exception innerException) : base(ResponseCodeType.ErrorInvalidOperation, message, innerException)
		{
		}

		// Token: 0x06003E7B RID: 15995 RVA: 0x000D8BAD File Offset: 0x000D6DAD
		public ServiceInvalidOperationException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidOperation, messageId, innerException)
		{
		}

		// Token: 0x17000F1C RID: 3868
		// (get) Token: 0x06003E7C RID: 15996 RVA: 0x000D8BBC File Offset: 0x000D6DBC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007SP1;
			}
		}
	}
}
