using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000730 RID: 1840
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/messages")]
	[Serializable]
	public class ConversationResponseShape : ResponseShape
	{
		// Token: 0x060037A2 RID: 14242 RVA: 0x000C5B73 File Offset: 0x000C3D73
		public ConversationResponseShape()
		{
		}

		// Token: 0x060037A3 RID: 14243 RVA: 0x000C5B7B File Offset: 0x000C3D7B
		internal ConversationResponseShape(ShapeEnum baseShape, PropertyPath[] additionalProperties) : base(baseShape, additionalProperties)
		{
		}

		// Token: 0x17000D1F RID: 3359
		// (get) Token: 0x060037A4 RID: 14244 RVA: 0x000C5B85 File Offset: 0x000C3D85
		// (set) Token: 0x060037A5 RID: 14245 RVA: 0x000C5B8D File Offset: 0x000C3D8D
		[XmlIgnore]
		[DataMember(IsRequired = false)]
		public bool InferenceEnabled { get; set; }
	}
}
