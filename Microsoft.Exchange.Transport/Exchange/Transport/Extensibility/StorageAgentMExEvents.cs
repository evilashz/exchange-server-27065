using System;
using System.Xml.Linq;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Transport.Internal.MExRuntime;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x0200030D RID: 781
	internal static class StorageAgentMExEvents
	{
		// Token: 0x060021F6 RID: 8694 RVA: 0x00080594 File Offset: 0x0007E794
		public static void Initialize(string configFilePath)
		{
			if (StorageAgentMExEvents.mexRuntime != null)
			{
				throw new InvalidOperationException("Cannot Initialize() again without calling Shutdown() first");
			}
			StorageAgentMExEvents.mexRuntime = new MExRuntime();
			StorageAgentMExEvents.mexRuntime.Initialize(configFilePath, "Microsoft.Exchange.Data.Transport.Storage.StorageAgent", Components.Configuration.ProcessTransportRole, ConfigurationContext.Setup.InstallPath, new FactoryInitializer(ProcessAccessManager.RegisterAgentFactory));
			AgentLatencyTracker.RegisterMExRuntime(LatencyAgentGroup.Storage, StorageAgentMExEvents.mexRuntime);
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x000805F3 File Offset: 0x0007E7F3
		public static void Shutdown()
		{
			if (StorageAgentMExEvents.mexRuntime != null)
			{
				StorageAgentMExEvents.mexRuntime.Shutdown();
				StorageAgentMExEvents.mexRuntime = null;
			}
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x0008060C File Offset: 0x0007E80C
		public static IMExSession GetExecutionContext()
		{
			if (StorageAgentMExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize must be called before GetExecutionContext");
			}
			StorageAgentState state = new StorageAgentState();
			return StorageAgentMExEvents.mexRuntime.CreateSession(state, "BootScanner");
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00080641 File Offset: 0x0007E841
		public static void FreeExecutionContext(MExSession context)
		{
			if (StorageAgentMExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize must be called before FreeExecutionContext");
			}
			context.Close();
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x0008065C File Offset: 0x0007E85C
		public static void RaiseEvent(IMExSession mexSession, string eventTopic, params object[] contexts)
		{
			if (StorageAgentMExEvents.mexRuntime == null)
			{
				throw new InvalidOperationException("Initialize() must be called before RaiseEvent()");
			}
			try
			{
				mexSession.Invoke(eventTopic, contexts[0], contexts[1]);
			}
			catch (LocalizedException e)
			{
				StorageAgentMExEvents.TraceAgentExchangeExceptions(mexSession, e);
				throw;
			}
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x000806A4 File Offset: 0x0007E8A4
		public static void TraceAgentExchangeExceptions(IMExSession mexSession, LocalizedException e)
		{
			ExTraceGlobals.ExtensibilityTracer.TraceError<string, string, LocalizedException>(0L, "Agent {0} running in context {1} hit Unhandled Exception {2}", (mexSession.CurrentAgent == null) ? null : mexSession.CurrentAgent.Name, mexSession.EventTopic, e);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x000806D4 File Offset: 0x0007E8D4
		public static XElement[] GetDiagnosticInfo(DiagnosableParameters parameters)
		{
			if (StorageAgentMExEvents.mexRuntime != null)
			{
				return StorageAgentMExEvents.mexRuntime.GetDiagnosticInfo(parameters, "StorageAgent");
			}
			return null;
		}

		// Token: 0x040011C9 RID: 4553
		private static MExRuntime mexRuntime;
	}
}
