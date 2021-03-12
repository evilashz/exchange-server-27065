using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Microsoft.ManagementGUI.WinForms;

namespace Microsoft.Exchange.Management.SystemManager.WinForms
{
	// Token: 0x020001C8 RID: 456
	public class LinkLabelCommand : ExchangeLinkLabel
	{
		// Token: 0x0600132C RID: 4908 RVA: 0x0004E108 File Offset: 0x0004C308
		public LinkLabelCommand()
		{
			base.Name = "LinkLabelCommand";
			this.UseMnemonic = false;
			this.bindingSource = new BindingSource();
			this.bindingSource.CurrentItemChanged += this.bindingSource_CurrentItemChanged;
		}

		// Token: 0x17000464 RID: 1124
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0004E16F File Offset: 0x0004C36F
		// (set) Token: 0x0600132E RID: 4910 RVA: 0x0004E177 File Offset: 0x0004C377
		[DefaultValue(false)]
		public bool UseMnemonic
		{
			get
			{
				return base.UseMnemonic;
			}
			set
			{
				base.UseMnemonic = value;
			}
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x0004E180 File Offset: 0x0004C380
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.bindingSource.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0004E197 File Offset: 0x0004C397
		// (set) Token: 0x06001331 RID: 4913 RVA: 0x0004E19F File Offset: 0x0004C39F
		[DefaultValue("")]
		public string Text
		{
			get
			{
				return this.markupText;
			}
			set
			{
				value = (value ?? "");
				if (value != this.Text)
				{
					this.SetTextFromMarkupText(value);
					this.markupText = value;
				}
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0004E1C9 File Offset: 0x0004C3C9
		// (set) Token: 0x06001333 RID: 4915 RVA: 0x0004E1D1 File Offset: 0x0004C3D1
		public string ListSeparator
		{
			get
			{
				return this.listSeparator;
			}
			set
			{
				value = (value ?? this.DefaultListSeparator);
				if (value != this.ListSeparator)
				{
					this.listSeparator = value;
					this.SetTextFromMarkupText(this.Text);
				}
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001334 RID: 4916 RVA: 0x0004E201 File Offset: 0x0004C401
		protected virtual string DefaultListSeparator
		{
			get
			{
				return CultureInfo.CurrentUICulture.TextInfo.ListSeparator;
			}
		}

		// Token: 0x06001335 RID: 4917 RVA: 0x0004E212 File Offset: 0x0004C412
		private bool ShouldSerializeListSeparator()
		{
			return this.ListSeparator != this.DefaultListSeparator;
		}

		// Token: 0x06001336 RID: 4918 RVA: 0x0004E225 File Offset: 0x0004C425
		private void ResetListSeparator()
		{
			this.ListSeparator = this.DefaultListSeparator;
		}

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x06001337 RID: 4919 RVA: 0x0004E233 File Offset: 0x0004C433
		// (set) Token: 0x06001338 RID: 4920 RVA: 0x0004E240 File Offset: 0x0004C440
		[DefaultValue(null)]
		public object DataSource
		{
			get
			{
				return this.bindingSource.DataSource;
			}
			set
			{
				this.bindingSource.DataSource = value;
			}
		}

		// Token: 0x06001339 RID: 4921 RVA: 0x0004E24E File Offset: 0x0004C44E
		public void SuspendUpdates()
		{
			this.suspendUpdates++;
		}

		// Token: 0x0600133A RID: 4922 RVA: 0x0004E25E File Offset: 0x0004C45E
		public void ResumeUpdates()
		{
			if (this.suspendUpdates == 0)
			{
				throw new InvalidOperationException();
			}
			this.suspendUpdates--;
			if (this.suspendUpdates == 0)
			{
				this.bindingSource.ResetBindings(false);
			}
		}

		// Token: 0x0600133B RID: 4923 RVA: 0x0004E290 File Offset: 0x0004C490
		private void bindingSource_CurrentItemChanged(object sender, EventArgs e)
		{
			if (this.suspendUpdates == 0)
			{
				this.SetTextFromMarkupText(this.Text);
			}
		}

		// Token: 0x0600133C RID: 4924 RVA: 0x0004E2A8 File Offset: 0x0004C4A8
		private void SetTextFromMarkupText(string markupText)
		{
			MarkupParser markupParser = new MarkupParser();
			markupParser.Markup = markupText;
			markupParser.ReplaceAnchorValues(this.DataSource, this.ListSeparator);
			StringBuilder stringBuilder = new StringBuilder();
			base.Links.Clear();
			foreach (object obj in markupParser.Nodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (XmlNodeType.Element == xmlNode.NodeType && "a" == xmlNode.Name)
				{
					XmlAttribute xmlAttribute = xmlNode.Attributes["id"];
					if (xmlAttribute != null)
					{
						base.Links.Add(new StringInfo(stringBuilder.ToString()).LengthInTextElements, new StringInfo(xmlNode.InnerText).LengthInTextElements, xmlAttribute.Value);
					}
				}
				stringBuilder.Append(xmlNode.InnerText);
			}
			if (base.Text == stringBuilder.ToString() && !string.IsNullOrEmpty(base.Text))
			{
				this.OnTextChanged(EventArgs.Empty);
			}
			base.Text = stringBuilder.ToString();
		}

		// Token: 0x0600133D RID: 4925 RVA: 0x0004E3DC File Offset: 0x0004C5DC
		protected override void OnClick(EventArgs e)
		{
			base.Select();
			base.OnClick(e);
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x0600133E RID: 4926 RVA: 0x0004E3EC File Offset: 0x0004C5EC
		public override int PreferredHeight
		{
			get
			{
				if (!string.IsNullOrEmpty(base.Text))
				{
					if (base.IsHandleCreated)
					{
						using (Graphics graphics = base.CreateGraphics())
						{
							graphics.PageUnit = GraphicsUnit.Pixel;
							using (StringFormat stringFormat = new StringFormat(StringFormat.GenericTypographic.FormatFlags | StringFormatFlags.FitBlackBox | StringFormatFlags.NoClip))
							{
								int width = base.Width - (base.Padding.Left + base.Padding.Right);
								int height = Size.Ceiling(graphics.MeasureString(base.Text, this.Font, width, stringFormat)).Height;
								return height + 3 + base.Padding.Top + base.Padding.Bottom;
							}
						}
					}
					return base.Height;
				}
				return 0;
			}
		}

		// Token: 0x0600133F RID: 4927 RVA: 0x0004E4E8 File Offset: 0x0004C6E8
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			this.ApplyPreferredHeight();
		}

		// Token: 0x06001340 RID: 4928 RVA: 0x0004E4F7 File Offset: 0x0004C6F7
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.ApplyPreferredHeight();
		}

		// Token: 0x06001341 RID: 4929 RVA: 0x0004E506 File Offset: 0x0004C706
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			this.ApplyPreferredHeight();
		}

		// Token: 0x06001342 RID: 4930 RVA: 0x0004E515 File Offset: 0x0004C715
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			this.ApplyPreferredHeight();
		}

		// Token: 0x06001343 RID: 4931 RVA: 0x0004E524 File Offset: 0x0004C724
		private void ApplyPreferredHeight()
		{
			int preferredHeight = this.PreferredHeight;
			if (base.Height != preferredHeight)
			{
				base.Height = preferredHeight;
			}
		}

		// Token: 0x04000721 RID: 1825
		private BindingSource bindingSource;

		// Token: 0x04000722 RID: 1826
		private string markupText = "";

		// Token: 0x04000723 RID: 1827
		private string listSeparator = CultureInfo.CurrentUICulture.TextInfo.ListSeparator;

		// Token: 0x04000724 RID: 1828
		private int suspendUpdates;
	}
}
