using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Net.LiveIDAuthentication
{
	// Token: 0x02000765 RID: 1893
	internal sealed class SamlAuthenticationToken : BaseAuthenticationToken
	{
		// Token: 0x0600255A RID: 9562 RVA: 0x0004E6FC File Offset: 0x0004C8FC
		public SamlAuthenticationToken(string assertionXml)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("assertionXml", assertionXml);
			this.assertionXml = assertionXml;
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x0600255B RID: 9563 RVA: 0x0004E716 File Offset: 0x0004C916
		public string AssertionXml
		{
			get
			{
				return this.assertionXml;
			}
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x0004E71E File Offset: 0x0004C91E
		public override string ToString()
		{
			return this.assertionXml;
		}

		// Token: 0x040022B6 RID: 8886
		private readonly string assertionXml;
	}
}
