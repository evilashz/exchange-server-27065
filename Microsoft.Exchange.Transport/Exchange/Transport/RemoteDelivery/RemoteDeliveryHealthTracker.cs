using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Data.Transport;

namespace Microsoft.Exchange.Transport.RemoteDelivery
{
	// Token: 0x020003B3 RID: 947
	internal class RemoteDeliveryHealthTracker
	{
		// Token: 0x06002AA5 RID: 10917 RVA: 0x000AA36F File Offset: 0x000A856F
		public RemoteDeliveryHealthTracker(TimeSpan refreshInterval, int threshold, IRemoteDeliveryHealthPerformanceCounters perfCounterWrapper)
		{
			this.refreshInterval = refreshInterval;
			this.messageThresholdToUpdateCounters = threshold;
			this.perfCounterWrapper = perfCounterWrapper;
			this.outboundIPPoolDictionary = new Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>>();
		}

		// Token: 0x06002AA6 RID: 10918 RVA: 0x000AA3A2 File Offset: 0x000A85A2
		public bool StartRefresh()
		{
			if (DateTime.UtcNow - this.lastRefreshedTime < this.refreshInterval)
			{
				return false;
			}
			this.outboundIPPoolDictionaryBeingUpdated = new Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>>();
			this.subjectErrorsBeingUpdated = new int[8];
			return true;
		}

		// Token: 0x06002AA7 RID: 10919 RVA: 0x000AA3DC File Offset: 0x000A85DC
		public void UpdateHealthUsingQueueData(IRoutedMessageQueue queue)
		{
			if (queue.Key.NextHopType.IsSmtpConnectorDeliveryType)
			{
				RiskLevel riskLevel = queue.Key.RiskLevel;
				int outboundIPPool = queue.Key.OutboundIPPool;
				Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData> dictionary;
				if (!this.outboundIPPoolDictionaryBeingUpdated.TryGetValue(riskLevel, out dictionary))
				{
					dictionary = new Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>();
					this.outboundIPPoolDictionaryBeingUpdated[riskLevel] = dictionary;
				}
				RemoteDeliveryHealthTracker.OutboundIPPoolData value;
				if (!dictionary.TryGetValue(outboundIPPool, out value))
				{
					value = default(RemoteDeliveryHealthTracker.OutboundIPPoolData);
				}
				if (!string.IsNullOrEmpty(queue.LastError.StatusCode))
				{
					value.QueuesWithErrors++;
				}
				int activeQueueLength = queue.ActiveQueueLength;
				value.MessageCount += activeQueueLength;
				value.QueueCount++;
				dictionary[outboundIPPool] = value;
				if (!string.IsNullOrWhiteSpace(queue.LastError.EnhancedStatusCode))
				{
					string[] array = queue.LastError.EnhancedStatusCode.Split(new char[]
					{
						'.'
					}, StringSplitOptions.RemoveEmptyEntries);
					int num;
					if (array.Length == 3 && int.TryParse(array[1], out num) && num >= 0 && num <= 7)
					{
						this.subjectErrorsBeingUpdated[num] += activeQueueLength;
					}
				}
			}
		}

		// Token: 0x06002AA8 RID: 10920 RVA: 0x000AA526 File Offset: 0x000A8726
		public void CompleteRefresh()
		{
			this.ProcessOutboundIPPoolDictionary(this.outboundIPPoolDictionaryBeingUpdated);
			this.ProcessSubjectErrors(this.subjectErrorsBeingUpdated);
			this.outboundIPPoolDictionary = this.outboundIPPoolDictionaryBeingUpdated;
			this.outboundIPPoolDictionaryBeingUpdated = null;
			this.subjectErrorsBeingUpdated = null;
			this.lastRefreshedTime = DateTime.UtcNow;
		}

		// Token: 0x06002AA9 RID: 10921 RVA: 0x000AA568 File Offset: 0x000A8768
		public XElement GetDiagnosticInfo()
		{
			Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>> dictionary = this.outboundIPPoolDictionary;
			XElement xelement = new XElement("Pools");
			XElement xelement2 = new XElement("Total");
			xelement.Add(xelement2);
			XElement content = new XElement("UpdateTime", this.lastRefreshedTime);
			xelement.Add(content);
			int num = 0;
			foreach (RiskLevel riskLevel in dictionary.Keys)
			{
				foreach (int num2 in dictionary[riskLevel].Keys)
				{
					RemoteDeliveryHealthTracker.OutboundIPPoolData outboundIPPoolData = dictionary[riskLevel][num2];
					num++;
					XElement xelement3 = new XElement("Pool");
					xelement3.Add(new XElement("RiskLevel", riskLevel));
					xelement3.Add(new XElement("Port", num2));
					xelement3.Add(new XElement("QueueCount", outboundIPPoolData.QueueCount));
					xelement3.Add(new XElement("MessageCount", outboundIPPoolData.MessageCount));
					xelement3.Add(new XElement("QueueFailurePercentage", outboundIPPoolData.PercentageQueuesWithErrors));
					xelement.Add(xelement3);
				}
			}
			xelement2.Value = num.ToString();
			return xelement;
		}

