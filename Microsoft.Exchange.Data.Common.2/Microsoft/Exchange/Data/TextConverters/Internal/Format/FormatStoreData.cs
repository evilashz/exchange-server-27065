using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x020002B3 RID: 691
	internal class FormatStoreData
	{
		// Token: 0x040020FF RID: 8447
		internal const int GlobalStringValuesCount = 2;

		// Token: 0x04002100 RID: 8448
		internal const int GlobalMultiValuesCount = 1;

		// Token: 0x04002101 RID: 8449
		internal const int GlobalStylesCount = 26;

		// Token: 0x04002102 RID: 8450
		internal const int GlobalInheritanceMasksCount = 6;

		// Token: 0x04002103 RID: 8451
		internal static FormatStore.StringValueEntry[] GlobalStringValues = new FormatStore.StringValueEntry[]
		{
			default(FormatStore.StringValueEntry),
			new FormatStore.StringValueEntry("Courier New")
		};

		// Token: 0x04002104 RID: 8452
		internal static FormatStore.MultiValueEntry[] GlobalMultiValues = new FormatStore.MultiValueEntry[]
		{
			default(FormatStore.MultiValueEntry)
		};

		// Token: 0x04002105 RID: 8453
		internal static FormatStore.StyleEntry[] GlobalStyles = new FormatStore.StyleEntry[]
		{
			default(FormatStore.StyleEntry),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(0U, 0U), null),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3072U), new PropertyBitMask(0U, 0U), null),
			new FormatStore.StyleEntry(new FlagProperties(12U), new PropertyBitMask(0U, 0U), null),
			new FormatStore.StyleEntry(new FlagProperties(48U), new PropertyBitMask(0U, 0U), null),
			new FormatStore.StyleEntry(new FlagProperties(2U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(192U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(768U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(6U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -1)),
				new Property(PropertyId.FontFace, new PropertyValue(PropertyType.String, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(4U, 0U), new Property[]
			{
				new Property(PropertyId.FontFace, new PropertyValue(PropertyType.String, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(512U, 0U), new Property[]
			{
				new Property(PropertyId.UnicodeBiDi, new PropertyValue(PropertyType.Enum, 2))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3145728U), new PropertyBitMask(0U, 0U), null),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(8U, 0U), new Property[]
			{
				new Property(PropertyId.TextAlignment, new PropertyValue(PropertyType.Enum, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3932160U), new PropertyBitMask(6U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -1)),
				new Property(PropertyId.FontFace, new PropertyValue(PropertyType.String, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3932160U), new PropertyBitMask(6U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.RelHtmlFontUnits, -2)),
				new Property(PropertyId.FontFace, new PropertyValue(PropertyType.String, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(40960U, 0U), new Property[]
			{
				new Property(PropertyId.RightMargin, new PropertyValue(PropertyType.AbsLength, 4800)),
				new Property(PropertyId.LeftMargin, new PropertyValue(PropertyType.AbsLength, 4800))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 6))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 5))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 4))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 3))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 2))
			}),
			new FormatStore.StyleEntry(new FlagProperties(3U), new PropertyBitMask(2U, 0U), new Property[]
			{
				new Property(PropertyId.FontSize, new PropertyValue(PropertyType.HtmlFontUnits, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(0U, 6U), new Property[]
			{
				new Property(PropertyId.ListStyle, new PropertyValue(PropertyType.Enum, 2)),
				new Property(PropertyId.ListStart, new PropertyValue(PropertyType.Integer, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(0U, 2U), new Property[]
			{
				new Property(PropertyId.ListStyle, new PropertyValue(PropertyType.Enum, 1))
			}),
			new FormatStore.StyleEntry(new FlagProperties(0U), new PropertyBitMask(20480U, 0U), new Property[]
			{
				new Property(PropertyId.Margins, new PropertyValue(PropertyType.AbsLength, 0)),
				new Property(PropertyId.BottomMargin, new PropertyValue(PropertyType.AbsLength, 0))
			})
		};

		// Token: 0x04002106 RID: 8454
		internal static FormatStore.InheritaceMask[] GlobalInheritanceMasks = new FormatStore.InheritaceMask[]
		{
			new FormatStore.InheritaceMask(new FlagProperties(0U), new PropertyBitMask(0U, 0U)),
			new FormatStore.InheritaceMask(new FlagProperties(uint.MaxValue), new PropertyBitMask(uint.MaxValue, uint.MaxValue)),
			new FormatStore.InheritaceMask(new FlagProperties(16777215U), new PropertyBitMask(351U, 2U)),
			new FormatStore.InheritaceMask(new FlagProperties(12779520U), new PropertyBitMask(324U, 2U)),
			new FormatStore.InheritaceMask(new FlagProperties(268435455U), new PropertyBitMask(1375U, 32738U)),
			new FormatStore.InheritaceMask(new FlagProperties(16777215U), new PropertyBitMask(327U, 0U))
		};

		// Token: 0x020002B4 RID: 692
		public static class DefaultInheritanceMaskIndex
		{
			// Token: 0x04002107 RID: 8455
			public const int Null = 0;

			// Token: 0x04002108 RID: 8456
			public const int Any = 1;

			// Token: 0x04002109 RID: 8457
			public const int Normal = 2;

			// Token: 0x0400210A RID: 8458
			public const int Table = 3;

			// Token: 0x0400210B RID: 8459
			public const int TableRow = 4;

			// Token: 0x0400210C RID: 8460
			public const int Text = 5;
		}
	}
}
