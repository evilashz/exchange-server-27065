using System;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000595 RID: 1429
	[XmlType(TypeName = "RuleValidationErrorType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	public class RuleValidationError
	{
		// Token: 0x170006F9 RID: 1785
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000AB6C1 File Offset: 0x000A98C1
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x000AB6C9 File Offset: 0x000A98C9
		[XmlElement(Order = 0)]
		public RuleFieldURI FieldURI { get; set; }

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06002854 RID: 10324 RVA: 0x000AB6D2 File Offset: 0x000A98D2
		// (set) Token: 0x06002855 RID: 10325 RVA: 0x000AB6DA File Offset: 0x000A98DA
		[XmlElement(Order = 1)]
		public RuleValidationErrorCode ErrorCode { get; set; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06002856 RID: 10326 RVA: 0x000AB6E3 File Offset: 0x000A98E3
		// (set) Token: 0x06002857 RID: 10327 RVA: 0x000AB6EB File Offset: 0x000A98EB
		[XmlElement(Order = 2)]
		public string ErrorMessage { get; set; }

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002858 RID: 10328 RVA: 0x000AB6F4 File Offset: 0x000A98F4
		// (set) Token: 0x06002859 RID: 10329 RVA: 0x000AB6FC File Offset: 0x000A98FC
		[XmlElement(Order = 3)]
		public string FieldValue { get; set; }
	}
}
