using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000409 RID: 1033
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UpdateRumInfo : OrganizerRumInfo
	{
		// Token: 0x06002EEF RID: 12015 RVA: 0x000C11A8 File Offset: 0x000BF3A8
		private UpdateRumInfo() : this(null, null, CalendarInconsistencyFlag.None)
		{
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x000C11C8 File Offset: 0x000BF3C8
		protected UpdateRumInfo(ExDateTime? originalStartTime, IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag) : base(RumType.Update, originalStartTime, attendees)
		{
			EnumValidator<CalendarInconsistencyFlag>.ThrowIfInvalid(inconsistencyFlag, "inconsistencyFlag");
			this.InconsistencyFlagList = new List<CalendarInconsistencyFlag>(1)
			{
				inconsistencyFlag
			};
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x000C1200 File Offset: 0x000BF400
		public static UpdateRumInfo CreateMasterInstance(IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag)
		{
			return new UpdateRumInfo(null, attendees, inconsistencyFlag);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x000C121D File Offset: 0x000BF41D
		public static UpdateRumInfo CreateOccurrenceInstance(ExDateTime originalStartTime, IList<Attendee> attendees, CalendarInconsistencyFlag inconsistencyFlag)
		{
			return new UpdateRumInfo(new ExDateTime?(originalStartTime), attendees, inconsistencyFlag);
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06002EF3 RID: 12019 RVA: 0x000C122C File Offset: 0x000BF42C
		// (set) Token: 0x06002EF4 RID: 12020 RVA: 0x000C1234 File Offset: 0x000BF434
		public IEnumerable<CalendarInconsistencyFlag> InconsistencyFlagList { get; private set; }

		// Token: 0x06002EF5 RID: 12021 RVA: 0x000C1240 File Offset: 0x000BF440
		protected override void Merge(RumInfo infoToMerge)
		{
			if (infoToMerge is UpdateRumInfo)
			{
				base.Merge(infoToMerge);
				UpdateRumInfo updateRumInfo = (UpdateRumInfo)infoToMerge;
				this.InconsistencyFlagList = this.InconsistencyFlagList.Union(updateRumInfo.InconsistencyFlagList);
				return;
			}
			string message = string.Format("An update RUM can be merged only with another update RUM. RumInfo type is {0}", infoToMerge.GetType());
			throw new ArgumentException(message, "infoToMerge");
		}
	}
}
