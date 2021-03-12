using System;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000CE RID: 206
	[Serializable]
	public sealed class X400ProxyAddress : ProxyAddress
	{
		// Token: 0x0600055B RID: 1371 RVA: 0x00012E44 File Offset: 0x00011044
		public X400ProxyAddress(string address, bool isPrimaryAddress) : this(X400AddressParser.GetCanonical(address, false, out address), address, isPrimaryAddress)
		{
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00012E57 File Offset: 0x00011057
		private X400ProxyAddress(bool endingWithSemicolon, string address, bool isPrimaryAddress) : base(ProxyAddressPrefix.X400, address, isPrimaryAddress)
		{
			this.endingWithSemicolon = endingWithSemicolon;
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00012E6D File Offset: 0x0001106D
		internal bool EndingWithSemicolon
		{
			get
			{
				return this.endingWithSemicolon;
			}
		}

		// Token: 0x04000312 RID: 786
		private bool endingWithSemicolon;
	}
}
