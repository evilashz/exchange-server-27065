using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Text;
using Microsoft.Exchange.Diagnostics.Components.UnifiedMessaging;
using Microsoft.Exchange.UM.UMCommon;

namespace Microsoft.Exchange.UM.UMCore
{
	// Token: 0x020001F5 RID: 501
	internal class TimeRangePrompt : VariablePrompt<TimeRange>
	{
		// Token: 0x06000EAD RID: 3757 RVA: 0x00041B54 File Offset: 0x0003FD54
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Type={0}, Name={1}, File={2}, Value={3}", new object[]
			{
				"timeRange",
				base.Config.PromptName,
				string.Empty,
				this.range.ToString(base.Culture)
			});
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x00041BAC File Offset: 0x0003FDAC
		internal override string ToSsml()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TimeRangePrompt returning ssmlstring: {0}.", new object[]
			{
				this.ssmlString
			});
			return this.ssmlString;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x00041BE0 File Offset: 0x0003FDE0
		protected override void InternalInitialize()
		{
			this.range = base.InitVal;
			this.IntializeSSML();
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x00041BF4 File Offset: 0x0003FDF4
		private void IntializeSSML()
		{
			if (!this.TryGetTimeRangeFileSSML(out this.ssmlString))
			{
				this.InitializeDefaultSSML();
			}
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x00041C0C File Offset: 0x0003FE0C
		private void InitializeDefaultSSML()
		{
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TimeRangePrompt initializing default SSML.", new object[0]);
			this.ssmlString = string.Empty;
			PromptConfigBase timeRange = GlobCfg.DefaultPrompts.TimeRange;
			ArrayList arrayList = new ArrayList();
			timeRange.AddPrompts(arrayList, null, base.Culture);
			bool flag = (this.range.EndTime - this.range.StartTime).TotalHours > 12.0;
			bool flag2 = this.range.EndTime.Date != this.range.StartTime.Date;
			foreach (object obj in arrayList)
			{
				Prompt prompt = (Prompt)obj;
				TimePrompt timePrompt = prompt as TimePrompt;
				if (timePrompt != null)
				{
					if (string.Compare(timePrompt.PromptName, "startTime", StringComparison.OrdinalIgnoreCase) == 0)
					{
						timePrompt.Initialize(this.range.StartTime, flag2, flag || flag2);
					}
					if (string.Compare(timePrompt.PromptName, "endTime", StringComparison.OrdinalIgnoreCase) == 0)
					{
						timePrompt.Initialize(this.range.EndTime, flag2, flag || flag2);
					}
				}
				this.ssmlString += prompt.ToSsml();
			}
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x00041D90 File Offset: 0x0003FF90
		private bool TryGetTimeRangeFileSSML(out string ssml)
		{
			ssml = null;
			if ((this.range.EndTime - this.range.StartTime).TotalMinutes > 120.0)
			{
				return false;
			}
			string value = SpokenDateTimeFormatter.NormalizeHour(base.Culture, this.range.StartTime);
			string value2 = SpokenDateTimeFormatter.NormalizeMinutes(this.range.StartTime);
			string value3 = SpokenDateTimeFormatter.NormalizeHour(base.Culture, this.range.EndTime);
			string value4 = SpokenDateTimeFormatter.NormalizeMinutes(this.range.EndTime);
			StringBuilder stringBuilder = new StringBuilder();
			if (CommonUtil.Is24HourTimeFormat(base.Culture.DateTimeFormat.ShortTimePattern))
			{
				stringBuilder.Append("24-");
			}
			stringBuilder.Append(value);
			stringBuilder.Append(value2);
			stringBuilder.Append("-");
			stringBuilder.Append(value3);
			stringBuilder.Append(value4);
			stringBuilder.Append(".wav");
			string text = Path.Combine(Util.WavPathFromCulture(base.Culture), stringBuilder.ToString());
			CallIdTracer.TraceDebug(ExTraceGlobals.StateMachineTracer, this, "TimeRangePrompt looking for file '{0}'.", new object[]
			{
				text
			});
			if (!File.Exists(text))
			{
				return false;
			}
			ssml = string.Format(CultureInfo.InvariantCulture, "<audio src=\"{0}\" />", new object[]
			{
				text
			});
			return true;
		}

		// Token: 0x04000B0A RID: 2826
		private TimeRange range;

		// Token: 0x04000B0B RID: 2827
		private string ssmlString;
	}
}
