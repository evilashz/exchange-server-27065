using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Forefront.RecoveryActionArbiter.Contract;
using Microsoft.Win32;

namespace Microsoft.Exchange.Monitoring.ServiceContextProvider
{
	// Token: 0x02000002 RID: 2
	public sealed class ServiceContextProvider
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		private ServiceContextProvider()
		{
			string forefrontArbitrationServiceUrl = DatacenterRegistry.GetForefrontArbitrationServiceUrl();
			if (DatacenterRegistry.IsForefrontForOffice() && forefrontArbitrationServiceUrl != string.Empty)
			{
				this.SetRaaServiceStrategy(new RaaServiceStrategy());
				return;
			}
			this.SetRaaServiceStrategy(new RaaServiceNoOpStrategy());
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000002 RID: 2 RVA: 0x00002114 File Offset: 0x00000314
		public static ServiceContextProvider Instance
		{
			get
			{
				if (ServiceContextProvider.instance == null)
				{
					lock (ServiceContextProvider.syncRoot)
					{
						if (ServiceContextProvider.instance == null)
						{
							ServiceContextProvider.instance = new ServiceContextProvider();
						}
					}
				}
				return ServiceContextProvider.instance;
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002174 File Offset: 0x00000374
		internal static RegistryKey ServiceContextProviderRegistryKey
		{
			get
			{
				RegistryKey registryKey;
				if (ServiceContextProvider.UseExchangeLabsRegistryKey)
				{
					registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\ExchangeLabs", true);
				}
				else
				{
					registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft", true);
				}
				using (registryKey)
				{
					if (registryKey != null)
					{
						RegistryKey registryKey3 = registryKey.OpenSubKey("ServiceContextProvider", true);
						if (registryKey3 == null)
						{
							registryKey3 = registryKey.CreateSubKey("ServiceContextProvider");
						}
						return registryKey3;
					}
				}
				return null;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000004 RID: 4 RVA: 0x000021F4 File Offset: 0x000003F4
		// (set) Token: 0x06000005 RID: 5 RVA: 0x000021FB File Offset: 0x000003FB
		internal static bool UseExchangeLabsRegistryKey { get; set; }

		// Token: 0x06000006 RID: 6 RVA: 0x00002204 File Offset: 0x00000404
		public static bool RecoveryRequestExists(string recoveryID)
		{
			bool result;
			using (RegistryKey registryKey = ServiceContextProvider.ServiceContextProviderRegistryKey.OpenSubKey(recoveryID.ToString()))
			{
				result = (registryKey != null);
			}
			return result;
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002248 File Offset: 0x00000448
		public bool RequestApprovalForRecovery(string recoveryID, RecoveryFlags recoveryFlags, string failureReason)
		{
			return this.RequestApprovalForRecovery(recoveryID, 2, 1, 0, recoveryFlags, failureReason, "");
		}

		// Token: 0x06000008 RID: 8 RVA: 0x0000225C File Offset: 0x0000045C
		public bool RequestApprovalForRecovery(string recoveryID, ArbitrationScope scope, ArbitrationSource source, RequestedAction requestedAction, RecoveryFlags recoveryFlags, string failureReason, string machineName = "")
		{
			ApprovalRequest approvalRequest = new ApprovalRequest();
			approvalRequest.MachineName = (string.IsNullOrEmpty(machineName) ? Environment.MachineName : machineName);
			approvalRequest.RecoveryFlags = recoveryFlags;
			approvalRequest.FailureReason = failureReason;
			approvalRequest.ArbitrationScope = scope;
			approvalRequest.ArbitrationSource = source;
			approvalRequest.RequestedAction = requestedAction;
			approvalRequest.FailureCategory = 4;
			ApprovalResponse approvalResponse = this.raaServiceStrategy.RequestApprovalForRecovery(approvalRequest);
			if (approvalResponse.ArbitrationResult == 1)
			{
				ServiceContextProvider.SaveRecoveryRequest(recoveryID);
				return true;
			}
			return false;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000022D5 File Offset: 0x000004D5
		public void NotifyRecoveryCompletion(string recoveryID, bool recoverySucceeded, string machineName = "")
		{
			if (ServiceContextProvider.RecoveryRequestExists(recoveryID))
			{
				if (ServiceContextProvider.RecoveryRequestCount() == 1)
				{
					this.raaServiceStrategy.NotifyRecoveryCompletion(string.IsNullOrEmpty(machineName) ? Environment.MachineName : machineName, recoverySucceeded);
				}
				ServiceContextProvider.RemoveRecoveryRequest(recoveryID);
			}
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002309 File Offset: 0x00000509
		internal void SetRaaServiceStrategy(IRaaService strategy)
		{
			this.raaServiceStrategy = strategy;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002314 File Offset: 0x00000514
		private static void SaveRecoveryRequest(string recoveryID)
		{
			using (RegistryKey serviceContextProviderRegistryKey = ServiceContextProvider.ServiceContextProviderRegistryKey)
			{
				RegistryKey registryKey = serviceContextProviderRegistryKey.CreateSubKey(recoveryID.ToString());
				registryKey.Dispose();
			}
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002358 File Offset: 0x00000558
		private static void RemoveRecoveryRequest(string recoveryID)
		{
			ServiceContextProvider.ServiceContextProviderRegistryKey.DeleteSubKey(recoveryID.ToString());
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000236C File Offset: 0x0000056C
		private static int RecoveryRequestCount()
		{
			int subKeyCount;
			using (RegistryKey serviceContextProviderRegistryKey = ServiceContextProvider.ServiceContextProviderRegistryKey)
			{
				subKeyCount = serviceContextProviderRegistryKey.SubKeyCount;
			}
			return subKeyCount;
		}

		// Token: 0x04000001 RID: 1
		private static volatile ServiceContextProvider instance;

		// Token: 0x04000002 RID: 2
		private static object syncRoot = new object();

		// Token: 0x04000003 RID: 3
		private IRaaService raaServiceStrategy;
	}
}
