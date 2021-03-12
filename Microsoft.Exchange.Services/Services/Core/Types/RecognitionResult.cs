using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x0200085C RID: 2140
	[XmlType(TypeName = "RecognitionResultType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class RecognitionResult
	{
		// Token: 0x06003D86 RID: 15750 RVA: 0x000D798B File Offset: 0x000D5B8B
		public RecognitionResult()
		{
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000D7993 File Offset: 0x000D5B93
		internal RecognitionResult(string result)
		{
			this.Result = result;
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003D88 RID: 15752 RVA: 0x000D79A2 File Offset: 0x000D5BA2
		// (set) Token: 0x06003D89 RID: 15753 RVA: 0x000D79AA File Offset: 0x000D5BAA
		[XmlAttribute("Result", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
		[DataMember(IsRequired = true)]
		public string Result { get; set; }
	}
}
