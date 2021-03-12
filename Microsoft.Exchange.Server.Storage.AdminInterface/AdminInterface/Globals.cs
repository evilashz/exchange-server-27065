using System;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x02000061 RID: 97
	public static class Globals
	{
		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060001AC RID: 428 RVA: 0x0000E9C1 File Offset: 0x0000CBC1
		// (set) Token: 0x060001AD RID: 429 RVA: 0x0000E9C8 File Offset: 0x0000CBC8
		public static IAdminRpcServer AdminRpcServer { get; private set; }

		// Token: 0x060001AE RID: 430 RVA: 0x0000E9DD File Offset: 0x0000CBDD
		public static void Initialize()
		{
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.AdminRpcServer = new AdminRpcServer();
			SimpleQueryTargets.Initialize();
			RopSummaryResolver.Add(OperationType.Admin, (byte operationId) => ((AdminMethod)operationId).ToString());
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000EA11 File Offset: 0x0000CC11
		public static void DatabaseMounting(Context context, StoreDatabase database)
		{
			SimpleQueryTargets.MountEventHandler(database);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000EA94 File Offset: 0x0000CC94
		public static void WriteReferenceData()
		{
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<OperationType>(LoggerManager.TraceGuids.OperationType, (OperationType key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<ClientType>(LoggerManager.TraceGuids.ClientType, (ClientType key) => key != ClientType.MaxValue, (ClientType key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<RopId>(LoggerManager.TraceGuids.RopId, (RopId key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<TaskTypeId>(LoggerManager.TraceGuids.TaskType, (TaskTypeId key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<AdminMethod>(LoggerManager.TraceGuids.AdminMethod, (AdminMethod key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<OperationDetail>(LoggerManager.TraceGuids.OperationDetail, (OperationDetail key) => key == OperationDetail.None, (OperationDetail key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<MapiObjectType>(LoggerManager.TraceGuids.OperationDetail, (MapiObjectType key) => (int)(key + 1000U));
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask>(LoggerManager.TraceGuids.OperationDetail, (AdminRpcServer.AdminDoMaintenanceTask.MaintenanceTask key) => (int)(key + 2000U));
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<Operation>(LoggerManager.TraceGuids.OperationDetail, (Operation key) => (int)(key + 3000U));
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<AdminRpcServer.AdminExecuteTask50.MaintenanceTask>(LoggerManager.TraceGuids.OperationDetail, (AdminRpcServer.AdminExecuteTask50.MaintenanceTask key) => (int)(key + 4000U));
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<MapiObjectType>(LoggerManager.TraceGuids.OperationDetail, (MapiObjectType key) => true, (MapiObjectType key) => (int)(key + 5000U), (MapiObjectType key) => "Stream." + key.ToString());
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<ErrorCodeValue>(LoggerManager.TraceGuids.ErrorCode, (ErrorCodeValue key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<MailboxStatus>(LoggerManager.TraceGuids.MailboxStatus, (MailboxStatus key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<MailboxInfo.MailboxType>(LoggerManager.TraceGuids.MailboxType, (MailboxInfo.MailboxType key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<MailboxInfo.MailboxTypeDetail>(LoggerManager.TraceGuids.MailboxTypeDetail, (MailboxInfo.MailboxTypeDetail key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<BreadcrumbKind>(LoggerManager.TraceGuids.BreadCrumbKind, (BreadcrumbKind key) => (int)key);
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<ExecutionDiagnostics.OperationSource>(LoggerManager.TraceGuids.OperationSource, (ExecutionDiagnostics.OperationSource key) => (int)key);
			ClientActivityStrings.WriteReferenceData();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000EDB1 File Offset: 0x0000CFB1
		public static void Terminate()
		{
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.AdminRpcServer = null;
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000EDB9 File Offset: 0x0000CFB9
		public static void DatabaseMounted(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000EDBB File Offset: 0x0000CFBB
		public static void DatabaseDismounting(Context context, StoreDatabase database)
		{
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000EDC0 File Offset: 0x0000CFC0
		private static void WriteReferenceData<TEnum>(Guid guid, Func<TEnum, int> convert)
		{
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<TEnum>(guid, (TEnum key) => true, convert);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000EDE4 File Offset: 0x0000CFE4
		private static void WriteReferenceData<TEnum>(Guid guid, Func<TEnum, bool> check, Func<TEnum, int> convert)
		{
			Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceData<TEnum>(guid, check, convert, (TEnum key) => key.ToString());
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		private static void WriteReferenceData<TEnum>(Guid guid, Func<TEnum, bool> check, Func<TEnum, int> convert, Func<TEnum, string> label)
		{
			IBinaryLogger logger = LoggerManager.GetLogger(LoggerType.ReferenceData);
			if (logger == null || !logger.IsLoggingEnabled)
			{
				return;
			}
			foreach (object obj in Enum.GetValues(typeof(TEnum)))
			{
				TEnum arg = (TEnum)((object)obj);
				if (check(arg))
				{
					Microsoft.Exchange.Server.Storage.AdminInterface.Globals.WriteReferenceKeyValue(logger, guid, convert(arg), label(arg));
				}
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000EE88 File Offset: 0x0000D088
		private static void WriteReferenceKeyValue(IBinaryLogger logger, Guid guid, int key, string value)
		{
			using (TraceBuffer traceBuffer = TraceRecord.Create(guid, true, false, key, value))
			{
				logger.TryWrite(traceBuffer);
			}
		}
	}
}
