using System;
using Microsoft.Exchange.Common.IL;
using Microsoft.Exchange.Data.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.AdminInterface;
using Microsoft.Exchange.Protocols.MAPI;
using Microsoft.Exchange.RpcClientAccess;
using Microsoft.Exchange.RpcClientAccess.Monitoring;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;
using Microsoft.Exchange.Server.Storage.StoreCommonServices;

namespace Microsoft.Exchange.Server.Storage.AdminInterface
{
	// Token: 0x0200000F RID: 15
	internal abstract class AdminRpc
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002687 File Offset: 0x00000887
		protected AdminRpc(AdminMethod methodId, ClientSecurityContext callerSecurityContext, byte[] auxiliaryIn)
		{
			this.clientSecurityContext = callerSecurityContext;
			this.auxiliaryIn = auxiliaryIn;
			this.methodId = methodId;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000026AB File Offset: 0x000008AB
		protected AdminRpc(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid? mdbGuid, byte[] auxiliaryIn) : this(methodId, callerSecurityContext, auxiliaryIn)
		{
			this.hasMdbGuidArgument = true;
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000026C5 File Offset: 0x000008C5
		protected AdminRpc(AdminMethod methodId, ClientSecurityContext callerSecurityContext, Guid? mdbGuid, AdminRpc.ExpectedDatabaseState expectedDatabaseState, byte[] auxiliaryIn) : this(methodId, callerSecurityContext, mdbGuid, auxiliaryIn)
		{
			this.expectedDatabaseState = expectedDatabaseState;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000030 RID: 48 RVA: 0x000026DA File Offset: 0x000008DA
		// (set) Token: 0x06000031 RID: 49 RVA: 0x000026E2 File Offset: 0x000008E2
		public Guid? MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
			set
			{
				this.mdbGuid = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000026EB File Offset: 0x000008EB
		public byte[] AuxiliaryIn
		{
			get
			{
				return this.auxiliaryIn;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000026F3 File Offset: 0x000008F3
		public byte[] AuxiliaryOut
		{
			get
			{
				return this.auxiliaryOut;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000034 RID: 52 RVA: 0x000026FB File Offset: 0x000008FB
		internal virtual int OperationDetail
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000026FE File Offset: 0x000008FE
		protected StoreDatabase Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000036 RID: 54 RVA: 0x00002706 File Offset: 0x00000906
		protected DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.databaseInfo;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000037 RID: 55 RVA: 0x0000270E File Offset: 0x0000090E
		protected ClientSecurityContext ClientSecurityContext
		{
			get
			{
				return this.clientSecurityContext;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000038 RID: 56 RVA: 0x00002716 File Offset: 0x00000916
		protected int RequestId
		{
			get
			{
				return this.requestId;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000039 RID: 57 RVA: 0x0000271E File Offset: 0x0000091E
		protected string ClientId
		{
			get
			{
				return this.clientId;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600003A RID: 58 RVA: 0x00002726 File Offset: 0x00000926
		private bool EnableDiagnosticTracing
		{
			get
			{
				return this.requestId != 0;
			}
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000027FC File Offset: 0x000009FC
		public ErrorCode EcExecute()
		{
			AdminRpc.<>c__DisplayClass1 CS$<>8__locals1 = new AdminRpc.<>c__DisplayClass1();
			CS$<>8__locals1.<>4__this = this;
			CS$<>8__locals1.localErrorCode = ErrorCode.NoError;
			CS$<>8__locals1.exceptionThrown = true;
			CS$<>8__locals1.executionDiagnostics = new AdminExecutionDiagnostics(this.methodId, this.OperationDetail);
			WatsonOnUnhandledException.Guard(CS$<>8__locals1.executionDiagnostics, new TryDelegate(CS$<>8__locals1, (UIntPtr)ldftn(<EcExecute>b__0)));
			if (CS$<>8__locals1.exceptionThrown)
			{
				CS$<>8__locals1.localErrorCode = ErrorCode.CreateCallFailed((LID)37528U);
			}
			return CS$<>8__locals1.localErrorCode;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000287C File Offset: 0x00000A7C
		internal void ParseAuxiliaryInput()
		{
			if (this.auxiliaryIn == null || this.auxiliaryIn.Length >= 1048576)
			{
				return;
			}
			AuxiliaryData auxiliaryData = AuxiliaryData.Parse(this.auxiliaryIn);
			for (int i = 0; i < auxiliaryData.Input.Count; i++)
			{
				AuxiliaryBlock auxiliaryBlock = auxiliaryData.Input[i];
				if (!(auxiliaryBlock is UnknownAuxiliaryBlock))
				{
					AuxiliaryBlockTypes type = auxiliaryBlock.Type;
					if (type != AuxiliaryBlockTypes.DiagCtxReqId)
					{
						if (type == AuxiliaryBlockTypes.DiagCtxClientId)
						{
							DiagCtxClientIdAuxiliaryBlock diagCtxClientIdAuxiliaryBlock = (DiagCtxClientIdAuxiliaryBlock)auxiliaryBlock;
							this.clientId = diagCtxClientIdAuxiliaryBlock.ClientId;
						}
					}
					else
					{
						DiagCtxReqIdAuxiliaryBlock diagCtxReqIdAuxiliaryBlock = (DiagCtxReqIdAuxiliaryBlock)auxiliaryBlock;
						this.requestId = diagCtxReqIdAuxiliaryBlock.RequestId;
					}
				}
			}
		}

		// Token: 0x0600003D RID: 61
		protected abstract ErrorCode EcExecuteRpc(MapiContext context);

		// Token: 0x0600003E RID: 62 RVA: 0x00002918 File Offset: 0x00000B18
		protected virtual ErrorCode EcInitializeResources(MapiContext context)
		{
			ErrorCode result = ErrorCode.NoError;
			if (this.hasMdbGuidArgument)
			{
				((AdminExecutionDiagnostics)context.Diagnostics).AdminExMonLogger.SetMdbGuid(this.mdbGuid.Value);
				this.database = Storage.FindDatabase(this.mdbGuid.Value);
				if (this.database == null)
				{
					result = ErrorCode.CreateMdbNotInitialized((LID)38525U);
				}
				else
				{
					try
					{
						this.databaseInfo = Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory.GetDatabaseInfo(context, this.database.MdbGuid);
					}
					catch (DatabaseNotFoundException exception)
					{
						context.OnExceptionCatch(exception);
						Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MountedStoreNotInActiveDirectory, new object[]
						{
							this.database.MdbGuid
						});
						Microsoft.Exchange.Server.Storage.Common.Globals.AssertRetail(false, "Mounted database has no associated record in AD.");
					}
					context.Connect(this.database);
					switch (this.expectedDatabaseState)
					{
					case AdminRpc.ExpectedDatabaseState.OnlineActive:
						if (!this.database.IsOnlineActive)
						{
							this.database.TraceState((LID)46076U);
							result = ErrorCode.CreateMdbNotInitialized((LID)34901U);
						}
						break;
					case AdminRpc.ExpectedDatabaseState.OnlinePassive:
						if (!this.database.IsOnlinePassive)
						{
							this.database.TraceState((LID)62460U);
							result = ErrorCode.CreateMdbNotInitialized((LID)42576U);
						}
						break;
					case AdminRpc.ExpectedDatabaseState.AnyOnlineState:
						if (!this.database.IsOnlineActive && !this.database.IsOnlinePassive)
						{
							this.database.TraceState((LID)37884U);
							result = ErrorCode.CreateMdbNotInitialized((LID)58960U);
						}
						break;
					case AdminRpc.ExpectedDatabaseState.OnlinePassiveAttachedReadOnly:
						if (!this.database.IsOnlinePassiveAttachedReadOnly)
						{
							this.database.TraceState((LID)33788U);
							result = ErrorCode.CreateMdbNotInitialized((LID)58364U);
						}
						break;
					case AdminRpc.ExpectedDatabaseState.AnyAttachedState:
						if (!this.database.IsOnlineActive && !this.database.IsOnlinePassiveAttachedReadOnly)
						{
							this.database.TraceState((LID)41980U);
							result = ErrorCode.CreateMdbNotInitialized((LID)38140U);
						}
						break;
					}
					((AdminExecutionDiagnostics)context.Diagnostics).OnBeforeRpc(this.mdbGuid.Value, RopSummaryCollector.GetRopSummaryCollector(context));
				}
			}
			return result;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002B78 File Offset: 0x00000D78
		protected virtual void CleanupResources(MapiContext context)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002B7C File Offset: 0x00000D7C
		protected virtual ErrorCode EcValidateArguments(MapiContext context)
		{
			ErrorCode result = ErrorCode.NoError;
			if (this.hasMdbGuidArgument && this.mdbGuid == null)
			{
				result = ErrorCode.CreateInvalidParameter((LID)47741U);
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002BB5 File Offset: 0x00000DB5
		protected virtual ErrorCode EcCheckPermissions(MapiContext context)
		{
			return AdminRpcPermissionChecks.EcDefaultCheck(context, this.DatabaseInfo);
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002BC4 File Offset: 0x00000DC4
		private ErrorCode EcExecute_Unwrapped(AdminExecutionDiagnostics executionDiagnostics)
		{
			if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.AdminRpcTracer.TraceFunction<AdminMethod>(35453, 0L, "Entering EcExecute_Unwrapped for {0}", this.methodId);
			}
			LockManager.AssertNoLocksHeld();
			bool flag = true;
			ErrorCode errorCode = ErrorCode.NoError;
			bool flag2 = false;
			this.ParseAuxiliaryInput();
			ClientType clientType;
			if (!ClientTypeHelper.TryGetClientType(this.clientId, out clientType))
			{
				clientType = ClientType.Administrator;
			}
			using (MapiContext mapiContext = MapiContext.CreateSessionless(executionDiagnostics, this.clientSecurityContext, clientType, LocaleMap.GetLcidFromCulture(CultureHelper.DefaultCultureInfo)))
			{
				using (AdminExMonLogger adminExMonLogger = new AdminExMonLogger(false, string.Empty))
				{
					try
					{
						executionDiagnostics.AdminExMonLogger = adminExMonLogger;
						executionDiagnostics.OnRpcBegin();
						adminExMonLogger.ServiceName = this.clientId;
						DiagnosticContext.Reset();
						errorCode = AdminRpcInterface.EcEnterRpcCall();
						if (errorCode != ErrorCode.NoError)
						{
							errorCode = errorCode.Propagate((LID)46717U);
						}
						else
						{
							flag2 = true;
							errorCode = this.EcValidateArguments(mapiContext);
							if (errorCode != ErrorCode.NoError)
							{
								errorCode = errorCode.Propagate((LID)63101U);
							}
							else
							{
								errorCode = this.EcInitializeResources(mapiContext);
								if (errorCode != ErrorCode.NoError)
								{
									errorCode = errorCode.Propagate((LID)34221U);
								}
								else
								{
									errorCode = this.EcCheckPermissions(mapiContext);
									if (errorCode != ErrorCode.NoError)
									{
										errorCode = errorCode.Propagate((LID)54909U);
									}
									else
									{
										errorCode = this.EcExecuteRpc(mapiContext);
										if (errorCode != ErrorCode.NoError)
										{
											errorCode = errorCode.Propagate((LID)42621U);
										}
										else if (mapiContext.IsConnected)
										{
											mapiContext.Commit();
										}
									}
								}
							}
						}
						flag = false;
					}
					catch (StoreException ex)
					{
						mapiContext.OnExceptionCatch(ex);
						ErrorHelper.TraceException(ExTraceGlobals.AdminRpcTracer, (LID)59009U, ex);
						errorCode = ErrorCode.CreateWithLid((LID)53912U, ex.Error);
						flag = false;
					}
					catch (BufferParseException exception)
					{
						mapiContext.OnExceptionCatch(exception);
						ErrorHelper.TraceException(ExTraceGlobals.AdminRpcTracer, (LID)61880U, exception);
						errorCode = ErrorCode.CreateRpcFormat((LID)37304U);
						flag = false;
					}
					catch (NonFatalDatabaseException ex2)
					{
						mapiContext.OnExceptionCatch(ex2);
						ErrorHelper.TraceException(ExTraceGlobals.AdminRpcTracer, (LID)56264U, ex2);
						errorCode = ErrorCode.CreateWithLid((LID)46280U, ex2.Error);
						flag = false;
					}
					catch (FatalDatabaseException exception2)
					{
						mapiContext.OnExceptionCatch(exception2);
						ErrorHelper.TraceException(ExTraceGlobals.AdminRpcTracer, (LID)54472U, exception2);
						errorCode = ErrorCode.CreateDatabaseError((LID)42184U);
						flag = false;
					}
					finally
					{
						try
						{
							this.CleanupResources(mapiContext);
						}
						finally
						{
							try
							{
								if (this.database != null)
								{
									if (mapiContext.IsConnected)
									{
										mapiContext.Disconnect();
									}
									this.database = null;
								}
							}
							catch (NonFatalDatabaseException exception3)
							{
								mapiContext.OnExceptionCatch(exception3);
								errorCode = ErrorCode.CreateDatabaseError((LID)63688U);
							}
							catch (FatalDatabaseException exception4)
							{
								mapiContext.OnExceptionCatch(exception4);
								errorCode = ErrorCode.CreateDatabaseError((LID)41400U);
							}
						}
						if (flag2)
						{
							AdminRpcInterface.ExitRpcCall();
						}
						mapiContext.DismountOnCriticalFailure();
						executionDiagnostics.OnRpcEnd(errorCode);
						if (!flag)
						{
							this.ProduceAuxiliaryOutput(executionDiagnostics);
						}
						bool flag3 = flag || errorCode != ErrorCode.NoError;
						if (ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.FunctionTrace) || (flag3 && ExTraceGlobals.AdminRpcTracer.IsTraceEnabled(TraceType.ErrorTrace)))
						{
							string message = string.Format("Exiting EcExecute_Unwrapped for {0}: (ec = {1:X})", this.methodId, (flag && errorCode == ErrorCode.NoError) ? ErrorCodeValue.ExceptionThrown : errorCode);
							if (flag3)
							{
								ExTraceGlobals.AdminRpcTracer.TraceError(48188, 0L, message);
							}
							else
							{
								ExTraceGlobals.AdminRpcTracer.TraceFunction(36349, 0L, message);
							}
						}
						LockManager.AssertNoLocksHeld();
					}
				}
			}
			if (errorCode != ErrorCode.NoError && !ErrorHelper.ShouldSkipBreadcrumb(1, (byte)this.methodId, errorCode, 0U))
			{
				ErrorHelper.AddBreadcrumb(BreadcrumbKind.AdminError, 1, (byte)this.methodId, (byte)clientType, (this.mdbGuid != null) ? this.mdbGuid.GetHashCode() : 0, executionDiagnostics.MailboxNumber, (int)errorCode, null);
			}
			return errorCode;
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000030D4 File Offset: 0x000012D4
		private void ProduceAuxiliaryOutput(AdminExecutionDiagnostics executionDiagnostics)
		{
			AuxiliaryData auxiliaryData = AuxiliaryData.Parse(null);
			if (this.EnableDiagnosticTracing && DiagnosticContext.HasData)
			{
				auxiliaryData.AppendOutput(new DiagCtxCtxDataAuxiliaryBlock(DiagnosticContext.PackInfo()));
			}
			auxiliaryData.AppendOutput(executionDiagnostics.CreateRpcStatisticsAuxiliaryBlock(null));
			byte[] array = new byte[auxiliaryData.CalculateSerializedOutputSize()];
			int num;
			auxiliaryData.Serialize(new ArraySegment<byte>(array), out num);
			this.auxiliaryOut = array;
		}

		// Token: 0x04000047 RID: 71
		private const int SizeofExchangePerfHeader = 4;

		// Token: 0x04000048 RID: 72
		private readonly AdminMethod methodId;

		// Token: 0x04000049 RID: 73
		private ClientSecurityContext clientSecurityContext;

		// Token: 0x0400004A RID: 74
		private byte[] auxiliaryIn;

		// Token: 0x0400004B RID: 75
		private byte[] auxiliaryOut;

		// Token: 0x0400004C RID: 76
		private bool hasMdbGuidArgument;

		// Token: 0x0400004D RID: 77
		private AdminRpc.ExpectedDatabaseState expectedDatabaseState = AdminRpc.ExpectedDatabaseState.OnlineActive;

		// Token: 0x0400004E RID: 78
		private Guid? mdbGuid;

		// Token: 0x0400004F RID: 79
		private StoreDatabase database;

		// Token: 0x04000050 RID: 80
		private DatabaseInfo databaseInfo;

		// Token: 0x04000051 RID: 81
		private int requestId;

		// Token: 0x04000052 RID: 82
		private string clientId;

		// Token: 0x02000010 RID: 16
		[Flags]
		internal enum ExpectedDatabaseState
		{
			// Token: 0x04000054 RID: 84
			OnlineActive = 1,
			// Token: 0x04000055 RID: 85
			OnlinePassive = 2,
			// Token: 0x04000056 RID: 86
			OnlinePassiveAttachedReadOnly = 4,
			// Token: 0x04000057 RID: 87
			AnyOnlineState = 3,
			// Token: 0x04000058 RID: 88
			AnyAttachedState = 5
		}
	}
}
