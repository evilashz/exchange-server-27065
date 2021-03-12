using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Search.Core.Common;
using Microsoft.Exchange.Search.Core.Diagnostics;
using Microsoft.Exchange.SharePointSignalStore;

namespace Microsoft.Exchange.MailboxAssistants.Assistants.SharePointSignalStore
{
	// Token: 0x0200022C RID: 556
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class UserSessionLogger : ILogger
	{
		// Token: 0x060014FB RID: 5371 RVA: 0x00078354 File Offset: 0x00076554
		public UserSessionLogger(string userIdentity, IDiagnosticsSession diagnosticsSession)
		{
			Util.ThrowOnNullArgument(userIdentity, "userIdentity");
			Util.ThrowOnNullArgument(diagnosticsSession, "diagnosticsSession");
			this.userMsgPrefix = string.Format(CultureInfo.InvariantCulture, "U={0} - ", new object[]
			{
				userIdentity
			});
			this.diagnosticsSession = diagnosticsSession;
		}

		// Token: 0x060014FC RID: 5372 RVA: 0x000783A8 File Offset: 0x000765A8
		public void LogWarning(string format, params object[] args)
		{
			string operation = string.Format(CultureInfo.InvariantCulture, this.userMsgPrefix + format, args);
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Warnings, operation, new object[0]);
		}

		// Token: 0x060014FD RID: 5373 RVA: 0x000783E0 File Offset: 0x000765E0
		public void LogInfo(string format, params object[] args)
		{
			string operation = string.Format(CultureInfo.InvariantCulture, this.userMsgPrefix + format, args);
			this.diagnosticsSession.LogDiagnosticsInfo(DiagnosticsLoggingTag.Informational, operation, new object[0]);
		}

		// Token: 0x04000CA1 RID: 3233
		private readonly string userMsgPrefix;

		// Token: 0x04000CA2 RID: 3234
		private IDiagnosticsSession diagnosticsSession;
	}
}
