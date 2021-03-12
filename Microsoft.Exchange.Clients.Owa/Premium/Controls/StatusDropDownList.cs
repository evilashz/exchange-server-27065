using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x0200041B RID: 1051
	internal sealed class StatusDropDownList : DropDownList
	{
		// Token: 0x060025B9 RID: 9657 RVA: 0x000DA808 File Offset: 0x000D8A08
		public StatusDropDownList(string id, TaskStatus statusMapping)
		{
			int num = (int)statusMapping;
			base..ctor(id, num.ToString(), null);
			this.statusMapping = statusMapping;
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x000DA82D File Offset: 0x000D8A2D
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write(LocalizedStrings.GetHtmlEncoded(TaskUtilities.GetStatusString(this.statusMapping)));
		}

		// Token: 0x060025BB RID: 9659 RVA: 0x000DA848 File Offset: 0x000D8A48
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[StatusDropDownList.taskStatusTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem((int)StatusDropDownList.taskStatusTypes[i], TaskUtilities.GetStatusString(StatusDropDownList.taskStatusTypes[i]));
			}
			return array;
		}

		// Token: 0x04001A0E RID: 6670
		private static readonly TaskStatus[] taskStatusTypes = new TaskStatus[]
		{
			TaskStatus.NotStarted,
			TaskStatus.InProgress,
			TaskStatus.Completed,
			TaskStatus.WaitingOnOthers,
			TaskStatus.Deferred
		};

		// Token: 0x04001A0F RID: 6671
		private TaskStatus statusMapping;
	}
}
