using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DD RID: 989
	[DataContract(Name = "GroupMember", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public class GroupMember : IExtensibleDataObject
	{
		// Token: 0x1700055D RID: 1373
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x0008CE87 File Offset: 0x0008B087
		// (set) Token: 0x06001821 RID: 6177 RVA: 0x0008CE8F File Offset: 0x0008B08F
		public ExtensionDataObject ExtensionData
		{
			get
			{
				return this.extensionDataField;
			}
			set
			{
				this.extensionDataField = value;
			}
		}

		// Token: 0x1700055E RID: 1374
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x0008CE98 File Offset: 0x0008B098
		// (set) Token: 0x06001823 RID: 6179 RVA: 0x0008CEA0 File Offset: 0x0008B0A0
		[DataMember]
		public string CommonName
		{
			get
			{
				return this.CommonNameField;
			}
			set
			{
				this.CommonNameField = value;
			}
		}

		// Token: 0x1700055F RID: 1375
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x0008CEA9 File Offset: 0x0008B0A9
		// (set) Token: 0x06001825 RID: 6181 RVA: 0x0008CEB1 File Offset: 0x0008B0B1
		[DataMember]
		public string DisplayName
		{
			get
			{
				return this.DisplayNameField;
			}
			set
			{
				this.DisplayNameField = value;
			}
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001826 RID: 6182 RVA: 0x0008CEBA File Offset: 0x0008B0BA
		// (set) Token: 0x06001827 RID: 6183 RVA: 0x0008CEC2 File Offset: 0x0008B0C2
		[DataMember]
		public string EmailAddress
		{
			get
			{
				return this.EmailAddressField;
			}
			set
			{
				this.EmailAddressField = value;
			}
		}

		// Token: 0x17000561 RID: 1377
		// (get) Token: 0x06001828 RID: 6184 RVA: 0x0008CECB File Offset: 0x0008B0CB
		// (set) Token: 0x06001829 RID: 6185 RVA: 0x0008CED3 File Offset: 0x0008B0D3
		[DataMember]
		public GroupMemberType GroupMemberType
		{
			get
			{
				return this.GroupMemberTypeField;
			}
			set
			{
				this.GroupMemberTypeField = value;
			}
		}

		// Token: 0x17000562 RID: 1378
		// (get) Token: 0x0600182A RID: 6186 RVA: 0x0008CEDC File Offset: 0x0008B0DC
		// (set) Token: 0x0600182B RID: 6187 RVA: 0x0008CEE4 File Offset: 0x0008B0E4
		[DataMember]
		public bool? IsLicensed
		{
			get
			{
				return this.IsLicensedField;
			}
			set
			{
				this.IsLicensedField = value;
			}
		}

		// Token: 0x17000563 RID: 1379
		// (get) Token: 0x0600182C RID: 6188 RVA: 0x0008CEED File Offset: 0x0008B0ED
		// (set) Token: 0x0600182D RID: 6189 RVA: 0x0008CEF5 File Offset: 0x0008B0F5
		[DataMember]
		public DateTime? LastDirSyncTime
		{
			get
			{
				return this.LastDirSyncTimeField;
			}
			set
			{
				this.LastDirSyncTimeField = value;
			}
		}

		// Token: 0x17000564 RID: 1380
		// (get) Token: 0x0600182E RID: 6190 RVA: 0x0008CEFE File Offset: 0x0008B0FE
		// (set) Token: 0x0600182F RID: 6191 RVA: 0x0008CF06 File Offset: 0x0008B106
		[DataMember]
		public Guid? ObjectId
		{
			get
			{
				return this.ObjectIdField;
			}
			set
			{
				this.ObjectIdField = value;
			}
		}

		// Token: 0x17000565 RID: 1381
		// (get) Token: 0x06001830 RID: 6192 RVA: 0x0008CF0F File Offset: 0x0008B10F
		// (set) Token: 0x06001831 RID: 6193 RVA: 0x0008CF17 File Offset: 0x0008B117
		[DataMember]
		public ProvisioningStatus? OverallProvisioningStatus
		{
			get
			{
				return this.OverallProvisioningStatusField;
			}
			set
			{
				this.OverallProvisioningStatusField = value;
			}
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001832 RID: 6194 RVA: 0x0008CF20 File Offset: 0x0008B120
		// (set) Token: 0x06001833 RID: 6195 RVA: 0x0008CF28 File Offset: 0x0008B128
		[DataMember]
		public ValidationStatus? ValidationStatus
		{
			get
			{
				return this.ValidationStatusField;
			}
			set
			{
				this.ValidationStatusField = value;
			}
		}

		// Token: 0x0400110D RID: 4365
		private ExtensionDataObject extensionDataField;

		// Token: 0x0400110E RID: 4366
		private string CommonNameField;

		// Token: 0x0400110F RID: 4367
		private string DisplayNameField;

		// Token: 0x04001110 RID: 4368
		private string EmailAddressField;

		// Token: 0x04001111 RID: 4369
		private GroupMemberType GroupMemberTypeField;

		// Token: 0x04001112 RID: 4370
		private bool? IsLicensedField;

		// Token: 0x04001113 RID: 4371
		private DateTime? LastDirSyncTimeField;

		// Token: 0x04001114 RID: 4372
		private Guid? ObjectIdField;

		// Token: 0x04001115 RID: 4373
		private ProvisioningStatus? OverallProvisioningStatusField;

		// Token: 0x04001116 RID: 4374
		private ValidationStatus? ValidationStatusField;
	}
}
