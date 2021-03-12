using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007DB RID: 2011
	internal sealed class InvalidValueForPropertyException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003B2B RID: 15147 RVA: 0x000CFD7B File Offset: 0x000CDF7B
		public InvalidValueForPropertyException(Enum messageId) : base(ResponseCodeType.ErrorInvalidValueForProperty, messageId)
		{
		}

		// Token: 0x06003B2C RID: 15148 RVA: 0x000CFD89 File Offset: 0x000CDF89
		public InvalidValueForPropertyException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidValueForProperty, messageId, innerException)
		{
		}

		// Token: 0x06003B2D RID: 15149 RVA: 0x000CFD98 File Offset: 0x000CDF98
		public InvalidValueForPropertyException(PropertyPath propertyPath, Exception innerException) : base(CoreResources.IDs.ErrorInvalidValueForProperty, propertyPath, innerException)
		{
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003B2E RID: 15150 RVA: 0x000CFDAC File Offset: 0x000CDFAC
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2007;
			}
		}

		// Token: 0x06003B2F RID: 15151 RVA: 0x000CFDB4 File Offset: 0x000CDFB4
		internal static InvalidValueForPropertyException CreateDuplicateDictionaryKeyError(Enum messageId, string dictionaryKey, Exception innerException)
		{
			return new InvalidValueForPropertyException(messageId, innerException)
			{
				ConstantValues = 
				{
					{
						"DictionaryKey",
						dictionaryKey
					}
				}
			};
		}

		// Token: 0x06003B30 RID: 15152 RVA: 0x000CFDDC File Offset: 0x000CDFDC
		internal static InvalidValueForPropertyException CreateConversionError(Enum messageId, string valueToConvert, string convertToType, Exception innerException)
		{
			return new InvalidValueForPropertyException(messageId, innerException)
			{
				ConstantValues = 
				{
					{
						"ValueToConvert",
						valueToConvert
					},
					{
						"ConvertToType",
						convertToType
					}
				}
			};
		}
	}
}
