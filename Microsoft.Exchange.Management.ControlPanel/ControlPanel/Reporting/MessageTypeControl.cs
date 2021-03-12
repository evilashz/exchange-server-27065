using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data.Search.AqsParser;

namespace Microsoft.Exchange.Management.ControlPanel.Reporting
{
	// Token: 0x020003E1 RID: 993
	[ClientScriptResource("MessageTypeControl", "Microsoft.Exchange.Management.ControlPanel.Client.Reporting.js")]
	public class MessageTypeControl : WebControl, IScriptControl, INamingContainer
	{
		// Token: 0x0600332F RID: 13103 RVA: 0x0009E960 File Offset: 0x0009CB60
		public MessageTypeControl()
		{
			this.locStringMapping = new Dictionary<string, string>();
			this.locStringMapping.Add(KindKeyword.email.ToString(), Strings.MessageTypeEmail);
			this.locStringMapping.Add(KindKeyword.meetings.ToString(), Strings.MessageTypeMeetings);
			this.locStringMapping.Add(KindKeyword.tasks.ToString(), Strings.MessageTypeTasks);
			this.locStringMapping.Add(KindKeyword.notes.ToString(), Strings.MessageTypeNotes);
			this.locStringMapping.Add(KindKeyword.docs.ToString(), Strings.MessageTypeDocs);
			this.locStringMapping.Add(KindKeyword.journals.ToString(), Strings.MessageTypeJournal);
			this.locStringMapping.Add(KindKeyword.contacts.ToString(), Strings.MessageTypeContacts);
			this.locStringMapping.Add(KindKeyword.im.ToString(), Strings.MessageTypeInstantMessage);
			this.locStringMapping.Add(KindKeyword.voicemail.ToString(), Strings.MessageTypeVoiceMail);
			this.locStringMapping.Add(KindKeyword.posts.ToString(), Strings.MessageTypePosts);
			this.locStringMapping.Add(KindKeyword.rssfeeds.ToString(), Strings.MessageTypeRssFeeds);
			this.locStringMapping.Add(KindKeyword.faxes.ToString(), Strings.MessageTypeFaxes);
		}

		// Token: 0x1700200F RID: 8207
		// (get) Token: 0x06003330 RID: 13104 RVA: 0x0009EB01 File Offset: 0x0009CD01
		// (set) Token: 0x06003331 RID: 13105 RVA: 0x0009EB09 File Offset: 0x0009CD09
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] DefaultValue { get; set; }

		// Token: 0x17002010 RID: 8208
		// (get) Token: 0x06003332 RID: 13106 RVA: 0x0009EB12 File Offset: 0x0009CD12
		// (set) Token: 0x06003333 RID: 13107 RVA: 0x0009EB1A File Offset: 0x0009CD1A
		[TypeConverter(typeof(StringArrayConverter))]
		public string[] Value { get; set; }

		// Token: 0x17002011 RID: 8209
		// (get) Token: 0x06003334 RID: 13108 RVA: 0x0009EB23 File Offset: 0x0009CD23
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0009EB28 File Offset: 0x0009CD28
		IEnumerable<ScriptDescriptor> IScriptControl.GetScriptDescriptors()
		{
			ScriptControlDescriptor scriptControlDescriptor = new ScriptControlDescriptor("MessageTypeControl", this.ClientID);
			scriptControlDescriptor.AddElementProperty("LaunchButton", this.btnLaunch.ClientID);
			scriptControlDescriptor.AddElementProperty("ValueLabel", this.lblValue.ClientID);
			scriptControlDescriptor.AddProperty("MessageTypePickerUrl", base.ResolveClientUrl("~/Reporting/MessageTypePicker.aspx"));
			scriptControlDescriptor.AddProperty("ValueArray", this.Value);
			scriptControlDescriptor.AddProperty("LocStringMapping", this.locStringMapping);
			return new ScriptDescriptor[]
			{
				scriptControlDescriptor
			};
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x0009EBB6 File Offset: 0x0009CDB6
		IEnumerable<ScriptReference> IScriptControl.GetScriptReferences()
		{
			return ScriptObjectBuilder.GetScriptReferences(typeof(MessageTypeControl));
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x0009EBC8 File Offset: 0x0009CDC8
		protected override void CreateChildControls()
		{
			base.CreateChildControls();
			this.promptText = new Literal();
			this.promptText.ID = "promptText";
			this.promptText.Text = Strings.MessageTypesToSearch;
			this.lblValue = new Label();
			this.lblValue.ID = "lblValue";
			this.lblValue.CssClass = "messageTypePromptLabel";
			this.btnLaunch = new HtmlButton();
			this.btnLaunch.ID = "btnLaunch";
			this.btnLaunch.InnerText = Strings.MessageTypesToSearchButtonText;
			this.btnLaunch.CausesValidation = false;
			this.btnLaunch.Attributes["onClick"] = "javascript:return false;";
			this.btnLaunch.Attributes.Add("class", "messageTypeLaunchButton");
			Table table = new Table();
			table.CellPadding = 0;
			table.CellSpacing = 0;
			TableRow tableRow = new TableRow();
			TableCell tableCell = new TableCell();
			tableCell.Controls.Add(this.promptText);
			tableCell.Controls.Add(this.lblValue);
			tableRow.Cells.Add(tableCell);
			TableRow tableRow2 = new TableRow();
			TableCell tableCell2 = new TableCell();
			tableCell2.Controls.Add(this.btnLaunch);
			tableRow2.Cells.Add(tableCell2);
			table.Rows.Add(tableRow);
			table.Rows.Add(tableRow2);
			this.Controls.Add(table);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0009ED48 File Offset: 0x0009CF48
		protected override void OnPreRender(EventArgs e)
		{
			if (!this.Page.IsPostBack && this.DefaultValue != null && this.DefaultValue.Length > 0)
			{
				foreach (string value in this.DefaultValue)
				{
					Enum.Parse(typeof(KindKeyword), value, true);
				}
				this.Value = this.DefaultValue;
			}
			base.OnPreRender(e);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptControl<MessageTypeControl>(this);
			}
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0009EDCC File Offset: 0x0009CFCC
		protected override void Render(HtmlTextWriter writer)
		{
			this.btnLaunch.Attributes["class"] = (this.Enabled ? "messageTypeLaunchButton" : "messageTypeLaunchButtonDisabled");
			this.btnLaunch.Disabled = !this.Enabled;
			base.Render(writer);
			if (!base.DesignMode)
			{
				ScriptManager.GetCurrent(this.Page).RegisterScriptDescriptors(this);
			}
		}

		// Token: 0x040024AF RID: 9391
		private const string MessageTypePickerUrl = "~/Reporting/MessageTypePicker.aspx";

		// Token: 0x040024B0 RID: 9392
		private Literal promptText;

		// Token: 0x040024B1 RID: 9393
		private Label lblValue;

		// Token: 0x040024B2 RID: 9394
		private HtmlButton btnLaunch;

		// Token: 0x040024B3 RID: 9395
		private Dictionary<string, string> locStringMapping;
	}
}
