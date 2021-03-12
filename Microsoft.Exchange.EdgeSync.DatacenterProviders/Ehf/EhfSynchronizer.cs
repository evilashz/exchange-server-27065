using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.DirectoryServices.Protocols;
using System.ServiceModel;
using System.ServiceModel.Security;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.EdgeSync.Common;
using Microsoft.Exchange.EdgeSync.Datacenter;
using Microsoft.Exchange.HostedServices.AdminCenter.UI.Services;

namespace Microsoft.Exchange.EdgeSync.Ehf
{
	// Token: 0x0200000B RID: 11
	internal class EhfSynchronizer
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003083 File Offset: 0x00001283
		public EhfSynchronizer(EhfTargetConnection ehfConnection)
		{
			this.ehfConnection = ehfConnection;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00003092 File Offset: 0x00001292
		protected EhfTargetConnection EhfConnection
		{
			get
			{
				return this.ehfConnection;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000042 RID: 66 RVA: 0x0000309A File Offset: 0x0000129A
		protected EhfProvisioningService ProvisioningService
		{
			get
			{
				return this.ehfConnection.ProvisioningService;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000030A7 File Offset: 0x000012A7
		protected EhfADAdapter ADAdapter
		{
			get
			{
				return this.ehfConnection.ADAdapter;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000044 RID: 68 RVA: 0x000030B4 File Offset: 0x000012B4
		protected EhfTargetServerConfig Config
		{
			get
			{
				return this.ehfConnection.Config;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000045 RID: 69 RVA: 0x000030C1 File Offset: 0x000012C1
		protected EdgeSyncDiag DiagSession
		{
			get
			{
				return this.ehfConnection.DiagSession;
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000030CE File Offset: 0x000012CE
		public virtual void ClearBatches()
		{
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000030D0 File Offset: 0x000012D0
		public virtual bool FlushBatches()
		{
			return true;
		}

		// Token: 0x06000048 RID: 72 RVA: 0x000030D4 File Offset: 0x000012D4
		protected static bool LoadFullEntry(ExSearchResultEntry entry, string[] attributeNames, EhfTargetConnection ehfConnection)
		{
			ExSearchResultEntry exSearchResultEntry = ehfConnection.ADAdapter.ReadObjectEntry(entry.DistinguishedName, true, attributeNames);
			if (exSearchResultEntry == null)
			{
				ehfConnection.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.Low, "Could not load object with DN <{0}>; ignoring the object", new object[]
				{
					entry.DistinguishedName
				});
				return false;
			}
			foreach (KeyValuePair<string, DirectoryAttribute> keyValuePair in exSearchResultEntry.Attributes)
			{
				if (!entry.Attributes.ContainsKey(keyValuePair.Key))
				{
					entry.Attributes.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			return true;
		}

		// Token: 0x06000049 RID: 73 RVA: 0x0000318C File Offset: 0x0000138C
		protected static T[] CombineArrays<T>(T[] array1, T[] array2)
		{
			T[] array3 = new T[array1.Length + array2.Length];
			array1.CopyTo(array3, 0);
			array2.CopyTo(array3, array1.Length);
			return array3;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000031B9 File Offset: 0x000013B9
		protected bool AddItemToBatch<ItemT>(ItemT item, ref List<ItemT> batch)
		{
			this.AddItemToLazyList<ItemT>(item, ref batch);
			return batch.Count >= this.Config.EhfSyncAppConfig.BatchSize;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000031DF File Offset: 0x000013DF
		protected void AddItemToLazyList<ItemT>(ItemT item, ref List<ItemT> list)
		{
			if (list == null)
			{
				list = new List<ItemT>(this.Config.EhfSyncAppConfig.BatchSize);
			}
			list.Add(item);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003204 File Offset: 0x00001404
		protected void InvokeProvisioningService(string operationName, EhfSynchronizer.ProvisioningServiceCall serviceCall, int numberOfEntries)
		{
			ExEventLog.EventTuple eventTuple = default(ExEventLog.EventTuple);
			Exception ex = null;
			EhfProvisioningService.MessageSecurityExceptionReason messageSecurityExceptionReason = EhfProvisioningService.MessageSecurityExceptionReason.Other;
			this.DiagSession.Tracer.TraceDebug<string>((long)this.DiagSession.GetHashCode(), "Executing EHF provisioning operation {0}", operationName);
			int transientExceptionRetryCount = this.Config.EhfSyncAppConfig.TransientExceptionRetryCount;
			bool flag = true;
			do
			{
				if (ex != null)
				{
					this.LogAndTraceException(operationName, ex, messageSecurityExceptionReason.ToString(), transientExceptionRetryCount + 1);
				}
				try
				{
					Stopwatch stopwatch = new Stopwatch();
					stopwatch.Start();
					serviceCall();
					stopwatch.Stop();
					this.ehfConnection.PerfCounterHandler.OnOperationSuccessfullyCompleted(operationName, stopwatch.ElapsedMilliseconds, numberOfEntries);
					this.DiagSession.LogAndTraceInfo(EdgeSyncLoggingLevel.High, "Successfully executed EHF provisioning operation {0}", new object[]
					{
						operationName
					});
					return;
				}
				catch (FaultException<ServiceFault> faultException)
				{
					ex = faultException;
					ServiceFault detail = faultException.Detail;
					if (detail.Id == FaultId.UnableToConnectToDatabase)
					{
						eventTuple = EdgeSyncEventLogConstants.Tuple_EhfTransientFailure;
						this.ehfConnection.PerfCounterHandler.OnOperationTransientFailure(operationName);
					}
					else
					{
						eventTuple = EdgeSyncEventLogConstants.Tuple_EhfCommunicationFailure;
						this.ehfConnection.PerfCounterHandler.OnOperationCommunicationFailure(operationName);
					}
				}
				catch (MessageSecurityException ex2)
				{
					messageSecurityExceptionReason = EhfProvisioningService.DecodeMessageSecurityException(ex2);
					switch (messageSecurityExceptionReason)
					{
					case EhfProvisioningService.MessageSecurityExceptionReason.DatabaseFailure:
						ex = ex2.InnerException;
						eventTuple = EdgeSyncEventLogConstants.Tuple_EhfTransientFailure;
						this.ehfConnection.PerfCounterHandler.OnOperationTransientFailure(operationName);
						goto IL_186;
					case EhfProvisioningService.MessageSecurityExceptionReason.InvalidCredentials:
						ex = ex2.InnerException;
						eventTuple = EdgeSyncEventLogConstants.Tuple_EhfInvalidCredentials;
						this.ehfConnection.PerfCounterHandler.OnOperationInvalidCredentialsFailure(operationName);
						flag = false;
						goto IL_186;
					}
					ex = ex2;
					eventTuple = EdgeSyncEventLogConstants.Tuple_EhfCommunicationFailure;
					this.ehfConnection.PerfCounterHandler.OnOperationCommunicationFailure(operationName);
					IL_186:;
				}
				catch (CommunicationException ex3)
				{
					ex = ex3;
					eventTuple = EdgeSyncEventLogConstants.Tuple_EhfCommunicationFailure;
					this.ehfConnection.PerfCounterHandler.OnOperationCommunicationFailure(operationName);
				}
				catch (TimeoutException ex4)
				{
					ex = ex4;
					eventTuple = EdgeSyncEventLogConstants.Tuple_EhfOperationTimedOut;
					this.ehfConnection.PerfCounterHandler.OnOperationTimeoutFailure(operationName);
					flag = false;
				}
				catch (EhfProvisioningService.ContractViolationException ex5)
				{
					ex = ex5;
					eventTuple = EdgeSyncEventLogConstants.Tuple_EhfServiceContractViolation;
					this.ehfConnection.PerfCounterHandler.OnOperationContractViolationFailure(operationName);
					flag = false;
				}
			}
			while (flag && transientExceptionRetryCount-- > 0);
			if (ex != null)
			{
				this.EventLogAndTraceException(operationName, eventTuple, ex, messageSecurityExceptionReason.ToString());
				this.ehfConnection.AbortSyncCycle(ex);
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x0000346C File Offset: 0x0000166C
		protected void EventLogAndTraceException(string operationName, ExEventLog.EventTuple eventTuple, Exception exception, string exceptionReason)
		{
			this.LogAndTraceException(operationName, exception, exceptionReason);
			string periodicKey = (eventTuple.Period == ExEventLog.EventPeriod.LogPeriodic) ? operationName : null;
			this.DiagSession.EventLog.LogEvent(eventTuple, periodicKey, new object[]
			{
				operationName,
				exceptionReason,
				exception
			});
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000034BC File Offset: 0x000016BC
		protected void LogAndTraceException(string operationName, Exception exception, string exceptionReason)
		{
			this.DiagSession.LogAndTraceException(exception, "Exception occurred while executing EHF provisioning operation {0}; Exception Reason {1}", new object[]
			{
				operationName,
				exceptionReason
			});
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000034EC File Offset: 0x000016EC
		protected void LogAndTraceException(string operationName, Exception exception, string exceptionReason, int remainingRetryCount)
		{
			this.DiagSession.LogAndTraceException(exception, "Exception occurred while executing EHF provisioning operation {0}; Exception Reason {1}; Remaining Retry Count {2}", new object[]
			{
				operationName,
				exceptionReason,
				remainingRetryCount
			});
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00003524 File Offset: 0x00001724
		protected void HandlePerEntryFailureCounts(string operationName, int batchSize, int transientFailureCount, int permanentFailureCount, bool criticalOperation)
		{
			string text = null;
			if (transientFailureCount > 0)
			{
				text = this.DiagSession.LogAndTraceError("{0} completed with {1} per-entry transient failure(s); aborting this sync cycle", new object[]
				{
					operationName,
					transientFailureCount
				});
			}
			else if (permanentFailureCount > 0)
			{
				if (criticalOperation)
				{
					text = this.DiagSession.LogAndTraceError("Critical operation {0} completed with {1} per-entry permanent failures; aborting this sync cycle", new object[]
					{
						operationName,
						permanentFailureCount
					});
				}
				else if (permanentFailureCount == batchSize && batchSize >= 10)
				{
					text = this.DiagSession.LogAndTraceError("{0} completed with {1} per-entry permanent failures; all entries in batch failed; aborting this sync cycle", new object[]
					{
						operationName,
						permanentFailureCount
					});
				}
				else
				{
					this.DiagSession.LogAndTraceError("{0} completed with {1} per-entry permanent failures for batch size {2}; sync cycle will proceed", new object[]
					{
						operationName,
						permanentFailureCount,
						batchSize
					});
				}
			}
			if (transientFailureCount > 0 || permanentFailureCount > 0)
			{
				this.ehfConnection.PerfCounterHandler.OnPerEntryFailures(operationName, transientFailureCount, permanentFailureCount);
			}
			if (text != null)
			{
				this.DiagSession.EventLog.LogEvent(EdgeSyncEventLogConstants.Tuple_EhfPerEntryFailuresInBatch, null, new object[]
				{
					text
				});
				this.ehfConnection.AbortSyncCycle(new EdgeSyncCycleFailedException(text));
			}
		}

		// Token: 0x0400001B RID: 27
		private EhfTargetConnection ehfConnection;

		// Token: 0x0200000C RID: 12
		// (Invoke) Token: 0x06000052 RID: 82
		protected delegate void ProvisioningServiceCall();
	}
}
