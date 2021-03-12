using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using System.Text;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Configuration.Common;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.ApplicationLogic;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Data.Storage.Auditing;
using Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Exchange.Management.SystemConfigurationTasks;
using Microsoft.Exchange.Provisioning;
using Microsoft.Exchange.SoapWebClient.EWS;
using Microsoft.Exchange.WebServices.Data;
using Microsoft.Mapi;

namespace Microsoft.Exchange.ProvisioningAgent
{
	// Token: 0x02000008 RID: 8
	internal class AdminLogProvisioningHandler : ProvisioningHandlerBase
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002BB8 File Offset: 0x00000DB8
		static AdminLogProvisioningHandler()
		{
			AdminLogProvisioningHandler.blockedOrganizations = new CacheWithExpiration<OrganizationId, AdminLogProvisioningHandler.OrganizationBlockTime>(256, AdminLogProvisioningHandler.BlockedOrganizationIdsCacheTimeout, null);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002C29 File Offset: 0x00000E29
		public AdminLogProvisioningHandler(ConfigurationCache configurationCache)
		{
			this.configurationCache = configurationCache;
			DCAdminActionsLogger.Start();
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000031 RID: 49 RVA: 0x00002C3D File Offset: 0x00000E3D
		private AdminAuditLogRecord AuditLogFields
		{
			get
			{
				if (this.logFields == null)
				{
					this.logFields = new AdminAuditLogRecord(AdminLogProvisioningHandler.Tracer);
				}
				return this.logFields;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000032 RID: 50 RVA: 0x00002C60 File Offset: 0x00000E60
		private static AdminAuditPerfCountersInstance PerfCounters
		{
			get
			{
				if (AdminLogProvisioningHandler.perfCounters != null)
				{
					return AdminLogProvisioningHandler.perfCounters;
				}
				lock (AdminLogProvisioningHandler.syncObject)
				{
					if (AdminLogProvisioningHandler.perfCounters != null)
					{
						return AdminLogProvisioningHandler.perfCounters;
					}
					using (Process currentProcess = Process.GetCurrentProcess())
					{
						string text = null;
						for (int i = 0; i < 500; i++)
						{
							text = ((i == 0) ? currentProcess.ProcessName : (currentProcess.ProcessName + "#" + i.ToString()));
							if (!AdminAuditPerfCounters.InstanceExists(text))
							{
								break;
							}
							text = null;
						}
						if (text == null)
						{
							text = currentProcess.ProcessName;
						}
						AdminAuditPerfCountersInstance instance = AdminAuditPerfCounters.GetInstance(text);
						instance.ProcessId.RawValue = (long)currentProcess.Id;
						AdminLogProvisioningHandler.perfCounters = instance;
					}
				}
				return AdminLogProvisioningHandler.perfCounters;
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002D90 File Offset: 0x00000F90
		public override ProvisioningValidationError[] Validate(IConfigurable readOnlyIConfigurable)
		{
			Exception exception = null;
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					try
					{
						this.InternalValidate(readOnlyIConfigurable);
					}
					catch (MultipleAdminAuditLogConfigException exception2)
					{
						exception = exception2;
					}
				});
			}
			catch (GrayException exception)
			{
				GrayException exception3;
				exception = exception3;
			}
			catch (ServerInMMException ex)
			{
				return new ProvisioningValidationError[]
				{
					new ProvisioningValidationError(ex.LocalizedString, ExchangeErrorCategory.ServerOperation, ex)
				};
			}
			finally
			{
				if (exception != null)
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceError<Exception>((long)this.GetHashCode(), "An exception handled in AdminLogProvisioningHandler.Validate(). Error:\n {0}", exception);
					}
					AdminAuditLogHealthHandler.GetInstance().Health.AddException(exception);
				}
			}
			return null;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002E9C File Offset: 0x0000109C
		public override void OnComplete(bool succeeded, Exception e)
		{
			DiagnosticContext.Reset();
			try
			{
				GrayException.MapAndReportGrayExceptions(delegate()
				{
					this.InternalOnComplete(succeeded, e);
				});
			}
			catch (GrayException ex)
			{
				DiagnosticContext.TraceLocation((LID)35516U);
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceError<GrayException>((long)this.GetHashCode(), "GrayException handled in AdminLogProvisioningHandler.OnComplete(). Error:\n {0}", ex);
				}
				AdminAuditLogHealthHandler.GetInstance().Health.AddException(ex);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002F3C File Offset: 0x0000113C
		private void InternalValidate(IConfigurable readOnlyIConfigurable)
		{
			base.LogMessage(Strings.EnteredValidate);
			if (AuditFeatureManager.IsAdminAuditCmdletBlockListEnabled() && AdminLogCmdletSkipList.ShouldSkipCmdlet(base.TaskName))
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} skipping auditing for a blocked Cmdlet: {1}", this, base.TaskName);
				}
				return;
			}
			if (AdminAuditSettings.Instance.BypassForwardSync && AdminLogProvisioningHandler.Caller.IsMsoSyncServiceAccount(base.UserScope.UserId))
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} skipping logging for forwardsync-issued cmdlets.", this);
				}
				return;
			}
			this.realCurrentOrgId = null;
			ADObject adobject = readOnlyIConfigurable as ADObject;
			if (adobject != null)
			{
				this.realCurrentOrgId = adobject.OrganizationId;
			}
			if (this.realCurrentOrgId == null)
			{
				this.realCurrentOrgId = base.UserScope.CurrentOrganizationId;
			}
			AdminLogProvisioningHandler.OrganizationBlockTime organizationBlockTime;
			bool flag = AdminLogProvisioningHandler.blockedOrganizations.TryGetValue(this.realCurrentOrgId, DateTime.UtcNow, out organizationBlockTime);
			if (flag)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, OrganizationId>((long)this.GetHashCode(), "{0} skipping logging for blocked organization: {1}.", this, this.realCurrentOrgId);
				}
				return;
			}
			this.configWrapper = this.configurationCache.FindOrCreate(this.realCurrentOrgId, base.LogMessage);
			foreach (object obj in base.UserSpecifiedParameters.Keys)
			{
				string text = (string)obj;
				if (string.Equals(text, "WhatIf", StringComparison.OrdinalIgnoreCase))
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Cmdlet name {1}. Parameter: {2}. Return from handler Validate since the parameter is WhatIf.", this, base.TaskName, text);
					}
					return;
				}
			}
			if (this.configWrapper.CmdletAlwaysLogged(base.TaskName))
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Cmdlet {1} always gets logged because it updates either AdminAuditLogConfig or CmdletExtensionAgent object.", this, base.TaskName);
				}
				this.realConfigObject = (ADConfigurationObject)readOnlyIConfigurable;
				this.alwaysLogging = true;
			}
			else if (!this.configWrapper.LoggingEnabled)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} LoggingEnabled is false. Return from handler Validate.", this);
					return;
				}
				return;
			}
			else
			{
				if (!this.configWrapper.ShouldLogBasedOnCmdletName(base.TaskName))
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Cmdlet does not match. Return from handler Validate.", this);
					}
					return;
				}
				foreach (object obj2 in base.UserSpecifiedParameters.Keys)
				{
					string text2 = (string)obj2;
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Cmdlet name {1}. Parameter: {2}", this, base.TaskName, text2);
					}
					if (this.configWrapper.ParameterWildcardMatcher.IsMatch(text2))
					{
						this.parameterMatched = true;
					}
				}
				if (base.UserSpecifiedParameters.Keys.Count == 0 && this.configWrapper.ParameterWildcardMatcher.IsMatch(string.Empty))
				{
					this.parameterMatched = true;
				}
				if (!this.parameterMatched)
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, MultiValuedProperty<string>>((long)this.GetHashCode(), "{0} None of the parameters matched settings {1}. Return from handler Validate.", this, base.TaskName, this.configWrapper.AdminAuditLogConfig.AdminAuditLogParameters);
					}
					return;
				}
				if (readOnlyIConfigurable == null)
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name: {1}. readOnlyIConfigurable is null.", this, base.TaskName);
					}
					this.realConfigObject = null;
				}
				else if (readOnlyIConfigurable is ADPresentationObject)
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name: {1}. readOnlyIConfigurable is a ADPresentationObject.", this, base.TaskName);
					}
					this.realConfigObject = ((ADPresentationObject)readOnlyIConfigurable).DataObject;
				}
				else
				{
					if (!(readOnlyIConfigurable is ConfigurableObject))
					{
						if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
						{
							AdminLogProvisioningHandler.Tracer.TraceError<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Type of readOnlyIConfigurable is neither ADPresentationObject nor ConfigurableObject. Skip logging this object.", this);
						}
						this.parameterMatched = false;
						return;
					}
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name: {1}. readOnlyIConfigurable is a ConfigurableObject.", this, base.TaskName);
					}
					this.realConfigObject = (ConfigurableObject)readOnlyIConfigurable;
				}
			}
			string text3 = string.Empty;
			if (AdminAuditLogHelper.IsDiscoverySearchModifierCmdlet(base.TaskName))
			{
				text3 = this.GetDiscoverySearchName();
			}
			else if (this.realConfigObject != null && this.realConfigObject.Identity != null)
			{
				text3 = this.realConfigObject.Identity.ToString();
			}
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Object '{1}' was saved for logging in Validate. Cmdlet name '{2}'", this, text3, base.TaskName);
			}
			PropertyBag modifiedPropertyValues;
			PropertyBag originalPropertyValues;
			this.GetChangedProperties(this.realConfigObject, out modifiedPropertyValues, out originalPropertyValues);
			this.AuditLogFields.ModifiedPropertyValues = modifiedPropertyValues;
			this.AuditLogFields.OriginalPropertyValues = originalPropertyValues;
			this.AuditLogFields.ObjectModified = text3;
			if (this.realConfigObject != null && this.realConfigObject.Identity != null && !this.realConfigObject.ToString().Equals(this.realConfigObject.Identity.ToString()))
			{
				this.AuditLogFields.ModifiedObjectResolvedName = this.realConfigObject.ToString();
			}
			else
			{
				this.AuditLogFields.ModifiedObjectResolvedName = this.AuditLogFields.ObjectModified;
			}
			base.LogMessage(Strings.ExitedValidate);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x000034E4 File Offset: 0x000016E4
		private void InternalOnComplete(bool succeeded, Exception e)
		{
			base.LogMessage(Strings.EnteredOnComplete);
			if (!succeeded && e != null && e.GetType().FullName.Equals(AdminLogProvisioningHandler.ShouldContinueExceptionName, StringComparison.OrdinalIgnoreCase))
			{
				succeeded = true;
				e = null;
			}
			if (!succeeded)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Failed operation, skipping auditing.", this);
				}
				DiagnosticContext.TraceLocation((LID)51900U);
				return;
			}
			if (!this.parameterMatched && !this.alwaysLogging)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Either cmdlet/parameters did not match, or unconditionally logging not required for this cmdlet. Return from handler OnComplete.", this);
				}
				DiagnosticContext.TraceLocation((LID)45756U);
				return;
			}
			Stopwatch stopwatch = Stopwatch.StartNew();
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Proceeding to populate various fields in OnComplete.", this);
			}
			this.AuditLogFields.Cmdlet = base.TaskName;
			this.AuditLogFields.Parameters = base.UserSpecifiedParameters;
			if (base.UserScope != null)
			{
				if (base.UserScope.UserId != null)
				{
					this.AuditLogFields.UserId = base.UserScope.UserId;
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Task name {1}. UserId {2}", this, base.TaskName, this.AuditLogFields.UserId);
					}
				}
				else
				{
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name {1}. UserId is null. Getting current process and user names.", this, base.TaskName);
					}
					Process currentProcess = Process.GetCurrentProcess();
					using (WindowsIdentity current = WindowsIdentity.GetCurrent())
					{
						this.AuditLogFields.UserId = ((current == null) ? string.Empty : current.Name);
						AdminAuditLogRecord auditLogFields = this.AuditLogFields;
						auditLogFields.UserId = auditLogFields.UserId + " (" + currentProcess.ProcessName + ")";
						if (this.AuditLogFields.UserId == string.Empty)
						{
							this.AuditLogFields.UserId = "UnknownUser";
						}
						if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Task name {1}. UserId is null. Current user name (process name) : {2}.", this, base.TaskName, this.AuditLogFields.UserId);
						}
					}
				}
			}
			this.AuditLogFields.Succeeded = succeeded;
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, bool>((long)this.GetHashCode(), "{0} Task name {1}. Succeeded {2}", this, base.TaskName, this.AuditLogFields.Succeeded);
			}
			this.AuditLogFields.ExternalAccess = AdminAuditExternalAccessDeterminer.IsExternalAccess(base.UserScope.UserId, base.UserScope.ExecutingUserOrganizationId, this.realCurrentOrgId);
			this.AuditLogFields.Error = Strings.NoError;
			this.AuditLogFields.RunDate = DateTime.UtcNow;
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, DateTime>((long)this.GetHashCode(), "{0} Task name {1}. RunDate {2}", this, base.TaskName, this.AuditLogFields.RunDate);
			}
			this.WriteAuditRecord(stopwatch);
			base.LogMessage(Strings.ExitedOnComplete);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003830 File Offset: 0x00001A30
		private void WriteAuditRecord(Stopwatch stopwatch)
		{
			using (AdminAuditOpticsLogData adminAuditOpticsLogData = new AdminAuditOpticsLogData())
			{
				adminAuditOpticsLogData.Tenant = (string.IsNullOrEmpty(this.realCurrentOrgId.ToString()) ? "First Org" : this.realCurrentOrgId.ToString());
				adminAuditOpticsLogData.CmdletName = this.AuditLogFields.Cmdlet;
				adminAuditOpticsLogData.ExternalAccess = this.AuditLogFields.ExternalAccess;
				adminAuditOpticsLogData.OperationSucceeded = this.AuditLogFields.Succeeded;
				adminAuditOpticsLogData.ExecutingUserOrganizationId = base.UserScope.ExecutingUserOrganizationId;
				adminAuditOpticsLogData.Asynchronous = false;
				adminAuditOpticsLogData.RecordId = CombGuidGenerator.NewGuid();
				string text = null;
				Exception actualException = null;
				LocalizedException ex = null;
				if (this.configWrapper != null)
				{
					this.AuditLogFields.Verbose = (this.configWrapper.AdminAuditLogConfig != null && this.configWrapper.AdminAuditLogConfig.LogLevel == AuditLogLevel.Verbose);
					if (this.configWrapper.ArbitrationMailboxStatus == ArbitrationMailboxStatus.UnableToKnow)
					{
						Exception error = this.configWrapper.Error;
						text = Strings.ErrorsDuringAdminLogProvisioningHandlerValidate(error.ToString());
						ex = new ErrorsDuringAdminLogProvisioningHandlerValidateException(error.Message, error);
						this.configWrapper.ResetLogMailboxStatus();
						adminAuditOpticsLogData.Disable();
						DiagnosticContext.TraceLocation((LID)62140U);
					}
					else
					{
						try
						{
							ExTraceGlobals.FaultInjectionTracer.TraceTest(3292933439U);
							IAuditLog mailboxLogger = this.configWrapper.MailboxLogger;
							adminAuditOpticsLogData.Asynchronous = mailboxLogger.IsAsynchronous;
							adminAuditOpticsLogData.RecordSize = mailboxLogger.WriteAuditRecord(this.AuditLogFields);
							if (mailboxLogger.IsAsynchronous)
							{
								adminAuditOpticsLogData.Disable();
							}
							if (this.AuditLogFields.ExternalAccess)
							{
								DCAdminActionsLogger.LogDCAdminAction(Guid.NewGuid(), this.BuildCustomData());
							}
						}
						catch (TenantAccessBlockedException)
						{
							DiagnosticContext.TraceLocation((LID)37564U);
							this.BlockOrganization(this.realCurrentOrgId);
							adminAuditOpticsLogData.Disable();
							return;
						}
						catch (AuditLogServiceException ex2)
						{
							DiagnosticContext.TraceLocation((LID)32972U);
							ex = ex2;
							text = ex2.ToString();
							actualException = ex2.GetBaseException();
							AdminAuditLogHealthHandler.GetInstance().Health.AddException(ex2);
							ResponseCodeType responseCodeType;
							Enum.TryParse<ResponseCodeType>(ex2.Code, true, out responseCodeType);
							ResponseCodeType responseCodeType2 = responseCodeType;
							if (responseCodeType2 == ResponseCodeType.ErrorImpersonationFailed || responseCodeType2 == ResponseCodeType.ErrorNonExistentMailbox)
							{
								this.BlockOrganization(this.realCurrentOrgId);
								adminAuditOpticsLogData.Disable();
								return;
							}
						}
						catch (LocalizedException ex3)
						{
							DiagnosticContext.TraceLocation((LID)53948U);
							ex = ex3;
							text = ex3.ToString();
							actualException = ex3.GetBaseException();
							AdminAuditLogHealthHandler.GetInstance().Health.AddException(ex3);
						}
					}
					if (text != null)
					{
						adminAuditOpticsLogData.AuditSucceeded = false;
						adminAuditOpticsLogData.LoggingError = ex;
						if (!(ex is QuotaExceededException))
						{
							ExTraceGlobals.FaultInjectionTracer.TraceTest<string>(3292933438U, ref text);
							this.LogExceptionWithTrace(AuditLogParseSerialize.GetAsString(this.AuditLogFields), text, actualException);
						}
					}
					else
					{
						if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Successfully saved log in admin log mailbox.", this);
						}
						stopwatch.Stop();
						AdminLogProvisioningHandler.PerfCounters.TotalAuditEntrySize.IncrementBy((long)adminAuditOpticsLogData.RecordSize);
						AdminLogProvisioningHandler.PerfCounters.AverageAuditMessageSizeBase.IncrementBy(100L);
						AdminLogProvisioningHandler.PerfCounters.TotalAuditSaved.Increment();
						AdminLogProvisioningHandler.PerfCounters.TotalAuditSaveTime.IncrementBy(stopwatch.ElapsedMilliseconds);
						adminAuditOpticsLogData.AuditSucceeded = true;
						adminAuditOpticsLogData.LoggingError = null;
						adminAuditOpticsLogData.LoggingTime = stopwatch.ElapsedMilliseconds;
					}
				}
				else
				{
					DiagnosticContext.TraceLocation((LID)41660U);
					adminAuditOpticsLogData.AuditSucceeded = false;
					adminAuditOpticsLogData.LoggingError = new ErrorsDuringAdminLogProvisioningHandlerValidateException("Tenant configuration could not be found");
					adminAuditOpticsLogData.Disable();
					if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler>((long)this.GetHashCode(), "{0} Unable to save log as tenant configuration could not be found.", this);
					}
				}
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003C3C File Offset: 0x00001E3C
		private void BlockOrganization(OrganizationId organizationId)
		{
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, OrganizationId>((long)this.GetHashCode(), "{0} skipping logging for blocked organization: {1}.", this, organizationId);
			}
			AdminLogProvisioningHandler.blockedOrganizations.TryAdd(organizationId, DateTime.UtcNow, new AdminLogProvisioningHandler.OrganizationBlockTime());
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00003C7C File Offset: 0x00001E7C
		private void LogExceptionWithTrace(string auditMessageData, string errorMessage, Exception actualException)
		{
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceError<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Failed to save log in the admin log mailbox. ErrorMessage: '{1}'.", this, errorMessage);
			}
			if (actualException is MapiExceptionMaxObjsExceeded && AdminLogProvisioningHandler.lastEventLogDate.ContainsKey(this.realCurrentOrgId) && DateTime.UtcNow.Subtract(AdminLogProvisioningHandler.lastEventLogDate[this.realCurrentOrgId]).TotalHours < 24.0)
			{
				return;
			}
			if (errorMessage.Length + auditMessageData.Length > AdminLogProvisioningHandler.MaximumAuditEventLogSize)
			{
				if (auditMessageData.Length <= 0 && errorMessage.Length > AdminLogProvisioningHandler.MaximumAuditEventLogSize)
				{
					errorMessage = errorMessage.Substring(0, AdminLogProvisioningHandler.MaximumAuditEventLogSize);
				}
				else if (errorMessage.Length <= 0 && auditMessageData.Length > AdminLogProvisioningHandler.MaximumAuditEventLogSize)
				{
					auditMessageData = auditMessageData.Substring(0, AdminLogProvisioningHandler.MaximumAuditEventLogSize);
				}
				else if (errorMessage.Length > 0 && auditMessageData.Length > 0)
				{
					float num = (float)(errorMessage.Length + auditMessageData.Length);
					float num2 = (float)errorMessage.Length / num;
					float num3 = (float)auditMessageData.Length / num;
					int length = (int)(num2 * (float)AdminLogProvisioningHandler.MaximumAuditEventLogSize);
					int length2 = (int)(num3 * (float)AdminLogProvisioningHandler.MaximumAuditEventLogSize);
					errorMessage = errorMessage.Substring(0, length);
					auditMessageData = auditMessageData.Substring(0, length2);
				}
			}
			ExEventLog.EventTuple eventInfo;
			if (actualException is MapiExceptionMaxObjsExceeded)
			{
				AdminLogProvisioningHandler.lastEventLogDate[this.realCurrentOrgId] = DateTime.UtcNow;
				eventInfo = ManagementEventLogConstants.Tuple_AdminLogFull;
			}
			else
			{
				eventInfo = ManagementEventLogConstants.Tuple_FailedToLog;
			}
			if (AuditFeatureManager.IsAdminAuditEventLogThrottlingEnabled())
			{
				eventInfo = new ExEventLog.EventTuple(eventInfo.EventId, eventInfo.CategoryId, eventInfo.EntryType, eventInfo.Level, ExEventLog.EventPeriod.LogPeriodic);
			}
			string text = (this.realCurrentOrgId.ToString() == string.Empty) ? "First Organization" : this.realCurrentOrgId.ToString();
			ExManagementApplicationLogger.LogEvent(eventInfo, new string[]
			{
				auditMessageData,
				text,
				errorMessage
			});
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003E64 File Offset: 0x00002064
		private string MakeOneLineString(string value)
		{
			StringBuilder stringBuilder = new StringBuilder(value.Length + 5);
			int i = 0;
			while (i < value.Length)
			{
				char c = value[i];
				if (c <= '"')
				{
					if (c != '\0')
					{
						switch (c)
						{
						case '\t':
							stringBuilder.Append("\\t");
							break;
						case '\n':
							stringBuilder.Append("\\n");
							break;
						case '\v':
						case '\f':
							goto IL_106;
						case '\r':
							stringBuilder.Append("\\r");
							break;
						default:
							if (c != '"')
							{
								goto IL_106;
							}
							stringBuilder.Append("\\\"");
							break;
						}
					}
					else
					{
						stringBuilder.Append("\\0");
					}
				}
				else if (c != '\'')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case '\u2028':
						case '\u2029':
							stringBuilder.Append("\\u");
							stringBuilder.Append(((int)value[i]).ToString("X4", CultureInfo.InvariantCulture));
							break;
						default:
							goto IL_106;
						}
					}
					else
					{
						stringBuilder.Append("\\\\");
					}
				}
				else
				{
					stringBuilder.Append("\\'");
				}
				IL_114:
				i++;
				continue;
				IL_106:
				stringBuilder.Append(value[i]);
				goto IL_114;
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003F9B File Offset: 0x0000219B
		public override string ToString()
		{
			return "AdminLogProvisioningHandler: ";
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003FA4 File Offset: 0x000021A4
		private void GetChangedProperties(ConfigurableObject configObject, out PropertyBag updates, out PropertyBag originals)
		{
			updates = null;
			originals = null;
			if (configObject == null)
			{
				return;
			}
			if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name {1}. Entered GetChangedProperties.", this, base.TaskName);
			}
			if (configObject.ObjectSchema == null)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name {1}. configObject.ObjectSchema is null.", this, base.TaskName);
				}
				return;
			}
			if (configObject.ObjectSchema.AllProperties == null)
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string>((long)this.GetHashCode(), "{0} Task name {1}. configObject.ObjectSchema.AllProperties is null.", this, base.TaskName);
				}
				return;
			}
			updates = new PropertyBag();
			originals = new PropertyBag();
			foreach (PropertyDefinition propertyDefinition in configObject.ObjectSchema.AllProperties)
			{
				if (configObject.propertyBag.IsChanged((ProviderPropertyDefinition)propertyDefinition))
				{
					updates.Add(propertyDefinition.Name, configObject.propertyBag[(ProviderPropertyDefinition)propertyDefinition]);
					object value;
					if (configObject.propertyBag.TryGetOriginalValue((ProviderPropertyDefinition)propertyDefinition, out value))
					{
						originals.Add(propertyDefinition.Name, value);
						if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Task name {1}. propDef: {2} was changed.", this, base.TaskName, propertyDefinition.Name);
						}
					}
					else if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						AdminLogProvisioningHandler.Tracer.TraceDebug<AdminLogProvisioningHandler, string, string>((long)this.GetHashCode(), "{0} Task name {1}. propDef: {2} was changed, but the original value was not found.", this, base.TaskName, propertyDefinition.Name);
					}
				}
			}
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00004164 File Offset: 0x00002364
		private string FormatParametersForProtocolLog()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("(");
			bool flag = true;
			foreach (object obj in this.AuditLogFields.Parameters.Keys)
			{
				string text = (string)obj;
				if (!flag)
				{
					stringBuilder.Append("|");
				}
				flag = false;
				stringBuilder.AppendFormat("{0}={1}", text, AdminAuditLogRecord.GetValueString(this.AuditLogFields.Parameters[text], ' '));
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00004220 File Offset: 0x00002420
		private List<KeyValuePair<string, object>> BuildCustomData()
		{
			return new List<KeyValuePair<string, object>>(8)
			{
				new KeyValuePair<string, object>("ObjectModified", this.AuditLogFields.ObjectModified),
				new KeyValuePair<string, object>("CmdletName", this.AuditLogFields.Cmdlet),
				new KeyValuePair<string, object>("CmdletParameters", this.FormatParametersForProtocolLog()),
				new KeyValuePair<string, object>("Caller", this.AuditLogFields.UserId),
				new KeyValuePair<string, object>("Succeeded", this.AuditLogFields.Succeeded.ToString()),
				new KeyValuePair<string, object>("Error", string.IsNullOrEmpty(this.AuditLogFields.Error) ? "" : this.AuditLogFields.Error.ToString()),
				new KeyValuePair<string, object>("RunDate", this.AuditLogFields.RunDate),
				new KeyValuePair<string, object>("OriginatingServer", AdminLogProvisioningHandler.MachineName + " (15.00.1497.010)")
			};
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00004338 File Offset: 0x00002538
		private string GetDiscoverySearchName()
		{
			if (!base.UserSpecifiedParameters.Contains("Identity"))
			{
				if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
				{
					AdminLogProvisioningHandler.Tracer.TraceError<string>((long)this.GetHashCode(), "The {1} Cmdlet is missing the Identity parameter.", base.TaskName);
				}
				return string.Empty;
			}
			DiscoverySearchDataProvider discoverySearchDataProvider = new DiscoverySearchDataProvider(base.UserScope.CurrentOrganizationId);
			SearchFilter filter = new SearchFilter.SearchFilterCollection(0, new List<SearchFilter>
			{
				new SearchFilter.IsEqualTo(EwsStoreObjectSchema.Identity.StorePropertyDefinition, base.UserSpecifiedParameters["Identity"].ToString()),
				new SearchFilter.ContainsSubstring(ItemSchema.ItemClass, "IPM.Configuration.MailboxDiscoverySearch", 1, 1)
			}.ToArray());
			IEnumerable<MailboxDiscoverySearch> enumerable = discoverySearchDataProvider.FindInFolder<MailboxDiscoverySearch>(filter, null);
			string result;
			try
			{
				using (IEnumerator<MailboxDiscoverySearch> enumerator = enumerable.GetEnumerator())
				{
					if (enumerator.MoveNext())
					{
						return enumerator.Current.Name;
					}
				}
				result = base.UserSpecifiedParameters["Identity"].ToString();
			}
			catch (DataSourceOperationException)
			{
				result = base.UserSpecifiedParameters["Identity"].ToString();
			}
			return result;
		}

		// Token: 0x04000023 RID: 35
		private const int BlockedOrganizationIdsCacheSize = 256;

		// Token: 0x04000024 RID: 36
		private const string toString = "AdminLogProvisioningHandler: ";

		// Token: 0x04000025 RID: 37
		private static readonly string ShouldContinueExceptionName = "Microsoft.Exchange.Management.ControlPanel.ShouldContinueException";

		// Token: 0x04000026 RID: 38
		private static readonly string MachineName = Environment.MachineName;

		// Token: 0x04000027 RID: 39
		private static int MaximumAuditEventLogSize = 30000;

		// Token: 0x04000028 RID: 40
		private static AdminAuditPerfCountersInstance perfCounters;

		// Token: 0x04000029 RID: 41
		private static readonly TimeSpan BlockedOrganizationIdsCacheTimeout = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400002A RID: 42
		private bool alwaysLogging;

		// Token: 0x0400002B RID: 43
		private bool parameterMatched;

		// Token: 0x0400002C RID: 44
		private AdminAuditLogRecord logFields;

		// Token: 0x0400002D RID: 45
		private ConfigurableObject realConfigObject;

		// Token: 0x0400002E RID: 46
		private OrganizationId realCurrentOrgId;

		// Token: 0x0400002F RID: 47
		private readonly ConfigurationCache configurationCache;

		// Token: 0x04000030 RID: 48
		private ConfigWrapper configWrapper;

		// Token: 0x04000031 RID: 49
		private static readonly Microsoft.Exchange.Diagnostics.Trace Tracer = ExTraceGlobals.AdminAuditLogTracer;

		// Token: 0x04000032 RID: 50
		private static ConcurrentDictionary<OrganizationId, DateTime> lastEventLogDate = new ConcurrentDictionary<OrganizationId, DateTime>();

		// Token: 0x04000033 RID: 51
		private static readonly CacheWithExpiration<OrganizationId, AdminLogProvisioningHandler.OrganizationBlockTime> blockedOrganizations;

		// Token: 0x04000034 RID: 52
		private static readonly object syncObject = new object();

		// Token: 0x02000009 RID: 9
		internal static class Caller
		{
			// Token: 0x06000040 RID: 64 RVA: 0x00004474 File Offset: 0x00002674
			public static bool IsMsoSyncServiceAccount(string userId)
			{
				if (!AdminAuditLogHelper.RunningOnDataCenter)
				{
					return false;
				}
				if (!AdminLogProvisioningHandler.Caller.syncUserIdInitialized)
				{
					lock (AdminLogProvisioningHandler.Caller.lockObj)
					{
						if (!AdminLogProvisioningHandler.Caller.syncUserIdInitialized)
						{
							try
							{
								ADSessionSettings sessionSettings = ADSessionSettings.FromOrganizationIdWithoutRbacScopes(ADSystemConfigurationSession.GetRootOrgContainerIdForLocalForest(), OrganizationId.ForestWideOrgId, null, false);
								IRecipientSession tenantOrRootOrgRecipientSession = DirectorySessionFactory.Default.GetTenantOrRootOrgRecipientSession(true, ConsistencyMode.PartiallyConsistent, sessionSettings, 1358, "IsMsoSyncServiceAccount", "f:\\15.00.1497\\sources\\dev\\Management\\src\\ProvisioningAgent\\AdminLog\\AdminLogProvisioningHandler.cs");
								ADRecipient[] array = tenantOrRootOrgRecipientSession.Find(null, QueryScope.SubTree, AdminLogProvisioningHandler.Caller.SyncServiceAccountFilter, null, 2);
								if (array == null || array.Length == 0)
								{
									AdminLogProvisioningHandler.Caller.syncUserId = null;
								}
								else
								{
									AdminLogProvisioningHandler.Caller.syncUserId = array[0].Id.ToString();
								}
								AdminLogProvisioningHandler.Caller.syncUserIdInitialized = true;
							}
							catch (ADTransientException arg)
							{
								if (AdminLogProvisioningHandler.Tracer.IsTraceEnabled(TraceType.ErrorTrace))
								{
									AdminLogProvisioningHandler.Tracer.TraceError<ADTransientException>(0L, "ADTransient exception handled in AdminLogProvisioningHandler.IsMsoSyncServiceAccount(). We capture the log. Error:\n {0}", arg);
								}
							}
						}
					}
				}
				return string.Equals(userId, AdminLogProvisioningHandler.Caller.syncUserId, StringComparison.OrdinalIgnoreCase);
			}

			// Token: 0x04000035 RID: 53
			private const string accountName = "MsoSyncServiceAccount";

			// Token: 0x04000036 RID: 54
			private static volatile bool syncUserIdInitialized = false;

			// Token: 0x04000037 RID: 55
			private static object lockObj = new object();

			// Token: 0x04000038 RID: 56
			private static string syncUserId = null;

			// Token: 0x04000039 RID: 57
			private static QueryFilter SyncServiceAccountFilter = new AndFilter(new QueryFilter[]
			{
				new TextFilter(ADObjectSchema.Name, "MsoSyncServiceAccount", MatchOptions.FullString, MatchFlags.IgnoreCase),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientType, RecipientType.User),
				new ComparisonFilter(ComparisonOperator.Equal, ADRecipientSchema.RecipientTypeDetails, RecipientTypeDetails.LinkedUser)
			});
		}

		// Token: 0x0200000A RID: 10
		private class OrganizationBlockTime : ILifetimeTrackable
		{
			// Token: 0x06000042 RID: 66 RVA: 0x000045F0 File Offset: 0x000027F0
			public OrganizationBlockTime()
			{
				this.CreateTime = DateTime.UtcNow;
			}

			// Token: 0x17000010 RID: 16
			// (get) Token: 0x06000043 RID: 67 RVA: 0x00004603 File Offset: 0x00002803
			// (set) Token: 0x06000044 RID: 68 RVA: 0x0000460B File Offset: 0x0000280B
			public DateTime CreateTime { get; private set; }

			// Token: 0x17000011 RID: 17
			// (get) Token: 0x06000045 RID: 69 RVA: 0x00004614 File Offset: 0x00002814
			// (set) Token: 0x06000046 RID: 70 RVA: 0x0000461C File Offset: 0x0000281C
			public DateTime LastAccessTime { get; set; }
		}
	}
}
