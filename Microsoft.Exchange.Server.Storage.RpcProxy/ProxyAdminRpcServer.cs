using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Microsoft.Exchange.Common.HA;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ManagedStore.RpcProxy;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.AdminRpc;
using Microsoft.Exchange.RpcClientAccess.Parser;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Server.Storage.AdminInterface;
using Microsoft.Exchange.Server.Storage.Common;
using Microsoft.Exchange.Server.Storage.DirectoryServices;

namespace Microsoft.Exchange.Server.Storage.RpcProxy
{
	// Token: 0x02000005 RID: 5
	public sealed class ProxyAdminRpcServer : IAdminRpcServer
	{
		// Token: 0x06000002 RID: 2 RVA: 0x000020E1 File Offset: 0x000002E1
		internal ProxyAdminRpcServer(IRpcInstanceManager manager)
		{
			this.manager = manager;
			this.mountInProgressSet = new Dictionary<Guid, CancellationTokenSource>();
			this.instanceStatusTimeout = ConfigurationSchema.InstanceStatusTimeout.Value;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000210B File Offset: 0x0000030B
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002113 File Offset: 0x00000313
		internal TimeSpan InstanceStatusRpcTimeout
		{
			get
			{
				return this.instanceStatusTimeout;
			}
			set
			{
				this.instanceStatusTimeout = value;
			}
		}

		// Token: 0x06000005 RID: 5 RVA: 0x0000211C File Offset: 0x0000031C
		public void AdminGetIFVersion(ClientSecurityContext callerSecurityContext, out ushort majorVersion, out ushort minorVersion)
		{
			majorVersion = 7;
			minorVersion = 17;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002128 File Offset: 0x00000328
		public int EcListAllMdbStatus50(ClientSecurityContext callerSecurityContext, bool basicInformation, out uint countMdbs, out byte[] mdbStatus, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "ENTER CALL PROXY [ADMIN][EcListAllMdbStatus50]");
			}
			int result;
			try
			{
				countMdbs = 0U;
				int num = 0;
				List<InstanceMdbStatus> list = new List<InstanceMdbStatus>(16);
				List<byte[]> list2 = new List<byte[]>(16);
				HashSet<Guid> hashSet = null;
				using (LockManager.Lock(this.mountInProgressSet))
				{
					foreach (Guid item in this.mountInProgressSet.Keys)
					{
						if (hashSet == null)
						{
							hashSet = new HashSet<Guid>();
						}
						hashSet.Add(item);
					}
				}
				if (hashSet != null)
				{
					List<AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus> list3 = null;
					foreach (Guid guid in hashSet)
					{
						if (list3 == null)
						{
							list3 = new List<AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus>(1);
						}
						list3.Clear();
						try
						{
							ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
							DatabaseInfo databaseInfo = ((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).GetDatabaseInfo(NullExecutionContext.Instance, guid);
							list3.Add(new AdminRpcServer.AdminRpcListAllMdbStatus.MdbStatus(guid, 64UL, databaseInfo.MdbName, databaseInfo.ServerName, databaseInfo.LegacyDN));
							byte[] array;
							AdminRpcParseFormat.SerializeMdbStatus(list3, basicInformation, out array);
							if (array != null && array.Length > 0)
							{
								countMdbs += 1U;
								list.Add(new InstanceMdbStatus
								{
									Count = 1U,
									Blob = array
								});
								num += array.Length;
							}
						}
						catch (StoreException ex)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
							if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder = new StringBuilder(1000);
								stringBuilder.Append("Failed to generate MdbStatus blob. mdb:[");
								stringBuilder.Append(guid);
								stringBuilder.Append("] error:[");
								stringBuilder.Append(ex.ToString());
								stringBuilder.Append("]");
								ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder.ToString());
							}
						}
						catch (FailRpcException ex2)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(ex2);
							if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
							{
								StringBuilder stringBuilder2 = new StringBuilder(1000);
								stringBuilder2.Append("Failed to check permissions. mdb:[");
								stringBuilder2.Append(guid);
								stringBuilder2.Append("] error:[");
								stringBuilder2.Append(ex2.ToString());
								stringBuilder2.Append("]");
								ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder2.ToString());
							}
						}
					}
				}
				List<Guid> list4 = new List<Guid>();
				foreach (Guid guid2 in this.manager.GetActiveInstances())
				{
					try
					{
						ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid2, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
						if (hashSet == null || !hashSet.Contains(guid2))
						{
							list4.Add(guid2);
						}
					}
					catch (FailRpcException ex3)
					{
						NullExecutionDiagnostics.Instance.OnExceptionCatch(ex3);
						if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							StringBuilder stringBuilder3 = new StringBuilder(1000);
							stringBuilder3.Append("Failed to check permissions. mdb:[");
							stringBuilder3.Append(guid2);
							stringBuilder3.Append("] error:[");
							stringBuilder3.Append(ex3.ToString());
							stringBuilder3.Append("]");
							ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder3.ToString());
						}
					}
				}
				if (list4.Count > 0)
				{
					RpcInstanceManager.AdminCallGuard[] array2 = new RpcInstanceManager.AdminCallGuard[list4.Count];
					AdminRpcClient[] array3 = new AdminRpcClient[list4.Count];
					IRpcAsyncResult[] array4 = new IRpcAsyncResult[list4.Count];
					int num2 = 0;
					try
					{
						foreach (Guid instanceId in list4)
						{
							array2[num2] = this.manager.GetAdminRpcClient(instanceId, "EcListAllMdbStatus50", out array3[num2]);
							num2++;
						}
						for (int i = 0; i < num2; i++)
						{
							try
							{
								if (array3[i] != null)
								{
									array4[i] = array3[i].BeginEcListAllMdbStatus50(basicInformation, auxiliaryIn, null, null);
								}
							}
							catch (RpcException ex4)
							{
								NullExecutionDiagnostics.Instance.OnExceptionCatch(ex4);
								if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder4 = new StringBuilder(1000);
									stringBuilder4.Append("Failed to begin async EcListAllMdbStatus50. mdb:[");
									stringBuilder4.Append(list4[i]);
									stringBuilder4.Append("] error:[");
									stringBuilder4.Append(ex4.ToString());
									stringBuilder4.Append("]");
									ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder4.ToString());
								}
								this.PublishFailureItem(list4[i], FailureTag.UnaccessibleStoreWorker);
							}
						}
						bool flag = false;
						int num3 = 0;
						while (num3 < num2 && !flag)
						{
							if (array4[num3] != null)
							{
								flag = !array4[num3].AsyncWaitHandle.WaitOne(this.instanceStatusTimeout);
								if (flag && ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
								{
									StringBuilder stringBuilder5 = new StringBuilder(100);
									stringBuilder5.Append("Timeout detected on async EcListAllMdbStatus50. mdb:[");
									stringBuilder5.Append(list4[num3]);
									stringBuilder5.Append("]");
									ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder5.ToString());
								}
							}
							num3++;
						}
						if (flag)
						{
							for (int j = 0; j < num2; j++)
							{
								if (array4[j] != null && !array4[j].AsyncWaitHandle.WaitOne(0))
								{
									if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										StringBuilder stringBuilder6 = new StringBuilder(1000);
										stringBuilder6.Append("Aborting EcListAllMdbStatus50 call to worker due to timeout. mdb:[");
										stringBuilder6.Append(list4[j]);
										stringBuilder6.Append("]");
										ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder6.ToString());
									}
									array4[j].Cancel();
								}
							}
						}
						for (int k = 0; k < num2; k++)
						{
							if (array4[k] != null)
							{
								try
								{
									uint num4;
									byte[] array5;
									byte[] item2;
									ErrorCode second = ErrorCode.CreateWithLid((LID)56696U, (ErrorCodeValue)array3[k].EndEcListAllMdbStatus50(array4[k], out num4, out array5, out item2));
									list2.Add(item2);
									if (ErrorCode.NoError == second && array5 != null && array5.Length > 0)
									{
										countMdbs += num4;
										list.Add(new InstanceMdbStatus
										{
											Count = num4,
											Blob = array5
										});
										num += array5.Length;
									}
								}
								catch (RpcException ex5)
								{
									NullExecutionDiagnostics.Instance.OnExceptionCatch(ex5);
									if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
									{
										StringBuilder stringBuilder7 = new StringBuilder(1000);
										stringBuilder7.Append("EcListAllMdbStatus50 threw an exception. mdb:[");
										stringBuilder7.Append(list4[k]);
										stringBuilder7.Append("] error:[");
										stringBuilder7.Append(ex5.ToString());
										stringBuilder7.Append("]");
										ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder7.ToString());
									}
									if (ex5 is CallCancelledException)
									{
										this.PublishFailureItem(list4[k], FailureTag.HungStoreWorker);
									}
									else if (ex5 is ServerTooBusyException)
									{
										this.PublishFailureItem(list4[k], FailureTag.UnaccessibleStoreWorker);
									}
								}
							}
						}
					}
					finally
					{
						for (int l = 0; l < num2; l++)
						{
							array2[l].Dispose();
						}
					}
				}
				if (list.Count > 0)
				{
					byte[] array6 = new byte[num];
					int num5 = 0;
					int num6 = 0;
					foreach (InstanceMdbStatus status in list)
					{
						ProxyAdminRpcServer.MergeInstanceStatus(array6, (int)countMdbs, status, ref num5, ref num6);
					}
					mdbStatus = array6;
				}
				else
				{
					countMdbs = 0U;
					mdbStatus = null;
				}
				auxiliaryOut = ProxyAdminRpcServer.MergeOutBuffers(list2);
				result = (int)ErrorCode.NoError;
			}
			finally
			{
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "EXIT CALL PROXY [ADMIN][EcListAllMdbStatus50]");
				}
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002A88 File Offset: 0x00000C88
		public int EcListMdbStatus50(ClientSecurityContext callerSecurityContext, Guid[] mdbGuids, out uint[] mdbStatusFlags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			mdbStatusFlags = new uint[mdbGuids.Length];
			List<byte[]> list = new List<byte[]>(mdbGuids.Length);
			for (int i = 0; i < mdbGuids.Length; i++)
			{
				Guid guid = mdbGuids[i];
				mdbStatusFlags[i] = 0U;
				AdminRpcClient adminRpcClient2;
				using (this.manager.GetAdminRpcClient(guid, "EcListMdbStatus50", out adminRpcClient2))
				{
					if (adminRpcClient2 != null)
					{
						ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
						try
						{
							uint[] array;
							byte[] item;
							ErrorCode second = ErrorCode.CreateWithLid((LID)44408U, (ErrorCodeValue)adminRpcClient2.EcListMdbStatus50(new Guid[]
							{
								guid
							}, out array, auxiliaryIn, out item));
							list.Add(item);
							if (ErrorCode.NoError == second)
							{
								mdbStatusFlags[i] = array[0];
							}
						}
						catch (RpcException exception)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
						}
					}
				}
			}
			auxiliaryOut = ProxyAdminRpcServer.MergeOutBuffers(list);
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public int EcGetDatabaseSizeEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out uint totalPages, out uint availablePages, out uint pageSize, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			uint localTotalPages = 0U;
			uint localAvailablePages = 0U;
			uint localPageSize = 0U;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetDatabaseSizeEx50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetDatabaseSizeEx50(mdbGuid, out localTotalPages, out localAvailablePages, out localPageSize, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			totalPages = localTotalPages;
			availablePages = localAvailablePages;
			pageSize = localPageSize;
			return result;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002C80 File Offset: 0x00000E80
		public int EcAdminGetCnctTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetCnctTable50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetCnctTable50(mdbGuid, lparam, out localResult, propTags, cpid, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002D24 File Offset: 0x00000F24
		public int EcGetLastBackupTimes50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastCompleteBackupTime, out long lastIncrementalBackupTime, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			long localLastCompleteBackupTime = 0L;
			long localLastIncrementalBackupTime = 0L;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetLastBackupTimes50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetLastBackupTimes50(mdbGuid, out localLastCompleteBackupTime, out localLastIncrementalBackupTime, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			lastCompleteBackupTime = localLastCompleteBackupTime;
			lastIncrementalBackupTime = localLastIncrementalBackupTime;
			return result;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002DAC File Offset: 0x00000FAC
		public int EcClearAbsentInDsFlagOnMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcClearAbsentInDsFlagOnMailbox50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcClearAbsentInDsFlagOnMailbox50(mdbGuid, mailboxGuid, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002DF8 File Offset: 0x00000FF8
		public int EcPurgeCachedMailboxObject50(ClientSecurityContext callerSecurityContext, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			List<byte[]> list = new List<byte[]>(16);
			foreach (Guid guid in this.manager.GetActiveInstances())
			{
				AdminRpcClient adminRpcClient2;
				using (this.manager.GetAdminRpcClient(guid, "EcPurgeCachedMailboxObject50", out adminRpcClient2))
				{
					if (adminRpcClient2 != null)
					{
						try
						{
							ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
							byte[] item;
							adminRpcClient2.EcPurgeCachedMailboxObject50(mailboxGuid, auxiliaryIn, out item);
							list.Add(item);
						}
						catch (FailRpcException exception)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
						}
						catch (RpcException exception2)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
						}
					}
				}
			}
			auxiliaryOut = ProxyAdminRpcServer.MergeOutBuffers(list);
			return (int)ErrorCode.NoError;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002F0C File Offset: 0x0000110C
		public int EcSyncMailboxesWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcSyncMailboxesWithDS50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcSyncMailboxesWithDS50(mdbGuid, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002F78 File Offset: 0x00001178
		public int EcAdminDeletePrivateMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminDeletePrivateMailbox50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminDeletePrivateMailbox50(mdbGuid, mailboxGuid, flags, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002FF4 File Offset: 0x000011F4
		public int EcSetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcSetMailboxSecurityDescriptor50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcSetMailboxSecurityDescriptor50(mdbGuid, mailboxGuid, ntsd, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00003070 File Offset: 0x00001270
		public int EcGetMailboxSecurityDescriptor50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, out byte[] ntsd, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localNtsd = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetMailboxSecurityDescriptor50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetMailboxSecurityDescriptor50(mdbGuid, mailboxGuid, out localNtsd, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			ntsd = localNtsd;
			return result;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00003114 File Offset: 0x00001314
		public int EcAdminGetLogonTable50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetLogonTable50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetLogonTable50(mdbGuid, lparam, out localResult, propTags, cpid, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003190 File Offset: 0x00001390
		public int EcMountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			bool flag = false;
			bool flag2 = false;
			CancellationTokenSource cancellationTokenSource = null;
			auxiliaryOut = null;
			DiagnosticContext.Reset();
			if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "ENTER CALL PROXY [ADMIN][EcMountDatabase50]");
			}
			try
			{
				ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, mdbGuid, ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation);
				try
				{
					using (LockManager.Lock(this.mountInProgressSet))
					{
						if (this.mountInProgressSet.ContainsKey(mdbGuid))
						{
							return 2612;
						}
						cancellationTokenSource = new CancellationTokenSource();
						this.mountInProgressSet.Add(mdbGuid, cancellationTokenSource);
						flag2 = true;
					}
					DiagnosticContext.TraceDword((LID)37952U, (uint)Environment.TickCount);
					errorCode = this.manager.StartInstance(mdbGuid, flags, ref flag, cancellationTokenSource.Token);
					DiagnosticContext.TraceDword((LID)54336U, (uint)Environment.TickCount);
					if (ErrorCode.NoError == errorCode)
					{
						AdminRpcClient adminRpcClient2;
						using (this.manager.GetAdminRpcClient(mdbGuid, "EcMountDatabase50", out adminRpcClient2))
						{
							if (adminRpcClient2 == null)
							{
								throw new FailRpcException("Instance is not available.", 1142);
							}
							errorCode = ErrorCode.CreateWithLid((LID)63864U, (ErrorCodeValue)adminRpcClient2.EcMountDatabase50(mdbGuid, flags, auxiliaryIn, out auxiliaryOut));
							this.AppendAuxiliaryOutput(auxiliaryOut);
							DiagnosticContext.TraceDword((LID)62528U, (uint)Environment.TickCount);
							flag = (ErrorCode.NoError != errorCode);
						}
					}
				}
				finally
				{
					if (flag)
					{
						this.manager.StopInstance(mdbGuid, false);
					}
					using (LockManager.Lock(this.mountInProgressSet))
					{
						if (flag2)
						{
							this.mountInProgressSet.Remove(mdbGuid);
							Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_MountCompleted, new object[]
							{
								mdbGuid
							});
						}
						if (cancellationTokenSource != null)
						{
							cancellationTokenSource.Dispose();
						}
					}
				}
			}
			catch (FailRpcException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					stringBuilder.Append("EcMountDatabase50 threw an exception. mdb:[");
					stringBuilder.Append(mdbGuid);
					stringBuilder.Append("] exception:[");
					stringBuilder.Append(ex.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				errorCode = ErrorCode.CreateWithLid((LID)57888U, (ErrorCodeValue)ex.ErrorCode);
			}
			finally
			{
				if (auxiliaryOut == null && errorCode != ErrorCode.NoError && DiagnosticContext.HasData)
				{
					auxiliaryOut = this.ProduceAuxiliaryOutput();
				}
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "EXIT CALL PROXY [ADMIN][EcMountDatabase50]");
				}
			}
			return (int)errorCode;
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000034D8 File Offset: 0x000016D8
		public int EcUnmountDatabase50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			bool flag = 0U != (flags & 16U);
			auxiliaryOut = null;
			DiagnosticContext.Reset();
			if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "ENTER CALL PROXY [ADMIN][EcUnmountDatabase50]");
			}
			try
			{
				ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, mdbGuid, ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation);
				DiagnosticContext.TraceDword((LID)58432U, (uint)Environment.TickCount);
				AdminRpcClient adminRpcClient2;
				using (this.manager.GetAdminRpcClient(mdbGuid, "EcUnmountDatabase50", out adminRpcClient2))
				{
					if (adminRpcClient2 != null)
					{
						if (!flag)
						{
							errorCode = ErrorCode.CreateWithLid((LID)39288U, (ErrorCodeValue)adminRpcClient2.EcUnmountDatabase50(mdbGuid, flags, auxiliaryIn, out auxiliaryOut));
							this.AppendAuxiliaryOutput(auxiliaryOut);
							DiagnosticContext.TraceDword((LID)51264U, (uint)Environment.TickCount);
							Microsoft.Exchange.Server.Storage.Common.Globals.LogEvent(MSExchangeISEventLogConstants.Tuple_DismountCompleted, new object[]
							{
								mdbGuid
							});
						}
					}
					else
					{
						using (LockManager.Lock(this.mountInProgressSet))
						{
							if (flag && this.mountInProgressSet.ContainsKey(mdbGuid))
							{
								this.mountInProgressSet[mdbGuid].Cancel();
								errorCode = ErrorCode.NoError;
							}
							else
							{
								errorCode = ErrorCode.CreateNotFound((LID)55672U);
							}
						}
					}
				}
				if (ErrorCode.NoError == errorCode)
				{
					this.manager.StopInstance(mdbGuid, flag);
				}
			}
			catch (FailRpcException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					stringBuilder.Append("EcUnmountDatabase50 threw an exception. mdb:[");
					stringBuilder.Append(mdbGuid);
					stringBuilder.Append("] exception:[");
					stringBuilder.Append(ex.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				errorCode = ErrorCode.CreateWithLid((LID)33312U, (ErrorCodeValue)ex.ErrorCode);
			}
			finally
			{
				if (auxiliaryOut == null && errorCode != ErrorCode.NoError && DiagnosticContext.HasData)
				{
					auxiliaryOut = this.ProduceAuxiliaryOutput();
				}
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, "EXIT CALL PROXY [ADMIN][EcUnmountDatabase50]");
				}
			}
			return (int)errorCode;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000037A4 File Offset: 0x000019A4
		public int EcAdminSetMailboxBasicInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] mailboxInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminSetMailboxBasicInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminSetMailboxBasicInfo50(mdbGuid, mailboxGuid, mailboxInfo, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x000037F8 File Offset: 0x000019F8
		public int EcPurgeCachedMdbObject50(ClientSecurityContext callerSecurityContext, Guid objectGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			List<byte[]> list = new List<byte[]>(16);
			foreach (Guid guid in this.manager.GetActiveInstances())
			{
				AdminRpcClient adminRpcClient2;
				using (this.manager.GetAdminRpcClient(guid, "EcPurgeCachedMailboxObject50", out adminRpcClient2))
				{
					if (adminRpcClient2 != null)
					{
						try
						{
							ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
							byte[] item;
							adminRpcClient2.EcPurgeCachedMdbObject50(objectGuid, auxiliaryIn, out item);
							list.Add(item);
						}
						catch (FailRpcException exception)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
						}
						catch (RpcException exception2)
						{
							NullExecutionDiagnostics.Instance.OnExceptionCatch(exception2);
						}
					}
				}
			}
			auxiliaryOut = ProxyAdminRpcServer.MergeOutBuffers(list);
			((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).RefreshDatabaseInfo(NullExecutionContext.Instance, objectGuid);
			if (!this.manager.IsInstanceStarted(objectGuid))
			{
				((IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory).RefreshServerInfo(NullExecutionContext.Instance);
			}
			return (int)ErrorCode.NoError;
		}

		// Token: 0x06000016 RID: 22 RVA: 0x0000398C File Offset: 0x00001B8C
		public int EcAdminGetMailboxTable50(ClientSecurityContext callerSecurityContext, Guid? mdbGuid, int lparam, out byte[] result, uint[] propTags, uint cpid, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			int result2 = (int)ErrorCode.NoError;
			if (mdbGuid != null)
			{
				byte[] localResult = null;
				uint localRowCount = 0U;
				result2 = this.CallWorker(callerSecurityContext, mdbGuid.Value, "EcAdminGetMailboxTable50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
				{
					return instanceClient.EcAdminGetMailboxTable50(mdbGuid, lparam, out localResult, propTags, cpid, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
				}, out auxiliaryOut);
				result = localResult;
				rowCount = localRowCount;
			}
			else
			{
				List<byte[]> list = new List<byte[]>(16);
				List<byte[]> list2 = new List<byte[]>(16);
				rowCount = 0U;
				foreach (Guid guid in this.manager.GetActiveInstances())
				{
					AdminRpcClient adminRpcClient2;
					using (this.manager.GetAdminRpcClient(guid, "EcAdminGetMailboxTable50", out adminRpcClient2))
					{
						if (adminRpcClient2 != null)
						{
							ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, guid, ProxyAdminRpcServer.RequiredPrivilege.Administrator);
							try
							{
								byte[] array = null;
								byte[] item = null;
								uint num = 0U;
								ErrorCode errorCode = ErrorCode.CreateWithLid((LID)40392U, (ErrorCodeValue)adminRpcClient2.EcAdminGetMailboxTable50(mdbGuid, lparam, out array, propTags, cpid, out num, auxiliaryIn, out item));
								list2.Add(item);
								if (ErrorCode.NoError != errorCode)
								{
									rowCount = 0U;
									list.Clear();
									result2 = (int)errorCode;
									break;
								}
								if (ErrorCode.NoError == errorCode && array != null && array.Length > 0)
								{
									rowCount += num;
									list.Add(array);
								}
							}
							catch (RpcException exception)
							{
								NullExecutionDiagnostics.Instance.OnExceptionCatch(exception);
							}
						}
					}
				}
				auxiliaryOut = ProxyAdminRpcServer.MergeOutBuffers(list2);
				result = ProxyAdminRpcServer.MergeOutBuffers(list);
			}
			return result2;
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00003BFC File Offset: 0x00001DFC
		public int EcAdminNotifyOnDSChange50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint obj, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminNotifyOnDSChange50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminNotifyOnDSChange50(mdbGuid, mailboxGuid, obj, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00003C80 File Offset: 0x00001E80
		public int EcReadMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			byte[] localResponse = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcReadMdbEvents50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcReadMdbEvents50(mdbGuid, ref localMdbVersionGuid, request, out localResponse, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			response = localResponse;
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00003D18 File Offset: 0x00001F18
		public int EcSyncMailboxWithDS50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcSyncMailboxWithDS50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcSyncMailboxWithDS50(mdbGuid, mailboxGuid, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00003D98 File Offset: 0x00001F98
		public int EcDeleteMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			uint localDelCount = 0U;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcDeleteMdbWatermarksForConsumer50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcDeleteMdbWatermarksForConsumer50(mdbGuid, ref localMdbVersionGuid, mailboxDsGuid, consumerGuid, out localDelCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			delCount = localDelCount;
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00003E44 File Offset: 0x00002044
		public int EcDeleteMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out uint delCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			uint localDelCount = 0U;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcDeleteMdbWatermarksForMailbox50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcDeleteMdbWatermarksForMailbox50(mdbGuid, ref localMdbVersionGuid, mailboxDsGuid, out localDelCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			delCount = localDelCount;
			return result;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00003EE4 File Offset: 0x000020E4
		public int EcSaveMdbWatermarks50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcSaveMdbWatermarks50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcSaveMdbWatermarks50(mdbGuid, ref localMdbVersionGuid, wms, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			return result;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00003F80 File Offset: 0x00002180
		public int EcGetMdbWatermarksForConsumer50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid? mailboxDsGuid, Guid consumerGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			MDBEVENTWM[] localWms = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetMdbWatermarksForConsumer50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetMdbWatermarksForConsumer50(mdbGuid, ref localMdbVersionGuid, mailboxDsGuid, consumerGuid, out localWms, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			wms = localWms;
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000402C File Offset: 0x0000222C
		public int EcWriteMdbEvents50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			byte[] localResponse = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcWriteMdbEvents50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcWriteMdbEvents50(mdbGuid, ref localMdbVersionGuid, request, out localResponse, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			response = localResponse;
			return result;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000040D0 File Offset: 0x000022D0
		public int EcGetMdbWatermarksForMailbox50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, ref Guid mdbVersionGuid, Guid mailboxDsGuid, out MDBEVENTWM[] wms, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			Guid localMdbVersionGuid = mdbVersionGuid;
			MDBEVENTWM[] localWms = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetMdbWatermarksForMailbox50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetMdbWatermarksForMailbox50(mdbGuid, ref localMdbVersionGuid, mailboxDsGuid, out localWms, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mdbVersionGuid = localMdbVersionGuid;
			wms = localWms;
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00004168 File Offset: 0x00002368
		public int EcDoMaintenanceTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint task, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcDoMaintenanceTask50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcDoMaintenanceTask50(mdbGuid, task, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x0000420C File Offset: 0x0000240C
		public int EcGetLastBackupInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out long lastCompleteBackupTime, out long lastIncrementalBackupTime, out long lastDifferentialBackupTime, out long lastCopyBackupTime, out int snapFull, out int snapIncremental, out int snapDifferential, out int snapCopy, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			long localLastCompleteBackupTime = 0L;
			long localLastIncrementalBackupTime = 0L;
			long localLastDifferentialBackupTime = 0L;
			long localLastCopyBackupTime = 0L;
			int localSnapFull = 0;
			int localSnapIncremental = 0;
			int localSnapDifferential = 0;
			int localSnapCopy = 0;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetLastBackupInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetLastBackupInfo50(mdbGuid, out localLastCompleteBackupTime, out localLastIncrementalBackupTime, out localLastDifferentialBackupTime, out localLastCopyBackupTime, out localSnapFull, out localSnapIncremental, out localSnapDifferential, out localSnapCopy, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			lastCompleteBackupTime = localLastCompleteBackupTime;
			lastIncrementalBackupTime = localLastIncrementalBackupTime;
			lastDifferentialBackupTime = localLastDifferentialBackupTime;
			lastCopyBackupTime = localLastCopyBackupTime;
			snapFull = localSnapFull;
			snapIncremental = localSnapIncremental;
			snapDifferential = localSnapDifferential;
			snapCopy = localSnapCopy;
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000431C File Offset: 0x0000251C
		public int EcAdminGetMailboxTableEntry50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetMailboxTableEntry50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetMailboxTableEntry50(mdbGuid, mailboxGuid, propTags, cpid, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000043E4 File Offset: 0x000025E4
		public int EcAdminGetMailboxTableEntryFlags50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] propTags, uint cpid, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetMailboxTableEntryFlags50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetMailboxTableEntryFlags50(mdbGuid, mailboxGuid, flags, propTags, cpid, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000044C0 File Offset: 0x000026C0
		public int EcLogReplayRequest2(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint logReplayMax, uint logReplayFlags, out uint logReplayNext, out byte[] databaseInfo, out uint patchPageNumber, out byte[] patchToken, out byte[] patchData, out uint[] corruptPages, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			uint localLogReplayNext = 0U;
			byte[] localDatabaseInfo = null;
			uint localPatchPageNumber = 0U;
			byte[] localPatchToken = null;
			byte[] localPatchData = null;
			uint[] localCorruptPages = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcLogReplayRequestEx2", ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation, delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcLogReplayRequest2(mdbGuid, logReplayMax, logReplayFlags, out localLogReplayNext, out localDatabaseInfo, out localPatchPageNumber, out localPatchToken, out localPatchData, out localCorruptPages, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			logReplayNext = localLogReplayNext;
			databaseInfo = localDatabaseInfo;
			patchPageNumber = localPatchPageNumber;
			patchToken = localPatchToken;
			patchData = localPatchData;
			corruptPages = localCorruptPages;
			return result;
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000045A0 File Offset: 0x000027A0
		public int EcStartBlockModeReplicationToPassive50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string passiveName, uint highestGenSentToPassive, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcStartBlockModeReplicationToPassive50", ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation, delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcStartBlockModeReplicationToPassive50(mdbGuid, passiveName, highestGenSentToPassive, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x0000463C File Offset: 0x0000283C
		public int EcAdminGetViewsTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetViewsTableEx50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetViewsTableEx50(mdbGuid, mailboxGuid, folderLTID, propTags, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00004700 File Offset: 0x00002900
		public int EcAdminGetRestrictionTableEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, LTID folderLTID, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetRestrictionTableEx50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetRestrictionTableEx50(mdbGuid, mailboxGuid, folderLTID, propTags, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000047A4 File Offset: 0x000029A4
		public int EcAdminExecuteTask50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid taskClass, int taskId, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminExecuteTask50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminExecuteTask50(mdbGuid, taskClass, taskId, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000047F8 File Offset: 0x000029F8
		public int EcAdminGetFeatureVersion50(ClientSecurityContext callerSecurityContext, uint feature, out uint version, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ErrorCode errorCode = ErrorCode.NoError;
			switch (feature)
			{
			case 1U:
				version = 104U;
				break;
			case 2U:
				version = 1U;
				break;
			case 3U:
				version = 1U;
				break;
			case 4U:
				version = 4U;
				break;
			default:
				version = 0U;
				errorCode = ErrorCode.CreateInvalidParameter((LID)56440U);
				break;
			}
			auxiliaryOut = null;
			return (int)errorCode;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00004888 File Offset: 0x00002A88
		public int EcAdminGetMailboxSignature50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, out byte[] mailboxSignature, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localMailboxSignature = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetMailboxSignature50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetMailboxSignature50(mdbGuid, mailboxGuid, flags, out localMailboxSignature, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			mailboxSignature = localMailboxSignature;
			return result;
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00004920 File Offset: 0x00002B20
		public int EcAdminISIntegCheck50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint flags, uint[] taskIds, out string requestId, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			string localRequestId = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminISIntegCheck50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminISIntegCheck50(mdbGuid, mailboxGuid, flags, taskIds, out localRequestId, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			requestId = localRequestId;
			return result;
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000049B4 File Offset: 0x00002BB4
		public int EcMultiMailboxSearch(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] searchRequest, out byte[] searchResponse, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResponseByteArray = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcMultiMailboxSearch", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcMultiMailboxSearch(mdbGuid, searchRequest, out localResponseByteArray, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			searchResponse = localResponseByteArray;
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004A38 File Offset: 0x00002C38
		public int EcGetMultiMailboxSearchKeywordStats(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] keywordStatRequest, out byte[] searchResponse, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResponseByteArray = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcGetMultiMailboxSearchKeywordStats", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcGetMultiMailboxSearchKeywordStats(mdbGuid, keywordStatRequest, out localResponseByteArray, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			searchResponse = localResponseByteArray;
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00004AC4 File Offset: 0x00002CC4
		public int EcAdminGetResourceMonitorDigest50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propertyTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetResourceMonitorDigest50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetResourceMonitorDigest50(mdbGuid, propertyTags, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00004B60 File Offset: 0x00002D60
		public int EcAdminGetDatabaseProcessInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint[] propTags, out byte[] result, out uint rowCount, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResult = null;
			uint localRowCount = 0U;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetDatabaseProcessInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetDatabaseProcessInfo50(mdbGuid, propTags, out localResult, out localRowCount, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			rowCount = localRowCount;
			return result2;
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00004BF4 File Offset: 0x00002DF4
		public int EcAdminProcessSnapshotOperation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, uint operationCode, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminProcessSnapshotOperation50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminProcessSnapshotOperation50(mdbGuid, operationCode, flags, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00004C6C File Offset: 0x00002E6C
		public int EcAdminGetPhysicalDatabaseInformation50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, out byte[] databaseInfo, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localDatabaseInfo = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminGetPhysicalDatabaseInformation50", ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation, delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminGetPhysicalDatabaseInformation50(mdbGuid, out localDatabaseInfo, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			databaseInfo = localDatabaseInfo;
			return result;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00004CF0 File Offset: 0x00002EF0
		public int EcAdminPrePopulateCacheEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, byte[] partitionHint, string dcName, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminPrePopulateCacheEx50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminPrePopulateCacheEx50(mdbGuid, mailboxGuid, partitionHint, dcName, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00004D68 File Offset: 0x00002F68
		public int EcForceNewLog50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcForceNewLog50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcForceNewLog50(mdbGuid, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00004DE0 File Offset: 0x00002FE0
		public int EcAdminIntegrityCheckEx50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid mailboxGuid, uint operation, byte[] request, out byte[] response, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			byte[] localResponse = null;
			int result = this.CallWorker(callerSecurityContext, mdbGuid, "EcAdminIntegrityCheckEx50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcAdminIntegrityCheckEx50(mdbGuid, mailboxGuid, operation, request, out localResponse, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			response = localResponse;
			return result;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004E7C File Offset: 0x0000307C
		public int EcCreateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcCreateUserInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcCreateUserInfo50(mdbGuid, userInfoGuid, flags, properties, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004F0C File Offset: 0x0000310C
		public int EcReadUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, uint[] propertyTags, out ArraySegment<byte> result, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			ArraySegment<byte> localResult = Array<byte>.EmptySegment;
			int result2 = this.CallWorker(callerSecurityContext, mdbGuid, "EcReadUserInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcReadUserInfo50(mdbGuid, userInfoGuid, flags, propertyTags, out localResult, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
			result = localResult;
			return result2;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004FB4 File Offset: 0x000031B4
		public int EcUpdateUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] properties, uint[] deletePropertyTags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcUpdateUserInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcUpdateUserInfo50(mdbGuid, userInfoGuid, flags, properties, deletePropertyTags, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00005040 File Offset: 0x00003240
		public int EcDeleteUserInfo50(ClientSecurityContext callerSecurityContext, Guid mdbGuid, Guid userInfoGuid, uint flags, byte[] auxiliaryIn, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, "EcDeleteUserInfo50", delegate(AdminRpcClient instanceClient, out byte[] localAuxiliaryOut)
			{
				return instanceClient.EcDeleteUserInfo50(mdbGuid, userInfoGuid, flags, auxiliaryIn, out localAuxiliaryOut);
			}, out auxiliaryOut);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00005094 File Offset: 0x00003294
		internal static void CheckPermissions(ClientSecurityContext callerSecurityContext, Guid databaseGuid, ProxyAdminRpcServer.RequiredPrivilege privilege)
		{
			if (callerSecurityContext == null)
			{
				return;
			}
			if (callerSecurityContext.IsSystem)
			{
				return;
			}
			if (!callerSecurityContext.IsAuthenticated)
			{
				throw new FailRpcException("Unauthenticated caller.", -2147024891);
			}
			SecurityDescriptor securityDescriptor = null;
			IRpcProxyDirectory rpcProxyDirectory = (IRpcProxyDirectory)Microsoft.Exchange.Server.Storage.DirectoryServices.Globals.Directory;
			try
			{
				if (privilege == ProxyAdminRpcServer.RequiredPrivilege.Administrator)
				{
					securityDescriptor = rpcProxyDirectory.GetDatabaseSecurityDescriptor(NullExecutionContext.Instance, databaseGuid);
				}
				else if (privilege == ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation)
				{
					securityDescriptor = rpcProxyDirectory.GetServerSecurityDescriptor(NullExecutionContext.Instance);
				}
			}
			catch (StoreException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				throw new FailRpcException("Invalid database guid.", (int)ex.Error);
			}
			bool flag = false;
			if (privilege == ProxyAdminRpcServer.RequiredPrivilege.Administrator)
			{
				flag = SecurityHelper.CheckAdministrativeRights(callerSecurityContext, securityDescriptor);
			}
			else if (privilege == ProxyAdminRpcServer.RequiredPrivilege.ConstrainedDelegation)
			{
				flag = SecurityHelper.CheckConstrainedDelegationPrivilege(callerSecurityContext, securityDescriptor);
			}
			if (!flag)
			{
				throw new FailRpcException("Caller does not have enough privileges to perform the call.", -2147024891);
			}
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00005154 File Offset: 0x00003354
		internal static byte[] MergeOutBuffers(IList<byte[]> buffers)
		{
			int num = 0;
			foreach (byte[] array in buffers)
			{
				if (array != null)
				{
					num += array.Length;
				}
			}
			if (num == 0)
			{
				return null;
			}
			byte[] array2 = new byte[num];
			int num2 = 0;
			foreach (byte[] array3 in buffers)
			{
				if (array3 != null)
				{
					Buffer.BlockCopy(array3, 0, array2, num2, array3.Length);
					num2 += array3.Length;
				}
			}
			return array2;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00005204 File Offset: 0x00003404
		internal static void MergeInstanceStatus(byte[] resultStatus, int countMdbs, InstanceMdbStatus status, ref int fixedIndex, ref int variableIndex)
		{
			if (variableIndex == 0)
			{
				variableIndex = countMdbs * 52;
			}
			int maxPosition = countMdbs * 52;
			int num = (int)(status.Count * 52U);
			Array.Copy(status.Blob, 0, resultStatus, fixedIndex, num);
			if (num < status.Blob.Length)
			{
				Array.Copy(status.Blob, num, resultStatus, variableIndex, status.Blob.Length - num);
				int num2 = fixedIndex;
				int num3 = variableIndex - num;
				if (num3 > 0)
				{
					while (num2 - fixedIndex < num)
					{
						num2 += 36;
						ProxyAdminRpcServer.FixupStatusOffsetField(resultStatus, ref num2, maxPosition, num3);
						ProxyAdminRpcServer.FixupStatusOffsetField(resultStatus, ref num2, maxPosition, num3);
						ProxyAdminRpcServer.FixupStatusOffsetField(resultStatus, ref num2, maxPosition, num3);
						ProxyAdminRpcServer.FixupStatusOffsetField(resultStatus, ref num2, maxPosition, num3);
					}
				}
			}
			fixedIndex += num;
			variableIndex += status.Blob.Length - num;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x000052C0 File Offset: 0x000034C0
		private static void FixupStatusOffsetField(byte[] buffer, ref int position, int maxPosition, int fixupDelta)
		{
			int num = position;
			int num2 = (int)ParseSerialize.GetDword(buffer, ref num, maxPosition);
			if (num2 > 0)
			{
				num2 += fixupDelta;
				position += ParseSerialize.SerializeInt32(num2, buffer, position);
				return;
			}
			position += 4;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000052F7 File Offset: 0x000034F7
		private int CallWorker(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string methodName, ProxyAdminRpcServer.CallWorkerDelegate callDelegate, out byte[] auxiliaryOut)
		{
			return this.CallWorker(callerSecurityContext, mdbGuid, methodName, ProxyAdminRpcServer.RequiredPrivilege.Administrator, callDelegate, out auxiliaryOut);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00005308 File Offset: 0x00003508
		private int CallWorker(ClientSecurityContext callerSecurityContext, Guid mdbGuid, string methodName, ProxyAdminRpcServer.RequiredPrivilege requiredPrivilege, ProxyAdminRpcServer.CallWorkerDelegate callDelegate, out byte[] auxiliaryOut)
		{
			DiagnosticContext.Reset();
			auxiliaryOut = null;
			int num = 0;
			if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
			{
				ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, string.Format("ENTER CALL PROXY [ADMIN][{0}]", methodName));
			}
			try
			{
				AdminRpcClient adminRpcClient2;
				using (this.manager.GetAdminRpcClient(mdbGuid, methodName, out adminRpcClient2))
				{
					if (adminRpcClient2 != null)
					{
						ProxyAdminRpcServer.CheckPermissions(callerSecurityContext, mdbGuid, requiredPrivilege);
						num = callDelegate(adminRpcClient2, out auxiliaryOut);
					}
					else
					{
						num = (int)ErrorCode.CreateMdbNotInitialized((LID)49696U);
					}
				}
			}
			catch (FailRpcException ex)
			{
				NullExecutionDiagnostics.Instance.OnExceptionCatch(ex);
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					StringBuilder stringBuilder = new StringBuilder(1000);
					stringBuilder.Append(methodName);
					stringBuilder.Append(" threw an exception. mdb:[");
					stringBuilder.Append(mdbGuid);
					stringBuilder.Append("] exception:[");
					stringBuilder.Append(ex.ToString());
					stringBuilder.Append("]");
					ExTraceGlobals.ProxyAdminTracer.TraceDebug(0L, stringBuilder.ToString());
				}
				DiagnosticContext.TraceLocation((LID)48544U);
				num = ex.ErrorCode;
			}
			finally
			{
				if (auxiliaryOut == null && num != (int)ErrorCode.NoError && DiagnosticContext.HasData)
				{
					auxiliaryOut = this.ProduceAuxiliaryOutput();
				}
				if (ExTraceGlobals.ProxyAdminTracer.IsTraceEnabled(TraceType.FunctionTrace))
				{
					ExTraceGlobals.ProxyAdminTracer.TraceFunction(0L, string.Format("EXIT CALL PROXY [ADMIN][{0}]", methodName));
				}
			}
			return num;
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000054A0 File Offset: 0x000036A0
		private byte[] ProduceAuxiliaryOutput()
		{
			if (!DiagnosticContext.HasData)
			{
				return null;
			}
			AuxiliaryData auxiliaryData = AuxiliaryData.Parse(null);
			auxiliaryData.AppendOutput(new DiagCtxCtxDataAuxiliaryBlock(DiagnosticContext.PackInfo()));
			byte[] array = new byte[auxiliaryData.CalculateSerializedOutputSize()];
			int num;
			auxiliaryData.Serialize(new ArraySegment<byte>(array), out num);
			return array;
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000054E8 File Offset: 0x000036E8
		private void PublishFailureItem(Guid databaseGuid, FailureTag failureTag)
		{
			DatabaseFailureItem databaseFailureItem = new DatabaseFailureItem(FailureNameSpace.Store, failureTag, databaseGuid)
			{
				ComponentName = "StoreService",
				InstanceName = this.manager.GetInstanceDisplayName(databaseGuid)
			};
			databaseFailureItem.Publish();
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00005524 File Offset: 0x00003724
		private void AppendAuxiliaryOutput(byte[] auxiliaryOut)
		{
			List<AuxiliaryBlock> list = AuxiliaryData.ParseAuxiliaryBuffer(new ArraySegment<byte>(auxiliaryOut));
			foreach (AuxiliaryBlock auxiliaryBlock in list)
			{
				if (auxiliaryBlock is DiagCtxCtxDataAuxiliaryBlock)
				{
					DiagnosticContext.AppendToBuffer(((DiagCtxCtxDataAuxiliaryBlock)auxiliaryBlock).ContextData);
				}
			}
		}

		// Token: 0x0400000A RID: 10
		private IRpcInstanceManager manager;

		// Token: 0x0400000B RID: 11
		private TimeSpan instanceStatusTimeout;

		// Token: 0x0400000C RID: 12
		private Dictionary<Guid, CancellationTokenSource> mountInProgressSet;

		// Token: 0x02000006 RID: 6
		// (Invoke) Token: 0x06000043 RID: 67
		private delegate int CallWorkerDelegate(AdminRpcClient instanceClient, out byte[] auxiliaryOut);

		// Token: 0x02000007 RID: 7
		internal enum RequiredPrivilege
		{
			// Token: 0x0400000E RID: 14
			Administrator,
			// Token: 0x0400000F RID: 15
			ConstrainedDelegation
		}
	}
}
