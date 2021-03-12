using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Principal;
using Microsoft.Exchange.Data.ApplicationLogic.ConnectionSettingsDiscovery;
using Microsoft.Exchange.Data.ApplicationLogic.Diagnostics;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000346 RID: 838
	internal sealed class LoggingAdapter : ILogAdapter
	{
		// Token: 0x060017A2 RID: 6050 RVA: 0x0007E926 File Offset: 0x0007CB26
		public LoggingAdapter(CallContext callContext, Microsoft.Exchange.Diagnostics.Trace log)
		{
			this.callContext = callContext;
			this.log = log;
			this.stopwatch = new Stopwatch();
		}

		// Token: 0x060017A3 RID: 6051 RVA: 0x0007E948 File Offset: 0x0007CB48
		public void Trace(string messageTemplate, params object[] args)
		{
			string text = string.Format(CultureInfo.InvariantCulture, messageTemplate, args);
			this.log.TraceInformation(this.GetHashCode(), 0L, "{0}. Mailbox: {1}. AccessType: {2}. AccessingAs SID: {3}", new object[]
			{
				text,
				this.Mailbox,
				this.callContext.MailboxAccessType,
				this.CallerSid
			});
		}

		// Token: 0x060017A4 RID: 6052 RVA: 0x0007E9AC File Offset: 0x0007CBAC
		public void LogError(string messageTemplate, params object[] args)
		{
			string text = string.Format(CultureInfo.InvariantCulture, messageTemplate, args);
			this.log.TraceError(this.GetHashCode(), 0L, "{0}. Mailbox: {1}. AccessType: {2}. AccessingAs SID: {3}", new object[]
			{
				text,
				this.Mailbox,
				this.callContext.MailboxAccessType,
				this.CallerSid
			});
		}

		// Token: 0x060017A5 RID: 6053 RVA: 0x0007EA10 File Offset: 0x0007CC10
		public void LogException(Exception exception, string additionalMessage, params object[] args)
		{
			string str = string.Format(CultureInfo.InvariantCulture, additionalMessage, args);
			string str2 = string.Format(CultureInfo.InvariantCulture, "Exception [{0}]: {1}. Stack trace: {2}. ", new object[]
			{
				exception.GetType().Name,
				exception.Message,
				exception.StackTrace
			});
			this.log.TraceError(this.GetHashCode(), 0L, "{0}. Mailbox: {1}. AccessType: {2}. AccessingAs SID: {3}", new object[]
			{
				str + str2,
				this.Mailbox,
				this.callContext.MailboxAccessType,
				this.CallerSid
			});
		}

		// Token: 0x060017A6 RID: 6054 RVA: 0x0007EAB1 File Offset: 0x0007CCB1
		public void ExecuteMonitoredOperation(Enum logMetadata, Action operation)
		{
			this.stopwatch.Restart();
			operation();
			this.stopwatch.Stop();
			this.callContext.ProtocolLog.Set(logMetadata, this.stopwatch.ElapsedMilliseconds);
		}

		// Token: 0x060017A7 RID: 6055 RVA: 0x0007EAF1 File Offset: 0x0007CCF1
		public void LogOperationResult(Enum logMetadata, string domain, bool succeeded)
		{
			this.callContext.ProtocolLog.Set(logMetadata, succeeded);
			this.callContext.ProtocolLog.Set(ConnectionSettingsDiscoveryMetadata.Domain, domain);
		}

		// Token: 0x060017A8 RID: 6056 RVA: 0x0007EB23 File Offset: 0x0007CD23
		public void RegisterLogMetaData(string actionName, Type logMetaDataEnumType)
		{
			OwsLogRegistry.Register(actionName, logMetaDataEnumType, new Type[0]);
		}

		// Token: 0x060017A9 RID: 6057 RVA: 0x0007EB32 File Offset: 0x0007CD32
		public void LogOperationException(Enum logMetadata, Exception ex)
		{
			this.callContext.ProtocolLog.Set(logMetadata, ex.ToString());
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x060017AA RID: 6058 RVA: 0x0007EB4C File Offset: 0x0007CD4C
		private string Mailbox
		{
			get
			{
				if (this.callContext.AccessingPrincipal == null)
				{
					return null;
				}
				return this.callContext.AccessingPrincipal.MailboxInfo.PrimarySmtpAddress.ToString();
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060017AB RID: 6059 RVA: 0x0007EB8B File Offset: 0x0007CD8B
		private SecurityIdentifier CallerSid
		{
			get
			{
				if (this.callContext.AccessingPrincipal == null)
				{
					return null;
				}
				return this.callContext.EffectiveCallerSid;
			}
		}

		// Token: 0x04000FDF RID: 4063
		private readonly CallContext callContext;

		// Token: 0x04000FE0 RID: 4064
		private readonly Microsoft.Exchange.Diagnostics.Trace log;

		// Token: 0x04000FE1 RID: 4065
		private readonly Stopwatch stopwatch;
	}
}
