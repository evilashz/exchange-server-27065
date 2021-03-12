using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BA RID: 1978
	internal class InvalidMailboxException : ServicePermanentExceptionWithPropertyPath
	{
		// Token: 0x06003AC6 RID: 15046 RVA: 0x000CF67C File Offset: 0x000CD87C
		public InvalidMailboxException(int memberIndex, PropertyPath propertyPath, LocalizedException innerException, Enum messageId) : base(ResponseCodeType.ErrorInvalidMailbox, messageId, propertyPath, innerException)
		{
			base.ConstantValues.Add("MemberIndex", memberIndex.ToString());
			ServiceError serviceError = ServiceErrors.GetServiceError(innerException);
			base.ConstantValues.Add("Inner.ResponseCode", serviceError.MessageKey.ToString());
			base.ConstantValues.Add("Inner.MessageText", serviceError.MessageText);
			base.ConstantValues.Add("Inner.DescriptiveLinkKey", serviceError.DescriptiveLinkId.ToString());
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000CF707 File Offset: 0x000CD907
		public InvalidMailboxException(PropertyPath propertyPath, string property, string actualValue) : base(CoreResources.IDs.ErrorInvalidMailbox, propertyPath)
		{
			base.ConstantValues.Add("Property", property);
			base.ConstantValues.Add("Value", actualValue);
		}

		// Token: 0x06003AC8 RID: 15048 RVA: 0x000CF73C File Offset: 0x000CD93C
		public InvalidMailboxException(PropertyPath propertyPath, string property, string expectedValue, string actualValue, Enum messageId) : base(ResponseCodeType.ErrorInvalidMailbox, messageId, propertyPath)
		{
			base.ConstantValues.Add("Property", property);
			base.ConstantValues.Add("Value", actualValue);
			base.ConstantValues.Add("ExpectedValue", expectedValue);
		}

		// Token: 0x06003AC9 RID: 15049 RVA: 0x000CF788 File Offset: 0x000CD988
		public InvalidMailboxException(PropertyPath propertyPath, Enum messageId) : base(ResponseCodeType.ErrorInvalidMailbox, messageId, propertyPath)
		{
		}

		// Token: 0x17000DD9 RID: 3545
		// (get) Token: 0x06003ACA RID: 15050 RVA: 0x000CF794 File Offset: 0x000CD994
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}
	}
}
