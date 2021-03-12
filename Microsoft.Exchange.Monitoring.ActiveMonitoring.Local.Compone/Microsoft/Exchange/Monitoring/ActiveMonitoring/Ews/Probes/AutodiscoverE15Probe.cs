using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Ews.Probes
{
	// Token: 0x0200016E RID: 366
	public class AutodiscoverE15Probe : AutodiscoverCommon
	{
		// Token: 0x06000A9B RID: 2715 RVA: 0x00043541 File Offset: 0x00041741
		protected override void DoWork(CancellationToken cancellationToken)
		{
			base.RunAutodiscoverProbe(cancellationToken, "AutodiscoverE15Probe");
		}

		// Token: 0x06000A9C RID: 2716 RVA: 0x00043550 File Offset: 0x00041750
		public override void PopulateDefinition<Definition>(Definition definition, Dictionary<string, string> propertyBag)
		{
			ProbeDefinition probeDefinition = definition as ProbeDefinition;
			string text = propertyBag["Name"];
			bool flag = true;
			bool isMbxProbe = true;
			string a;
			if ((a = text.ToLower()) != null)
			{
				if (!(a == "autodiscoverselftestProbe"))
				{
					if (a == "autodiscoverctpprobe")
					{
						flag = (LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled && !LocalEndpointManager.IsDataCenter);
						isMbxProbe = false;
					}
				}
				else
				{
					flag = LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled;
					isMbxProbe = true;
				}
			}
			if (!flag)
			{
				throw new InvalidOperationException(string.Format("The server role is not valid for probe type {0}", text));
			}
			string location = Assembly.GetExecutingAssembly().Location;
			string fullName = typeof(AutodiscoverE15Probe).FullName;
			string autodiscoverSvcEndpoint = EwsConstants.AutodiscoverSvcEndpoint;
			InvokeProbeUtils.PopulateDefinition(probeDefinition, propertyBag, location, fullName, autodiscoverSvcEndpoint, isMbxProbe);
			probeDefinition.SecondaryEndpoint = EwsConstants.AutodiscoverXmlEndpoint;
		}
	}
}
