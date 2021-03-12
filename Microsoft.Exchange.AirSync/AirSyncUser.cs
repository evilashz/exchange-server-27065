using System;
using System.Net;
using System.Security.Principal;
using System.Web;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.RightsManagement;
using Microsoft.Exchange.Diagnostics.Components.AirSync;
using Microsoft.Exchange.Security.RightsManagement;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.AirSync
{
	// Token: 0x02000027 RID: 39
	internal sealed class AirSyncUser : IAirSyncUser
	{
		// Token: 0x060002D9 RID: 729 RVA: 0x0000F458 File Offset: 0x0000D658
		internal AirSyncUser(IAirSyncContext context)
		{
			this.context = context;
			bool flag = false;
			bool flag2 = true;
			try
			{
				if (this.context.Request.WasFromCafe)
				{
					this.InitializeFromRehydratedIdentity();
				}
				else if (this.context.Request.WasProxied)
				{
					flag2 = false;
					this.InitializeFromClientSecurityContext();
				}
				else
				{
					this.InitializeFromLoggedOnIdentity();
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					((IAirSyncUser)this).DisposeBudget();
					if (flag2 && this.clientSecurityContextWrapper != null)
					{
						this.clientSecurityContextWrapper.Dispose();
						this.clientSecurityContextWrapper = null;
					}
				}
			}
		}

		// Token: 0x17000138 RID: 312
		// (get) Token: 0x060002DA RID: 730 RVA: 0x0000F4F0 File Offset: 0x0000D6F0
		// (set) Token: 0x060002DB RID: 731 RVA: 0x0000F4F8 File Offset: 0x0000D6F8
		internal IStandardBudget Budget { get; private set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x060002DC RID: 732 RVA: 0x0000F501 File Offset: 0x0000D701
		// (set) Token: 0x060002DD RID: 733 RVA: 0x0000F509 File Offset: 0x0000D709
		internal IEasFeaturesManager Features { get; private set; }

		// Token: 0x1700013A RID: 314
		// (get) Token: 0x060002DE RID: 734 RVA: 0x0000F512 File Offset: 0x0000D712
		IAirSyncContext IAirSyncUser.Context
		{
			get
			{
				return this.context;
			}
		}

		// Token: 0x1700013B RID: 315
		// (get) Token: 0x060002DF RID: 735 RVA: 0x0000F51A File Offset: 0x0000D71A
		Guid IAirSyncUser.DeviceBehaviorCacheGuid
		{
			get
			{
				return this.deviceBehaviorCacheGuid;
			}
		}

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x060002E0 RID: 736 RVA: 0x0000F522 File Offset: 0x0000D722
		IStandardBudget IAirSyncUser.Budget
		{
			get
			{
				return this.Budget;
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x060002E1 RID: 737 RVA: 0x0000F52A File Offset: 0x0000D72A
		IIdentity IAirSyncUser.Identity
		{
			get
			{
				return this.context.Principal.Identity;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x060002E2 RID: 738 RVA: 0x0000F53C File Offset: 0x0000D73C
		WindowsIdentity IAirSyncUser.WindowsIdentity
		{
			get
			{
				return (WindowsIdentity)this.context.Principal.Identity;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x060002E3 RID: 739 RVA: 0x0000F553 File Offset: 0x0000D753
		byte[] IAirSyncUser.SID
		{
			get
			{
				return this.clientSecurityContextWrapper.UserSidBytes;
			}
		}

		// Token: 0x17000140 RID: 320
		// (get) Token: 0x060002E4 RID: 740 RVA: 0x0000F560 File Offset: 0x0000D760
		ExchangePrincipal IAirSyncUser.ExchangePrincipal
		{
			get
			{
				if (this.exchangePrincipalInitialized)
				{
					return this.exchangePrincipal;
				}
				this.InitializeExchangePrincipal();
				return this.exchangePrincipal;
			}
		}

		// Token: 0x17000141 RID: 321
		// (get) Token: 0x060002E5 RID: 741 RVA: 0x0000F57D File Offset: 0x0000D77D
		WindowsPrincipal IAirSyncUser.WindowsPrincipal
		{
			get
			{
				return this.windowsPrincipal;
			}
		}

		// Token: 0x17000142 RID: 322
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x0000F585 File Offset: 0x0000D785
		bool IAirSyncUser.IsEnabled
		{
			get
			{
				return this.activeDirectoryUser != null && this.activeDirectoryUser.ActiveSyncEnabled;
			}
		}

		// Token: 0x17000143 RID: 323
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x0000F59C File Offset: 0x0000D79C
		ActiveSyncMiniRecipient IAirSyncUser.ADUser
		{
			get
			{
				((IAirSyncUser)this).InitializeADUser();
				return this.activeDirectoryUser;
			}
		}

		// Token: 0x17000144 RID: 324
		// (get) Token: 0x060002E8 RID: 744 RVA: 0x0000F5AA File Offset: 0x0000D7AA
		OrganizationId IAirSyncUser.OrganizationId
		{
			get
			{
				if (((IAirSyncUser)this).ADUser == null)
				{
					return null;
				}
				return ((IAirSyncUser)this).ADUser.OrganizationId;
			}
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x0000F5C1 File Offset: 0x0000D7C1
		// (set) Token: 0x060002EA RID: 746 RVA: 0x0000F5C9 File Offset: 0x0000D7C9
		bool IAirSyncUser.MailboxIsOnE12Server
		{
			get
			{
				return this.mailboxIsOnE12Server;
			}
			set
			{
				this.mailboxIsOnE12Server = value;
			}
		}

		// Token: 0x17000146 RID: 326
		// (get) Token: 0x060002EB RID: 747 RVA: 0x0000F5D2 File Offset: 0x0000D7D2
		bool IAirSyncUser.IsMonitoringTestUser
		{
			get
			{
				return string.Compare(this.context.Request.UserAgent, "TestActiveSyncConnectivity", StringComparison.OrdinalIgnoreCase) == 0;
			}
		}

		// Token: 0x17000147 RID: 327
		// (get) Token: 0x060002EC RID: 748 RVA: 0x0000F5F2 File Offset: 0x0000D7F2
		ClientSecurityContextWrapper IAirSyncUser.ClientSecurityContextWrapper
		{
			get
			{
				return this.clientSecurityContextWrapper;
			}
		}

		// Token: 0x17000148 RID: 328
		// (get) Token: 0x060002ED RID: 749 RVA: 0x0000F5FA File Offset: 0x0000D7FA
		string IAirSyncUser.Name
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x17000149 RID: 329
		// (get) Token: 0x060002EE RID: 750 RVA: 0x0000F602 File Offset: 0x0000D802
		string IAirSyncUser.ServerFullyQualifiedDomainName
		{
			get
			{
				if (this.exchangePrincipal != null)
				{
					return this.exchangePrincipal.MailboxInfo.Location.ServerFqdn;
				}
				return null;
			}
		}

		// Token: 0x1700014A RID: 330
		// (get) Token: 0x060002EF RID: 751 RVA: 0x0000F623 File Offset: 0x0000D823
		Guid IAirSyncUser.MailboxGuid
		{
			get
			{
				return ((IAirSyncUser)this).ExchangePrincipal.MailboxInfo.MailboxGuid;
			}
		}

		// Token: 0x1700014B RID: 331
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x0000F635 File Offset: 0x0000D835
		string IAirSyncUser.DisplayName
		{
			get
			{
				return ((IAirSyncUser)this).ExchangePrincipal.MailboxInfo.DisplayName;
			}
		}

		// Token: 0x1700014C RID: 332
		// (get) Token: 0x060002F1 RID: 753 RVA: 0x0000F648 File Offset: 0x0000D848
		string IAirSyncUser.SmtpAddress
		{
			get
			{
				return ((IAirSyncUser)this).ExchangePrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x1700014D RID: 333
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x0000F674 File Offset: 0x0000D874
		bool IAirSyncUser.IrmEnabled
		{
			get
			{
				if (ADNotificationManager.GetPolicyData(this) == null || !ADNotificationManager.GetPolicyData(this).IsIrmEnabled)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "IRM feature disabled via EAS policy for user {0}", ((IAirSyncUser)this).DisplayName);
					return false;
				}
				bool result;
				try
				{
					result = RmsClientManager.IRMConfig.IsClientAccessServerEnabledForTenant(((IAirSyncUser)this).OrganizationId);
				}
				catch (ExchangeConfigurationException ex)
				{
					AirSyncDiagnostics.TraceError<ExchangeConfigurationException>(ExTraceGlobals.RequestsTracer, null, "ExchangeConfigurationException while reading IRM Configuration: {0}", ex);
					throw new AirSyncPermanentException(StatusCode.IRM_TransientError, ex, false)
					{
						ErrorStringForProtocolLogger = "asuIeExchangeConfigurationException"
					};
				}
				catch (RightsManagementException ex2)
				{
					AirSyncDiagnostics.TraceError<RightsManagementException>(ExTraceGlobals.RequestsTracer, null, "RightsManagementException while reading IRM Configuration: {0}", ex2);
					throw new AirSyncPermanentException(ex2.IsPermanent ? StatusCode.IRM_PermanentError : StatusCode.IRM_TransientError, ex2, false)
					{
						ErrorStringForProtocolLogger = "asuIeRightsManagementException" + ex2.FailureCode.ToString()
					};
				}
				return result;
			}
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x0000F75C File Offset: 0x0000D95C
		string IAirSyncUser.WindowsLiveId
		{
			get
			{
				if (!GlobalSettings.IsWindowsLiveIDEnabled)
				{
					return null;
				}
				return this.context.WindowsLiveId;
			}
		}

		// Token: 0x1700014F RID: 335
		// (get) Token: 0x060002F4 RID: 756 RVA: 0x0000F772 File Offset: 0x0000D972
		IEasFeaturesManager IAirSyncUser.Features
		{
			get
			{
				return this.Features;
			}
		}

		// Token: 0x17000150 RID: 336
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0000F77C File Offset: 0x0000D97C
		BudgetKey IAirSyncUser.BudgetKey
		{
			get
			{
				if (this.budgetKey == null)
				{
					if (GlobalSettings.UseTestBudget)
					{
						this.budgetKey = new UnthrottledBudgetKey(this.clientSecurityContextWrapper.UserSid.ToString(), BudgetType.Eas);
					}
					else
					{
						CommandType commandType = this.context.Request.CommandType;
						if (commandType != CommandType.Sync)
						{
							switch (commandType)
							{
							case CommandType.Ping:
							case CommandType.ItemOperations:
								break;
							default:
								this.budgetKey = new SidBudgetKey(this.clientSecurityContextWrapper.UserSid, BudgetType.Eas, false, ADUserCache.GetSessionSettings(((IAirSyncUser)this).ExchangePrincipal.MailboxInfo.OrganizationId, ((IAirSyncUser)this).Context.ProtocolLogger));
								goto IL_F1;
							}
						}
						this.budgetKey = new EasDeviceBudgetKey(this.clientSecurityContextWrapper.UserSid, this.context.DeviceIdentity.DeviceId, this.context.DeviceIdentity.DeviceType, ADUserCache.GetSessionSettings(((IAirSyncUser)this).ExchangePrincipal.MailboxInfo.OrganizationId, ((IAirSyncUser)this).Context.ProtocolLogger));
					}
				}
				IL_F1:
				return this.budgetKey;
			}
		}

		// Token: 0x17000151 RID: 337
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x0000F880 File Offset: 0x0000DA80
		bool IAirSyncUser.IsConsumerOrganizationUser
		{
			get
			{
				return this.Features.IsEnabled(EasFeature.ConsumerOrganizationUser) || (((IAirSyncUser)this).ADUser != null && ((IAirSyncUser)this).ADUser.IsConsumerOrganization());
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0000F8AC File Offset: 0x0000DAAC
		BackOffValue IAirSyncUser.GetBudgetBackOffValue()
		{
			BackOffValue backOffValue = null;
			if (((IAirSyncUser)this).Budget != null)
			{
				ITokenBucket budgetTokenBucket = ((IAirSyncUser)this).GetBudgetTokenBucket();
				if (budgetTokenBucket == null)
				{
					AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.GetBudgetBackOffDuration] Budget does not contain a token bucket.  Likely unthrottled.");
					return BackOffValue.NoBackOffValue;
				}
				float balance = budgetTokenBucket.GetBalance();
				AirSyncDiagnostics.TraceInfo<float, int>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.GetBudgetBackOffDuration]. Balance :{0}, RechargeRate:{1}", balance, budgetTokenBucket.RechargeRate);
				backOffValue = new BackOffValue
				{
					BackOffReason = "Budget"
				};
				if ((double)balance < GlobalSettings.BudgetBackOffMinThreshold.TotalMilliseconds)
				{
					backOffValue.BackOffDuration = Math.Ceiling((GlobalSettings.BudgetBackOffMinThreshold.TotalMilliseconds - (double)balance) * 60.0 * 60.0 / (double)budgetTokenBucket.RechargeRate);
					backOffValue.BackOffType = ((balance > (float)(ulong.MaxValue * (ulong)((IAirSyncUser)this).Budget.ThrottlingPolicy.EasMaxBurst.Value)) ? BackOffType.Medium : BackOffType.High);
				}
				else
				{
					backOffValue.BackOffDuration = Math.Ceiling((GlobalSettings.BudgetBackOffMinThreshold.TotalMilliseconds - (double)balance) / 1000.0);
				}
			}
			AirSyncDiagnostics.TraceInfo<double, BackOffType>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.GetBudgetBackOffDuration]. BudgetBackOff Duration:{0} sec. BackOffType:{1}", backOffValue.BackOffDuration, backOffValue.BackOffType);
			return backOffValue ?? BackOffValue.NoBackOffValue;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x0000F9DC File Offset: 0x0000DBDC
		void IAirSyncUser.DisposeBudget()
		{
			if (this.Budget == null)
			{
				return;
			}
			try
			{
				((IAirSyncUser)this).Budget.Dispose();
			}
			catch (FailFastException arg)
			{
				AirSyncDiagnostics.TraceError<FailFastException>(ExTraceGlobals.RequestsTracer, null, "Budget.Dispose failed with exception: {0}", arg);
			}
			if (this.context != null)
			{
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Budget, "(D)" + ((IAirSyncUser)this).Budget.ToString());
				if (GlobalSettings.WriteBudgetDiagnostics || this.context.User.IsMonitoringTestUser)
				{
					this.context.Response.AppendHeader("X-BudgetDiagnostics", ((IAirSyncUser)this).Budget.ToString());
				}
			}
			this.Budget = null;
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x0000FA94 File Offset: 0x0000DC94
		void IAirSyncUser.PrepareToHang()
		{
			this.activeDirectoryUser = null;
		}

		// Token: 0x060002FA RID: 762 RVA: 0x0000FAA0 File Offset: 0x0000DCA0
		void IAirSyncUser.AcquireBudget()
		{
			if (((IAirSyncUser)this).Budget != null)
			{
				return;
			}
			this.Budget = StandardBudget.Acquire(((IAirSyncUser)this).BudgetKey);
			((IAirSyncUser)this).SetBudgetDiagnosticValues(true);
			this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Budget, "(A)" + ((IAirSyncUser)this).Budget.ToString());
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0000FAF8 File Offset: 0x0000DCF8
		void IAirSyncUser.SetBudgetDiagnosticValues(bool start)
		{
			ITokenBucket budgetTokenBucket = ((IAirSyncUser)this).GetBudgetTokenBucket();
			if (budgetTokenBucket != null)
			{
				float balance = budgetTokenBucket.GetBalance();
				this.context.SetDiagnosticValue(start ? ConditionalHandlerSchema.BudgetBalanceStart : ConditionalHandlerSchema.BudgetBalanceEnd, balance);
				this.context.SetDiagnosticValue(start ? ConditionalHandlerSchema.IsOverBudgetAtStart : ConditionalHandlerSchema.IsOverBudgetAtEnd, balance < 0f);
			}
			StandardBudgetWrapper standardBudgetWrapper = ((IAirSyncUser)this).Budget as StandardBudgetWrapper;
			if (standardBudgetWrapper != null)
			{
				this.context.SetDiagnosticValue(start ? ConditionalHandlerSchema.ConcurrencyStart : ConditionalHandlerSchema.ConcurrencyEnd, standardBudgetWrapper.GetInnerBudget().Connections);
			}
		}

		// Token: 0x060002FC RID: 764 RVA: 0x0000FB98 File Offset: 0x0000DD98
		void IAirSyncUser.InitializeADUser()
		{
			if (this.activeDirectoryUser == null)
			{
				this.activeDirectoryUser = ADUserCache.TryGetADUser(this, this.context.ProtocolLogger);
				if (this.activeDirectoryUser != null)
				{
					this.deviceBehaviorCacheGuid = this.activeDirectoryUser.OriginalId.ObjectGuid;
					this.Features = EasFeaturesManager.Create(this.activeDirectoryUser, this.context.FlightingOverrides);
				}
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0000FC00 File Offset: 0x0000DE00
		ITokenBucket IAirSyncUser.GetBudgetTokenBucket()
		{
			StandardBudgetWrapper standardBudgetWrapper = ((IAirSyncUser)this).Budget as StandardBudgetWrapper;
			if (standardBudgetWrapper != null)
			{
				return standardBudgetWrapper.GetInnerBudget().CasTokenBucket;
			}
			return null;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000FC2C File Offset: 0x0000DE2C
		private void InitializeFromClientSecurityContext()
		{
			string proxyHeader = this.context.Request.ProxyHeader;
			if (this.clientSecurityContextWrapper != null)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromClientSecurityContext]. clientSecurityContextWrapper is not null. calling dispose.");
				this.clientSecurityContextWrapper.Dispose();
			}
			this.clientSecurityContextWrapper = (ClientSecurityContextWrapper)HttpRuntime.Cache.Get(proxyHeader);
			if (this.clientSecurityContextWrapper == null)
			{
				AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromClientSecurityContext] ProxyHeader key '{0}' was missing from HttpRuntime cache.  Returning HttpStatusNeedIdentity.", proxyHeader);
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "MissingCscCacheEntry");
				AirSyncPermanentException ex = new AirSyncPermanentException((HttpStatusCode)441, StatusCode.None, null, false);
				throw ex;
			}
			AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromClientSecurityContext] ProxyHeader key '{0}' was found in the HttpRuntime cache.  Reusing CSC for user.", proxyHeader);
			string[] array = proxyHeader.Split(",".ToCharArray(), 2);
			if (array.Length != 2)
			{
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "BadProxyHeader");
				throw new AirSyncPermanentException(HttpStatusCode.BadRequest, StatusCode.InvalidCombinationOfIDs, null, false);
			}
			this.username = array[1];
			((IAirSyncUser)this).InitializeADUser();
			((IAirSyncUser)this).AcquireBudget();
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000FD28 File Offset: 0x0000DF28
		private void InitializeFromRehydratedIdentity()
		{
			if (this.clientSecurityContextWrapper != null)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromRehydratedIdentity]. clientSecurityContextWrapper is not null. calling dispose.");
				this.clientSecurityContextWrapper.Dispose();
			}
			this.clientSecurityContextWrapper = ClientSecurityContextWrapper.FromIdentity(((IAirSyncUser)this).Identity);
			this.username = ((IAirSyncUser)this).Identity.Name;
			AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromRehydratedIdentity] Hyrdating CSC from user {0}", this.username);
			((IAirSyncUser)this).InitializeADUser();
			((IAirSyncUser)this).AcquireBudget();
		}

		// Token: 0x06000300 RID: 768 RVA: 0x0000FD9C File Offset: 0x0000DF9C
		private void InitializeFromLoggedOnIdentity()
		{
			if (((IAirSyncUser)this).WindowsIdentity.User == null)
			{
				AirSyncDiagnostics.TraceError(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromLoggedOnIdentity] Anonymous user is forbidden.");
				this.context.ProtocolLogger.SetValue(ProtocolLoggerData.Error, "AnonymousUser");
				AirSyncPermanentException ex = new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.UserCannotBeAnonymous, EASServerStrings.AnonymousAccessError, true);
				throw ex;
			}
			this.windowsPrincipal = new WindowsPrincipal(((IAirSyncUser)this).WindowsIdentity);
			this.username = this.context.Request.LogonUserName;
			if (this.clientSecurityContextWrapper != null)
			{
				AirSyncDiagnostics.TraceInfo(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromLoggedOnIdentity]. clientSecurityContextWrapper is not null. calling dispose.");
				this.clientSecurityContextWrapper.Dispose();
			}
			this.clientSecurityContextWrapper = ClientSecurityContextWrapper.FromWindowsIdentity(((IAirSyncUser)this).WindowsIdentity);
			AirSyncDiagnostics.TraceError<string>(ExTraceGlobals.RequestsTracer, null, "[AirSyncUser.InitializeFromLoggedOnIdentity] Acquired CSC for user '{0}'", this.username);
			((IAirSyncUser)this).InitializeADUser();
			((IAirSyncUser)this).AcquireBudget();
		}

		// Token: 0x06000301 RID: 769 RVA: 0x0000FE9C File Offset: 0x0000E09C
		private void InitializeExchangePrincipal()
		{
			try
			{
				this.exchangePrincipalInitialized = true;
				if (((IAirSyncUser)this).ADUser == null)
				{
					throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.UserHasNoMailbox, null, false)
					{
						ErrorStringForProtocolLogger = "UserHasNoMailbox"
					};
				}
				AirSyncDiagnostics.FaultInjectionPoint(3414568253U, delegate
				{
					this.exchangePrincipal = ExchangePrincipal.FromMiniRecipient(((IAirSyncUser)this).ADUser);
				}, delegate
				{
					throw new ObjectNotFoundException(new LocalizedString("FaultInjection ObjectNotFoundException"));
				});
			}
			catch (ObjectNotFoundException innerException)
			{
				string redirectAddressForUserHasNoMailbox = this.GetRedirectAddressForUserHasNoMailbox(((IAirSyncUser)this).ADUser);
				if (!string.IsNullOrEmpty(redirectAddressForUserHasNoMailbox))
				{
					throw new IncorrectUrlRequestException((HttpStatusCode)451, "X-MS-Location", redirectAddressForUserHasNoMailbox);
				}
				throw new AirSyncPermanentException(HttpStatusCode.Forbidden, StatusCode.UserHasNoMailbox, innerException, false)
				{
					ErrorStringForProtocolLogger = "UserHasNoMailbox"
				};
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000100E8 File Offset: 0x0000E2E8
		private string GetRedirectAddressForUserHasNoMailbox(ActiveSyncMiniRecipient activesyncMiniRecipient)
		{
			string easEndpoint = null;
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.ActiveSync.RedirectForOnBoarding.Enabled)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "The hybrid on boarding redirect feature is only for OnPrem servers.");
				return null;
			}
			if (this.context.CommandType != CommandType.Options && this.context.AirSyncVersion < GlobalSettings.MinRedirectProtocolVersion)
			{
				AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "The protocol version is less than 14.0 that doesn't support 451 redirect protocol call.");
				return null;
			}
			AirSyncDiagnostics.FaultInjectionPoint(3414568253U, delegate
			{
				if (activesyncMiniRecipient != null && activesyncMiniRecipient.ExternalEmailAddress != null)
				{
					AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Try to figure out eas endpoint for user: {0}.", activesyncMiniRecipient.ExternalEmailAddress.AddressString);
					this.context.ProtocolLogger.SetValue(ProtocolLoggerData.RedirectTo, "TryToFigureOutEasEndpoint");
					SmtpProxyAddress smtpProxyAddress = activesyncMiniRecipient.ExternalEmailAddress as SmtpProxyAddress;
					if (smtpProxyAddress != null && !string.IsNullOrEmpty(smtpProxyAddress.AddressString))
					{
						OrganizationIdCacheValue organizationIdCacheValue = OrganizationIdCache.Singleton.Get(activesyncMiniRecipient.OrganizationId);
						string domain = ((SmtpAddress)smtpProxyAddress).Domain;
						OrganizationRelationship organizationRelationship = organizationIdCacheValue.GetOrganizationRelationship(domain);
						if (organizationRelationship != null)
						{
							Uri targetOwaURL = organizationRelationship.TargetOwaURL;
							easEndpoint = this.TransferTargetOwaUrlToEasEndpoint(targetOwaURL);
							AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "Redirect to EASEndpoint : {0}.", easEndpoint);
							this.context.ProtocolLogger.AppendValue(ProtocolLoggerData.RedirectTo, easEndpoint);
							return;
						}
						AirSyncDiagnostics.TraceDebug<string>(ExTraceGlobals.RequestsTracer, this, "OrganizationRelationShip is null for the domain {0}", domain);
						return;
					}
					else
					{
						AirSyncDiagnostics.TraceDebug(ExTraceGlobals.RequestsTracer, this, "External email address is null");
					}
				}
			}, delegate
			{
				Uri targetOwaUri = new Uri("http://outlook.com/owa");
				easEndpoint = this.TransferTargetOwaUrlToEasEndpoint(targetOwaUri);
			});
			return easEndpoint;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x00010198 File Offset: 0x0000E398
		private string TransferTargetOwaUrlToEasEndpoint(Uri targetOwaUri)
		{
			string result = null;
			if (targetOwaUri != null && targetOwaUri.Host != null)
			{
				if (targetOwaUri.Host.Equals("outlook.com"))
				{
					result = string.Format("https://{0}/Microsoft-Server-ActiveSync", "outlook.office365.com");
				}
				else
				{
					result = string.Format("https://{0}/Microsoft-Server-ActiveSync", targetOwaUri.Host);
				}
			}
			return result;
		}

		// Token: 0x0400024D RID: 589
		private const uint Migration451RedirectFaultInjectionLid = 3414568253U;

		// Token: 0x0400024E RID: 590
		private const string ActvieSyncServerUrl = "https://{0}/Microsoft-Server-ActiveSync";

		// Token: 0x0400024F RID: 591
		private const string CorrectO365SNSUrl = "outlook.office365.com";

		// Token: 0x04000250 RID: 592
		private const string LegacyO365OWAHost = "outlook.com";

		// Token: 0x04000251 RID: 593
		private BudgetKey budgetKey;

		// Token: 0x04000252 RID: 594
		private IAirSyncContext context;

		// Token: 0x04000253 RID: 595
		private ExchangePrincipal exchangePrincipal;

		// Token: 0x04000254 RID: 596
		private bool exchangePrincipalInitialized;

		// Token: 0x04000255 RID: 597
		private WindowsPrincipal windowsPrincipal;

		// Token: 0x04000256 RID: 598
		private ActiveSyncMiniRecipient activeDirectoryUser;

		// Token: 0x04000257 RID: 599
		private bool mailboxIsOnE12Server;

		// Token: 0x04000258 RID: 600
		private ClientSecurityContextWrapper clientSecurityContextWrapper;

		// Token: 0x04000259 RID: 601
		private Guid deviceBehaviorCacheGuid;

		// Token: 0x0400025A RID: 602
		private string username;
	}
}
