using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200084E RID: 2126
	[Serializable]
	internal sealed class PropertyRequestFailedException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003D50 RID: 15696 RVA: 0x000D7439 File Offset: 0x000D5639
		public PropertyRequestFailedException(Enum messageId) : base(ResponseCodeType.ErrorItemPropertyRequestFailed, messageId)
		{
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x000D7447 File Offset: 0x000D5647
		public PropertyRequestFailedException(Enum messageId, PropertyPath propertyPath, Exception innerException) : base(ResponseCodeType.ErrorItemPropertyRequestFailed, messageId, propertyPath, innerException)
		{
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000D7457 File Offset: 0x000D5657
		public PropertyRequestFailedException(Enum messageId, PropertyPath propertyPath) : base(PropertyRequestFailedException.GetResponseCodeType(messageId), messageId, propertyPath)
		{
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000D7468 File Offset: 0x000D5668
		private static ResponseCodeType GetResponseCodeType(Enum messageId)
		{
			ResponseCodeType result = ResponseCodeType.ErrorItemPropertyRequestFailed;
			CoreResources.IDs ds = (CoreResources.IDs)messageId;
			CoreResources.IDs ds2 = ds;
			if (ds2 != CoreResources.IDs.ErrorItemPropertyRequestFailed)
			{
				if (ds2 == (CoreResources.IDs)2370747299U)
				{
					result = ResponseCodeType.ErrorFolderPropertRequestFailed;
				}
			}
			else
			{
				result = ResponseCodeType.ErrorItemPropertyRequestFailed;
			}
			return result;
		}

		// Token: 0x17000EAD RID: 3757
		// (get) Token: 0x06003D54 RID: 15700 RVA: 0x000D74A3 File Offset: 0x000D56A3
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}
	}
}
