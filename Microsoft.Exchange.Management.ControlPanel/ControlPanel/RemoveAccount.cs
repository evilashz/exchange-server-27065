using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000290 RID: 656
	[DataContract]
	public class RemoveAccount : WebServiceParameters
	{
		// Token: 0x17001D29 RID: 7465
		// (get) Token: 0x06002AC3 RID: 10947 RVA: 0x00085E17 File Offset: 0x00084017
		public override string AssociatedCmdlet
		{
			get
			{
				return "Remove-Mailbox";
			}
		}

		// Token: 0x17001D2A RID: 7466
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x00085E1E File Offset: 0x0008401E
		public override string RbacScope
		{
			get
			{
				return "@W:Organization";
			}
		}

		// Token: 0x17001D2B RID: 7467
		// (get) Token: 0x06002AC5 RID: 10949 RVA: 0x00085E25 File Offset: 0x00084025
		// (set) Token: 0x06002AC6 RID: 10950 RVA: 0x00085E41 File Offset: 0x00084041
		[DataMember]
		public bool KeepWindowsLiveID
		{
			get
			{
				return (bool)(base["KeepWindowsLiveID"] ?? false);
			}
			set
			{
				base["KeepWindowsLiveID"] = value;
			}
		}
	}
}
