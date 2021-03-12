using System;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020003E3 RID: 995
	public class NewMailboxSearch : BaseForm
	{
		// Token: 0x06003342 RID: 13122 RVA: 0x0009F17A File Offset: 0x0009D37A
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
		}

		// Token: 0x040024C6 RID: 9414
		protected Label tbxKeywords_label;

		// Token: 0x040024C7 RID: 9415
		protected CheckBox cbxIncludeUnsearchableItems;

		// Token: 0x040024C8 RID: 9416
		protected Literal lblMessageFromTo;

		// Token: 0x040024C9 RID: 9417
		protected Label saeSenders_label;

		// Token: 0x040024CA RID: 9418
		protected Literal lblMessageFromToAnd;

		// Token: 0x040024CB RID: 9419
		protected Label saeRecipients_label;

		// Token: 0x040024CC RID: 9420
		protected Label rbDateList_label;

		// Token: 0x040024CD RID: 9421
		protected RadioButtonList rbDateList;

		// Token: 0x040024CE RID: 9422
		protected HtmlGenericControl divDateList;

		// Token: 0x040024CF RID: 9423
		protected Label dcStartDate_label;

		// Token: 0x040024D0 RID: 9424
		protected Label dcEndDate_label;

		// Token: 0x040024D1 RID: 9425
		protected Label rbSearchList_label;

		// Token: 0x040024D2 RID: 9426
		protected RadioButtonList rbSearchList;

		// Token: 0x040024D3 RID: 9427
		protected Literal lblSearchNameDescription;

		// Token: 0x040024D4 RID: 9428
		protected Label tbxSearchName_label;

		// Token: 0x040024D5 RID: 9429
		protected TextBox tbxSearchName;

		// Token: 0x040024D6 RID: 9430
		protected Label rbSearchTypeList_label;

		// Token: 0x040024D7 RID: 9431
		protected RadioButtonList rbSearchTypeList;

		// Token: 0x040024D8 RID: 9432
		protected CheckBox cbxExcludeDuplicateMessages;

		// Token: 0x040024D9 RID: 9433
		protected CheckBox cbxEnableFullLogging;

		// Token: 0x040024DA RID: 9434
		protected HtmlGenericControl divTargetMailbox;

		// Token: 0x040024DB RID: 9435
		protected Label pickerTargetMailbox_label;
	}
}
