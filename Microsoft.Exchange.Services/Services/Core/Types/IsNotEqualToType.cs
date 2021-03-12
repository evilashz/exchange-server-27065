using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200064B RID: 1611
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsNotEqualTo")]
	[Serializable]
	public class IsNotEqualToType : TwoOperandExpressionType
	{
		// Token: 0x17000B3C RID: 2876
		// (get) Token: 0x060031FA RID: 12794 RVA: 0x000B76B0 File Offset: 0x000B58B0
		internal override string FilterType
		{
			get
			{
				return "IsNotEqualTo";
			}
		}
	}
}
