using System;

namespace Microsoft.Exchange.Management.ReportingTask.Common
{
	// Token: 0x020006B0 RID: 1712
	[AttributeUsage(AttributeTargets.Property, Inherited = false)]
	public class SuppressPiiAttribute : Attribute
	{
		// Token: 0x17001219 RID: 4633
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x00101EF4 File Offset: 0x001000F4
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x00101EFC File Offset: 0x001000FC
		public PiiDataType PiiDataType { get; set; }
	}
}
