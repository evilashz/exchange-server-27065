using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000434 RID: 1076
	internal sealed class WorkDurationDropDownList : DropDownList
	{
		// Token: 0x060026E8 RID: 9960 RVA: 0x000DE4B4 File Offset: 0x000DC6B4
		public WorkDurationDropDownList(string id, DurationUnit duration)
		{
			int num = (int)duration;
			base..ctor(id, num.ToString(), null);
			this.duration = duration;
		}

		// Token: 0x060026E9 RID: 9961 RVA: 0x000DE4D9 File Offset: 0x000DC6D9
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write(LocalizedStrings.GetHtmlEncoded(TaskUtilities.GetWorkDurationUnitString(this.duration)));
		}

		// Token: 0x060026EA RID: 9962 RVA: 0x000DE4F4 File Offset: 0x000DC6F4
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[WorkDurationDropDownList.durationUnitTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem((int)WorkDurationDropDownList.durationUnitTypes[i], TaskUtilities.GetWorkDurationUnitString(WorkDurationDropDownList.durationUnitTypes[i]));
			}
			return array;
		}

		// Token: 0x04001B43 RID: 6979
		private static readonly DurationUnit[] durationUnitTypes = new DurationUnit[]
		{
			DurationUnit.Minutes,
			DurationUnit.Hours,
			DurationUnit.Days,
			DurationUnit.Weeks
		};

		// Token: 0x04001B44 RID: 6980
		private DurationUnit duration;
	}
}
