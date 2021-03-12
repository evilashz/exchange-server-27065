using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Microsoft.Exchange.SoapWebClient.EWS
{
	// Token: 0x02000230 RID: 560
	[XmlType(Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[DesignerCategory("code")]
	[GeneratedCode("wsdl", "4.0.30319.17627")]
	[DebuggerStepThrough]
	[Serializable]
	public class CompleteNameType
	{
		// Token: 0x04000E81 RID: 3713
		public string Title;

		// Token: 0x04000E82 RID: 3714
		public string FirstName;

		// Token: 0x04000E83 RID: 3715
		public string MiddleName;

		// Token: 0x04000E84 RID: 3716
		public string LastName;

		// Token: 0x04000E85 RID: 3717
		public string Suffix;

		// Token: 0x04000E86 RID: 3718
		public string Initials;

		// Token: 0x04000E87 RID: 3719
		public string FullName;

		// Token: 0x04000E88 RID: 3720
		public string Nickname;

		// Token: 0x04000E89 RID: 3721
		public string YomiFirstName;

		// Token: 0x04000E8A RID: 3722
		public string YomiLastName;
	}
}
