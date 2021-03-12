using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000645 RID: 1605
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange", Name = "IsEqualTo")]
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class IsEqualToType : TwoOperandExpressionType
	{
		// Token: 0x17000B36 RID: 2870
		// (get) Token: 0x060031ED RID: 12781 RVA: 0x000B764C File Offset: 0x000B584C
		internal override string FilterType
		{
			get
			{
				return "IsEqualTo";
			}
		}
	}
}
