using System;
using System.IO;
using System.Text;

namespace Microsoft.Exchange.Net.WebApplicationClient
{
	// Token: 0x02000B20 RID: 2848
	internal abstract class TextBody : RequestBody
	{
		// Token: 0x06003D7C RID: 15740 RVA: 0x000A02A4 File Offset: 0x0009E4A4
		public sealed override void Write(Stream writeStream)
		{
			using (StreamWriter streamWriter = new StreamWriter(writeStream, Encoding.ASCII))
			{
				this.Write(streamWriter);
			}
		}

		// Token: 0x06003D7D RID: 15741
		public abstract void Write(TextWriter writer);
	}
}
