using System;
using System.Collections.Generic;
using Microsoft.Ceres.InteractionEngine.Processing.BuiltIn.Parsing.Kql;
using Microsoft.Ceres.NlpBase.RichTypes.QueryTree;
using Microsoft.Exchange.Data.Search.AqsParser;
using Microsoft.Exchange.Data.Search.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Search.KqlParser
{
	// Token: 0x02000D0E RID: 3342
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class SpecialPropertyHandler : ISpecialPropertyHandler
	{
		// Token: 0x06007318 RID: 29464 RVA: 0x001FE8D8 File Offset: 0x001FCAD8
		public SpecialPropertyHandler(LocalizedKeywordMapping keywordMapping, List<ParserErrorInfo> errors)
		{
			this.keywordMapping = keywordMapping;
			this.errors = errors;
		}

		// Token: 0x06007319 RID: 29465 RVA: 0x001FE8F0 File Offset: 0x001FCAF0
		public TreeNode CheckForSpecialProperty(string name, string value, PropertyOperator op, bool excluded)
		{
			PropertyKeyword propertyKeyword;
			KindKeyword kindKeyword;
			if (this.errors != null && this.keywordMapping.TryGetPropertyKeyword(name, out propertyKeyword) && propertyKeyword == PropertyKeyword.Kind && !this.keywordMapping.TryGetKindKeyword(value, out kindKeyword))
			{
				this.errors.Add(new ParserErrorInfo(ParserErrorCode.InvalidKindFormat));
			}
			return null;
		}

		// Token: 0x04005058 RID: 20568
		private readonly LocalizedKeywordMapping keywordMapping;

		// Token: 0x04005059 RID: 20569
		private List<ParserErrorInfo> errors;
	}
}
