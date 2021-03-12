using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002B2 RID: 690
	[Serializable]
	internal sealed class ButtonControl : DetailsTemplateControl
	{
		// Token: 0x17000797 RID: 1943
		// (get) Token: 0x06001FB3 RID: 8115 RVA: 0x0008C719 File Offset: 0x0008A919
		// (set) Token: 0x06001FB4 RID: 8116 RVA: 0x0008C721 File Offset: 0x0008A921
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

		// Token: 0x06001FB6 RID: 8118 RVA: 0x0008C753 File Offset: 0x0008A953
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Button;
		}

		// Token: 0x06001FB7 RID: 8119 RVA: 0x0008C756 File Offset: 0x0008A956
		public override string ToString()
		{
			return "Button";
		}

		// Token: 0x040012FF RID: 4863
		internal static uint MapiInt = 1728315405U;
	}
}
