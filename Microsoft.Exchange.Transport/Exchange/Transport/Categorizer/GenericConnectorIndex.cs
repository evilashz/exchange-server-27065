using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x02000236 RID: 566
	internal class GenericConnectorIndex : ConnectorIndex
	{
		// Token: 0x060018EB RID: 6379 RVA: 0x00065498 File Offset: 0x00063698
		public GenericConnectorIndex(string addressType, DateTime timestamp) : base(timestamp)
		{
			this.addressType = addressType;
			this.entries = new Dictionary<string, GenericConnectorIndex.IndexEntry>();
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x000654B4 File Offset: 0x000636B4
		public override ConnectorMatchResult TryFindBestConnector(string address, long messageSize, out ConnectorRoutingDestination connectorDestination)
		{
			connectorDestination = null;
			int num = -1;
			bool flag = false;
			ConnectorIndex.ConnectorWithCost connectorWithCost = null;
			foreach (GenericConnectorIndex.IndexEntry indexEntry in this.entries.Values)
			{
				int num2 = indexEntry.Pattern.Match(address);
				if (num2 != -1 && num2 >= num)
				{
					ConnectorIndex.ConnectorWithCost connectorWithCost2;
					ConnectorMatchResult connectorMatchResult = indexEntry.TryGetConnectorForMessageSize(messageSize, this.Timestamp, this.addressType, address, out connectorWithCost2);
					if (connectorMatchResult == ConnectorMatchResult.Success)
					{
						if (num == num2)
						{
							if (connectorWithCost.CompareTo(connectorWithCost2) <= 0)
							{
								continue;
							}
						}
						else
						{
							num = num2;
						}
						connectorWithCost = connectorWithCost2;
						if (address.Length + 1 == num2)
						{
							break;
						}
					}
					else if (connectorMatchResult == ConnectorMatchResult.MaxMessageSizeExceeded)
					{
						flag = true;
					}
				}
			}
			if (connectorWithCost != null)
			{
				connectorDestination = connectorWithCost.ConnectorDestination;
				return ConnectorMatchResult.Success;
			}
			if (!flag)
			{
				return ConnectorMatchResult.NoAddressMatch;
			}
			return ConnectorMatchResult.MaxMessageSizeExceeded;
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00065584 File Offset: 0x00063784
		public override void AddConnector(AddressSpace addressSpace, ConnectorRoutingDestination connectorDestination)
		{
			WildcardPattern wildcardPattern = new WildcardPattern(addressSpace.Address);
			RoutingDiag.Tracer.TraceDebug((long)this.GetHashCode(), "[{0}] Normalized non-SMTP address space '{1}' to '{2}' (pattern type is {3}) for connector {4}.", new object[]
			{
				this.Timestamp,
				addressSpace.Address,
				wildcardPattern.Pattern,
				wildcardPattern.Type,
				connectorDestination.StringIdentity
			});
			GenericConnectorIndex.IndexEntry indexEntry;
			if (!this.entries.TryGetValue(wildcardPattern.Pattern, out indexEntry))
			{
				indexEntry = new GenericConnectorIndex.IndexEntry(wildcardPattern);
				this.entries.Add(wildcardPattern.Pattern, indexEntry);
				RoutingDiag.Tracer.TraceDebug<DateTime, WildcardPattern>((long)this.GetHashCode(), "[{0}] Added entry '{1}' into generic connector index.", this.Timestamp, wildcardPattern);
			}
			indexEntry.AddConnector(connectorDestination, addressSpace);
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00065650 File Offset: 0x00063850
		public override bool Match(ConnectorIndex other)
		{
			GenericConnectorIndex genericConnectorIndex = other as GenericConnectorIndex;
			if (genericConnectorIndex == null)
			{
				return false;
			}
			return RoutingUtils.MatchDictionaries<string, GenericConnectorIndex.IndexEntry>(this.entries, genericConnectorIndex.entries, (GenericConnectorIndex.IndexEntry entry1, GenericConnectorIndex.IndexEntry entry2) => entry1.Match(entry2));
		}

		// Token: 0x04000C00 RID: 3072
		private string addressType;

		// Token: 0x04000C01 RID: 3073
		private Dictionary<string, GenericConnectorIndex.IndexEntry> entries;

		// Token: 0x02000237 RID: 567
		private class IndexEntry
		{
			// Token: 0x060018F0 RID: 6384 RVA: 0x00065697 File Offset: 0x00063897
			public IndexEntry(WildcardPattern pattern)
			{
				this.pattern = pattern;
				this.connectors = new List<ConnectorIndex.ConnectorWithCost>();
			}

			// Token: 0x17000688 RID: 1672
			// (get) Token: 0x060018F1 RID: 6385 RVA: 0x000656B1 File Offset: 0x000638B1
			public WildcardPattern Pattern
			{
				get
				{
					return this.pattern;
				}
			}

			// Token: 0x060018F2 RID: 6386 RVA: 0x000656B9 File Offset: 0x000638B9
			public void AddConnector(ConnectorRoutingDestination connectorDestination, AddressSpace addressSpace)
			{
				ConnectorIndex.ConnectorWithCost.InsertConnector(connectorDestination, addressSpace, this.connectors);
			}

			// Token: 0x060018F3 RID: 6387 RVA: 0x000656C8 File Offset: 0x000638C8
			public ConnectorMatchResult TryGetConnectorForMessageSize(long messageSize, DateTime timestamp, string addressType, string address, out ConnectorIndex.ConnectorWithCost connectorWithCost)
			{
				connectorWithCost = null;
				int connectorForMessageSize = ConnectorIndex.ConnectorWithCost.GetConnectorForMessageSize(this.connectors, messageSize, timestamp, addressType, address);
				if (connectorForMessageSize < 0)
				{
					return ConnectorMatchResult.MaxMessageSizeExceeded;
				}
				connectorWithCost = this.connectors[connectorForMessageSize];
				return ConnectorMatchResult.Success;
			}

			// Token: 0x060018F4 RID: 6388 RVA: 0x00065700 File Offset: 0x00063900
			public bool Match(GenericConnectorIndex.IndexEntry other)
			{
				return ConnectorIndex.ConnectorWithCost.MatchLists(this.connectors, other.connectors);
			}

			// Token: 0x04000C03 RID: 3075
			private WildcardPattern pattern;

			// Token: 0x04000C04 RID: 3076
			private List<ConnectorIndex.ConnectorWithCost> connectors;
		}
	}
}
