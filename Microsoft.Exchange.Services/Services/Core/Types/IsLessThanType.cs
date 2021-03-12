using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000648 RID: 1608
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsLessThan")]
	[Serializable]
	public class IsLessThanType : TwoOperandExpressionType
	{
		// Token: 0x17000B39 RID: 2873
		// (get) Token: 0x060031F4 RID: 12788 RVA: 0x000B7683 File Offset: 0x000B5883
		internal override string FilterType
		{
			get
			{
				return "IsLessThan";
			}
		}
	}
}
