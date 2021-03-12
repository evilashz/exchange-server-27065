using System;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Entities.DataModel.Calendaring.Recurrence;
using Microsoft.Exchange.Entities.DataProviders;
using Microsoft.Exchange.Entities.TypeConversion.Converters;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities.Calendaring.TypeConversion.Converters.Recurrence
{
	// Token: 0x02000089 RID: 137
	internal class RecurrenceConverter : IConverter<Recurrence, PatternedRecurrence>, IConverter<PatternedRecurrence, Recurrence>
	{
		// Token: 0x06000348 RID: 840 RVA: 0x0000BF5F File Offset: 0x0000A15F
		public RecurrenceConverter(ExTimeZone timeZone)
		{
			this.timeZone = RecurrenceConverter.GetSafeTimeZoneForRecurrence(timeZone);
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x06000349 RID: 841 RVA: 0x0000BF73 File Offset: 0x0000A173
		private static ExTimeZone GmtTimeZone
		{
			get
			{
				return RecurrenceConverter.GmtTimeZoneLazy.Value;
			}
		}

		// Token: 0x0600034A RID: 842 RVA: 0x0000BF80 File Offset: 0x0000A180
		public Recurrence Convert(PatternedRecurrence value)
		{
			Recurrence result;
			if (value == null)
			{
				result = null;
			}
			else
			{
				RecurrencePattern recurrencePattern = RecurrenceConverter.PatternConverter.Convert(value.Pattern);
				if (recurrencePattern == null)
				{
					throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter("Pattern"));
				}
				RecurrenceRange recurrenceRange = RecurrenceConverter.RangeConverter.Convert(value.Range);
				if (recurrenceRange == null)
				{
					throw new InvalidRequestException(Strings.ErrorMissingRequiredParameter("Range"));
				}
				result = ((this.timeZone == null) ? new Recurrence(recurrencePattern, recurrenceRange) : new Recurrence(recurrencePattern, recurrenceRange, this.timeZone, this.timeZone));
			}
			return result;
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0000C004 File Offset: 0x0000A204
		public PatternedRecurrence Convert(Recurrence value)
		{
			if (value != null)
			{
				return new PatternedRecurrence
				{
					Pattern = RecurrenceConverter.PatternConverter.Convert(value.Pattern),
					Range = RecurrenceConverter.RangeConverter.Convert(value.Range)
				};
			}
			return null;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0000C049 File Offset: 0x0000A249
		private static ExTimeZone GetSafeTimeZoneForRecurrence(ExTimeZone timeZone)
		{
			if (timeZone != ExTimeZone.UtcTimeZone)
			{
				return timeZone;
			}
			return RecurrenceConverter.GmtTimeZone;
		}

		// Token: 0x0600034D RID: 845 RVA: 0x0000C05C File Offset: 0x0000A25C
		private static ExTimeZone LoadGmtTimeZone()
		{
			ExTimeZone result;
			if (!ExTimeZoneEnumerator.Instance.TryGetTimeZoneByName("Greenwich Standard Time", out result))
			{
				ExTimeZoneInformation exTimeZoneInformation = new ExTimeZoneInformation("Greenwich Standard Time", "Greenwich Standard Time");
				ExTimeZoneRuleGroup exTimeZoneRuleGroup = new ExTimeZoneRuleGroup(null);
				ExTimeZoneRule ruleInfo = new ExTimeZoneRule("Standard", "Standard", new TimeSpan(0L), null);
				exTimeZoneRuleGroup.AddRule(ruleInfo);
				exTimeZoneInformation.AddGroup(exTimeZoneRuleGroup);
				result = new ExTimeZone(exTimeZoneInformation);
			}
			return result;
		}

		// Token: 0x040000F0 RID: 240
		private const string GreenwichStandardTime = "Greenwich Standard Time";

		// Token: 0x040000F1 RID: 241
		private const string StandardTimeZoneRule = "Standard";

		// Token: 0x040000F2 RID: 242
		private static readonly Lazy<ExTimeZone> GmtTimeZoneLazy = new Lazy<ExTimeZone>(new Func<ExTimeZone>(RecurrenceConverter.LoadGmtTimeZone), true);

		// Token: 0x040000F3 RID: 243
		private static readonly PatternConverter PatternConverter = new PatternConverter();

		// Token: 0x040000F4 RID: 244
		private static readonly RangeConverter RangeConverter = new RangeConverter();

		// Token: 0x040000F5 RID: 245
		private readonly ExTimeZone timeZone;
	}
}
