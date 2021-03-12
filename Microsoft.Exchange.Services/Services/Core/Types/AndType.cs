using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200063C RID: 1596
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "And")]
	[Serializable]
	public class AndType : MultipleOperandBooleanExpressionType
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x060031BC RID: 12732 RVA: 0x000B746A File Offset: 0x000B566A
		internal override string FilterType
		{
			get
			{
				return "And";
			}
		}
	}
}
