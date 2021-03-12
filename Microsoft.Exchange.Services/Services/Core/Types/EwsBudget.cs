using System;
using System.Linq;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Services;
using Microsoft.Exchange.Services.DispatchPipe.Ews;
using Microsoft.Exchange.Services.Wcf;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020003DA RID: 986
	internal class EwsBudget : StandardBudget
	{
		// Token: 0x06001B9A RID: 7066 RVA: 0x0009CBB8 File Offset: 0x0009ADB8
		public static IEwsBudget Acquire(CallContext callContext)
		{
			IEwsBudget result;
			try
			{
				ExternalCallContext externalCallContext = callContext as ExternalCallContext;
				BudgetKey key;
				if (externalCallContext != null)
				{
					key = EwsBudget.GetExternalCallContextBudgetKey(externalCallContext);
				}
				else
				{
					key = EwsBudget.GetCallContextBudgetKey(callContext);
				}
				IEwsBudget ewsBudget = EwsBudget.Acquire(key);
				result = ewsBudget;
			}
			catch (OverBudgetException exception)
			{
				throw FaultExceptionUtilities.CreateFault(exception, FaultParty.Sender);
			}
			return result;
		}

		// Token: 0x06001B9B RID: 7067 RVA: 0x0009CC08 File Offset: 0x0009AE08
		public new static IEwsBudget Acquire(BudgetKey key)
		{
			EwsBudget innerBudget = EwsBudgetCache.Singleton.Get(key);
			return new EwsBudgetWrapper(innerBudget);
		}

		// Token: 0x06001B9C RID: 7068 RVA: 0x0009CC28 File Offset: 0x0009AE28
		public CostHandle StartHangingConnection(Action<CostHandle> onRelease)
		{
			lock (base.SyncRoot)
			{
				if (!base.ThrottlingPolicy.MaxConcurrency.IsUnlimited && this.hangingConnections + 1 > Global.HangingConnectionLimit)
				{
					throw base.CreateOverBudgetException("MaxStreamingConcurrency", Global.HangingConnectionLimit.ToString(), 0);
				}
				this.hangingConnections++;
			}
			return new CostHandle(this, CostType.HangingConnection, onRelease, "EwsBudgetCache.StartHangingConnection", default(TimeSpan));
		}

		// Token: 0x06001B9D RID: 7069 RVA: 0x0009CCC8 File Offset: 0x0009AEC8
		protected override void AccountForCostHandle(CostHandle costHandle)
		{
			if (costHandle.CostType == CostType.HangingConnection)
			{
				lock (base.SyncRoot)
				{
					if (this.hangingConnections <= 0)
					{
						throw new InvalidOperationException("[EwsBudget.AccountForCostHandle] End for HangingConnections was called, but there are no outstanding HangingConnections.");
					}
					this.hangingConnections--;
					return;
				}
			}
			base.AccountForCostHandle(costHandle);
		}

		// Token: 0x17000344 RID: 836
		// (get) Token: 0x06001B9E RID: 7070 RVA: 0x0009CD34 File Offset: 0x0009AF34
		public int HangingConnections
		{
			get
			{
				return this.hangingConnections;
			}
		}

		// Token: 0x06001B9F RID: 7071 RVA: 0x0009CD3C File Offset: 0x0009AF3C
		internal static CostType GetConnectionCostType()
		{
			if (EwsOperationContextBase.Current != null)
			{
				object obj = null;
				if (EwsOperationContextBase.Current.RequestMessage.Properties.TryGetValue("ConnectionCostType", out obj))
				{
					return (CostType)obj;
				}
			}
			return CostType.Connection;
		}

		// Token: 0x06001BA0 RID: 7072 RVA: 0x0009CD78 File Offset: 0x0009AF78
		private static BudgetType GetBudgetTypeForMethodCalled(CallContext callContext)
		{
			BudgetType result = Global.BudgetType;
			if (Global.BulkOperationThrottlingEnabled && Global.BulkOperationMethods.Contains(callContext.MethodName))
			{
				result = Global.BulkOperationBudgetType;
			}
			if (Global.NonInteractiveThrottlingEnabled && Global.NonInteractiveOperationMethods.Contains(callContext.MethodName))
			{
				result = Global.NonInteractiveOperationBudgetType;
			}
			return result;
		}

		// Token: 0x06001BA1 RID: 7073 RVA: 0x0009CDCC File Offset: 0x0009AFCC
		private static BudgetKey GetExternalCallContextBudgetKey(ExternalCallContext externalCallContext)
		{
			ExTraceGlobals.ThrottlingTracer.TraceDebug<SmtpAddress>(0L, "[EwsBudget.GetExternalCallContextBudgetKey] Getting budget key for external caller {0}", externalCallContext.EmailAddress);
			return new StringBudgetKey(externalCallContext.EmailAddress.ToString(), false, EwsBudget.GetBudgetTypeForMethodCalled(externalCallContext));
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x0009CE10 File Offset: 0x0009B010
		private static BudgetKey GetCallContextBudgetKey(CallContext callContext)
		{
			ADSessionSettings settings = ADSessionSettings.FromAllTenantsOrRootOrgAutoDetect(callContext.ADRecipientSessionContext.OrganizationId);
			BudgetType budgetTypeForMethodCalled = EwsBudget.GetBudgetTypeForMethodCalled(callContext);
			if (EwsBudget.IsLongRunningScenarioContext(callContext))
			{
				if (!Global.LongRunningScenarioNonBackgroundTasks.Contains(callContext.MethodName))
				{
					callContext.BackgroundLoad = true;
				}
				if (VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).Ews.LongRunningScenarioThrottling.Enabled)
				{
					callContext.IsLongRunningScenario = true;
				}
				return new UnthrottledBudgetKey(callContext.OriginalCallerContext.IdentifierString, budgetTypeForMethodCalled);
			}
			if (EwsBudget.IsWellKnownClientsBackgroundSyncScenario(callContext))
			{
				return new UnthrottledBudgetKey(callContext.OriginalCallerContext.IdentifierString, budgetTypeForMethodCalled);
			}
			if (callContext.MailboxAccessType == MailboxAccessType.ExchangeImpersonation || (callContext.MailboxAccessType == MailboxAccessType.ApplicationAction && callContext.EffectiveCaller.ClientSecurityContext != null))
			{
				if (EwsBudget.GetConnectionCostType() == CostType.HangingConnection && callContext.IsPartnerUser)
				{
					ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget::InitializeFromCallContext] EI call for act as account '{0}', caller is FPO partner {1}. However for streaming notification, we grant unthrottled policy.", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid), callContext.OriginalCallerContext.IdentifierString);
					return new UnthrottledBudgetKey(callContext.OriginalCallerContext.IdentifierString, budgetTypeForMethodCalled, false);
				}
				ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget::InitializeFromCallContext] EI call for act as account '{0}', caller is '{1}'.  Using service account budget for act as account.", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid), (callContext.OriginalCallerContext.Sid == null) ? "<null>" : SidToAccountMap.Singleton.Get(callContext.OriginalCallerContext.Sid));
				return new SidBudgetKey(callContext.EffectiveCallerSid, budgetTypeForMethodCalled, true, settings);
			}
			else if (callContext.MailboxAccessType == MailboxAccessType.ServerToServer)
			{
				if (callContext.AllowUnthrottledBudget)
				{
					ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget.InitializeFromCallContext] Admin/service S2S call for account '{0}', caller '{1}'.  Using unthrottled budget.", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid), SidToAccountMap.Singleton.Get(callContext.OriginalCallerContext.Sid));
					return new UnthrottledBudgetKey(callContext.OriginalCallerContext.IdentifierString, budgetTypeForMethodCalled, true);
				}
				ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget.InitializeFromCallContext] S2S call for act as account '{0}', caller '{1}'.  Using service account budget for act as account.", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid), SidToAccountMap.Singleton.Get(callContext.OriginalCallerContext.Sid));
				return new SidBudgetKey(callContext.EffectiveCallerSid, budgetTypeForMethodCalled, true, settings);
			}
			else
			{
				if (callContext.EffectiveCaller.ClientSecurityContext != null)
				{
					ExTraceGlobals.ThrottlingTracer.TraceDebug<string>(0L, "[EwsBudget.InitializeFromCallContext] Getting normal budget for caller {0}", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid));
					return new SidBudgetKey(callContext.EffectiveCallerSid, budgetTypeForMethodCalled, false, settings);
				}
				ExTraceGlobals.ThrottlingTracer.TraceDebug<OrganizationId>(0L, "[EwsBudget.InitializeFromCallContext] Getting tenant budget for caller org {0}", callContext.OriginalCallerContext.OrganizationId);
				return new TenantBudgetKey(callContext.OriginalCallerContext.OrganizationId, budgetTypeForMethodCalled);
			}
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x0009D0C0 File Offset: 0x0009B2C0
		private static bool IsWellKnownClientsBackgroundSyncScenario(CallContext callContext)
		{
			return Global.BackgroundSyncTasksForWellKnownClientsEnabled && (Global.BackgroundSyncTasksForWellKnownClients.Contains("*") || Global.BackgroundSyncTasksForWellKnownClients.Any((string x) => string.Equals(x, callContext.MethodName, StringComparison.OrdinalIgnoreCase))) && !string.IsNullOrEmpty(callContext.UserAgent) && Global.WellKnownClientsForBackgroundSync.Any((string x) => callContext.UserAgent.IndexOf(x, StringComparison.OrdinalIgnoreCase) >= 0);
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x0009D134 File Offset: 0x0009B334
		private static bool IsLongRunningScenarioContext(CallContext callContext)
		{
			if (Global.LongRunningScenarioTasks.Contains(callContext.MethodName) && !string.IsNullOrEmpty(callContext.UserAgent) && Global.LongRunningScenarioEnabledUserAgents.IsMatch(callContext.UserAgent))
			{
				if (callContext.LogonTypeSource == LogonTypeSource.OpenAsAdminOrSystemServiceHeader)
				{
					ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget::InitializeFromCallContext] (IsLongRunningScenarioContext) Granting unlimited budget to {0} calling as system because the call is part of a long running process {1} ", (callContext.EffectiveCallerSid != null) ? SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid) : "NA", callContext.UserAgent);
					return true;
				}
				if (callContext.ManagementRole != null && (callContext.ManagementRole.HasUserRoles || callContext.ManagementRole.HasApplicationRoles))
				{
					foreach (string[] array2 in new string[][]
					{
						callContext.ManagementRole.ApplicationRoles,
						callContext.ManagementRole.UserRoles
					})
					{
						if (array2 != null)
						{
							foreach (string text in array2)
							{
								RoleType item;
								if (Enum.TryParse<RoleType>(text, out item) && Global.LongRunningScenarioEnabledRoleTypes.Contains(item))
								{
									ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget::InitializeFromCallContext] (IsLongRunningScenarioContext) Granting unlimited budget when calling with role {0} because the call is part of a long running process {1} ", text, callContext.UserAgent);
									return true;
								}
							}
						}
					}
				}
				if (callContext.EffectiveCaller != null && callContext.EffectiveCaller.ObjectSid != null)
				{
					try
					{
						ExchangeRunspaceConfiguration exchangeRunspaceConfiguration = ExchangeRunspaceConfigurationCache.Singleton.Get(callContext.EffectiveCaller, null, false);
						if (exchangeRunspaceConfiguration != null && Global.LongRunningScenarioEnabledRoleTypes.Intersect(exchangeRunspaceConfiguration.RoleTypes).Any<RoleType>())
						{
							ExTraceGlobals.ThrottlingTracer.TraceDebug<string, string>(0L, "[EwsBudget::InitializeFromCallContext] (IsLongRunningScenarioContext) Granting unlimited budget to {0} because the call is part of a long running process {1} ", SidToAccountMap.Singleton.Get(callContext.EffectiveCallerSid), callContext.UserAgent);
							return true;
						}
					}
					catch (ImpersonateUserDeniedException)
					{
						ExTraceGlobals.ThrottlingTracer.TraceDebug<string>(0L, "[EwsBudget::InitializeFromCallContext] (IsLongRunningScenarioContext) Not granting unlimited budget because impersonation was invalid {0} ", callContext.UserAgent);
					}
					return false;
				}
				return false;
			}
			return false;
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x0009D32C File Offset: 0x0009B52C
		public EwsBudget(BudgetKey budgetKey, IThrottlingPolicy throttlingPolicy) : base(budgetKey, throttlingPolicy)
		{
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x0009D336 File Offset: 0x0009B536
		public override string ToString()
		{
			return string.Format("{0},HangingConn:{1},{2}", base.ToString(), this.hangingConnections, this.GetSubscriptionState());
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x0009D35C File Offset: 0x0009B55C
		private string GetSubscriptionState()
		{
			if (Subscriptions.Singleton != null)
			{
				int subscriptionCountForUser = Subscriptions.Singleton.GetSubscriptionCountForUser(base.Owner);
				return string.Format("Sub:{0},MaxSub:{1};", subscriptionCountForUser, base.ThrottlingPolicy.FullPolicy.EwsMaxSubscriptions);
			}
			return "NoSub";
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x0009D3AC File Offset: 0x0009B5AC
		internal static void LogOverBudgetToIIS(OverBudgetException overBudgetException)
		{
			if (CallContext.Current != null && CallContext.Current.HttpContext != null && CallContext.Current.HttpContext.Response != null)
			{
				string value = string.Format(";OverBudget({0}/{1}),Owner:{2}[{3}]", new object[]
				{
					overBudgetException.IsServiceAccountBudget ? "ServiceAccount" : "Normal",
					overBudgetException.PolicyPart,
					overBudgetException.Owner,
					overBudgetException.Snapshot
				});
				RequestDetailsLogger.Current.AppendGenericError("OverBudget", value);
			}
		}

		// Token: 0x04001222 RID: 4642
		public const string MaxStreamingConcurrencyPart = "MaxStreamingConcurrency";

		// Token: 0x04001223 RID: 4643
		private int hangingConnections;
	}
}
