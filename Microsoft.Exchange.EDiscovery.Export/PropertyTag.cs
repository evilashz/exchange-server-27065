using System;
using System.Globalization;

namespace Microsoft.Exchange.EDiscovery.Export
{
	// Token: 0x02000071 RID: 113
	internal struct PropertyTag
	{
		// Token: 0x17000165 RID: 357
		// (get) Token: 0x060007A6 RID: 1958 RVA: 0x0001BBCD File Offset: 0x00019DCD
		// (set) Token: 0x060007A7 RID: 1959 RVA: 0x0001BBD5 File Offset: 0x00019DD5
		public uint Value
		{
			get
			{
				return this.tagValue;
			}
			set
			{
				this.tagValue = value;
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060007A8 RID: 1960 RVA: 0x0001BBE0 File Offset: 0x00019DE0
		public uint NormalizedValueForPst
		{
			get
			{
				PropertyTag.PropertyType propertyType = PropertyTag.PropertyType.Multivalued & this.Type;
				if ((ushort)(this.Type & PropertyTag.PropertyType.Unicode) == 33968)
				{
					propertyType |= PropertyTag.PropertyType.String;
				}
				else if ((ushort)(this.Type & PropertyTag.PropertyType.Ascii) == 34020)
				{
					propertyType |= PropertyTag.PropertyType.AnsiString;
				}
				else
				{
					propertyType = this.Type;
				}
				return (uint)((int)this.Id << 16 | (int)propertyType);
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060007A9 RID: 1961 RVA: 0x0001BC45 File Offset: 0x00019E45
		public ushort Id
		{
			get
			{
				return (ushort)((this.Value & 4294901760U) >> 16);
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060007AA RID: 1962 RVA: 0x0001BC57 File Offset: 0x00019E57
		public PropertyTag.PropertyType Type
		{
			get
			{
				return (PropertyTag.PropertyType)(this.Value & 65535U);
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060007AB RID: 1963 RVA: 0x0001BC68 File Offset: 0x00019E68
		public bool IsFixedSize
		{
			get
			{
				PropertyTag.PropertyType propertyType = this.Type & ~PropertyTag.PropertyType.Multivalued;
				switch (propertyType)
				{
				case PropertyTag.PropertyType.Unspecified:
				case PropertyTag.PropertyType.Null:
				case PropertyTag.PropertyType.Int16:
				case PropertyTag.PropertyType.Int32:
				case PropertyTag.PropertyType.Float:
				case PropertyTag.PropertyType.Double:
				case PropertyTag.PropertyType.Currency:
				case PropertyTag.PropertyType.AppTime:
				case PropertyTag.PropertyType.Error:
				case PropertyTag.PropertyType.Boolean:
				case PropertyTag.PropertyType.Int64:
					break;
				case (PropertyTag.PropertyType)8:
				case (PropertyTag.PropertyType)9:
				case (PropertyTag.PropertyType)12:
				case PropertyTag.PropertyType.Object:
				case (PropertyTag.PropertyType)14:
				case (PropertyTag.PropertyType)15:
				case (PropertyTag.PropertyType)16:
				case (PropertyTag.PropertyType)17:
				case (PropertyTag.PropertyType)18:
				case (PropertyTag.PropertyType)19:
					return false;
				default:
					if (propertyType != PropertyTag.PropertyType.SysTime && propertyType != PropertyTag.PropertyType.ClassId)
					{
						return false;
					}
					break;
				}
				return true;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060007AC RID: 1964 RVA: 0x0001BCEA File Offset: 0x00019EEA
		public bool IsMultivalued
		{
			get
			{
				return (ushort)(this.Type & PropertyTag.PropertyType.Multivalued) == 4096;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060007AD RID: 1965 RVA: 0x0001BD00 File Offset: 0x00019F00
		public bool IsNamedProperty
		{
			get
			{
				return this.Id <= 65534 && this.Id >= 32768;
			}
		}

		// Token: 0x060007AE RID: 1966 RVA: 0x0001BD24 File Offset: 0x00019F24
		public static int GetSizeOfFixedSizeProperty(PropertyTag.PropertyType propType)
		{
			PropertyTag.PropertyType propertyType = propType & ~PropertyTag.PropertyType.Multivalued;
			switch (propertyType)
			{
			case PropertyTag.PropertyType.Unspecified:
			case PropertyTag.PropertyType.Null:
				return 0;
			case PropertyTag.PropertyType.Int16:
			case PropertyTag.PropertyType.Boolean:
				return 2;
			case PropertyTag.PropertyType.Int32:
			case PropertyTag.PropertyType.Float:
			case PropertyTag.PropertyType.Error:
				return 4;
			case PropertyTag.PropertyType.Double:
			case PropertyTag.PropertyType.Currency:
			case PropertyTag.PropertyType.AppTime:
			case PropertyTag.PropertyType.Int64:
				break;
			case (PropertyTag.PropertyType)8:
			case (PropertyTag.PropertyType)9:
			case (PropertyTag.PropertyType)12:
			case PropertyTag.PropertyType.Object:
			case (PropertyTag.PropertyType)14:
			case (PropertyTag.PropertyType)15:
			case (PropertyTag.PropertyType)16:
			case (PropertyTag.PropertyType)17:
			case (PropertyTag.PropertyType)18:
			case (PropertyTag.PropertyType)19:
				goto IL_7A;
			default:
				if (propertyType != PropertyTag.PropertyType.SysTime)
				{
					if (propertyType != PropertyTag.PropertyType.ClassId)
					{
						goto IL_7A;
					}
					return 16;
				}
				break;
			}
			return 8;
			IL_7A:
			throw new ArgumentOutOfRangeException(string.Format(CultureInfo.CurrentCulture, "Mapi property type '{0}' is not a supported fixed size property", new object[]
			{
				propType
			}), null);
		}

		// Token: 0x060007AF RID: 1967 RVA: 0x0001BDD4 File Offset: 0x00019FD4
		public static implicit operator PropertyTag(uint tagValue)
		{
			return new PropertyTag
			{
				Value = tagValue
			};
		}

		// Token: 0x0400029B RID: 667
		private const ushort MinUserDefinedNamed = 32768;

		// Token: 0x0400029C RID: 668
		private const ushort MaxUserDefinedNamed = 65534;

		// Token: 0x0400029D RID: 669
		public static readonly PropertyTag DisplayName = 805371935U;

		// Token: 0x0400029E RID: 670
		public static readonly PropertyTag DisplayTo = 235143199U;

		// Token: 0x0400029F RID: 671
		public static readonly PropertyTag DisplayCc = 235077663U;

		// Token: 0x040002A0 RID: 672
		public static readonly PropertyTag DisplayBcc = 235012127U;

		// Token: 0x040002A1 RID: 673
		public static readonly PropertyTag Importance = 1507331U;

		// Token: 0x040002A2 RID: 674
		public static readonly PropertyTag MessageFlags = 235339779U;

		// Token: 0x040002A3 RID: 675
		public static readonly PropertyTag RecipientType = 202702851U;

		// Token: 0x040002A4 RID: 676
		private uint tagValue;

		// Token: 0x02000072 RID: 114
		public enum ContextPropertyId : ushort
		{
			// Token: 0x040002A6 RID: 678
			PropTagNewAttachment = 16384,
			// Token: 0x040002A7 RID: 679
			PropTagStartEmbed,
			// Token: 0x040002A8 RID: 680
			PropTagEndEmbed,
			// Token: 0x040002A9 RID: 681
			PropTagStartRecipient,
			// Token: 0x040002AA RID: 682
			PropTagEndRecipient,
			// Token: 0x040002AB RID: 683
			PropTagEndAttachment = 16398,
			// Token: 0x040002AC RID: 684
			PropTagFastTransferDel = 16406
		}

		// Token: 0x02000073 RID: 115
		[Flags]
		public enum PropertyType : ushort
		{
			// Token: 0x040002AE RID: 686
			Unspecified = 0,
			// Token: 0x040002AF RID: 687
			Null = 1,
			// Token: 0x040002B0 RID: 688
			Int16 = 2,
			// Token: 0x040002B1 RID: 689
			Int32 = 3,
			// Token: 0x040002B2 RID: 690
			Float = 4,
			// Token: 0x040002B3 RID: 691
			Double = 5,
			// Token: 0x040002B4 RID: 692
			Currency = 6,
			// Token: 0x040002B5 RID: 693
			AppTime = 7,
			// Token: 0x040002B6 RID: 694
			Error = 10,
			// Token: 0x040002B7 RID: 695
			Boolean = 11,
			// Token: 0x040002B8 RID: 696
			Object = 13,
			// Token: 0x040002B9 RID: 697
			Int64 = 20,
			// Token: 0x040002BA RID: 698
			AnsiString = 30,
			// Token: 0x040002BB RID: 699
			String = 31,
			// Token: 0x040002BC RID: 700
			SysTime = 64,
			// Token: 0x040002BD RID: 701
			ClassId = 72,
			// Token: 0x040002BE RID: 702
			ServerEntryId = 251,
			// Token: 0x040002BF RID: 703
			Restriction = 253,
			// Token: 0x040002C0 RID: 704
			Actions = 254,
			// Token: 0x040002C1 RID: 705
			Binary = 258,
			// Token: 0x040002C2 RID: 706
			Multivalued = 4096,
			// Token: 0x040002C3 RID: 707
			Unicode = 33968,
			// Token: 0x040002C4 RID: 708
			Ascii = 34020
		}
	}
}
