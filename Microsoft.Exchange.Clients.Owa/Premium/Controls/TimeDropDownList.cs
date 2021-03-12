using System;
using System.IO;
using Microsoft.Exchange.Clients.Owa.Core;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Clients.Owa.Premium.Controls
{
	// Token: 0x02000425 RID: 1061
	internal sealed class TimeDropDownList : DropDownList
	{
		// Token: 0x060025E6 RID: 9702 RVA: 0x000DB6DF File Offset: 0x000D98DF
		private TimeDropDownList(ExDateTime selectedTime, string id, string valueId, ExDateTime endTime) : base(id, false, null, null)
		{
			this.selectedTime = selectedTime;
			this.valueId = valueId;
			this.endTime = endTime;
		}

		// Token: 0x060025E7 RID: 9703 RVA: 0x000DB701 File Offset: 0x000D9901
		public static void RenderTimePicker(TextWriter writer, ExDateTime selectedTime, string id)
		{
			TimeDropDownList.RenderTimePicker(writer, selectedTime, id, true);
		}

		// Token: 0x060025E8 RID: 9704 RVA: 0x000DB70C File Offset: 0x000D990C
		public static void RenderTimePicker(TextWriter writer, ExDateTime selectedTime, string id, bool isEnabled)
		{
			TimeDropDownList.RenderTimePicker(writer, selectedTime, id, isEnabled, true);
		}

		// Token: 0x060025E9 RID: 9705 RVA: 0x000DB718 File Offset: 0x000D9918
		public static void RenderTimePicker(TextWriter writer, ExDateTime selectedTime, string id, bool isEnabled, bool isItemEditable)
		{
			TimeDropDownList.RenderTimePicker(writer, selectedTime, id, isEnabled, isItemEditable, ExDateTime.MinValue);
		}

		// Token: 0x060025EA RID: 9706 RVA: 0x000DB72C File Offset: 0x000D992C
		public static void RenderTimePicker(TextWriter writer, ExDateTime selectedTime, string id, bool isEnabled, bool isItemEditable, ExDateTime endTime)
		{
			new TimeDropDownList(selectedTime, id, "txtTime", endTime)
			{
				Enabled = isEnabled,
				isItemEditable = isItemEditable
			}.Render(writer);
		}

		// Token: 0x060025EB RID: 9707 RVA: 0x000DB760 File Offset: 0x000D9960
		protected override void RenderExpandoData(TextWriter writer)
		{
			base.RenderExpandoData(writer);
			ISessionContext sessionContext = OwaContext.Current.SessionContext;
			writer.Write(" sTm=\"");
			writer.Write(DateTimeUtilities.GetJavascriptDate(this.selectedTime));
			writer.Write("\"");
			if (this.endTime != ExDateTime.MinValue)
			{
				writer.Write(" sEndTm=\"");
				writer.Write(DateTimeUtilities.GetJavascriptDate(this.endTime));
				writer.Write("\"");
			}
			writer.Write(" sStf=\"");
			Utilities.SanitizeHtmlEncode(sessionContext.TimeFormat, writer);
			writer.Write("\"");
			writer.Write(" sAmPmRx=\"");
			TimeDropDownList.RenderAmPmRegularExpression(writer);
			writer.Write("\"");
			writer.Write(" L_InvldTm=\"");
			writer.Write(SanitizedHtmlString.FromStringId(-863308736));
			writer.Write("\"");
			writer.Write(" L_Dec=\"");
			writer.Write(SanitizedHtmlString.FromStringId(-1032346272));
			writer.Write("\"");
			writer.Write(" L_AM=\"");
			writer.Write(Utilities.SanitizeHtmlEncode(Culture.AMDesignator));
			writer.Write("\"");
			writer.Write(" L_PM=\"");
			writer.Write(Utilities.SanitizeHtmlEncode(Culture.PMDesignator));
			writer.Write("\"");
		}

		// Token: 0x060025EC RID: 9708 RVA: 0x000DB8B8 File Offset: 0x000D9AB8
		protected override void RenderSelectedValue(TextWriter writer)
		{
			writer.Write("<input type=\"text\" id=");
			writer.Write(this.valueId);
			if (!this.isItemEditable)
			{
				writer.Write(" readonly=\"true\" ");
			}
			if (base.Enabled)
			{
				writer.Write(" value=\"");
			}
			else
			{
				writer.Write(" disabled value=\"");
			}
			writer.Write(this.selectedTime.ToString(this.sessionContext.TimeFormat));
			writer.Write("\">");
		}

		// Token: 0x060025ED RID: 9709 RVA: 0x000DB936 File Offset: 0x000D9B36
		protected override void RenderListItems(TextWriter writer)
		{
		}

		// Token: 0x060025EE RID: 9710 RVA: 0x000DB938 File Offset: 0x000D9B38
		private static void RenderAmPmRegularExpression(TextWriter writer)
		{
			string amdesignator = Culture.AMDesignator;
			string pmdesignator = Culture.PMDesignator;
			if (!string.IsNullOrEmpty(amdesignator) && !string.IsNullOrEmpty(pmdesignator) && amdesignator[0] != pmdesignator[0])
			{
				writer.Write("(");
				writer.Write(Utilities.SanitizeHtmlEncode(Culture.AMDesignator));
				writer.Write("|");
				Utilities.SanitizeHtmlEncode(amdesignator.Substring(0, 1), writer);
				writer.Write(")|(");
				writer.Write(Utilities.SanitizeHtmlEncode(Culture.PMDesignator));
				writer.Write("|");
				Utilities.SanitizeHtmlEncode(pmdesignator.Substring(0, 1), writer);
				writer.Write(")");
				return;
			}
			writer.Write("(");
			if (!string.IsNullOrEmpty(amdesignator))
			{
				writer.Write(Utilities.SanitizeHtmlEncode(Culture.AMDesignator));
			}
			writer.Write(")|(");
			if (!string.IsNullOrEmpty(pmdesignator))
			{
				writer.Write(Utilities.SanitizeHtmlEncode(Culture.PMDesignator));
			}
			writer.Write(")");
		}

		// Token: 0x04001A24 RID: 6692
		private ExDateTime selectedTime;

		// Token: 0x04001A25 RID: 6693
		private ExDateTime endTime;

		// Token: 0x04001A26 RID: 6694
		private string valueId;

		// Token: 0x04001A27 RID: 6695
		private bool isItemEditable;
	}
}
