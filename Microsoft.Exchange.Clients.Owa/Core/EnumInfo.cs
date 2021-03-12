using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002C0 RID: 704
	public class EnumInfo<T> where T : struct
	{
		// Token: 0x06001B80 RID: 7040 RVA: 0x0009DBE4 File Offset: 0x0009BDE4
		public EnumInfo(Strings.IDs stringIdValue, T enumValue)
		{
			this.enumValue = enumValue;
			this.stringIdValue = stringIdValue;
		}

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06001B81 RID: 7041 RVA: 0x0009DBFA File Offset: 0x0009BDFA
		public Strings.IDs StringIdValue
		{
			get
			{
				return this.stringIdValue;
			}
		}

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06001B82 RID: 7042 RVA: 0x0009DC02 File Offset: 0x0009BE02
		public T EnumValue
		{
			get
			{
				return this.enumValue;
			}
		}

		// Token: 0x040013F7 RID: 5111
		private T enumValue;

		// Token: 0x040013F8 RID: 5112
		private Strings.IDs stringIdValue;
	}
}
