using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Common;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000220 RID: 544
	internal class DagSelector
	{
		// Token: 0x060017E5 RID: 6117 RVA: 0x00060D7C File Offset: 0x0005EF7C
		public DagSelector(int messageThresholdPerServer, double messageThresholdIncreaseFactor, int activeServersForDagToBeRoutable, int minimumSitesForDagToBeRoutable, bool logDiagnosticInfo, ITenantDagQuota tenantDagQuota, IEnumerable<DeliveryGroup> dags)
		{
			RoutingUtils.ThrowIfNull(tenantDagQuota, "tenantDagQuota");
			RoutingUtils.ThrowIfNull(dags, "dags");
			ArgumentValidator.ThrowIfZeroOrNegative("activeServersForDagToBeRoutable", activeServersForDagToBeRoutable);
			ArgumentValidator.ThrowIfZeroOrNegative("minimumSitesForDagToBeRoutable", minimumSitesForDagToBeRoutable);
			this.InitializeDagDictionary(messageThresholdPerServer, activeServersForDagToBeRoutable, minimumSitesForDagToBeRoutable, dags);
			this.messageThresholdIncreaseFactor = messageThresholdIncreaseFactor;
			this.logDiagnosticInfo = logDiagnosticInfo;
			this.tenantDagQuota = tenantDagQuota;
			this.tenantDagQuota.RefreshDagCount(this.dagsInOrder.Count);
			this.currentMessageThresholdMultiplier = 0;
		}

		// Token: 0x060017E6 RID: 6118 RVA: 0x00060E00 File Offset: 0x0005F000
		public void IncrementMessagesDeliveredBasedOnMailbox(Guid dagId, Guid tenantId)
		{
			DagSelector.DagData dagData;
			if (this.dagDictionary.TryGetValue(dagId, out dagData))
			{
				dagData.IncrementMessagesDeliveredBasedOnMailbox();
			}
			this.tenantDagQuota.IncrementMessagesDeliveredToTenant(tenantId);
		}

		// Token: 0x060017E7 RID: 6119 RVA: 0x00060E30 File Offset: 0x0005F030
		public bool TryGetDagDeliveryGroup(Guid externalOrganizationId, out DeliveryGroup dagDeliveryGroup)
		{
			if (this.dagsInOrder.Count == 0)
			{
				dagDeliveryGroup = null;
				return false;
			}
			int num = DagSelector.GetHashForGuid(externalOrganizationId) % this.dagsInOrder.Count;
			int dagCountForTenant = this.tenantDagQuota.GetDagCountForTenant(externalOrganizationId);
			int num2 = 0;
			for (;;)
			{
				int num3 = this.currentMessageThresholdMultiplier;
				double messageThresholdFactor = this.GetMessageThresholdFactor();
				if (this.TryGetDagDeliveryGroup(num, dagCountForTenant, messageThresholdFactor, true, out dagDeliveryGroup))
				{
					break;
				}
				if (this.TryGetDagDeliveryGroup((num + dagCountForTenant) % this.dagsInOrder.Count, this.dagsInOrder.Count - dagCountForTenant, messageThresholdFactor, false, out dagDeliveryGroup))
				{
					goto Block_3;
				}
				Interlocked.CompareExchange(ref this.currentMessageThresholdMultiplier, num3 + 1, num3);
				if (++num2 > 1000)
				{
					goto Block_4;
				}
			}
			this.tenantDagQuota.IncrementMessagesDeliveredToTenant(externalOrganizationId);
			return true;
			Block_3:
			this.tenantDagQuota.IncrementMessagesDeliveredToTenant(externalOrganizationId);
			return true;
			Block_4:
			throw new InvalidOperationException(string.Format("DagSelector.TryGetDagDeliveryGroup has iterated {0} times and not found a DAG that can accept messages", num2));
		}

		// Token: 0x060017E8 RID: 6120 RVA: 0x00060F04 File Offset: 0x0005F104
		public bool TryGetDiagnosticInfo(bool verbose, DiagnosableParameters parameters, out XElement diagnosticInfo)
		{
			bool flag = verbose;
			if (!flag)
			{
				flag = parameters.Argument.Equals("DagSelector", StringComparison.InvariantCultureIgnoreCase);
			}
			if (flag)
			{
				diagnosticInfo = this.GetDiagnosticInfo();
			}
			else
			{
				diagnosticInfo = null;
			}
			return flag;
		}

		// Token: 0x060017E9 RID: 6121 RVA: 0x00060F3C File Offset: 0x0005F13C
		public void LogDiagnosticInfo()
		{
			if (this.logDiagnosticInfo)
			{
				RoutingDiag.EventLogger.LogEvent(TransportEventLogConstants.Tuple_DagSelectorDiagnosticInfo, null, new object[]
				{
					this.GetDiagnosticInfo()
				});
			}
		}

		// Token: 0x060017EA RID: 6122 RVA: 0x00060F73 File Offset: 0x0005F173
		private static int GetHashForGuid(Guid tenantId)
		{
			return Math.Abs(tenantId.GetHashCode());
		}

		// Token: 0x060017EB RID: 6123 RVA: 0x00060F90 File Offset: 0x0005F190
		private void InitializeDagDictionary(int messageThreshold, int activeServersForDagToBeRoutable, int minimumSitesForDagToBeRoutable, IEnumerable<DeliveryGroup> dags)
		{
			this.dagDictionary = new Dictionary<Guid, DagSelector.DagData>();
			List<Guid> list = new List<Guid>();
			List<DeliveryGroup> list2 = null;
			foreach (DeliveryGroup deliveryGroup in dags)
			{
				DagSelector.DagData dagData = new DagSelector.DagData(messageThreshold, activeServersForDagToBeRoutable, minimumSitesForDagToBeRoutable, deliveryGroup);
				if (dagData.CanAcceptMessages)
				{
					if (TransportHelpers.AttemptAddToDictionary<Guid, DagSelector.DagData>(this.dagDictionary, deliveryGroup.NextHopGuid, dagData, new TransportHelpers.DiagnosticsHandler<Guid, DagSelector.DagData>(RoutingUtils.LogErrorWhenAddToDictionaryFails<Guid, DagSelector.DagData>)))
					{
						list.Add(deliveryGroup.NextHopGuid);
					}
				}
				else
				{
					RoutingUtils.AddItemToLazyList<DeliveryGroup>(deliveryGroup, ref list2);
				}
			}
			if (list2 != null)
			{
				ExEventLog eventLogger = RoutingDiag.EventLogger;
				ExEventLog.EventTuple tuple_InactiveDagsExcludedFromDagSelector = TransportEventLogConstants.Tuple_InactiveDagsExcludedFromDagSelector;
				string periodicKey = null;
				object[] array = new object[1];
				array[0] = string.Join(", ", from dag in list2
				select dag.Name);
				eventLogger.LogEvent(tuple_InactiveDagsExcludedFromDagSelector, periodicKey, array);
			}
			list.Sort();
			this.dagsInOrder = list;
		}

		// Token: 0x060017EC RID: 6124 RVA: 0x00061090 File Offset: 0x0005F290
		private XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("DagSelector");
			xelement.SetAttributeValue("MessageThresholdFactor", this.messageThresholdIncreaseFactor);
			xelement.SetAttributeValue("MessageThresholdMultiplier", this.currentMessageThresholdMultiplier);
			XElement xelement2 = new XElement("DagData");
			xelement.Add(xelement2);
			for (int i = 0; i < this.dagsInOrder.Count; i++)
			{
				xelement2.Add(this.dagDictionary[this.dagsInOrder[i]].GetDiagnosticInfo(this.GetMessageThresholdFactor()));
			}
			return xelement;
		}

		// Token: 0x060017ED RID: 6125 RVA: 0x0006113C File Offset: 0x0005F33C
		private bool TryGetDagDeliveryGroup(int startOffset, int dagsForTenant, double messageThresholdFactor, bool randomizeStartOffset, out DeliveryGroup deliveryGroup)
		{
			int num = randomizeStartOffset ? ((startOffset + RoutingUtils.GetRandomNumber(dagsForTenant)) % this.dagsInOrder.Count) : startOffset;
			for (int i = 0; i < dagsForTenant; i++)
			{
				DagSelector.DagData dagData = this.dagDictionary[this.dagsInOrder[num]];
				if (dagData.IsUnderMessageThreshold(messageThresholdFactor))
				{
					deliveryGroup = dagData.DeliveryGroup;
					dagData.IncrementMessagesDeliveredBasedOnDagSelector();
					return true;
				}
				num = (num + 1) % this.dagsInOrder.Count;
			}
			deliveryGroup = null;
			return false;
		}

		// Token: 0x060017EE RID: 6126 RVA: 0x000611B8 File Offset: 0x0005F3B8
		private double GetMessageThresholdFactor()
		{
			return 1.0 + (double)this.currentMessageThresholdMultiplier * this.messageThresholdIncreaseFactor;
		}

		// Token: 0x04000BA6 RID: 2982
		private const int TryGetDagDeliveryGroupMaxLoopCount = 1000;

		// Token: 0x04000BA7 RID: 2983
		private ITenantDagQuota tenantDagQuota;

		// Token: 0x04000BA8 RID: 2984
		private Dictionary<Guid, DagSelector.DagData> dagDictionary;

		// Token: 0x04000BA9 RID: 2985
		private List<Guid> dagsInOrder;

		// Token: 0x04000BAA RID: 2986
		private int currentMessageThresholdMultiplier;

		// Token: 0x04000BAB RID: 2987
		private readonly double messageThresholdIncreaseFactor;

		// Token: 0x04000BAC RID: 2988
		private readonly bool logDiagnosticInfo;

		// Token: 0x02000221 RID: 545
		private class DagData
		{
			// Token: 0x060017F0 RID: 6128 RVA: 0x000611D2 File Offset: 0x0005F3D2
			public DagData(int messageThresholdPerServer, int activeServersForDagToBeRoutable, int minimumSitesForDagToBeRoutable, DeliveryGroup dag)
			{
				this.DeliveryGroup = dag;
				this.messageThreshold = DagSelector.DagData.GetMessageThresholdBasedOnDag(dag, messageThresholdPerServer, activeServersForDagToBeRoutable, minimumSitesForDagToBeRoutable);
			}

			// Token: 0x17000660 RID: 1632
			// (get) Token: 0x060017F1 RID: 6129 RVA: 0x000611F2 File Offset: 0x0005F3F2
			// (set) Token: 0x060017F2 RID: 6130 RVA: 0x000611FA File Offset: 0x0005F3FA
			public DeliveryGroup DeliveryGroup { get; private set; }

			// Token: 0x17000661 RID: 1633
			// (get) Token: 0x060017F3 RID: 6131 RVA: 0x00061203 File Offset: 0x0005F403
			public bool CanAcceptMessages
			{
				get
				{
					return this.messageThreshold > 0;
				}
			}

			// Token: 0x060017F4 RID: 6132 RVA: 0x0006120E File Offset: 0x0005F40E
			public void IncrementMessagesDeliveredBasedOnMailbox()
			{
				Interlocked.Increment(ref this.messagesDeliveredBasedOnMailbox);
			}

			// Token: 0x060017F5 RID: 6133 RVA: 0x0006121C File Offset: 0x0005F41C
			public void IncrementMessagesDeliveredBasedOnDagSelector()
			{
				Interlocked.Increment(ref this.messagesDeliveredBasedOnDagSelector);
			}

			// Token: 0x060017F6 RID: 6134 RVA: 0x0006122A File Offset: 0x0005F42A
			public bool IsUnderMessageThreshold(double multiplier)
			{
				return (double)(this.messagesDeliveredBasedOnMailbox + this.messagesDeliveredBasedOnDagSelector) < (double)this.messageThreshold * multiplier;
			}

			// Token: 0x060017F7 RID: 6135 RVA: 0x00061248 File Offset: 0x0005F448
			public XElement GetDiagnosticInfo(double messageThresholdFactor)
			{
				XElement xelement = new XElement("Dag");
				xelement.SetAttributeValue("Name", this.DeliveryGroup.Name);
				xelement.SetAttributeValue("Id", this.DeliveryGroup.NextHopGuid);
				xelement.SetAttributeValue("MessageThreshold", Math.Ceiling((double)this.messageThreshold * messageThresholdFactor));
				xelement.SetAttributeValue("MessagesDeliveredBasedOnMailbox", this.messagesDeliveredBasedOnMailbox);
				xelement.SetAttributeValue("MessagesDeliveredBasedOnDagSelector", this.messagesDeliveredBasedOnDagSelector);
				return xelement;
			}

			// Token: 0x060017F8 RID: 6136 RVA: 0x000612FC File Offset: 0x0005F4FC
			private static int GetMessageThresholdBasedOnDag(DeliveryGroup dag, int messageThresholdPerServer, int activeServersForDagToBeRoutable, int minimumSitesForDagToBeRoutable)
			{
				int num = 0;
				HashSet<Guid> hashSet = new HashSet<Guid>();
				foreach (RoutingServerInfo routingServerInfo in dag.AllServersNoFallback)
				{
					if (routingServerInfo.IsHubTransportActive)
					{
						num++;
						if (routingServerInfo.ADSite != null)
						{
							hashSet.Add(routingServerInfo.ADSite.ObjectGuid);
						}
					}
				}
				if (num < activeServersForDagToBeRoutable)
				{
					return 0;
				}
				if (hashSet.Count < minimumSitesForDagToBeRoutable)
				{
					return 0;
				}
				return messageThresholdPerServer * num;
			}

			// Token: 0x04000BAE RID: 2990
			private readonly int messageThreshold;

			// Token: 0x04000BAF RID: 2991
			private int messagesDeliveredBasedOnMailbox;

			// Token: 0x04000BB0 RID: 2992
			private int messagesDeliveredBasedOnDagSelector;
		}
	}
}
