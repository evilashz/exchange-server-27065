using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063D RID: 1597
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "Or")]
	[Serializable]
	public class OrType : MultipleOperandBooleanExpressionType
	{
		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x060031BE RID: 12734 RVA: 0x000B7479 File Offset: 0x000B5679
		internal override string FilterType
		{
			get
			{
				return "Or";
			}
		}
	}
}
