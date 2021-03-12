using System;
using Microsoft.Exchange.Data.Transport;
using Microsoft.Exchange.Data.Transport.Routing;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Transport.Extensibility
{
	// Token: 0x02000302 RID: 770
	internal class AgentErrorHandlingDeferAction : IErrorHandlingAction
	{
		// Token: 0x060021AD RID: 8621 RVA: 0x0007FB64 File Offset: 0x0007DD64
		public AgentErrorHandlingDeferAction(TimeSpan waitTime, bool isProgressive)
		{
			this.WaitTime = waitTime;
			this.IsWaitTimeProgressive = isProgressive;
		}

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060021AE RID: 8622 RVA: 0x0007FB7A File Offset: 0x0007DD7A
		public ErrorHandlingActionType ActionType
		{
			get
			{
				return ErrorHandlingActionType.Defer;
			}
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060021AF RID: 8623 RVA: 0x0007FB7D File Offset: 0x0007DD7D
		// (set) Token: 0x060021B0 RID: 8624 RVA: 0x0007FB85 File Offset: 0x0007DD85
		public TimeSpan WaitTime { get; private set; }

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x060021B1 RID: 8625 RVA: 0x0007FB8E File Offset: 0x0007DD8E
		// (set) Token: 0x060021B2 RID: 8626 RVA: 0x0007FB96 File Offset: 0x0007DD96
		public bool IsWaitTimeProgressive { get; private set; }

		// Token: 0x17000AC1 RID: 2753
		// (get) Token: 0x060021B3 RID: 8627 RVA: 0x0007FB9F File Offset: 0x0007DD9F
		// (set) Token: 0x060021B4 RID: 8628 RVA: 0x0007FBA7 File Offset: 0x0007DDA7
		public SmtpResponse? SmtpResponse { get; set; }

		// Token: 0x060021B5 RID: 8629 RVA: 0x0007FBB0 File Offset: 0x0007DDB0
		public void TakeAction(QueuedMessageEventSource source, MailItem mailItem)
		{
			TransportMailItemWrapper transportMailItemWrapper = mailItem as TransportMailItemWrapper;
			if (transportMailItemWrapper == null)
			{
				throw new ArgumentException("mailItem");
			}
			TransportMailItem transportMailItem = transportMailItemWrapper.TransportMailItem;
			int num = transportMailItem.ExtendedProperties.GetValue<int>("Microsoft.Exchange.Transport.AgentErrorDeferCount", 0) + 1;
			transportMailItem.ExtendedProperties.SetValue<int>("Microsoft.Exchange.Transport.AgentErrorDeferCount", num);
			SmtpResponse value;
			if (this.SmtpResponse != null)
			{
				value = this.SmtpResponse.Value;
			}
			else
			{
				value = new SmtpResponse("421", "4.7.11", "Message deferred by Agent error handling action", true, new string[]
				{
					string.Empty
				});
			}
			source.Defer(AgentErrorHandlingDeferAction.GetDeferInterval(this.WaitTime, num, this.IsWaitTimeProgressive), value);
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0007FC64 File Offset: 0x0007DE64
		internal static TimeSpan GetDeferInterval(TimeSpan waitTime, int deferCount, bool isWaitTimeProgressive)
		{
			if (isWaitTimeProgressive)
			{
				int num = (int)waitTime.TotalSeconds * deferCount;
				num += Convert.ToInt32(AgentErrorHandlingDeferAction.RandomGenerator.NextDouble() * 180.0);
				return TimeSpan.FromSeconds(Math.Min((double)num, AgentErrorHandlingDeferAction.MaxDeferralInterval.TotalSeconds));
			}
			return waitTime;
		}

		// Token: 0x040011AB RID: 4523
		private const int DeferralRandomizationWindowInSeconds = 180;

		// Token: 0x040011AC RID: 4524
		private static readonly TimeSpan MaxDeferralInterval = TimeSpan.FromDays(1.0);

		// Token: 0x040011AD RID: 4525
		private static readonly Random RandomGenerator = new Random((int)DateTime.UtcNow.Ticks);
	}
}
