using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000224 RID: 548
	internal class PrincipalNotAllowedByPolicyExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E29 RID: 3625 RVA: 0x00045610 File Offset: 0x00043810
		public PrincipalNotAllowedByPolicyExceptionMapping() : base(typeof(PrincipalNotAllowedByPolicyException), ExchangeVersion.Exchange2010, ResponseCodeType.ErrorUserNotAllowedByPolicy, CoreResources.IDs.ErrorUserNotAllowedByPolicy)
		{
		}

		// Token: 0x06000E2A RID: 3626 RVA: 0x00045634 File Offset: 0x00043834
		protected override IDictionary<string, string> GetConstantValues(LocalizedException localizedException)
		{
			PrincipalNotAllowedByPolicyException ex = base.VerifyExceptionType<PrincipalNotAllowedByPolicyException>(localizedException);
			return new Dictionary<string, string>
			{
				{
					"UserId",
					ex.Principal.ToString()
				}
			};
		}

		// Token: 0x04000AF7 RID: 2807
		private const string UserId = "UserId";
	}
}
