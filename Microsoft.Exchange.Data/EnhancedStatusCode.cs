using System;
using Microsoft.Exchange.Data.Transport.Smtp;

namespace Microsoft.Exchange.Data
{
	// Token: 0x0200005F RID: 95
	[Serializable]
	public class EnhancedStatusCode
	{
		// Token: 0x060002C5 RID: 709 RVA: 0x0000D059 File Offset: 0x0000B259
		public EnhancedStatusCode(string code)
		{
			this.enhancedStatusCodeImpl = new EnhancedStatusCodeImpl(code);
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000D06D File Offset: 0x0000B26D
		private EnhancedStatusCode(EnhancedStatusCodeImpl escImpl)
		{
			this.enhancedStatusCodeImpl = escImpl;
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002C7 RID: 711 RVA: 0x0000D07C File Offset: 0x0000B27C
		public string Value
		{
			get
			{
				return this.enhancedStatusCodeImpl.Value;
			}
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000D089 File Offset: 0x0000B289
		public static EnhancedStatusCode Parse(string code)
		{
			return new EnhancedStatusCode(code);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000D091 File Offset: 0x0000B291
		public static bool TryParse(string code, out EnhancedStatusCode enhancedStatusCode)
		{
			return EnhancedStatusCode.TryParse(code, 0, out enhancedStatusCode);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x0000D09C File Offset: 0x0000B29C
		public static bool TryParse(string line, int startIndex, out EnhancedStatusCode enhancedStatusCode)
		{
			EnhancedStatusCodeImpl escImpl;
			bool flag = EnhancedStatusCodeImpl.TryParse(line, startIndex, out escImpl);
			enhancedStatusCode = null;
			if (flag)
			{
				enhancedStatusCode = new EnhancedStatusCode(escImpl);
			}
			return flag;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x0000D0C2 File Offset: 0x0000B2C2
		public static bool IsValid(string status)
		{
			return EnhancedStatusCodeImpl.IsValid(status);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x0000D0CA File Offset: 0x0000B2CA
		public static bool operator ==(EnhancedStatusCode val1, EnhancedStatusCode val2)
		{
			return object.ReferenceEquals(val1, val2) || (!object.ReferenceEquals(val1, null) && !object.ReferenceEquals(val2, null) && val1.Value == val2.Value);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x0000D0FC File Offset: 0x0000B2FC
		public static bool operator !=(EnhancedStatusCode val1, EnhancedStatusCode val2)
		{
			return !(val1 == val2);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x0000D108 File Offset: 0x0000B308
		public override string ToString()
		{
			return this.enhancedStatusCodeImpl.Value;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x0000D118 File Offset: 0x0000B318
		public override bool Equals(object other)
		{
			EnhancedStatusCode enhancedStatusCode = other as EnhancedStatusCode;
			return enhancedStatusCode != null && this == enhancedStatusCode;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0000D13E File Offset: 0x0000B33E
		public override int GetHashCode()
		{
			return this.enhancedStatusCodeImpl.Value.GetHashCode();
		}

		// Token: 0x0400012F RID: 303
		private EnhancedStatusCodeImpl enhancedStatusCodeImpl;
	}
}
