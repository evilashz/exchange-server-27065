using System;
using Microsoft.Exchange.Cluster.Replay;
using Microsoft.Exchange.Cluster.ReplayEventLog;
using Microsoft.Exchange.Cluster.Shared;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x020000A8 RID: 168
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class AmBcsSingleCopyFailureLogger : IAmBcsErrorLogger
	{
		// Token: 0x1700017A RID: 378
		// (get) Token: 0x060006D9 RID: 1753 RVA: 0x00020D7B File Offset: 0x0001EF7B
		// (set) Token: 0x060006DA RID: 1754 RVA: 0x00020D83 File Offset: 0x0001EF83
		private string ErrorMessage { get; set; }

		// Token: 0x060006DB RID: 1755 RVA: 0x00020D8C File Offset: 0x0001EF8C
		public bool IsFailed()
		{
			return this.IsFailedForServer(null);
		}

		// Token: 0x060006DC RID: 1756 RVA: 0x00020D95 File Offset: 0x0001EF95
		public bool IsFailedForServer(AmServerName server)
		{
			return !string.IsNullOrEmpty(this.ErrorMessage);
		}

		// Token: 0x060006DD RID: 1757 RVA: 0x00020DA5 File Offset: 0x0001EFA5
		public void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage)
		{
			this.SetErrorIfApplicable(false, errorMessage);
		}

		// Token: 0x060006DE RID: 1758 RVA: 0x00020DB0 File Offset: 0x0001EFB0
		public void ReportCopyStatusFailure(AmServerName server, string statusCheckThatFailed, string checksRun, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs)
		{
			this.SetErrorIfApplicable(false, errorMessage);
		}

		// Token: 0x060006DF RID: 1759 RVA: 0x00020DBB File Offset: 0x0001EFBB
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage)
		{
			this.SetErrorIfApplicable(false, errorMessage);
		}

		// Token: 0x060006E0 RID: 1760 RVA: 0x00020DC5 File Offset: 0x0001EFC5
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, bool overwriteAllowed)
		{
			this.SetErrorIfApplicable(overwriteAllowed, errorMessage);
		}

		// Token: 0x060006E1 RID: 1761 RVA: 0x00020DD0 File Offset: 0x0001EFD0
		public void ReportServerFailure(AmServerName server, string serverCheckThatFailed, string errorMessage, ReplayCrimsonEvent evt, params object[] evtArgs)
		{
			this.SetErrorIfApplicable(false, errorMessage);
		}

		// Token: 0x060006E2 RID: 1762 RVA: 0x00020DDA File Offset: 0x0001EFDA
		public Exception GetLastException()
		{
			if (string.IsNullOrEmpty(this.ErrorMessage))
			{
				return null;
			}
			return new AmBcsSingleCopyValidationException(this.ErrorMessage);
		}

		// Token: 0x060006E3 RID: 1763 RVA: 0x00020DF6 File Offset: 0x0001EFF6
		public string[] GetAllExceptions()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060006E4 RID: 1764 RVA: 0x00020DFD File Offset: 0x0001EFFD
		private void SetErrorIfApplicable(bool overwriteAllowed, string errorMessage)
		{
			if (overwriteAllowed || !this.IsFailed())
			{
				this.ErrorMessage = errorMessage;
			}
		}
	}
}
