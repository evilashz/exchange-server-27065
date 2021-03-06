using System;
using System.Collections.Generic;
using System.Globalization;
using System.Management.Automation;
using System.Threading;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Core;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Tracking;
using Microsoft.Exchange.InfoWorker.Common.Availability;
using Microsoft.Exchange.InfoWorker.Common.MessageTracking;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.Authorization;
using Microsoft.Exchange.Transport.Logging.Search;
using Microsoft.Exchange.VariantConfiguration;

namespace Microsoft.Exchange.Management.Tracking
{
	// Token: 0x0200009E RID: 158
	[Cmdlet("Get", "MessageTrackingReport")]
	public sealed class GetMessageTrackingReport : GetTenantADObjectWithIdentityTaskBase<MessageTrackingReportId, MessageTrackingReport>
	{
		// Token: 0x0600055D RID: 1373 RVA: 0x00014D7A File Offset: 0x00012F7A
		public GetMessageTrackingReport()
		{
			this.ResultSize = 1000U;
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x00014D92 File Offset: 0x00012F92
		// (set) Token: 0x0600055F RID: 1375 RVA: 0x00014DA9 File Offset: 0x00012FA9
		[Parameter(Mandatory = true, Position = 0, ValueFromPipeline = true, ValueFromPipelineByPropertyName = true)]
		public override MessageTrackingReportId Identity
		{
			get
			{
				return (MessageTrackingReportId)base.Fields["SearchResult"];
			}
			set
			{
				base.Fields["SearchResult"] = value;
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00014DBC File Offset: 0x00012FBC
		// (set) Token: 0x06000561 RID: 1377 RVA: 0x00014DD3 File Offset: 0x00012FD3
		[Parameter(Mandatory = false)]
		public SmtpAddress RecipientPathFilter
		{
			get
			{
				return (SmtpAddress)base.Fields["RecipientPathFilter"];
			}
			set
			{
				base.Fields["RecipientPathFilter"] = value;
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x00014DEB File Offset: 0x00012FEB
		// (set) Token: 0x06000563 RID: 1379 RVA: 0x00014E02 File Offset: 0x00013002
		[Parameter(Mandatory = false)]
		public string[] Recipients
		{
			get
			{
				return (string[])base.Fields["Recipients"];
			}
			set
			{
				base.Fields["Recipients"] = value;
			}
		}

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000564 RID: 1380 RVA: 0x00014E15 File Offset: 0x00013015
		// (set) Token: 0x06000565 RID: 1381 RVA: 0x00014E2C File Offset: 0x0001302C
		[Parameter(Mandatory = false)]
		public Unlimited<uint> ResultSize
		{
			get
			{
				return (Unlimited<uint>)base.Fields["ResultSize"];
			}
			set
			{
				base.Fields["ResultSize"] = value;
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x00014E44 File Offset: 0x00013044
		// (set) Token: 0x06000567 RID: 1383 RVA: 0x00014E5B File Offset: 0x0001305B
		[Parameter(Mandatory = false)]
		public MessageTrackingDetailLevel DetailLevel
		{
			get
			{
				return (MessageTrackingDetailLevel)base.Fields["DetailLevel"];
			}
			set
			{
				base.Fields["DetailLevel"] = value;
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000568 RID: 1384 RVA: 0x00014E73 File Offset: 0x00013073
		// (set) Token: 0x06000569 RID: 1385 RVA: 0x00014E8A File Offset: 0x0001308A
		[Parameter(Mandatory = false)]
		public ReportTemplate ReportTemplate
		{
			get
			{
				return (ReportTemplate)base.Fields["ReportTemplate"];
			}
			set
			{
				base.Fields["ReportTemplate"] = value;
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x0600056A RID: 1386 RVA: 0x00014EA2 File Offset: 0x000130A2
		// (set) Token: 0x0600056B RID: 1387 RVA: 0x00014EB9 File Offset: 0x000130B9
		[Parameter(Mandatory = false)]
		public SwitchParameter DoNotResolve
		{
			get
			{
				return (SwitchParameter)base.Fields["DoNotResolve"];
			}
			set
			{
				base.Fields["DoNotResolve"] = value;
			}
		}

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x0600056C RID: 1388 RVA: 0x00014ED1 File Offset: 0x000130D1
		// (set) Token: 0x0600056D RID: 1389 RVA: 0x00014EE8 File Offset: 0x000130E8
		[Parameter(Mandatory = false)]
		public _DeliveryStatus Status
		{
			get
			{
				return (_DeliveryStatus)base.Fields["Status"];
			}
			set
			{
				base.Fields["Status"] = value;
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x00014F00 File Offset: 0x00013100
		// (set) Token: 0x0600056F RID: 1391 RVA: 0x00014F26 File Offset: 0x00013126
		[Parameter(Mandatory = false)]
		public SwitchParameter BypassDelegateChecking
		{
			get
			{
				return (SwitchParameter)(base.Fields["BypassDelegationCheck"] ?? false);
			}
			set
			{
				base.Fields["BypassDelegationCheck"] = value;
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x00014F3E File Offset: 0x0001313E
		// (set) Token: 0x06000571 RID: 1393 RVA: 0x00014F5F File Offset: 0x0001315F
		[Parameter(Mandatory = false)]
		public TraceLevel TraceLevel
		{
			get
			{
				return (TraceLevel)(base.Fields["TraceLevel"] ?? TraceLevel.Low);
			}
			set
			{
				base.Fields["TraceLevel"] = value;
			}
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00014F77 File Offset: 0x00013177
		protected override void InternalBeginProcessing()
		{
			base.InternalBeginProcessing();
			if (base.NeedSuppressingPiiData && base.ExchangeRunspaceConfig != null)
			{
				this.ResolvePiiParameters();
				base.ExchangeRunspaceConfig.EnablePiiMap = true;
			}
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00014FA1 File Offset: 0x000131A1
		protected override bool IsKnownException(Exception exception)
		{
			return exception is TrackingSearchException || DataAccessHelper.IsDataAccessKnownException(exception) || base.IsKnownException(exception);
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00014FBC File Offset: 0x000131BC
		protected override IConfigDataProvider CreateSession()
		{
			return DirectorySessionFactory.Default.GetTenantOrTopologyConfigurationSession(base.DomainController, true, ConsistencyMode.PartiallyConsistent, base.SessionSettings, 239, "CreateSession", "f:\\15.00.1497\\sources\\dev\\Management\\src\\Management\\transport\\Tracking\\GetMessageTrackingReport.cs");
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x00014FEC File Offset: 0x000131EC
		protected override OrganizationId ResolveCurrentOrganization()
		{
			ADObjectId entryId;
			if (base.TryGetExecutingUserId(out entryId))
			{
				this.executingUser = base.TenantGlobalCatalogSession.ReadADRawEntry(entryId, new PropertyDefinition[]
				{
					ADRecipientSchema.EmailAddresses,
					ADRecipientSchema.PrimarySmtpAddress,
					ADUserSchema.Languages
				});
			}
			this.trackedUser = TrackedUser.Create(this.Identity.UserGuid, base.TenantGlobalCatalogSession);
			if (this.trackedUser == null || this.trackedUser.ADRecipient == null)
			{
				ExTraceGlobals.SearchLibraryTracer.TraceError<MessageTrackingReportId>((long)this.GetHashCode(), "Identity {0} could not be resolved to an ADRecipient or was an invalid ADRecipient. Search will fail", this.Identity);
				this.trackedUser = null;
				return base.ResolveCurrentOrganization();
			}
			OrganizationId organizationId = this.trackedUser.ADRecipient.OrganizationId;
			if (!base.ExecutingUserOrganizationId.Equals(OrganizationId.ForestWideOrgId) && !base.ExecutingUserOrganizationId.Equals(organizationId) && !organizationId.OrganizationalUnit.IsDescendantOf(base.ExecutingUserOrganizationId.OrganizationalUnit))
			{
				ExTraceGlobals.SearchLibraryTracer.TraceError<MessageTrackingReportId>((long)this.GetHashCode(), "Error, executing user is not parent of tracked mailbox: {0}", this.Identity);
				throw new ManagementObjectNotFoundException(Strings.ErrorManagementObjectNotFound(this.Identity.ToString()));
			}
			return organizationId;
		}

		// Token: 0x06000576 RID: 1398 RVA: 0x0001510C File Offset: 0x0001330C
		protected override void InternalValidate()
		{
			if (this.trackedUser == null)
			{
				base.WriteError(new ArgumentException(CoreStrings.InvalidMessageTrackingReportId, "MessageTrackingReportId"), ErrorCategory.InvalidArgument, this.Identity);
				return;
			}
			if (!base.Fields.Contains("ReportTemplate"))
			{
				this.ReportTemplate = ReportTemplate.Summary;
			}
			if (base.Fields.Contains("RecipientPathFilter") && (!this.RecipientPathFilter.IsValidAddress || this.RecipientPathFilter == SmtpAddress.Empty))
			{
				base.WriteError(new ArgumentException(Strings.InvalidRecipient, "RecipientPathFilter"), ErrorCategory.InvalidArgument, this.RecipientPathFilter);
				return;
			}
			if (this.ReportTemplate == ReportTemplate.RecipientPath)
			{
				if (!base.Fields.Contains("RecipientPathFilter"))
				{
					base.WriteError(new ArgumentException(CoreStrings.RecipientPathFilterNeeded, "ReportTemplate"), ErrorCategory.InvalidArgument, this.ReportTemplate);
				}
				if (!this.Identity.IsSender && !this.trackedUser.IsAddressOneOfProxies((string)this.RecipientPathFilter))
				{
					string message = string.Format(CoreStrings.NotAuthorizedToViewRecipientPath, this.trackedUser.SmtpAddress);
					base.WriteError(new ArgumentException(message), ErrorCategory.InvalidArgument, this.RecipientPathFilter);
				}
			}
			if (base.Fields.Contains("Status"))
			{
				if (this.ReportTemplate != ReportTemplate.Summary)
				{
					base.WriteError(new ArgumentException(CoreStrings.StatusFilterCannotBeSpecified, "Status"), ErrorCategory.InvalidArgument, this.Status);
				}
				this.status = new _DeliveryStatus?(this.Status);
			}
			if (!base.Fields.Contains("DetailLevel"))
			{
				this.DetailLevel = MessageTrackingDetailLevel.Basic;
			}
			if (!base.Fields.Contains("BypassDelegationCheck"))
			{
				this.BypassDelegateChecking = new SwitchParameter(false);
			}
			if (!base.Fields.Contains("DoNotResolve"))
			{
				this.DoNotResolve = new SwitchParameter(false);
			}
			if (this.BypassDelegateChecking.IsPresent)
			{
				this.BypassDelegateChecking.ToBool();
			}
			ADObjectId executingUserId;
			if (!this.BypassDelegateChecking && base.TryGetExecutingUserId(out executingUserId))
			{
				if (!this.trackedUser.IsMailbox)
				{
					Utils.HandleError(this, "User for which tracking is being done does not exist", CoreStrings.TrackingSearchNotAuthorized, true, false, ErrorCategory.InvalidArgument, this.Identity);
					return;
				}
				string debugMessage;
				if (!Utils.AccessCheck((ADObjectId)this.trackedUser.ADUser.Identity, executingUserId, base.TenantGlobalCatalogSession, out debugMessage))
				{
					Utils.HandleError(this, debugMessage, CoreStrings.TrackingSearchNotAuthorized, true, false, ErrorCategory.InvalidArgument, this.trackedUser.ADUser);
				}
			}
			base.InternalValidate();
		}

		// Token: 0x06000577 RID: 1399 RVA: 0x000153A4 File Offset: 0x000135A4
		protected override void InternalProcessRecord()
		{
			bool flag;
			if ("Exchange Control Panel".Equals(base.Host.Name, StringComparison.OrdinalIgnoreCase))
			{
				flag = ServerCache.Instance.InitializeIfNeeded(HostId.ECPApplicationPool);
			}
			else
			{
				flag = ServerCache.Instance.InitializeIfNeeded(HostId.PowershellApplicationPool);
			}
			if (!flag)
			{
				Utils.HandleError(this, "Cannot initialize AD configuration", CoreStrings.TrackingErrorFailedToInitialize, this.BypassDelegateChecking, false, ErrorCategory.ObjectNotFound, null);
			}
			DirectoryContext directoryContext = null;
			int num = 0;
			MessageTrackingReport messageTrackingReport = null;
			TrackingEventBudget trackingEventBudget = null;
			try
			{
				TrackingEventBudget.AcquireThread();
				if (this.TraceLevel == TraceLevel.High)
				{
					CommonDiagnosticsLogTracer traceWriter = new CommonDiagnosticsLogTracer();
					TraceWrapper.SearchLibraryTracer.Register(traceWriter);
					BaseTrace.CurrentThreadSettings.EnableTracing();
				}
				if (base.ExchangeRunspaceConfig == null)
				{
					ExTraceGlobals.TaskTracer.TraceError((long)this.GetHashCode(), "Get-MessageTrackingReport permissions cannot be retrieved because the ExchangeRunspaceConfiguration is null");
					base.WriteError(new TrackingSearchException(CoreStrings.TrackingSearchNotAuthorized), ErrorCategory.InvalidOperation, this.Identity);
				}
				ReportConstraints reportConstraints = new ReportConstraints();
				SmtpAddress[] recipientPathFilter = null;
				if (base.Fields.Contains("RecipientPathFilter"))
				{
					recipientPathFilter = new SmtpAddress[]
					{
						this.RecipientPathFilter
					};
				}
				reportConstraints.BypassDelegateChecking = this.BypassDelegateChecking;
				reportConstraints.DetailLevel = this.DetailLevel;
				reportConstraints.DoNotResolve = this.DoNotResolve;
				reportConstraints.RecipientPathFilter = recipientPathFilter;
				reportConstraints.Recipients = this.Recipients;
				reportConstraints.ReportTemplate = this.ReportTemplate;
				reportConstraints.ResultSize = this.ResultSize;
				reportConstraints.TrackingAsSender = this.Identity.IsSender;
				reportConstraints.Sender = SmtpAddress.Empty;
				ReportConstraints reportConstraints2 = reportConstraints;
				_DeliveryStatus? deliveryStatus = this.status;
				int? num2 = (deliveryStatus != null) ? new int?((int)deliveryStatus.GetValueOrDefault()) : null;
				reportConstraints2.Status = ((num2 != null) ? new DeliveryStatus?((DeliveryStatus)num2.GetValueOrDefault()) : null);
				reportConstraints.ReturnQueueEvents = this.ShouldReturnQueueEvents();
				if (this.Identity.IsSender)
				{
					ADRecipient adrecipient = base.TenantGlobalCatalogSession.FindByExchangeGuid(this.Identity.UserGuid);
					if (adrecipient != null)
					{
						reportConstraints.Sender = adrecipient.PrimarySmtpAddress;
					}
				}
				MultiValuedProperty<CultureInfo> executingUserLanguages = base.ExchangeRunspaceConfig.ExecutingUserLanguages;
				CultureInfo clientCulture = (executingUserLanguages != null && executingUserLanguages.Count > 0) ? executingUserLanguages[0] : CultureInfo.InvariantCulture;
				ClientContext clientContext = null;
				ClientSecurityContext clientSecurityContext = null;
				bool flag2 = false;
				try
				{
					clientSecurityContext = Utils.GetSecurityContextForUser(base.ExchangeRunspaceConfig.SecurityAccessToken, base.ExchangeRunspaceConfig.DelegatedPrincipal, this.trackedUser.ADUser);
					clientContext = ClientContext.Create(clientSecurityContext, base.CurrentOrganizationId, null, null, clientCulture, null);
					OrganizationId currentOrganizationId = base.TenantGlobalCatalogSession.SessionSettings.CurrentOrganizationId;
					TrackingErrorCollection errors = new TrackingErrorCollection();
					TimeSpan timeout = Utils.GetTimeout(this.BypassDelegateChecking);
					trackingEventBudget = new TrackingEventBudget(errors, timeout);
					directoryContext = new DirectoryContext(clientContext, currentOrganizationId, base.GlobalConfigSession, this.ConfigurationSession, base.TenantGlobalCatalogSession, trackingEventBudget, SearchMessageTrackingReport.GetDiagnosticsLevel(this.TraceLevel, base.Fields.IsModified("TraceLevel")), errors, false);
					directoryContext.Acquire();
					flag2 = true;
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Task, Names<DeliveryReportsTask>.Map[1]);
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.OpType, Names<OpType>.Map[0]);
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Mid, this.Identity.ToString());
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Data1, Names<ReportTemplate>.Map[(int)this.ReportTemplate]);
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Usr, this.trackedUser.SmtpAddress.ToString());
					if (this.BypassDelegateChecking)
					{
						ADObjectId adobjectId;
						if (base.TryGetExecutingUserId(out adobjectId))
						{
							directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.ExUser, adobjectId.Name);
						}
						else
						{
							ExTraceGlobals.TaskTracer.TraceDebug((long)this.GetHashCode(), "ExecutingUserId is null.");
						}
					}
					directoryContext.DiagnosticsContext.WriteEvent();
					LogCache logCache = new LogCache(DateTime.MinValue, DateTime.MaxValue, directoryContext.TrackingBudget);
					this.getMessageTrackingReport = new GetMessageTrackingReportImpl(directoryContext, SearchScope.World, this.Identity.InternalMessageTrackingReportId, logCache, reportConstraints);
					MessageTrackingReport messageTrackingReport2 = this.getMessageTrackingReport.Execute();
					MultiValuedProperty<CultureInfo> userLanguages;
					if (this.executingUser != null)
					{
						userLanguages = (this.executingUser[ADUserSchema.Languages] as MultiValuedProperty<CultureInfo>);
					}
					else
					{
						userLanguages = new MultiValuedProperty<CultureInfo>(Thread.CurrentThread.CurrentUICulture);
					}
					bool flag3 = false;
					if (messageTrackingReport2 == null)
					{
						base.WriteError(new TrackingExceptionNoResultsDueToLogsExpired(), ErrorCategory.ObjectNotFound, this.Identity);
					}
					else
					{
						if (messageTrackingReport2.HasHandedOffPaths && !this.Identity.IsSender)
						{
							flag3 = true;
							bool flag4 = this.TryGetReportForRecipientOrganization(directoryContext, logCache, reportConstraints, userLanguages, out messageTrackingReport2);
							if (flag4 && messageTrackingReport2 == null)
							{
								base.WriteError(new TrackingExceptionNoResultsDueToUntrackableMessagePath(), ErrorCategory.ObjectNotFound, this.Identity);
							}
							else if (!flag4)
							{
								base.WriteError(new TrackingExceptionNoResultsDueToLogsNotFound(), ErrorCategory.ObjectNotFound, this.Identity);
							}
						}
						messageTrackingReport = MessageTrackingReport.Create(this.ConfigurationSession, base.TenantGlobalCatalogSession, userLanguages, this.ReportTemplate == ReportTemplate.Summary, this.DetailLevel == MessageTrackingDetailLevel.Verbose, this.Identity.IsSender, messageTrackingReport2, this.DoNotResolve, this.getMessageTrackingReport.Errors.Errors.Count == 0);
						if (messageTrackingReport == null)
						{
							ExTraceGlobals.TaskTracer.TraceDebug<int>((long)this.GetHashCode(), "Report is null while internalReport is not null and has {0} events", (messageTrackingReport2.RecipientTrackingEvents == null) ? 0 : messageTrackingReport2.RecipientTrackingEvents.Length);
							base.WriteError(new TrackingExceptionNoResultsDueToLogsNotFound(), ErrorCategory.ObjectNotFound, this.Identity);
						}
						else
						{
							num = ((messageTrackingReport.RecipientTrackingEvents == null) ? 0 : messageTrackingReport.RecipientTrackingEvents.Length);
							messageTrackingReport = this.FilterReport(messageTrackingReport);
							if (messageTrackingReport == null && !flag3 && !this.Identity.IsSender)
							{
								bool flag5 = this.TryGetReportForRecipientOrganization(directoryContext, logCache, reportConstraints, executingUserLanguages, out messageTrackingReport2);
								if (!flag5)
								{
									base.WriteError(new TrackingExceptionNoResultsDueToLogsNotFound(), ErrorCategory.ObjectNotFound, this.Identity);
								}
								else if (flag5 && messageTrackingReport2 == null)
								{
									base.WriteError(new TrackingExceptionNoResultsDueToUntrackableMessagePath(), ErrorCategory.ObjectNotFound, this.Identity);
								}
								else
								{
									messageTrackingReport = MessageTrackingReport.Create(this.ConfigurationSession, base.TenantGlobalCatalogSession, userLanguages, this.ReportTemplate == ReportTemplate.Summary, this.DetailLevel == MessageTrackingDetailLevel.Verbose, this.Identity.IsSender, messageTrackingReport2, this.DoNotResolve, this.getMessageTrackingReport.Errors.Errors.Count == 0);
									messageTrackingReport = this.FilterReport(messageTrackingReport);
									if (messageTrackingReport == null)
									{
										base.WriteError(new TrackingExceptionNoResultsDueToUntrackableMessagePath(), ErrorCategory.ObjectNotFound, this.Identity);
									}
								}
							}
							if (messageTrackingReport != null)
							{
								if (base.NeedSuppressingPiiData)
								{
									Utils.RedactRecipientTrackingEvents(messageTrackingReport.RecipientTrackingEvents, this);
								}
								this.WriteResult(messageTrackingReport);
							}
						}
					}
				}
				catch (AuthzException)
				{
					base.WriteError(new TrackingSearchException(CoreStrings.TrackingSearchNotAuthorized), ErrorCategory.InvalidOperation, this.Identity);
				}
				finally
				{
					if (directoryContext != null && flag2)
					{
						directoryContext.Yield();
					}
					if (clientContext != null)
					{
						clientContext.Dispose();
					}
					if (clientSecurityContext != null)
					{
						clientSecurityContext.Dispose();
					}
				}
			}
			catch (TrackingTransientException ex)
			{
				Utils.HandleTrackingException(directoryContext, ex, this, false, this.BypassDelegateChecking);
			}
			catch (TrackingFatalException ex2)
			{
				Utils.HandleTrackingException(directoryContext, ex2, this, false, this.BypassDelegateChecking);
			}
			catch (DataSourceOperationException ex3)
			{
				Utils.HandleError(this, ex3.ToString(), ex3.LocalizedString, this.BypassDelegateChecking, false, ErrorCategory.InvalidData, null);
			}
			catch (DataValidationException ex4)
			{
				Utils.HandleError(this, ex4.ToString(), ex4.LocalizedString, this.BypassDelegateChecking, false, ErrorCategory.InvalidData, null);
			}
			catch (TransientException ex5)
			{
				Utils.HandleError(this, ex5.ToString(), ex5.LocalizedString, this.BypassDelegateChecking, true, ErrorCategory.InvalidData, null);
			}
			finally
			{
				if ((this.BypassDelegateChecking || this.ReportTemplate == ReportTemplate.RecipientPath || messageTrackingReport == null || messageTrackingReport.RecipientTrackingEvents == null || messageTrackingReport.RecipientTrackingEvents.Length == 0) && this.getMessageTrackingReport != null)
				{
					Utils.WriteWarnings(this, directoryContext, this.BypassDelegateChecking, this.getMessageTrackingReport.Errors.Errors);
				}
				if (trackingEventBudget != null)
				{
					trackingEventBudget.Dispose();
				}
				if (directoryContext != null)
				{
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Task, Names<DeliveryReportsTask>.Map[1]);
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.OpType, Names<OpType>.Map[1]);
					directoryContext.DiagnosticsContext.AddProperty(DiagnosticProperty.Cnt, num);
					directoryContext.DiagnosticsContext.WriteEvent();
					Utils.WriteDiagnostics(this, directoryContext.DiagnosticsContext, base.NeedSuppressingPiiData);
				}
				if (this.TraceLevel == TraceLevel.High)
				{
					TraceWrapper.SearchLibraryTracer.Unregister();
					BaseTrace.CurrentThreadSettings.DisableTracing();
				}
				TrackingEventBudget.ReleaseThread();
				if (directoryContext == null || Utils.AreAnyErrorsLocalToThisForest(directoryContext.Errors.Errors))
				{
					PerfCounterData.ResultCounter.AddFailure();
				}
				else
				{
					PerfCounterData.ResultCounter.AddSuccess();
				}
				if (ServerCache.Instance.HostId == HostId.ECPApplicationPool)
				{
					InfoWorkerMessageTrackingPerformanceCounters.MessageTrackingFailureRateTask.RawValue = (long)PerfCounterData.ResultCounter.FailurePercentage;
				}
			}
		}

		// Token: 0x06000578 RID: 1400 RVA: 0x00015D04 File Offset: 0x00013F04
		private void ResolvePiiParameters()
		{
			SmtpAddress recipientPathFilter;
			if (base.Fields.Contains("RecipientPathFilter") && Utils.TryResolveRedactedSmtpAddress(this.RecipientPathFilter, this, out recipientPathFilter))
			{
				this.RecipientPathFilter = recipientPathFilter;
			}
			Utils.TryResolveRedactedStringArray(this.Recipients, this);
		}

		// Token: 0x06000579 RID: 1401 RVA: 0x00015D48 File Offset: 0x00013F48
		private bool TryGetReportForRecipientOrganization(DirectoryContext directoryContext, LogCache logCache, ReportConstraints constraints, IList<CultureInfo> userLanguages, out MessageTrackingReport report)
		{
			report = null;
			SearchMessageTrackingReportImpl searchMessageTrackingReportImpl = new SearchMessageTrackingReportImpl(directoryContext, SearchScope.Organization, null, null, null, new TrackedUser[]
			{
				this.trackedUser
			}, logCache, null, this.Identity.MessageId, this.ResultSize, false, true, true, false);
			List<MessageTrackingSearchResult> messages = searchMessageTrackingReportImpl.Execute();
			List<MessageTrackingSearchResult> list = searchMessageTrackingReportImpl.FilterResultsBySubjectAndRecipients(messages, userLanguages);
			if (list != null && list.Count > 0)
			{
				MessageTrackingReportId messageTrackingReportId = list[0].MessageTrackingReportId;
				Guid exchangeGuid = this.trackedUser.ADUser.ExchangeGuid;
				MessageTrackingReportId messageTrackingReportId2 = new MessageTrackingReportId(messageTrackingReportId.MessageId, messageTrackingReportId.Server, messageTrackingReportId.InternalMessageId, exchangeGuid, messageTrackingReportId.Domain, false);
				GetMessageTrackingReportImpl getMessageTrackingReportImpl = new GetMessageTrackingReportImpl(directoryContext, SearchScope.Organization, messageTrackingReportId2, logCache, constraints);
				report = getMessageTrackingReportImpl.Execute();
				return true;
			}
			return false;
		}

		// Token: 0x0600057A RID: 1402 RVA: 0x00015E08 File Offset: 0x00014008
		private bool ShouldReturnQueueEvents()
		{
			return VariantConfiguration.InvariantNoFlightingSnapshot.MessageTracking.QueueViewerDiagnostics.Enabled && this.BypassDelegateChecking && this.DetailLevel == MessageTrackingDetailLevel.Verbose;
		}

		// Token: 0x0600057B RID: 1403 RVA: 0x00015E48 File Offset: 0x00014048
		private MessageTrackingReport FilterReport(MessageTrackingReport report)
		{
			if (report == null)
			{
				return report;
			}
			if (!this.Identity.IsSender)
			{
				report.RecipientAddresses = new SmtpAddress[]
				{
					this.trackedUser.SmtpAddress
				};
				report.RecipientDisplayNames = new string[]
				{
					this.trackedUser.DisplayName
				};
			}
			if (report.RecipientTrackingEvents.Length == 0)
			{
				return report;
			}
			bool flag = false;
			List<RecipientTrackingEvent> list = null;
			RecipientTrackingEvent[] recipientTrackingEvents = report.RecipientTrackingEvents;
			int i = 0;
			while (i < recipientTrackingEvents.Length)
			{
				RecipientTrackingEvent recipientTrackingEvent = recipientTrackingEvents[i];
				if (this.ReportTemplate == ReportTemplate.Summary)
				{
					if (this.EventMeetsCriteriaForSummaryReport(recipientTrackingEvent))
					{
						report.IncrementEventTypeCount(recipientTrackingEvent.Status);
						goto IL_A7;
					}
				}
				else if (this.EventMeetsCriteriaForPathReport(recipientTrackingEvent))
				{
					goto IL_A7;
				}
				IL_109:
				i++;
				continue;
				IL_A7:
				if (flag)
				{
					goto IL_109;
				}
				if (list != null && (long)list.Count >= (long)((ulong)this.ResultSize.Value))
				{
					flag = true;
					if (this.ReportTemplate != ReportTemplate.Summary)
					{
						break;
					}
					goto IL_109;
				}
				else
				{
					if (list == null)
					{
						list = new List<RecipientTrackingEvent>();
					}
					if (this.ResultSize.Value > 0U)
					{
						list.Add(recipientTrackingEvent);
					}
					if (this.Identity.IsSender || this.ReportTemplate != ReportTemplate.Summary)
					{
						goto IL_109;
					}
					break;
				}
			}
			if (flag && this.ReportTemplate != ReportTemplate.Summary)
			{
				this.WriteWarning(CoreStrings.TrackingWarningTooManyEvents);
			}
			if (list == null)
			{
				return null;
			}
			if (0 < list.Count && !this.BypassDelegateChecking)
			{
				EventDescription eventDescription = EventDescription.Submitted;
				bool flag2 = false;
				if (!this.Identity.IsSender)
				{
					eventDescription = ((this.ReportTemplate == ReportTemplate.Summary) ? list[0].EventDescriptionEnum : list[list.Count - 1].EventDescriptionEnum);
					flag2 = true;
				}
				else if (ReportTemplate.RecipientPath == this.ReportTemplate)
				{
					eventDescription = list[list.Count - 1].EventDescriptionEnum;
					flag2 = true;
				}
				EventDescriptionInformation eventDescriptionInformation;
				if (!EnumAttributeInfo<EventDescription, EventDescriptionInformation>.TryGetValue((int)eventDescription, out eventDescriptionInformation))
				{
					throw new InvalidOperationException(string.Format("Value {0} was not annotated", eventDescription));
				}
				if (flag2 && eventDescriptionInformation.IsTerminal)
				{
					this.getMessageTrackingReport.Errors.ResetAllErrors();
				}
			}
			report.RecipientTrackingEvents = list.ToArray();
			return report;
		}

		// Token: 0x0600057C RID: 1404 RVA: 0x00016054 File Offset: 0x00014254
		private bool EventMeetsCriteriaForSummaryReport(RecipientTrackingEvent recipEvent)
		{
			if (!this.Identity.IsSender && !this.trackedUser.IsAddressOneOfProxies((string)recipEvent.RecipientAddress))
			{
				return false;
			}
			if (this.Identity.IsSender && this.Recipients != null && this.Recipients.Length > 0)
			{
				bool flag = false;
				string text = recipEvent.RecipientAddress.ToString();
				foreach (string value in this.Recipients)
				{
					if (text.IndexOf(value, StringComparison.OrdinalIgnoreCase) != -1)
					{
						flag = true;
						break;
					}
				}
				if (!flag && !this.IsAddressOneOfRecipientFilterProxies(text))
				{
					return false;
				}
			}
			return this.status == null || !(recipEvent.Status != this.status);
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00016138 File Offset: 0x00014338
		private bool IsAddressOneOfRecipientFilterProxies(string address)
		{
			if (!this.proxyAddressesFromRecipientsInitialized)
			{
				this.proxyAddressesFromRecipients = Utils.GetAllSmtpProxiesForRecipientFilters(this.Recipients, base.TenantGlobalCatalogSession);
				this.proxyAddressesFromRecipientsInitialized = true;
			}
			return this.proxyAddressesFromRecipients != null && this.proxyAddressesFromRecipients.Contains(address);
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00016184 File Offset: 0x00014384
		private bool EventMeetsCriteriaForPathReport(RecipientTrackingEvent recipEvent)
		{
			bool result = this.BypassDelegateChecking;
			if (this.Identity.IsSender)
			{
				if (recipEvent.EventDescriptionEnum == EventDescription.MovedToFolderByInboxRule || recipEvent.EventDescriptionEnum == EventDescription.ForwardedToDelegateAndDeleted)
				{
					return result;
				}
			}
			else if (recipEvent.BccRecipient)
			{
				if (recipEvent.EventDescriptionEnum == EventDescription.Expanded)
				{
					return result;
				}
				if (!recipEvent.RecipientAddress.Equals(this.trackedUser.SmtpAddress))
				{
					if (recipEvent.EventDescriptionEnum == EventDescription.PendingModeration || recipEvent.EventDescriptionEnum == EventDescription.ApprovedModeration || recipEvent.EventDescriptionEnum == EventDescription.FailedModeration)
					{
						return result;
					}
					recipEvent.RecipientAddress = this.trackedUser.SmtpAddress;
					recipEvent.RecipientDisplayName = this.trackedUser.DisplayName;
				}
			}
			return true;
		}

		// Token: 0x040001F8 RID: 504
		private GetMessageTrackingReportImpl getMessageTrackingReport;

		// Token: 0x040001F9 RID: 505
		private ADRawEntry executingUser;

		// Token: 0x040001FA RID: 506
		private _DeliveryStatus? status;

		// Token: 0x040001FB RID: 507
		private TrackedUser trackedUser;

		// Token: 0x040001FC RID: 508
		private HashSet<string> proxyAddressesFromRecipients;

		// Token: 0x040001FD RID: 509
		private bool proxyAddressesFromRecipientsInitialized;
	}
}
