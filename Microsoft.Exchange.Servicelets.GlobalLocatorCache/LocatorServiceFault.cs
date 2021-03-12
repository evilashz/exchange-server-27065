using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Servicelets.GlobalLocatorCache
{
	// Token: 0x02000017 RID: 23
	[DataContract(Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	public class LocatorServiceFault
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00003CC8 File Offset: 0x00001EC8
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00003CD0 File Offset: 0x00001ED0
		[DataMember]
		public ErrorCode ErrorCode { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00003CD9 File Offset: 0x00001ED9
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00003CE1 File Offset: 0x00001EE1
		[DataMember]
		public string Message { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000067 RID: 103 RVA: 0x00003CEA File Offset: 0x00001EEA
		// (set) Token: 0x06000068 RID: 104 RVA: 0x00003CF2 File Offset: 0x00001EF2
		[DataMember]
		public bool CanRetry { get; set; }

		// Token: 0x06000069 RID: 105 RVA: 0x00003CFB File Offset: 0x00001EFB
		public override string ToString()
		{
			return string.Format("ErrorCode: <{0}>, Message: <{1}>, CanRetry: <{2}>", this.ErrorCode, this.Message, this.CanRetry);
		}
	}
}
