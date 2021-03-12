using System;

namespace System.Globalization
{
	// Token: 0x02000383 RID: 899
	internal class TokenHashValue
	{
		// Token: 0x06002E0D RID: 11789 RVA: 0x000B0662 File Offset: 0x000AE862
		internal TokenHashValue(string tokenString, TokenType tokenType, int tokenValue)
		{
			this.tokenString = tokenString;
			this.tokenType = tokenType;
			this.tokenValue = tokenValue;
		}

		// Token: 0x04001314 RID: 4884
		internal string tokenString;

		// Token: 0x04001315 RID: 4885
		internal TokenType tokenType;

		// Token: 0x04001316 RID: 4886
		internal int tokenValue;
	}
}
