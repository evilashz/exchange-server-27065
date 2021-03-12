using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000324 RID: 804
	[DataContract]
	public class DeviceClassPickerFilter : WebServiceParameters
	{
		// Token: 0x17001EC1 RID: 7873
		// (get) Token: 0x06002ED6 RID: 11990 RVA: 0x0008EFD0 File Offset: 0x0008D1D0
		public override string AssociatedCmdlet
		{
			get
			{
				return "Get-ActiveSyncDeviceClass";
			}
		}

		// Token: 0x17001EC2 RID: 7874
		// (get) Token: 0x06002ED7 RID: 11991 RVA: 0x0008EFD7 File Offset: 0x0008D1D7
		public override string RbacScope
		{
			get
			{
				return "@C:OrganizationConfig";
			}
		}

		// Token: 0x17001EC3 RID: 7875
		// (get) Token: 0x06002ED8 RID: 11992 RVA: 0x0008EFDE File Offset: 0x0008D1DE
		// (set) Token: 0x06002ED9 RID: 11993 RVA: 0x0008EFE6 File Offset: 0x0008D1E6
		[DataMember]
		public string DeviceType { get; set; }

		// Token: 0x17001EC4 RID: 7876
		// (get) Token: 0x06002EDA RID: 11994 RVA: 0x0008EFEF File Offset: 0x0008D1EF
		// (set) Token: 0x06002EDB RID: 11995 RVA: 0x0008EFF7 File Offset: 0x0008D1F7
		[DataMember]
		public bool GroupDeviceType { get; set; }

		// Token: 0x17001EC5 RID: 7877
		// (get) Token: 0x06002EDC RID: 11996 RVA: 0x0008F000 File Offset: 0x0008D200
		// (set) Token: 0x06002EDD RID: 11997 RVA: 0x0008F012 File Offset: 0x0008D212
		public string Filter
		{
			get
			{
				return (string)base["Filter"];
			}
			set
			{
				base["Filter"] = value;
			}
		}
	}
}
