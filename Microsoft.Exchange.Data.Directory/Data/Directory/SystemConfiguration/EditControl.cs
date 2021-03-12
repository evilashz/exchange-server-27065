using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002C2 RID: 706
	[Serializable]
	public sealed class EditControl : DetailsTemplateControl
	{
		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x0600203C RID: 8252 RVA: 0x0008F51F File Offset: 0x0008D71F
		// (set) Token: 0x0600203D RID: 8253 RVA: 0x0008F527 File Offset: 0x0008D727
		public string AttributeName
		{
			get
			{
				return this.m_AttributeName;
			}
			set
			{
				DetailsTemplateControl.ValidateAttributeName(value, this.GetAttributeControlType(), base.GetType().Name);
				this.m_AttributeName = value;
			}
		}

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x0600203E RID: 8254 RVA: 0x0008F547 File Offset: 0x0008D747
		// (set) Token: 0x0600203F RID: 8255 RVA: 0x0008F54F File Offset: 0x0008D74F
		public int MaxLength
		{
			get
			{
				return this.m_MaxLength;
			}
			set
			{
				DetailsTemplateControl.ValidateRange(value, 1, DetailsTemplateControl.EditMaxLength);
				this.m_MaxLength = value;
			}
		}

		// Token: 0x170007C9 RID: 1993
		// (get) Token: 0x06002040 RID: 8256 RVA: 0x0008F564 File Offset: 0x0008D764
		// (set) Token: 0x06002041 RID: 8257 RVA: 0x0008F56C File Offset: 0x0008D76C
		public bool UseSystemPasswordChar
		{
			get
			{
				return this.useSystemPasswordChar;
			}
			set
			{
				this.useSystemPasswordChar = value;
			}
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x06002042 RID: 8258 RVA: 0x0008F575 File Offset: 0x0008D775
		// (set) Token: 0x06002043 RID: 8259 RVA: 0x0008F57D File Offset: 0x0008D77D
		public bool ReadOnly
		{
			get
			{
				return this.readOnly;
			}
			set
			{
				this.readOnly = value;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x06002044 RID: 8260 RVA: 0x0008F586 File Offset: 0x0008D786
		// (set) Token: 0x06002045 RID: 8261 RVA: 0x0008F58E File Offset: 0x0008D78E
		public bool Multiline
		{
			get
			{
				return this.multiline;
			}
			set
			{
				if (value != this.multiline)
				{
					this.multiline = value;
					base.NotifyPropertyChanged("Multiline");
				}
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x06002046 RID: 8262 RVA: 0x0008F5AB File Offset: 0x0008D7AB
		// (set) Token: 0x06002047 RID: 8263 RVA: 0x0008F5B3 File Offset: 0x0008D7B3
		public bool ConfirmationRequired
		{
			get
			{
				return this.confirmationRequired;
			}
			set
			{
				this.confirmationRequired = value;
			}
		}

		// Token: 0x06002048 RID: 8264 RVA: 0x0008F5BC File Offset: 0x0008D7BC
		internal override DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.Edit;
		}

		// Token: 0x06002049 RID: 8265 RVA: 0x0008F5C0 File Offset: 0x0008D7C0
		internal override DetailsTemplateControl.ControlFlags GetControlFlags()
		{
			DetailsTemplateControl.ControlFlags originalFlags = this.OriginalFlags;
			DetailsTemplateControl.SetBitField(!this.ReadOnly, DetailsTemplateControl.ControlFlags.ReadOnly, ref originalFlags);
			DetailsTemplateControl.SetBitField(this.Multiline, DetailsTemplateControl.ControlFlags.Multiline, ref originalFlags);
			DetailsTemplateControl.SetBitField(this.UseSystemPasswordChar, DetailsTemplateControl.ControlFlags.UseSystemPasswordChar, ref originalFlags);
			DetailsTemplateControl.SetBitField(this.ConfirmationRequired, DetailsTemplateControl.ControlFlags.ConfirmationRequired, ref originalFlags);
			return originalFlags;
		}

		// Token: 0x0600204A RID: 8266 RVA: 0x0008F614 File Offset: 0x0008D814
		internal EditControl(DetailsTemplateControl.ControlFlags controlFlags)
		{
			this.UseSystemPasswordChar = ((controlFlags & DetailsTemplateControl.ControlFlags.UseSystemPasswordChar) != (DetailsTemplateControl.ControlFlags)0U);
			this.ReadOnly = ((controlFlags & DetailsTemplateControl.ControlFlags.ReadOnly) == (DetailsTemplateControl.ControlFlags)0U);
			this.Multiline = ((controlFlags & DetailsTemplateControl.ControlFlags.Multiline) != (DetailsTemplateControl.ControlFlags)0U);
			this.ConfirmationRequired = ((controlFlags & DetailsTemplateControl.ControlFlags.ConfirmationRequired) != (DetailsTemplateControl.ControlFlags)0U);
		}

		// Token: 0x0600204B RID: 8267 RVA: 0x0008F668 File Offset: 0x0008D868
		internal override bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return base.ValidateAttributeHelper(propertiesDictionary);
		}

		// Token: 0x0600204C RID: 8268 RVA: 0x0008F671 File Offset: 0x0008D871
		public EditControl()
		{
			this.OriginalFlags = DetailsTemplateControl.ControlFlags.AcceptDBCS;
			this.m_Text = DetailsTemplateControl.NoTextString;
			this.MaxLength = DetailsTemplateControl.EditDefaultLength;
		}

		// Token: 0x0600204D RID: 8269 RVA: 0x0008F69E File Offset: 0x0008D89E
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Edit;
		}

		// Token: 0x0600204E RID: 8270 RVA: 0x0008F6A1 File Offset: 0x0008D8A1
		internal override DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.Edit;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x0008F6A5 File Offset: 0x0008D8A5
		public override string ToString()
		{
			return "Edit";
		}

		// Token: 0x04001387 RID: 4999
		private bool useSystemPasswordChar;

		// Token: 0x04001388 RID: 5000
		private bool readOnly = true;

		// Token: 0x04001389 RID: 5001
		private bool multiline;

		// Token: 0x0400138A RID: 5002
		private bool confirmationRequired;
	}
}
