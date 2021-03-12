using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Infoworker.MeetingValidator
{
	// Token: 0x02000017 RID: 23
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class RepairSteps
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00004CFE File Offset: 0x00002EFE
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x00004D06 File Offset: 0x00002F06
		public bool InquiredMeeting { get; private set; }

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060000A5 RID: 165 RVA: 0x00004D0F File Offset: 0x00002F0F
		// (set) Token: 0x060000A6 RID: 166 RVA: 0x00004D17 File Offset: 0x00002F17
		private RumInfo MasterRum { get; set; }

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x00004D20 File Offset: 0x00002F20
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x00004D28 File Offset: 0x00002F28
		private Dictionary<ExDateTime, RumInfo> OccurrenceRums { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00004D34 File Offset: 0x00002F34
		public List<RumInfo> SendableRums
		{
			get
			{
				if (this.MasterRum.IsNullOp)
				{
					return this.OccurrenceRums.Values.ToList<RumInfo>();
				}
				return new List<RumInfo>
				{
					this.MasterRum
				};
			}
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00004D72 File Offset: 0x00002F72
		private RepairSteps()
		{
		}

		// Token: 0x060000AB RID: 171 RVA: 0x00004D7A File Offset: 0x00002F7A
		private void Initialize(RumInfo master, Dictionary<ExDateTime, RumInfo> occurrences)
		{
			this.MasterRum = master;
			this.OccurrenceRums = occurrences;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x00004D8C File Offset: 0x00002F8C
		internal static RepairSteps CreateInstance()
		{
			RepairSteps repairSteps = new RepairSteps();
			repairSteps.Initialize(NullOpRumInfo.CreateInstance(), new Dictionary<ExDateTime, RumInfo>());
			return repairSteps;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00004DB0 File Offset: 0x00002FB0
		internal void AddStep(RumInfo rum)
		{
			if (rum == null)
			{
				throw new ArgumentNullException("rum", "RUM cannot be null.");
			}
			if (!rum.IsNullOp)
			{
				if (rum.IsOccurrenceRum)
				{
					this.AddOccurrenceRum(rum);
					return;
				}
				this.MasterRum = RumInfo.Merge(this.MasterRum, rum);
				this.OccurrenceRums.Clear();
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00004E08 File Offset: 0x00003008
		internal void Merge(RepairSteps stepsToMerge)
		{
			if (stepsToMerge == null)
			{
				throw new ArgumentNullException("stepsToMerge");
			}
			this.MasterRum = RumInfo.Merge(this.MasterRum, stepsToMerge.MasterRum);
			foreach (RumInfo rum in stepsToMerge.OccurrenceRums.Values)
			{
				this.AddOccurrenceRum(rum);
			}
		}

		// Token: 0x060000AF RID: 175 RVA: 0x00004E88 File Offset: 0x00003088
		private void AddOccurrenceRum(RumInfo rum)
		{
			if (this.MasterRum.IsNullOp)
			{
				ExDateTime value = rum.OccurrenceOriginalStartTime.Value;
				if (this.OccurrenceRums.ContainsKey(value))
				{
					this.OccurrenceRums[value] = RumInfo.Merge(this.OccurrenceRums[value], rum);
					return;
				}
				this.OccurrenceRums.Add(rum.OccurrenceOriginalStartTime.Value, rum);
			}
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004EF8 File Offset: 0x000030F8
		internal bool SendRums(CalendarItemBase baseItem, ref Dictionary<GlobalObjectId, List<Attendee>> organizerRumsSent)
		{
			bool flag = false;
			if (this.MasterRum.IsNullOp)
			{
				using (Dictionary<ExDateTime, RumInfo>.ValueCollection.Enumerator enumerator = this.OccurrenceRums.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RumInfo info = enumerator.Current;
						if (RumAgent.Instance.SendRums(info, RumFactory.Instance.Policy.CopyRumsToSentItems, baseItem, ref organizerRumsSent))
						{
							flag = true;
						}
					}
					return flag;
				}
			}
			flag = RumAgent.Instance.SendRums(this.MasterRum, RumFactory.Instance.Policy.CopyRumsToSentItems, baseItem, ref organizerRumsSent);
			if (flag && this.MasterRum.Type == RumType.Inquiry)
			{
				this.InquiredMeeting = true;
			}
			return flag;
		}

		// Token: 0x060000B1 RID: 177 RVA: 0x00004FB4 File Offset: 0x000031B4
		public void ForEachSendableRum(Action<RumInfo> action)
		{
			if (this.MasterRum.IsNullOp)
			{
				using (Dictionary<ExDateTime, RumInfo>.ValueCollection.Enumerator enumerator = this.OccurrenceRums.Values.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RumInfo obj = enumerator.Current;
						action(obj);
					}
					return;
				}
			}
			action(this.MasterRum);
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005028 File Offset: 0x00003228
		public int SendableRumsCount
		{
			get
			{
				if (!this.MasterRum.IsNullOp)
				{
					return 1;
				}
				return this.OccurrenceRums.Count;
			}
		}
	}
}
