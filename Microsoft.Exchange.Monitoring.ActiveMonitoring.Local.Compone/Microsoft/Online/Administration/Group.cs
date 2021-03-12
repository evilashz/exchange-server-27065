using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Online.Administration
{
	// Token: 0x020003DC RID: 988
	[DebuggerStepThrough]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DataContract(Name = "Group", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.Administration")]
	public class Group : IExtensibleDataObject
	{
		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x06001807 RID: 6151 RVA: 0x0008CDB3 File Offset: 0x0008AFB3
		// (set) Token: 0x06001808 RID: 6152 RVA: 0x0008CDBB File Offset: 0x0008AFBB
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

		// Token: 0x17000552 RID: 1362
		// (get) Token: 0x06001809 RID: 6153 RVA: 0x0008CDC4 File Offset: 0x0008AFC4
		// (set) Token: 0x0600180A RID: 6154 RVA: 0x0008CDCC File Offset: 0x0008AFCC
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

		// Token: 0x17000553 RID: 1363
		// (get) Token: 0x0600180B RID: 6155 RVA: 0x0008CDD5 File Offset: 0x0008AFD5
		// (set) Token: 0x0600180C RID: 6156 RVA: 0x0008CDDD File Offset: 0x0008AFDD
		[DataMember]
		public string Description
		{
			get
			{
				return this.DescriptionField;
			}
			set
			{
				this.DescriptionField = value;
			}
		}

		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600180D RID: 6157 RVA: 0x0008CDE6 File Offset: 0x0008AFE6
		// (set) Token: 0x0600180E RID: 6158 RVA: 0x0008CDEE File Offset: 0x0008AFEE
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

		// Token: 0x17000555 RID: 1365
		// (get) Token: 0x0600180F RID: 6159 RVA: 0x0008CDF7 File Offset: 0x0008AFF7
		// (set) Token: 0x06001810 RID: 6160 RVA: 0x0008CDFF File Offset: 0x0008AFFF
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

		// Token: 0x17000556 RID: 1366
		// (get) Token: 0x06001811 RID: 6161 RVA: 0x0008CE08 File Offset: 0x0008B008
		// (set) Token: 0x06001812 RID: 6162 RVA: 0x0008CE10 File Offset: 0x0008B010
		[DataMember]
		public ValidationError[] Errors
		{
			get
			{
				return this.ErrorsField;
			}
			set
			{
				this.ErrorsField = value;
			}
		}

		// Token: 0x17000557 RID: 1367
		// (get) Token: 0x06001813 RID: 6163 RVA: 0x0008CE19 File Offset: 0x0008B019
		// (set) Token: 0x06001814 RID: 6164 RVA: 0x0008CE21 File Offset: 0x0008B021
		[DataMember]
		public GroupType? GroupType
		{
			get
			{
				return this.GroupTypeField;
			}
			set
			{
				this.GroupTypeField = value;
			}
		}

		// Token: 0x17000558 RID: 1368
		// (get) Token: 0x06001815 RID: 6165 RVA: 0x0008CE2A File Offset: 0x0008B02A
		// (set) Token: 0x06001816 RID: 6166 RVA: 0x0008CE32 File Offset: 0x0008B032
		[DataMember]
		public bool? IsSystem
		{
			get
			{
				return this.IsSystemField;
			}
			set
			{
				this.IsSystemField = value;
			}
		}

		// Token: 0x17000559 RID: 1369
		// (get) Token: 0x06001817 RID: 6167 RVA: 0x0008CE3B File Offset: 0x0008B03B
		// (set) Token: 0x06001818 RID: 6168 RVA: 0x0008CE43 File Offset: 0x0008B043
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

		// Token: 0x1700055A RID: 1370
		// (get) Token: 0x06001819 RID: 6169 RVA: 0x0008CE4C File Offset: 0x0008B04C
		// (set) Token: 0x0600181A RID: 6170 RVA: 0x0008CE54 File Offset: 0x0008B054
		[DataMember]
		public string ManagedBy
		{
			get
			{
				return this.ManagedByField;
			}
			set
			{
				this.ManagedByField = value;
			}
		}

		// Token: 0x1700055B RID: 1371
		// (get) Token: 0x0600181B RID: 6171 RVA: 0x0008CE5D File Offset: 0x0008B05D
		// (set) Token: 0x0600181C RID: 6172 RVA: 0x0008CE65 File Offset: 0x0008B065
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

		// Token: 0x1700055C RID: 1372
		// (get) Token: 0x0600181D RID: 6173 RVA: 0x0008CE6E File Offset: 0x0008B06E
		// (set) Token: 0x0600181E RID: 6174 RVA: 0x0008CE76 File Offset: 0x0008B076
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

		// Token: 0x04001101 RID: 4353
		private ExtensionDataObject extensionDataField;

		// Token: 0x04001102 RID: 4354
		private string CommonNameField;

		// Token: 0x04001103 RID: 4355
		private string DescriptionField;

		// Token: 0x04001104 RID: 4356
		private string DisplayNameField;

		// Token: 0x04001105 RID: 4357
		private string EmailAddressField;

		// Token: 0x04001106 RID: 4358
		private ValidationError[] ErrorsField;

		// Token: 0x04001107 RID: 4359
		private GroupType? GroupTypeField;

		// Token: 0x04001108 RID: 4360
		private bool? IsSystemField;

		// Token: 0x04001109 RID: 4361
		private DateTime? LastDirSyncTimeField;

		// Token: 0x0400110A RID: 4362
		private string ManagedByField;

		// Token: 0x0400110B RID: 4363
		private Guid? ObjectIdField;

		// Token: 0x0400110C RID: 4364
		private ValidationStatus? ValidationStatusField;
	}
}
