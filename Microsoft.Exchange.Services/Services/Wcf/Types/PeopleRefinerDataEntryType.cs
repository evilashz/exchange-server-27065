using System;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000B37 RID: 2871
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[XmlType(TypeName = "PeopleRefinerDataEntryType", Namespace = "http://schemas.microsoft.com/exchange/services/2006/types")]
	[Serializable]
	public class PeopleRefinerDataEntryType : RefinerDataEntryType
	{
		// Token: 0x06005163 RID: 20835 RVA: 0x0010A748 File Offset: 0x00108948
		public PeopleRefinerDataEntryType()
		{
		}

		// Token: 0x06005164 RID: 20836 RVA: 0x0010A750 File Offset: 0x00108950
		public PeopleRefinerDataEntryType(string displayName, string smtpAddress, long hitCount, string refinementQuery) : base(hitCount, refinementQuery)
		{
			this.DisplayName = displayName;
			this.SmtpAddress = smtpAddress;
		}

		// Token: 0x17001392 RID: 5010
		// (get) Token: 0x06005165 RID: 20837 RVA: 0x0010A769 File Offset: 0x00108969
		// (set) Token: 0x06005166 RID: 20838 RVA: 0x0010A771 File Offset: 0x00108971
		[DataMember(IsRequired = true)]
		public string DisplayName { get; set; }

		// Token: 0x17001393 RID: 5011
		// (get) Token: 0x06005167 RID: 20839 RVA: 0x0010A77A File Offset: 0x0010897A
		// (set) Token: 0x06005168 RID: 20840 RVA: 0x0010A782 File Offset: 0x00108982
		[DataMember(IsRequired = true)]
		public string SmtpAddress { get; set; }
	}
}
