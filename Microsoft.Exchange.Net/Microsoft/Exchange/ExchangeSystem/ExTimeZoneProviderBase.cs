using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.ExchangeSystem
{
	// Token: 0x0200006D RID: 109
	public class ExTimeZoneProviderBase
	{
		// Token: 0x060003C3 RID: 963 RVA: 0x0000FE08 File Offset: 0x0000E008
		public ExTimeZoneProviderBase(string id)
		{
			if (id == null)
			{
				throw new ArgumentNullException("id");
			}
			if (id == string.Empty)
			{
				throw new ArgumentException("id");
			}
			this.Id = id;
			this.TimeZoneByIdDictionary = new Dictionary<string, ExTimeZone>(StringComparer.OrdinalIgnoreCase);
			this.TimeZoneRuleByIdDictionary = new Dictionary<string, ExTimeZoneRule>();
		}

		// Token: 0x060003C4 RID: 964 RVA: 0x0000FE63 File Offset: 0x0000E063
		public bool TryGetTimeZoneByName(string name, out ExTimeZone timeZone)
		{
			return this.TimeZoneByIdDictionary.TryGetValue(name, out timeZone);
		}

		// Token: 0x060003C5 RID: 965 RVA: 0x0000FE72 File Offset: 0x0000E072
		public bool TryGetTimeZoneById(string timeZoneId, out ExTimeZone timeZone)
		{
			return this.TimeZoneByIdDictionary.TryGetValue(timeZoneId, out timeZone);
		}

		// Token: 0x060003C6 RID: 966 RVA: 0x0000FE81 File Offset: 0x0000E081
		public bool TryGetTimeZoneRuleById(string timeZoneRuleId, out ExTimeZoneRule timeZoneRule)
		{
			return this.TimeZoneRuleByIdDictionary.TryGetValue(timeZoneRuleId, out timeZoneRule);
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x0000FE90 File Offset: 0x0000E090
		public IEnumerable<ExTimeZone> GetTimeZones()
		{
			return this.TimeZoneByIdDictionary.Values;
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x0000FEA0 File Offset: 0x0000E0A0
		public void AddTimeZone(ExTimeZone timeZone)
		{
			if (timeZone == null)
			{
				throw new ArgumentNullException("timeZone");
			}
			if (this.TimeZoneByIdDictionary.ContainsKey(timeZone.Id))
			{
				throw new InvalidTimeZoneException("Time zone id already exists");
			}
			if (this.TimeZoneByIdDictionary.ContainsKey(timeZone.Id))
			{
				throw new InvalidTimeZoneException("Time zone name already exists");
			}
			foreach (ExTimeZoneRuleGroup exTimeZoneRuleGroup in timeZone.TimeZoneInformation.Groups)
			{
				foreach (ExTimeZoneRule exTimeZoneRule in exTimeZoneRuleGroup.Rules)
				{
					if (this.TimeZoneRuleByIdDictionary.ContainsKey(exTimeZoneRule.Id))
					{
						throw new InvalidTimeZoneException("Time zone rule id already exists: Id={0}" + exTimeZoneRule.Id);
					}
					this.TimeZoneRuleByIdDictionary[exTimeZoneRule.Id] = exTimeZoneRule;
				}
			}
			this.TimeZoneByIdDictionary[timeZone.Id] = timeZone;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0000FFB8 File Offset: 0x0000E1B8
		// (set) Token: 0x060003CA RID: 970 RVA: 0x0000FFC0 File Offset: 0x0000E1C0
		private protected Dictionary<string, ExTimeZone> TimeZoneByIdDictionary { protected get; private set; }

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003CB RID: 971 RVA: 0x0000FFC9 File Offset: 0x0000E1C9
		// (set) Token: 0x060003CC RID: 972 RVA: 0x0000FFD1 File Offset: 0x0000E1D1
		private protected Dictionary<string, ExTimeZoneRule> TimeZoneRuleByIdDictionary { protected get; private set; }

		// Token: 0x040001DD RID: 477
		public readonly string Id;
	}
}
