using System;
using System.Globalization;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x0200010E RID: 270
	internal class DatePrompt : VariablePrompt<ExDateTime>
	{
		// Token: 0x0600079C RID: 1948 RVA: 0x0001EEE8 File Offset: 0x0001D0E8
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"date",
				base.Config.PromptName,
				string.Empty,
				this.date.ToString("M", base.Culture)
			});
		}

		// Token: 0x0600079D RID: 1949 RVA: 0x0001EF44 File Offset: 0x0001D144
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "Date prompt returning ssml string: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x0600079E RID: 1950 RVA: 0x0001EF78 File Offset: 0x0001D178
		protected override void InternalInitialize()
		{
			this.date = base.InitVal;
			this.ssmlString = SpokenDateTimeFormatter.ToSsml(base.Culture, this.date, SpokenDateTimeFormatter.GetSpokenDateFormat(base.Culture), this.date.ToString("M", base.Culture));
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "DatePrompt successfully intialized date '{0}' to ssml '{1}.", new object[]
			{
				this.date,
				this.ssmlString
			});
		}

		// Token: 0x0400083F RID: 2111
		internal const string RecordedMonthFileTemplate = "Month-{0}.wav";

		// Token: 0x04000840 RID: 2112
		internal const string RecordedDayFileTemplate = "Day-{0}.wav";

		// Token: 0x04000841 RID: 2113
		private ExDateTime date;

		// Token: 0x04000842 RID: 2114
		private string ssmlString;
	}
}
