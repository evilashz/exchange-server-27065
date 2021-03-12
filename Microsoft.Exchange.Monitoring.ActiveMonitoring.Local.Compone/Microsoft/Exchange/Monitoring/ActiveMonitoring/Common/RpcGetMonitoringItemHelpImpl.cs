using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x0200056F RID: 1391
	internal static class RpcGetMonitoringItemHelpImpl
	{
		// Token: 0x060022DA RID: 8922 RVA: 0x000D2A00 File Offset: 0x000D0C00
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGetMonitoringItemHelp.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcGetMonitoringItemHelp.Request>(requestInfo, 1, 0);
			RpcGetMonitoringItemHelp.Reply reply = new RpcGetMonitoringItemHelp.Reply();
			List<PropertyInformation> propertyInformation = RpcGetMonitoringItemHelpImpl.GetPropertyInformation(request.MonitorIdentity);
			reply.HelpEntries = ((propertyInformation != null) ? propertyInformation : new List<PropertyInformation>());
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, reply, 1, 0);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000D2A44 File Offset: 0x000D0C44
		private static List<PropertyInformation> GetPropertyInformation(string monitorIdentity)
		{
			string empty = string.Empty;
			string empty2 = string.Empty;
			RpcGetMonitoringItemHelpImpl.SetAssemblyPathAndType(monitorIdentity, ref empty, ref empty2);
			if (string.IsNullOrEmpty(empty) || string.IsNullOrEmpty(empty2))
			{
				WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.GenericRpcTracer, RpcGetMonitoringItemHelpImpl.traceContext, "RpcGetMonitoringItemHelpImpl.GetPropertyInformation() returning null. (monitorIdentity: {0}, assemblyPath:{1}, typeName:{2})", monitorIdentity, empty, empty2, null, "GetPropertyInformation", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcGetMonitoringItemHelpImpl.cs", 87);
				return null;
			}
			return RpcGetMonitoringItemHelpImpl.GetPropertyInformation(empty, empty2);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000D2AA4 File Offset: 0x000D0CA4
		private static void SetAssemblyPathAndType(string monitorIdentity, ref string assemblyPath, ref string typeName)
		{
			WorkDefinition workDefinition = InvokeNowCommon.GetWorkDefinition(monitorIdentity);
			if (workDefinition != null)
			{
				assemblyPath = workDefinition.AssemblyPath;
				typeName = workDefinition.TypeName;
			}
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000D2ACC File Offset: 0x000D0CCC
		private static List<PropertyInformation> GetPropertyInformation(string assemblyPath, string monitorType)
		{
			Assembly assembly = Assembly.LoadFrom(assemblyPath);
			Type type = assembly.GetType(monitorType, false, true);
			WorkItem workItem = (WorkItem)Activator.CreateInstance(type, new object[0]);
			List<PropertyInformation> list = new List<PropertyInformation>();
			foreach (PropertyInformation item in workItem.GetPropertyInformation(workItem.GetSubstitutePropertyInformation()))
			{
				list.Add(item);
			}
			List<PropertyInformation> list2 = new List<PropertyInformation>();
			foreach (PropertyInformation item2 in workItem.GetPropertyInformation(list))
			{
				list2.Add(item2);
			}
			return list2;
		}

		// Token: 0x04001906 RID: 6406
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.GetMonitoringItemHelp;

		// Token: 0x04001907 RID: 6407
		private static TracingContext traceContext = TracingContext.Default;
	}
}
