using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002EA RID: 746
	[Serializable]
	internal sealed class GroupboxControl : DetailsTemplateControl
	{
		// Token: 0x170008D3 RID: 2259
		// (get) Token: 0x060022E4 RID: 8932 RVA: 0x00097FD9 File Offset: 0x000961D9
		// (set) Token: 0x060022E5 RID: 8933 RVA: 0x00097FE1 File Offset: 0x000961E1
		public string Text
		{
			get
			{
				return this.m_Text;
			}
			set
			{
				if (this.m_Text != value)
				{
					DetailsTemplateControl.ValidateText(value, DetailsTemplateControl.TextLengths.Label);
					this.m_Text = value;
					base.NotifyPropertyChanged("Text");
				}
			}
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x0009800B File Offset: 0x0009620B
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Groupbox;
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x00098016 File Offset: 0x00096216
		public override string ToString()
		{
			return "Group Box";
		}
	}
}
