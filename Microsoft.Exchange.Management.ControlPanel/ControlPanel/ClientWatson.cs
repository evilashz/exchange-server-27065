using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000279 RID: 633
	[DataContract]
	public class ClientWatson
	{
		// Token: 0x17001CBB RID: 7355
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x000834A5 File Offset: 0x000816A5
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x000834AD File Offset: 0x000816AD
		[DataMember]
		public string Message { get; set; }

		// Token: 0x17001CBC RID: 7356
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x000834B6 File Offset: 0x000816B6
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x000834BE File Offset: 0x000816BE
		[DataMember]
		public string Url { get; set; }

		// Token: 0x17001CBD RID: 7357
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x000834C7 File Offset: 0x000816C7
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x000834CF File Offset: 0x000816CF
		[DataMember]
		public string Location { get; set; }

		// Token: 0x17001CBE RID: 7358
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x000834D8 File Offset: 0x000816D8
		// (set) Token: 0x060029B9 RID: 10681 RVA: 0x000834E0 File Offset: 0x000816E0
		[DataMember]
		public string StackTrace { get; set; }

		// Token: 0x17001CBF RID: 7359
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x000834E9 File Offset: 0x000816E9
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x000834F1 File Offset: 0x000816F1
		[DataMember]
		public string RequestId { get; set; }

		// Token: 0x17001CC0 RID: 7360
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000834FA File Offset: 0x000816FA
		// (set) Token: 0x060029BD RID: 10685 RVA: 0x00083502 File Offset: 0x00081702
		[DataMember]
		public string Time { get; set; }

		// Token: 0x17001CC1 RID: 7361
		// (get) Token: 0x060029BE RID: 10686 RVA: 0x0008350B File Offset: 0x0008170B
		// (set) Token: 0x060029BF RID: 10687 RVA: 0x00083513 File Offset: 0x00081713
		[DataMember]
		public string ErrorType { get; set; }
	}
}
