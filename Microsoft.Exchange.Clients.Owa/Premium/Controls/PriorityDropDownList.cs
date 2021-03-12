using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x020003F7 RID: 1015
	internal sealed class PriorityDropDownList : DropDownList
	{
		// Token: 0x0600252B RID: 9515 RVA: 0x000D739C File Offset: 0x000D559C
		public PriorityDropDownList(string id, Importance priority)
		{
			int num = (int)priority;
			base..ctor(id, num.ToString(), null);
			this.priority = priority;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x000D73C1 File Offset: 0x000D55C1
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write(LocalizedStrings.GetHtmlEncoded(TaskUtilities.GetPriorityString(this.priority)));
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x000D73DC File Offset: 0x000D55DC
		protected override DropDownListItem[] CreateListItems()
		{
			DropDownListItem[] array = new DropDownListItem[PriorityDropDownList.importanceTypes.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new DropDownListItem((int)PriorityDropDownList.importanceTypes[i], TaskUtilities.GetPriorityString(PriorityDropDownList.importanceTypes[i]));
			}
			return array;
		}

		// Token: 0x040019AD RID: 6573
		private static readonly Importance[] importanceTypes = new Importance[]
		{
			Importance.Low,
			Importance.Normal,
			Importance.High
		};

		// Token: 0x040019AE RID: 6574
		private Importance priority;
	}
}
