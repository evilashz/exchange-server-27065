using System;
using System.Security.Principal;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000252 RID: 594
	internal class SuggesterSidCompositeKey
	{
		// Token: 0x06000F8C RID: 3980 RVA: 0x0004CA13 File Offset: 0x0004AC13
		public SuggesterSidCompositeKey(SecurityIdentifier sid, string fqdn)
		{
			if (sid == null)
			{
				throw new ArgumentNullException("sid");
			}
			this.key = SuggesterSidCompositeKey.BuildKey(sid, fqdn);
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x0004CA3C File Offset: 0x0004AC3C
		internal static string BuildKey(SecurityIdentifier sid, string fqdn)
		{
			return string.Format("{0}{1}", sid, fqdn);
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000F8E RID: 3982 RVA: 0x0004CA4A File Offset: 0x0004AC4A
		public string Key
		{
			get
			{
				return this.key;
			}
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x0004CA52 File Offset: 0x0004AC52
		public override string ToString()
		{
			return this.Key;
		}

		// Token: 0x04000BCF RID: 3023
		private string key;
	}
}
