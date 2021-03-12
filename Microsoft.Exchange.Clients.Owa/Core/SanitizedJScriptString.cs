using System;
using Microsoft.Exchange.Security;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020002E1 RID: 737
	public sealed class SanitizedJScriptString : SanitizedStringBase<OwaHtml>
	{
		// Token: 0x06001C54 RID: 7252 RVA: 0x000A266C File Offset: 0x000A086C
		public SanitizedJScriptString(string value) : base(value)
		{
			base.DecreeToBeUntrusted();
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x000A267B File Offset: 0x000A087B
		private SanitizedJScriptString()
		{
		}

		// Token: 0x06001C56 RID: 7254 RVA: 0x000A2684 File Offset: 0x000A0884
		public static SanitizedJScriptString StringAssignmentStatement(string varName, string varValue)
		{
			return new SanitizedJScriptString
			{
				TrustedValue = string.Concat(new string[]
				{
					"var ",
					varName,
					" = \"",
					StringSanitizer<OwaHtml>.PolicyObject.EscapeJScript(varValue),
					"\";"
				})
			};
		}

		// Token: 0x06001C57 RID: 7255 RVA: 0x000A26D5 File Offset: 0x000A08D5
		protected override string Sanitize(string rawValue)
		{
			return StringSanitizer<OwaHtml>.PolicyObject.EscapeJScript(rawValue);
		}
	}
}
