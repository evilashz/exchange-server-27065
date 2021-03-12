using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003DA RID: 986
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class StructuredErrors
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x000777F6 File Offset: 0x000759F6
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x000777FE File Offset: 0x000759FE
		[DataMember]
		public string[] Path { get; set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x00077807 File Offset: 0x00075A07
		[DataMember]
		public StructuredError[] Error
		{
			get
			{
				if (this.nestedErrors == null)
				{
					return null;
				}
				return this.nestedErrors.ToArray();
			}
		}

		// Token: 0x06001F99 RID: 8089 RVA: 0x0007781E File Offset: 0x00075A1E
		public void AddError(StructuredError error)
		{
			if (this.nestedErrors == null)
			{
				this.nestedErrors = new List<StructuredError>(1);
			}
			this.nestedErrors.Add(error);
		}

		// Token: 0x040011F9 RID: 4601
		private List<StructuredError> nestedErrors;
	}
}
