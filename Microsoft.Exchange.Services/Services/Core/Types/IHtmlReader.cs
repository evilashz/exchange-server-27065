using System;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000F68 RID: 3944
	public interface IHtmlReader : IDisposable
	{
		// Token: 0x060063E6 RID: 25574
		void SetNormalizeHtml(bool value);

		// Token: 0x060063E7 RID: 25575
		bool ParseNext();

		// Token: 0x060063E8 RID: 25576
		TokenKind GetTokenKind();

		// Token: 0x060063E9 RID: 25577
		string GetTagName();

		// Token: 0x060063EA RID: 25578
		int GetCurrentOffset();

		// Token: 0x060063EB RID: 25579
		bool ParseNextAttribute();

		// Token: 0x060063EC RID: 25580
		string GetAttributeName();

		// Token: 0x060063ED RID: 25581
		string GetAttributeValue();
	}
}
