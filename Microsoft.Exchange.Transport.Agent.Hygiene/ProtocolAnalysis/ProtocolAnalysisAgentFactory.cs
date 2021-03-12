using System;
using System.Collections;
using System.Collections.Specialized;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Smtp;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProtocolAnalysis;
using Microsoft.Exchange.Extensibility.Internal;
using Microsoft.Exchange.Transport.Agent.AntiSpam.Common;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Background;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Configuration;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.DbAccess;
using Microsoft.Exchange.Transport.Agent.ProtocolAnalysis.Update;

namespace Microsoft.Exchange.Transport.Agent.ProtocolAnalysis
{
	// Token: 0x02000041 RID: 65
	public sealed class ProtocolAnalysisAgentFactory : SmtpReceiveAgentFactory
	{
		// Token: 0x06000168 RID: 360 RVA: 0x0000C400 File Offset: 0x0000A600
		public ProtocolAnalysisAgentFactory()
		{
			this.factorySenders = new HybridDictionary(100);
			this.sendersToFlush = new ArrayList();
			ProtocolAnalysisAgent.AgentFactory = this;
			this.LoadConfiguration();
			Database.Attach();
			this.LoadSrlConfiguration();
			ConfigurationAccess.HandleConfigChangeEvent += this.OnConfigChanged;
			this.factQueue = new ProtocolAnalysisAgentFactory.FactoryPendingQueue();
			this.shutDown = false;
			this.queueThread = new Thread(new ThreadStart(this.QueueThreadProc));
			this.queueThread.Start();
			this.bgAgentFactory = new ProtocolAnalysisBgAgentFactory();
			this.updateAgentFactory = new StsUpdateAgentFactory();
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000C49D File Offset: 0x0000A69D
		internal static bool SrlCalculationDisabled
		{
			get
			{
				return ProtocolAnalysisAgentFactory.IsDnsServerListEmpty();
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600016A RID: 362 RVA: 0x0000C4A4 File Offset: 0x0000A6A4
		internal ProtocolAnalysisBgAgentFactory BgAgentFactory
		{
			get
			{
				return this.bgAgentFactory;
			}
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C4AC File Offset: 0x0000A6AC
		public static bool IsDnsServerListEmpty()
		{
			return 0 == TransportFacades.Dns.ServerList.Count;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C4C0 File Offset: 0x0000A6C0
		public override void Close()
		{
			this.shutDown = true;
			this.queueThread.Join(5000);
			ConfigurationAccess.Unsubscribe();
			Database.Detach();
			ProtocolAnalysisAgentFactory.PerformanceCounters.RemoveCounters();
			this.bgAgentFactory.Close();
			this.updateAgentFactory.Close();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C500 File Offset: 0x0000A700
		public override SmtpReceiveAgent CreateAgent(SmtpServer server)
		{
			if (server == null)
			{
				throw new ArgumentNullException("server", "ProtocolAnalysisAgentFactory.CreateAgent: server parameter must not be null");
			}
			this.smtpFactoryServer = server;
			ProtocolAnalysisAgent result = new ProtocolAnalysisAgent(this, server, this.settings);
			this.bgAgentFactory.CreateAgent(server);
			this.updateAgentFactory.CreateAgent(server);
			return result;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C54E File Offset: 0x0000A74E
		public void OnDisconnect(IDictionary agentSenders)
		{
			this.factQueue.Enqueue(agentSenders);
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C55C File Offset: 0x0000A75C
		internal static void LogSrlCalculationDisabled()
		{
			ProtocolAnalysisAgent.EventLogger.LogEvent(AgentsEventLogConstants.Tuple_DnsNotConfigured, null, null);
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C570 File Offset: 0x0000A770
		private void OnConfigChanged(object o, ConfigChangedEventArgs e)
		{
			if (e != null && e.Fields != null)
			{
				this.srlSettings.Fields = e.Fields;
				return;
			}
			this.LoadConfiguration();
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C595 File Offset: 0x0000A795
		private void LoadConfiguration()
		{
			this.settings = ConfigurationAccess.ConfigSettings;
			this.maxIdleTime = this.settings.MaxIdleTime;
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C5B4 File Offset: 0x0000A7B4
		private void LoadSrlConfiguration()
		{
			this.srlSettings = new ProtocolAnalysisSrlSettings();
			PropertyBag propertyBag = Database.ScanSrlConfiguration();
			if (propertyBag == null || propertyBag.Count < 74)
			{
				this.srlSettings.InitializeDefaults();
				return;
			}
			this.srlSettings.Fields = propertyBag;
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C5F8 File Offset: 0x0000A7F8
		private void QueueThreadProc()
		{
			ExTraceGlobals.FactoryTracer.TraceDebug(0L, "ProtocolAnalysisAgentFactory thread starts");
			while (!this.shutDown)
			{
				IDictionary dictionary = this.factQueue.Dequeue();
				if (dictionary == null)
				{
					Thread.Sleep(1000);
				}
				else
				{
					this.ProcessSenderData(dictionary);
				}
			}
			this.FlushIdleSenders();
			if (this.factorySenders.Count != 0)
			{
				throw new InvalidOperationException("There are still senders left");
			}
			ExTraceGlobals.FactoryTracer.TraceDebug(0L, "ProtocolAnalysisAgentFactory thread shuts down");
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C674 File Offset: 0x0000A874
		private void ProcessSenderData(IDictionary agentSenders)
		{
			IDictionaryEnumerator enumerator = agentSenders.GetEnumerator();
			while (!this.shutDown && enumerator.MoveNext())
			{
				bool flag = false;
				FactorySenderData factorySenderData = null;
				AgentSenderData agentSenderData = (AgentSenderData)enumerator.Value;
				IPAddress ipaddress = (IPAddress)enumerator.Key;
				if (agentSenderData == null)
				{
					throw new InvalidOperationException("Failed to retrieve value from SenderCollection.");
				}
				if (!StsUtil.IsValidSenderIP(ipaddress))
				{
					ExTraceGlobals.FactoryTracer.TraceDebug(0L, "senderIP is not valid.  Exiting ProcessSenderData()");
					return;
				}
				int num = agentSenderData.NumMsgs;
				if (this.factorySenders.Contains(ipaddress))
				{
					factorySenderData = (FactorySenderData)this.factorySenders[ipaddress];
					if (factorySenderData == null)
					{
						throw new InvalidOperationException("Can't find senderCollection inside agent factory.");
					}
					num += factorySenderData.NumMsgs;
				}
				if (num >= this.settings.MinMessagesPerDatabaseTransaction)
				{
					flag = true;
					this.factorySenders.Remove(ipaddress);
				}
				else
				{
					if (factorySenderData == null)
					{
						factorySenderData = new FactorySenderData(DateTime.UtcNow);
					}
					factorySenderData.Merge(agentSenderData);
					this.factorySenders[ipaddress] = factorySenderData;
					if (!this.factorySenders.Contains(ipaddress))
					{
						throw new InvalidOperationException("Failed to add sender back to factory collection.");
					}
				}
				if (!this.shutDown && flag)
				{
					this.FlushSender(ipaddress, factorySenderData, agentSenderData);
				}
			}
			if (this.shutDown)
			{
				return;
			}
			this.FlushIdleSenders();
		}

		// Token: 0x06000175 RID: 373 RVA: 0x0000C7AC File Offset: 0x0000A9AC
		private void FlushIdleSenders()
		{
			IDictionaryEnumerator enumerator = this.factorySenders.GetEnumerator();
			while (enumerator.MoveNext())
			{
				FactorySenderData factorySenderData = (FactorySenderData)enumerator.Value;
				if (this.shutDown || DateTime.UtcNow.Subtract(factorySenderData.LastUpdateTime).TotalMinutes >= (double)this.maxIdleTime)
				{
					this.sendersToFlush.Add((IPAddress)enumerator.Key);
				}
			}
			for (int i = 0; i < this.sendersToFlush.Count; i++)
			{
				IPAddress ipaddress = (IPAddress)this.sendersToFlush[i];
				FactorySenderData factoryData = (FactorySenderData)this.factorySenders[ipaddress];
				this.factorySenders.Remove(ipaddress);
				this.FlushSender(ipaddress, factoryData, null);
			}
			this.sendersToFlush.Clear();
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C87C File Offset: 0x0000AA7C
		private void FlushSender(IPAddress senderIP, FactorySenderData factoryData, AgentSenderData agentData)
		{
			if (!StsUtil.IsValidSenderIP(senderIP))
			{
				throw new ArgumentOutOfRangeException("senderIP", senderIP, "The sender address must be valid");
			}
			ExTraceGlobals.CalculateSrlTracer.TraceDebug<IPAddress>(0L, "Ready to flush sender data, IP: {0}", senderIP);
			if (factoryData == null)
			{
				factoryData = new FactorySenderData(DateTime.UtcNow);
			}
			if (agentData != null)
			{
				factoryData.Merge(agentData);
			}
			SenderDataObject senderDataObject = new SenderDataObject(senderIP);
			if (this.shutDown)
			{
				return;
			}
			senderDataObject.ProcessSender(factoryData, this.settings, this.srlSettings, this.smtpFactoryServer.AcceptedDomains);
		}

		// Token: 0x04000162 RID: 354
		private const int MinSrlValues = 74;

		// Token: 0x04000163 RID: 355
		internal static bool FirstConnect = true;

		// Token: 0x04000164 RID: 356
		private HybridDictionary factorySenders;

		// Token: 0x04000165 RID: 357
		private ArrayList sendersToFlush;

		// Token: 0x04000166 RID: 358
		private ProtocolAnalysisAgentFactory.FactoryPendingQueue factQueue;

		// Token: 0x04000167 RID: 359
		private Thread queueThread;

		// Token: 0x04000168 RID: 360
		private bool shutDown;

		// Token: 0x04000169 RID: 361
		private SmtpServer smtpFactoryServer;

		// Token: 0x0400016A RID: 362
		private int maxIdleTime;

		// Token: 0x0400016B RID: 363
		private ProtocolAnalysisBgAgentFactory bgAgentFactory;

		// Token: 0x0400016C RID: 364
		private StsUpdateAgentFactory updateAgentFactory;

		// Token: 0x0400016D RID: 365
		private SenderReputationConfig settings;

		// Token: 0x0400016E RID: 366
		private ProtocolAnalysisSrlSettings srlSettings;

		// Token: 0x02000042 RID: 66
		internal sealed class PerformanceCounters
		{
			// Token: 0x06000178 RID: 376 RVA: 0x0000C903 File Offset: 0x0000AB03
			public static void SenderSrl(int srl)
			{
				if (srl < 0 || srl > 9)
				{
					throw new ArgumentOutOfRangeException("srl", srl, "SRL must be within 0-9");
				}
				if (ProtocolAnalysisAgentFactory.PerformanceCounters.srlCounters != null)
				{
					ProtocolAnalysisAgentFactory.PerformanceCounters.srlCounters[srl].Increment();
				}
			}

			// Token: 0x06000179 RID: 377 RVA: 0x0000C938 File Offset: 0x0000AB38
			public static void BlockSenderLocalSrl()
			{
				ProtocolAnalysisPerfCounters.BlockSenderLocalSrl.Increment();
			}

			// Token: 0x0600017A RID: 378 RVA: 0x0000C945 File Offset: 0x0000AB45
			public static void BlockSenderRemoteSrl()
			{
				ProtocolAnalysisPerfCounters.BlockSenderRemoteSrl.Increment();
			}

			// Token: 0x0600017B RID: 379 RVA: 0x0000C952 File Offset: 0x0000AB52
			public static void BlockSenderLocalOpenProxy()
			{
				ProtocolAnalysisPerfCounters.BlockSenderLocalOpenProxy.Increment();
			}

			// Token: 0x0600017C RID: 380 RVA: 0x0000C95F File Offset: 0x0000AB5F
			public static void BlockSenderRemoteOpenProxy()
			{
				ProtocolAnalysisPerfCounters.BlockSenderRemoteOpenProxy.Increment();
			}

			// Token: 0x0600017D RID: 381 RVA: 0x0000C96C File Offset: 0x0000AB6C
			public static void BypassSrlCalculation()
			{
				ProtocolAnalysisPerfCounters.BypassSrlCalculation.Increment();
			}

			// Token: 0x0600017E RID: 382 RVA: 0x0000C979 File Offset: 0x0000AB79
			public static void SenderProcessed()
			{
				ProtocolAnalysisPerfCounters.SenderProcessed.Increment();
			}

			// Token: 0x0600017F RID: 383 RVA: 0x0000C988 File Offset: 0x0000AB88
			public static void RemoveCounters()
			{
				ProtocolAnalysisPerfCounters.BlockSenderLocalSrl.RawValue = 0L;
				ProtocolAnalysisPerfCounters.BlockSenderRemoteSrl.RawValue = 0L;
				ProtocolAnalysisPerfCounters.BlockSenderLocalOpenProxy.RawValue = 0L;
				ProtocolAnalysisPerfCounters.BlockSenderRemoteOpenProxy.RawValue = 0L;
				ProtocolAnalysisPerfCounters.BypassSrlCalculation.RawValue = 0L;
				ProtocolAnalysisPerfCounters.SenderProcessed.RawValue = 0L;
				foreach (ExPerformanceCounter exPerformanceCounter in ProtocolAnalysisAgentFactory.PerformanceCounters.srlCounters)
				{
					exPerformanceCounter.RawValue = 0L;
				}
			}

			// Token: 0x0400016F RID: 367
			private static ExPerformanceCounter[] srlCounters = new ExPerformanceCounter[]
			{
				ProtocolAnalysisPerfCounters.SenderSRL0,
				ProtocolAnalysisPerfCounters.SenderSRL1,
				ProtocolAnalysisPerfCounters.SenderSRL2,
				ProtocolAnalysisPerfCounters.SenderSRL3,
				ProtocolAnalysisPerfCounters.SenderSRL4,
				ProtocolAnalysisPerfCounters.SenderSRL5,
				ProtocolAnalysisPerfCounters.SenderSRL6,
				ProtocolAnalysisPerfCounters.SenderSRL7,
				ProtocolAnalysisPerfCounters.SenderSRL8,
				ProtocolAnalysisPerfCounters.SenderSRL9
			};
		}

		// Token: 0x02000043 RID: 67
		internal sealed class FactoryPendingQueue
		{
			// Token: 0x06000182 RID: 386 RVA: 0x0000CA74 File Offset: 0x0000AC74
			public IDictionary Dequeue()
			{
				if (this.factQueue.Count > 0)
				{
					return (IDictionary)this.factQueue.Dequeue();
				}
				return null;
			}

			// Token: 0x06000183 RID: 387 RVA: 0x0000CA96 File Offset: 0x0000AC96
			public void Enqueue(IDictionary sender)
			{
				this.factQueue.Enqueue(sender);
			}

			// Token: 0x04000170 RID: 368
			private Queue factQueue = Queue.Synchronized(new Queue());
		}
	}
}
