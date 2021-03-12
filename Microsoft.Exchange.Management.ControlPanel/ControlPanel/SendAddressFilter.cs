using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E4 RID: 740
	[DataContract]
	public class SendAddressFilter : SelfMailboxParameters
	{
		// Token: 0x17001E18 RID: 7704
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x0008A175 File Offset: 0x00088375
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-SendAddress";
			}
		}

		// Token: 0x17001E19 RID: 7705
		// (get) Token: 0x06002D03 RID: 11523 RVA: 0x0008A17C File Offset: 0x0008837C
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}

		// Token: 0x17001E1A RID: 7706
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x0008A183 File Offset: 0x00088383
		// (set) Token: 0x06002D05 RID: 11525 RVA: 0x0008A195 File Offset: 0x00088395
		[DataMember]
		public string AddressId
		{
			get
			{
				return (string)base["AddressId"];
			}
			set
			{
				base["AddressId"] = value.Trim();
			}
		}
	}
}
