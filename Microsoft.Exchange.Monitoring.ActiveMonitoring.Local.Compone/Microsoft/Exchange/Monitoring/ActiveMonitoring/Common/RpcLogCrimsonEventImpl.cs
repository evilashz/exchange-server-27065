using System;
using System.Reflection;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000570 RID: 1392
	internal static class RpcLogCrimsonEventImpl
	{
		// Token: 0x060022DF RID: 8927 RVA: 0x000D2BAC File Offset: 0x000D0DAC
		public static void HandleRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcLogCrimsonEventImpl.Request request = ActiveMonitoringGenericRpcHelper.ValidateAndGetAttachedRequest<RpcLogCrimsonEventImpl.Request>(requestInfo, 1, 0);
			RpcLogCrimsonEventImpl.LogCrimsonEventByReflection(typeof(ManagedAvailabilityCrimsonEvents), request.CrimsonEventName, request.IsPeriodic, request.Parameters);
			RpcLogCrimsonEventImpl.Reply attachedReply = new RpcLogCrimsonEventImpl.Reply();
			replyInfo = ActiveMonitoringGenericRpcHelper.PrepareServerReply(requestInfo, attachedReply, 1, 0);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000D2BF4 File Offset: 0x000D0DF4
		public static void SendRequest(string serverName, string crimsonEventName, bool isPeriodic, TimeSpan timeout, params object[] parameters)
		{
			RpcLogCrimsonEventImpl.Request attachedRequest = new RpcLogCrimsonEventImpl.Request(crimsonEventName, isPeriodic, parameters);
			WTFDiagnostics.TraceDebug<string, string, bool>(ExTraceGlobals.GenericRpcTracer, RpcLogCrimsonEventImpl.traceContext, "RpcLogCrimsonEventImpl.SendRequest() called. (serverName:{0}, eventName: {1}, isPeriodic: {2})", serverName, crimsonEventName, isPeriodic, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcLogCrimsonEventImpl.cs", 102);
			RpcGenericRequestInfo requestInfo = ActiveMonitoringGenericRpcHelper.PrepareClientRequest(attachedRequest, ActiveMonitoringGenericRpcCommandId.LogCrimsonEvent, 1, 0);
			ActiveMonitoringGenericRpcHelper.RunRpcAndGetReply<RpcLogCrimsonEventImpl.Reply>(requestInfo, serverName, (int)timeout.TotalMilliseconds);
			WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.GenericRpcTracer, RpcLogCrimsonEventImpl.traceContext, "RpcLogCrimsonEventImpl.SendRequest() returned. (serverName:{0})", serverName, null, "SendRequest", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\RpcLogCrimsonEventImpl.cs", 123);
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000D2C6C File Offset: 0x000D0E6C
		internal static void LogCrimsonEventByReflection(Type containerClass, string eventName, bool isPeriodic, object[] parameters)
		{
			FieldInfo field = containerClass.GetField(eventName);
			if (field == null)
			{
				throw new InvalidOperationException(string.Format("Failed to find event {0} in ManagedAvailabilityCrimsonEvents", eventName));
			}
			CrimsonEvent crimsonEvent = (CrimsonEvent)field.GetValue(containerClass);
			Type type = crimsonEvent.GetType();
			type.InvokeMember(isPeriodic ? "LogPeriodicGeneric" : "LogGeneric", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod, null, crimsonEvent, parameters);
		}

		// Token: 0x04001908 RID: 6408
		public const int MajorVersion = 1;

		// Token: 0x04001909 RID: 6409
		public const int MinorVersion = 0;

		// Token: 0x0400190A RID: 6410
		public const ActiveMonitoringGenericRpcCommandId CommandCode = ActiveMonitoringGenericRpcCommandId.LogCrimsonEvent;

		// Token: 0x0400190B RID: 6411
		private static TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000571 RID: 1393
		[Serializable]
		internal class Request
		{
			// Token: 0x060022E3 RID: 8931 RVA: 0x000D2CD9 File Offset: 0x000D0ED9
			internal Request(string crimsonEventName, bool isPeriodic, object[] parameters)
			{
				this.CrimsonEventName = crimsonEventName;
				this.IsPeriodic = isPeriodic;
				this.Parameters = parameters;
			}

			// Token: 0x17000745 RID: 1861
			// (get) Token: 0x060022E4 RID: 8932 RVA: 0x000D2CF6 File Offset: 0x000D0EF6
			// (set) Token: 0x060022E5 RID: 8933 RVA: 0x000D2CFE File Offset: 0x000D0EFE
			internal string CrimsonEventName { get; private set; }

			// Token: 0x17000746 RID: 1862
			// (get) Token: 0x060022E6 RID: 8934 RVA: 0x000D2D07 File Offset: 0x000D0F07
			// (set) Token: 0x060022E7 RID: 8935 RVA: 0x000D2D0F File Offset: 0x000D0F0F
			internal bool IsPeriodic { get; private set; }

			// Token: 0x17000747 RID: 1863
			// (get) Token: 0x060022E8 RID: 8936 RVA: 0x000D2D18 File Offset: 0x000D0F18
			// (set) Token: 0x060022E9 RID: 8937 RVA: 0x000D2D20 File Offset: 0x000D0F20
			internal object[] Parameters { get; private set; }
		}

		// Token: 0x02000572 RID: 1394
		[Serializable]
		internal class Reply
		{
		}
	}
}
