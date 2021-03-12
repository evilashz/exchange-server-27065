using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020001F4 RID: 500
	[DataContract]
	public class JsonFaultBody
	{
		// Token: 0x17000493 RID: 1171
		// (get) Token: 0x06001196 RID: 4502 RVA: 0x0004383F File Offset: 0x00041A3F
		// (set) Token: 0x06001197 RID: 4503 RVA: 0x00043847 File Offset: 0x00041A47
		[DataMember(IsRequired = true)]
		public string FaultMessage { get; set; }

		// Token: 0x17000494 RID: 1172
		// (get) Token: 0x06001198 RID: 4504 RVA: 0x00043850 File Offset: 0x00041A50
		// (set) Token: 0x06001199 RID: 4505 RVA: 0x00043858 File Offset: 0x00041A58
		[DataMember(IsRequired = true)]
		public int ErrorCode { get; set; }

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x0600119A RID: 4506 RVA: 0x00043861 File Offset: 0x00041A61
		// (set) Token: 0x0600119B RID: 4507 RVA: 0x00043869 File Offset: 0x00041A69
		[DataMember(EmitDefaultValue = false)]
		public string StackTrace { get; set; }

		// Token: 0x17000496 RID: 1174
		// (get) Token: 0x0600119C RID: 4508 RVA: 0x00043872 File Offset: 0x00041A72
		// (set) Token: 0x0600119D RID: 4509 RVA: 0x0004387A File Offset: 0x00041A7A
		[DataMember(EmitDefaultValue = false)]
		public string ResponseCode { get; set; }

		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x0600119E RID: 4510 RVA: 0x00043883 File Offset: 0x00041A83
		// (set) Token: 0x0600119F RID: 4511 RVA: 0x0004388B File Offset: 0x00041A8B
		[DataMember]
		public bool IsTransient { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x060011A0 RID: 4512 RVA: 0x00043894 File Offset: 0x00041A94
		// (set) Token: 0x060011A1 RID: 4513 RVA: 0x0004389C File Offset: 0x00041A9C
		[DataMember(EmitDefaultValue = false)]
		public int BackOffPeriodInMs { get; set; }

		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x060011A2 RID: 4514 RVA: 0x000438A5 File Offset: 0x00041AA5
		// (set) Token: 0x060011A3 RID: 4515 RVA: 0x000438AD File Offset: 0x00041AAD
		[DataMember(EmitDefaultValue = false)]
		public string ExceptionName { get; set; }
	}
}
