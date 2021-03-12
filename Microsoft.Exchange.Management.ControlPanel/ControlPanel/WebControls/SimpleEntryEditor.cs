using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Web.UI;
using AjaxControlToolkit;

namespace Microsoft.Exchange.Management.ControlPanel.WebControls
{
	// Token: 0x02000567 RID: 1383
	[ClientScriptResource("SimpleEntryEditor", "Microsoft.Exchange.Management.ControlPanel.Client.Rules.js")]
	[ParseChildren(true)]
	[ToolboxData("<{0}:SimpleEntryEditor runat=server></{0}:SimpleEntryEditor>")]
	public abstract class SimpleEntryEditor<T> : ScriptControlBase where T : Control, new()
	{
		// Token: 0x0600406C RID: 16492 RVA: 0x000C4930 File Offset: 0x000C2B30
		protected SimpleEntryEditor() : base(HtmlTextWriterTag.Div)
		{
			base.Style.Add(HtmlTextWriterStyle.Display, "none");
			this.Controls.Add(this.EditControl);
			EncodingLabel encodingLabel = new EncodingLabel();
			encodingLabel.ID = "label";
			encodingLabel.CssClass = "HiddenForScreenReader";
			T t = this.EditControl;
			t.Controls.Add(encodingLabel);
		}

		// Token: 0x170024F7 RID: 9463
		// (get) Token: 0x0600406D RID: 16493 RVA: 0x000C49BD File Offset: 0x000C2BBD
		// (set) Token: 0x0600406E RID: 16494 RVA: 0x000C49C5 File Offset: 0x000C2BC5
		[Browsable(false)]
		public new double Width
		{
			get
			{
				return this.width;
			}
			set
			{
				this.width = value;
			}
		}

		// Token: 0x170024F8 RID: 9464
		// (get) Token: 0x0600406F RID: 16495 RVA: 0x000C49D0 File Offset: 0x000C2BD0
		[Browsable(false)]
		public string EditControlID
		{
			get
			{
				T t = this.EditControl;
				return t.ClientID;
			}
		}

		// Token: 0x170024F9 RID: 9465
		// (get) Token: 0x06004070 RID: 16496 RVA: 0x000C49F1 File Offset: 0x000C2BF1
		[Browsable(false)]
		public string TypeName
		{
			get
			{
				return base.GetType().Name;
			}
		}

		// Token: 0x170024FA RID: 9466
		// (get) Token: 0x06004071 RID: 16497 RVA: 0x000C49FE File Offset: 0x000C2BFE
		[Browsable(false)]
		public T EditControl
		{
			get
			{
				return this.editControl;
			}
		}

		// Token: 0x06004072 RID: 16498 RVA: 0x000C4A08 File Offset: 0x000C2C08
		protected sealed override List<ScriptDescriptor> CreateScriptDescriptors()
		{
			List<ScriptDescriptor> list = new List<ScriptDescriptor>();
			ScriptComponentDescriptor item = new ScriptComponentDescriptor(this.ClientControlType);
			list.Add(item);
			return list;
		}

		// Token: 0x06004073 RID: 16499 RVA: 0x000C4A30 File Offset: 0x000C2C30
		protected override void BuildScriptDescriptor(ScriptComponentDescriptor descriptor)
		{
			base.BuildScriptDescriptor(descriptor);
			descriptor.AddProperty("Width", this.Width);
			descriptor.AddProperty("EditControlID", this.EditControlID, true);
			descriptor.AddProperty("TypeName", this.TypeName, true);
		}

		// Token: 0x04002AE0 RID: 10976
		private double width = -1.0;

		// Token: 0x04002AE1 RID: 10977
		private T editControl = Activator.CreateInstance<T>();
	}
}
