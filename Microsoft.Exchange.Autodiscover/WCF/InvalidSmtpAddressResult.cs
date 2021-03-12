using System;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Autodiscover.WCF
{
	// Token: 0x02000081 RID: 129
	internal sealed class InvalidSmtpAddressResult : ResultBase
	{
		// Token: 0x06000369 RID: 873 RVA: 0x00015A24 File Offset: 0x00013C24
		internal InvalidSmtpAddressResult(UserResultMapping userResultMapping) : base(userResultMapping)
		{
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00015A2D File Offset: 0x00013C2D
		internal override UserResponse CreateResponse(IBudget budget)
		{
			return base.CreateInvalidUserResponse();
		}
	}
}
