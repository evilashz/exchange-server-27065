using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000228 RID: 552
	internal class RightsNotAllowedByPolicyExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E35 RID: 3637 RVA: 0x000457C6 File Offset: 0x000439C6
		public RightsNotAllowedByPolicyExceptionMapping() : base(typeof(RightsNotAllowedByPolicyException), ExchangeVersion.Exchange2010, ResponseCodeType.ErrorPermissionNotAllowedByPolicy, CoreResources.IDs.ErrorPermissionNotAllowedByPolicy)
		{
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000457E8 File Offset: 0x000439E8
		protected override IDictionary<string, string> GetConstantValues(LocalizedException localizedException)
		{
			RightsNotAllowedByPolicyException ex = base.VerifyExceptionType<RightsNotAllowedByPolicyException>(localizedException);
			return new Dictionary<string, string>
			{
				{
					"UserId",
					ex.RightsNotAllowedRecipients[0].Principal.ToString()
				}
			};
		}

		// Token: 0x04000AFE RID: 2814
		private const string UserId = "UserId";
	}
}
