using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000733 RID: 1843
	[Serializable]
	internal sealed class DataSizeLimitException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x060037AA RID: 14250 RVA: 0x000C5BDB File Offset: 0x000C3DDB
		public DataSizeLimitException(PropertyPath propertyPath) : base((CoreResources.IDs)2935460503U, propertyPath)
		{
		}

		// Token: 0x060037AB RID: 14251 RVA: 0x000C5BEE File Offset: 0x000C3DEE
		public DataSizeLimitException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorDataSizeLimitExceeded, messageId, innerException)
		{
		}

		// Token: 0x17000D22 RID: 3362
		// (get) Token: 0x060037AC RID: 14252 RVA: 0x000C5BFA File Offset: 0x000C3DFA
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x04001EF9 RID: 7929
		internal const int DataSizeLimit = 2147483647;
	}
}
