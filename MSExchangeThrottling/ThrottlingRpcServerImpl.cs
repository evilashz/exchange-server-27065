using System;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using Microsoft.Exchange.Conversion;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.Throttling;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000005 RID: 5
	internal sealed class ThrottlingRpcServerImpl : ThrottlingRpcServer
	{
		// Token: 0x06000020 RID: 32 RVA: 0x00002504 File Offset: 0x00000704
		public static bool TryCreateInstance(TimeSpan startupTimeout, out ThrottlingRpcServerImpl instance)
		{
			TimeSpan timeout = TimeSpan.FromMilliseconds(Math.Min(ThrottlingAppConfig.ADOperationTimeout.TotalMilliseconds, startupTimeout.TotalMilliseconds / 2.0));
			ObjectSecurity rpcSecurityDescriptor;
			if (!ThrottlingRpcServerImpl.TryGetRpcSecurityDescriptor(timeout, out rpcSecurityDescriptor))
			{
				instance = null;
				return false;
			}
			if (!ThrottlingRpcServerImpl.TryRegisterRpcServer(rpcSecurityDescriptor, out instance))
			{
				instance = null;
				return false;
			}
			instance.userSubmissionTokens = new TokenBucketMap<Guid>();
			instance.userSubmissionTokens.Start();
			instance.mailboxRuleSubmissionTokens = new TokenBucketMap<Guid>();
			instance.mailboxRuleSubmissionTokens.Start();
			return true;
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002588 File Offset: 0x00000788
		public override bool ObtainSubmissionTokens(Guid mailboxGuid, int requestedTokenCount, int totalTokenCount, int submissionType)
		{
			bool result;
			try
			{
				bool flag = this.ObtainTokens(mailboxGuid, RequestedAction.UserMailSubmission, requestedTokenCount, totalTokenCount, null, null, null);
				result = flag;
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = false;
			}
			return result;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000025C4 File Offset: 0x000007C4
		public override byte[] ObtainTokens(byte[] request)
		{
			byte[] result;
			try
			{
				RequestedAction requestedAction = RequestedAction.Invalid;
				Guid mailboxGuid = Guid.Empty;
				int requestedTokenCount = 0;
				int totalTokenCount = 0;
				string clientHostName = null;
				string clientProcessName = null;
				string clientType = null;
				MdbefPropertyCollection mdbefPropertyCollection = MdbefPropertyCollection.Create(request, 0, request.Length);
				object obj;
				if (mdbefPropertyCollection.TryGetValue(2415984712U, out obj) && obj is Guid)
				{
					mailboxGuid = (Guid)obj;
				}
				if (mdbefPropertyCollection.TryGetValue(2416050179U, out obj) && obj is int)
				{
					requestedAction = (RequestedAction)((int)obj);
				}
				if (mdbefPropertyCollection.TryGetValue(2416115715U, out obj) && obj is int)
				{
					requestedTokenCount = (int)obj;
				}
				if (mdbefPropertyCollection.TryGetValue(2416181251U, out obj) && obj is int)
				{
					totalTokenCount = (int)obj;
				}
				if (mdbefPropertyCollection.TryGetValue(2416312351U, out obj) && obj != null && obj is string)
				{
					clientHostName = (string)obj;
				}
				if (mdbefPropertyCollection.TryGetValue(2416377887U, out obj) && obj != null && obj is string)
				{
					clientProcessName = (string)obj;
				}
				if (mdbefPropertyCollection.TryGetValue(2416246815U, out obj) && obj != null && obj is string)
				{
					clientType = (string)obj;
				}
				bool flag = this.ObtainTokens(mailboxGuid, requestedAction, requestedTokenCount, totalTokenCount, clientHostName, clientProcessName, clientType);
				byte[] bytes = new MdbefPropertyCollection
				{
					{
						2566914059U,
						flag
					}
				}.GetBytes();
				result = bytes;
			}
			catch (Exception exception)
			{
				ExWatson.SendReportAndCrashOnAnotherThread(exception);
				result = null;
			}
			return result;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002750 File Offset: 0x00000950
		public void Stop()
		{
			ThrottlingService.StartStopBreadcrumbs.Drop("Stopping RPC Server", new object[0]);
			RpcServerBase.StopServer(ThrottlingRpcServer.RpcIntfHandle);
			ThrottlingService.StartStopBreadcrumbs.Drop("RPC Server stopped", new object[0]);
			this.userSubmissionTokens.Stop();
			this.userSubmissionTokens = null;
			this.mailboxRuleSubmissionTokens.Stop();
			this.mailboxRuleSubmissionTokens = null;
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000027B5 File Offset: 0x000009B5
		public void ExportData()
		{
			this.userSubmissionTokens.ExportData();
			this.mailboxRuleSubmissionTokens.ExportData();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000027FC File Offset: 0x000009FC
		private static bool TryGetRpcSecurityDescriptor(TimeSpan timeout, out ObjectSecurity sd)
		{
			ObjectSecurity tmp = null;
			sd = null;
			ThrottlingService.StartStopBreadcrumbs.Drop("Obtaining RPC security descriptor", new object[0]);
			ThrottlingService.Tracer.TraceDebug(0L, "Obtaining RPC security descriptor");
			ADOperationResult result = null;
			Thread thread = new Thread(delegate()
			{
				result = ADNotificationAdapter.TryRunADOperation(delegate()
				{
					tmp = ThrottlingRpcServerImpl.GetRpcSecurityDescriptor();
				}, 3);
			});
			thread.Start();
			if (!thread.Join(timeout))
			{
				result = new ADOperationResult(ADOperationErrorCode.PermanentError, new TimeoutException(string.Format("Timed out AD operation after {0}", timeout.ToString())));
			}
			if (result.Succeeded)
			{
				ThrottlingService.StartStopBreadcrumbs.Drop("Successfully obtained RPC security descriptor", new object[0]);
				ThrottlingService.Tracer.TraceDebug(0L, "Successfully obtained RPC security descriptor");
				sd = tmp;
				return true;
			}
			ThrottlingService.StartStopBreadcrumbs.Drop("Failed to obtain RPC security descriptor. Exception: {0}", new object[]
			{
				result.Exception
			});
			ThrottlingService.Tracer.TraceError<Exception>(0L, "Failed to obtain RPC security descriptor. Exception: {0}", result.Exception);
			ThrottlingService.EventLogger.LogEvent(ThrottlingServiceEventLogConstants.Tuple_RpcSecurityDescriptorFailure, null, new object[]
			{
				result.Exception
			});
			return false;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002938 File Offset: 0x00000B38
		private static ObjectSecurity GetRpcSecurityDescriptor()
		{
			FileSecurity fileSecurity = new FileSecurity();
			IRootOrganizationRecipientSession rootOrganizationRecipientSession = DirectorySessionFactory.Default.CreateRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 348, "GetRpcSecurityDescriptor", "f:\\15.00.1497\\sources\\dev\\data\\src\\ThrottlingService\\Service\\ThrottlingRpcServerImpl.cs");
			ThrottlingService.StartStopBreadcrumbs.Drop("Calling GetExchangeServersUsgSid", new object[0]);
			SecurityIdentifier exchangeServersUsgSid = rootOrganizationRecipientSession.GetExchangeServersUsgSid();
			ThrottlingService.StartStopBreadcrumbs.Drop("GetExchangeServersUsgSid call completed", new object[0]);
			FileSystemAccessRule fileSystemAccessRule = new FileSystemAccessRule(exchangeServersUsgSid, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetAccessRule(fileSystemAccessRule);
			if (ThrottlingAppConfig.AuthenticatedUsersRpcEnabled)
			{
				SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null);
				fileSystemAccessRule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.AddAccessRule(fileSystemAccessRule);
				ThrottlingService.Tracer.TraceDebug(0L, "RPC calls are allowed for all authenticated users.");
			}
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			SecurityIdentifier identity2 = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			fileSystemAccessRule = new FileSystemAccessRule(identity2, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(fileSystemAccessRule);
			fileSecurity.SetOwner(securityIdentifier);
			return fileSecurity;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002A20 File Offset: 0x00000C20
		private static uint GetThreadPoolSize()
		{
			int result;
			int num;
			ThreadPool.GetMaxThreads(out result, out num);
			return (uint)result;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002A38 File Offset: 0x00000C38
		private static bool TryRegisterRpcServer(ObjectSecurity rpcSecurityDescriptor, out ThrottlingRpcServerImpl serverInstance)
		{
			ThrottlingService.StartStopBreadcrumbs.Drop("Trying to register RPC server", new object[0]);
			ThrottlingService.Tracer.TraceDebug(0L, "Trying to register RPC server");
			serverInstance = null;
			try
			{
				serverInstance = (ThrottlingRpcServerImpl)RpcServerBase.RegisterServer(typeof(ThrottlingRpcServerImpl), rpcSecurityDescriptor, 1, false, ThrottlingRpcServerImpl.GetThreadPoolSize());
			}
			catch (RpcException ex)
			{
				ThrottlingService.StartStopBreadcrumbs.Drop("Failed to register RPC server: {0}", new object[]
				{
					ex
				});
				ThrottlingService.Tracer.TraceError<RpcException>(0L, "Failed to register RPC server: {0}", ex);
				ThrottlingService.EventLogger.LogEvent(ThrottlingServiceEventLogConstants.Tuple_RegisterRpcServerFailure, null, new object[]
				{
					ex
				});
				return false;
			}
			ThrottlingService.StartStopBreadcrumbs.Drop("Successfully registered RPC server", new object[0]);
			ThrottlingService.Tracer.TraceDebug(0L, "Successfully registered RPC server");
			return true;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002B18 File Offset: 0x00000D18
		private bool ObtainTokens(Guid mailboxGuid, RequestedAction requestedAction, int requestedTokenCount, int totalTokenCount, string clientHostName, string clientProcessName, string clientType)
		{
			ThrottlingService.Tracer.TraceDebug(0L, "ObtainTokens() request: mailboxGuid=<{0}>; requestedAction={1}; requestedTokenCount={2}; totalTokenCount={3}", new object[]
			{
				mailboxGuid,
				requestedAction,
				requestedTokenCount,
				totalTokenCount
			});
			ObtainTokensRequest<Guid> request = new ObtainTokensRequest<Guid>(mailboxGuid, requestedTokenCount, totalTokenCount, requestedAction, clientHostName, clientProcessName, clientType);
			bool flag;
			if (totalTokenCount < 1 || requestedTokenCount < 1 || requestedTokenCount > totalTokenCount)
			{
				ThrottlingService.Tracer.TraceError(0L, "ObtainSubmissionTokens(): invalid arguments supplied");
				flag = false;
			}
			else if (requestedAction == RequestedAction.UserMailSubmission)
			{
				flag = this.userSubmissionTokens.ObtainTokens(request);
			}
			else if (requestedAction == RequestedAction.MailboxRuleMailSubmission)
			{
				flag = this.mailboxRuleSubmissionTokens.ObtainTokens(request);
			}
			else
			{
				ThrottlingServiceLog.LogThrottlingBypassed<Guid>(request);
				ThrottlingService.Tracer.TraceDebug<RequestedAction>(0L, "Action {1} is not supported, therefor is allowed by default", requestedAction);
				flag = true;
			}
			if (flag)
			{
				ThrottlingService.Tracer.TraceDebug(0L, "ObtainTokens(): request allowed");
			}
			else
			{
				ThrottlingService.EventLogger.LogEvent(ThrottlingServiceEventLogConstants.Tuple_MailboxThrottled, mailboxGuid.ToString(), new object[]
				{
					mailboxGuid,
					clientHostName,
					requestedAction,
					requestedTokenCount,
					totalTokenCount,
					clientType,
					clientProcessName
				});
				ThrottlingService.Tracer.TraceDebug(0L, "ObtainTokens(): request denied");
			}
			return flag;
		}

		// Token: 0x04000016 RID: 22
		private const int ADRetryCount = 3;

		// Token: 0x04000017 RID: 23
		private TokenBucketMap<Guid> userSubmissionTokens;

		// Token: 0x04000018 RID: 24
		private TokenBucketMap<Guid> mailboxRuleSubmissionTokens;
	}
}
