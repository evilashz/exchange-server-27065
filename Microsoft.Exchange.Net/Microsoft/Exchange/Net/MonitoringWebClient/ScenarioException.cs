using System;
using System.Text;

namespace Microsoft.Exchange.Net.MonitoringWebClient
{
	// Token: 0x0200077D RID: 1917
	internal class ScenarioException : Exception
	{
		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x060025F9 RID: 9721 RVA: 0x00050107 File Offset: 0x0004E307
		// (set) Token: 0x060025FA RID: 9722 RVA: 0x0005010F File Offset: 0x0004E30F
		public RequestTarget FailureSource { get; private set; }

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x060025FB RID: 9723 RVA: 0x00050118 File Offset: 0x0004E318
		// (set) Token: 0x060025FC RID: 9724 RVA: 0x00050120 File Offset: 0x0004E320
		public FailureReason FailureReason { get; private set; }

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x060025FD RID: 9725 RVA: 0x00050129 File Offset: 0x0004E329
		// (set) Token: 0x060025FE RID: 9726 RVA: 0x00050131 File Offset: 0x0004E331
		public FailingComponent FailingComponent { get; private set; }

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x060025FF RID: 9727 RVA: 0x0005013A File Offset: 0x0004E33A
		// (set) Token: 0x06002600 RID: 9728 RVA: 0x00050142 File Offset: 0x0004E342
		public virtual string ExceptionHint { get; private set; }

		// Token: 0x06002601 RID: 9729 RVA: 0x0005014B File Offset: 0x0004E34B
		public ScenarioException(string message, Exception innerException, RequestTarget failureSource, FailureReason failureReason, FailingComponent failingComponent, string exceptionHint) : base(message, innerException)
		{
			this.FailureSource = failureSource;
			this.FailureReason = failureReason;
			this.FailingComponent = failingComponent;
			this.ExceptionHint = exceptionHint;
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002602 RID: 9730 RVA: 0x00050174 File Offset: 0x0004E374
		public override string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(MonitoringWebClientStrings.ScenarioExceptionMessageHeader(base.GetType().FullName, base.Message, this.FailureSource.ToString(), this.FailureReason.ToString(), this.FailingComponent.ToString(), this.ExceptionHint));
				if (base.InnerException != null)
				{
					stringBuilder.Append(MonitoringWebClientStrings.ScenarioExceptionInnerException(base.InnerException.Message));
				}
				return stringBuilder.ToString();
			}
		}
	}
}
