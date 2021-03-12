using System;
using System.Collections.Generic;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Storage.Cluster;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x0200088B RID: 2187
	internal static class DagNetworkValidation
	{
		// Token: 0x06004C09 RID: 19465 RVA: 0x0013BB7C File Offset: 0x00139D7C
		internal static void ValidateSwitches(DatabaseAvailabilityGroupNetwork net, Task.TaskErrorLoggingDelegate writeError)
		{
			if (net.IsModified(DatabaseAvailabilityGroupNetworkSchema.ReplicationEnabled) && net.IsModified(DatabaseAvailabilityGroupNetworkSchema.IgnoreNetwork))
			{
				if (net.IgnoreNetwork && net.ReplicationEnabled)
				{
					writeError(new DagNetworkInconsistentRoleException(), ErrorCategory.InvalidArgument, null);
					return;
				}
			}
			else
			{
				if (net.IsModified(DatabaseAvailabilityGroupNetworkSchema.ReplicationEnabled) && net.ReplicationEnabled)
				{
					net.IgnoreNetwork = false;
				}
				if (net.IsModified(DatabaseAvailabilityGroupNetworkSchema.IgnoreNetwork) && net.IgnoreNetwork)
				{
					net.ReplicationEnabled = false;
				}
			}
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0013BBFC File Offset: 0x00139DFC
		internal static void ValidateSubnets(IEnumerable<DatabaseAvailabilityGroupSubnetId> subnets, DagNetworkConfiguration netConfig, string networkName, DatabaseAvailabilityGroupNetwork networkBeingChanged, Task.TaskErrorLoggingDelegate writeError, Task.TaskWarningLoggingDelegate writeWarning, Task.TaskVerboseLoggingDelegate writeVerbose)
		{
			SortedList<DatabaseAvailabilityGroupSubnetId, object> sortedList = new SortedList<DatabaseAvailabilityGroupSubnetId, object>(DagSubnetIdComparer.Comparer);
			foreach (DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId in subnets)
			{
				int num = sortedList.IndexOfKey(databaseAvailabilityGroupSubnetId);
				if (num >= 0)
				{
					writeError(new DagNetworkDistinctSubnetListException(databaseAvailabilityGroupSubnetId.ToString(), sortedList.Keys[num].ToString()), ErrorCategory.InvalidArgument, null);
				}
				sortedList.Add(databaseAvailabilityGroupSubnetId, null);
			}
			foreach (DatabaseAvailabilityGroupSubnetId databaseAvailabilityGroupSubnetId2 in subnets)
			{
				DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork;
				DatabaseAvailabilityGroupNetworkSubnet databaseAvailabilityGroupNetworkSubnet;
				if (!netConfig.FindSubNet(databaseAvailabilityGroupSubnetId2, out databaseAvailabilityGroupNetwork, out databaseAvailabilityGroupNetworkSubnet))
				{
					writeWarning(Strings.DagNetworkUnknownSubnetWarning(databaseAvailabilityGroupSubnetId2.ToString()));
				}
				else
				{
					if (databaseAvailabilityGroupNetworkSubnet.State != DatabaseAvailabilityGroupNetworkSubnet.SubnetState.Unknown && !databaseAvailabilityGroupSubnetId2.IPRange.Equals(databaseAvailabilityGroupNetworkSubnet.SubnetId.IPRange))
					{
						writeError(new DagNetworkSubnetIdConflictException(databaseAvailabilityGroupSubnetId2.ToString(), databaseAvailabilityGroupNetworkSubnet.SubnetId.ToString()), ErrorCategory.InvalidArgument, null);
					}
					if (databaseAvailabilityGroupNetwork != networkBeingChanged)
					{
						ExTraceGlobals.CmdletsTracer.TraceError<DatabaseAvailabilityGroupSubnetId, string, string>(0L, "Subnet {0} is moving from network {1} to {2}", databaseAvailabilityGroupSubnetId2, databaseAvailabilityGroupNetwork.Name, networkName);
						if (writeVerbose != null)
						{
							writeVerbose(Strings.DagNetworkSubnetMoving(databaseAvailabilityGroupSubnetId2.ToString(), databaseAvailabilityGroupNetwork.Name, networkName));
						}
					}
				}
			}
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0013BD64 File Offset: 0x00139F64
		internal static bool AreAllNetsDisabled(DagNetworkConfiguration netConfig)
		{
			foreach (DatabaseAvailabilityGroupNetwork databaseAvailabilityGroupNetwork in netConfig.Networks)
			{
				if (databaseAvailabilityGroupNetwork.ReplicationEnabled)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0013BD99 File Offset: 0x00139F99
		internal static void WarnIfAllNetsAreDisabled(DagNetworkConfiguration netConfig, Task.TaskWarningLoggingDelegate writeWarning)
		{
			if (DagNetworkValidation.AreAllNetsDisabled(netConfig))
			{
				writeWarning(Strings.DagNetworkAllDisabledWarning);
			}
		}
	}
}
