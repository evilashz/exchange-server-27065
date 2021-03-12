using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A75 RID: 2677
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class E164NumberData : OptionsPropertyChangeTracker
	{
		// Token: 0x06004BEC RID: 19436 RVA: 0x00105EB4 File Offset: 0x001040B4
		public E164NumberData()
		{
		}

		// Token: 0x06004BED RID: 19437 RVA: 0x00105EBC File Offset: 0x001040BC
		internal E164NumberData(E164Number number)
		{
			this.CountryCode = number.CountryCode;
			this.SignificantNumber = number.SignificantNumber;
		}

		// Token: 0x17001178 RID: 4472
		// (get) Token: 0x06004BEE RID: 19438 RVA: 0x00105EDC File Offset: 0x001040DC
		// (set) Token: 0x06004BEF RID: 19439 RVA: 0x00105EE4 File Offset: 0x001040E4
		[DataMember]
		public string CountryCode { get; set; }

		// Token: 0x17001179 RID: 4473
		// (get) Token: 0x06004BF0 RID: 19440 RVA: 0x00105EED File Offset: 0x001040ED
		// (set) Token: 0x06004BF1 RID: 19441 RVA: 0x00105EF5 File Offset: 0x001040F5
		[DataMember]
		public string SignificantNumber { get; set; }
	}
}
