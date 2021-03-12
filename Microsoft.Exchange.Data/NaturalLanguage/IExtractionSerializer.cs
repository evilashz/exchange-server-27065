using System;
using System.Xml;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x02000073 RID: 115
	public interface IExtractionSerializer<T>
	{
		// Token: 0x0600039C RID: 924
		T[] ReadXml(XmlReader reader, Version version);

		// Token: 0x0600039D RID: 925
		void WriteXml(XmlWriter writer, T[] t);
	}
}
