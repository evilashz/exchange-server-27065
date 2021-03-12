using System;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002EC RID: 748
	[Serializable]
	internal sealed class ListboxControl : DetailsTemplateControl
	{
		// Token: 0x170008D5 RID: 2261
		// (get) Token: 0x060022EE RID: 8942 RVA: 0x00098061 File Offset: 0x00096261
		// (set) Token: 0x060022EF RID: 8943 RVA: 0x00098069 File Offset: 0x00096269
		public ScrollBars ScrollBars
		{
			get
			{
				return this.scrollBars;
			}
			set
			{
				if (this.scrollBars != value)
				{
					this.scrollBars = value;
					base.NotifyPropertyChanged("ScrollBars");
				}
			}
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060022F0 RID: 8944 RVA: 0x00098086 File Offset: 0x00096286
		// (set) Token: 0x060022F1 RID: 8945 RVA: 0x0009808E File Offset: 0x0009628E
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

		// Token: 0x060022F2 RID: 8946 RVA: 0x000980AE File Offset: 0x000962AE
		internal override DetailsTemplateControl.ControlTypes GetControlType()
		{
			return DetailsTemplateControl.ControlTypes.Listbox;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000980B1 File Offset: 0x000962B1
		internal override DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.Listbox;
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000980B4 File Offset: 0x000962B4
		internal override bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return base.ValidateAttributeHelper(propertiesDictionary);
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000980C0 File Offset: 0x000962C0
		internal override DetailsTemplateControl.ControlFlags GetControlFlags()
		{
			DetailsTemplateControl.ControlFlags originalFlags = this.OriginalFlags;
			bool flag = this.scrollBars == ScrollBars.Both || this.scrollBars == ScrollBars.Vertical;
			bool flag2 = this.scrollBars == ScrollBars.Both || this.scrollBars == ScrollBars.Horizontal;
			DetailsTemplateControl.SetBitField(!flag, DetailsTemplateControl.ControlFlags.ReadOnly, ref originalFlags);
			DetailsTemplateControl.SetBitField(!flag2, DetailsTemplateControl.ControlFlags.Multiline, ref originalFlags);
			return originalFlags;
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x0009811C File Offset: 0x0009631C
		internal ListboxControl(DetailsTemplateControl.ControlFlags controlFlags)
		{
			bool flag = (controlFlags & DetailsTemplateControl.ControlFlags.ReadOnly) == (DetailsTemplateControl.ControlFlags)0U;
			bool flag2 = (controlFlags & DetailsTemplateControl.ControlFlags.Multiline) == (DetailsTemplateControl.ControlFlags)0U;
			if (flag2 && flag)
			{
				this.scrollBars = ScrollBars.Both;
				return;
			}
			if (flag2)
			{
				this.scrollBars = ScrollBars.Horizontal;
				return;
			}
			if (flag)
			{
				this.scrollBars = ScrollBars.Vertical;
				return;
			}
			this.scrollBars = ScrollBars.None;
		}

		// Token: 0x060022F7 RID: 8951 RVA: 0x00098168 File Offset: 0x00096368
		public ListboxControl()
		{
			this.m_Text = DetailsTemplateControl.NoTextString;
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x0009817B File Offset: 0x0009637B
		internal override DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.Listbox;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x0009817F File Offset: 0x0009637F
		public override string ToString()
		{
			return "List Box";
		}

		// Token: 0x040015C0 RID: 5568
		private ScrollBars scrollBars;
	}
}
