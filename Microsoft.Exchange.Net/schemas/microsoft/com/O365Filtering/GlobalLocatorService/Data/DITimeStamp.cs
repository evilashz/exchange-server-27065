using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C27 RID: 3111
	[KnownType(typeof(DIDomainInfo))]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[KnownType(typeof(DITenantInfo))]
	[DebuggerStepThrough]
	[KnownType(typeof(GLSProperty))]
	[DataContract(Name = "DITimeStamp", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class DITimeStamp : IExtensibleDataObject
	{
		// Token: 0x1700111D RID: 4381
		// (get) Token: 0x06004430 RID: 17456 RVA: 0x000B6AB9 File Offset: 0x000B4CB9
		// (set) Token: 0x06004431 RID: 17457 RVA: 0x000B6AC1 File Offset: 0x000B4CC1
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

		// Token: 0x1700111E RID: 4382
		// (get) Token: 0x06004432 RID: 17458 RVA: 0x000B6ACA File Offset: 0x000B4CCA
		// (set) Token: 0x06004433 RID: 17459 RVA: 0x000B6AD2 File Offset: 0x000B4CD2
		[DataMember]
		public DateTime? ChangedDatetime
		{
			get
			{
				return this.ChangedDatetimeField;
			}
			set
			{
				this.ChangedDatetimeField = value;
			}
		}

		// Token: 0x1700111F RID: 4383
		// (get) Token: 0x06004434 RID: 17460 RVA: 0x000B6ADB File Offset: 0x000B4CDB
		// (set) Token: 0x06004435 RID: 17461 RVA: 0x000B6AE3 File Offset: 0x000B4CE3
		[DataMember]
		public DateTime? CreatedDatetime
		{
			get
			{
				return this.CreatedDatetimeField;
			}
			set
			{
				this.CreatedDatetimeField = value;
			}
		}

		// Token: 0x17001120 RID: 4384
		// (get) Token: 0x06004436 RID: 17462 RVA: 0x000B6AEC File Offset: 0x000B4CEC
		// (set) Token: 0x06004437 RID: 17463 RVA: 0x000B6AF4 File Offset: 0x000B4CF4
		[DataMember]
		public DateTime? DeletedDatetime
		{
			get
			{
				return this.DeletedDatetimeField;
			}
			set
			{
				this.DeletedDatetimeField = value;
			}
		}

		// Token: 0x040039D8 RID: 14808
		private ExtensionDataObject extensionDataField;

		// Token: 0x040039D9 RID: 14809
		private DateTime? ChangedDatetimeField;

		// Token: 0x040039DA RID: 14810
		private DateTime? CreatedDatetimeField;

		// Token: 0x040039DB RID: 14811
		private DateTime? DeletedDatetimeField;
	}
}
