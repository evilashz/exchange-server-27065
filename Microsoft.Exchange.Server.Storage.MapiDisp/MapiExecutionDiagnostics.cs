using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.HA;
using Microsoft.Exchange.Server.Storage.PhysicalAccess;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;
using Microsoft.Isam.Esent.Interop.Vista;

namespace Microsoft.Exchange.Server.Storage.MapiDisp
{
	// Token: 0x02000005 RID: 5
	internal class MapiExecutionDiagnostics : RpcExecutionDiagnostics, IDiagnosticInfoProvider
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600014B RID: 331 RVA: 0x00029691 File Offset: 0x00027891
		// (set) Token: 0x0600014C RID: 332 RVA: 0x00029699 File Offset: 0x00027899
		internal MapiExMonLogger MapiExMonLogger
		{
			get
			{
				return this.mapiExmonLogger;
			}
			set
			{
				this.mapiExmonLogger = value;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600014D RID: 333 RVA: 0x000296A2 File Offset: 0x000278A2
		public override byte OpNumber
		{
			get
			{
				return (byte)this.ropId;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600014E RID: 334 RVA: 0x000296AA File Offset: 0x000278AA
		protected override bool HasDataToLog
		{
			get
			{
				return base.HasDataToLog || (this.IsInstantSearchFolderView && ConfigurationSchema.DiagnosticsThresholdInstantSearchFolderView.Value <= base.ChunkStatistics.ElapsedTime);
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600014F RID: 335 RVA: 0x000296E0 File Offset: 0x000278E0
		private bool IsInstantSearchFolderView
		{
			get
			{
				MapiViewMessage mapiViewMessage = this.mapiObject as MapiViewMessage;
				return mapiViewMessage != null && mapiViewMessage.CorrelationId != null;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000150 RID: 336 RVA: 0x0002970C File Offset: 0x0002790C
		protected override bool HasClientActivityDataToLog
		{
			get
			{
				return base.HasClientActivityDataToLog || (base.ClientType != ClientType.MoMT && (this.mapiObject != null && this.mapiObject.Logon != null && !this.mapiObject.Logon.ClientActivity.ActivityReported && this.mapiObject.Logon.ClientActivity.NumberOfRpcCalls >= ConfigurationSchema.DiagnosticsThresholdHeavyActivityRpcCount.Value));
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0002977F File Offset: 0x0002797F
		internal int RpcBytesReceived
		{
			get
			{
				return this.rpcBytesReceived;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00029787 File Offset: 0x00027987
		internal int RpcBytesSent
		{
			get
			{
				return this.rpcBytesSent;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0002978F File Offset: 0x0002798F
		internal int NumberOfRops
		{
			get
			{
				return this.numberOfRops;
			}
		}

		// Token: 0x1700000F RID: 15
		// (set) Token: 0x06000154 RID: 340 RVA: 0x00029797 File Offset: 0x00027997
		internal MapiBase MapiObject
		{
			set
			{
				this.mapiObject = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000155 RID: 341 RVA: 0x000297A0 File Offset: 0x000279A0
		public override uint TypeIdentifier
		{
			get
			{
				return 3U;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x000297A4 File Offset: 0x000279A4
		public void GetDiagnosticData(long maxSize, out uint threadId, out uint requestId, out DiagnosticContextFlags flags, out byte[] data)
		{
			DiagnosticContext.TraceDwordAndString((LID)10786U, 0U, MapiExecutionDiagnostics.diagnosticInfoHeaderString);
			threadId = (uint)Environment.CurrentManagedThreadId;
			requestId = 0U;
			int maxSize2 = Math.Min((int)maxSize, 512);
			byte b;
			DiagnosticContext.ExtractInfo(maxSize2, out b, out data);
			flags = (DiagnosticContextFlags)b;
		}

		// Token: 0x06000157 RID: 343 RVA: 0x000297EB File Offset: 0x000279EB
		public override int GetHashCode()
		{
			return base.GetHashCode() ^ "Rop".GetHashCode() ^ this.ropId.GetHashCode() ^ (this.operationDetail ?? string.Empty).GetHashCode();
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00029824 File Offset: 0x00027A24
		internal new void OnRpcBegin()
		{
			base.OnRpcBegin();
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0002982C File Offset: 0x00027A2C
		internal new void OnRpcEnd()
		{
			base.OnRpcEnd();
		}

		// Token: 0x0600015A RID: 346 RVA: 0x00029834 File Offset: 0x00027A34
		internal void OnRopBegin(RopId ropId)
		{
			this.MapiExMonLogger.LogPrepareForRop(ropId);
			base.OnBeginOperation();
			this.ropId = ropId;
			ILogTransactionInformation logTransactionInformationBlock = new LogTransactionInformationMapi(this.ropId);
			base.LogTransactionInformationCollector.AddLogTransactionInformationBlock(logTransactionInformationBlock);
			base.ClearExceptionHistory();
			this.MapiExMonLogger.SetClientActivityInfo(base.ClientActivityId.ToString(), base.ClientComponentName, base.ClientProtocolName, base.ClientActionString);
		}

		// Token: 0x0600015B RID: 347 RVA: 0x000298AC File Offset: 0x00027AAC
		internal void OnRopEnd(RopId ropId, ErrorCode errorCode)
		{
			this.numberOfRops++;
			JET_THREADSTATS threadStats;
			Factory.GetDatabaseThreadStats(out threadStats);
			bool isNewActivity = false;
			this.MapiExMonLogger.LogCompletedRop(ropId, errorCode, threadStats);
			if (this.mapiObject != null)
			{
				if (this.mapiObject.MapiObjectType == MapiObjectType.Stream && this.mapiObject.ParentObject != null)
				{
					base.OpDetail = (int)(this.mapiObject.ParentObject.MapiObjectType + 5000U);
				}
				else
				{
					base.OpDetail = (int)(this.mapiObject.MapiObjectType + 1000U);
				}
				if (this.mapiObject.Logon != null)
				{
					isNewActivity = this.mapiObject.Logon.ClientActivity.Update(base.ClientActivityId, base.RpcExecutionCookie, this.ropId);
				}
			}
			base.OnEndOperation(OperationType.Rop, base.ExpandedClientActionStringId, (byte)ropId, (uint)errorCode, isNewActivity);
			if (base.DumpClientActivityDiagnosticIfNeeded())
			{
				this.mapiObject.Logon.ClientActivity.ActivityReported = true;
			}
			this.ropId = RopId.None;
			if (this.mapiObject != null)
			{
				this.mapiObject.ClearDiagnosticInformation();
				this.mapiObject = null;
			}
			ResourceDigestStats activity = new ResourceDigestStats
			{
				ROPCount = 1,
				LdapSearches = base.OperationStatistics.DirectoryCount,
				TimeInServer = base.OperationStatistics.ElapsedTime
			};
			base.ActivityCollector.LogActivity(activity);
		}

		// Token: 0x0600015C RID: 348 RVA: 0x00029A00 File Offset: 0x00027C00
		internal override void OnBeforeEndMailboxOperation()
		{
			base.OnBeforeEndMailboxOperation();
			StoreDatabase storeDatabase = Storage.FindDatabase(base.DatabaseGuid);
			if (storeDatabase != null)
			{
				JetHADatabase jetHADatabase = storeDatabase.PhysicalDatabase as JetHADatabase;
				if (jetHADatabase != null)
				{
					base.ReplicationThrottlingData.Update(jetHADatabase.ThrottlingData.DataProtectionHealth, jetHADatabase.ThrottlingData.DataAvailabilityHealth);
				}
			}
		}

		// Token: 0x0600015D RID: 349 RVA: 0x00029A52 File Offset: 0x00027C52
		internal void UpdateRpcBytesReceived(int bytes)
		{
			this.rpcBytesReceived += bytes;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x00029A62 File Offset: 0x00027C62
		internal void UpdateRpcBytesSent(int bytes)
		{
			this.rpcBytesSent += bytes;
		}

		// Token: 0x0600015F RID: 351 RVA: 0x00029A74 File Offset: 0x00027C74
		internal override void EnablePerClientTypePerfCounterUpdate()
		{
			base.EnablePerClientTypePerfCounterUpdate();
			this.rpcBytesReceived = 0;
			this.rpcBytesSent = 0;
			this.numberOfRops = 0;
			this.rpcExecutionStartTimeStamp = StopwatchStamp.GetStamp();
			if (base.PerClientPerfInstance != null)
			{
				base.PerClientPerfInstance.RPCRequests.Increment();
			}
		}

		// Token: 0x06000160 RID: 352 RVA: 0x00029AC0 File Offset: 0x00027CC0
		internal override void DisablePerClientTypePerfCounterUpdate()
		{
			if (base.PerClientPerfInstance != null)
			{
				base.PerClientPerfInstance.RPCRequests.Decrement();
				base.PerClientPerfInstance.RPCPacketsRate.Increment();
				base.PerClientPerfInstance.RPCBytesInRate.IncrementBy((long)this.rpcBytesReceived);
				base.PerClientPerfInstance.RPCBytesOutRate.IncrementBy((long)this.rpcBytesSent);
				base.PerClientPerfInstance.RPCOperationRate.IncrementBy((long)this.numberOfRops);
				base.PerClientPerfInstance.RPCAverageLatencyBase.Increment();
				base.PerClientPerfInstance.RPCAverageLatency.IncrementBy((long)this.rpcExecutionStartTimeStamp.ElapsedTime.TotalMilliseconds);
			}
			base.DisablePerClientTypePerfCounterUpdate();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x00029B7E File Offset: 0x00027D7E
		protected override void FormatOperationInformation(TraceContentBuilder cb, int indentLevel)
		{
			base.FormatOperationInformation(cb, indentLevel);
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Rop: " + this.ropId);
			if (this.mapiObject != null)
			{
				this.mapiObject.FormatDiagnosticInformation(cb, indentLevel);
			}
		}

		// Token: 0x06000162 RID: 354 RVA: 0x00029BBC File Offset: 0x00027DBC
		protected override void FormatClientActivityDiagnosticInformation(TraceContentBuilder cb, int indentLevel)
		{
			base.FormatClientActivityDiagnosticInformation(cb, indentLevel);
			if (this.mapiObject == null || this.mapiObject.Logon == null || this.mapiObject.Logon.ClientActivity == null || this.mapiObject.Logon.ClientActivity.RopsCount == null)
			{
				return;
			}
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Executed ROPs:");
			ushort[] ropsCount = this.mapiObject.Logon.ClientActivity.RopsCount;
			for (int i = 0; i < ropsCount.Length; i++)
			{
				if (ropsCount[i] != 0)
				{
					ExecutionDiagnostics.FormatLine(cb, indentLevel + 1, string.Format("{0}: {1}", (RopId)i, ropsCount[i]));
				}
			}
		}

		// Token: 0x06000163 RID: 355 RVA: 0x00029C68 File Offset: 0x00027E68
		public void ExtractClientActivityFromAuxiliaryData(AuxiliaryData auxiliaryData)
		{
			if (auxiliaryData == null || auxiliaryData.Input == null)
			{
				return;
			}
			for (int i = 0; i < auxiliaryData.Input.Count; i++)
			{
				AuxiliaryBlock auxiliaryBlock = auxiliaryData.Input[i];
				if (auxiliaryBlock.Type == AuxiliaryBlockTypes.ClientActivity)
				{
					ClientActivityAuxiliaryBlock clientActivityAuxiliaryBlock = (ClientActivityAuxiliaryBlock)auxiliaryBlock;
					base.SetClientActivityInfo(clientActivityAuxiliaryBlock.ActivityId, clientActivityAuxiliaryBlock.ComponentName, clientActivityAuxiliaryBlock.ProtocolName, clientActivityAuxiliaryBlock.ActionString);
					base.UpdateTestCaseId((TestCaseId)((int)clientActivityAuxiliaryBlock.TestCaseId));
					return;
				}
			}
		}

		// Token: 0x06000164 RID: 356 RVA: 0x00029CE8 File Offset: 0x00027EE8
		protected override void GetSummaryInformation(Guid correlationId, ref ExecutionDiagnostics.LongOperationSummary summary)
		{
			base.GetSummaryInformation(correlationId, ref summary);
			summary.OperationType = "Rop";
			summary.OperationName = this.ropId.ToString();
			if (this.mapiObject != null)
			{
				this.mapiObject.GetSummaryInformation(ref summary);
				this.operationDetail = summary.OperationDetail;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x00029D40 File Offset: 0x00027F40
		protected override void GetClientActivitySummaryInformation(Guid correlationId, ref ExecutionDiagnostics.HeavyClientActivitySummary summary)
		{
			base.GetClientActivitySummaryInformation(correlationId, ref summary);
			summary.TotalRpcCalls = (uint)this.mapiObject.Logon.ClientActivity.NumberOfRpcCalls;
			uint num = 0U;
			foreach (ushort num2 in this.mapiObject.Logon.ClientActivity.RopsCount)
			{
				num += (uint)num2;
			}
			summary.TotalRops = num;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00029DA8 File Offset: 0x00027FA8
		protected override void FormatThresholdInformation(TraceContentBuilder cb, int indentLevel)
		{
			if (this.IsInstantSearchFolderView)
			{
				long value = (long)base.ChunkStatistics.ElapsedTime.TotalMilliseconds;
				long threshold = (long)ConfigurationSchema.DiagnosticsThresholdInstantSearchFolderView.Value.TotalMilliseconds;
				ExecutionDiagnostics.FormatThresholdLine(cb, indentLevel, "Instant Search View", value, threshold, "ms");
			}
			base.FormatThresholdInformation(cb, indentLevel);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00029E01 File Offset: 0x00028001
		protected override void FormatClientActivityThresholdInformation(TraceContentBuilder cb, int indentLevel)
		{
			ExecutionDiagnostics.FormatLine(cb, indentLevel, "Total RPC calls: >=" + this.mapiObject.Logon.ClientActivity.NumberOfRpcCalls);
			base.FormatClientActivityThresholdInformation(cb, indentLevel);
		}

		// Token: 0x04000103 RID: 259
		private const int MaximalSupportedDiagnosticInfoSize = 512;

		// Token: 0x04000104 RID: 260
		private static string diagnosticInfoHeaderString = string.Format("{0}:{1}", "15.00.1497.010", MapiDispHelper.GetDnsHostName());

		// Token: 0x04000105 RID: 261
		private RopId ropId;

		// Token: 0x04000106 RID: 262
		private MapiBase mapiObject;

		// Token: 0x04000107 RID: 263
		private MapiExMonLogger mapiExmonLogger;

		// Token: 0x04000108 RID: 264
		private int rpcBytesReceived;

		// Token: 0x04000109 RID: 265
		private int rpcBytesSent;

		// Token: 0x0400010A RID: 266
		private int numberOfRops;

		// Token: 0x0400010B RID: 267
		private StopwatchStamp rpcExecutionStartTimeStamp;

		// Token: 0x0400010C RID: 268
		private string operationDetail;
	}
}
