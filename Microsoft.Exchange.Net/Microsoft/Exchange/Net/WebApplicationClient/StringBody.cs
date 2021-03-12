using System;
using System.IO;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B2B RID: 2859
	internal class StringBody : TextBody
	{
		// Token: 0x06003DAB RID: 15787 RVA: 0x000A09A4 File Offset: 0x0009EBA4
		public StringBody(string body)
		{
			this.Body = body;
		}

		// Token: 0x17000F41 RID: 3905
		// (get) Token: 0x06003DAC RID: 15788 RVA: 0x000A09B3 File Offset: 0x0009EBB3
		// (set) Token: 0x06003DAD RID: 15789 RVA: 0x000A09BB File Offset: 0x0009EBBB
		public string Body { get; private set; }

		// Token: 0x06003DAE RID: 15790 RVA: 0x000A09C4 File Offset: 0x0009EBC4
		public override void Write(TextWriter writer)
		{
			if (this.Body != null)
			{
				writer.Write(this.Body);
			}
		}
	}
}
