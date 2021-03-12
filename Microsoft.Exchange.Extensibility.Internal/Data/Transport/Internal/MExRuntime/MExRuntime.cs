using System;
using System.Collections.Generic;
using System.Runtime;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.CtsResources;
using Microsoft.Exchange.Data.Transport.Delivery;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Data.Transport.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MExRuntime;
using Microsoft.Exchange.Extensibility.Internal;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x0200008B RID: 139
	internal sealed class MExRuntime : IMExRuntime
	{
		// Token: 0x0600044A RID: 1098 RVA: 0x000147F0 File Offset: 0x000129F0
		public MExRuntime()
		{
			string arg = this.GetHashCode().ToString();
			this.InstanceNameFormatted = string.Format("[{0}][{1}] ", "MExRuntime", arg);
			ExTraceGlobals.InitializeTracer.TraceDebug<OperatingSystem, Version, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "OS: {0}; CLR: {1}; GC: {2}", Environment.OSVersion, Environment.Version, GCSettings.IsServerGC ? "Server" : "Workstation");
			ExTraceGlobals.InitializeTracer.TraceDebug((long)this.GetHashCode(), this.InstanceNameFormatted + "created");
		}

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x00014887 File Offset: 0x00012A87
		public int AgentCount
		{
			get
			{
				if (this.runtimeState != 1)
				{
					throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
				}
				return this.settings.AgentCount;
			}
		}

		// Token: 0x0600044C RID: 1100 RVA: 0x000148A8 File Offset: 0x00012AA8
		public void Initialize(string configFile, string agentGroup, ProcessTransportRole processTransportRole, string installPath, FactoryInitializer factoryInitializer = null)
		{
			if (this.runtimeState != 0)
			{
				ExTraceGlobals.InitializeTracer.TraceError<int>((long)this.GetHashCode(), this.InstanceNameFormatted + "invalid state for initialization: {0}", this.runtimeState);
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			this.config = new MExConfiguration(processTransportRole, installPath);
			this.config.Load(configFile);
			this.settings = new RuntimeSettings(this.config, agentGroup, factoryInitializer);
			this.perfCounters = new MExPerfCounters(processTransportRole, this.settings.CreateDefaultAgentOrder());
			Interlocked.CompareExchange(ref this.runtimeState, 1, 0);
			ExTraceGlobals.InitializeTracer.TraceDebug<string, string>((long)this.GetHashCode(), this.InstanceNameFormatted + "initialized ('{0}', '{1}')", configFile, agentGroup);
		}

		// Token: 0x0600044D RID: 1101 RVA: 0x00014965 File Offset: 0x00012B65
		public IMExSession CreateSession(ICloneableInternal state, string name)
		{
			return this.CreateSession(state, name, null, null, null);
		}

		// Token: 0x0600044E RID: 1102 RVA: 0x00014972 File Offset: 0x00012B72
		public IMExSession CreateSession(ICloneableInternal state, string name, Func<bool> resumeAgentCallback)
		{
			return this.CreateSession(state, name, null, null, resumeAgentCallback);
		}

		// Token: 0x0600044F RID: 1103 RVA: 0x00014980 File Offset: 0x00012B80
		public IMExSession CreateSession(ICloneableInternal state, string name, Action startAsyncAgentCallback, Action completeAsyncAgentCallback, Func<bool> resumeAgentCallback)
		{
			if (this.runtimeState != 1)
			{
				ExTraceGlobals.InitializeTracer.TraceError<int>((long)this.GetHashCode(), this.InstanceNameFormatted + "invalid state for initialization: {0}", this.runtimeState);
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			MExSession mexSession = new MExSession(this.settings, state, name, this.GetHashCode().ToString(), startAsyncAgentCallback, completeAsyncAgentCallback, resumeAgentCallback);
			this.perfCounters.Subscribe(mexSession.Dispatcher);
			return mexSession;
		}

		// Token: 0x06000450 RID: 1104 RVA: 0x000149FC File Offset: 0x00012BFC
		public void Shutdown()
		{
			int num = Interlocked.Exchange(ref this.runtimeState, 2);
			ExTraceGlobals.ShutdownTracer.TraceDebug<string, int>((long)this.GetHashCode(), "{0}shutdown invoked ({1})", this.InstanceNameFormatted, num);
			if (num == 1)
			{
				ExTraceGlobals.ShutdownTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}shutting down perf counters.", this.InstanceNameFormatted);
				this.perfCounters.Shutdown();
				ExTraceGlobals.ShutdownTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}shutting down settings.", this.InstanceNameFormatted);
				this.settings.Shutdown();
			}
			ExTraceGlobals.ShutdownTracer.TraceDebug<string>((long)this.GetHashCode(), "{0}shutdown completed.", this.InstanceNameFormatted);
		}

		// Token: 0x06000451 RID: 1105 RVA: 0x00014AA1 File Offset: 0x00012CA1
		public string GetAgentName(int agentSequenceNumber)
		{
			if (this.runtimeState != 1)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			return this.settings.GetAgentName(agentSequenceNumber);
		}

		// Token: 0x06000452 RID: 1106 RVA: 0x00014AC4 File Offset: 0x00012CC4
		public XElement[] GetDiagnosticInfo(DiagnosableParameters parameters, string agentType)
		{
			XElement[] array = null;
			bool flag = parameters.Argument.IndexOf("basic", StringComparison.OrdinalIgnoreCase) != -1;
			bool flag2 = parameters.Argument.IndexOf("verbose", StringComparison.OrdinalIgnoreCase) != -1;
			Dictionary<string, HashSet<string>> dictionary = null;
			if (flag2)
			{
				string[] eventBindings = MExRuntime.AgentTypeToEventBindingsMap[agentType];
				dictionary = this.ComputeAgentNameToActiveEventsMap(eventBindings);
			}
			if (flag || flag2)
			{
				if (this.runtimeState == 1)
				{
					int agentCount = this.AgentCount;
					array = new XElement[agentCount + 1];
					array[0] = new XElement("Count", agentCount);
					int i = 0;
					while (i < agentCount)
					{
						XElement xelement = new XElement(agentType);
						string agentName = this.settings.GetAgentName(i);
						xelement.Add(new XElement("Name", agentName));
						xelement.Add(new XElement("Priority", ++i));
						if (flag2)
						{
							HashSet<string> hashSet;
							if (!dictionary.TryGetValue(agentName, out hashSet))
							{
								hashSet = new HashSet<string>();
							}
							XElement xelement2 = new XElement("ActiveEvents");
							foreach (string content in hashSet)
							{
								xelement2.Add(new XElement("ActiveEvent", content));
							}
							xelement.Add(xelement2);
						}
						array[i] = xelement;
					}
				}
			}
			else
			{
				array = new XElement[]
				{
					new XElement("help", "Supported arguments: basic, verbose, help.")
				};
			}
			return array;
		}

		// Token: 0x06000453 RID: 1107 RVA: 0x00014C7C File Offset: 0x00012E7C
		private Dictionary<string, HashSet<string>> ComputeAgentNameToActiveEventsMap(string[] eventBindings)
		{
			Dictionary<string, HashSet<string>> dictionary = new Dictionary<string, HashSet<string>>();
			foreach (string text in eventBindings)
			{
				IEnumerable<string> enumerable = this.settings.AgentSubscription[text];
				foreach (string key in enumerable)
				{
					HashSet<string> hashSet;
					if (!dictionary.TryGetValue(key, out hashSet))
					{
						hashSet = new HashSet<string>();
						dictionary.Add(key, hashSet);
					}
					hashSet.Add(text);
				}
			}
			return dictionary;
		}

		// Token: 0x040004B5 RID: 1205
		private const int NotInintializedState = 0;

		// Token: 0x040004B6 RID: 1206
		private const int InitializedState = 1;

		// Token: 0x040004B7 RID: 1207
		private const int ShutdownState = 2;

		// Token: 0x040004B8 RID: 1208
		private static readonly Dictionary<string, string[]> AgentTypeToEventBindingsMap = new Dictionary<string, string[]>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"SmtpReceiveAgent",
				SmtpEventBindings.All
			},
			{
				"RoutingAgent",
				RoutingEventBindings.All
			},
			{
				"DeliveryAgent",
				DeliveryAgentEventBindings.All
			},
			{
				"StorageAgent",
				StorageEventBindings.All
			}
		};

		// Token: 0x040004B9 RID: 1209
		private readonly string InstanceNameFormatted;

		// Token: 0x040004BA RID: 1210
		private int runtimeState;

		// Token: 0x040004BB RID: 1211
		private MExConfiguration config;

		// Token: 0x040004BC RID: 1212
		private RuntimeSettings settings;

		// Token: 0x040004BD RID: 1213
		private MExPerfCounters perfCounters;
	}
}
