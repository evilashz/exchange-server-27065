using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.DirectoryServices;
using System.Globalization;
using System.Security.AccessControl;
using System.Security.Principal;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Net.WSTrust;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020002A4 RID: 676
	internal abstract class BaseMessageTrackingReport<RequestType, SingleItemType> : SingleStepServiceCommand<RequestType, SingleItemType> where RequestType : BaseRequest
	{
		// Token: 0x17000237 RID: 567
		// (get) Token: 0x060011FC RID: 4604
		protected abstract string Domain { get; }

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x060011FD RID: 4605
		protected abstract string DiagnosticsLevel { get; }

		// Token: 0x060011FE RID: 4606 RVA: 0x00058123 File Offset: 0x00056323
		protected virtual void PrepareRequest()
		{
		}

		// Token: 0x060011FF RID: 4607 RVA: 0x00058128 File Offset: 0x00056328
		public BaseMessageTrackingReport(CallContext callContext, RequestType request) : base(callContext, request)
		{
			this.PrepareRequest();
			if (string.IsNullOrEmpty(this.DiagnosticsLevel) || !EnumValidator<Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel>.TryParse(this.DiagnosticsLevel, EnumParseOptions.Default, out this.diagnosticsLevelEnum))
			{
				this.diagnosticsLevelEnum = Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel.None;
			}
			if (!ServerCache.Instance.InitializeIfNeeded(HostId.EWSApplicationPool))
			{
				ExTraceGlobals.WebServiceTracer.TraceError((long)this.GetHashCode(), "Failed to initialize MessageTracking ServerCache");
				throw new MessageTrackingFatalException();
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06001200 RID: 4608 RVA: 0x0005819F File Offset: 0x0005639F
		internal override bool SupportsExternalUsers
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06001201 RID: 4609 RVA: 0x000581A2 File Offset: 0x000563A2
		internal override Offer ExpectedOffer
		{
			get
			{
				return Offer.SharingCalendarFreeBusy;
			}
		}

		// Token: 0x06001202 RID: 4610 RVA: 0x000581A9 File Offset: 0x000563A9
		protected void InitializeRequest()
		{
			this.LoadOrganization();
			this.CheckAccess();
			this.AfterCheckAccess();
		}

		// Token: 0x06001203 RID: 4611 RVA: 0x000581C0 File Offset: 0x000563C0
		protected void CleanupRequest()
		{
			if (this.diagnosticsLevelEnum == Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel.Etw && this.perThreadTracingEnabled)
			{
				TraceWrapper.SearchLibraryTracer.Unregister();
			}
			if (this.directoryContext != null)
			{
				if (this.acquiredDirectoryContext)
				{
					this.directoryContext.Yield();
				}
				if (this.directoryContext.TrackingBudget != null)
				{
					this.directoryContext.TrackingBudget.Dispose();
				}
				if (this.directoryContext.ClientContext != null)
				{
					this.directoryContext.ClientContext.Dispose();
				}
			}
		}

		// Token: 0x06001204 RID: 4612 RVA: 0x0005823D File Offset: 0x0005643D
		protected void AddError(TrackingError trackingError)
		{
			if (!TrackingErrorCollection.IsNullOrEmpty(this.errors))
			{
				this.errors.Errors.Add(trackingError);
			}
		}

		// Token: 0x06001205 RID: 4613 RVA: 0x00058260 File Offset: 0x00056460
		protected string[] GetDiagnosticsToTransmit()
		{
			string[] result = null;
			if (this.directoryContext != null && this.directoryContext.DiagnosticsContext != null)
			{
				result = this.directoryContext.DiagnosticsContext.Serialize();
			}
			return result;
		}

		// Token: 0x06001206 RID: 4614 RVA: 0x00058298 File Offset: 0x00056498
		protected void LogRequestStatus(bool succeeded)
		{
			List<KeyValuePair<string, object>> list = new List<KeyValuePair<string, object>>();
			list.Add(new KeyValuePair<string, object>("CallerIdentity", this.CallerIdentityForLogging()));
			list.Add(new KeyValuePair<string, object>("ResolvedDomain", this.Domain ?? string.Empty));
			list.Add(new KeyValuePair<string, object>("Succeeded", succeeded.ToString(CultureInfo.InvariantCulture)));
			if (base.Request != null)
			{
				((IMessageTrackingRequestLogInformation)((object)base.Request)).AddRequestDataForLogging(list);
			}
			CommonDiagnosticsLog.Instance.LogEvent(CommonDiagnosticsLog.Source.DeliveryReports, list);
		}

		// Token: 0x06001207 RID: 4615 RVA: 0x0005832C File Offset: 0x0005652C
		private static void GetSecurityDescriptors(IEwsBudget callerBudget, out RawSecurityDescriptor trackingSecurityDescriptor, out RawSecurityDescriptor localServerSecurityDescriptor)
		{
			if (!BaseMessageTrackingReport<RequestType, SingleItemType>.securityDescriptorInitialized)
			{
				lock (BaseMessageTrackingReport<RequestType, SingleItemType>.initLock)
				{
					if (!BaseMessageTrackingReport<RequestType, SingleItemType>.securityDescriptorInitialized)
					{
						try
						{
							BaseMessageTrackingReport<RequestType, SingleItemType>.trackingSecurityDescriptor = BaseMessageTrackingReport<RequestType, SingleItemType>.CreateSecurityDescriptor(callerBudget);
							BaseMessageTrackingReport<RequestType, SingleItemType>.localServerSecurityDescriptor = BaseMessageTrackingReport<RequestType, SingleItemType>.GetLocalCasSecurityDescriptor(callerBudget);
							BaseMessageTrackingReport<RequestType, SingleItemType>.securityDescriptorInitialized = true;
						}
						catch (TransientException arg)
						{
							ExTraceGlobals.WebServiceTracer.TraceError<TransientException>(0L, "TransientException when initializing Security Descriptor: {0}", arg);
							throw new MessageTrackingTransientException();
						}
						catch (DataSourceOperationException arg2)
						{
							ExTraceGlobals.WebServiceTracer.TraceError<DataSourceOperationException>(0L, "DataSourceOperationException when initializing Security Descriptor: {0}", arg2);
							throw new MessageTrackingFatalException();
						}
						catch (DataValidationException arg3)
						{
							ExTraceGlobals.WebServiceTracer.TraceError<DataValidationException>(0L, "DataValidationException when initializing Security Descriptor: {0}", arg3);
							throw new MessageTrackingFatalException();
						}
					}
				}
			}
			trackingSecurityDescriptor = BaseMessageTrackingReport<RequestType, SingleItemType>.trackingSecurityDescriptor;
			localServerSecurityDescriptor = BaseMessageTrackingReport<RequestType, SingleItemType>.localServerSecurityDescriptor;
		}

		// Token: 0x06001208 RID: 4616 RVA: 0x00058414 File Offset: 0x00056614
		private static RawSecurityDescriptor GetLocalCasSecurityDescriptor(IEwsBudget callerBudget)
		{
			ITopologyConfigurationSession topologyConfigurationSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 365, "GetLocalCasSecurityDescriptor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
			Server server = topologyConfigurationSession.ReadLocalServer();
			if (server == null)
			{
				ExTraceGlobals.WebServiceTracer.TraceError(0L, "Cannot read local server");
				throw new MessageTrackingFatalException();
			}
			RawSecurityDescriptor rawSecurityDescriptor = topologyConfigurationSession.ReadSecurityDescriptor(server.Id);
			if (rawSecurityDescriptor == null)
			{
				ExTraceGlobals.WebServiceTracer.TraceError<ADObjectId>(0L, "Cannot read security descriptor for server with id: {0}", server.Id);
				throw new MessageTrackingFatalException();
			}
			return rawSecurityDescriptor;
		}

		// Token: 0x06001209 RID: 4617 RVA: 0x00058490 File Offset: 0x00056690
		private static OrganizationId GetOrganizationId(string domain, IEwsBudget callerBudget)
		{
			ExTraceGlobals.WebServiceTracer.TraceDebug<string>(0L, "Trying to get OrgId for domain: {0}", domain);
			OrganizationId organizationId = null;
			if (!VariantConfiguration.InvariantNoFlightingSnapshot.Global.MultiTenancy.Enabled)
			{
				return OrganizationId.ForestWideOrgId;
			}
			if (ServerCache.Instance.TryGetOrganizationId(domain, out organizationId))
			{
				return organizationId;
			}
			try
			{
				IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromTenantAcceptedDomain(domain), 424, "GetOrganizationId", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
				QueryFilter filter = new ComparisonFilter(ComparisonOperator.Equal, AcceptedDomainSchema.DomainName, domain);
				ADPagedReader<AcceptedDomain> adpagedReader = tenantOrTopologyConfigurationSession.FindPaged<AcceptedDomain>(null, QueryScope.SubTree, filter, null, 0);
				if (adpagedReader == null)
				{
					ExTraceGlobals.WebServiceTracer.TraceError(0L, "Domain not found in AD");
					throw new ServiceArgumentException(CoreResources.IDs.ErrorMessageTrackingNoSuchDomain);
				}
				int num = 0;
				foreach (AcceptedDomain acceptedDomain in adpagedReader)
				{
					num++;
					if (num > 1)
					{
						ExTraceGlobals.WebServiceTracer.TraceError(0L, "Multiple domains found in AD");
						throw new ServiceArgumentException(CoreResources.IDs.ErrorMessageTrackingPermanentError);
					}
					organizationId = acceptedDomain.OrganizationId;
				}
				if (organizationId == null)
				{
					ExTraceGlobals.WebServiceTracer.TraceError(0L, "Domain was not found in AD");
					throw new ServiceArgumentException(CoreResources.IDs.ErrorMessageTrackingNoSuchDomain);
				}
			}
			catch (CannotResolveTenantNameException)
			{
				ExTraceGlobals.WebServiceTracer.TraceError(0L, "Domain not found in AD");
				throw new ServiceArgumentException(CoreResources.IDs.ErrorMessageTrackingNoSuchDomain);
			}
			ExTraceGlobals.WebServiceTracer.TraceDebug<string, OrganizationId>(0L, "Domain {0} mapped to Org {1}", domain, organizationId);
			return organizationId;
		}

		// Token: 0x0600120A RID: 4618 RVA: 0x00058620 File Offset: 0x00056820
		private static RawSecurityDescriptor CreateSecurityDescriptor(IEwsBudget callerBudget)
		{
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 487, "CreateSecurityDescriptor", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
			ADGroup adgroup = tenantOrRootOrgRecipientSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.ExSWkGuid, ADSession.GetConfigurationNamingContextForLocalForest());
			if (adgroup == null)
			{
				ExTraceGlobals.WebServiceTracer.TraceError(0L, "Could not get Exchange Servers Group");
				throw new MessageTrackingFatalException();
			}
			SecurityIdentifier sid = adgroup.Sid;
			ADGroup adgroup2 = tenantOrRootOrgRecipientSession.ResolveWellKnownGuid<ADGroup>(WellKnownGuid.EoaWkGuid, ADSession.GetConfigurationNamingContextForLocalForest());
			if (adgroup == null)
			{
				ExTraceGlobals.WebServiceTracer.TraceError(0L, "Could not get Exchange Org Admins Group");
				throw new MessageTrackingFatalException();
			}
			SecurityIdentifier sid2 = adgroup2.Sid;
			ActiveDirectorySecurity activeDirectorySecurity = new ActiveDirectorySecurity();
			SecurityIdentifier identity = new SecurityIdentifier(WellKnownSidType.NetworkServiceSid, null);
			ActiveDirectoryAccessRule rule = new ActiveDirectoryAccessRule(identity, ActiveDirectoryRights.ReadProperty, AccessControlType.Allow);
			activeDirectorySecurity.AddAccessRule(rule);
			rule = new ActiveDirectoryAccessRule(sid, ActiveDirectoryRights.ReadProperty, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All);
			activeDirectorySecurity.AddAccessRule(rule);
			rule = new ActiveDirectoryAccessRule(sid2, ActiveDirectoryRights.ReadProperty, AccessControlType.Allow, ActiveDirectorySecurityInheritance.All);
			activeDirectorySecurity.AddAccessRule(rule);
			return new RawSecurityDescriptor(activeDirectorySecurity.GetSecurityDescriptorBinaryForm(), 0)
			{
				Owner = new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null)
			};
		}

		// Token: 0x0600120B RID: 4619 RVA: 0x00058724 File Offset: 0x00056924
		private string CallerIdentityForLogging()
		{
			if (base.CallContext != null)
			{
				if (base.CallContext.IsExternalUser)
				{
					ExternalCallContext externalCallContext = (ExternalCallContext)base.CallContext;
					return "External:" + externalCallContext.EmailAddress;
				}
				if (base.CallContext.OriginalCallerContext.Sid != null)
				{
					return "CallerSid:" + base.CallContext.OriginalCallerContext.Sid.ToString();
				}
			}
			return "CallContextNull";
		}

		// Token: 0x0600120C RID: 4620 RVA: 0x000587A8 File Offset: 0x000569A8
		private void LoadOrganization()
		{
			OrganizationId organizationId = BaseMessageTrackingReport<RequestType, SingleItemType>.GetOrganizationId(this.Domain, base.CallerBudget);
			IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 596, "LoadOrganization", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
			IConfigurationSession tenantOrTopologyConfigurationSession = DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromOrganizationIdWithoutRbacScopesServiceOnly(organizationId), 600, "LoadOrganization", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
			ITopologyConfigurationSession globalConfigSession = DirectorySessionFactory.Default.CreateTopologyConfigurationSession(ConsistencyMode.IgnoreInvalid, ADSessionSettings.FromRootOrgScopeSet(), 609, "LoadOrganization", "f:\\15.00.1497\\sources\\dev\\services\\src\\Core\\servicecommands\\BaseMessageTrackingReport.cs");
			ClientContext clientContext;
			try
			{
				ExternalCallContext externalCallContext = base.CallContext as ExternalCallContext;
				if (externalCallContext != null)
				{
					clientContext = ClientContext.Create(externalCallContext.EmailAddress, externalCallContext.ExternalId, externalCallContext.WSSecurityHeader, externalCallContext.SharingSecurityHeader, externalCallContext.Budget, null, EWSSettings.ClientCulture);
				}
				else
				{
					clientContext = ClientContext.Create(base.CallContext.EffectiveCaller.ClientSecurityContext, base.CallerBudget, null, EWSSettings.ClientCulture);
				}
			}
			catch (AuthzException arg)
			{
				ExTraceGlobals.WebServiceTracer.TraceError<AuthzException>((long)this.GetHashCode(), "Exception while creating client context: {0}", arg);
				throw new MessageTrackingFatalException();
			}
			this.errors = new TrackingErrorCollection();
			this.directoryContext = new DirectoryContext(clientContext, organizationId, globalConfigSession, tenantOrTopologyConfigurationSession, tenantOrRootOrgRecipientSession, null, this.diagnosticsLevelEnum, this.errors, true);
			this.directoryContext.Acquire();
			this.acquiredDirectoryContext = true;
		}

		// Token: 0x0600120D RID: 4621 RVA: 0x00058900 File Offset: 0x00056B00
		private void CheckAccess()
		{
			if (!base.CallContext.IsExternalUser)
			{
				SecurityIdentifier sid = base.CallContext.OriginalCallerContext.Sid;
				if (sid != null)
				{
					RawSecurityDescriptor rawSecurityDescriptor;
					RawSecurityDescriptor rawSecurityDescriptor2;
					BaseMessageTrackingReport<RequestType, SingleItemType>.GetSecurityDescriptors(base.CallerBudget, out rawSecurityDescriptor, out rawSecurityDescriptor2);
					bool flag = false;
					try
					{
						flag = AuthzAuthorization.CheckSingleExtendedRight(sid, rawSecurityDescriptor2, WellKnownGuid.TokenSerializationRightGuid);
					}
					catch (Win32Exception arg)
					{
						ExTraceGlobals.WebServiceTracer.TraceError<SecurityIdentifier, Win32Exception>((long)this.GetHashCode(), "Failed call to CheckSingleExtendedRight for Caller Sid: {0} with Win32Exception: {1}", sid, arg);
						throw new MessageTrackingFatalException();
					}
					if (!flag)
					{
						ExTraceGlobals.WebServiceTracer.TraceError<SecurityIdentifier>((long)this.GetHashCode(), "Caller Sid: {0} was denied access based on server's Security Descriptor, trying local SD.", sid);
						if ((16 & AuthzAuthorization.CheckGenericPermission(sid, rawSecurityDescriptor, AccessMask.ReadProp)) != 16)
						{
							ExTraceGlobals.WebServiceTracer.TraceError<SecurityIdentifier>((long)this.GetHashCode(), "Caller Sid: {0} was denied access based on the tracking Security Descriptor", sid);
							throw new ServiceAccessDeniedException();
						}
					}
					ExTraceGlobals.WebServiceTracer.TraceDebug<SecurityIdentifier>((long)this.GetHashCode(), "Allowed access for {0} based on Security Descriptor", sid);
					return;
				}
				ExTraceGlobals.WebServiceTracer.TraceError((long)this.GetHashCode(), "Caller denied access, did not match security descriptor or organization relationship table");
				throw new ServiceAccessDeniedException();
			}
			else
			{
				ExternalCallContext externalCallContext = (ExternalCallContext)base.CallContext;
				SmtpAddress emailAddress = externalCallContext.EmailAddress;
				if (!emailAddress.IsValidAddress)
				{
					ExTraceGlobals.WebServiceTracer.TraceError<SmtpAddress>((long)this.GetHashCode(), "Caller SMTP address invalid: {0}", emailAddress);
					throw new ServiceAccessDeniedException();
				}
				if (!ServerCache.Instance.IsRemoteTrustedOrg(this.directoryContext.OrganizationId, emailAddress.Domain))
				{
					ExTraceGlobals.WebServiceTracer.TraceError<SmtpAddress>((long)this.GetHashCode(), "Caller {0} is denied access based on organization relationship", emailAddress);
					throw new ServiceAccessDeniedException();
				}
				ExTraceGlobals.WebServiceTracer.TraceError<SmtpAddress>((long)this.GetHashCode(), "Caller {0} allowed access based on organization relationship", emailAddress);
				return;
			}
		}

		// Token: 0x0600120E RID: 4622 RVA: 0x00058A98 File Offset: 0x00056C98
		private void AfterCheckAccess()
		{
			bool flag = this.AllowDebugMode();
			this.InitializeTracing(flag);
			TimeSpan timeout = this.GetTimeout(!flag);
			this.directoryContext.TrackingBudget = new TrackingEventBudget(this.errors, timeout);
		}

		// Token: 0x0600120F RID: 4623 RVA: 0x00058AD8 File Offset: 0x00056CD8
		private bool AllowDebugMode()
		{
			bool isExternalUser = base.CallContext.IsExternalUser;
			string text = string.Empty;
			if (isExternalUser)
			{
				text = ((ExternalCallContext)base.CallContext).EmailAddress.Domain;
			}
			return VariantConfiguration.InvariantNoFlightingSnapshot.MessageTracking.AllowDebugMode.Enabled || !isExternalUser || text.EndsWith(".microsoft.com", StringComparison.OrdinalIgnoreCase) || text.Equals("microsoft.com", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001210 RID: 4624 RVA: 0x00058B50 File Offset: 0x00056D50
		private void InitializeTracing(bool allowDebugMode)
		{
			if (!allowDebugMode && this.diagnosticsLevelEnum >= Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel.Etw)
			{
				this.diagnosticsLevelEnum = Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel.Verbose;
			}
			if (this.diagnosticsLevelEnum == Microsoft.Exchange.Transport.Logging.Search.DiagnosticsLevel.Etw)
			{
				CommonDiagnosticsLogTracer traceWriter = new CommonDiagnosticsLogTracer();
				TraceWrapper.SearchLibraryTracer.Register(traceWriter);
				this.perThreadTracingEnabled = true;
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x00058B94 File Offset: 0x00056D94
		private TimeSpan GetTimeout(bool enforceMaximumLimit)
		{
			TimeSpan timeSpan = Constants.DefaultServerSideTimeout;
			string empty = string.Empty;
			if (this.extendedProperties == null || this.extendedProperties.Timeout == null)
			{
				ExTraceGlobals.WebServiceTracer.TraceDebug((long)this.GetHashCode(), "No timeout specified in request, using default");
				return Constants.DefaultServerSideTimeout;
			}
			timeSpan = this.extendedProperties.Timeout.Value;
			if (timeSpan > Constants.DefaultServerSideTimeout && enforceMaximumLimit)
			{
				ExTraceGlobals.WebServiceTracer.TraceError<double>((long)this.GetHashCode(), "Client requested timeout of {0} ms which is above the maximum limit they are authorized for", timeSpan.TotalMilliseconds);
				timeSpan = Constants.DefaultServerSideTimeout;
			}
			ExTraceGlobals.WebServiceTracer.TraceError<double>((long)this.GetHashCode(), "Using client requested timeout of {0} ms", timeSpan.TotalMilliseconds);
			return timeSpan;
		}

		// Token: 0x04000CE3 RID: 3299
		private static object initLock = new object();

		// Token: 0x04000CE4 RID: 3300
		private static RawSecurityDescriptor trackingSecurityDescriptor;

		// Token: 0x04000CE5 RID: 3301
		private static RawSecurityDescriptor localServerSecurityDescriptor;

		// Token: 0x04000CE6 RID: 3302
		private static bool securityDescriptorInitialized;

		// Token: 0x04000CE7 RID: 3303
		protected DirectoryContext directoryContext;

		// Token: 0x04000CE8 RID: 3304
		protected TrackingExtendedProperties extendedProperties;

		// Token: 0x04000CE9 RID: 3305
		protected TrackingErrorCollection errors = TrackingErrorCollection.Empty;

		// Token: 0x04000CEA RID: 3306
		private DiagnosticsLevel diagnosticsLevelEnum;

		// Token: 0x04000CEB RID: 3307
		private bool perThreadTracingEnabled;

		// Token: 0x04000CEC RID: 3308
		private bool acquiredDirectoryContext;
	}
}
