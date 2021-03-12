using System;
using System.Collections.Generic;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring.Transport
{
	// Token: 0x0200025E RID: 606
	public class ComponentStateBasedServiceProbe : GenericServiceProbe
	{
		// Token: 0x06001448 RID: 5192 RVA: 0x0003BB44 File Offset: 0x00039D44
		public override void PopulateDefinition<ProbeDefinition>(ProbeDefinition pDef, Dictionary<string, string> propertyBag)
		{
			if (pDef == null)
			{
				throw new ArgumentException("Please specify a value for probeDefinition");
			}
			if (propertyBag.ContainsKey("ServerComponentName"))
			{
				pDef.Attributes["ServerComponentName"] = propertyBag["ServerComponentName"].ToString().Trim();
				return;
			}
			throw new ArgumentException("Please specify value forServerComponentName");
		}

		// Token: 0x06001449 RID: 5193 RVA: 0x0003BBA8 File Offset: 0x00039DA8
		protected override bool ShouldRun()
		{
			if (!base.ShouldRun())
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.ServiceTracer, base.TraceContext, "Skipping probe execution as base.ShouldRun returned false.", null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\ComponentStateBasedServiceProbe.cs", 64);
				return false;
			}
			if (!base.Definition.Attributes.ContainsKey("ServerComponentName"))
			{
				throw new ArgumentException(string.Format("{0} attribute is missing from probe definition.", "ServerComponentName"));
			}
			string text = base.Definition.Attributes["ServerComponentName"];
			if (!ServerComponentStateManager.IsValidComponent(text))
			{
				throw new ArgumentException(string.Format("{0} is not a valid value for {1} attribute.", text, "ServerComponentName"));
			}
			ServerComponentEnum serverComponentEnum = (ServerComponentEnum)Enum.Parse(typeof(ServerComponentEnum), text);
			string windowsServiceName = base.GetWindowsServiceName();
			if (TransportCommon.IsServiceDisabledAndInactive(windowsServiceName, serverComponentEnum))
			{
				WTFDiagnostics.TraceDebug<string, ServerComponentEnum>(ExTraceGlobals.MonitoringTracer, base.TraceContext, "Skipping probe execution as service ({0}) is disabled and component state ({1}) is marked as inactive.", windowsServiceName, serverComponentEnum, null, "ShouldRun", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Transport\\ComponentStateBasedServiceProbe.cs", 87);
				base.Result.StateAttribute1 = string.Format("{0} Inactive and Disabled", text);
				return false;
			}
			return true;
		}

		// Token: 0x040009C4 RID: 2500
		internal const string ServerComponentName = "ServerComponentName";
	}
}
