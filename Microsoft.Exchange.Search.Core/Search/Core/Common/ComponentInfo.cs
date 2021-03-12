using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics.Components.Search;
using Microsoft.Exchange.Search.Core.Diagnostics;

namespace Microsoft.Exchange.Search.Core.Common
{
	// Token: 0x02000057 RID: 87
	internal class ComponentInfo
	{
		// Token: 0x060001A8 RID: 424 RVA: 0x00003444 File Offset: 0x00001644
		private ComponentInfo(Type type)
		{
			this.type = type;
			this.stateInfoMap = new Dictionary<uint, StateInfo>();
			this.signalInfoMap = new Dictionary<uint, SignalInfo>();
			this.transitionInfoMap = new Dictionary<KeyValuePair<uint, uint>, List<TransitionInfo>>();
			this.diagnosticsSession = DiagnosticsSession.CreateComponentDiagnosticsSession("ComponentInfo", ComponentInstance.Globals.Search.ServiceName, ExTraceGlobals.CoreComponentRegistryTracer, (long)this.type.GetHashCode());
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001A9 RID: 425 RVA: 0x000034AA File Offset: 0x000016AA
		internal Type Type
		{
			get
			{
				return this.type;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001AA RID: 426 RVA: 0x000034B2 File Offset: 0x000016B2
		internal Dictionary<KeyValuePair<uint, uint>, List<TransitionInfo>> TransitionInfoMap
		{
			get
			{
				return this.transitionInfoMap;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060001AB RID: 427 RVA: 0x000034BA File Offset: 0x000016BA
		internal Dictionary<uint, StateInfo> StateInfoMap
		{
			get
			{
				return this.stateInfoMap;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060001AC RID: 428 RVA: 0x000034C2 File Offset: 0x000016C2
		internal Dictionary<uint, SignalInfo> SignalInfoMap
		{
			get
			{
				return this.signalInfoMap;
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000034CA File Offset: 0x000016CA
		public override string ToString()
		{
			return this.type.ToString();
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000034D7 File Offset: 0x000016D7
		internal static ComponentInfo Create<T>() where T : StatefulComponent
		{
			return new ComponentInfo(typeof(T));
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000034E8 File Offset: 0x000016E8
		internal void RegisterState(Enum state)
		{
			StateInfo stateInfo = StateInfo.Create(state);
			if (this.stateInfoMap.ContainsKey(stateInfo.Value))
			{
				string text = string.Format("State {0} with key {1} has already been registered", state, stateInfo);
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, text, new object[0]);
				throw new ArgumentException(text);
			}
			this.diagnosticsSession.TraceDebug<StateInfo>("Registering state {0}", stateInfo);
			this.stateInfoMap.Add(stateInfo.Value, stateInfo);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000355C File Offset: 0x0000175C
		internal void RegisterSignal(Enum signal, SignalPriority priority)
		{
			SignalInfo signalInfo = SignalInfo.Create(signal, priority);
			if (this.signalInfoMap.ContainsKey(signalInfo.Value))
			{
				string text = string.Format("State {0} with key {1} has already been registered", signalInfo.Value, signalInfo);
				this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Failures, "RegisterSignal", new object[]
				{
					text
				});
				throw new InvalidOperationException(text);
			}
			this.diagnosticsSession.TraceDebug<SignalInfo>("Registering signal {0}", signalInfo);
			this.signalInfoMap.Add(signalInfo.Value, signalInfo);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000035E4 File Offset: 0x000017E4
		internal void RegisterTransition(uint sourceState, uint signal, uint targetState, ConditionMethod condition, ActionMethod action)
		{
			SignalInfo signalInfo = null;
			this.diagnosticsSession.TraceDebug("Registering transition for source: {0}, signal: {1}, target: {2}, condition: {3}, action: {4}", new object[]
			{
				sourceState,
				signal,
				targetState,
				condition,
				action
			});
			uint num = signal;
			if ((signal & 4026531840U) != 0U)
			{
				num &= 268435455U;
			}
			if (!this.signalInfoMap.TryGetValue(num, out signalInfo))
			{
				throw new ArgumentException(string.Format("Attempting to register a transition for a signal ({0}) that hasn't been registered", signal));
			}
			StateInfo stateInfo = null;
			if (sourceState != 4294967295U && !this.stateInfoMap.TryGetValue(sourceState, out stateInfo))
			{
				throw new ArgumentException(string.Format("Attempting to register a transition for a source state ({0}) that hasn't been registered", sourceState));
			}
			if (targetState == 4294967295U)
			{
				throw new ArgumentException(string.Format("Registering a target state with value 'Any' is disallowed", new object[0]));
			}
			StateInfo arg = null;
			if (!this.stateInfoMap.TryGetValue(targetState, out arg))
			{
				throw new ArgumentException(string.Format("Attempting to register a transition for a target state ({0}) that hasn't been registered", targetState));
			}
			KeyValuePair<uint, uint> key = new KeyValuePair<uint, uint>(sourceState, signal);
			bool flag = false;
			List<TransitionInfo> list;
			if (!this.transitionInfoMap.TryGetValue(key, out list))
			{
				list = new List<TransitionInfo>(1);
				flag = true;
			}
			TransitionInfo transitionInfo = TransitionInfo.Create(condition, action, targetState);
			this.diagnosticsSession.TraceDebug("Registered transition: [{0}, {1}] {2}, a:{3}, c:{4}", new object[]
			{
				stateInfo,
				signalInfo,
				transitionInfo,
				action,
				condition
			});
			if (this.transitionInfoMap.ContainsKey(key))
			{
				this.diagnosticsSession.TraceDebug<StateInfo, SignalInfo>("Transition for state {0} with signal {1} already exists, overwriting.", arg, signalInfo);
			}
			list.Add(transitionInfo);
			if (flag)
			{
				this.transitionInfoMap.Add(key, list);
			}
		}

		// Token: 0x040000A1 RID: 161
		private readonly IDiagnosticsSession diagnosticsSession;

		// Token: 0x040000A2 RID: 162
		private Type type;

		// Token: 0x040000A3 RID: 163
		private Dictionary<KeyValuePair<uint, uint>, List<TransitionInfo>> transitionInfoMap;

		// Token: 0x040000A4 RID: 164
		private Dictionary<uint, StateInfo> stateInfoMap;

		// Token: 0x040000A5 RID: 165
		private Dictionary<uint, SignalInfo> signalInfoMap;
	}
}
