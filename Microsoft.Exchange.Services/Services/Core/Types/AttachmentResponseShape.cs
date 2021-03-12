using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x020006C2 RID: 1730
	[XmlType(TypeName = "AttachmentResponseShapeType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Name = "AttachmentResponseShapeType", Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class AttachmentResponseShape : ItemResponseShape
	{
		// Token: 0x06003566 RID: 13670 RVA: 0x000BFD65 File Offset: 0x000BDF65
		public AttachmentResponseShape()
		{
			base.BaseShape = ShapeEnum.AllProperties;
		}
	}
}
