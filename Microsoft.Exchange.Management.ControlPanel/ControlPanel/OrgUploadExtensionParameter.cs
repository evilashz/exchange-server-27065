using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001DA RID: 474
	[DataContract]
	public class OrgUploadExtensionParameter : UploadExtensionParameter
	{
		// Token: 0x17001B9A RID: 7066
		// (get) Token: 0x0600258E RID: 9614 RVA: 0x000735D6 File Offset: 0x000717D6
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}
	}
}
