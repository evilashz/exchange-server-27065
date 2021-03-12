using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Transport;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x020004B3 RID: 1203
	internal class ClientIPTable
	{
		// Token: 0x06003651 RID: 13905 RVA: 0x000DE7F4 File Offset: 0x000DC9F4
		public ClientData Add(IPAddress address, out int totalConnections)
		{
			ArgumentValidator.ThrowIfNull("address", address);
			totalConnections = Interlocked.Increment(ref this.connectionCount);
			ClientData result;
			lock (((ICollection)this.table).SyncRoot)
			{
				ClientData clientData;
				if (this.table.TryGetValue(address, out clientData))
				{
					clientData.Count++;
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<IPAddress, int, bool>((long)address.GetHashCode(), "Client IP entry for {0} found: count={1} discredited={2}", address, clientData.Count, clientData.Discredited);
					result = clientData;
				}
				else
				{
					clientData = new ClientData();
					clientData.Count = 1;
					this.table.Add(address, clientData);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<IPAddress>((long)address.GetHashCode(), "Client IP entry for {0} not found: created", address);
					result = clientData;
				}
			}
			return result;
		}

		// Token: 0x06003652 RID: 13906 RVA: 0x000DE8C4 File Offset: 0x000DCAC4
		public ClientData Add(IPAddress address, ulong significantAddressBytes, out int totalConnections)
		{
			ArgumentValidator.ThrowIfNull("address", address);
			totalConnections = Interlocked.Increment(ref this.connectionCount);
			ClientData result;
			lock (((ICollection)this.table).SyncRoot)
			{
				ClientData clientData;
				if (this.ipAddressSignificantBytesTable.TryGetValue(significantAddressBytes, out clientData))
				{
					clientData.Count++;
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug((long)address.GetHashCode(), "Client IP entry for {0} found using its required significant 64 bits {1}: count={2} discredited={3}", new object[]
					{
						address,
						significantAddressBytes,
						clientData.Count,
						clientData.Discredited
					});
					result = clientData;
				}
				else
				{
					clientData = new ClientData();
					clientData.Count = 1;
					this.ipAddressSignificantBytesTable.Add(significantAddressBytes, clientData);
					ExTraceGlobals.SmtpReceiveTracer.TraceDebug<IPAddress, ulong>((long)address.GetHashCode(), "Client IP entry for {0} not found using its required significant 64 bits {1}: created", address, significantAddressBytes);
					result = clientData;
				}
			}
			return result;
		}

		// Token: 0x06003653 RID: 13907 RVA: 0x000DE9C0 File Offset: 0x000DCBC0
		public void Remove(IPAddress address)
		{
			ArgumentValidator.ThrowIfNull("address", address);
			Interlocked.Decrement(ref this.connectionCount);
			lock (((ICollection)this.table).SyncRoot)
			{
				ClientData clientData;
				if (this.table.TryGetValue(address, out clientData))
				{
					clientData.Count--;
					if (clientData.Count == 0)
					{
						this.table.Remove(address);
					}
				}
			}
		}

		// Token: 0x06003654 RID: 13908 RVA: 0x000DEA4C File Offset: 0x000DCC4C
		public void Remove(ulong significantAddressBytes)
		{
			Interlocked.Decrement(ref this.connectionCount);
			lock (((ICollection)this.table).SyncRoot)
			{
				ClientData clientData;
				if (this.ipAddressSignificantBytesTable.TryGetValue(significantAddressBytes, out clientData))
				{
					clientData.Count--;
					if (clientData.Count == 0)
					{
						this.ipAddressSignificantBytesTable.Remove(significantAddressBytes);
					}
				}
			}
		}

		// Token: 0x04001BC9 RID: 7113
		private const int InitialTableSize = 500;

		// Token: 0x04001BCA RID: 7114
		private int connectionCount;

		// Token: 0x04001BCB RID: 7115
		private Dictionary<IPAddress, ClientData> table = new Dictionary<IPAddress, ClientData>(500);

		// Token: 0x04001BCC RID: 7116
		private Dictionary<ulong, ClientData> ipAddressSignificantBytesTable = new Dictionary<ulong, ClientData>();
	}
}
