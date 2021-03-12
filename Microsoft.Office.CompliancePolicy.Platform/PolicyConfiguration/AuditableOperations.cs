using System;
using System.Runtime.Serialization;

namespace Microsoft.Office.CompliancePolicy.PolicyConfiguration
{
	// Token: 0x0200008C RID: 140
	[DataContract]
	public enum AuditableOperations
	{
		// Token: 0x0400025F RID: 607
		[EnumMember]
		None,
		// Token: 0x04000260 RID: 608
		[EnumMember]
		Administrate,
		// Token: 0x04000261 RID: 609
		[EnumMember]
		CreateUpdate,
		// Token: 0x04000262 RID: 610
		[EnumMember]
		View,
		// Token: 0x04000263 RID: 611
		[EnumMember]
		MoveCopy,
		// Token: 0x04000264 RID: 612
		[EnumMember]
		Delete,
		// Token: 0x04000265 RID: 613
		[EnumMember]
		Forward,
		// Token: 0x04000266 RID: 614
		[EnumMember]
		SendAsOthers,
		// Token: 0x04000267 RID: 615
		[EnumMember]
		PermissionChange,
		// Token: 0x04000268 RID: 616
		[EnumMember]
		CheckOut,
		// Token: 0x04000269 RID: 617
		[EnumMember]
		CheckIn,
		// Token: 0x0400026A RID: 618
		[EnumMember]
		Workflow,
		// Token: 0x0400026B RID: 619
		[EnumMember]
		Search,
		// Token: 0x0400026C RID: 620
		[EnumMember]
		SchemaChange,
		// Token: 0x0400026D RID: 621
		[EnumMember]
		ProfileChange,
		// Token: 0x0400026E RID: 622
		Count
	}
}
