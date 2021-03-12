using System;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Entities
{
	// Token: 0x02000014 RID: 20
	public class DateTimeHelper
	{
		// Token: 0x0600008C RID: 140 RVA: 0x00003BD0 File Offset: 0x00001DD0
		public virtual ExTimeZone GetTimeZoneOrDefault(string id, ExTimeZone defaultTimeZone)
		{
			ExTimeZone result;
			if (!this.TryParseTimeZoneId(id, out result))
			{
				return defaultTimeZone;
			}
			return result;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003BEB File Offset: 0x00001DEB
		public virtual bool TryParseTimeZoneId(string id, out ExTimeZone timeZone)
		{
			if (string.IsNullOrEmpty(id))
			{
				timeZone = null;
			}
			else if (id == "tzone://Microsoft/Utc")
			{
				timeZone = ExTimeZone.UtcTimeZone;
			}
			else
			{
				this.EnumeratorTryGetTimeZoneByName(id, out timeZone);
			}
			return timeZone != null;
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003C21 File Offset: 0x00001E21
		public virtual ExDateTime ChangeTimeZone(ExDateTime time, ExTimeZone targetTimeZone, bool applyBias = true)
		{
			if (!applyBias)
			{
				return targetTimeZone.Assign(time);
			}
			return targetTimeZone.ConvertDateTime(time);
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003C38 File Offset: 0x00001E38
		protected virtual bool EnumeratorTryGetTimeZoneByName(string name, out ExTimeZone timeZone)
		{
			ExTimeZoneEnumerator instance = ExTimeZoneEnumerator.Instance;
			return instance.TryGetTimeZoneByName(name, out timeZone);
		}
	}
}
