using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000649 RID: 1609
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsGreaterThanOrEqualTo")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class IsGreaterThanOrEqualToType : TwoOperandExpressionType
	{
		// Token: 0x17000B3A RID: 2874
		// (get) Token: 0x060031F6 RID: 12790 RVA: 0x000B7692 File Offset: 0x000B5892
		internal override string FilterType
		{
			get
			{
				return "IsGreaterThanOrEqualTo";
			}
		}
	}
}
