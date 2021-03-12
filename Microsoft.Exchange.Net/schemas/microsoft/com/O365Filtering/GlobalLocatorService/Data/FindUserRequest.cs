using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C46 RID: 3142
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "FindUserRequest", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindUserRequest : IExtensibleDataObject
	{
		// Token: 0x1700116A RID: 4458
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x000B70B5 File Offset: 0x000B52B5
		// (set) Token: 0x060044E7 RID: 17639 RVA: 0x000B70BD File Offset: 0x000B52BD
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

		// Token: 0x1700116B RID: 4459
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x000B70C6 File Offset: 0x000B52C6
		// (set) Token: 0x060044E9 RID: 17641 RVA: 0x000B70CE File Offset: 0x000B52CE
		[DataMember]
		public int ReadFlag
		{
			get
			{
				return this.ReadFlagField;
			}
			set
			{
				this.ReadFlagField = value;
			}
		}

		// Token: 0x1700116C RID: 4460
		// (get) Token: 0x060044EA RID: 17642 RVA: 0x000B70D7 File Offset: 0x000B52D7
		// (set) Token: 0x060044EB RID: 17643 RVA: 0x000B70DF File Offset: 0x000B52DF
		[DataMember]
		public TenantQuery Tenant
		{
			get
			{
				return this.TenantField;
			}
			set
			{
				this.TenantField = value;
			}
		}

		// Token: 0x1700116D RID: 4461
		// (get) Token: 0x060044EC RID: 17644 RVA: 0x000B70E8 File Offset: 0x000B52E8
		// (set) Token: 0x060044ED RID: 17645 RVA: 0x000B70F0 File Offset: 0x000B52F0
		[DataMember(IsRequired = true)]
		public UserQuery User
		{
			get
			{
				return this.UserField;
			}
			set
			{
				this.UserField = value;
			}
		}

		// Token: 0x04003A30 RID: 14896
		private ExtensionDataObject extensionDataField;

		// Token: 0x04003A31 RID: 14897
		private int ReadFlagField;

		// Token: 0x04003A32 RID: 14898
		private TenantQuery TenantField;

		// Token: 0x04003A33 RID: 14899
		private UserQuery UserField;
	}
}
