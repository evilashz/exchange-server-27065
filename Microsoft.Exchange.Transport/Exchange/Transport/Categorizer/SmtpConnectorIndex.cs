using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200026B RID: 619
	internal class SmtpConnectorIndex : ConnectorIndex
	{
		// Token: 0x06001B19 RID: 6937 RVA: 0x0006F1F1 File Offset: 0x0006D3F1
		public SmtpConnectorIndex(DateTime timestamp) : base(timestamp)
		{
			this.table = new Dictionary<DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString, SmtpConnectorIndex.SmtpIndexNode>();
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x0006F208 File Offset: 0x0006D408
		public override ConnectorMatchResult TryFindBestConnector(string address, long messageSize, out ConnectorRoutingDestination connectorDestination)
		{
			connectorDestination = null;
			RoutingUtils.ThrowIfNullOrEmpty(address, "address");
			SmtpDomain smtpDomain = null;
			if (!SmtpDomain.TryParse(address, out smtpDomain))
			{
				return ConnectorMatchResult.InvalidSmtpAddress;
			}
			ConnectorMatchResult connectorMatchResult = ConnectorMatchResult.NoAddressMatch;
			DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString subString = new DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString(address, 0);
			SmtpConnectorIndex.SmtpIndexNode smtpIndexNode;
			if (this.table.TryGetValue(subString, out smtpIndexNode))
			{
				connectorMatchResult = smtpIndexNode.TryGetConnector(messageSize, this.Timestamp, address, out connectorDestination);
			}
			if (connectorMatchResult != ConnectorMatchResult.Success)
			{
				int[] array = DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.FindAllDots(address);
				int num = (array.Length > this.maxDots) ? (array.Length - this.maxDots) : 0;
				while (num < array.Length && connectorMatchResult != ConnectorMatchResult.Success)
				{
					subString.SetIndex(array[num] + 1);
					if (this.table.TryGetValue(subString, out smtpIndexNode))
					{
						connectorMatchResult = smtpIndexNode.TryGetConnector(messageSize, this.Timestamp, address, out connectorDestination);
					}
					num++;
				}
			}
			if (connectorMatchResult != ConnectorMatchResult.Success && this.star != null)
			{
				connectorMatchResult = this.star.TryGetConnector(messageSize, this.Timestamp, address, out connectorDestination);
			}
			return connectorMatchResult;
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x0006F2E4 File Offset: 0x0006D4E4
		public override void AddConnector(AddressSpace addressSpace, ConnectorRoutingDestination connectorDestination)
		{
			if (addressSpace.DomainWithSubdomains.SmtpDomain == null)
			{
				if (this.star == null)
				{
					this.star = new SmtpConnectorIndex.SmtpIndexNode(addressSpace);
				}
				this.star.AddConnector(connectorDestination, addressSpace);
				return;
			}
			SmtpConnectorIndex.SmtpIndexNode smtpIndexNode = null;
			DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString key = new DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString(addressSpace.Domain, 0);
			if (!this.table.TryGetValue(key, out smtpIndexNode))
			{
				smtpIndexNode = new SmtpConnectorIndex.SmtpIndexNode(addressSpace);
				this.table[key] = smtpIndexNode;
			}
			smtpIndexNode.AddConnector(connectorDestination, addressSpace);
			int num = DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.CountDots(addressSpace.Domain);
			if (num >= this.maxDots)
			{
				this.maxDots = num + 1;
			}
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x0006F384 File Offset: 0x0006D584
		public override bool Match(ConnectorIndex other)
		{
			SmtpConnectorIndex smtpConnectorIndex = other as SmtpConnectorIndex;
			if (smtpConnectorIndex == null)
			{
				return false;
			}
			if (!RoutingUtils.NullMatch(this.star, smtpConnectorIndex.star) || (this.star != null && !this.star.Match(smtpConnectorIndex.star)))
			{
				return false;
			}
			return RoutingUtils.MatchDictionaries<DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString, SmtpConnectorIndex.SmtpIndexNode>(this.table, smtpConnectorIndex.table, (SmtpConnectorIndex.SmtpIndexNode node1, SmtpConnectorIndex.SmtpIndexNode node2) => node1.Match(node2));
		}

		// Token: 0x04000CCC RID: 3276
		private readonly Dictionary<DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.SubString, SmtpConnectorIndex.SmtpIndexNode> table;

		// Token: 0x04000CCD RID: 3277
		private SmtpConnectorIndex.SmtpIndexNode star;

		// Token: 0x04000CCE RID: 3278
		private int maxDots;

		// Token: 0x0200026C RID: 620
		private class SmtpIndexNode : DomainMatchMap<SmtpConnectorIndex.SmtpIndexNode>.IDomainEntry
		{
			// Token: 0x06001B1E RID: 6942 RVA: 0x0006F3FB File Offset: 0x0006D5FB
			public SmtpIndexNode(AddressSpace addressSpace)
			{
				this.domainWithSubdomains = addressSpace.DomainWithSubdomains;
				this.connectors = new List<ConnectorIndex.ConnectorWithCost>();
			}

			// Token: 0x17000729 RID: 1833
			// (get) Token: 0x06001B1F RID: 6943 RVA: 0x0006F41A File Offset: 0x0006D61A
			public List<ConnectorIndex.ConnectorWithCost> Connectors
			{
				get
				{
					return this.connectors;
				}
			}

			// Token: 0x1700072A RID: 1834
			// (get) Token: 0x06001B20 RID: 6944 RVA: 0x0006F422 File Offset: 0x0006D622
			public SmtpConnectorIndex.SmtpIndexNode Sibling
			{
				get
				{
					return this.sibling;
				}
			}

			// Token: 0x1700072B RID: 1835
			// (get) Token: 0x06001B21 RID: 6945 RVA: 0x0006F42A File Offset: 0x0006D62A
			public SmtpDomainWithSubdomains DomainName
			{
				get
				{
					return this.domainWithSubdomains;
				}
			}

			// Token: 0x06001B22 RID: 6946 RVA: 0x0006F434 File Offset: 0x0006D634
			public void AddConnector(ConnectorRoutingDestination connectorDestination, AddressSpace addressSpace)
			{
				SmtpConnectorIndex.SmtpIndexNode smtpIndexNode = this;
				if (addressSpace.DomainWithSubdomains.IncludeSubDomains != this.domainWithSubdomains.IncludeSubDomains)
				{
					if (this.sibling == null)
					{
						this.sibling = new SmtpConnectorIndex.SmtpIndexNode(addressSpace);
					}
					smtpIndexNode = this.sibling;
				}
				ConnectorIndex.ConnectorWithCost.InsertConnector(connectorDestination, addressSpace, smtpIndexNode.connectors);
			}

			// Token: 0x06001B23 RID: 6947 RVA: 0x0006F484 File Offset: 0x0006D684
			public ConnectorMatchResult TryGetConnector(long messageSize, DateTime timestamp, string address, out ConnectorRoutingDestination connectorDestination)
			{
				connectorDestination = null;
				SmtpConnectorIndex.SmtpIndexNode smtpIndexNode = this;
				if (this.DomainName.SmtpDomain != null)
				{
					smtpIndexNode = this.GetCorrectNode(address);
					if (smtpIndexNode == null)
					{
						return ConnectorMatchResult.NoAddressMatch;
					}
				}
				return ConnectorIndex.ConnectorWithCost.TryGetConnectorForMessageSize(smtpIndexNode.connectors, messageSize, timestamp, "smtp", address, out connectorDestination);
			}

			// Token: 0x06001B24 RID: 6948 RVA: 0x0006F4C8 File Offset: 0x0006D6C8
			public bool Match(SmtpConnectorIndex.SmtpIndexNode other)
			{
				if (!RoutingUtils.NullMatch(this.sibling, other.sibling))
				{
					return false;
				}
				List<ConnectorIndex.ConnectorWithCost> l;
				List<ConnectorIndex.ConnectorWithCost> l2;
				if (this.domainWithSubdomains.IncludeSubDomains)
				{
					l = this.connectors;
					l2 = ((this.sibling == null) ? null : this.sibling.connectors);
				}
				else
				{
					l2 = this.connectors;
					l = ((this.sibling == null) ? null : this.sibling.connectors);
				}
				List<ConnectorIndex.ConnectorWithCost> l3;
				List<ConnectorIndex.ConnectorWithCost> l4;
				if (other.domainWithSubdomains.IncludeSubDomains)
				{
					l3 = other.connectors;
					l4 = ((other.sibling == null) ? null : other.sibling.connectors);
				}
				else
				{
					l4 = other.connectors;
					l3 = ((other.sibling == null) ? null : other.sibling.connectors);
				}
				return ConnectorIndex.ConnectorWithCost.MatchLists(l2, l4) && ConnectorIndex.ConnectorWithCost.MatchLists(l, l3);
			}

			// Token: 0x06001B25 RID: 6949 RVA: 0x0006F594 File Offset: 0x0006D794
			private SmtpConnectorIndex.SmtpIndexNode GetCorrectNode(string address)
			{
				SmtpConnectorIndex.SmtpIndexNode result = this;
				if (address.Equals(this.DomainName.Domain.ToString(), StringComparison.OrdinalIgnoreCase))
				{
					if (this.DomainName.IncludeSubDomains && this.sibling != null)
					{
						result = this.sibling;
					}
				}
				else if (!this.DomainName.IncludeSubDomains)
				{
					result = this.sibling;
				}
				return result;
			}

			// Token: 0x04000CD0 RID: 3280
			private readonly SmtpDomainWithSubdomains domainWithSubdomains;

			// Token: 0x04000CD1 RID: 3281
			private List<ConnectorIndex.ConnectorWithCost> connectors;

			// Token: 0x04000CD2 RID: 3282
			private SmtpConnectorIndex.SmtpIndexNode sibling;
		}
	}
}
