using System;
using Microsoft.Exchange.Diagnostics.Components.Services;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000763 RID: 1891
	internal class ExceededFindCountLimitException : ServicePermanentException
	{
		// Token: 0x06003875 RID: 14453 RVA: 0x000C7744 File Offset: 0x000C5944
		public ExceededFindCountLimitException() : base((CoreResources.IDs)2226715912U)
		{
			int findCountLimit = Global.FindCountLimit;
			ExTraceGlobals.FaultInjectionTracer.TraceTest<int>(3913690429U, ref findCountLimit);
			base.ConstantValues.Add("PolicyLimit", findCountLimit.ToString());
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003876 RID: 14454 RVA: 0x000C778F File Offset: 0x000C598F
		internal override ExchangeVersion EffectiveVersion
		{
			get
			{
				return ExchangeVersion.Exchange2010;
			}
		}

		// Token: 0x06003877 RID: 14455 RVA: 0x000C7796 File Offset: 0x000C5996
		public static void Throw()
		{
			if (ExchangeVersion.Current.Supports(ExchangeVersion.Exchange2010))
			{
				throw new ExceededFindCountLimitException();
			}
			throw new ServerBusyException();
		}

		// Token: 0x04001F54 RID: 8020
		private const string PolicyLimitKey = "PolicyLimit";
	}
}
