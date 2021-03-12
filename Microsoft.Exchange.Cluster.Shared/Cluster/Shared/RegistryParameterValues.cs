using System;
using Microsoft.Exchange.Cluster.Common.ConfigurableParameters;
using Microsoft.Exchange.Cluster.Common.Extensions;

namespace Microsoft.Exchange.Cluster.Shared
{
	// Token: 0x02000084 RID: 132
	internal class RegistryParameterValues : ConfigurableParameterAccessorBase
	{
		// Token: 0x060004DD RID: 1245 RVA: 0x00012809 File Offset: 0x00010A09
		public RegistryParameterValues() : base(RegistryParameterDefinitions.Instance, Assert.Instance)
		{
			base.LoadInitialValues();
		}

		// Token: 0x060004DE RID: 1246 RVA: 0x00012824 File Offset: 0x00010A24
		protected override StateAccessor GetStateAccessor()
		{
			return new RegistryStateAccess("SOFTWARE\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters");
		}
	}
}
