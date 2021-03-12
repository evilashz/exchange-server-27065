using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020002E3 RID: 739
	[DataContract]
	public class RemoveUserPhotoParameters : WebServiceParameters
	{
		// Token: 0x17001E15 RID: 7701
		// (get) Token: 0x06002CFD RID: 11517 RVA: 0x0008A13F File Offset: 0x0008833F
		// (set) Token: 0x06002CFE RID: 11518 RVA: 0x0008A151 File Offset: 0x00088351
		[DataMember]
		public Identity Identity
		{
			get
			{
				return (Identity)base["Identity"];
			}
			set
			{
				base["Identity"] = value;
			}
		}

		// Token: 0x17001E16 RID: 7702
		// (get) Token: 0x06002CFF RID: 11519 RVA: 0x0008A15F File Offset: 0x0008835F
		public override string AssociatedCmdlet
		{
			get
			{
				return "Remove-UserPhoto";
			}
		}

		// Token: 0x17001E17 RID: 7703
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x0008A166 File Offset: 0x00088366
		public override string RbacScope
		{
			get
			{
				return "@R:Self";
			}
		}
	}
}
