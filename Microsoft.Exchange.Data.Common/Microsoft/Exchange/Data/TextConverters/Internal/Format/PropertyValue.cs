using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x02000298 RID: 664
	internal struct PropertyValue
	{
		// Token: 0x06001A2E RID: 6702 RVA: 0x000CF305 File Offset: 0x000CD505
		public PropertyValue(uint rawValue)
		{
			this.rawValue = rawValue;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x000CF30E File Offset: 0x000CD50E
		public PropertyValue(bool value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(value);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x000CF31C File Offset: 0x000CD51C
		public PropertyValue(PropertyType type, int value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x000CF32B File Offset: 0x000CD52B
		public PropertyValue(PropertyType type, uint value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000CF33A File Offset: 0x000CD53A
		public PropertyValue(LengthUnits lengthUnits, float value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(lengthUnits, value);
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x000CF349 File Offset: 0x000CD549
		public PropertyValue(LengthUnits lengthUnits, int value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(lengthUnits, value);
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x000CF358 File Offset: 0x000CD558
		public PropertyValue(PropertyType type, float value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x000CF367 File Offset: 0x000CD567
		public PropertyValue(RGBT color)
		{
			this.rawValue = PropertyValue.ComposeRawValue(color);
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x000CF375 File Offset: 0x000CD575
		public PropertyValue(Enum value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(value);
		}

		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x000CF383 File Offset: 0x000CD583
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x000CF38B File Offset: 0x000CD58B
		public uint RawValue
		{
			get
			{
				return this.rawValue;
			}
			set
			{
				this.rawValue = value;
			}
		}

		// Token: 0x1700069D RID: 1693
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x000CF394 File Offset: 0x000CD594
		public uint RawType
		{
			get
			{
				return this.rawValue & 4160749568U;
			}
		}

		// Token: 0x1700069E RID: 1694
		// (get) Token: 0x06001A3A RID: 6714 RVA: 0x000CF3A2 File Offset: 0x000CD5A2
		public PropertyType Type
		{
			get
			{
				return (PropertyType)((this.rawValue & 4160749568U) >> 27);
			}
		}

		// Token: 0x1700069F RID: 1695
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x000CF3B4 File Offset: 0x000CD5B4
		public int Value
		{
			get
			{
				return (int)((int)(this.rawValue & 134217727U) << 5) >> 5;
			}
		}

		// Token: 0x170006A0 RID: 1696
		// (get) Token: 0x06001A3C RID: 6716 RVA: 0x000CF3C6 File Offset: 0x000CD5C6
		public uint UnsignedValue
		{
			get
			{
				return this.rawValue & 134217727U;
			}
		}

		// Token: 0x170006A1 RID: 1697
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x000CF3D4 File Offset: 0x000CD5D4
		public bool IsNull
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Null);
			}
		}

		// Token: 0x170006A2 RID: 1698
		// (get) Token: 0x06001A3E RID: 6718 RVA: 0x000CF3E4 File Offset: 0x000CD5E4
		public bool IsCalculated
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Calculated);
			}
		}

		// Token: 0x170006A3 RID: 1699
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000CF3F4 File Offset: 0x000CD5F4
		public bool IsBool
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Bool);
			}
		}

		// Token: 0x170006A4 RID: 1700
		// (get) Token: 0x06001A40 RID: 6720 RVA: 0x000CF404 File Offset: 0x000CD604
		public bool IsEnum
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Enum);
			}
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x000CF414 File Offset: 0x000CD614
		public bool IsString
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.String);
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001A42 RID: 6722 RVA: 0x000CF424 File Offset: 0x000CD624
		public bool IsMultiValue
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.MultiValue);
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001A43 RID: 6723 RVA: 0x000CF434 File Offset: 0x000CD634
		public bool IsRefCountedHandle
		{
			get
			{
				return this.IsString || this.IsMultiValue;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001A44 RID: 6724 RVA: 0x000CF446 File Offset: 0x000CD646
		public bool IsColor
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Color);
			}
		}

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x000CF456 File Offset: 0x000CD656
		public bool IsInteger
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Integer);
			}
		}

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x06001A46 RID: 6726 RVA: 0x000CF466 File Offset: 0x000CD666
		public bool IsFractional
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Fractional);
			}
		}

		// Token: 0x170006AB RID: 1707
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x000CF476 File Offset: 0x000CD676
		public bool IsPercentage
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Percentage);
			}
		}

		// Token: 0x170006AC RID: 1708
		// (get) Token: 0x06001A48 RID: 6728 RVA: 0x000CF487 File Offset: 0x000CD687
		public bool IsAbsLength
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.AbsLength);
			}
		}

		// Token: 0x170006AD RID: 1709
		// (get) Token: 0x06001A49 RID: 6729 RVA: 0x000CF498 File Offset: 0x000CD698
		public bool IsPixels
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Pixels);
			}
		}

		// Token: 0x170006AE RID: 1710
		// (get) Token: 0x06001A4A RID: 6730 RVA: 0x000CF4A9 File Offset: 0x000CD6A9
		public bool IsEms
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Ems);
			}
		}

		// Token: 0x170006AF RID: 1711
		// (get) Token: 0x06001A4B RID: 6731 RVA: 0x000CF4BA File Offset: 0x000CD6BA
		public bool IsExs
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Exs);
			}
		}

		// Token: 0x170006B0 RID: 1712
		// (get) Token: 0x06001A4C RID: 6732 RVA: 0x000CF4CB File Offset: 0x000CD6CB
		public bool IsMilliseconds
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Milliseconds);
			}
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x06001A4D RID: 6733 RVA: 0x000CF4DC File Offset: 0x000CD6DC
		public bool IsKHz
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.kHz);
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06001A4E RID: 6734 RVA: 0x000CF4ED File Offset: 0x000CD6ED
		public bool IsDegrees
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.Degrees);
			}
		}

		// Token: 0x170006B3 RID: 1715
		// (get) Token: 0x06001A4F RID: 6735 RVA: 0x000CF4FE File Offset: 0x000CD6FE
		public bool IsHtmlFontUnits
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.HtmlFontUnits);
			}
		}

		// Token: 0x170006B4 RID: 1716
		// (get) Token: 0x06001A50 RID: 6736 RVA: 0x000CF50F File Offset: 0x000CD70F
		public bool IsRelativeHtmlFontUnits
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.RelHtmlFontUnits);
			}
		}

		// Token: 0x170006B5 RID: 1717
		// (get) Token: 0x06001A51 RID: 6737 RVA: 0x000CF520 File Offset: 0x000CD720
		public bool IsAbsRelLength
		{
			get
			{
				return this.RawType == PropertyValue.GetRawType(PropertyType.AbsLength) || this.RawType == PropertyValue.GetRawType(PropertyType.RelLength) || this.RawType == PropertyValue.GetRawType(PropertyType.Pixels);
			}
		}

		// Token: 0x170006B6 RID: 1718
		// (get) Token: 0x06001A52 RID: 6738 RVA: 0x000CF551 File Offset: 0x000CD751
		public int StringHandle
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006B7 RID: 1719
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x000CF559 File Offset: 0x000CD759
		public int MultiValueHandle
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006B8 RID: 1720
		// (get) Token: 0x06001A54 RID: 6740 RVA: 0x000CF561 File Offset: 0x000CD761
		public bool Bool
		{
			get
			{
				return this.UnsignedValue != 0U;
			}
		}

		// Token: 0x170006B9 RID: 1721
		// (get) Token: 0x06001A55 RID: 6741 RVA: 0x000CF56F File Offset: 0x000CD76F
		public int Enum
		{
			get
			{
				return (int)this.UnsignedValue;
			}
		}

		// Token: 0x170006BA RID: 1722
		// (get) Token: 0x06001A56 RID: 6742 RVA: 0x000CF577 File Offset: 0x000CD777
		public RGBT Color
		{
			get
			{
				return new RGBT(this.UnsignedValue);
			}
		}

		// Token: 0x170006BB RID: 1723
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x000CF584 File Offset: 0x000CD784
		public float Percentage
		{
			get
			{
				return (float)this.Value / 10000f;
			}
		}

		// Token: 0x170006BC RID: 1724
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x000CF593 File Offset: 0x000CD793
		public int Percentage10K
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006BD RID: 1725
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x000CF59B File Offset: 0x000CD79B
		public float Fractional
		{
			get
			{
				return (float)this.Value / 10000f;
			}
		}

		// Token: 0x170006BE RID: 1726
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x000CF5AA File Offset: 0x000CD7AA
		public int Integer
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006BF RID: 1727
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x000CF5B2 File Offset: 0x000CD7B2
		public int Milliseconds
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006C0 RID: 1728
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x000CF5BA File Offset: 0x000CD7BA
		public int KHz
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006C1 RID: 1729
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x000CF5C2 File Offset: 0x000CD7C2
		public int Degrees
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x000CF5CA File Offset: 0x000CD7CA
		public int BaseUnits
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x000CF5D2 File Offset: 0x000CD7D2
		public float Twips
		{
			get
			{
				return (float)this.Value / 8f;
			}
		}

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x000CF5E1 File Offset: 0x000CD7E1
		public int TwipsInteger
		{
			get
			{
				return this.Value / 8;
			}
		}

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x000CF5EB File Offset: 0x000CD7EB
		public float Points
		{
			get
			{
				return (float)this.Value / 160f;
			}
		}

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001A62 RID: 6754 RVA: 0x000CF5FA File Offset: 0x000CD7FA
		public int PointsInteger
		{
			get
			{
				return this.Value / 160;
			}
		}

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x000CF608 File Offset: 0x000CD808
		public int PointsInteger160
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006C8 RID: 1736
		// (get) Token: 0x06001A64 RID: 6756 RVA: 0x000CF610 File Offset: 0x000CD810
		public float Picas
		{
			get
			{
				return (float)this.Value / 1920f;
			}
		}

		// Token: 0x170006C9 RID: 1737
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x000CF61F File Offset: 0x000CD81F
		public float Inches
		{
			get
			{
				return (float)this.Value / 11520f;
			}
		}

		// Token: 0x170006CA RID: 1738
		// (get) Token: 0x06001A66 RID: 6758 RVA: 0x000CF62E File Offset: 0x000CD82E
		public float Centimeters
		{
			get
			{
				return (float)this.Value / 4535.433f;
			}
		}

		// Token: 0x170006CB RID: 1739
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x000CF63D File Offset: 0x000CD83D
		public int MillimetersInteger
		{
			get
			{
				return this.Value / 454;
			}
		}

		// Token: 0x170006CC RID: 1740
		// (get) Token: 0x06001A68 RID: 6760 RVA: 0x000CF64B File Offset: 0x000CD84B
		public float Millimeters
		{
			get
			{
				return (float)this.Value / 453.5433f;
			}
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x000CF65A File Offset: 0x000CD85A
		public int HtmlFontUnits
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006CE RID: 1742
		// (get) Token: 0x06001A6A RID: 6762 RVA: 0x000CF662 File Offset: 0x000CD862
		public float Pixels
		{
			get
			{
				return (float)this.Value / 96f;
			}
		}

		// Token: 0x170006CF RID: 1743
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x000CF671 File Offset: 0x000CD871
		public int PixelsInteger
		{
			get
			{
				return this.Value / 96;
			}
		}

		// Token: 0x170006D0 RID: 1744
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x000CF67C File Offset: 0x000CD87C
		public int PixelsInteger96
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006D1 RID: 1745
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x000CF684 File Offset: 0x000CD884
		public float Ems
		{
			get
			{
				return (float)this.Value / 160f;
			}
		}

		// Token: 0x170006D2 RID: 1746
		// (get) Token: 0x06001A6E RID: 6766 RVA: 0x000CF693 File Offset: 0x000CD893
		public int EmsInteger
		{
			get
			{
				return this.Value / 160;
			}
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x000CF6A1 File Offset: 0x000CD8A1
		public int EmsInteger160
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06001A70 RID: 6768 RVA: 0x000CF6A9 File Offset: 0x000CD8A9
		public float Exs
		{
			get
			{
				return (float)this.Value / 160f;
			}
		}

		// Token: 0x170006D5 RID: 1749
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x000CF6B8 File Offset: 0x000CD8B8
		public int ExsInteger
		{
			get
			{
				return this.Value / 160;
			}
		}

		// Token: 0x170006D6 RID: 1750
		// (get) Token: 0x06001A72 RID: 6770 RVA: 0x000CF6C6 File Offset: 0x000CD8C6
		public int ExsInteger160
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x170006D7 RID: 1751
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x000CF6CE File Offset: 0x000CD8CE
		public int RelativeHtmlFontUnits
		{
			get
			{
				return this.Value;
			}
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000CF6D6 File Offset: 0x000CD8D6
		public static uint GetRawType(PropertyType type)
		{
			return (uint)((uint)type << 27);
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000CF6DC File Offset: 0x000CD8DC
		public static bool operator ==(PropertyValue x, PropertyValue y)
		{
			return x.rawValue == y.rawValue;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000CF6EE File Offset: 0x000CD8EE
		public static bool operator !=(PropertyValue x, PropertyValue y)
		{
			return x.rawValue != y.rawValue;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000CF703 File Offset: 0x000CD903
		public void Set(uint rawValue)
		{
			this.rawValue = rawValue;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000CF70C File Offset: 0x000CD90C
		public void Set(bool value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(value);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000CF71A File Offset: 0x000CD91A
		public void Set(PropertyType type, int value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000CF729 File Offset: 0x000CD929
		public void Set(PropertyType type, uint value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000CF738 File Offset: 0x000CD938
		public void Set(LengthUnits lengthUnits, float value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(lengthUnits, value);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000CF747 File Offset: 0x000CD947
		public void Set(PropertyType type, float value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(type, value);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000CF756 File Offset: 0x000CD956
		public void Set(RGBT color)
		{
			this.rawValue = PropertyValue.ComposeRawValue(color);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000CF764 File Offset: 0x000CD964
		public void Set(Enum value)
		{
			this.rawValue = PropertyValue.ComposeRawValue(value);
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000CF774 File Offset: 0x000CD974
		public override string ToString()
		{
			switch (this.Type)
			{
			case PropertyType.Null:
				return "null";
			case PropertyType.Calculated:
				return "calculated";
			case PropertyType.Bool:
				return this.Bool.ToString();
			case PropertyType.String:
				return "string: " + this.StringHandle.ToString();
			case PropertyType.MultiValue:
				return "multi: " + this.MultiValueHandle.ToString();
			case PropertyType.Enum:
				return "enum: " + this.Enum.ToString();
			case PropertyType.Color:
				return "color: " + this.Color.ToString();
			case PropertyType.Integer:
				return this.Integer.ToString() + " (integer)";
			case PropertyType.Fractional:
				return this.Fractional.ToString() + " (fractional)";
			case PropertyType.Percentage:
				return this.Percentage.ToString() + "%";
			case PropertyType.AbsLength:
				return string.Concat(new string[]
				{
					this.Points.ToString(),
					"pt (",
					this.Inches.ToString(),
					"in, ",
					this.Millimeters.ToString(),
					"mm) (abs)"
				});
			case PropertyType.RelLength:
				return string.Concat(new string[]
				{
					this.Points.ToString(),
					"pt (",
					this.Inches.ToString(),
					"in, ",
					this.Millimeters.ToString(),
					"mm) (rel)"
				});
			case PropertyType.Pixels:
				return this.Pixels.ToString() + "px";
			case PropertyType.Ems:
				return this.Ems.ToString() + "em";
			case PropertyType.Exs:
				return this.Exs.ToString() + "ex";
			case PropertyType.HtmlFontUnits:
				return this.HtmlFontUnits.ToString() + " (html font units)";
			case PropertyType.RelHtmlFontUnits:
				return this.RelativeHtmlFontUnits.ToString() + " (relative html font units)";
			case PropertyType.Multiple:
				return this.Integer.ToString() + "*";
			case PropertyType.Milliseconds:
				return this.Milliseconds.ToString() + "ms";
			case PropertyType.kHz:
				return this.KHz.ToString() + "kHz";
			case PropertyType.Degrees:
				return this.Degrees.ToString() + "deg";
			default:
				return "unknown value type";
			}
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000CFA73 File Offset: 0x000CDC73
		public override bool Equals(object obj)
		{
			return obj is PropertyValue && this.rawValue == ((PropertyValue)obj).rawValue;
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000CFA92 File Offset: 0x000CDC92
		public override int GetHashCode()
		{
			return (int)this.rawValue;
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000CFA9A File Offset: 0x000CDC9A
		internal static int ConvertHtmlFontUnitsToTwips(int nHtmlSize)
		{
			nHtmlSize = Math.Max(1, Math.Min(7, nHtmlSize));
			return PropertyValue.sizesInTwips[nHtmlSize - 1];
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000CFAB4 File Offset: 0x000CDCB4
		internal static int ConvertTwipsToHtmlFontUnits(int twips)
		{
			for (int i = 0; i < PropertyValue.maxSizesInTwips.Length; i++)
			{
				if (twips <= PropertyValue.maxSizesInTwips[i])
				{
					return i + 1;
				}
			}
			return PropertyValue.maxSizesInTwips.Length + 1;
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000CFAEA File Offset: 0x000CDCEA
		private static uint ComposeRawValue(bool value)
		{
			return PropertyValue.GetRawType(PropertyType.Bool) | (value ? 1U : 0U);
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000CFAFA File Offset: 0x000CDCFA
		private static uint ComposeRawValue(PropertyType type, int value)
		{
			return (uint)((int)((int)type << 27) | (value & 134217727));
		}

		// Token: 0x06001A86 RID: 6790 RVA: 0x000CFB08 File Offset: 0x000CDD08
		private static uint ComposeRawValue(PropertyType type, uint value)
		{
			return (uint)((uint)type << 27) | value;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000CFB10 File Offset: 0x000CDD10
		private static uint ComposeRawValue(LengthUnits lengthUnits, float len)
		{
			switch (lengthUnits)
			{
			case LengthUnits.BaseUnits:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)len & 134217727U);
			case LengthUnits.Twips:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 8f) & 134217727U);
			case LengthUnits.Points:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 160f) & 134217727U);
			case LengthUnits.Picas:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 1920f) & 134217727U);
			case LengthUnits.Inches:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 11520f) & 134217727U);
			case LengthUnits.Centimeters:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 4535.433f) & 134217727U);
			case LengthUnits.Millimeters:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | ((uint)(len * 453.5433f) & 134217727U);
			case LengthUnits.HtmlFontUnits:
				return PropertyValue.GetRawType(PropertyType.HtmlFontUnits) | ((uint)len & 134217727U);
			case LengthUnits.Pixels:
				return PropertyValue.GetRawType(PropertyType.Pixels) | ((uint)(len * 96f) & 134217727U);
			case LengthUnits.Ems:
				return PropertyValue.GetRawType(PropertyType.Ems) | ((uint)(len * 160f) & 134217727U);
			case LengthUnits.Exs:
				return PropertyValue.GetRawType(PropertyType.Exs) | ((uint)(len * 160f) & 134217727U);
			case LengthUnits.RelativeHtmlFontUnits:
				return PropertyValue.GetRawType(PropertyType.RelHtmlFontUnits) | (uint)((int)len & 134217727);
			case LengthUnits.Percents:
				return PropertyValue.GetRawType(PropertyType.Percentage) | ((uint)len & 134217727U);
			default:
				return 0U;
			}
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000CFC74 File Offset: 0x000CDE74
		private static uint ComposeRawValue(LengthUnits lengthUnits, int len)
		{
			switch (lengthUnits)
			{
			case LengthUnits.BaseUnits:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len & 134217727);
			case LengthUnits.Twips:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 8 & 134217727);
			case LengthUnits.Points:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 160 & 134217727);
			case LengthUnits.Picas:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 1920 & 134217727);
			case LengthUnits.Inches:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 11520 & 134217727);
			case LengthUnits.Centimeters:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 4535 & 134217727);
			case LengthUnits.Millimeters:
				return PropertyValue.GetRawType(PropertyType.AbsLength) | (uint)(len * 453 & 134217727);
			case LengthUnits.HtmlFontUnits:
				return PropertyValue.GetRawType(PropertyType.HtmlFontUnits) | (uint)(len & 134217727);
			case LengthUnits.Pixels:
				return PropertyValue.GetRawType(PropertyType.Pixels) | (uint)(len * 96 & 134217727);
			case LengthUnits.Ems:
				return PropertyValue.GetRawType(PropertyType.Ems) | (uint)(len * 160 & 134217727);
			case LengthUnits.Exs:
				return PropertyValue.GetRawType(PropertyType.Exs) | (uint)(len * 160 & 134217727);
			case LengthUnits.RelativeHtmlFontUnits:
				return PropertyValue.GetRawType(PropertyType.RelHtmlFontUnits) | (uint)(len & 134217727);
			case LengthUnits.Percents:
				return PropertyValue.GetRawType(PropertyType.Percentage) | (uint)(len & 134217727);
			default:
				return 0U;
			}
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000CFDC2 File Offset: 0x000CDFC2
		private static uint ComposeRawValue(PropertyType type, float value)
		{
			return PropertyValue.GetRawType(type) | ((uint)(value * 10000f) & 134217727U);
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000CFDD9 File Offset: 0x000CDFD9
		private static uint ComposeRawValue(RGBT color)
		{
			return PropertyValue.GetRawType(PropertyType.Color) | (color.RawValue & 134217727U);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000CFDEF File Offset: 0x000CDFEF
		private static uint ComposeRawValue(Enum value)
		{
			return PropertyValue.GetRawType(PropertyType.Enum) | (Convert.ToUInt32(value) & 134217727U);
		}

		// Token: 0x0400204D RID: 8269
		public const int ValueMax = 67108863;

		// Token: 0x0400204E RID: 8270
		public const int ValueMin = -67108863;

		// Token: 0x0400204F RID: 8271
		private const uint TypeMask = 4160749568U;

		// Token: 0x04002050 RID: 8272
		private const int TypeShift = 27;

		// Token: 0x04002051 RID: 8273
		private const uint ValueMask = 134217727U;

		// Token: 0x04002052 RID: 8274
		private const int ValueShift = 5;

		// Token: 0x04002053 RID: 8275
		public static readonly PropertyValue Null = default(PropertyValue);

		// Token: 0x04002054 RID: 8276
		public static readonly PropertyValue True = new PropertyValue(true);

		// Token: 0x04002055 RID: 8277
		public static readonly PropertyValue False = new PropertyValue(false);

		// Token: 0x04002056 RID: 8278
		private static readonly int[] sizesInTwips = new int[]
		{
			151,
			200,
			240,
			271,
			360,
			480,
			720
		};

		// Token: 0x04002057 RID: 8279
		private static readonly int[] maxSizesInTwips = new int[]
		{
			160,
			220,
			260,
			320,
			420,
			620
		};

		// Token: 0x04002058 RID: 8280
		private uint rawValue;
	}
}
