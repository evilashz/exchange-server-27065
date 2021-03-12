using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064A RID: 1610
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsGreaterThan")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class IsGreaterThanType : TwoOperandExpressionType
	{
		// Token: 0x17000B3B RID: 2875
		// (get) Token: 0x060031F8 RID: 12792 RVA: 0x000B76A1 File Offset: 0x000B58A1
		internal override string FilterType
		{
			get
			{
				return "IsGreaterThan";
			}
		}
	}
}
