using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003EA RID: 1002
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class AttachmentTypeEx
	{
		// Token: 0x170007C4 RID: 1988
		// (get) Token: 0x0600204E RID: 8270 RVA: 0x00078966 File Offset: 0x00076B66
		// (set) Token: 0x0600204F RID: 8271 RVA: 0x0007896E File Offset: 0x00076B6E
		[DataMember]
		public string AttachmentType { get; set; }

		// Token: 0x170007C5 RID: 1989
		// (get) Token: 0x06002050 RID: 8272 RVA: 0x00078977 File Offset: 0x00076B77
		// (set) Token: 0x06002051 RID: 8273 RVA: 0x0007897F File Offset: 0x00076B7F
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string CalculatedContentType { get; set; }

		// Token: 0x170007C6 RID: 1990
		// (get) Token: 0x06002052 RID: 8274 RVA: 0x00078988 File Offset: 0x00076B88
		// (set) Token: 0x06002053 RID: 8275 RVA: 0x00078990 File Offset: 0x00076B90
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string DateTimeCreated { get; set; }

		// Token: 0x170007C7 RID: 1991
		// (get) Token: 0x06002054 RID: 8276 RVA: 0x00078999 File Offset: 0x00076B99
		// (set) Token: 0x06002055 RID: 8277 RVA: 0x000789A1 File Offset: 0x00076BA1
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string FileName { get; set; }

		// Token: 0x170007C8 RID: 1992
		// (get) Token: 0x06002056 RID: 8278 RVA: 0x000789AA File Offset: 0x00076BAA
		// (set) Token: 0x06002057 RID: 8279 RVA: 0x000789B2 File Offset: 0x00076BB2
		[DataMember(EmitDefaultValue = false, IsRequired = false)]
		public string FileExtension { get; set; }
	}
}
