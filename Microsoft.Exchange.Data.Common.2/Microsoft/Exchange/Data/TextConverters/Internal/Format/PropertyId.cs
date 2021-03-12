using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Format
{
	// Token: 0x02000288 RID: 648
	internal enum PropertyId : byte
	{
		// Token: 0x04001F62 RID: 8034
		Null,
		// Token: 0x04001F63 RID: 8035
		FirstFlag,
		// Token: 0x04001F64 RID: 8036
		Bold = 1,
		// Token: 0x04001F65 RID: 8037
		Italic,
		// Token: 0x04001F66 RID: 8038
		Underline,
		// Token: 0x04001F67 RID: 8039
		Subscript,
		// Token: 0x04001F68 RID: 8040
		Superscript,
		// Token: 0x04001F69 RID: 8041
		Strikethrough,
		// Token: 0x04001F6A RID: 8042
		SmallCaps,
		// Token: 0x04001F6B RID: 8043
		Capitalize,
		// Token: 0x04001F6C RID: 8044
		RightToLeft,
		// Token: 0x04001F6D RID: 8045
		Preformatted,
		// Token: 0x04001F6E RID: 8046
		NoBreak,
		// Token: 0x04001F6F RID: 8047
		Visible,
		// Token: 0x04001F70 RID: 8048
		Overloaded1,
		// Token: 0x04001F71 RID: 8049
		Overloaded2,
		// Token: 0x04001F72 RID: 8050
		Overloaded3,
		// Token: 0x04001F73 RID: 8051
		MergedCell,
		// Token: 0x04001F74 RID: 8052
		TableLayoutFixed = 13,
		// Token: 0x04001F75 RID: 8053
		SelectMultiple = 13,
		// Token: 0x04001F76 RID: 8054
		OptionSelected = 13,
		// Token: 0x04001F77 RID: 8055
		ReadOnly = 13,
		// Token: 0x04001F78 RID: 8056
		TableBorderCollapse,
		// Token: 0x04001F79 RID: 8057
		Disabled = 14,
		// Token: 0x04001F7A RID: 8058
		Checked,
		// Token: 0x04001F7B RID: 8059
		LastFlag,
		// Token: 0x04001F7C RID: 8060
		FontColor,
		// Token: 0x04001F7D RID: 8061
		FontSize,
		// Token: 0x04001F7E RID: 8062
		FontFace,
		// Token: 0x04001F7F RID: 8063
		TextAlignment,
		// Token: 0x04001F80 RID: 8064
		FirstLineIndent,
		// Token: 0x04001F81 RID: 8065
		BlockAlignment,
		// Token: 0x04001F82 RID: 8066
		HorizontalAlignment = 22,
		// Token: 0x04001F83 RID: 8067
		VerticalAlignment = 22,
		// Token: 0x04001F84 RID: 8068
		BackColor,
		// Token: 0x04001F85 RID: 8069
		Display,
		// Token: 0x04001F86 RID: 8070
		Language,
		// Token: 0x04001F87 RID: 8071
		UnicodeBiDi,
		// Token: 0x04001F88 RID: 8072
		Width,
		// Token: 0x04001F89 RID: 8073
		Height,
		// Token: 0x04001F8A RID: 8074
		Margins,
		// Token: 0x04001F8B RID: 8075
		TopMargin = 29,
		// Token: 0x04001F8C RID: 8076
		RightMargin,
		// Token: 0x04001F8D RID: 8077
		BottomMargin,
		// Token: 0x04001F8E RID: 8078
		LeftMargin,
		// Token: 0x04001F8F RID: 8079
		Paddings,
		// Token: 0x04001F90 RID: 8080
		TopPadding = 33,
		// Token: 0x04001F91 RID: 8081
		RightPadding,
		// Token: 0x04001F92 RID: 8082
		BottomPadding,
		// Token: 0x04001F93 RID: 8083
		LeftPadding,
		// Token: 0x04001F94 RID: 8084
		BorderWidths,
		// Token: 0x04001F95 RID: 8085
		TopBorderWidth = 37,
		// Token: 0x04001F96 RID: 8086
		RightBorderWidth,
		// Token: 0x04001F97 RID: 8087
		BottomBorderWidth,
		// Token: 0x04001F98 RID: 8088
		LeftBorderWidth,
		// Token: 0x04001F99 RID: 8089
		BorderStyles,
		// Token: 0x04001F9A RID: 8090
		TopBorderStyle = 41,
		// Token: 0x04001F9B RID: 8091
		RightBorderStyle,
		// Token: 0x04001F9C RID: 8092
		BottomBorderStyle,
		// Token: 0x04001F9D RID: 8093
		LeftBorderStyle,
		// Token: 0x04001F9E RID: 8094
		BorderColors,
		// Token: 0x04001F9F RID: 8095
		TopBorderColor = 45,
		// Token: 0x04001FA0 RID: 8096
		RightBorderColor,
		// Token: 0x04001FA1 RID: 8097
		BottomBorderColor,
		// Token: 0x04001FA2 RID: 8098
		LeftBorderColor,
		// Token: 0x04001FA3 RID: 8099
		ListLevel,
		// Token: 0x04001FA4 RID: 8100
		ListStyle,
		// Token: 0x04001FA5 RID: 8101
		ListStart,
		// Token: 0x04001FA6 RID: 8102
		NumColumns,
		// Token: 0x04001FA7 RID: 8103
		NumRows,
		// Token: 0x04001FA8 RID: 8104
		TableShowEmptyCells,
		// Token: 0x04001FA9 RID: 8105
		TableCaptionSideTop,
		// Token: 0x04001FAA RID: 8106
		TableCellNoWrap,
		// Token: 0x04001FAB RID: 8107
		TableBorderSpacingVertical,
		// Token: 0x04001FAC RID: 8108
		TableBorderSpacingHorizontal,
		// Token: 0x04001FAD RID: 8109
		TableBorder,
		// Token: 0x04001FAE RID: 8110
		TableFrame,
		// Token: 0x04001FAF RID: 8111
		TableRules,
		// Token: 0x04001FB0 RID: 8112
		TableCellSpacing,
		// Token: 0x04001FB1 RID: 8113
		TableCellPadding,
		// Token: 0x04001FB2 RID: 8114
		BookmarkName,
		// Token: 0x04001FB3 RID: 8115
		HyperlinkUrl,
		// Token: 0x04001FB4 RID: 8116
		HyperlinkTarget,
		// Token: 0x04001FB5 RID: 8117
		ImageUrl,
		// Token: 0x04001FB6 RID: 8118
		ImageAltText,
		// Token: 0x04001FB7 RID: 8119
		QuotingLevelDelta,
		// Token: 0x04001FB8 RID: 8120
		Id,
		// Token: 0x04001FB9 RID: 8121
		Shape,
		// Token: 0x04001FBA RID: 8122
		Coords,
		// Token: 0x04001FBB RID: 8123
		MaxValue,
		// Token: 0x04001FBC RID: 8124
		ImageBorder = 59,
		// Token: 0x04001FBD RID: 8125
		IFrameUrl = 67,
		// Token: 0x04001FBE RID: 8126
		FormAction = 65,
		// Token: 0x04001FBF RID: 8127
		FormMethod,
		// Token: 0x04001FC0 RID: 8128
		FormEncodingType,
		// Token: 0x04001FC1 RID: 8129
		FormAcceptContentTypes,
		// Token: 0x04001FC2 RID: 8130
		FormAcceptCharsets,
		// Token: 0x04001FC3 RID: 8131
		Label = 68,
		// Token: 0x04001FC4 RID: 8132
		OptionValue,
		// Token: 0x04001FC5 RID: 8133
		InputValue = 69,
		// Token: 0x04001FC6 RID: 8134
		InputType = 59,
		// Token: 0x04001FC7 RID: 8135
		InputSize,
		// Token: 0x04001FC8 RID: 8136
		InputMaxLength,
		// Token: 0x04001FC9 RID: 8137
		ButtonType = 59,
		// Token: 0x04001FCA RID: 8138
		Prompt = 68
	}
}
