using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Office.CompliancePolicy.PolicyConfiguration;
using Microsoft.Office.CompliancePolicy.Tasks;

namespace Microsoft.Office.CompliancePolicy.Validators
{
	// Token: 0x02000139 RID: 313
	internal abstract class SourceValidator
	{
		// Token: 0x06000DC5 RID: 3525 RVA: 0x00031F48 File Offset: 0x00030148
		public SourceValidator(Task.TaskErrorLoggingDelegate writeErrorDelegate, Action<LocalizedString> writeWarningDelegate, Func<LocalizedString, bool> shouldContinueDelegate, ExecutionLog logger, string logTag, string tenantId, SourceValidator.Clients client)
		{
			if (client == SourceValidator.Clients.NewCompliancePolicy || client == SourceValidator.Clients.SetCompliancePolicy)
			{
				ArgumentValidator.ThrowIfNull("writeErrorDelegate", writeErrorDelegate);
				ArgumentValidator.ThrowIfNull("writeWarningDelegate", writeWarningDelegate);
				ArgumentValidator.ThrowIfNull("shouldContinueDelegate", shouldContinueDelegate);
			}
			this.writeErrorDelegate = writeErrorDelegate;
			this.writeWarningDelegate = writeWarningDelegate;
			this.shouldContinueDelegate = shouldContinueDelegate;
			this.logger = logger;
			this.logTag = logTag;
			this.tenantId = tenantId;
			this.client = client;
			this.logCorrelationId = Guid.NewGuid().ToString();
		}

		// Token: 0x1700049C RID: 1180
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x00031FD3 File Offset: 0x000301D3
		protected SourceValidator.Clients Client
		{
			get
			{
				return this.client;
			}
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00031FDB File Offset: 0x000301DB
		protected void WriteError(Exception exception, ErrorCategory errorCategory)
		{
			if (this.writeErrorDelegate != null)
			{
				this.writeErrorDelegate(exception, errorCategory, null);
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00031FF3 File Offset: 0x000301F3
		protected void WriteWarning(LocalizedString message)
		{
			if (this.writeWarningDelegate != null)
			{
				this.writeWarningDelegate(message);
			}
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x00032009 File Offset: 0x00030209
		protected bool ShouldContinue(LocalizedString message)
		{
			return this.shouldContinueDelegate != null && this.shouldContinueDelegate(message);
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00032021 File Offset: 0x00030221
		protected void LogOneEntry(ExecutionLog.EventType eventType, string messageFormat, params object[] args)
		{
			this.LogOneEntry(eventType, null, messageFormat, args);
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00032030 File Offset: 0x00030230
		protected void LogOneEntry(ExecutionLog.EventType eventType, Exception exception, string messageFormat, params object[] args)
		{
			if (this.logger != null)
			{
				string contextData = string.Format(messageFormat, args);
				this.logger.LogOneEntry(this.client.ToString(), this.tenantId, this.logCorrelationId, eventType, this.logTag, contextData, exception, null);
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00032080 File Offset: 0x00030280
		protected static int GetMaxLimitFromConfig(string maxLimitKey, int defaultLimit, int existingItemCount)
		{
			int intFromConfig = Utils.GetIntFromConfig(maxLimitKey, defaultLimit);
			int num = intFromConfig - existingItemCount;
			if (num < 0)
			{
				num = 0;
			}
			return num;
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000320A0 File Offset: 0x000302A0
		internal static bool IsWideScope(string bindingParameter)
		{
			return string.Compare(bindingParameter, "All", StringComparison.InvariantCultureIgnoreCase) == 0;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000320B1 File Offset: 0x000302B1
		internal static PolicyBindingTypes GetBindingType(string bindingValue)
		{
			if (string.Compare("All", bindingValue, StringComparison.InvariantCultureIgnoreCase) == 0)
			{
				return PolicyBindingTypes.Tenant;
			}
			return PolicyBindingTypes.IndividualResource;
		}

		// Token: 0x04000492 RID: 1170
		private readonly ExecutionLog logger;

		// Token: 0x04000493 RID: 1171
		private readonly string logTag;

		// Token: 0x04000494 RID: 1172
		private readonly string tenantId;

		// Token: 0x04000495 RID: 1173
		private readonly SourceValidator.Clients client;

		// Token: 0x04000496 RID: 1174
		private readonly string logCorrelationId;

		// Token: 0x04000497 RID: 1175
		private readonly Task.TaskErrorLoggingDelegate writeErrorDelegate;

		// Token: 0x04000498 RID: 1176
		private readonly Action<LocalizedString> writeWarningDelegate;

		// Token: 0x04000499 RID: 1177
		private readonly Func<LocalizedString, bool> shouldContinueDelegate;

		// Token: 0x0200013A RID: 314
		internal enum Clients
		{
			// Token: 0x0400049B RID: 1179
			SetCompliancePolicy,
			// Token: 0x0400049C RID: 1180
			NewCompliancePolicy,
			// Token: 0x0400049D RID: 1181
			UccPolicyUI
		}
	}
}
