﻿using System;
using System.Collections.Generic;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.HA;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Win32;

namespace Microsoft.Exchange.Server.Storage.HA
{
	// Token: 0x0200001F RID: 31
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class ThrottlingUpdater : TimerComponent
	{
		// Token: 0x060000EB RID: 235 RVA: 0x0000673C File Offset: 0x0000493C
		internal ThrottlingUpdater(Guid dbGuid) : base(ThrottlingUpdater.throttlingUpdateInterval, ThrottlingUpdater.throttlingUpdateInterval, "ThrottlingUpdater")
		{
			this.dbGuid = dbGuid;
			this.throttlingData = new ThrottlingData();
			this.throttlingData.MarkHealthy();
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00006792 File Offset: 0x00004992
		internal static Trace Tracer
		{
			get
			{
				return ExTraceGlobals.BlockModeCollectorTracer;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00006799 File Offset: 0x00004999
		internal ThrottlingData ThrottlingData
		{
			get
			{
				return this.throttlingData;
			}
		}

		// Token: 0x060000EE RID: 238 RVA: 0x000067A4 File Offset: 0x000049A4
		internal static int ReadThrottlingUpdaterIntervalMsec()
		{
			IRegistryReader instance = RegistryReader.Instance;
			return instance.GetValue<int>(Registry.LocalMachine, "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "ThrottlingUpdaterIntervalMsec", 15000);
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000067D4 File Offset: 0x000049D4
		internal static int ReadPassiveAcknowledgementTTLInMsec()
		{
			IRegistryReader instance = RegistryReader.Instance;
			return instance.GetValue<int>(Registry.LocalMachine, "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "PassiveAcknowledgementTTLInMsec", 30000);
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00006804 File Offset: 0x00004A04
		internal static bool ReadVerboseLoggingFlag()
		{
			IRegistryReader instance = RegistryReader.Instance;
			int value = instance.GetValue<int>(Registry.LocalMachine, "Software\\Microsoft\\ExchangeServer\\v15\\Replay\\Parameters", "EnableVerboseThrottlingLogging", 0);
			return value == 1;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00006838 File Offset: 0x00004A38
		internal void TriggerUpdate(LogCopyStatus activeCopyLogGenerationInfo, List<LogCopyStatus> passiveCopiesLogGenerationInfo, DateTime? previousLogGeneratedTimeUtc)
		{
			using (LockManager.Lock(this.lockObject, LockManager.LockType.LeafMonitorLock))
			{
				this.activeCopyLogGenerationInfo = activeCopyLogGenerationInfo;
				this.passiveCopiesLogGenerationInfo = passiveCopiesLogGenerationInfo;
				this.previousLogGeneratedTimeUtc = previousLogGeneratedTimeUtc;
				this.doUpdate = true;
			}
			base.StartNow();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006898 File Offset: 0x00004A98
		protected override void TimerCallbackInternal()
		{
			if (this.doUpdate)
			{
				this.ThrottlingDataUpdate();
			}
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000068A8 File Offset: 0x00004AA8
		private void ThrottlingDataUpdate()
		{
			if (this.doUpdate)
			{
				if (this.previousLogGeneratedTimeUtc == null)
				{
					this.throttlingData.MarkHealthy();
					ThrottlingUpdater.Tracer.TraceDebug<Guid>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): First throttling update, data has not been collected yet.", this.dbGuid);
					if (ThrottlingUpdater.verboseLoggingEnabled)
					{
						ReplayCrimsonEvents.ReplicationThrottlingFirstUpdate.Log<string>(this.dbGuid.ToString());
						return;
					}
				}
				else
				{
					DateTime t = this.previousLogGeneratedTimeUtc.Value.Subtract(ThrottlingUpdater.passiveAcknowledgementTTL);
					ThrottlingUpdater.DataMoveReplicationConstraintParameter dataMoveReplicationConstraintParameter = ThrottlingUpdater.DataMoveReplicationConstraintParameter.SecondCopy;
					string text = string.Empty;
					using (Context context = Context.CreateForSystem())
					{
						Exception ex = null;
						try
						{
							DatabaseInfo databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, this.dbGuid);
							text = databaseInfo.MdbName;
							dataMoveReplicationConstraintParameter = (ThrottlingUpdater.DataMoveReplicationConstraintParameter)(databaseInfo.DataMoveReplicationConstraint & 65535);
							if (databaseInfo.HostServerNames.Length <= 1 && dataMoveReplicationConstraintParameter != ThrottlingUpdater.DataMoveReplicationConstraintParameter.None)
							{
								ThrottlingUpdater.Tracer.TraceError<string, ThrottlingUpdater.DataMoveReplicationConstraintParameter>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Database is not replicated, yet constraint is not set to None in AD. It is set to {1}.", text, dataMoveReplicationConstraintParameter);
								ReplayCrimsonEvents.ReplicationThrottlingInvalidConstraintOnANonReplicatedDatabase.Log<string, ThrottlingUpdater.DataMoveReplicationConstraintParameter>(text, dataMoveReplicationConstraintParameter);
								dataMoveReplicationConstraintParameter = ThrottlingUpdater.DataMoveReplicationConstraintParameter.None;
							}
						}
						catch (DatabaseNotFoundException ex2)
						{
							context.OnExceptionCatch(ex2);
							ex = ex2;
						}
						catch (DirectoryTransientErrorException ex3)
						{
							context.OnExceptionCatch(ex3);
							ex = ex3;
						}
						catch (DirectoryPermanentErrorException ex4)
						{
							context.OnExceptionCatch(ex4);
							ex = ex4;
						}
						if (ex != null)
						{
							ThrottlingUpdater.Tracer.TraceError<Guid, string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Database cannot be read from AD. Error: {1}", this.dbGuid, ex.Message);
							text = this.dbGuid.ToString();
							dataMoveReplicationConstraintParameter = ThrottlingUpdater.DataMoveReplicationConstraintParameter.SecondDatacenter;
							ReplayCrimsonEvents.ReplicationThrottlingDatabaseIsNotFoundInAD.Log<string, string>(text, ex.Message);
						}
					}
					List<LogCopyStatus> list = null;
					switch (dataMoveReplicationConstraintParameter)
					{
					case ThrottlingUpdater.DataMoveReplicationConstraintParameter.None:
						ThrottlingUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Constraint is set to None, no copies added for consideration.", text);
						goto IL_2FF;
					case ThrottlingUpdater.DataMoveReplicationConstraintParameter.SecondCopy:
						ThrottlingUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Selecting passive client statuses based on SecondCopy constraint.", text);
						list = this.passiveCopiesLogGenerationInfo;
						ThrottlingUpdater.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): All {1} passive copie(s) added for consideration.", text, this.passiveCopiesLogGenerationInfo.Count);
						goto IL_2FF;
					case ThrottlingUpdater.DataMoveReplicationConstraintParameter.SecondDatacenter:
						ThrottlingUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Selecting passive client statuses based on SecondDatacenter constraint.", text);
						list = new List<LogCopyStatus>(this.passiveCopiesLogGenerationInfo.Count);
						foreach (LogCopyStatus logCopyStatus in this.passiveCopiesLogGenerationInfo)
						{
							if (logCopyStatus.IsCrossSite)
							{
								list.Add(logCopyStatus);
								ThrottlingUpdater.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Passive copy on server '{1}' is going to be considered for best copy selection, because it's AD site is not the same as active copy AD site.", text, logCopyStatus.NodeName);
							}
							else
							{
								ThrottlingUpdater.Tracer.TraceDebug<string, string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Passive copy on server '{1}' is not going to be considered for best copy selection, because it's AD site is the same as active copy AD site.", text, logCopyStatus.NodeName);
							}
						}
						ThrottlingUpdater.Tracer.TraceDebug<string, int>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): {1} passive copie(s) added for consideration.", text, list.Count);
						goto IL_2FF;
					}
					ThrottlingUpdater.Tracer.TraceError<string, ThrottlingUpdater.DataMoveReplicationConstraintParameter>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): Constraint {1} is not supported.", text, dataMoveReplicationConstraintParameter);
					ReplayCrimsonEvents.ReplicationThrottlingInvalidConstraintOnAReplicatedDatabase.Log<string, ThrottlingUpdater.DataMoveReplicationConstraintParameter>(text, dataMoveReplicationConstraintParameter);
					IL_2FF:
					if (dataMoveReplicationConstraintParameter == ThrottlingUpdater.DataMoveReplicationConstraintParameter.None)
					{
						ThrottlingUpdater.Tracer.TraceDebug<string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): database is not throttled.", text);
						if (ThrottlingUpdater.verboseLoggingEnabled)
						{
							ReplayCrimsonEvents.ReplicationThrottlingDatabaseIsNotThrottled.Log<string>(text);
						}
						this.throttlingData.MarkHealthy();
					}
					else
					{
						LogCopyStatus logCopyStatus2 = null;
						List<LogCopyStatus> list2 = new List<LogCopyStatus>(4);
						if (list != null)
						{
							foreach (LogCopyStatus logCopyStatus3 in list)
							{
								if (logCopyStatus3.LogGeneration > this.activeCopyLogGenerationInfo.LogGeneration)
								{
									ThrottlingUpdater.Tracer.TraceError((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): last log generation number 0x{1:X} received by the passive copy on server '{2}' is higher than current active copy generation number {3}.", new object[]
									{
										text,
										logCopyStatus3.LogGeneration,
										logCopyStatus3.NodeName,
										this.activeCopyLogGenerationInfo.LogGeneration
									});
									ReplayCrimsonEvents.ReplicationThrottlingPassiveCopyGenerationIsHigher.Log<string, ulong, string, ulong>(text, logCopyStatus3.LogGeneration, logCopyStatus3.NodeName, this.activeCopyLogGenerationInfo.LogGeneration);
								}
								else if (logCopyStatus3.TimeReceivedUTC < t)
								{
									ThrottlingUpdater.Tracer.TraceError((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): last log generation number 0x{1:X} acknowledgement sent by the passive copy on server '{2}' is stale. It was last received at {3}UTC.", new object[]
									{
										text,
										logCopyStatus3.LogGeneration,
										logCopyStatus3.NodeName,
										logCopyStatus3.TimeReceivedUTC
									});
									ReplayCrimsonEvents.ReplicationThrottlingPassiveCopyGenerationIsStale.Log<string, ulong, string, DateTime>(text, logCopyStatus3.LogGeneration, logCopyStatus3.NodeName, logCopyStatus3.TimeReceivedUTC);
								}
								else
								{
									ulong num = Math.Max(0UL, this.activeCopyLogGenerationInfo.LogGeneration - logCopyStatus3.InspectedLogGeneration);
									ulong num2 = Math.Max(0UL, logCopyStatus3.InspectedLogGeneration - logCopyStatus3.ReplayedLogGeneration);
									if (num <= 50UL && num2 <= 1000UL)
									{
										list2.Add(logCopyStatus3);
									}
									else
									{
										ThrottlingUpdater.Tracer.TraceError((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): skipping copy on server '{1}' because copy queue or replay queue are too large. Copy queue is: {2} and replay queue is: {3}.", new object[]
										{
											text,
											logCopyStatus3.NodeName,
											num,
											num2
										});
										ReplayCrimsonEvents.ReplicationThrottlingPassiveCopyTooFarBehind.LogPeriodic<string, string, ulong, ulong>(text, TimeSpan.FromMinutes(30.0), text, logCopyStatus3.NodeName, num, num2);
									}
								}
							}
						}
						if (list2.Count >= 1)
						{
							ThrottlingUpdater.Tracer.TraceDebug((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): found {1} copies with copy queue less than {2} and replay queue less than {3}.", new object[]
							{
								text,
								list2.Count,
								50U,
								1000U
							});
							ulong num3 = ulong.MaxValue;
							foreach (LogCopyStatus logCopyStatus4 in list2)
							{
								ulong num4 = Math.Max(0UL, this.activeCopyLogGenerationInfo.LogGeneration - logCopyStatus4.InspectedLogGeneration);
								ulong num5 = Math.Max(0UL, logCopyStatus4.InspectedLogGeneration - logCopyStatus4.ReplayedLogGeneration);
								ulong num6 = 34UL * num4 + num5;
								if (num6 < num3)
								{
									num3 = num6;
									logCopyStatus2 = logCopyStatus4;
								}
							}
						}
						if (logCopyStatus2 == null)
						{
							ThrottlingUpdater.Tracer.TraceError<string>((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): best copy could not be found.", text);
							ReplayCrimsonEvents.ReplicationThrottlingBestCopyWasNotFound.Log<string>(text);
							this.throttlingData.MarkFailed();
						}
						else
						{
							ThrottlingUpdater.Tracer.TraceDebug((long)this.GetHashCode(), "ThrottlingDataUpdate('{0}'): most up-to-date copy is on server '{1}'. It's last log copied number is 0x{2:X} and it was acknowledged at {3}UTC. Last inspected log number is 0x{4:X} and last replayed log number is 0x{5:X}.", new object[]
							{
								text,
								logCopyStatus2.NodeName,
								logCopyStatus2.LogGeneration,
								logCopyStatus2.TimeReceivedUTC,
								logCopyStatus2.InspectedLogGeneration,
								logCopyStatus2.ReplayedLogGeneration
							});
							if (ThrottlingUpdater.verboseLoggingEnabled)
							{
								ReplayCrimsonEvents.ReplicationThrottlingBestCopyWasFound.Log<string, string, ulong, DateTime, ulong, ulong>(text, logCopyStatus2.NodeName, logCopyStatus2.LogGeneration, logCopyStatus2.TimeReceivedUTC, logCopyStatus2.InspectedLogGeneration, logCopyStatus2.ReplayedLogGeneration);
							}
							ulong num7 = (this.activeCopyLogGenerationInfo.LogGeneration - logCopyStatus2.LogGeneration) * 1048576UL;
							ulong num8 = (logCopyStatus2.InspectedLogGeneration - logCopyStatus2.ReplayedLogGeneration) * 1048576UL;
							this.throttlingData.Update(num7, num8);
							if (ThrottlingUpdater.verboseLoggingEnabled)
							{
								ReplayCrimsonEvents.ReplicationThrottlingData.Log<string, ulong, ulong>(text, num7, num8);
							}
						}
					}
					this.doUpdate = false;
				}
			}
		}

		// Token: 0x04000090 RID: 144
		private const uint CopyQueueWeight = 34U;

		// Token: 0x04000091 RID: 145
		private static TimeSpan passiveAcknowledgementTTL = TimeSpan.FromMilliseconds((double)ThrottlingUpdater.ReadPassiveAcknowledgementTTLInMsec());

		// Token: 0x04000092 RID: 146
		private static TimeSpan throttlingUpdateInterval = TimeSpan.FromMilliseconds((double)ThrottlingUpdater.ReadThrottlingUpdaterIntervalMsec());

		// Token: 0x04000093 RID: 147
		private static bool verboseLoggingEnabled = ThrottlingUpdater.ReadVerboseLoggingFlag();

		// Token: 0x04000094 RID: 148
		private readonly Guid dbGuid;

		// Token: 0x04000095 RID: 149
		private object lockObject = new object();

		// Token: 0x04000096 RID: 150
		private bool doUpdate;

		// Token: 0x04000097 RID: 151
		private LogCopyStatus activeCopyLogGenerationInfo;

		// Token: 0x04000098 RID: 152
		private List<LogCopyStatus> passiveCopiesLogGenerationInfo;

		// Token: 0x04000099 RID: 153
		private ThrottlingData throttlingData;

		// Token: 0x0400009A RID: 154
		private DateTime? previousLogGeneratedTimeUtc = null;

		// Token: 0x02000020 RID: 32
		private enum DataMoveReplicationConstraintParameter
		{
			// Token: 0x0400009C RID: 156
			None,
			// Token: 0x0400009D RID: 157
			SecondCopy,
			// Token: 0x0400009E RID: 158
			SecondDatacenter = 3
		}
	}
}
