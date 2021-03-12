using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.ResourceHealth;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000226 RID: 550
	internal class ResourceUnhealthyExceptionMapping : StaticExceptionMapping
	{
		// Token: 0x06000E2D RID: 3629 RVA: 0x000456DE File Offset: 0x000438DE
		public ResourceUnhealthyExceptionMapping() : base(typeof(ResourceUnhealthyException), ExchangeVersion.Exchange2007, ResponseCodeType.ErrorServerBusy, (CoreResources.IDs)3655513582U)
		{
		}

		// Token: 0x06000E2E RID: 3630 RVA: 0x00045700 File Offset: 0x00043900
		protected override IDictionary<string, string> GetConstantValues(LocalizedException exception)
		{
			if (Global.WriteThrottlingDiagnostics)
			{
				ResourceUnhealthyException ex = base.VerifyExceptionType<ResourceUnhealthyException>(exception);
				return new Dictionary<string, string>
				{
					{
						"Resource",
						ex.ResourceKey.ToString()
					}
				};
			}
			return null;
		}

		// Token: 0x04000AFA RID: 2810
		private const string Resource = "Resource";
	}
}
