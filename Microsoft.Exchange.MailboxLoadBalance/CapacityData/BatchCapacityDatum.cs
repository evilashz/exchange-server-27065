using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxLoadBalance.CapacityData
{
	// Token: 0x02000023 RID: 35
	[DataContract]
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class BatchCapacityDatum : IExtensibleDataObject, IComparable<BatchCapacityDatum>
	{
		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000120 RID: 288 RVA: 0x000067C4 File Offset: 0x000049C4
		// (set) Token: 0x06000121 RID: 289 RVA: 0x000067CC File Offset: 0x000049CC
		public ExtensionDataObject ExtensionData { get; set; }

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000122 RID: 290 RVA: 0x000067D5 File Offset: 0x000049D5
		// (set) Token: 0x06000123 RID: 291 RVA: 0x000067DD File Offset: 0x000049DD
		[DataMember]
		public int MaximumNumberOfMailboxes { get; set; }

		// Token: 0x06000124 RID: 292 RVA: 0x000067E6 File Offset: 0x000049E6
		public int CompareTo(BatchCapacityDatum other)
		{
			return this.MaximumNumberOfMailboxes - other.MaximumNumberOfMailboxes;
		}

		// Token: 0x06000125 RID: 293 RVA: 0x000067F5 File Offset: 0x000049F5
		public override string ToString()
		{
			return string.Format("BatchDatum[{0} mailboxes]", this.MaximumNumberOfMailboxes);
		}
	}
}
