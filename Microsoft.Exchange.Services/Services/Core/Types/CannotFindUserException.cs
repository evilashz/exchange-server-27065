using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000716 RID: 1814
	internal sealed class CannotFindUserException : ServicePermanentException
	{
		// Token: 0x06003735 RID: 14133 RVA: 0x000C5587 File Offset: 0x000C3787
		public CannotFindUserException(Enum messageId) : base(ResponseCodeType.ErrorCannotFindUser, messageId)
		{
		}

		// Token: 0x06003736 RID: 14134 RVA: 0x000C5592 File Offset: 0x000C3792
		public CannotFindUserException(Enum messageId, Exception innerException) : base(ResponseCodeType.ErrorCannotFindUser, messageId, innerException)
		{
		}

		// Token: 0x06003737 RID: 14135 RVA: 0x000C559E File Offset: 0x000C379E
		public CannotFindUserException(ResponseCodeType responseCodeType, LocalizedString message) : base(responseCodeType, message)
		{
		}

		// Token: 0x17000CEF RID: 3311
		// (get) Token: 0x06003738 RID: 14136 RVA: 0x000C55A8 File Offset: 0x000C37A8
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2013;
			}
		}
	}
}
