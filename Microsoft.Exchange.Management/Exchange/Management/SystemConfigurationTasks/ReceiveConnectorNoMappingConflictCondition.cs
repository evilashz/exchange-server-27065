using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x02000B41 RID: 2881
	internal static class ReceiveConnectorNoMappingConflictCondition
	{
		// Token: 0x060068A5 RID: 26789 RVA: 0x001AF234 File Offset: 0x001AD434
		public static bool Verify(ReceiveConnector connectorBeingAddedOrEdited, IConfigurationSession session, out LocalizedException exception)
		{
			ArgumentValidator.ThrowIfNull("connectorBeginAddedOrEdited", connectorBeingAddedOrEdited);
			ArgumentValidator.ThrowIfNull("session", session);
			IEnumerable<ReceiveConnector> enumerable = session.Find<ReceiveConnector>(connectorBeingAddedOrEdited.Id.Parent, QueryScope.OneLevel, null, null, 0);
			foreach (ReceiveConnector receiveConnector in enumerable)
			{
				if (!connectorBeingAddedOrEdited.Identity.Equals(receiveConnector.Identity))
				{
					bool flag = false;
					bool flag2 = false;
					foreach (IPBinding ipbinding in connectorBeingAddedOrEdited.Bindings)
					{
						if (flag2)
						{
							break;
						}
						foreach (IPBinding ipbinding2 in receiveConnector.Bindings)
						{
							if (ipbinding.Equals(ipbinding2))
							{
								flag = true;
								flag2 = true;
								break;
							}
							if (ipbinding.AddressFamily == ipbinding2.AddressFamily)
							{
								if ((ipbinding.Address.Equals(IPAddress.Any) || ipbinding2.Address.Equals(IPAddress.Any)) && ipbinding.Port.Equals(ipbinding2.Port))
								{
									flag2 = true;
									break;
								}
								if ((ipbinding.Address.Equals(IPAddress.IPv6Any) || ipbinding2.Address.Equals(IPAddress.IPv6Any)) && ipbinding.Port.Equals(ipbinding2.Port))
								{
									flag2 = true;
									break;
								}
							}
						}
					}
					if (flag2)
					{
						if (connectorBeingAddedOrEdited.TransportRole != receiveConnector.TransportRole)
						{
							exception = new ReceiveConnectorRoleConflictException(receiveConnector.Id.ToString());
							return false;
						}
						if (flag)
						{
							if (ReceiveConnectorNoMappingConflictCondition.PartiallyOverlapOrEqual(connectorBeingAddedOrEdited.RemoteIPRanges, receiveConnector.RemoteIPRanges))
							{
								exception = new ConnectorMappingConflictException(receiveConnector.Id.ToString());
								return false;
							}
						}
						else if (ReceiveConnectorNoMappingConflictCondition.PartiallyOverlap(connectorBeingAddedOrEdited.RemoteIPRanges, receiveConnector.RemoteIPRanges))
						{
							exception = new ConnectorMappingConflictException(receiveConnector.Id.ToString());
							return false;
						}
					}
				}
			}
			exception = null;
			return true;
		}

		// Token: 0x060068A6 RID: 26790 RVA: 0x001AF4D4 File Offset: 0x001AD6D4
		private static bool PartiallyOverlapOrEqual(IEnumerable<IPRange> ranges1, MultiValuedProperty<IPRange> ranges2)
		{
			using (IEnumerator<IPRange> enumerator = ranges1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPRange range1 = enumerator.Current;
					if (ranges2.Any((IPRange range) => range1.Equals(range) || range1.PartiallyOverlaps(range)))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x001AF55C File Offset: 0x001AD75C
		private static bool PartiallyOverlap(IEnumerable<IPRange> ranges1, MultiValuedProperty<IPRange> ranges2)
		{
			using (IEnumerator<IPRange> enumerator = ranges1.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IPRange range1 = enumerator.Current;
					if (ranges2.Any((IPRange range) => range1.PartiallyOverlaps(range)))
					{
						return true;
					}
				}
			}
			return false;
		}
	}
}
