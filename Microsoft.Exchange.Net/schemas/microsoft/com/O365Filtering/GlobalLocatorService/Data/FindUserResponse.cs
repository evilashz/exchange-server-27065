using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C37 RID: 3127
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	[DataContract(Name = "FindUserResponse", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class FindUserResponse : ResponseBase
	{
		// Token: 0x1700113E RID: 4414
		// (get) Token: 0x06004480 RID: 17536 RVA: 0x000B6D59 File Offset: 0x000B4F59
		// (set) Token: 0x06004481 RID: 17537 RVA: 0x000B6D61 File Offset: 0x000B4F61
		[DataMember]
		public TenantInfo TenantInfo
		{
			get
			{
				return this.TenantInfoField;
			}
			set
			{
				this.TenantInfoField = value;
			}
		}

		// Token: 0x1700113F RID: 4415
		// (get) Token: 0x06004482 RID: 17538 RVA: 0x000B6D6A File Offset: 0x000B4F6A
		// (set) Token: 0x06004483 RID: 17539 RVA: 0x000B6D72 File Offset: 0x000B4F72
		[DataMember]
		public UserInfo UserInfo
		{
			get
			{
				return this.UserInfoField;
			}
			set
			{
				this.UserInfoField = value;
			}
		}

		// Token: 0x040039FF RID: 14847
		private TenantInfo TenantInfoField;

		// Token: 0x04003A00 RID: 14848
		private UserInfo UserInfoField;
	}
}
