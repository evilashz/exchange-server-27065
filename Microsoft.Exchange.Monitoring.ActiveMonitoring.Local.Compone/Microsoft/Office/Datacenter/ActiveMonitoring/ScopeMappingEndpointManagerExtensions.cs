using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x02000563 RID: 1379
	internal static class ScopeMappingEndpointManagerExtensions
	{
		// Token: 0x060022A2 RID: 8866 RVA: 0x000D19CE File Offset: 0x000CFBCE
		internal static ScopeMappingEndpoint GetEndpoint(this ScopeMappingEndpointManager endpointManager)
		{
			return LocalEndpointManager.Instance.ScopeMappingLocalEndpoint;
		}
	}
}
