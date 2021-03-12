using System;
using System.IO;
using System.Web.Util;
using Microsoft.Security.Application;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000544 RID: 1348
	public class AntiXssEncoder : HttpEncoder
	{
		// Token: 0x06003F6A RID: 16234 RVA: 0x000BF163 File Offset: 0x000BD363
		protected override void HtmlEncode(string value, TextWriter output)
		{
			output.Write(Encoder.HtmlEncode(value));
		}

		// Token: 0x06003F6B RID: 16235 RVA: 0x000BF171 File Offset: 0x000BD371
		protected override void HtmlAttributeEncode(string value, TextWriter output)
		{
			output.Write(Encoder.HtmlAttributeEncode(value));
		}
	}
}