		// Token: 0x06002AAA RID: 10922 RVA: 0x000AA750 File Offset: 0x000A8950
		private void ProcessSubjectErrors(int[] subjectErrors)
		{
			for (int i = 0; i < subjectErrors.Length; i++)
			{
				this.perfCounterWrapper.UpdateSmtpResponseSubCodePerfCounter(i, subjectErrors[i]);
			}
		}

		// Token: 0x06002AAB RID: 10923 RVA: 0x000AA77C File Offset: 0x000A897C
		private void ProcessOutboundIPPoolDictionary(Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>> outboundIPPoolDictionary)
		{
			foreach (RiskLevel riskLevel in outboundIPPoolDictionary.Keys)
			{
				foreach (int num in outboundIPPoolDictionary[riskLevel].Keys)
				{
					RemoteDeliveryHealthTracker.OutboundIPPoolData outboundIPPoolData = outboundIPPoolDictionary[riskLevel][num];
					this.UpdateOutboundIPPoolPerfCounter(num, riskLevel, outboundIPPoolData.PercentageQueuesWithErrors, outboundIPPoolData.MessageCount);
				}
			}
			foreach (RiskLevel riskLevel2 in this.outboundIPPoolDictionary.Keys)
			{
				foreach (int num2 in this.outboundIPPoolDictionary[riskLevel2].Keys)
				{
					if (!outboundIPPoolDictionary.ContainsKey(riskLevel2) || !outboundIPPoolDictionary[riskLevel2].ContainsKey(num2))
					{
						this.ResetOutboundIPPoolPerfCounter(num2, riskLevel2);
					}
				}
			}
		}

		// Token: 0x06002AAC RID: 10924 RVA: 0x000AA8DC File Offset: 0x000A8ADC
		private void UpdateOutboundIPPoolPerfCounter(int pool, RiskLevel riskLevel, int percentageQueuesWithErrors, int messageCount)
		{
			if (messageCount > this.messageThresholdToUpdateCounters)
			{
				this.perfCounterWrapper.UpdateOutboundIPPoolPerfCounter(pool.ToString(), riskLevel, percentageQueuesWithErrors);
				return;
			}
			this.ResetOutboundIPPoolPerfCounter(pool, riskLevel);
		}

		// Token: 0x06002AAD RID: 10925 RVA: 0x000AA905 File Offset: 0x000A8B05
		private void ResetOutboundIPPoolPerfCounter(int pool, RiskLevel riskLevel)
		{
			this.perfCounterWrapper.UpdateOutboundIPPoolPerfCounter(pool.ToString(), riskLevel, 0);
		}

		// Token: 0x0400159F RID: 5535
		private const int MaxKnownSmtpSubCode = 7;

		// Token: 0x040015A0 RID: 5536
		private readonly TimeSpan refreshInterval;

		// Token: 0x040015A1 RID: 5537
		private readonly int messageThresholdToUpdateCounters;

		// Token: 0x040015A2 RID: 5538
		private readonly IRemoteDeliveryHealthPerformanceCounters perfCounterWrapper;

		// Token: 0x040015A3 RID: 5539
		private DateTime lastRefreshedTime = DateTime.MinValue;

		// Token: 0x040015A4 RID: 5540
		private Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>> outboundIPPoolDictionary;

		// Token: 0x040015A5 RID: 5541
		private Dictionary<RiskLevel, Dictionary<int, RemoteDeliveryHealthTracker.OutboundIPPoolData>> outboundIPPoolDictionaryBeingUpdated;

		// Token: 0x040015A6 RID: 5542
		private int[] subjectErrorsBeingUpdated;

		// Token: 0x020003B4 RID: 948
		private struct OutboundIPPoolData
		{
			// Token: 0x17000CE8 RID: 3304
			// (get) Token: 0x06002AAE RID: 10926 RVA: 0x000AA91B File Offset: 0x000A8B1B
			// (set) Token: 0x06002AAF RID: 10927 RVA: 0x000AA923 File Offset: 0x000A8B23
			public int MessageCount { get; set; }

			// Token: 0x17000CE9 RID: 3305
			// (get) Token: 0x06002AB0 RID: 10928 RVA: 0x000AA92C File Offset: 0x000A8B2C
			// (set) Token: 0x06002AB1 RID: 10929 RVA: 0x000AA934 File Offset: 0x000A8B34
			public int QueueCount { get; set; }

			// Token: 0x17000CEA RID: 3306
			// (get) Token: 0x06002AB2 RID: 10930 RVA: 0x000AA93D File Offset: 0x000A8B3D
			// (set) Token: 0x06002AB3 RID: 10931 RVA: 0x000AA945 File Offset: 0x000A8B45
			public int QueuesWithErrors { get; set; }

			// Token: 0x17000CEB RID: 3307
			// (get) Token: 0x06002AB4 RID: 10932 RVA: 0x000AA94E File Offset: 0x000A8B4E
			public int PercentageQueuesWithErrors
			{
				get
				{
					if (this.QueueCount == 0)
					{
						return 0;
					}
					return this.QueuesWithErrors * 100 / this.QueueCount;
				}
			}
		}
	}
}
