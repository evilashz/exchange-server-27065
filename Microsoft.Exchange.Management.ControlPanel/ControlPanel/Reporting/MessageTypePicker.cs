using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using AjaxControlToolkit;
using Microsoft.Exchange.Data.Search.AqsParser;

namespace Microsoft.Exchange.Management.ControlPanel.Reporting
{
	// Token: 0x020003E2 RID: 994
	[RequiredScript(typeof(CommonToolkitScripts))]
	[ClientScriptResource("MessageTypePicker", "Microsoft.Exchange.Management.ControlPanel.Client.Reporting.js")]
	public class MessageTypePicker : BaseForm
	{
		// Token: 0x0600333A RID: 13114 RVA: 0x0009EE36 File Offset: 0x0009D036
		public MessageTypePicker()
		{
			base.HideFieldValidationAssistant = true;
			base.CommitButtonText = Strings.OkButtonText;
		}

		// Token: 0x17002012 RID: 8210
		// (get) Token: 0x0600333B RID: 13115 RVA: 0x0009EE55 File Offset: 0x0009D055
		public string AllTypesRadioButtonID
		{
			get
			{
				return this.rbMessageTypeAll.ClientID;
			}
		}

		// Token: 0x17002013 RID: 8211
		// (get) Token: 0x0600333C RID: 13116 RVA: 0x0009EE62 File Offset: 0x0009D062
		public string SpecificTypesRadioButtonID
		{
			get
			{
				return this.rbMessageTypeSpecific.ClientID;
			}
		}

		// Token: 0x17002014 RID: 8212
		// (get) Token: 0x0600333D RID: 13117 RVA: 0x0009EE6F File Offset: 0x0009D06F
		// (set) Token: 0x0600333E RID: 13118 RVA: 0x0009EE77 File Offset: 0x0009D077
		public string[] Value { get; set; }

		// Token: 0x0600333F RID: 13119 RVA: 0x0009EE80 File Offset: 0x0009D080
		protected void Page_Load(object sender, EventArgs e)
		{
			if (base.Request["types"] == null)
			{
				throw new BadQueryParameterException("types");
			}
			try
			{
				StringArrayConverter stringArrayConverter = new StringArrayConverter();
				this.Value = (string[])stringArrayConverter.ConvertFrom(base.Request["types"]);
			}
			catch (NotSupportedException innerException)
			{
				throw new BadQueryParameterException("types", innerException);
			}
			try
			{
				foreach (string value2 in this.Value)
				{
					Enum.Parse(typeof(KindKeyword), value2, true);
				}
			}
			catch (ArgumentException innerException2)
			{
				throw new BadQueryParameterException("types", innerException2);
			}
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x0009EF40 File Offset: 0x0009D140
		protected override void OnPreRender(EventArgs e)
		{
			base.OnPreRender(e);
			this.typeEnumToControlMapping = new Dictionary<string, string>(Enum.GetValues(typeof(KindKeyword)).Length);
			this.typeEnumToControlMapping.Add(KindKeyword.email.ToString(), this.cbxMessageTypeEmail.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.meetings.ToString(), this.cbxMessageTypeMeetings.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.tasks.ToString(), this.cbxMessageTypeTasks.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.notes.ToString(), this.cbxMessageTypeNotes.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.docs.ToString(), this.cbxMessageTypeDocs.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.journals.ToString(), this.cbxMessageTypeJournal.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.contacts.ToString(), this.cbxMessageTypeContacts.ClientID);
			this.typeEnumToControlMapping.Add(KindKeyword.im.ToString(), this.cbxMessageTypeIMs.ClientID);
			if (this.Value == null || this.Value.Length == 0)
			{
				this.rbMessageTypeAll.Checked = true;
				return;
			}
			this.rbMessageTypeSpecific.Checked = true;
			int length = ((CommonMaster)base.Master).ContentPlaceHolder.ClientID.Length;
			foreach (string key in this.Value)
			{
				string text = this.typeEnumToControlMapping[key];
				CheckBox checkBox = (CheckBox)base.ContentPanel.FindControl(text.Substring(length + 1, text.Length - length - 1));
				if (checkBox != null)
				{
					checkBox.Checked = true;
				}
			}
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0009F120 File Offset: 0x0009D320
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddElementProperty("AllTypesRadioButton", this.AllTypesRadioButtonID, true);
			descriptor.AddElementProperty("SpecificTypesRadioButton", this.SpecificTypesRadioButtonID, true);
			descriptor.AddProperty("value", this.Value);
			descriptor.AddProperty("TypeEnumToControlMapping", this.typeEnumToControlMapping);
		}

		// Token: 0x040024B6 RID: 9398
		private Dictionary<string, string> typeEnumToControlMapping;

		// Token: 0x040024B7 RID: 9399
		protected RadioButton rbMessageTypeAll;

		// Token: 0x040024B8 RID: 9400
		protected RadioButton rbMessageTypeSpecific;

		// Token: 0x040024B9 RID: 9401
		protected CheckBox cbxMessageTypeEmail;

		// Token: 0x040024BA RID: 9402
		protected CheckBox cbxMessageTypeMeetings;

		// Token: 0x040024BB RID: 9403
		protected CheckBox cbxMessageTypeTasks;

		// Token: 0x040024BC RID: 9404
		protected CheckBox cbxMessageTypeNotes;

		// Token: 0x040024BD RID: 9405
		protected CheckBox cbxMessageTypeDocs;

		// Token: 0x040024BE RID: 9406
		protected CheckBox cbxMessageTypeJournal;

		// Token: 0x040024BF RID: 9407
		protected CheckBox cbxMessageTypeContacts;

		// Token: 0x040024C0 RID: 9408
		protected CheckBox cbxMessageTypeIMs;

		// Token: 0x040024C1 RID: 9409
		protected CheckBox cbxMessageTypeVoiceMail;

		// Token: 0x040024C2 RID: 9410
		protected CheckBox cbxMessageTypeFaxes;

		// Token: 0x040024C3 RID: 9411
		protected CheckBox cbxMessageTypePosts;

		// Token: 0x040024C4 RID: 9412
		protected CheckBox cbxMessageTypeRssFeeds;
	}
}
