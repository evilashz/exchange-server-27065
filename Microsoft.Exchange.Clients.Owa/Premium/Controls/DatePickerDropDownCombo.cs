using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000349 RID: 841
	internal class DatePickerDropDownCombo : DropDownCombo
	{
		// Token: 0x06001FC4 RID: 8132 RVA: 0x000B7E1C File Offset: 0x000B601C
		private DatePickerDropDownCombo(string id, ExDateTime selectedDate, ExDateTime defaultMonth, DatePicker.Features datePickerFeatures) : base(id)
		{
			this.selectedDate = selectedDate;
			this.defaultMonth = defaultMonth;
			this.datePickerFeatures = (datePickerFeatures | DatePicker.Features.DropDown);
		}

		// Token: 0x06001FC5 RID: 8133 RVA: 0x000B7E5C File Offset: 0x000B605C
		public static void RenderDatePicker(TextWriter writer, string id, ExDateTime selectedDate)
		{
			DatePickerDropDownCombo.RenderDatePicker(writer, id, selectedDate, selectedDate, DatePicker.Features.TodayButton | DatePicker.Features.DropDown);
		}

		// Token: 0x06001FC6 RID: 8134 RVA: 0x000B7E69 File Offset: 0x000B6069
		public static void RenderDatePicker(TextWriter writer, string id, ExDateTime selectedDate, ExDateTime defaultMonth, DatePicker.Features datePickerFeatures)
		{
			DatePickerDropDownCombo.RenderDatePicker(writer, id, selectedDate, defaultMonth, datePickerFeatures, true);
		}

		// Token: 0x06001FC7 RID: 8135 RVA: 0x000B7E77 File Offset: 0x000B6077
		public static void RenderDatePicker(TextWriter writer, string id, ExDateTime selectedDate, DatePicker.Features datePickerFeatures, bool isEnabled)
		{
			DatePickerDropDownCombo.RenderDatePicker(writer, id, selectedDate, DateTimeUtilities.GetLocalTime(), datePickerFeatures, isEnabled);
		}

		// Token: 0x06001FC8 RID: 8136 RVA: 0x000B7E8C File Offset: 0x000B608C
		public static void RenderDatePicker(TextWriter writer, string id, ExDateTime selectedDate, ExDateTime defaultMonth, DatePicker.Features datePickerFeatures, bool isEnabled)
		{
			new DatePickerDropDownCombo(id, selectedDate, defaultMonth, datePickerFeatures)
			{
				Enabled = isEnabled
			}.Render(writer);
		}

		// Token: 0x06001FC9 RID: 8137 RVA: 0x000B7EB4 File Offset: 0x000B60B4
		protected override void RenderExpandoData(TextWriter writer)
		{
			base.RenderExpandoData(writer);
			writer.Write(" L_None=\"");
			writer.Write(SanitizedHtmlString.FromStringId(1414246128));
			writer.Write("\"");
			writer.Write(" sWkdDtFmt=\"");
			writer.Write(Utilities.SanitizeHtmlEncode(this.sessionContext.GetWeekdayDateFormat(false)));
			writer.Write("\"");
		}

		// Token: 0x06001FCA RID: 8138 RVA: 0x000B7F1C File Offset: 0x000B611C
		protected override void RenderSelectedValue(TextWriter writer)
		{
			UserContext userContext = UserContextManager.GetUserContext();
			if (this.selectedDate == ExDateTime.MinValue)
			{
				writer.Write(SanitizedHtmlString.FromStringId(1414246128));
				return;
			}
			writer.Write(this.selectedDate.ToString(userContext.UserOptions.GetWeekdayDateFormat(false)), writer);
		}

		// Token: 0x06001FCB RID: 8139 RVA: 0x000B7F70 File Offset: 0x000B6170
		protected override void RenderDropControl(TextWriter writer)
		{
			DatePicker datePicker;
			if (this.selectedDate == ExDateTime.MinValue)
			{
				datePicker = new DatePicker(base.Id + "DP", this.defaultMonth, (int)this.datePickerFeatures);
			}
			else
			{
				datePicker = new DatePicker(base.Id + "DP", new ExDateTime[]
				{
					this.selectedDate
				}, (int)this.datePickerFeatures);
			}
			writer.Write("<div id=\"divDP\" class=\"pu\" style=\"display:none\">");
			datePicker.Render(writer);
			writer.Write("</div>");
		}

		// Token: 0x04001719 RID: 5913
		private ExDateTime selectedDate = ExDateTime.MinValue;

		// Token: 0x0400171A RID: 5914
		private ExDateTime defaultMonth = ExDateTime.MinValue;

		// Token: 0x0400171B RID: 5915
		private DatePicker.Features datePickerFeatures = DatePicker.Features.TodayButton | DatePicker.Features.DropDown;
	}
}
