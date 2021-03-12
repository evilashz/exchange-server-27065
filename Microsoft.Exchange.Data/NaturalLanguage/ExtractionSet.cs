using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Data.NaturalLanguage
{
	// Token: 0x0200007D RID: 125
	public abstract class ExtractionSet
	{
		// Token: 0x060003BB RID: 955
		public abstract XmlSerializer GetSerializer();

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x060003BC RID: 956
		public abstract bool IsEmpty { get; }
	}
}
