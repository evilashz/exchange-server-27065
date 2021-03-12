using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Management.ClassificationDefinitions;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000203 RID: 515
	[DataContract]
	public class Fingerprint : BaseRow
	{
		// Token: 0x060026A8 RID: 9896 RVA: 0x000781D2 File Offset: 0x000763D2
		public Fingerprint(Fingerprint print)
		{
			this.Value = print.ToString();
			this.Description = print.Description;
		}

		// Token: 0x17001BE8 RID: 7144
		// (get) Token: 0x060026A9 RID: 9897 RVA: 0x000781F2 File Offset: 0x000763F2
		// (set) Token: 0x060026AA RID: 9898 RVA: 0x000781FA File Offset: 0x000763FA
		[DataMember]
		public string Value { get; set; }

		// Token: 0x17001BE9 RID: 7145
		// (get) Token: 0x060026AB RID: 9899 RVA: 0x00078203 File Offset: 0x00076403
		// (set) Token: 0x060026AC RID: 9900 RVA: 0x0007820B File Offset: 0x0007640B
		[DataMember]
		public string Description { get; set; }
	}
}
