using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000647 RID: 1607
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsLessThanOrEqualTo")]
	[Serializable]
	public class IsLessThanOrEqualToType : TwoOperandExpressionType
	{
		// Token: 0x17000B38 RID: 2872
		// (get) Token: 0x060031F2 RID: 12786 RVA: 0x000B7674 File Offset: 0x000B5874
		internal override string FilterType
		{
			get
			{
				return "IsLessThanOrEqualTo";
			}
		}
	}
}
