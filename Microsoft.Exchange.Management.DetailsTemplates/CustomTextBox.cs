using System;
using System.ComponentModel;
using System.Windows.Forms;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.DetailsTemplates
{
	// Token: 0x0200000A RID: 10
	[Designer(typeof(CustomControlDesigner))]
	internal sealed class CustomTextBox : TextBox, IDetailsTemplateControlBound
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002792 File Offset: 0x00000992
		public CustomTextBox()
		{
			this.AutoSize = false;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000039 RID: 57 RVA: 0x000027AC File Offset: 0x000009AC
		// (set) Token: 0x0600003A RID: 58 RVA: 0x000027B9 File Offset: 0x000009B9
		[TypeConverter(typeof(MAPITypeConverter))]
		public string AttributeName
		{
			get
			{
				return this.detailsTemplateControl.AttributeName;
			}
			set
			{
				this.detailsTemplateControl.AttributeName = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600003B RID: 59 RVA: 0x000027C7 File Offset: 0x000009C7
		// (set) Token: 0x0600003C RID: 60 RVA: 0x000027D4 File Offset: 0x000009D4
		public bool ConfirmationRequired
		{
			get
			{
				return this.detailsTemplateControl.ConfirmationRequired;
			}
			set
			{
				this.detailsTemplateControl.ConfirmationRequired = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600003D RID: 61 RVA: 0x000027E2 File Offset: 0x000009E2
		// (set) Token: 0x0600003E RID: 62 RVA: 0x000027EF File Offset: 0x000009EF
		public new bool Multiline
		{
			get
			{
				return this.detailsTemplateControl.Multiline;
			}
			set
			{
				this.detailsTemplateControl.Multiline = value;
				base.Multiline = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002804 File Offset: 0x00000A04
		// (set) Token: 0x06000040 RID: 64 RVA: 0x00002811 File Offset: 0x00000A11
		public new bool ReadOnly
		{
			get
			{
				return this.detailsTemplateControl.ReadOnly;
			}
			set
			{
				this.detailsTemplateControl.ReadOnly = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000041 RID: 65 RVA: 0x0000281F File Offset: 0x00000A1F
		// (set) Token: 0x06000042 RID: 66 RVA: 0x0000282C File Offset: 0x00000A2C
		public new bool UseSystemPasswordChar
		{
			get
			{
				return this.detailsTemplateControl.UseSystemPasswordChar;
			}
			set
			{
				this.detailsTemplateControl.UseSystemPasswordChar = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000043 RID: 67 RVA: 0x0000283A File Offset: 0x00000A3A
		// (set) Token: 0x06000044 RID: 68 RVA: 0x00002847 File Offset: 0x00000A47
		public override int MaxLength
		{
			get
			{
				return this.detailsTemplateControl.MaxLength;
			}
			set
			{
				this.detailsTemplateControl.MaxLength = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00002855 File Offset: 0x00000A55
		// (set) Token: 0x06000046 RID: 70 RVA: 0x0000285D File Offset: 0x00000A5D
		[Browsable(false)]
		public DetailsTemplateControl DetailsTemplateControl
		{
			get
			{
				return this.detailsTemplateControl;
			}
			set
			{
				this.detailsTemplateControl = (value as EditControl);
				base.Multiline = this.detailsTemplateControl.Multiline;
			}
		}

		// Token: 0x04000008 RID: 8
		private EditControl detailsTemplateControl = new EditControl();
	}
}
