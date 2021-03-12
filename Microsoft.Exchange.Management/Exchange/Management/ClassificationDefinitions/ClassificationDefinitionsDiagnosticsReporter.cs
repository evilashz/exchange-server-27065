using System;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.MessagingPolicies;
using Microsoft.Exchange.Management.Common;
using Microsoft.Exchange.Management.EventMessages;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Management.ClassificationDefinitions
{
	// Token: 0x02000824 RID: 2084
	internal class ClassificationDefinitionsDiagnosticsReporter : IClassificationDefinitionsDiagnosticsReporter
	{
		// Token: 0x170015BE RID: 5566
		// (get) Token: 0x0600481C RID: 18460 RVA: 0x001283C9 File Offset: 0x001265C9
		internal static IClassificationDefinitionsDiagnosticsReporter Instance
		{
			get
			{
				return ClassificationDefinitionsDiagnosticsReporter.singletonInstance;
			}
		}

		// Token: 0x170015BF RID: 5567
		// (get) Token: 0x0600481D RID: 18461 RVA: 0x001283D0 File Offset: 0x001265D0
		internal Trace Tracer
		{
			get
			{
				return this.dlpTracer;
			}
		}

		// Token: 0x0600481E RID: 18462 RVA: 0x001283D8 File Offset: 0x001265D8
		private ClassificationDefinitionsDiagnosticsReporter()
		{
			this.dlpTracer = ExTraceGlobals.ClassificationDefinitionsTracer;
		}

		// Token: 0x0600481F RID: 18463 RVA: 0x0012844C File Offset: 0x0012664C
		public void WriteCorruptRulePackageDiagnosticsInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn, Exception underlyingException)
		{
			string organizationId = currentOrganizationId.ToString();
			string exceptionDetails = underlyingException.ToString();
			Task.Factory.StartNew(delegate()
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_CorruptClassificationRuleCollection, new string[]
				{
					organizationId,
					offendingRulePackageObjectDn,
					exceptionDetails
				});
			});
			string eventMessage = string.Format("A corrupted classification rule collection has been identified under organization: '{0}'. Source object DN: {1}. Error details: {2}", organizationId, offendingRulePackageObjectDn, exceptionDetails);
			Task.Factory.StartNew(delegate()
			{
				EventNotificationItem.Publish(ClassificationDefinitionsDiagnosticsReporter.ServiceName, "ClassificationManagement", "CorruptClassificationRuleCollection", eventMessage, ResultSeverityLevel.Warning, false);
			});
			this.dlpTracer.TraceError((long)traceSourceHashCode, eventMessage);
		}

		// Token: 0x06004820 RID: 18464 RVA: 0x00128550 File Offset: 0x00126750
		public void WriteDuplicateRuleIdAcrossRulePacksDiagnosticsInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn1, string offendingRulePackageObjectDn2, string duplicateRuleId)
		{
			string organizationId = currentOrganizationId.ToString();
			Task.Factory.StartNew(delegate()
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_DuplicateDataClassificationIdAcrossRulePack, new string[]
				{
					organizationId,
					duplicateRuleId,
					offendingRulePackageObjectDn1,
					offendingRulePackageObjectDn2
				});
			});
			string eventMessage = string.Format("A duplicate data classification identifier '{1}' has been identified in objects with DN '{2}' and '{3}' under organization '{0}'.", new object[]
			{
				organizationId,
				duplicateRuleId,
				offendingRulePackageObjectDn1,
				offendingRulePackageObjectDn2
			});
			Task.Factory.StartNew(delegate()
			{
				EventNotificationItem.Publish(ClassificationDefinitionsDiagnosticsReporter.ServiceName, "ClassificationManagement", "NonUniqueDataClassificationIdentifierFound", eventMessage, ResultSeverityLevel.Warning, false);
			});
			this.dlpTracer.TraceError((long)traceSourceHashCode, eventMessage);
		}

		// Token: 0x06004821 RID: 18465 RVA: 0x00128654 File Offset: 0x00126854
		public void WriteClassificationEngineConfigurationErrorInformation(int traceSourceHashCode, Exception underlyingException)
		{
			string exceptionDetails = underlyingException.ToString();
			Task.Factory.StartNew(delegate()
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ClassificationEngineConfigurationError, new string[]
				{
					exceptionDetails
				});
			});
			string eventMessage = string.Format("Unable to obtain information from Microsoft Classification Engine configuration for classification rule collection validation purpose. Error details: {0}", exceptionDetails);
			Task.Factory.StartNew(delegate()
			{
				EventNotificationItem.Publish(ClassificationDefinitionsDiagnosticsReporter.ServiceName, "ClassificationManagement", "MceConfigIssue", eventMessage, ResultSeverityLevel.Warning, false);
			});
			this.dlpTracer.TraceError((long)traceSourceHashCode, eventMessage);
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x00128730 File Offset: 0x00126930
		public void WriteClassificationEngineUnexpectedFailureInValidation(int traceSourceHashCode, OrganizationId currentOrganizationId, int engineErrorCode)
		{
			string organizationId = (!object.ReferenceEquals(currentOrganizationId, null)) ? currentOrganizationId.ToString() : string.Empty;
			string hexEngineErrorCode = engineErrorCode.ToString("X8");
			Task.Factory.StartNew(delegate()
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ClassificationEngineFailure, new string[]
				{
					organizationId,
					string.Format("0x{0}", hexEngineErrorCode)
				});
			});
			string eventMessage = string.Format("MCE returned an error when trying to validate a classification rule collection for organization '{0}'. Error code returned was 0x{1}.", organizationId, hexEngineErrorCode);
			Task.Factory.StartNew(delegate()
			{
				EventNotificationItem.Publish(ClassificationDefinitionsDiagnosticsReporter.ServiceName, "ClassificationManagement", "MceFailureIssue", eventMessage, ResultSeverityLevel.Error, false);
			});
			this.dlpTracer.TraceError((long)traceSourceHashCode, eventMessage);
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00128828 File Offset: 0x00126A28
		public void WriteClassificationEngineTimeoutInValidation(int traceSourceHashCode, OrganizationId currentOrganizationId, int validationTimeout)
		{
			string organizationId = (!object.ReferenceEquals(currentOrganizationId, null)) ? currentOrganizationId.ToString() : string.Empty;
			string timeout = validationTimeout.ToString(CultureInfo.InvariantCulture);
			Task.Factory.StartNew(delegate()
			{
				ExManagementApplicationLogger.LogEvent(ManagementEventLogConstants.Tuple_ClassificationEngineTimeout, new string[]
				{
					organizationId,
					timeout
				});
			});
			string eventMessage = string.Format("MCE timed-out when trying to validate a classification rule collection for organization '{0}'. The operation timeout was {1} ms.", organizationId, timeout);
			Task.Factory.StartNew(delegate()
			{
				EventNotificationItem.Publish(ClassificationDefinitionsDiagnosticsReporter.ServiceName, "ClassificationManagement", "MceTimeoutIssue", eventMessage, ResultSeverityLevel.Error, false);
			});
			this.dlpTracer.TraceError((long)traceSourceHashCode, eventMessage);
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x001288C8 File Offset: 0x00126AC8
		public void WriteInvalidObjectInformation(int traceSourceHashCode, OrganizationId currentOrganizationId, string offendingRulePackageObjectDn)
		{
			string arg = currentOrganizationId.ToString();
			this.dlpTracer.TraceWarning<string, string>((long)traceSourceHashCode, "A classification definition management operation is attempted against object outside ClassificationDefinitions container. Organization: '{0}'. Incorrect object DN: '{1}'", arg, offendingRulePackageObjectDn);
		}

		// Token: 0x04002BE5 RID: 11237
		private const string ComponentName = "ClassificationManagement";

		// Token: 0x04002BE6 RID: 11238
		private static readonly ClassificationDefinitionsDiagnosticsReporter singletonInstance = new ClassificationDefinitionsDiagnosticsReporter();

		// Token: 0x04002BE7 RID: 11239
		private static readonly string ServiceName = ExchangeComponent.Classification.Name;

		// Token: 0x04002BE8 RID: 11240
		private readonly Trace dlpTracer;
	}
}
