using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020007BB RID: 1979
	internal sealed class InvalidManagementRoleHeaderException : ServicePermanentException
	{
		// Token: 0x06003ACB RID: 15051 RVA: 0x000CF79B File Offset: 0x000CD99B
		public InvalidManagementRoleHeaderException() : base((CoreResources.IDs)2674011741U)
		{
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000CF7AD File Offset: 0x000CD9AD
		public InvalidManagementRoleHeaderException(Enum messageId) : base(ResponseCodeType.ErrorInvalidManagementRoleHeader, messageId)
		{
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000CF7BB File Offset: 0x000CD9BB
		public InvalidManagementRoleHeaderException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorInvalidManagementRoleHeader, messageId, innerException)
		{
		}

		// Token: 0x17000DDA RID: 3546
		// (get) Token: 0x06003ACE RID: 15054 RVA: 0x000CF7CA File Offset: 0x000CD9CA
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2012;
			}
		}
	}
}
