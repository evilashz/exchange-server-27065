using System;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.MailboxTransport.StoreDriverCommon;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.StoreDriver.Shared
{
	// Token: 0x02000020 RID: 32
	internal static class MExEvents
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00006C94 File Offset: 0x00004E94
		public static IMExSession GetExecutionContext(StoreDriverServer storeDriverServer)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before GetExecutionContext()");
			}
			IMExSession imexSession = MExEvents.mexRuntime.CreateSession(storeDriverServer, "SD");
			imexSession.Dispatcher.OnAgentInvokeStart += new AgentInvokeStartHandler(MExEvents.OnAgentInvokeStart);
			imexSession.Dispatcher.OnAgentInvokeEnd += new AgentInvokeEndHandler(MExEvents.OnAgentInvokeReturns);
			return imexSession;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006CF3 File Offset: 0x00004EF3
		public static void FreeExecutionContext(IMExSession context)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before FreeExecutionContext()");
			}
			context.Close();
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006D0D File Offset: 0x00004F0D
		public static IMExSession CloneExecutionContext(IMExSession mexSession)
		{
			return (IMExSession)mexSession.Clone();
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006D1C File Offset: 0x00004F1C
		public static void RaiseEvent(IMExSession mexSession, string eventTopic, params object[] contexts)
		{
			if (MExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before RaiseEvent()");
			}
			IAsyncResult asyncResult = null;
			try
			{
				asyncResult = mexSession.BeginInvoke(eventTopic, contexts[0], contexts[1], null, null);
				mexSession.EndInvoke(asyncResult);
			}
			catch (LocalizedException e)
			{
				MExEvents.HandleAgentExchangeExceptions(mexSession, e);
			}
			MExAsyncResult mexAsyncResult = (MExAsyncResult)asyncResult;
			if (mexAsyncResult != null && mexAsyncResult.AsyncException != null)
			{
				throw new StoreDriverAgentRaisedException(mexAsyncResult.FaultyAgentName, mexAsyncResult.AsyncException);
			}
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006D94 File Offset: 0x00004F94
		public static void Initialize(string configFilePath, ProcessTransportRole role, LatencyAgentGroup latencyAgentGroup, string agentGroup)
		{
			if (MExEvents.mexRuntime != null)
			{
				throw new InvalidOperationException("Cannot Initialize() again without calling Shutdown() first");
			}
			MExEvents.processTransportRole = role;
			MExEvents.latencyAgentGroup = latencyAgentGroup;
			MExEvents.agentGroup = agentGroup;
			MExEvents.mexRuntime = new MExRuntime();
			MExEvents.mexRuntime.Initialize(configFilePath, agentGroup, role, ConfigurationContext.Setup.InstallPath, new FactoryInitializer(ProcessAccessManager.RegisterAgentFactory));
			AgentLatencyTracker.RegisterMExRuntime(latencyAgentGroup, MExEvents.mexRuntime);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006DF8 File Offset: 0x00004FF8
		public static void Shutdown()
		{
			if (MExEvents.mexRuntime != null)
			{
				MExEvents.mexRuntime.Shutdown();
				MExEvents.mexRuntime = null;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00006E11 File Offset: 0x00005011
		public static void HandleAgentExchangeExceptions(IMExSession mexSession, LocalizedException e)
		{
			TraceHelper.ExtensibilityTracer.TraceFail<string, string, LocalizedException>(TraceHelper.MessageProbeActivityId, 0L, "Agent {0} running in context {1} hit Unhandled Exception {2}", (mexSession.CurrentAgent == null) ? null : mexSession.CurrentAgent.Name, mexSession.EventTopic, e);
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00006E48 File Offset: 0x00005048
		private static void OnAgentInvokeStart(object dispatcher, IMExSession context)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.Action = context.CurrentAgent.Name;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00006E70 File Offset: 0x00005070
		private static void OnAgentInvokeReturns(object dispatcher, IMExSession context)
		{
			IActivityScope currentActivityScope = ActivityContext.GetCurrentActivityScope();
			if (currentActivityScope != null)
			{
				currentActivityScope.Action = null;
			}
		}

		// Token: 0x0400006D RID: 109
		private static MExRuntime mexRuntime;

		// Token: 0x0400006E RID: 110
		private static ProcessTransportRole processTransportRole;

		// Token: 0x0400006F RID: 111
		private static LatencyAgentGroup latencyAgentGroup;

		// Token: 0x04000070 RID: 112
		private static string agentGroup;
	}
}
