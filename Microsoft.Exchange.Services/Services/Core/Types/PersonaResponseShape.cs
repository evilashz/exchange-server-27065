using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000844 RID: 2116
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class PersonaResponseShape : ResponseShape
	{
		// Token: 0x06003CFE RID: 15614 RVA: 0x000D6FF1 File Offset: 0x000D51F1
		public PersonaResponseShape()
		{
		}

		// Token: 0x06003CFF RID: 15615 RVA: 0x000D6FF9 File Offset: 0x000D51F9
		internal PersonaResponseShape(ShapeEnum baseShape) : this(baseShape, null)
		{
		}

		// Token: 0x06003D00 RID: 15616 RVA: 0x000D7003 File Offset: 0x000D5203
		internal PersonaResponseShape(ShapeEnum baseShape, PropertyPath[] additionalProperties) : base(baseShape, additionalProperties)
		{
		}
	}
}
