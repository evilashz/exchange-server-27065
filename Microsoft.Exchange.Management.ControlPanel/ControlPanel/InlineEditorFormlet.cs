using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.ControlPanel.WebControls;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020005F7 RID: 1527
	public class InlineEditorFormlet : FormletControlBase<StringArrayParameter, StringArrayModalEditor>
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x000CF00F File Offset: 0x000CD20F
		public InlineEditorFormlet()
		{
			if (string.IsNullOrEmpty(base.FormletDialogTitle))
			{
				base.FormletDialogTitle = Strings.StringArrayDialogTitle;
			}
		}

		// Token: 0x06004483 RID: 17539 RVA: 0x000CF034 File Offset: 0x000CD234
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			base.Parameter = new StringArrayParameter("PopupInlineEditorParameter", new LocalizedString(base.FormletDialogTitle), LocalizedString.Empty, this.MaxLength, new LocalizedString(base.NoSelectionText), this.InputWaterMarkText, "return " + this.ValidationExpression, this.ValidationErrorMessage, this.DuplicateHandlingType);
			if (this.RequiredField != null)
			{
				base.Parameter.RequiredField = this.RequiredField.Value;
			}
		}

		// Token: 0x1700267A RID: 9850
		// (get) Token: 0x06004484 RID: 17540 RVA: 0x000CF0C3 File Offset: 0x000CD2C3
		// (set) Token: 0x06004485 RID: 17541 RVA: 0x000CF0CB File Offset: 0x000CD2CB
		public bool? RequiredField { get; set; }

		// Token: 0x1700267B RID: 9851
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x000CF0D4 File Offset: 0x000CD2D4
		// (set) Token: 0x06004487 RID: 17543 RVA: 0x000CF0DC File Offset: 0x000CD2DC
		public int MaxLength { get; set; }

		// Token: 0x1700267C RID: 9852
		// (get) Token: 0x06004488 RID: 17544 RVA: 0x000CF0E5 File Offset: 0x000CD2E5
		// (set) Token: 0x06004489 RID: 17545 RVA: 0x000CF0ED File Offset: 0x000CD2ED
		public string InputWaterMarkText { get; set; }

		// Token: 0x1700267D RID: 9853
		// (get) Token: 0x0600448A RID: 17546 RVA: 0x000CF0F6 File Offset: 0x000CD2F6
		// (set) Token: 0x0600448B RID: 17547 RVA: 0x000CF0FE File Offset: 0x000CD2FE
		public string ValidationExpression { get; set; }

		// Token: 0x1700267E RID: 9854
		// (get) Token: 0x0600448C RID: 17548 RVA: 0x000CF107 File Offset: 0x000CD307
		// (set) Token: 0x0600448D RID: 17549 RVA: 0x000CF10F File Offset: 0x000CD30F
		public string ValidationErrorMessage { get; set; }

		// Token: 0x1700267F RID: 9855
		// (get) Token: 0x0600448E RID: 17550 RVA: 0x000CF118 File Offset: 0x000CD318
		// (set) Token: 0x0600448F RID: 17551 RVA: 0x000CF120 File Offset: 0x000CD320
		public DuplicateHandlingType DuplicateHandlingType { get; set; }
	}
}
