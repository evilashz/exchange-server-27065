using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Search.Engine;
using Microsoft.Exchange.Search.OperatorSchema;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Probes
{
	// Token: 0x02000471 RID: 1137
	public sealed class SearchNumberOfParserServersDegradationProbe : SearchProbeBase
	{
		// Token: 0x06001CBA RID: 7354 RVA: 0x000A8DD4 File Offset: 0x000A6FD4
		protected override void InternalDoWork(CancellationToken cancellationToken)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Search Foundation for Exchange\\DocParser"))
			{
				if (registryKey != null)
				{
					object value = registryKey.GetValue("MaxPoolSize", null);
					if (value != null)
					{
						int num = Convert.ToInt32(value);
						if (num != SearchConfig.Instance.SandboxMaxPoolSize)
						{
							throw new SearchProbeFailureException(Strings.SearchNumberOfParserServersDegradation(num, SearchConfig.Instance.SandboxMaxPoolSize, ((double)SearchMemoryModel.GetSearchMemoryUsage() / 1024.0 / 1024.0).ToString("0.00")));
						}
					}
				}
			}
		}

		// Token: 0x040013C6 RID: 5062
		private const string MaxPoolSizePath = "SOFTWARE\\Microsoft\\Search Foundation for Exchange\\DocParser";

		// Token: 0x040013C7 RID: 5063
		private const string MaxPoolSize = "MaxPoolSize";
	}
}
