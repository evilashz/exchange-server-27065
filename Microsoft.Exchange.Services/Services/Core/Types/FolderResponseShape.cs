using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200078E RID: 1934
	[XmlType(TypeName = "FolderResponseShapeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class FolderResponseShape : ResponseShape
	{
		// Token: 0x060039AB RID: 14763 RVA: 0x000CB817 File Offset: 0x000C9A17
		public FolderResponseShape()
		{
		}

		// Token: 0x060039AC RID: 14764 RVA: 0x000CB81F File Offset: 0x000C9A1F
		internal FolderResponseShape(ShapeEnum baseShape, PropertyPath[] additionalProperties) : base(baseShape, additionalProperties)
		{
		}
	}
}
