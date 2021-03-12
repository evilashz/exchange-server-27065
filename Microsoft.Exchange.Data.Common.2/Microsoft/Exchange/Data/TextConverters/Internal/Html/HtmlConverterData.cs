using System;
using Microsoft.Exchange.Data.TextConverters.Internal.Format;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E4 RID: 484
	internal static class HtmlConverterData
	{
		// Token: 0x04001460 RID: 5216
		public static HtmlTagInstruction[] tagInstructions = new HtmlTagInstruction[]
		{
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.HyperLink, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Href, PropertyId.HyperlinkUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Target, PropertyId.HyperlinkTarget, HtmlConverterData.PropertyValueParsingMethods.ParseTarget),
				new HtmlAttributeInstruction(HtmlNameIndex.Id, PropertyId.Id, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.Area, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Href, PropertyId.HyperlinkUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Target, PropertyId.HyperlinkTarget, HtmlConverterData.PropertyValueParsingMethods.ParseTarget),
				new HtmlAttributeInstruction(HtmlNameIndex.Alt, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage),
				new HtmlAttributeInstruction(HtmlNameIndex.Shape, PropertyId.Shape, HtmlConverterData.PropertyValueParsingMethods.ParseAreaShape),
				new HtmlAttributeInstruction(HtmlNameIndex.Coords, PropertyId.Coords, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 1, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.BaseFont, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Face, PropertyId.FontFace, HtmlConverterData.PropertyValueParsingMethods.ParseFontFace),
				new HtmlAttributeInstruction(HtmlNameIndex.Size, PropertyId.FontSize, HtmlConverterData.PropertyValueParsingMethods.ParseFontSize),
				new HtmlAttributeInstruction(HtmlNameIndex.Color, PropertyId.FontColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor)
			}),
			new HtmlTagInstruction(FormatContainerType.Inline, 11, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 2, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.BlockQuote, 16, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.BGColor, PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.Button, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Value, PropertyId.QuotingLevelDelta, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.TableCaption, 13, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseBlockAlignment)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 13, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 9, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.TableColumn, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Span, PropertyId.NumColumns, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Valign, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseVerticalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.TableColumnGroup, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Span, PropertyId.NumColumns, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Valign, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseVerticalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.Block, 25, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 3, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.List, 24, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage),
				new HtmlAttributeInstruction(HtmlNameIndex.Id, PropertyId.Id, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 25, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.FieldSet, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Face, PropertyId.FontFace, HtmlConverterData.PropertyValueParsingMethods.ParseFontFace),
				new HtmlAttributeInstruction(HtmlNameIndex.Size, PropertyId.FontSize, HtmlConverterData.PropertyValueParsingMethods.ParseFontSize),
				new HtmlAttributeInstruction(HtmlNameIndex.Color, PropertyId.FontColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Form, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Action, PropertyId.HyperlinkUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.EncType, PropertyId.ImageUrl, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Accept, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.AcceptCharset, PropertyId.QuotingLevelDelta, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.Block, 17, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 18, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 19, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 20, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 21, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 22, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.HorizontalLine, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Size, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Color, PropertyId.FontColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseHorizontalAlignment)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseBlockAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Src, PropertyId.ImageUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl)
			}),
			new HtmlTagInstruction(FormatContainerType.Image, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseBlockAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Src, PropertyId.ImageUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Border, PropertyId.TableBorder, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Alt, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage),
				new HtmlAttributeInstruction(HtmlNameIndex.UseMap, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl)
			}),
			new HtmlTagInstruction(FormatContainerType.Image, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseBlockAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Src, PropertyId.ImageUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Border, PropertyId.TableBorder, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Alt, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage),
				new HtmlAttributeInstruction(HtmlNameIndex.UseMap, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl)
			}),
			new HtmlTagInstruction(FormatContainerType.Input, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.ReadOnly, PropertyId.Overloaded1, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Checked, PropertyId.Overloaded3, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Value, PropertyId.QuotingLevelDelta, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Size, PropertyId.TableFrame, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.MaxLength, PropertyId.TableRules, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Src, PropertyId.ImageUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Alt, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 5, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Prompt, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 9, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Label, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.For, PropertyId.HyperlinkUrl, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Legend, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseBlockAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.ListItem, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Start, PropertyId.ListStart, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.Block, 15, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Map, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.List, 24, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 12, 2, null),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.List, 23, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Start, PropertyId.ListStart, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.OptionGroup, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Label, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Option, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Selected, PropertyId.Overloaded1, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Label, PropertyId.ImageAltText, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Value, PropertyId.QuotingLevelDelta, HtmlConverterData.PropertyValueParsingMethods.ParseStringProperty),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.Block, 14, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Block, 14, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 3, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 9, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.Select, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.Multiple, PropertyId.Overloaded1, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 6, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 3, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 1, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 7, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 8, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.Table, 0, 3, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseHorizontalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Border, PropertyId.TableBorder, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Frame, PropertyId.TableFrame, HtmlConverterData.PropertyValueParsingMethods.ParseTableFrame),
				new HtmlAttributeInstruction(HtmlNameIndex.Rules, PropertyId.TableRules, HtmlConverterData.PropertyValueParsingMethods.ParseTableRules),
				new HtmlAttributeInstruction(HtmlNameIndex.BGColor, PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.CellSpacing, PropertyId.TableCellSpacing, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.CellPadding, PropertyId.TableCellPadding, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.TableExtraContent, 0, 2, null),
			new HtmlTagInstruction(FormatContainerType.TableCell, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.ColSpan, PropertyId.NumColumns, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.RowSpan, PropertyId.NumRows, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Valign, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseVerticalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.NoWrap, PropertyId.TableCellNoWrap, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.BGColor, PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.TextArea, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Name, PropertyId.BookmarkName, HtmlConverterData.PropertyValueParsingMethods.ParseUrl),
				new HtmlAttributeInstruction(HtmlNameIndex.ReadOnly, PropertyId.Overloaded1, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Disabled, PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.Cols, PropertyId.NumColumns, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Rows, PropertyId.NumRows, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.TableCell, 0, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Width, PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.ColSpan, PropertyId.NumColumns, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.RowSpan, PropertyId.NumRows, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeInteger),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Valign, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseVerticalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.NoWrap, PropertyId.TableCellNoWrap, HtmlConverterData.PropertyValueParsingMethods.ParseBooleanAttribute),
				new HtmlAttributeInstruction(HtmlNameIndex.BGColor, PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.TableRow, 0, 4, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Height, PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength),
				new HtmlAttributeInstruction(HtmlNameIndex.Align, PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.Valign, PropertyId.BlockAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseVerticalAlignment),
				new HtmlAttributeInstruction(HtmlNameIndex.BGColor, PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColor),
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 10, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 5, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.List, 24, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			new HtmlTagInstruction(FormatContainerType.PropertyContainer, 4, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			new HtmlTagInstruction(FormatContainerType.Block, 14, 2, new HtmlAttributeInstruction[]
			{
				new HtmlAttributeInstruction(HtmlNameIndex.Dir, PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection),
				new HtmlAttributeInstruction(HtmlNameIndex.Lang, PropertyId.Language, HtmlConverterData.PropertyValueParsingMethods.ParseLanguage)
			}),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction),
			default(HtmlTagInstruction)
		};

		// Token: 0x04001461 RID: 5217
		public static CssPropertyInstruction[] cssPropertyInstructions = new CssPropertyInstruction[]
		{
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Null, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCssWhiteSpace),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BlockAlignment, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCssVerticalAlignment),
			new CssPropertyInstruction(PropertyId.RightBorderWidth, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorder),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.FontFace, HtmlConverterData.PropertyValueParsingMethods.ParseFontFace, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BottomBorderWidth, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorder),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BackColor, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			new CssPropertyInstruction(PropertyId.BorderStyles, HtmlConverterData.PropertyValueParsingMethods.ParseBorderStyle, null),
			new CssPropertyInstruction(PropertyId.TableShowEmptyCells, HtmlConverterData.PropertyValueParsingMethods.ParseEmptyCells, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.TextAlignment, HtmlConverterData.PropertyValueParsingMethods.ParseTextAlignment, null),
			new CssPropertyInstruction(PropertyId.FirstFlag, HtmlConverterData.PropertyValueParsingMethods.ParseFontWeight, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.TableCaptionSideTop, HtmlConverterData.PropertyValueParsingMethods.ParseCaptionSide, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.LeftMargin, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.BorderWidths, HtmlConverterData.PropertyValueParsingMethods.ParseBorderWidth, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.UnicodeBiDi, HtmlConverterData.PropertyValueParsingMethods.ParseUnicodeBiDi, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Paddings, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeNonNegativeLength),
			new CssPropertyInstruction(PropertyId.BottomBorderWidth, HtmlConverterData.PropertyValueParsingMethods.ParseBorderWidth, null),
			new CssPropertyInstruction(PropertyId.Visible, HtmlConverterData.PropertyValueParsingMethods.ParseVisibility, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Overloaded1, HtmlConverterData.PropertyValueParsingMethods.ParseTableLayout, null),
			new CssPropertyInstruction(PropertyId.LeftBorderColor, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Height, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Margins, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeNonNegativeLength),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BottomBorderStyle, HtmlConverterData.PropertyValueParsingMethods.ParseBorderStyle, null),
			new CssPropertyInstruction(PropertyId.BorderColors, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeColor),
			new CssPropertyInstruction(PropertyId.Null, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCssTextDecoration),
			new CssPropertyInstruction(PropertyId.Display, HtmlConverterData.PropertyValueParsingMethods.ParseDisplay, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BottomMargin, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.BorderStyles, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorderStyle),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Null, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeAllBorders),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Width, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.FontColor, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.Overloaded2, HtmlConverterData.PropertyValueParsingMethods.ParseBorderCollapse, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.TableBorderSpacingVertical, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompoundBorderSpacing),
			new CssPropertyInstruction(PropertyId.Null, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCssTextTransform),
			new CssPropertyInstruction(PropertyId.RightBorderWidth, HtmlConverterData.PropertyValueParsingMethods.ParseBorderWidth, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.FirstLineIndent, HtmlConverterData.PropertyValueParsingMethods.ParseLength, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BottomBorderColor, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			new CssPropertyInstruction(PropertyId.RightMargin, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.RightPadding, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.RightBorderStyle, HtmlConverterData.PropertyValueParsingMethods.ParseBorderStyle, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BackColor, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBackground),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BorderWidths, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorderWidth),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BorderColors, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.RightToLeft, HtmlConverterData.PropertyValueParsingMethods.ParseDirection, null),
			new CssPropertyInstruction(PropertyId.SmallCaps, HtmlConverterData.PropertyValueParsingMethods.ParseFontVariant, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.FontSize, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeFont),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.RightBorderColor, HtmlConverterData.PropertyValueParsingMethods.ParseColorCss, null),
			new CssPropertyInstruction(PropertyId.Italic, HtmlConverterData.PropertyValueParsingMethods.ParseFontStyle, null),
			new CssPropertyInstruction(PropertyId.Margins, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.LeftBorderWidth, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorder),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.LeftBorderWidth, HtmlConverterData.PropertyValueParsingMethods.ParseBorderWidth, null),
			new CssPropertyInstruction(PropertyId.BottomPadding, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.FontSize, HtmlConverterData.PropertyValueParsingMethods.ParseCssFontSize, null),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.LeftBorderStyle, HtmlConverterData.PropertyValueParsingMethods.ParseBorderStyle, null),
			new CssPropertyInstruction(PropertyId.Paddings, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			new CssPropertyInstruction(PropertyId.LeftPadding, HtmlConverterData.PropertyValueParsingMethods.ParseNonNegativeLength, null),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			default(CssPropertyInstruction),
			new CssPropertyInstruction(PropertyId.BorderWidths, null, HtmlConverterData.MultiPropertyParsingMethods.ParseCompositeBorder),
			default(CssPropertyInstruction)
		};

		// Token: 0x020001E5 RID: 485
		public static class DefaultStyle
		{
			// Token: 0x04001462 RID: 5218
			public const int None = 0;

			// Token: 0x04001463 RID: 5219
			public const int B = 1;

			// Token: 0x04001464 RID: 5220
			public const int Big = 2;

			// Token: 0x04001465 RID: 5221
			public const int Del = 3;

			// Token: 0x04001466 RID: 5222
			public const int EM = 4;

			// Token: 0x04001467 RID: 5223
			public const int I = 4;

			// Token: 0x04001468 RID: 5224
			public const int Ins = 5;

			// Token: 0x04001469 RID: 5225
			public const int S = 3;

			// Token: 0x0400146A RID: 5226
			public const int Small = 6;

			// Token: 0x0400146B RID: 5227
			public const int Strike = 3;

			// Token: 0x0400146C RID: 5228
			public const int Strong = 1;

			// Token: 0x0400146D RID: 5229
			public const int Sub = 7;

			// Token: 0x0400146E RID: 5230
			public const int Sup = 8;

			// Token: 0x0400146F RID: 5231
			public const int U = 5;

			// Token: 0x04001470 RID: 5232
			public const int Var = 4;

			// Token: 0x04001471 RID: 5233
			public const int Code = 9;

			// Token: 0x04001472 RID: 5234
			public const int Cite = 4;

			// Token: 0x04001473 RID: 5235
			public const int Dfn = 4;

			// Token: 0x04001474 RID: 5236
			public const int Kbd = 9;

			// Token: 0x04001475 RID: 5237
			public const int Samp = 9;

			// Token: 0x04001476 RID: 5238
			public const int TT = 10;

			// Token: 0x04001477 RID: 5239
			public const int Bdo = 11;

			// Token: 0x04001478 RID: 5240
			public const int NoBR = 12;

			// Token: 0x04001479 RID: 5241
			public const int Center = 13;

			// Token: 0x0400147A RID: 5242
			public const int Xmp = 14;

			// Token: 0x0400147B RID: 5243
			public const int Pre = 14;

			// Token: 0x0400147C RID: 5244
			public const int Listing = 15;

			// Token: 0x0400147D RID: 5245
			public const int PlainText = 14;

			// Token: 0x0400147E RID: 5246
			public const int BlockQuote = 16;

			// Token: 0x0400147F RID: 5247
			public const int Address = 4;

			// Token: 0x04001480 RID: 5248
			public const int H1 = 17;

			// Token: 0x04001481 RID: 5249
			public const int H2 = 18;

			// Token: 0x04001482 RID: 5250
			public const int H3 = 19;

			// Token: 0x04001483 RID: 5251
			public const int H4 = 20;

			// Token: 0x04001484 RID: 5252
			public const int H5 = 21;

			// Token: 0x04001485 RID: 5253
			public const int H6 = 22;

			// Token: 0x04001486 RID: 5254
			public const int OL = 23;

			// Token: 0x04001487 RID: 5255
			public const int UL = 24;

			// Token: 0x04001488 RID: 5256
			public const int Dir = 24;

			// Token: 0x04001489 RID: 5257
			public const int Menu = 24;

			// Token: 0x0400148A RID: 5258
			public const int DT = 25;

			// Token: 0x0400148B RID: 5259
			public const int DD = 25;

			// Token: 0x0400148C RID: 5260
			public const int Caption = 13;
		}

		// Token: 0x020001E6 RID: 486
		public static class PropertyValueParsingMethods
		{
			// Token: 0x0400148D RID: 5261
			public static readonly PropertyValueParsingMethod ParseAreaShape = new PropertyValueParsingMethod(HtmlSupport.ParseAreaShape);

			// Token: 0x0400148E RID: 5262
			public static readonly PropertyValueParsingMethod ParseBlockAlignment = new PropertyValueParsingMethod(HtmlSupport.ParseBlockAlignment);

			// Token: 0x0400148F RID: 5263
			public static readonly PropertyValueParsingMethod ParseBooleanAttribute = new PropertyValueParsingMethod(HtmlSupport.ParseBooleanAttribute);

			// Token: 0x04001490 RID: 5264
			public static readonly PropertyValueParsingMethod ParseBorderCollapse = new PropertyValueParsingMethod(HtmlSupport.ParseBorderCollapse);

			// Token: 0x04001491 RID: 5265
			public static readonly PropertyValueParsingMethod ParseBorderStyle = new PropertyValueParsingMethod(HtmlSupport.ParseBorderStyle);

			// Token: 0x04001492 RID: 5266
			public static readonly PropertyValueParsingMethod ParseBorderWidth = new PropertyValueParsingMethod(HtmlSupport.ParseBorderWidth);

			// Token: 0x04001493 RID: 5267
			public static readonly PropertyValueParsingMethod ParseCaptionSide = new PropertyValueParsingMethod(HtmlSupport.ParseCaptionSide);

			// Token: 0x04001494 RID: 5268
			public static readonly PropertyValueParsingMethod ParseColor = new PropertyValueParsingMethod(HtmlSupport.ParseColor);

			// Token: 0x04001495 RID: 5269
			public static readonly PropertyValueParsingMethod ParseColorCss = new PropertyValueParsingMethod(HtmlSupport.ParseColorCss);

			// Token: 0x04001496 RID: 5270
			public static readonly PropertyValueParsingMethod ParseCssFontSize = new PropertyValueParsingMethod(HtmlSupport.ParseCssFontSize);

			// Token: 0x04001497 RID: 5271
			public static readonly PropertyValueParsingMethod ParseDirection = new PropertyValueParsingMethod(HtmlSupport.ParseDirection);

			// Token: 0x04001498 RID: 5272
			public static readonly PropertyValueParsingMethod ParseDisplay = new PropertyValueParsingMethod(HtmlSupport.ParseDisplay);

			// Token: 0x04001499 RID: 5273
			public static readonly PropertyValueParsingMethod ParseEmptyCells = new PropertyValueParsingMethod(HtmlSupport.ParseEmptyCells);

			// Token: 0x0400149A RID: 5274
			public static readonly PropertyValueParsingMethod ParseFontFace = new PropertyValueParsingMethod(HtmlSupport.ParseFontFace);

			// Token: 0x0400149B RID: 5275
			public static readonly PropertyValueParsingMethod ParseFontSize = new PropertyValueParsingMethod(HtmlSupport.ParseFontSize);

			// Token: 0x0400149C RID: 5276
			public static readonly PropertyValueParsingMethod ParseFontStyle = new PropertyValueParsingMethod(HtmlSupport.ParseFontStyle);

			// Token: 0x0400149D RID: 5277
			public static readonly PropertyValueParsingMethod ParseFontVariant = new PropertyValueParsingMethod(HtmlSupport.ParseFontVariant);

			// Token: 0x0400149E RID: 5278
			public static readonly PropertyValueParsingMethod ParseFontWeight = new PropertyValueParsingMethod(HtmlSupport.ParseFontWeight);

			// Token: 0x0400149F RID: 5279
			public static readonly PropertyValueParsingMethod ParseHorizontalAlignment = new PropertyValueParsingMethod(HtmlSupport.ParseHorizontalAlignment);

			// Token: 0x040014A0 RID: 5280
			public static readonly PropertyValueParsingMethod ParseLanguage = new PropertyValueParsingMethod(HtmlSupport.ParseLanguage);

			// Token: 0x040014A1 RID: 5281
			public static readonly PropertyValueParsingMethod ParseLength = new PropertyValueParsingMethod(HtmlSupport.ParseLength);

			// Token: 0x040014A2 RID: 5282
			public static readonly PropertyValueParsingMethod ParseNonNegativeInteger = new PropertyValueParsingMethod(HtmlSupport.ParseNonNegativeInteger);

			// Token: 0x040014A3 RID: 5283
			public static readonly PropertyValueParsingMethod ParseNonNegativeLength = new PropertyValueParsingMethod(HtmlSupport.ParseNonNegativeLength);

			// Token: 0x040014A4 RID: 5284
			public static readonly PropertyValueParsingMethod ParseStringProperty = new PropertyValueParsingMethod(HtmlSupport.ParseStringProperty);

			// Token: 0x040014A5 RID: 5285
			public static readonly PropertyValueParsingMethod ParseTableFrame = new PropertyValueParsingMethod(HtmlSupport.ParseTableFrame);

			// Token: 0x040014A6 RID: 5286
			public static readonly PropertyValueParsingMethod ParseTableLayout = new PropertyValueParsingMethod(HtmlSupport.ParseTableLayout);

			// Token: 0x040014A7 RID: 5287
			public static readonly PropertyValueParsingMethod ParseTableRules = new PropertyValueParsingMethod(HtmlSupport.ParseTableRules);

			// Token: 0x040014A8 RID: 5288
			public static readonly PropertyValueParsingMethod ParseTarget = new PropertyValueParsingMethod(HtmlSupport.ParseTarget);

			// Token: 0x040014A9 RID: 5289
			public static readonly PropertyValueParsingMethod ParseTextAlignment = new PropertyValueParsingMethod(HtmlSupport.ParseTextAlignment);

			// Token: 0x040014AA RID: 5290
			public static readonly PropertyValueParsingMethod ParseUnicodeBiDi = new PropertyValueParsingMethod(HtmlSupport.ParseUnicodeBiDi);

			// Token: 0x040014AB RID: 5291
			public static readonly PropertyValueParsingMethod ParseUrl = new PropertyValueParsingMethod(HtmlSupport.ParseUrl);

			// Token: 0x040014AC RID: 5292
			public static readonly PropertyValueParsingMethod ParseVerticalAlignment = new PropertyValueParsingMethod(HtmlSupport.ParseVerticalAlignment);

			// Token: 0x040014AD RID: 5293
			public static readonly PropertyValueParsingMethod ParseVisibility = new PropertyValueParsingMethod(HtmlSupport.ParseVisibility);
		}

		// Token: 0x020001E7 RID: 487
		public static class MultiPropertyParsingMethods
		{
			// Token: 0x040014AE RID: 5294
			public static readonly MultiPropertyParsingMethod ParseCompositeAllBorders = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeAllBorders);

			// Token: 0x040014AF RID: 5295
			public static readonly MultiPropertyParsingMethod ParseCompositeBackground = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeBackground);

			// Token: 0x040014B0 RID: 5296
			public static readonly MultiPropertyParsingMethod ParseCompositeBorder = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeBorder);

			// Token: 0x040014B1 RID: 5297
			public static readonly MultiPropertyParsingMethod ParseCompositeBorderStyle = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeBorderStyle);

			// Token: 0x040014B2 RID: 5298
			public static readonly MultiPropertyParsingMethod ParseCompositeBorderWidth = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeBorderWidth);

			// Token: 0x040014B3 RID: 5299
			public static readonly MultiPropertyParsingMethod ParseCompositeColor = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeColor);

			// Token: 0x040014B4 RID: 5300
			public static readonly MultiPropertyParsingMethod ParseCompositeFont = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeFont);

			// Token: 0x040014B5 RID: 5301
			public static readonly MultiPropertyParsingMethod ParseCompositeLength = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeLength);

			// Token: 0x040014B6 RID: 5302
			public static readonly MultiPropertyParsingMethod ParseCompositeNonNegativeLength = new MultiPropertyParsingMethod(HtmlSupport.ParseCompositeNonNegativeLength);

			// Token: 0x040014B7 RID: 5303
			public static readonly MultiPropertyParsingMethod ParseCompoundBorderSpacing = new MultiPropertyParsingMethod(HtmlSupport.ParseCompoundBorderSpacing);

			// Token: 0x040014B8 RID: 5304
			public static readonly MultiPropertyParsingMethod ParseCssTextDecoration = new MultiPropertyParsingMethod(HtmlSupport.ParseCssTextDecoration);

			// Token: 0x040014B9 RID: 5305
			public static readonly MultiPropertyParsingMethod ParseCssTextTransform = new MultiPropertyParsingMethod(HtmlSupport.ParseCssTextTransform);

			// Token: 0x040014BA RID: 5306
			public static readonly MultiPropertyParsingMethod ParseCssVerticalAlignment = new MultiPropertyParsingMethod(HtmlSupport.ParseCssVerticalAlignment);

			// Token: 0x040014BB RID: 5307
			public static readonly MultiPropertyParsingMethod ParseCssWhiteSpace = new MultiPropertyParsingMethod(HtmlSupport.ParseCssWhiteSpace);
		}
	}
}
