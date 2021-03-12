using System;
using System.Collections.Generic;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport.Categorizer
{
	// Token: 0x0200027F RID: 639
	internal class X400ConnectorIndex : ConnectorIndex
	{
		// Token: 0x06001B92 RID: 7058 RVA: 0x00070EDD File Offset: 0x0006F0DD
		public X400ConnectorIndex(DateTime timestamp) : base(timestamp)
		{
			this.root = new X400ConnectorIndex.X400IndexNode(null);
		}

		// Token: 0x06001B93 RID: 7059 RVA: 0x00070EF4 File Offset: 0x0006F0F4
		public override ConnectorMatchResult TryFindBestConnector(string address, long messageSize, out ConnectorRoutingDestination connectorDestination)
		{
			RoutingX400Address address2;
			if (!RoutingX400Address.TryParse(address, out address2))
			{
				RoutingDiag.Tracer.TraceError<DateTime, string>((long)this.GetHashCode(), "[{0}] Invalid X400 recipient address '{1}'; no connector matching will be performed.", this.Timestamp, address);
				connectorDestination = null;
				return ConnectorMatchResult.InvalidX400Address;
			}
			return this.root.Search(address2, 0, messageSize, this.Timestamp, address, out connectorDestination);
		}

		// Token: 0x06001B94 RID: 7060 RVA: 0x00070F43 File Offset: 0x0006F143
		public override void AddConnector(AddressSpace addressSpace, ConnectorRoutingDestination connectorDestination)
		{
			this.root.Insert(addressSpace, 0, connectorDestination);
		}

		// Token: 0x06001B95 RID: 7061 RVA: 0x00070F54 File Offset: 0x0006F154
		public override bool Match(ConnectorIndex other)
		{
			X400ConnectorIndex x400ConnectorIndex = other as X400ConnectorIndex;
			return x400ConnectorIndex != null && this.root.Match(x400ConnectorIndex.root);
		}

		// Token: 0x04000D04 RID: 3332
		private X400ConnectorIndex.X400IndexNode root;

		// Token: 0x02000280 RID: 640
		private class X400IndexNode : IComparable<X400ConnectorIndex.X400IndexNode>
		{
			// Token: 0x06001B96 RID: 7062 RVA: 0x00070F7E File Offset: 0x0006F17E
			public X400IndexNode(string componentValue)
			{
				if (componentValue != null)
				{
					this.componentPattern = new WildcardPattern(componentValue);
				}
			}

			// Token: 0x06001B97 RID: 7063 RVA: 0x00070F98 File Offset: 0x0006F198
			public void Insert(AddressSpace addressSpace, int componentIndex, ConnectorRoutingDestination connectorDestination)
			{
				if (componentIndex == addressSpace.X400Address.ComponentsCount)
				{
					this.AddConnector(connectorDestination, addressSpace);
					return;
				}
				X400ConnectorIndex.X400IndexNode x400IndexNode = new X400ConnectorIndex.X400IndexNode(addressSpace.X400Address[componentIndex]);
				if (this.children == null)
				{
					this.children = new List<X400ConnectorIndex.X400IndexNode>();
					this.children.Add(x400IndexNode);
				}
				else
				{
					int num = this.children.BinarySearch(x400IndexNode);
					if (num >= 0)
					{
						x400IndexNode = this.children[num];
					}
					else
					{
						num = ~num;
						this.children.Insert(num, x400IndexNode);
					}
				}
				x400IndexNode.Insert(addressSpace, componentIndex + 1, connectorDestination);
			}

			// Token: 0x06001B98 RID: 7064 RVA: 0x0007102C File Offset: 0x0006F22C
			public ConnectorMatchResult Search(RoutingX400Address address, int componentIndex, long messageSize, DateTime timestamp, string strAddress, out ConnectorRoutingDestination connectorDestination)
			{
				bool flag = false;
				ConnectorMatchResult connectorMatchResult;
				if (this.children != null)
				{
					bool flag2 = componentIndex == 1 && address[componentIndex].Equals(" ", StringComparison.OrdinalIgnoreCase);
					foreach (X400ConnectorIndex.X400IndexNode x400IndexNode in this.children)
					{
						if (flag2 || x400IndexNode.componentPattern.Match(address[componentIndex], '%') >= 0)
						{
							connectorMatchResult = x400IndexNode.Search(address, componentIndex + 1, messageSize, timestamp, strAddress, out connectorDestination);
							if (connectorMatchResult == ConnectorMatchResult.Success)
							{
								return ConnectorMatchResult.Success;
							}
							if (ConnectorMatchResult.MaxMessageSizeExceeded == connectorMatchResult)
							{
								flag = true;
							}
						}
					}
				}
				connectorMatchResult = this.TryGetConnectorForMessageSize(messageSize, timestamp, strAddress, out connectorDestination);
				if (ConnectorMatchResult.NoAddressMatch == connectorMatchResult && flag)
				{
					connectorMatchResult = ConnectorMatchResult.MaxMessageSizeExceeded;
				}
				return connectorMatchResult;
			}

			// Token: 0x06001B99 RID: 7065 RVA: 0x00071104 File Offset: 0x0006F304
			public bool Match(X400ConnectorIndex.X400IndexNode other)
			{
				if (RoutingUtils.NullMatch(this.componentPattern, other.componentPattern) && (this.componentPattern == null || this.componentPattern.Equals(other.componentPattern)) && ConnectorIndex.ConnectorWithCost.MatchLists(this.connectors, other.connectors))
				{
					return RoutingUtils.MatchOrderedLists<X400ConnectorIndex.X400IndexNode>(this.children, other.children, (X400ConnectorIndex.X400IndexNode node1, X400ConnectorIndex.X400IndexNode node2) => node1.Match(node2));
				}
				return false;
			}

			// Token: 0x06001B9A RID: 7066 RVA: 0x00071184 File Offset: 0x0006F384
			int IComparable<X400ConnectorIndex.X400IndexNode>.CompareTo(X400ConnectorIndex.X400IndexNode other)
			{
				bool flag = WildcardPattern.PatternType.Wildcard == this.componentPattern.Type;
				bool flag2 = WildcardPattern.PatternType.Wildcard == other.componentPattern.Type;
				if (!flag && !flag2)
				{
					return string.Compare(this.componentPattern.Pattern, other.componentPattern.Pattern, StringComparison.OrdinalIgnoreCase);
				}
				return flag.CompareTo(flag2);
			}

			// Token: 0x06001B9B RID: 7067 RVA: 0x000711DA File Offset: 0x0006F3DA
			private void AddConnector(ConnectorRoutingDestination connectorDestination, AddressSpace addressSpace)
			{
				if (this.connectors == null)
				{
					this.connectors = new List<ConnectorIndex.ConnectorWithCost>();
				}
				ConnectorIndex.ConnectorWithCost.InsertConnector(connectorDestination, addressSpace, this.connectors);
			}

			// Token: 0x06001B9C RID: 7068 RVA: 0x000711FC File Offset: 0x0006F3FC
			private ConnectorMatchResult TryGetConnectorForMessageSize(long messageSize, DateTime timestamp, string address, out ConnectorRoutingDestination matchingConnector)
			{
				return ConnectorIndex.ConnectorWithCost.TryGetConnectorForMessageSize(this.connectors, messageSize, timestamp, "x400", address, out matchingConnector);
			}

			// Token: 0x04000D05 RID: 3333
			private WildcardPattern componentPattern;

			// Token: 0x04000D06 RID: 3334
			private List<X400ConnectorIndex.X400IndexNode> children;

			// Token: 0x04000D07 RID: 3335
			private List<ConnectorIndex.ConnectorWithCost> connectors;
		}
	}
}
