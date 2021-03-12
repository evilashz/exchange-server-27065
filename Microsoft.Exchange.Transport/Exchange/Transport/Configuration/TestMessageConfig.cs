using System;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Mime;
using Microsoft.Exchange.Data.Transport.Email;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;

namespace Microsoft.Exchange.Transport.Configuration
{
	// Token: 0x020002F1 RID: 753
	internal class TestMessageConfig
	{
		// Token: 0x06002155 RID: 8533 RVA: 0x0007E498 File Offset: 0x0007C698
		public TestMessageConfig(EmailMessage message)
		{
			HeaderList headers = message.RootPart.Headers;
			Header header = headers.FindFirst("X-MS-Exchange-Organization-Test-Message-Send-Report-To");
			if (header != null)
			{
				TestMessageConfig.tracer.TraceDebug<string>(0L, "Report to address is {0}", header.Value);
				this.reportToAddress = new SmtpAddress(header.Value ?? string.Empty);
				if (!this.reportToAddress.IsValidAddress)
				{
					this.reportToAddress = SmtpAddress.Empty;
				}
			}
			header = headers.FindFirst("X-MS-Exchange-Organization-Test-Message");
			if (header != null)
			{
				TestMessageConfig.tracer.TraceDebug(0L, "Current message is a test message");
				this.isTestMessage = true;
				if (!string.Equals(header.Value, "Deliver", StringComparison.OrdinalIgnoreCase))
				{
					TestMessageConfig.tracer.TraceDebug(0L, "The test message will not be delivered");
					this.suppressDelivery = true;
				}
			}
			header = headers.FindFirst("X-MS-Exchange-Organization-Test-Message-Log-For");
			if (header != null)
			{
				this.logTypes = this.TryParseEnumValue<LogTypesEnum>(header.Value);
				TestMessageConfig.tracer.TraceDebug<LogTypesEnum>(0L, "Log types requested for test message is {0}", this.logTypes);
			}
			header = headers.FindFirst("X-MS-Exchange-Organization-Test-Message-Options");
			if (header != null)
			{
				this.options = header.Value;
				TestMessageConfig.tracer.TraceDebug<string>(0L, "Options for test message is {0}", this.options);
				this.SetFlagsBasedOnOptions();
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002156 RID: 8534 RVA: 0x0007E5D1 File Offset: 0x0007C7D1
		public bool IsTestMessage
		{
			get
			{
				return this.isTestMessage;
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002157 RID: 8535 RVA: 0x0007E5D9 File Offset: 0x0007C7D9
		public LogTypesEnum LogTypes
		{
			get
			{
				return this.logTypes;
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002158 RID: 8536 RVA: 0x0007E5E1 File Offset: 0x0007C7E1
		public bool SuppressDelivery
		{
			get
			{
				return this.suppressDelivery;
			}
		}

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002159 RID: 8537 RVA: 0x0007E5E9 File Offset: 0x0007C7E9
		public SmtpAddress ReportToAddress
		{
			get
			{
				return this.reportToAddress;
			}
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0007E5F1 File Offset: 0x0007C7F1
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0007E5F9 File Offset: 0x0007C7F9
		public bool ShouldExecuteDisabledAndInErrorRules { get; private set; }

		// Token: 0x0600215C RID: 8540 RVA: 0x0007E604 File Offset: 0x0007C804
		private T TryParseEnumValue<T>(string value)
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException("Type must be enum.");
			}
			if (string.IsNullOrEmpty(value))
			{
				return default(T);
			}
			T result;
			try
			{
				result = (T)((object)Enum.Parse(typeof(T), value));
			}
			catch (ArgumentException)
			{
				result = default(T);
			}
			return result;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0007E678 File Offset: 0x0007C878
		private void SetFlagsBasedOnOptions()
		{
			if (this.isTestMessage && string.Equals("ExecuteDisabledAndInErrorRules", this.options, StringComparison.OrdinalIgnoreCase))
			{
				this.ShouldExecuteDisabledAndInErrorRules = true;
			}
		}

		// Token: 0x04001189 RID: 4489
		private static readonly Trace tracer = ExTraceGlobals.MailboxRuleTracer;

		// Token: 0x0400118A RID: 4490
		private readonly string options;

		// Token: 0x0400118B RID: 4491
		private readonly SmtpAddress reportToAddress;

		// Token: 0x0400118C RID: 4492
		private readonly bool suppressDelivery;

		// Token: 0x0400118D RID: 4493
		private readonly LogTypesEnum logTypes;

		// Token: 0x0400118E RID: 4494
		private readonly bool isTestMessage;
	}
}
