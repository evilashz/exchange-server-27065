using System;
using System.IO;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Isam.Esent.Interop;

namespace Microsoft.Exchange.Cluster.ReplayService
{
	// Token: 0x02000002 RID: 2
	internal static class ExceptionInjectionCallback
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static Exception ExceptionLookup(string exceptionType)
		{
			Exception result = null;
			if (!string.IsNullOrEmpty(exceptionType))
			{
				if (exceptionType.Equals("System.IO.IOException", StringComparison.OrdinalIgnoreCase))
				{
					result = new IOException();
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Cluster.Replay.NetworkTransportException", StringComparison.OrdinalIgnoreCase))
				{
					result = new NetworkTransportException("Fault injection");
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Cluster.Shared.ClusterException", StringComparison.OrdinalIgnoreCase))
				{
					result = new ClusterException("Fault injection");
				}
				else if (exceptionType.Equals("Microsoft.Isam.Esent.Interop.EsentDatabaseInvalidIncrementalReseedException", StringComparison.OrdinalIgnoreCase))
				{
					result = new EsentDatabaseInvalidIncrementalReseedException();
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Cluster.Shared.AmGetFqdnFailedNotFoundException", StringComparison.OrdinalIgnoreCase))
				{
					result = new AmGetFqdnFailedNotFoundException("Fault injection");
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Cluster.Replay.NetworkCommunicationException", StringComparison.OrdinalIgnoreCase))
				{
					result = new NetworkCommunicationException("Fault node", "Fault injection");
				}
				else if (exceptionType.Equals("Microsoft.Exchange.Data.Storage.AmInvalidConfiguration", StringComparison.OrdinalIgnoreCase))
				{
					result = new AmInvalidConfiguration("Fault injection");
				}
			}
			return result;
		}
	}
}
