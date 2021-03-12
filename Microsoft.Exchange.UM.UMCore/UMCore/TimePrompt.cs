using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Exchange.UM.UMCommon.MessageContent;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F3 RID: 499
	internal class TimePrompt : VariablePrompt<ExDateTime>
	{
		// Token: 0x06000EA0 RID: 3744 RVA: 0x000418E8 File Offset: 0x0003FAE8
		public TimePrompt()
		{
			base.InitVal = ExDateTime.MinValue;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000418FB File Offset: 0x0003FAFB
		internal TimePrompt(string promptName, CultureInfo culture, ExDateTime value) : base(promptName, culture, value)
		{
		}

		// Token: 0x17000392 RID: 914
		// (get) Token: 0x06000EA2 RID: 3746 RVA: 0x00041906 File Offset: 0x0003FB06
		// (set) Token: 0x06000EA3 RID: 3747 RVA: 0x0004190E File Offset: 0x0003FB0E
		protected ExDateTime Time
		{
			get
			{
				return this.time;
			}
			set
			{
				this.time = value;
			}
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x00041918 File Offset: 0x0003FB18
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"time",
				base.Config.PromptName,
				string.Empty,
				this.time.ToString("t", base.Culture)
			});
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x00041974 File Offset: 0x0003FB74
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Time prompt returning ssmlstring: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000419A8 File Offset: 0x0003FBA8
		internal void Initialize(ExDateTime dt, bool shouldAddDate, bool shouldAddMeridian)
		{
			this.time = dt;
			if (shouldAddDate && this.time.Date != ExDateTime.Today)
			{
				this.ssmlString = SpokenDateTimeFormatter.ToSsml(base.Culture, this.time, SpokenDateTimeFormatter.GetSpokenDateTimeFormat(base.Culture), Strings.ShortTimeMonthDay(this.time.ToString("t", base.Culture), this.time.ToString("M", base.Culture)).ToString(base.Culture));
			}
			else if (shouldAddMeridian)
			{
				this.ssmlString = SpokenDateTimeFormatter.ToSsml(base.Culture, this.time, SpokenDateTimeFormatter.GetSpokenTimeFormat(base.Culture), this.time.ToString("t", base.Culture));
			}
			else
			{
				this.ssmlString = SpokenDateTimeFormatter.ToSsml(base.Culture, this.time, SpokenDateTimeFormatter.GetSpokenShortTimeFormat(base.Culture), this.time.ToString("t", base.Culture));
			}
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TimePrompt successfully intialized time '{0}' with ssml '{1}'.", new object[]
			{
				this.time,
				this.ssmlString
			});
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x00041ADD File Offset: 0x0003FCDD
		protected override void InternalInitialize()
		{
			this.Initialize(base.InitVal, true, true);
		}

		// Token: 0x04000B01 RID: 2817
		internal const string RecordedHourFileTemplate = "Hours-{0}.wav";

		// Token: 0x04000B02 RID: 2818
		internal const string RecordedMinuteFileTemplate = "Minutes-{0}.wav";

		// Token: 0x04000B03 RID: 2819
		internal const string RecordedMeridianFileTemplate = "Meridian-{0}.wav";

		// Token: 0x04000B04 RID: 2820
		internal const string TwentyFourHourPrefix = "24-";

		// Token: 0x04000B05 RID: 2821
		internal const string Recorded24HourFileTemplate = "24-Hours-{0}.wav";

		// Token: 0x04000B06 RID: 2822
		private ExDateTime time;

		// Token: 0x04000B07 RID: 2823
		private string ssmlString;
	}
}
