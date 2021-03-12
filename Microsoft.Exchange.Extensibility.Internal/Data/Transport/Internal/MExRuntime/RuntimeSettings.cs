using System;
using System.Threading;
using Microsoft.Exchange.CtsResources;

namespace Microsoft.Exchange.Data.Transport.Internal.MExRuntime
{
	// Token: 0x02000090 RID: 144
	internal sealed class RuntimeSettings : IRuntimeSettings
	{
		// Token: 0x060004A2 RID: 1186 RVA: 0x000161A4 File Offset: 0x000143A4
		public RuntimeSettings(MExConfiguration config, string agentGroup, FactoryInitializer factoryInitializer)
		{
			AgentInfo[] enabledAgentsByType = config.GetEnabledAgentsByType(agentGroup);
			this.factoryTable = new FactoryTable(enabledAgentsByType, factoryInitializer);
			this.agentsInDefaultOrder = new AgentRecord[enabledAgentsByType.Length];
			this.monitoringOptions = config.MonitoringOptions;
			for (int i = 0; i < this.agentsInDefaultOrder.Length; i++)
			{
				AgentInfo agentInfo = enabledAgentsByType[i];
				this.agentsInDefaultOrder[i] = new AgentRecord(agentInfo.Id, agentInfo.AgentName, agentInfo.BaseTypeName, i, agentInfo.IsInternal);
			}
			string[] agents;
			string[][] eventTopics;
			AgentRecord[] array;
			RuntimeSettings.InitializeAgentsAndSubscriptions(config, agentGroup, false, out agents, out eventTopics, out array);
			this.agentSubscription = new AgentSubscription(agentGroup, agents, eventTopics);
			RuntimeSettings.InitializeAgentsAndSubscriptions(config, agentGroup, true, out agents, out eventTopics, out this.publicAgentsInDefaultOrder);
			this.disposeAgents = config.DisposeAgents;
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x060004A3 RID: 1187 RVA: 0x00016272 File Offset: 0x00014472
		public AgentRecord[] PublicAgentsInDefaultOrder
		{
			get
			{
				return this.publicAgentsInDefaultOrder;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0001627A File Offset: 0x0001447A
		public bool DisposeAgents
		{
			get
			{
				return this.disposeAgents;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x060004A5 RID: 1189 RVA: 0x00016282 File Offset: 0x00014482
		public MonitoringOptions MonitoringOptions
		{
			get
			{
				return this.monitoringOptions;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0001628A File Offset: 0x0001448A
		public FactoryTable AgentFactories
		{
			get
			{
				return this.factoryTable;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x060004A7 RID: 1191 RVA: 0x00016292 File Offset: 0x00014492
		public AgentSubscription AgentSubscription
		{
			get
			{
				return this.agentSubscription;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0001629A File Offset: 0x0001449A
		public int AgentCount
		{
			get
			{
				return this.agentsInDefaultOrder.Length;
			}
		}

		// Token: 0x060004A9 RID: 1193 RVA: 0x000162A4 File Offset: 0x000144A4
		public AgentRecord[] CreateDefaultAgentOrder()
		{
			AgentRecord[] array = new AgentRecord[this.agentsInDefaultOrder.Length];
			for (int i = 0; i < this.agentsInDefaultOrder.Length; i++)
			{
				array[i] = new AgentRecord(this.agentsInDefaultOrder[i].Id, this.agentsInDefaultOrder[i].Name, this.agentsInDefaultOrder[i].Type, this.agentsInDefaultOrder[i].SequenceNumber, this.agentsInDefaultOrder[i].IsInternal);
			}
			return array;
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x0001631C File Offset: 0x0001451C
		public void SaveAgentSubscription(AgentRecord[] agentRecords)
		{
			long ticks = DateTime.UtcNow.Ticks;
			long num = this.timeToSaveAgentSubscription;
			if (ticks <= num)
			{
				return;
			}
			long num2 = Interlocked.CompareExchange(ref this.timeToSaveAgentSubscription, DateTime.MaxValue.Ticks, num);
			if (num != num2)
			{
				return;
			}
			string[][] array = new string[agentRecords.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new string[agentRecords[i].Instance.Handlers.Count];
				int num3 = 0;
				foreach (object obj in agentRecords[i].Instance.Handlers.Keys)
				{
					string text = (string)obj;
					array[i][num3++] = text;
				}
			}
			this.agentSubscription.Update(array);
			this.agentSubscription.Save();
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016420 File Offset: 0x00014620
		public void Shutdown()
		{
			if (this.sessionCount > 0)
			{
				throw new InvalidOperationException(MExRuntimeStrings.InvalidState);
			}
			this.factoryTable.Shutdown();
			if (this.agentSubscription != null)
			{
				this.agentSubscription.Dispose();
			}
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016454 File Offset: 0x00014654
		public void AddSessionRef()
		{
			Interlocked.Increment(ref this.sessionCount);
		}

		// Token: 0x060004AD RID: 1197 RVA: 0x00016462 File Offset: 0x00014662
		public void ReleaseSessionRef()
		{
			Interlocked.Decrement(ref this.sessionCount);
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x00016470 File Offset: 0x00014670
		public string GetAgentName(int agentSequenceNumber)
		{
			return this.agentsInDefaultOrder[agentSequenceNumber].Name;
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016480 File Offset: 0x00014680
		private static void InitializeAgentsAndSubscriptions(MExConfiguration config, string agentGroup, bool publicAgentsOnly, out string[] agents, out string[][] subscriptions, out AgentRecord[] agentsInDefaultOrder)
		{
			AgentInfo[] array = publicAgentsOnly ? config.GetEnaledPublicAgentsByType(agentGroup) : config.GetEnabledAgentsByType(agentGroup);
			agents = new string[array.Length];
			agentsInDefaultOrder = new AgentRecord[array.Length];
			subscriptions = new string[array.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				AgentInfo agentInfo = array[i];
				agentsInDefaultOrder[i] = new AgentRecord(agentInfo.Id, agentInfo.AgentName, agentInfo.BaseTypeName, i, agentInfo.IsInternal);
				agents[i] = agentInfo.AgentName;
				subscriptions[i] = new string[0];
			}
		}

		// Token: 0x040004E5 RID: 1253
		private readonly MonitoringOptions monitoringOptions;

		// Token: 0x040004E6 RID: 1254
		private readonly FactoryTable factoryTable;

		// Token: 0x040004E7 RID: 1255
		private readonly AgentRecord[] agentsInDefaultOrder;

		// Token: 0x040004E8 RID: 1256
		private readonly AgentSubscription agentSubscription;

		// Token: 0x040004E9 RID: 1257
		private readonly bool disposeAgents;

		// Token: 0x040004EA RID: 1258
		private readonly AgentRecord[] publicAgentsInDefaultOrder;

		// Token: 0x040004EB RID: 1259
		private int sessionCount;

		// Token: 0x040004EC RID: 1260
		private long timeToSaveAgentSubscription = DateTime.MinValue.Ticks;
	}
}
