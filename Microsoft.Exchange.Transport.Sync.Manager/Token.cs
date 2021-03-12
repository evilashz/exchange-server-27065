using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000012 RID: 18
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal struct Token : IEquatable<Token>
	{
		// Token: 0x060000B5 RID: 181 RVA: 0x000070DE File Offset: 0x000052DE
		internal Token(Guid value)
		{
			this.value = value;
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x000070E7 File Offset: 0x000052E7
		public static bool operator ==(Token x, Token y)
		{
			return x.Equals(y);
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x000070F1 File Offset: 0x000052F1
		public static bool operator !=(Token x, Token y)
		{
			return !x.Equals(y);
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x000070FE File Offset: 0x000052FE
		public override int GetHashCode()
		{
			return this.value.GetHashCode();
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x00007114 File Offset: 0x00005314
		public override bool Equals(object o)
		{
			if (!(o is Token))
			{
				return false;
			}
			Token token = (Token)o;
			return this.Equals(token);
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00007139 File Offset: 0x00005339
		public bool Equals(Token token)
		{
			return object.Equals(this.value, token.value);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00007157 File Offset: 0x00005357
		public override string ToString()
		{
			return this.value.ToString();
		}

		// Token: 0x0400005A RID: 90
		private Guid value;
	}
}
