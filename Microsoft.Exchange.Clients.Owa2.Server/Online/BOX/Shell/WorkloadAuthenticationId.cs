using System;
using System.CodeDom.Compiler;
using System.Runtime.Serialization;

namespace Microsoft.Online.BOX.Shell
{
	// Token: 0x0200006D RID: 109
	[DataContract(Name = "WorkloadAuthenticationId", Namespace = "http://schemas.datacontract.org/2004/07/Microsoft.Online.BOX.Shell")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	public enum WorkloadAuthenticationId
	{
		// Token: 0x040001CA RID: 458
		[EnumMember]
		None,
		// Token: 0x040001CB RID: 459
		[EnumMember]
		Exchange,
		// Token: 0x040001CC RID: 460
		[EnumMember]
		Sharepoint,
		// Token: 0x040001CD RID: 461
		[EnumMember]
		FIM,
		// Token: 0x040001CE RID: 462
		[EnumMember]
		Lync,
		// Token: 0x040001CF RID: 463
		[EnumMember]
		OfficeDotCom,
		// Token: 0x040001D0 RID: 464
		[EnumMember]
		CRM,
		// Token: 0x040001D1 RID: 465
		[EnumMember]
		Forefront,
		// Token: 0x040001D2 RID: 466
		[EnumMember]
		OnRamp,
		// Token: 0x040001D3 RID: 467
		[EnumMember]
		AADUX,
		// Token: 0x040001D4 RID: 468
		[EnumMember]
		AdminPortal,
		// Token: 0x040001D5 RID: 469
		[EnumMember]
		Apps,
		// Token: 0x040001D6 RID: 470
		[EnumMember]
		Yammer,
		// Token: 0x040001D7 RID: 471
		[EnumMember]
		Project,
		// Token: 0x040001D8 RID: 472
		[EnumMember]
		Pulse,
		// Token: 0x040001D9 RID: 473
		[EnumMember]
		PowerBI,
		// Token: 0x040001DA RID: 474
		[EnumMember]
		ExchangeAdmin,
		// Token: 0x040001DB RID: 475
		[EnumMember]
		ExchangeIWOptions,
		// Token: 0x040001DC RID: 476
		[EnumMember]
		ComplianceCenter,
		// Token: 0x040001DD RID: 477
		[EnumMember]
		WAC
	}
}
