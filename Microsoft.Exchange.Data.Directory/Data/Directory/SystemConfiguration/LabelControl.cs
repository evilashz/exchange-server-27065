using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002EB RID: 747
	[Serializable]
	internal sealed class LabelControl : DetailsTemplateControl
	{
		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x060022E9 RID: 8937 RVA: 0x0009801D File Offset: 0x0009621D
		// (set) Token: 0x060022EA RID: 8938 RVA: 0x00098025 File Offset: 0x00096225
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

		// Token: 0x060022EB RID: 8939 RVA: 0x0009804F File Offset: 0x0009624F
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Label;
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x0009805A File Offset: 0x0009625A
		public override string ToString()
		{
			return "Label";
		}
	}
}
