using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage.Cluster
{
	// Token: 0x02000318 RID: 792
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal abstract class HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException> : IHaRpcExceptionWrapper where TBaseException : LocalizedException, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal where TBaseTransientException : TransientException, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal
	{
		// Token: 0x06002383 RID: 9091 RVA: 0x00090C9C File Offset: 0x0008EE9C
		public void ClientRetryableOperation(string serverName, RpcClientOperation rpcOperation)
		{
			if (rpcOperation == null)
			{
				throw new ArgumentNullException("rpcOperation");
			}
			string serverName2 = HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SanitizeServerName(serverName);
			int num = 0;
			int num2 = 0;
			int num3 = 0;
			bool flag;
			do
			{
				flag = false;
				try
				{
					rpcOperation();
				}
				catch (RpcException ex)
				{
					if (this.ClientShouldRetryOnError(ex, serverName2, ref num, ref num2, ref num3))
					{
						flag = true;
					}
					else
					{
						this.ClientHandleRpcException(ex, serverName2);
					}
				}
			}
			while (flag);
		}

		// Token: 0x06002384 RID: 9092 RVA: 0x00090D08 File Offset: 0x0008EF08
		public void ClientRethrowIfFailed(string serverName, RpcErrorExceptionInfo errorInfo)
		{
			this.ClientRethrowIfFailed(null, serverName, errorInfo);
		}

		// Token: 0x06002385 RID: 9093 RVA: 0x00090D14 File Offset: 0x0008EF14
		public void ClientRethrowIfFailed(string databaseName, string serverName, RpcErrorExceptionInfo errorInfo)
		{
			Exception ex = null;
			string text = HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SanitizeServerName(serverName);
			if (errorInfo.IsFailed())
			{
				if (errorInfo.ReconstitutedException != null)
				{
					ex = this.ConstructClientExceptionFromServerException(text, errorInfo.ReconstitutedException);
				}
				else
				{
					if (errorInfo.SerializedException != null && errorInfo.SerializedException.Length > 0)
					{
						try
						{
							errorInfo.ReconstitutedException = SerializationServices.Deserialize<Exception>(errorInfo.SerializedException);
							ex = this.ConstructClientExceptionFromServerException(text, errorInfo.ReconstitutedException);
							goto IL_109;
						}
						catch (SerializationException innerException)
						{
							ex = this.GetGenericOperationFailedException(errorInfo.ErrorMessage, innerException);
							((TBaseException)((object)ex)).OriginatingServer = text;
							goto IL_109;
						}
						catch (TargetInvocationException innerException2)
						{
							ex = this.GetGenericOperationFailedException(errorInfo.ErrorMessage, innerException2);
							((TBaseException)((object)ex)).OriginatingServer = text;
							goto IL_109;
						}
					}
					if (!string.IsNullOrEmpty(errorInfo.ErrorMessage))
					{
						ex = this.GetGenericOperationFailedException(errorInfo.ErrorMessage);
						((TBaseException)((object)ex)).OriginatingServer = text;
					}
					else
					{
						ex = this.GetGenericOperationFailedWithEcException(errorInfo.ErrorCode);
						((TBaseException)((object)ex)).OriginatingServer = text;
					}
				}
				IL_109:
				IHaRpcServerBaseException ex2 = ex as IHaRpcServerBaseException;
				if (ex2 != null && string.IsNullOrEmpty(ex2.DatabaseName) && !string.IsNullOrEmpty(databaseName))
				{
					((IHaRpcServerBaseExceptionInternal)ex).DatabaseName = databaseName;
				}
			}
			if (ex != null)
			{
				throw ex;
			}
		}

		// Token: 0x06002386 RID: 9094 RVA: 0x00090E7C File Offset: 0x0008F07C
		public RpcErrorExceptionInfo RunRpcServerOperation(RpcServerOperation rpcOperation)
		{
			return this.RunRpcServerOperation(null, rpcOperation);
		}

		// Token: 0x06002387 RID: 9095 RVA: 0x00090E88 File Offset: 0x0008F088
		public RpcErrorExceptionInfo RunRpcServerOperation(string databaseName, RpcServerOperation rpcOperation)
		{
			RpcErrorExceptionInfo result = new RpcErrorExceptionInfo();
			try
			{
				try
				{
					rpcOperation();
				}
				catch (MapiRetryableException ex)
				{
					result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex);
				}
				catch (MapiPermanentException ex2)
				{
					result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex2);
				}
				catch (TBaseTransientException ex3)
				{
					TBaseTransientException ex4 = (TBaseTransientException)((object)ex3);
					result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex4);
				}
				catch (TBaseException ex5)
				{
					TBaseException ex6 = (TBaseException)((object)ex5);
					result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex6);
				}
				catch (TransientException ex7)
				{
					result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex7);
				}
				catch (Exception ex8)
				{
					if (this.IsKnownException(ex8))
					{
						result = this.ConvertExceptionToErrorExceptionInfo(databaseName, ex8);
					}
					else
					{
						ExWatson.SendReportAndCrashOnAnotherThread(ex8);
					}
				}
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
			}
			return result;
		}

		// Token: 0x06002388 RID: 9096 RVA: 0x00090F80 File Offset: 0x0008F180
		internal virtual bool IsKnownException(Exception ex)
		{
			return false;
		}

		// Token: 0x06002389 RID: 9097
		protected abstract TBaseException GetGenericOperationFailedException(string message);

		// Token: 0x0600238A RID: 9098
		protected abstract TBaseException GetGenericOperationFailedException(string message, Exception innerException);

		// Token: 0x0600238B RID: 9099
		protected abstract TBaseException GetGenericOperationFailedWithEcException(int errorCode);

		// Token: 0x0600238C RID: 9100
		protected abstract TBaseTransientException GetGenericOperationFailedTransientException(string message, Exception innerException);

		// Token: 0x0600238D RID: 9101
		protected abstract TBaseException GetServiceDownException(string serverName, Exception innerException);

		// Token: 0x0600238E RID: 9102 RVA: 0x00090F83 File Offset: 0x0008F183
		public RpcErrorExceptionInfo ConvertExceptionToErrorExceptionInfo(Exception ex)
		{
			return this.ConvertExceptionToErrorExceptionInfo(null, ex);
		}

		// Token: 0x0600238F RID: 9103 RVA: 0x00090F90 File Offset: 0x0008F190
		public RpcErrorExceptionInfo ConvertExceptionToErrorExceptionInfo(string databaseName, Exception ex)
		{
			RpcErrorExceptionInfo rpcErrorExceptionInfo = new RpcErrorExceptionInfo();
			if (ex == null)
			{
				return rpcErrorExceptionInfo;
			}
			TBaseException ex2 = ex as TBaseException;
			TBaseTransientException ex3 = ex as TBaseTransientException;
			string errorMessage;
			if (ex2 != null)
			{
				errorMessage = HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SetExceptionProperties<TBaseException>(ex2, true, databaseName);
			}
			else if (ex3 != null)
			{
				errorMessage = HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SetExceptionProperties<TBaseTransientException>(ex3, true, databaseName);
			}
			else
			{
				errorMessage = ex.ToString();
			}
			rpcErrorExceptionInfo.ErrorMessage = errorMessage;
			rpcErrorExceptionInfo.ReconstitutedException = ex;
			HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.TrySerializeException(ex, rpcErrorExceptionInfo);
			return rpcErrorExceptionInfo;
		}

		// Token: 0x06002390 RID: 9104 RVA: 0x00091008 File Offset: 0x0008F208
		private static string SetExceptionProperties<T>(T exception, bool fSetDatabaseName, string databaseName) where T : Exception, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal
		{
			string errorMessage = exception.ErrorMessage;
			exception.OriginatingStackTrace = exception.StackTrace;
			if (fSetDatabaseName)
			{
				exception.DatabaseName = databaseName;
			}
			return errorMessage;
		}

		// Token: 0x06002391 RID: 9105 RVA: 0x0009104C File Offset: 0x0008F24C
		private static void TrySerializeException(Exception ex, RpcErrorExceptionInfo errorInfo)
		{
			Exception ex2 = null;
			try
			{
				errorInfo.SerializedException = SerializationServices.Serialize(ex);
			}
			catch (SerializationException ex3)
			{
				ex2 = ex3;
			}
			catch (TargetInvocationException ex4)
			{
				ex2 = ex4;
			}
			if (ex2 != null)
			{
				ExTraceGlobals.ReplayServiceRpcTracer.TraceError<Type, Exception>(0L, "ConvertExceptionToErrorExceptionInfo: Failed to serialize Exception of type '{0}'. Serialization Exception: {1}", ex.GetType(), ex2);
				errorInfo.ErrorMessage = ex.ToString();
			}
		}

		// Token: 0x06002392 RID: 9106 RVA: 0x000910B8 File Offset: 0x0008F2B8
		private static string SanitizeServerName(string serverName)
		{
			if (!string.IsNullOrEmpty(serverName))
			{
				return serverName;
			}
			return Environment.MachineName;
		}

		// Token: 0x06002393 RID: 9107 RVA: 0x000910CC File Offset: 0x0008F2CC
		private Exception ConstructClientExceptionFromServerException(string originatingServerName, Exception serverException)
		{
			TBaseException ex = serverException as TBaseException;
			TBaseTransientException ex2 = serverException as TBaseTransientException;
			if (ex == null && ex2 == null)
			{
				if (this.IsKnownException(serverException))
				{
					return serverException;
				}
				if (serverException is TransientException)
				{
					ex2 = this.GetGenericOperationFailedTransientException(serverException.Message, serverException);
					HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SetExceptionProperties<TBaseTransientException>(ex2, false, null);
				}
				else
				{
					ex = this.GetGenericOperationFailedException(serverException.Message, serverException);
					HaRpcExceptionWrapperBase<TBaseException, TBaseTransientException>.SetExceptionProperties<TBaseException>(ex, false, null);
				}
			}
			Exception result;
			if (ex != null)
			{
				result = ex;
				this.SetExceptionServerNameIfEmpty<TBaseException>(ex, originatingServerName);
			}
			else
			{
				result = ex2;
				this.SetExceptionServerNameIfEmpty<TBaseTransientException>(ex2, originatingServerName);
			}
			return result;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x00091171 File Offset: 0x0008F371
		private void SetExceptionServerNameIfEmpty<T>(T exception, string serverName) where T : Exception, IHaRpcServerBaseException, IHaRpcServerBaseExceptionInternal
		{
			if (string.IsNullOrEmpty(exception.OriginatingServer))
			{
				exception.OriginatingServer = serverName;
			}
		}

		// Token: 0x06002395 RID: 9109 RVA: 0x00091194 File Offset: 0x0008F394
		private bool ClientShouldRetryOnError(RpcException rpcEx, string serverName, ref int callFailedCount, ref int clientRetryErrorCount, ref int clientRetryAndWaitErrorCount)
		{
			int errorCode = rpcEx.ErrorCode;
			bool flag;
			if (errorCode > 1130)
			{
				switch (errorCode)
				{
				case 1717:
				case 1722:
					break;
				case 1718:
				case 1719:
				case 1720:
				case 1724:
				case 1725:
					goto IL_B4;
				case 1721:
				case 1723:
					goto IL_93;
				case 1726:
				case 1727:
					callFailedCount++;
					flag = (callFailedCount < 6);
					goto IL_B6;
				default:
					if (errorCode != 1753)
					{
						if (errorCode != 1791)
						{
							goto IL_B4;
						}
						goto IL_93;
					}
					break;
				}
				clientRetryErrorCount++;
				flag = (clientRetryErrorCount < 2);
				goto IL_B6;
			}
			if (errorCode != 14 && errorCode != 164 && errorCode != 1130)
			{
				goto IL_B4;
			}
			IL_93:
			clientRetryAndWaitErrorCount++;
			flag = (clientRetryAndWaitErrorCount < 4);
			if (flag)
			{
				Thread.Sleep(250 * clientRetryAndWaitErrorCount);
				goto IL_B6;
			}
			goto IL_B6;
			IL_B4:
			flag = false;
			IL_B6:
			ExTraceGlobals.ReplayServiceRpcTracer.TraceError(0L, "RPC exception occurred while contacting server '{0}'. fRetry={1}, callFailedCount={2}, clientRetryErrorCount={3}, clientRetryAndWaitErrorCount={4}; Ex: {5}", new object[]
			{
				serverName,
				flag,
				callFailedCount,
				clientRetryErrorCount,
				clientRetryAndWaitErrorCount,
				rpcEx
			});
			return flag;
		}

		// Token: 0x06002396 RID: 9110 RVA: 0x000912A4 File Offset: 0x0008F4A4
		private void ClientHandleRpcException(RpcException ex, string serverName)
		{
			if (ex == null)
			{
				return;
			}
			ExTraceGlobals.ReplayServiceRpcTracer.TraceError<string, RpcException>(0L, "RPC exception occurred while contacting server '{0}'. Ex: {1}", serverName, ex);
			if (ReplayRpcErrorCode.IsRpcConnectionError(ex.ErrorCode))
			{
				TBaseException serviceDownException = this.GetServiceDownException(serverName, ex);
				throw serviceDownException;
			}
			TBaseException genericOperationFailedException = this.GetGenericOperationFailedException(ex.Message, ex);
			throw genericOperationFailedException;
		}

		// Token: 0x04001512 RID: 5394
		private const int MaxClientRetryCount = 2;

		// Token: 0x04001513 RID: 5395
		private const int MaxClientRetryAndWaitCount = 4;

		// Token: 0x04001514 RID: 5396
		private const int ClientRetryAndWaitDelayInMsec = 250;

		// Token: 0x04001515 RID: 5397
		private const int MaxCallFailedRetryCount = 6;

		// Token: 0x04001516 RID: 5398
		private const int RPC_S_CALL_FAILED = 1726;

		// Token: 0x04001517 RID: 5399
		private const int RPC_S_CALL_FAILED_DNE = 1727;

		// Token: 0x04001518 RID: 5400
		private const int RPC_S_SERVER_UNAVAILABLE = 1722;

		// Token: 0x04001519 RID: 5401
		private const int EPT_S_NOT_REGISTERED = 1753;

		// Token: 0x0400151A RID: 5402
		private const int RPC_S_UNKNOWN_IF = 1717;

		// Token: 0x0400151B RID: 5403
		private const int RPC_S_CALL_IN_PROGRESS = 1791;

		// Token: 0x0400151C RID: 5404
		private const int RPC_S_OUT_OF_RESOURCES = 1721;

		// Token: 0x0400151D RID: 5405
		private const int RPC_S_OUT_OF_THREADS = 164;

		// Token: 0x0400151E RID: 5406
		private const int RPC_S_SERVER_OUT_OF_MEMORY = 1130;

		// Token: 0x0400151F RID: 5407
		private const int RPC_S_OUT_OF_MEMORY = 14;

		// Token: 0x04001520 RID: 5408
		private const int RPC_S_SERVER_TOO_BUSY = 1723;
	}
}
