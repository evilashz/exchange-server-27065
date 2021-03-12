using System;
using System.ComponentModel;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x020002AC RID: 684
	[Serializable]
	public abstract class DetailsTemplateControl : INotifyPropertyChanged
	{
		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06001F9C RID: 8092 RVA: 0x0008C4C8 File Offset: 0x0008A6C8
		// (remove) Token: 0x06001F9D RID: 8093 RVA: 0x0008C500 File Offset: 0x0008A700
		public event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x06001F9E RID: 8094 RVA: 0x0008C535 File Offset: 0x0008A735
		protected static void ValidateAttributeName(string attribute, DetailsTemplateControl.AttributeControlTypes controlType, string controlName)
		{
			if (string.IsNullOrEmpty(attribute))
			{
				throw new ArgumentNullException(DirectoryStrings.AttributeNameNull);
			}
		}

		// Token: 0x06001F9F RID: 8095 RVA: 0x0008C54F File Offset: 0x0008A74F
		internal static void ValidateRange(int value, int minValue, int maxValue)
		{
			if (value < minValue || value > maxValue)
			{
				throw new ArgumentOutOfRangeException(DirectoryStrings.ValueNotInRange(minValue, maxValue));
			}
		}

		// Token: 0x06001FA0 RID: 8096 RVA: 0x0008C56B File Offset: 0x0008A76B
		internal static void ValidateText(string text, DetailsTemplateControl.TextLengths maxLength)
		{
			if (text == null)
			{
				throw new ArgumentNullException(DirectoryStrings.ControlTextNull);
			}
			if (text.Length > (int)maxLength)
			{
				throw new ArgumentException(DirectoryStrings.InvalidControlTextLength((int)maxLength));
			}
		}

		// Token: 0x06001FA1 RID: 8097 RVA: 0x0008C59A File Offset: 0x0008A79A
		internal static void SetBitField(bool setBit, DetailsTemplateControl.ControlFlags currentBit, ref DetailsTemplateControl.ControlFlags bitField)
		{
			if (setBit)
			{
				bitField |= currentBit;
				return;
			}
			bitField &= ~currentBit;
		}

		// Token: 0x06001FA2 RID: 8098 RVA: 0x0008C5AD File Offset: 0x0008A7AD
		protected void NotifyPropertyChanged(string info)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(info));
			}
		}

		// Token: 0x17000793 RID: 1939
		// (get) Token: 0x06001FA3 RID: 8099 RVA: 0x0008C5C9 File Offset: 0x0008A7C9
		// (set) Token: 0x06001FA4 RID: 8100 RVA: 0x0008C5D1 File Offset: 0x0008A7D1
		public int X
		{
			get
			{
				return this.x;
			}
			set
			{
				if (value != this.x)
				{
					DetailsTemplateControl.ValidateRange(value, 0, 32767);
					this.x = value;
					this.NotifyPropertyChanged("X");
				}
			}
		}

		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06001FA5 RID: 8101 RVA: 0x0008C5FA File Offset: 0x0008A7FA
		// (set) Token: 0x06001FA6 RID: 8102 RVA: 0x0008C602 File Offset: 0x0008A802
		public int Y
		{
			get
			{
				return this.y;
			}
			set
			{
				if (value != this.y)
				{
					DetailsTemplateControl.ValidateRange(value, 0, 32767);
					this.y = value;
					this.NotifyPropertyChanged("Y");
				}
			}
		}

		// Token: 0x17000795 RID: 1941
		// (get) Token: 0x06001FA7 RID: 8103 RVA: 0x0008C62B File Offset: 0x0008A82B
		// (set) Token: 0x06001FA8 RID: 8104 RVA: 0x0008C633 File Offset: 0x0008A833
		public int Width
		{
			get
			{
				return this.width;
			}
			set
			{
				if (value != this.Width)
				{
					DetailsTemplateControl.ValidateRange(value, 1, 32767);
					this.width = value;
					this.NotifyPropertyChanged("Width");
				}
			}
		}

		// Token: 0x17000796 RID: 1942
		// (get) Token: 0x06001FA9 RID: 8105 RVA: 0x0008C65C File Offset: 0x0008A85C
		// (set) Token: 0x06001FAA RID: 8106 RVA: 0x0008C664 File Offset: 0x0008A864
		public int Height
		{
			get
			{
				return this.height;
			}
			set
			{
				if (value != this.Height)
				{
					DetailsTemplateControl.ValidateRange(value, 1, 32767);
					this.height = value;
					this.NotifyPropertyChanged("Height");
				}
			}
		}

		// Token: 0x06001FAB RID: 8107
		internal abstract DetailsTemplateControl.ControlTypes GetControlType();

		// Token: 0x06001FAC RID: 8108 RVA: 0x0008C68D File Offset: 0x0008A88D
		internal virtual DetailsTemplateControl.ControlFlags GetControlFlags()
		{
			return this.OriginalFlags;
		}

		// Token: 0x06001FAD RID: 8109 RVA: 0x0008C695 File Offset: 0x0008A895
		internal virtual DetailsTemplateControl.AttributeControlTypes GetAttributeControlType()
		{
			return DetailsTemplateControl.AttributeControlTypes.None;
		}

		// Token: 0x06001FAE RID: 8110 RVA: 0x0008C698 File Offset: 0x0008A898
		internal virtual DetailsTemplateControl.MapiPrefix GetMapiPrefix()
		{
			return DetailsTemplateControl.MapiPrefix.None;
		}

		// Token: 0x06001FAF RID: 8111 RVA: 0x0008C69B File Offset: 0x0008A89B
		internal DetailsTemplateControl()
		{
		}

		// Token: 0x06001FB0 RID: 8112 RVA: 0x0008C6B9 File Offset: 0x0008A8B9
		internal virtual bool ValidateAttribute(MAPIPropertiesDictionary propertiesDictionary)
		{
			return true;
		}

		// Token: 0x06001FB1 RID: 8113 RVA: 0x0008C6BC File Offset: 0x0008A8BC
		internal bool ValidateAttributeHelper(MAPIPropertiesDictionary propertiesDictionary)
		{
			return !string.IsNullOrEmpty(this.m_AttributeName) && propertiesDictionary != null && propertiesDictionary[this.m_AttributeName] != null && (propertiesDictionary[this.m_AttributeName].ControlType & this.GetAttributeControlType()) != DetailsTemplateControl.AttributeControlTypes.None;
		}

		// Token: 0x040012CA RID: 4810
		private const int MinCoordinate = 0;

		// Token: 0x040012CB RID: 4811
		private const int MaxCoordinate = 32767;

		// Token: 0x040012CC RID: 4812
		private const int MinWidthOrHeigth = 1;

		// Token: 0x040012CD RID: 4813
		private const int MaxWidthOrHeigth = 32767;

		// Token: 0x040012CE RID: 4814
		internal static string NoTextString = "*";

		// Token: 0x040012CF RID: 4815
		internal static int EditMaxLength = 4096;

		// Token: 0x040012D0 RID: 4816
		internal static int EditDefaultLength = 1024;

		// Token: 0x040012D2 RID: 4818
		internal string m_AttributeName = string.Empty;

		// Token: 0x040012D3 RID: 4819
		internal string m_Text = string.Empty;

		// Token: 0x040012D4 RID: 4820
		internal int m_MaxLength;

		// Token: 0x040012D5 RID: 4821
		internal DetailsTemplateControl.ControlFlags OriginalFlags;

		// Token: 0x040012D6 RID: 4822
		private int x;

		// Token: 0x040012D7 RID: 4823
		private int y;

		// Token: 0x040012D8 RID: 4824
		private int width;

		// Token: 0x040012D9 RID: 4825
		private int height;

		// Token: 0x020002AD RID: 685
		internal enum TextLengths
		{
			// Token: 0x040012DB RID: 4827
			Label = 127,
			// Token: 0x040012DC RID: 4828
			Page = 31,
			// Token: 0x040012DD RID: 4829
			Groupbox = 127,
			// Token: 0x040012DE RID: 4830
			Checkbox = 127,
			// Token: 0x040012DF RID: 4831
			Button = 127
		}

		// Token: 0x020002AE RID: 686
		[Flags]
		protected internal enum AttributeControlTypes
		{
			// Token: 0x040012E1 RID: 4833
			None = 0,
			// Token: 0x040012E2 RID: 4834
			Edit = 1,
			// Token: 0x040012E3 RID: 4835
			MultiValued = 2,
			// Token: 0x040012E4 RID: 4836
			Checkbox = 4,
			// Token: 0x040012E5 RID: 4837
			Listbox = 8
		}

		// Token: 0x020002AF RID: 687
		internal enum ControlTypes
		{
			// Token: 0x040012E7 RID: 4839
			Label,
			// Token: 0x040012E8 RID: 4840
			Edit,
			// Token: 0x040012E9 RID: 4841
			Listbox,
			// Token: 0x040012EA RID: 4842
			Checkbox = 5,
			// Token: 0x040012EB RID: 4843
			Groupbox,
			// Token: 0x040012EC RID: 4844
			Button,
			// Token: 0x040012ED RID: 4845
			Page,
			// Token: 0x040012EE RID: 4846
			MultiValuedListbox = 11,
			// Token: 0x040012EF RID: 4847
			MultiValuedDropdown
		}

		// Token: 0x020002B0 RID: 688
		internal enum MapiPrefix : uint
		{
			// Token: 0x040012F1 RID: 4849
			None,
			// Token: 0x040012F2 RID: 4850
			Checkbox = 11U,
			// Token: 0x040012F3 RID: 4851
			Edit = 30U,
			// Token: 0x040012F4 RID: 4852
			Listbox = 13U,
			// Token: 0x040012F5 RID: 4853
			Button = 13U,
			// Token: 0x040012F6 RID: 4854
			MultiValued = 4126U
		}

		// Token: 0x020002B1 RID: 689
		[Flags]
		internal enum ControlFlags : uint
		{
			// Token: 0x040012F8 RID: 4856
			Multiline = 1U,
			// Token: 0x040012F9 RID: 4857
			HorizontalScroll = 1U,
			// Token: 0x040012FA RID: 4858
			ReadOnly = 2U,
			// Token: 0x040012FB RID: 4859
			VerticalScroll = 2U,
			// Token: 0x040012FC RID: 4860
			ConfirmationRequired = 4U,
			// Token: 0x040012FD RID: 4861
			UseSystemPasswordChar = 16U,
			// Token: 0x040012FE RID: 4862
			AcceptDBCS = 32U
		}
	}
}
