using System;
using System.Xml;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Core.DataConverter
{
	// Token: 0x02000177 RID: 375
	internal class XmlTextWithStreamingBehavior : XmlText
	{
		// Token: 0x06000AC4 RID: 2756 RVA: 0x000340EF File Offset: 0x000322EF
		public XmlTextWithStreamingBehavior(XmlDocument doc, object item, object copyBuffer, WriteStreamData writeData, params object[] parameters) : base("_WS_ToReplace_", doc)
		{
			this.item = item;
			this.copyBuffer = copyBuffer;
			this.writeData = writeData;
			this.parameters = parameters;
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0003411B File Offset: 0x0003231B
		public override void WriteTo(XmlWriter writer)
		{
			this.writeData(writer, this.item, this.copyBuffer, this.parameters);
		}

		// Token: 0x040007D4 RID: 2004
		private const string TextString = "_WS_ToReplace_";

		// Token: 0x040007D5 RID: 2005
		private object item;

		// Token: 0x040007D6 RID: 2006
		private object copyBuffer;

		// Token: 0x040007D7 RID: 2007
		private WriteStreamData writeData;

		// Token: 0x040007D8 RID: 2008
		private object[] parameters;
	}
}
