using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000AD3 RID: 2771
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class RetentionPolicyTagDisplayCollection
	{
		// Token: 0x170012AB RID: 4779
		// (get) Token: 0x06004EE8 RID: 20200 RVA: 0x0010831F File Offset: 0x0010651F
		// (set) Token: 0x06004EE9 RID: 20201 RVA: 0x00108327 File Offset: 0x00106527
		[DataMember(IsRequired = true)]
		public RetentionPolicyTagDisplay[] RetentionPolicyTags { get; set; }

		// Token: 0x06004EEA RID: 20202 RVA: 0x00108338 File Offset: 0x00106538
		public override string ToString()
		{
			IEnumerable<string> values = from e in this.RetentionPolicyTags
			select e.ToString();
			return string.Join(";", values);
		}
	}
}
