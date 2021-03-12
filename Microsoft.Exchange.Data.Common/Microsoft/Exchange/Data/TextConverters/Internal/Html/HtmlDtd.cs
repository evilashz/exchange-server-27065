using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Html
{
	// Token: 0x020001E9 RID: 489
	internal static class HtmlDtd
	{
		// Token: 0x060014F3 RID: 5363 RVA: 0x0009A427 File Offset: 0x00098627
		public static bool IsTagInSet(HtmlTagIndex tag, HtmlDtd.SetId set)
		{
			return 0 != (HtmlDtd.sets[(int)(set + (short)(tag >> 3))] & (byte)(1 << (int)(tag & HtmlTagIndex.Unknown)));
		}

		// Token: 0x04001538 RID: 5432
		public static HtmlDtd.TagDefinition[] tags = new HtmlDtd.TagDefinition[]
		{
			new HtmlDtd.TagDefinition(HtmlTagIndex._NULL, HtmlNameIndex._NOTANAME, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._ROOT, HtmlNameIndex._NOTANAME, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.Body, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Root, HtmlDtd.ContextTextType.Full, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._COMMENT, HtmlNameIndex._COMMENT, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._CONDITIONAL, HtmlNameIndex._CONDITIONAL, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._BANG, HtmlNameIndex._BANG, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._DTD, HtmlNameIndex._DTD, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._ASP, HtmlNameIndex._ASP, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Unknown, HtmlNameIndex.Unknown, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.A, HtmlNameIndex.A, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, (HtmlDtd.SetId)48, (HtmlDtd.SetId)64, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Abbr, HtmlNameIndex.Abbr, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Acronym, HtmlNameIndex.Acronym, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Address, HtmlNameIndex.Address, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Applet, HtmlNameIndex.Applet, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)160, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Area, HtmlNameIndex.Area, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.QUERY, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.B, HtmlNameIndex.B, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Base, HtmlNameIndex.Base, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.BaseFont, HtmlNameIndex.BaseFont, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)192, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Bdo, HtmlNameIndex.Bdo, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.BGSound, HtmlNameIndex.BGSound, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Big, HtmlNameIndex.Big, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Blink, HtmlNameIndex.Blink, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.BlockQuote, HtmlNameIndex.BlockQuote, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Body, HtmlNameIndex.Body, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, (HtmlDtd.SetId)208, (HtmlDtd.SetId)224, (HtmlDtd.SetId)144, HtmlTagIndex.Html, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Body, HtmlDtd.ContextTextType.Full, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.BR, HtmlNameIndex.BR, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.NBR_BRK_NBR_BRK, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Button, HtmlNameIndex.Button, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.EAT_EAT_EAT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)96, HtmlDtd.SetId.Null, (HtmlDtd.SetId)48, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Caption, HtmlNameIndex.Caption, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)288, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Center, HtmlNameIndex.Center, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Cite, HtmlNameIndex.Cite, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Code, HtmlNameIndex.Code, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Col, HtmlNameIndex.Col, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)304, HtmlDtd.SetId.Null, (HtmlDtd.SetId)320, (HtmlDtd.SetId)304, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.ColGroup, HtmlNameIndex.ColGroup, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)288, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Comment, HtmlNameIndex.Comment, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Comment, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.DD, HtmlNameIndex.DD, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)336, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Del, HtmlNameIndex.Del, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Dfn, HtmlNameIndex.Dfn, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Dir, HtmlNameIndex.Dir, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Div, HtmlNameIndex.Div, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.DL, HtmlNameIndex.DL, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.DT, HtmlNameIndex.DT, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)336, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.EM, HtmlNameIndex.EM, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Embed, HtmlNameIndex.Embed, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.FieldSet, HtmlNameIndex.FieldSet, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Font, HtmlNameIndex.Font, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Form, HtmlNameIndex.Form, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, (HtmlDtd.SetId)352, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Frame, HtmlNameIndex.Frame, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, (HtmlDtd.SetId)352, (HtmlDtd.SetId)368, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Frame, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.FrameSet, HtmlNameIndex.FrameSet, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)384, (HtmlDtd.SetId)144, HtmlTagIndex.Html, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Frameset, HtmlDtd.ContextTextType.None, (HtmlDtd.SetId)400, HtmlDtd.SetId.Null, (HtmlDtd.SetId)144),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H1, HtmlNameIndex.H1, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H2, HtmlNameIndex.H2, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H3, HtmlNameIndex.H3, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H4, HtmlNameIndex.H4, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H5, HtmlNameIndex.H5, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.H6, HtmlNameIndex.H6, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, (HtmlDtd.SetId)416, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Head, HtmlNameIndex.Head, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, (HtmlDtd.SetId)176, HtmlDtd.SetId.Null, (HtmlDtd.SetId)144, HtmlTagIndex.Html, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Head, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)432),
			new HtmlDtd.TagDefinition(HtmlTagIndex.HR, HtmlNameIndex.HR, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Html, HtmlNameIndex.Html, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.Body, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.I, HtmlNameIndex.I, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Iframe, HtmlNameIndex.Iframe, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)448, (HtmlDtd.SetId)208, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.IFrame, HtmlDtd.ContextTextType.Full, (HtmlDtd.SetId)448, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Image, HtmlNameIndex.Image, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Img, HtmlNameIndex.Img, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Input, HtmlNameIndex.Input, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.QUERY, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, (HtmlDtd.SetId)48, (HtmlDtd.SetId)464, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Ins, HtmlNameIndex.Ins, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.IsIndex, HtmlNameIndex.IsIndex, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Kbd, HtmlNameIndex.Kbd, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Label, HtmlNameIndex.Label, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Legend, HtmlNameIndex.Legend, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)480, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.LI, HtmlNameIndex.LI, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_NBR, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)496, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Link, HtmlNameIndex.Link, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Listing, HtmlNameIndex.Listing, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_PUT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.Pre, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Map, HtmlNameIndex.Map, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Marquee, HtmlNameIndex.Marquee, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_EAT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)208, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Menu, HtmlNameIndex.Menu, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Meta, HtmlNameIndex.Meta, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.NextId, HtmlNameIndex.NextId, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.NoBR, HtmlNameIndex.NoBR, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)512, (HtmlDtd.SetId)208, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.NoEmbed, HtmlNameIndex.NoEmbed, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)528, (HtmlDtd.SetId)544, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.NoFrames, HtmlNameIndex.NoFrames, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)528, (HtmlDtd.SetId)544, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.NoScript, HtmlNameIndex.NoScript, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)528, (HtmlDtd.SetId)544, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Object, HtmlNameIndex.Object, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.EAT_EAT_EAT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)144, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, (HtmlDtd.SetId)160, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.OL, HtmlNameIndex.OL, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.OptGroup, HtmlNameIndex.OptGroup, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)464, (HtmlDtd.SetId)464, HtmlDtd.SetId.Null, (HtmlDtd.SetId)560, (HtmlDtd.SetId)464, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Option, HtmlNameIndex.Option, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_EAT_EAT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)464, (HtmlDtd.SetId)464, HtmlDtd.SetId.Null, (HtmlDtd.SetId)576, (HtmlDtd.SetId)464, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.P, HtmlNameIndex.P, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Param, HtmlNameIndex.Param, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)160, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)160, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.PlainText, HtmlNameIndex.PlainText, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, true, HtmlDtd.TagFill.PUT_PUT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Pre, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Pre, HtmlNameIndex.Pre, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_PUT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.Pre, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Q, HtmlNameIndex.Q, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.RP, HtmlNameIndex.RP, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)592, (HtmlDtd.SetId)608, HtmlDtd.SetId.Null, (HtmlDtd.SetId)624, (HtmlDtd.SetId)640, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.RT, HtmlNameIndex.RT, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)608, (HtmlDtd.SetId)608, HtmlDtd.SetId.Null, (HtmlDtd.SetId)656, (HtmlDtd.SetId)640, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Ruby, HtmlNameIndex.Ruby, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, (HtmlDtd.SetId)672, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.S, HtmlNameIndex.S, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Samp, HtmlNameIndex.Samp, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Script, HtmlNameIndex.Script, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, false, HtmlDtd.TagFill.NUL_NUL_NUL_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Script, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Select, HtmlNameIndex.Select, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, (HtmlDtd.SetId)48, (HtmlDtd.SetId)464, (HtmlDtd.SetId)208, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Select, HtmlDtd.ContextTextType.Full, (HtmlDtd.SetId)688, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Small, HtmlNameIndex.Small, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Span, HtmlNameIndex.Span, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Data, HtmlNameIndex.Data, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Meter, HtmlNameIndex.Meter, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Strike, HtmlNameIndex.Strike, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Strong, HtmlNameIndex.Strong, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Style, HtmlNameIndex.Style, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Style, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Sub, HtmlNameIndex.Sub, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Sup, HtmlNameIndex.Sup, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Table, HtmlNameIndex.Table, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)272, (HtmlDtd.SetId)704, HtmlDtd.SetId.Null, (HtmlDtd.SetId)720, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Tbody, HtmlNameIndex.Tbody, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)288, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TC, HtmlNameIndex.Unknown, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)736, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TD, HtmlNameIndex.TD, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)752, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)768, (HtmlDtd.SetId)784, HtmlTagIndex.TR, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.TableCell, HtmlDtd.ContextTextType.Full, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TextArea, HtmlNameIndex.TextArea, HtmlDtd.Literal.Tags, false, HtmlDtd.TagFill.PUT_PUT_PUT_PUT, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, (HtmlDtd.SetId)48, (HtmlDtd.SetId)464, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Tfoot, HtmlNameIndex.Tfoot, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)288, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TH, HtmlNameIndex.TH, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)752, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)768, (HtmlDtd.SetId)784, HtmlTagIndex.TR, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.TableCell, HtmlDtd.ContextTextType.Full, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Thead, HtmlNameIndex.Thead, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)272, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)288, (HtmlDtd.SetId)272, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Title, HtmlNameIndex.Title, HtmlDtd.Literal.Tags, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Title, HtmlDtd.ContextTextType.Full, HtmlDtd.SetId.Null, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TR, HtmlNameIndex.TR, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_BRK_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.NEVER, (HtmlDtd.SetId)800, (HtmlDtd.SetId)272, HtmlDtd.SetId.Null, (HtmlDtd.SetId)816, (HtmlDtd.SetId)832, HtmlTagIndex.Tbody, false, HtmlDtd.TagTextScope.EXCLUDE, HtmlTagIndex.TC, HtmlDtd.SetId.Null, HtmlTagIndex.TD, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.TT, HtmlNameIndex.TT, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.U, HtmlNameIndex.U, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.UL, HtmlNameIndex.UL, HtmlDtd.Literal.None, true, HtmlDtd.TagFill.PUT_EAT_PUT_EAT, HtmlDtd.TagFmt.BRK_BRK_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._IMPLICIT_BEGIN, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Var, HtmlNameIndex.Var, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.OVERLAP, HtmlDtd.TagTextType.QUERY, (HtmlDtd.SetId)32, (HtmlDtd.SetId)32, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)80, HtmlTagIndex.Body, true, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Wbr, HtmlNameIndex.Wbr, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.ALWAYS, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Xmp, HtmlNameIndex.Xmp, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, false, HtmlDtd.TagFill.PUT_PUT_PUT_EAT, HtmlDtd.TagFmt.BRK_NBR_NBR_BRK, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.ALWAYS, (HtmlDtd.SetId)96, (HtmlDtd.SetId)112, HtmlDtd.SetId.Null, (HtmlDtd.SetId)128, (HtmlDtd.SetId)112, HtmlTagIndex.Body, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.Pre, HtmlDtd.ContextTextType.Literal, HtmlDtd.SetId.Null, (HtmlDtd.SetId)240, (HtmlDtd.SetId)256),
			new HtmlDtd.TagDefinition(HtmlTagIndex.Xml, HtmlNameIndex.Xml, HtmlDtd.Literal.Tags | HtmlDtd.Literal.Entities, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.NESTED, HtmlDtd.TagTextType.QUERY, HtmlDtd.SetId.Empty, (HtmlDtd.SetId)144, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, (HtmlDtd.SetId)176, HtmlTagIndex.Head, false, HtmlDtd.TagTextScope.INCLUDE, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._Pxml, HtmlNameIndex._Pxml, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._Import, HtmlNameIndex._Import, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null),
			new HtmlDtd.TagDefinition(HtmlTagIndex._Xml_Namespace, HtmlNameIndex._Xml_Namespace, HtmlDtd.Literal.None, false, HtmlDtd.TagFill.PUT_NUL_PUT_NUL, HtmlDtd.TagFmt.AUT_AUT_AUT_AUT, HtmlDtd.TagScope.EMPTY, HtmlDtd.TagTextType.NEVER, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Empty, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, false, HtmlDtd.TagTextScope.NEUTRAL, HtmlTagIndex._NULL, HtmlDtd.SetId.Null, HtmlTagIndex._NULL, HtmlDtd.ContextType.None, HtmlDtd.ContextTextType.None, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null, HtmlDtd.SetId.Null)
		};

		// Token: 0x04001539 RID: 5433
		public static byte[] sets = new byte[]
		{
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3,
			0,
			0,
			64,
			0,
			32,
			0,
			0,
			0,
			0,
			18,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			3,
			0,
			0,
			0,
			0,
			32,
			0,
			0,
			0,
			0,
			18,
			0,
			0,
			0,
			0,
			64,
			3,
			0,
			0,
			16,
			0,
			32,
			0,
			0,
			0,
			192,
			187,
			0,
			0,
			0,
			0,
			96,
			3,
			40,
			0,
			0,
			0,
			104,
			64,
			16,
			0,
			192,
			187,
			4,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			48,
			80,
			0,
			0,
			128,
			5,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			64,
			0,
			65,
			0,
			80,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			66,
			0,
			0,
			0,
			0,
			0,
			128,
			1,
			16,
			128,
			41,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			128,
			1,
			16,
			128,
			41,
			0,
			0,
			0,
			0,
			0,
			0,
			65,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			32,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			8,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			0,
			49,
			0,
			0,
			0,
			8,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			192,
			15,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			80,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			28,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			32,
			16,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			3,
			0,
			0,
			64,
			0,
			32,
			0,
			128,
			1,
			0,
			18,
			0,
			0,
			0,
			0,
			0,
			3,
			0,
			0,
			64,
			0,
			32,
			0,
			0,
			1,
			0,
			18,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			1,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			192,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			1,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			0,
			0,
			0,
			8,
			0,
			128,
			1,
			8,
			192,
			191,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			19,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			1,
			16,
			64,
			1,
			0,
			0,
			0,
			0,
			0,
			66,
			0,
			0,
			0,
			0,
			0,
			128,
			1,
			16,
			0,
			19,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			64,
			128,
			0,
			0,
			0,
			1,
			0,
			70,
			0,
			200,
			15,
			0,
			16,
			128,
			1,
			16,
			0,
			19,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			0,
			0,
			0,
			0,
			0,
			2,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			192,
			58,
			0,
			0,
			0,
			1,
			0,
			70,
			0,
			200,
			15,
			0,
			16,
			128,
			1,
			16,
			0,
			129,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			0,
			128,
			40,
			0,
			0
		};

		// Token: 0x020001EA RID: 490
		internal enum TagScope : byte
		{
			// Token: 0x0400153B RID: 5435
			EMPTY,
			// Token: 0x0400153C RID: 5436
			OVERLAP,
			// Token: 0x0400153D RID: 5437
			NESTED
		}

		// Token: 0x020001EB RID: 491
		internal enum TagTextType : byte
		{
			// Token: 0x0400153F RID: 5439
			NEVER,
			// Token: 0x04001540 RID: 5440
			ALWAYS,
			// Token: 0x04001541 RID: 5441
			QUERY
		}

		// Token: 0x020001EC RID: 492
		internal enum TagTextScope : byte
		{
			// Token: 0x04001543 RID: 5443
			NEUTRAL,
			// Token: 0x04001544 RID: 5444
			EXCLUDE,
			// Token: 0x04001545 RID: 5445
			INCLUDE
		}

		// Token: 0x020001ED RID: 493
		internal enum ContextType : byte
		{
			// Token: 0x04001547 RID: 5447
			None,
			// Token: 0x04001548 RID: 5448
			Root,
			// Token: 0x04001549 RID: 5449
			Head,
			// Token: 0x0400154A RID: 5450
			Title,
			// Token: 0x0400154B RID: 5451
			Body,
			// Token: 0x0400154C RID: 5452
			TableCell,
			// Token: 0x0400154D RID: 5453
			Select,
			// Token: 0x0400154E RID: 5454
			Pre,
			// Token: 0x0400154F RID: 5455
			Frameset,
			// Token: 0x04001550 RID: 5456
			Frame,
			// Token: 0x04001551 RID: 5457
			IFrame,
			// Token: 0x04001552 RID: 5458
			Object,
			// Token: 0x04001553 RID: 5459
			Script,
			// Token: 0x04001554 RID: 5460
			Style,
			// Token: 0x04001555 RID: 5461
			Comment,
			// Token: 0x04001556 RID: 5462
			NoShow
		}

		// Token: 0x020001EE RID: 494
		internal enum ContextTextType : byte
		{
			// Token: 0x04001558 RID: 5464
			None,
			// Token: 0x04001559 RID: 5465
			Discard = 0,
			// Token: 0x0400155A RID: 5466
			Literal,
			// Token: 0x0400155B RID: 5467
			Full
		}

		// Token: 0x020001EF RID: 495
		[Flags]
		internal enum Literal : byte
		{
			// Token: 0x0400155D RID: 5469
			None = 0,
			// Token: 0x0400155E RID: 5470
			Tags = 1,
			// Token: 0x0400155F RID: 5471
			Entities = 2
		}

		// Token: 0x020001F0 RID: 496
		internal enum FillCode : byte
		{
			// Token: 0x04001561 RID: 5473
			NUL,
			// Token: 0x04001562 RID: 5474
			PUT,
			// Token: 0x04001563 RID: 5475
			EAT
		}

		// Token: 0x020001F1 RID: 497
		internal struct TagFill
		{
			// Token: 0x1700055A RID: 1370
			// (get) Token: 0x060014F5 RID: 5365 RVA: 0x0009C14D File Offset: 0x0009A34D
			public HtmlDtd.FillCode LB
			{
				get
				{
					return (HtmlDtd.FillCode)(this.value >> 6);
				}
			}

			// Token: 0x1700055B RID: 1371
			// (get) Token: 0x060014F6 RID: 5366 RVA: 0x0009C158 File Offset: 0x0009A358
			public HtmlDtd.FillCode RB
			{
				get
				{
					return (HtmlDtd.FillCode)(this.value >> 4 & 3);
				}
			}

			// Token: 0x1700055C RID: 1372
			// (get) Token: 0x060014F7 RID: 5367 RVA: 0x0009C165 File Offset: 0x0009A365
			public HtmlDtd.FillCode LE
			{
				get
				{
					return (HtmlDtd.FillCode)(this.value >> 2 & 3);
				}
			}

			// Token: 0x1700055D RID: 1373
			// (get) Token: 0x060014F8 RID: 5368 RVA: 0x0009C172 File Offset: 0x0009A372
			public HtmlDtd.FillCode RE
			{
				get
				{
					return (HtmlDtd.FillCode)(this.value & 3);
				}
			}

			// Token: 0x060014F9 RID: 5369 RVA: 0x0009C17D File Offset: 0x0009A37D
			private TagFill(HtmlDtd.FillCode lB, HtmlDtd.FillCode rB, HtmlDtd.FillCode lE, HtmlDtd.FillCode rE)
			{
				this.value = (byte)(lB << 6 | rB << 4 | lE << 2 | rE);
			}

			// Token: 0x04001564 RID: 5476
			internal readonly byte value;

			// Token: 0x04001565 RID: 5477
			public static readonly HtmlDtd.TagFill PUT_NUL_PUT_NUL = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.NUL, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.NUL);

			// Token: 0x04001566 RID: 5478
			public static readonly HtmlDtd.TagFill NUL_NUL_NUL_NUL = new HtmlDtd.TagFill(HtmlDtd.FillCode.NUL, HtmlDtd.FillCode.NUL, HtmlDtd.FillCode.NUL, HtmlDtd.FillCode.NUL);

			// Token: 0x04001567 RID: 5479
			public static readonly HtmlDtd.TagFill NUL_EAT_EAT_NUL = new HtmlDtd.TagFill(HtmlDtd.FillCode.NUL, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.NUL);

			// Token: 0x04001568 RID: 5480
			public static readonly HtmlDtd.TagFill PUT_EAT_PUT_EAT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT);

			// Token: 0x04001569 RID: 5481
			public static readonly HtmlDtd.TagFill PUT_PUT_PUT_PUT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT);

			// Token: 0x0400156A RID: 5482
			public static readonly HtmlDtd.TagFill EAT_EAT_EAT_PUT = new HtmlDtd.TagFill(HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.PUT);

			// Token: 0x0400156B RID: 5483
			public static readonly HtmlDtd.TagFill PUT_PUT_PUT_EAT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT);

			// Token: 0x0400156C RID: 5484
			public static readonly HtmlDtd.TagFill PUT_EAT_PUT_PUT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.PUT);

			// Token: 0x0400156D RID: 5485
			public static readonly HtmlDtd.TagFill PUT_EAT_EAT_EAT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT);

			// Token: 0x0400156E RID: 5486
			public static readonly HtmlDtd.TagFill EAT_EAT_EAT_EAT = new HtmlDtd.TagFill(HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT);

			// Token: 0x0400156F RID: 5487
			public static readonly HtmlDtd.TagFill PUT_EAT_EAT_PUT = new HtmlDtd.TagFill(HtmlDtd.FillCode.PUT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.EAT, HtmlDtd.FillCode.PUT);
		}

		// Token: 0x020001F2 RID: 498
		internal enum FmtCode : byte
		{
			// Token: 0x04001571 RID: 5489
			AUT,
			// Token: 0x04001572 RID: 5490
			BRK,
			// Token: 0x04001573 RID: 5491
			NBR
		}

		// Token: 0x020001F3 RID: 499
		internal struct TagFmt
		{
			// Token: 0x1700055E RID: 1374
			// (get) Token: 0x060014FB RID: 5371 RVA: 0x0009C23B File Offset: 0x0009A43B
			public HtmlDtd.FmtCode LB
			{
				get
				{
					return (HtmlDtd.FmtCode)(this.value >> 6);
				}
			}

			// Token: 0x1700055F RID: 1375
			// (get) Token: 0x060014FC RID: 5372 RVA: 0x0009C246 File Offset: 0x0009A446
			public HtmlDtd.FmtCode RB
			{
				get
				{
					return (HtmlDtd.FmtCode)(this.value >> 4 & 3);
				}
			}

			// Token: 0x17000560 RID: 1376
			// (get) Token: 0x060014FD RID: 5373 RVA: 0x0009C253 File Offset: 0x0009A453
			public HtmlDtd.FmtCode LE
			{
				get
				{
					return (HtmlDtd.FmtCode)(this.value >> 2 & 3);
				}
			}

			// Token: 0x17000561 RID: 1377
			// (get) Token: 0x060014FE RID: 5374 RVA: 0x0009C260 File Offset: 0x0009A460
			public HtmlDtd.FmtCode RE
			{
				get
				{
					return (HtmlDtd.FmtCode)(this.value & 3);
				}
			}

			// Token: 0x060014FF RID: 5375 RVA: 0x0009C26B File Offset: 0x0009A46B
			private TagFmt(HtmlDtd.FmtCode lB, HtmlDtd.FmtCode rB, HtmlDtd.FmtCode lE, HtmlDtd.FmtCode rE)
			{
				this.value = (byte)(lB << 6 | rB << 4 | lE << 2 | rE);
			}

			// Token: 0x04001574 RID: 5492
			internal readonly byte value;

			// Token: 0x04001575 RID: 5493
			public static readonly HtmlDtd.TagFmt BRK_BRK_BRK_BRK = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.BRK);

			// Token: 0x04001576 RID: 5494
			public static readonly HtmlDtd.TagFmt AUT_AUT_AUT_AUT = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.AUT, HtmlDtd.FmtCode.AUT, HtmlDtd.FmtCode.AUT, HtmlDtd.FmtCode.AUT);

			// Token: 0x04001577 RID: 5495
			public static readonly HtmlDtd.TagFmt NBR_BRK_NBR_BRK = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.BRK);

			// Token: 0x04001578 RID: 5496
			public static readonly HtmlDtd.TagFmt BRK_NBR_NBR_BRK = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.BRK);

			// Token: 0x04001579 RID: 5497
			public static readonly HtmlDtd.TagFmt BRK_BRK_NBR_BRK = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.BRK);

			// Token: 0x0400157A RID: 5498
			public static readonly HtmlDtd.TagFmt BRK_NBR_NBR_NBR = new HtmlDtd.TagFmt(HtmlDtd.FmtCode.BRK, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.NBR, HtmlDtd.FmtCode.NBR);
		}

		// Token: 0x020001F4 RID: 500
		public enum SetId : short
		{
			// Token: 0x0400157C RID: 5500
			Null,
			// Token: 0x0400157D RID: 5501
			Empty
		}

		// Token: 0x020001F5 RID: 501
		public class TagDefinition
		{
			// Token: 0x06001501 RID: 5377 RVA: 0x0009C2E8 File Offset: 0x0009A4E8
			public TagDefinition(HtmlTagIndex tagIndex, HtmlNameIndex nameIndex, HtmlDtd.Literal literal, bool blockElement, HtmlDtd.TagFill fill, HtmlDtd.TagFmt fmt, HtmlDtd.TagScope scope, HtmlDtd.TagTextType textType, HtmlDtd.SetId endContainers, HtmlDtd.SetId beginContainers, HtmlDtd.SetId maskingContainers, HtmlDtd.SetId prohibitedContainers, HtmlDtd.SetId requiredContainers, HtmlTagIndex defaultContainer, bool queueForRequired, HtmlDtd.TagTextScope textScope, HtmlTagIndex textSubcontainer, HtmlDtd.SetId match, HtmlTagIndex unmatchedSubstitute, HtmlDtd.ContextType contextType, HtmlDtd.ContextTextType contextTextType, HtmlDtd.SetId accept, HtmlDtd.SetId reject, HtmlDtd.SetId ignoreEnd)
			{
				this.TagIndex = tagIndex;
				this.NameIndex = nameIndex;
				this.Literal = literal;
				this.BlockElement = blockElement;
				this.Fill = fill;
				this.Fmt = fmt;
				this.Scope = scope;
				this.TextType = textType;
				this.EndContainers = endContainers;
				this.BeginContainers = beginContainers;
				this.MaskingContainers = maskingContainers;
				this.ProhibitedContainers = prohibitedContainers;
				this.RequiredContainers = requiredContainers;
				this.DefaultContainer = defaultContainer;
				this.QueueForRequired = queueForRequired;
				this.TextScope = textScope;
				this.TextSubcontainer = textSubcontainer;
				this.Match = match;
				this.UnmatchedSubstitute = unmatchedSubstitute;
				this.ContextType = contextType;
				this.ContextTextType = contextTextType;
				this.Accept = accept;
				this.Reject = reject;
				this.IgnoreEnd = ignoreEnd;
			}

			// Token: 0x0400157E RID: 5502
			public HtmlNameIndex NameIndex;

			// Token: 0x0400157F RID: 5503
			public HtmlTagIndex TagIndex;

			// Token: 0x04001580 RID: 5504
			public HtmlDtd.Literal Literal;

			// Token: 0x04001581 RID: 5505
			public bool BlockElement;

			// Token: 0x04001582 RID: 5506
			public HtmlDtd.TagFill Fill;

			// Token: 0x04001583 RID: 5507
			public HtmlDtd.TagFmt Fmt;

			// Token: 0x04001584 RID: 5508
			public HtmlDtd.TagScope Scope;

			// Token: 0x04001585 RID: 5509
			public HtmlDtd.TagTextType TextType;

			// Token: 0x04001586 RID: 5510
			public HtmlDtd.SetId EndContainers;

			// Token: 0x04001587 RID: 5511
			public HtmlDtd.SetId BeginContainers;

			// Token: 0x04001588 RID: 5512
			public HtmlDtd.SetId MaskingContainers;

			// Token: 0x04001589 RID: 5513
			public HtmlDtd.SetId ProhibitedContainers;

			// Token: 0x0400158A RID: 5514
			public HtmlDtd.SetId RequiredContainers;

			// Token: 0x0400158B RID: 5515
			public HtmlTagIndex DefaultContainer;

			// Token: 0x0400158C RID: 5516
			public bool QueueForRequired;

			// Token: 0x0400158D RID: 5517
			public HtmlDtd.TagTextScope TextScope;

			// Token: 0x0400158E RID: 5518
			public HtmlTagIndex TextSubcontainer;

			// Token: 0x0400158F RID: 5519
			public HtmlDtd.SetId Match;

			// Token: 0x04001590 RID: 5520
			public HtmlTagIndex UnmatchedSubstitute;

			// Token: 0x04001591 RID: 5521
			public HtmlDtd.ContextType ContextType;

			// Token: 0x04001592 RID: 5522
			public HtmlDtd.ContextTextType ContextTextType;

			// Token: 0x04001593 RID: 5523
			public HtmlDtd.SetId Accept;

			// Token: 0x04001594 RID: 5524
			public HtmlDtd.SetId Reject;

			// Token: 0x04001595 RID: 5525
			public HtmlDtd.SetId IgnoreEnd;
		}
	}
}
