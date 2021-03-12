using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A70 RID: 2672
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class SetCalendarNotificationRequest : BaseJsonRequest
	{
		// Token: 0x17001170 RID: 4464
		// (get) Token: 0x06004BD4 RID: 19412 RVA: 0x00105DCE File Offset: 0x00103FCE
		// (set) Token: 0x06004BD5 RID: 19413 RVA: 0x00105DD6 File Offset: 0x00103FD6
		[DataMember(IsRequired = true)]
		public CalendarNotificationOptions Options { get; set; }

		// Token: 0x06004BD6 RID: 19414 RVA: 0x00105DDF File Offset: 0x00103FDF
		public override string ToString()
		{
			return string.Format("SetCalendarNotificationRequest: {0}", this.Options);
		}
	}
}
