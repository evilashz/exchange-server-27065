using System;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage.ActiveMonitoring;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Rpc;
using Microsoft.Exchange.Rpc.ActiveMonitoring;
using Microsoft.Exchange.Rpc.Cluster;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Common
{
	// Token: 0x02000566 RID: 1382
	internal sealed class ActiveMonitoringRpcServer : ActiveMonitoringRpcServerBase
	{
		// Token: 0x060022AB RID: 8875 RVA: 0x000D1D25 File Offset: 0x000CFF25
		public static void Start()
		{
			ActiveMonitoringRpcServer.Start(new ActiveMonitoringRpcServer.DiagnosticLogger(ActiveMonitoringRpcServer.NullDiagnosticLogger));
		}

		// Token: 0x060022AC RID: 8876 RVA: 0x000D1D38 File Offset: 0x000CFF38
		public static void Start(ActiveMonitoringRpcServer.DiagnosticLogger logDiagnosticInfo)
		{
			logDiagnosticInfo("Entering ActiveMonitoringRpcServer.Start()", new object[0]);
			FileSecurity fileSecurity = new FileSecurity();
			SecurityIdentifier securityIdentifier = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);
			FileSystemAccessRule accessRule = new FileSystemAccessRule(securityIdentifier, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.SetOwner(securityIdentifier);
			fileSecurity.SetAccessRule(accessRule);
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null);
			FileSystemAccessRule rule = new FileSystemAccessRule(identity, FileSystemRights.ReadData, AccessControlType.Allow);
			fileSecurity.AddAccessRule(rule);
			SecurityIdentifier securityIdentifier2 = null;
			try
			{
				logDiagnosticInfo("Querying Active Directory for Managed Availability Servers SID", new object[0]);
				securityIdentifier2 = DirectoryAccessor.GetManagedAvailabilityServersUsgSid();
				logDiagnosticInfo("Retrieved SID from AD successfully", new object[0]);
				ActiveMonitoringRpcServer.UpdateManagedAvailabilityServersUsgSidCache(securityIdentifier2);
			}
			catch (Exception ex)
			{
				logDiagnosticInfo("AD call failed. Exception message: {0}", new object[]
				{
					ex.ToString()
				});
				securityIdentifier2 = ActiveMonitoringRpcServer.ReadManagedAvailabilityServersUsgSidFromCache();
			}
			if (securityIdentifier2 != null)
			{
				FileSystemAccessRule rule2 = new FileSystemAccessRule(securityIdentifier2, FileSystemRights.ReadData, AccessControlType.Allow);
				fileSecurity.AddAccessRule(rule2);
				string name = "SOFTWARE\\Microsoft\\ExchangeServer";
				logDiagnosticInfo("Setting permissions on ExchangeServer reg key", new object[0]);
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(name, true))
				{
					if (registryKey != null)
					{
						RegistrySecurity accessControl = registryKey.GetAccessControl();
						accessControl.AddAccessRule(new RegistryAccessRule(securityIdentifier2, RegistryRights.FullControl, InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit, PropagationFlags.InheritOnly, AccessControlType.Allow));
						registryKey.SetAccessControl(accessControl);
					}
				}
			}
			logDiagnosticInfo("Publishing bugcheck info to crimson", new object[0]);
			Tuple<RecoveryActionEntry, RecoveryActionEntry> tuple = ForceRebootHelper.PublishRebootActionEntryIfRequired();
			RecoveryActionsRepository.Instance.Initialize(tuple, true, null);
			logDiagnosticInfo("Registering server", new object[0]);
			RpcServerBase.RegisterServer(typeof(ActiveMonitoringRpcServer), fileSecurity, 1);
			logDiagnosticInfo("Server registered", new object[0]);
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x000D1EEC File Offset: 0x000D00EC
		public static void Stop()
		{
			ActiveMonitoringRpcServerBase.StopServer();
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x000D1EF4 File Offset: 0x000D00F4
		public override int RequestMonitoring(Guid mdbGuid)
		{
			WTFDiagnostics.TraceFunction<Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Received monitoring request for mailbox database {0}", mdbGuid, null, "RequestMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 243);
			if (!ServerComponentStateManager.IsOnline(ServerComponentEnum.Monitoring))
			{
				WTFDiagnostics.TraceDebug(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Rejecting request because monitoring state is off", null, "RequestMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 247);
				return -2147417338;
			}
			string databaseActiveHost = DirectoryAccessor.Instance.GetDatabaseActiveHost(mdbGuid);
			if (string.IsNullOrWhiteSpace(databaseActiveHost))
			{
				WTFDiagnostics.TraceDebug<Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Host server name not found for MDB guid {0}", mdbGuid, null, "RequestMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 255);
				return -2147024809;
			}
			if (!DirectoryAccessor.Instance.IsServerCompatible(databaseActiveHost))
			{
				WTFDiagnostics.TraceDebug<string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Target server {0} is not compatible with this server. Rejecting the requst", databaseActiveHost, null, "RequestMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 262);
				return -2147417340;
			}
			if (MonitoringServerManager.TryAddDatabase(mdbGuid))
			{
				return 0;
			}
			return -2147417343;
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x000D1FE2 File Offset: 0x000D01E2
		public override void CancelMonitoring(Guid mdbGuid)
		{
			WTFDiagnostics.TraceFunction<Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Received request to stop monitoring for mailbox database {0}", mdbGuid, null, "CancelMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 284);
			MonitoringServerManager.RemoveDatabase(mdbGuid);
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x000D2010 File Offset: 0x000D0210
		public override int Heartbeat(string serverName, Guid mdbGuid)
		{
			return -2147417342;
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x000D2018 File Offset: 0x000D0218
		public override int RequestCredential(string serverName, Guid mdbGuid, string userPrincipalName, ref string credential)
		{
			WTFDiagnostics.TraceFunction<string, string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Received request from server {0} for mailbox {1} on database {2}", serverName, userPrincipalName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 313);
			int result;
			try
			{
				if (string.IsNullOrWhiteSpace(userPrincipalName))
				{
					WTFDiagnostics.TraceError<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox '' on database {1} because UPN is not specified", serverName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 320);
					result = -2147024809;
				}
				else if (!DirectoryAccessor.Instance.IsServerCompatible(serverName))
				{
					WTFDiagnostics.TraceError<string, string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox {1} on database {2} because requesting server is not part of the same monitoring group", serverName, userPrincipalName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 327);
					result = -2147417340;
				}
				else
				{
					MailboxDatabase mailboxDatabaseFromGuid = DirectoryAccessor.Instance.GetMailboxDatabaseFromGuid(mdbGuid);
					if (mailboxDatabaseFromGuid == null)
					{
						WTFDiagnostics.TraceError<string, string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox {1} on database {2} because the database does not exist", serverName, userPrincipalName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 335);
						result = -2147024809;
					}
					else if (!DirectoryAccessor.Instance.IsDatabaseCopyActiveOnLocalServer(mailboxDatabaseFromGuid))
					{
						WTFDiagnostics.TraceError<string, string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox {1} on database {2} because the active copy is not hosted here", serverName, userPrincipalName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 344);
						result = -2147417341;
					}
					else
					{
						ADUser aduser = DirectoryAccessor.Instance.SearchMonitoringMailbox(null, userPrincipalName, ref mailboxDatabaseFromGuid);
						if (aduser == null)
						{
							WTFDiagnostics.TraceError<string, string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox {1} on database {2} because the mailbox does not exist", serverName, userPrincipalName, mdbGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 352);
							result = -2147024809;
						}
						else if (aduser.Database.ObjectGuid != mdbGuid)
						{
							WTFDiagnostics.TraceError<string, string, Guid, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request from server {0} for mailbox {1} on database {2} because the mailbox actually lives on database {3}", serverName, userPrincipalName, mdbGuid, aduser.Database.ObjectGuid, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 359);
							result = -2147024809;
						}
						else
						{
							credential = DirectoryAccessor.Instance.GetMonitoringMailboxCredential(aduser);
							WTFDiagnostics.TraceDebug<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Credential request from server {0} for monitoring mailbox {1} returning credential {2}", serverName, userPrincipalName, credential, null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 365);
							result = 0;
						}
					}
				}
			}
			catch (Exception ex)
			{
				credential = ex.ToString();
				WTFDiagnostics.TraceError<string, string, string>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Unhandled exception servicing RPC from {0} to set credential on mailbox {1}: {2}", serverName, userPrincipalName, ex.ToString(), null, "RequestCredential", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 374);
				if (ex is WLCDPartnerException)
				{
					result = -2147417337;
				}
				else
				{
					result = -2147417336;
				}
			}
			return result;
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x000D22A4 File Offset: 0x000D04A4
		public override RpcErrorExceptionInfo GenericRequest(RpcGenericRequestInfo requestInfo, ref RpcGenericReplyInfo replyInfo)
		{
			RpcGenericReplyInfo tmpReplyInfo = null;
			RpcErrorExceptionInfo result = ActiveMonitoringRpcExceptionWrapper.Instance.RunRpcServerOperation(delegate()
			{
				ActiveMonitoringGenericRpcServerHelper.Dispatch(requestInfo, ref tmpReplyInfo);
			});
			if (tmpReplyInfo != null)
			{
				replyInfo = tmpReplyInfo;
			}
			return result;
		}

		// Token: 0x060022B3 RID: 8883 RVA: 0x000D22F0 File Offset: 0x000D04F0
		public override int CreateMonitoringMailbox(string displayName, Guid mdbGuid)
		{
			WTFDiagnostics.TraceFunction<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Received request to create monitoring mailbox {0} on database {1}", displayName, mdbGuid, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 424);
			if (string.IsNullOrWhiteSpace(displayName))
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request to create monitoring mailbox because Display Name is not specified", null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 428);
			}
			MailboxDatabase mailboxDatabaseFromGuid = DirectoryAccessor.Instance.GetMailboxDatabaseFromGuid(mdbGuid);
			if (mailboxDatabaseFromGuid == null)
			{
				WTFDiagnostics.TraceFunction<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Reject request to create monitoring mailbox {0} on database {1} because database does not exist", displayName, mdbGuid, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 434);
				return -2147024809;
			}
			string text = null;
			if (DirectoryAccessor.Instance.CreateMonitoringMailbox(displayName, mailboxDatabaseFromGuid, out text) == null)
			{
				WTFDiagnostics.TraceFunction<string, Guid>(ExTraceGlobals.CommonComponentsTracer, this.traceContext, "Attempt to create monitoring mailbox {0} on database {1} failed", displayName, mdbGuid, null, "CreateMonitoringMailbox", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Local\\Rpc\\ActiveMonitoringRpcServer.cs", 442);
				return -2147024809;
			}
			return 0;
		}

		// Token: 0x060022B4 RID: 8884 RVA: 0x000D23CF File Offset: 0x000D05CF
		private static void NullDiagnosticLogger(string message, params object[] messageArgs)
		{
		}

		// Token: 0x060022B5 RID: 8885 RVA: 0x000D23D4 File Offset: 0x000D05D4
		private static void UpdateManagedAvailabilityServersUsgSidCache(SecurityIdentifier managedAvailabilityServersSid)
		{
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(ActiveMonitoringRpcServer.RegistryPathBase))
			{
				registryKey.SetValue("ManagedAvailabilityServersUsgSid", managedAvailabilityServersSid.ToString());
			}
		}

		// Token: 0x060022B6 RID: 8886 RVA: 0x000D2420 File Offset: 0x000D0620
		private static SecurityIdentifier ReadManagedAvailabilityServersUsgSidFromCache()
		{
			SecurityIdentifier result;
			using (RegistryKey registryKey = Registry.LocalMachine.CreateSubKey(ActiveMonitoringRpcServer.RegistryPathBase))
			{
				string text = registryKey.GetValue("ManagedAvailabilityServersUsgSid") as string;
				if (!string.IsNullOrWhiteSpace(text))
				{
					result = new SecurityIdentifier(text);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x040018F5 RID: 6389
		private const string ManagedAvailabilityServersUsgSidValueName = "ManagedAvailabilityServersUsgSid";

		// Token: 0x040018F6 RID: 6390
		private static readonly string RegistryPathBase = "SOFTWARE\\Microsoft\\ExchangeServer\\v15\\ActiveMonitoring\\";

		// Token: 0x040018F7 RID: 6391
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x02000567 RID: 1383
		// (Invoke) Token: 0x060022BA RID: 8890
		public delegate void DiagnosticLogger(string message, params object[] messageArgs);
	}
}
