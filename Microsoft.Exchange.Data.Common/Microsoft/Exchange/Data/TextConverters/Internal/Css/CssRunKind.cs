using System;

namespace Microsoft.Exchange.Data.TextConverters.Internal.Css
{
	// Token: 0x020001A7 RID: 423
	internal enum CssRunKind : uint
	{
		// Token: 0x04001296 RID: 4758
		Invalid,
		// Token: 0x04001297 RID: 4759
		Text = 67108864U,
		// Token: 0x04001298 RID: 4760
		Space = 184549376U,
		// Token: 0x04001299 RID: 4761
		SimpleSelector = 201326592U,
		// Token: 0x0400129A RID: 4762
		Identifier = 218103808U,
		// Token: 0x0400129B RID: 4763
		Delimiter = 234881024U,
		// Token: 0x0400129C RID: 4764
		AtRuleName = 251658240U,
		// Token: 0x0400129D RID: 4765
		SelectorName = 268435456U,
		// Token: 0x0400129E RID: 4766
		SelectorCombinatorOrComma = 285212672U,
		// Token: 0x0400129F RID: 4767
		SelectorPseudoStart = 301989888U,
		// Token: 0x040012A0 RID: 4768
		SelectorPseudo = 318767104U,
		// Token: 0x040012A1 RID: 4769
		SelectorPseudoArg = 335544320U,
		// Token: 0x040012A2 RID: 4770
		SelectorClassStart = 352321536U,
		// Token: 0x040012A3 RID: 4771
		SelectorClass = 369098752U,
		// Token: 0x040012A4 RID: 4772
		SelectorHashStart = 385875968U,
		// Token: 0x040012A5 RID: 4773
		SelectorHash = 402653184U,
		// Token: 0x040012A6 RID: 4774
		SelectorAttribStart = 419430400U,
		// Token: 0x040012A7 RID: 4775
		SelectorAttribName = 436207616U,
		// Token: 0x040012A8 RID: 4776
		SelectorAttribEquals = 452984832U,
		// Token: 0x040012A9 RID: 4777
		SelectorAttribIncludes = 469762048U,
		// Token: 0x040012AA RID: 4778
		SelectorAttribDashmatch = 486539264U,
		// Token: 0x040012AB RID: 4779
		SelectorAttribIdentifier = 503316480U,
		// Token: 0x040012AC RID: 4780
		SelectorAttribString = 520093696U,
		// Token: 0x040012AD RID: 4781
		SelectorAttribEnd = 536870912U,
		// Token: 0x040012AE RID: 4782
		PropertyName = 671088640U,
		// Token: 0x040012AF RID: 4783
		PropertyColon = 687865856U,
		// Token: 0x040012B0 RID: 4784
		ImportantStart = 704643072U,
		// Token: 0x040012B1 RID: 4785
		Important = 721420288U,
		// Token: 0x040012B2 RID: 4786
		Operator = 738197504U,
		// Token: 0x040012B3 RID: 4787
		UnaryOperator = 754974720U,
		// Token: 0x040012B4 RID: 4788
		Dot = 771751936U,
		// Token: 0x040012B5 RID: 4789
		Percent = 788529152U,
		// Token: 0x040012B6 RID: 4790
		Metrics = 805306368U,
		// Token: 0x040012B7 RID: 4791
		TermIdentifier = 822083584U,
		// Token: 0x040012B8 RID: 4792
		UnicodeRange = 838860800U,
		// Token: 0x040012B9 RID: 4793
		FunctionStart = 855638016U,
		// Token: 0x040012BA RID: 4794
		FunctionEnd = 872415232U,
		// Token: 0x040012BB RID: 4795
		HexColorStart = 889192448U,
		// Token: 0x040012BC RID: 4796
		HexColor = 905969664U,
		// Token: 0x040012BD RID: 4797
		String = 922746880U,
		// Token: 0x040012BE RID: 4798
		Numeric = 939524096U,
		// Token: 0x040012BF RID: 4799
		Url = 973078528U,
		// Token: 0x040012C0 RID: 4800
		PropertySemicolon = 956301312U,
		// Token: 0x040012C1 RID: 4801
		PageIdent = 1174405120U,
		// Token: 0x040012C2 RID: 4802
		PagePseudoStart = 1191182336U,
		// Token: 0x040012C3 RID: 4803
		PagePseudo = 1207959552U
	}
}
