using System;
using System.Collections.Generic;
using Microsoft.Ceres.InteractionEngine.Processing.BuiltIn.Parsing.Kql;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.Data.Search.KqlParser
{
	// Token: 0x02000D0A RID: 3338
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PropertyLookup : IPropertyLookup
	{
		// Token: 0x060072E1 RID: 29409 RVA: 0x001FD73F File Offset: 0x001FB93F
		public PropertyLookup(LocalizedKeywordMapping keywordMapping, ICollection<PropertyKeyword> keywords, List<ParserErrorInfo> errors)
		{
			this.keywordMapping = keywordMapping;
			this.keywords = keywords;
			this.errors = errors;
		}

		// Token: 0x060072E2 RID: 29410 RVA: 0x001FD75C File Offset: 0x001FB95C
		public PropertyInformation GetPropertyInformation(string name)
		{
			PropertyKeyword propertyKeyword;
			if (!this.keywordMapping.TryGetPropertyKeyword(name, out propertyKeyword) || !this.keywords.Contains(propertyKeyword))
			{
				if (this.errors != null)
				{
					this.errors.Add(new ParserErrorInfo(ParserErrorCode.InvalidPropertyKey));
				}
				return null;
			}
			if (propertyKeyword == PropertyKeyword.All)
			{
				return new PropertyInformation(name, typeof(string));
			}
			if (propertyKeyword == PropertyKeyword.IsFlagged)
			{
				return new PropertyInformation(name, typeof(bool));
			}
			Type type = PropertyLookup.GetPropertyKeywordType(propertyKeyword);
			if (type == typeof(ExDateTime))
			{
				type = typeof(DateTime);
			}
			return new PropertyInformation(name, type);
		}

		// Token: 0x060072E3 RID: 29411 RVA: 0x001FD7F9 File Offset: 0x001FB9F9
		private static Type GetPropertyKeywordType(PropertyKeyword propertyKeyword)
		{
			return Globals.PropertyKeywordToDefinitionMap[propertyKeyword][0].Type;
		}

		// Token: 0x04005045 RID: 20549
		private readonly LocalizedKeywordMapping keywordMapping;

		// Token: 0x04005046 RID: 20550
		private readonly ICollection<PropertyKeyword> keywords;

		// Token: 0x04005047 RID: 20551
		private List<ParserErrorInfo> errors;
	}
}
