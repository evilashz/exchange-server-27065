using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020003C5 RID: 965
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	public class GetCertsInfoResponse
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001EEF RID: 7919 RVA: 0x00076D29 File Offset: 0x00074F29
		// (set) Token: 0x06001EF0 RID: 7920 RVA: 0x00076D31 File Offset: 0x00074F31
		[DataMember]
		public string CertsRawData { get; set; }

		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001EF1 RID: 7921 RVA: 0x00076D3A File Offset: 0x00074F3A
		// (set) Token: 0x06001EF2 RID: 7922 RVA: 0x00076D42 File Offset: 0x00074F42
		[DataMember]
		public bool IsValid { get; set; }

		// Token: 0x17000739 RID: 1849
		// (get) Token: 0x06001EF3 RID: 7923 RVA: 0x00076D4B File Offset: 0x00074F4B
		// (set) Token: 0x06001EF4 RID: 7924 RVA: 0x00076D53 File Offset: 0x00074F53
		[DataMember]
		public uint PolicyFlag { get; set; }

		// Token: 0x1700073A RID: 1850
		// (get) Token: 0x06001EF5 RID: 7925 RVA: 0x00076D5C File Offset: 0x00074F5C
		// (set) Token: 0x06001EF6 RID: 7926 RVA: 0x00076D64 File Offset: 0x00074F64
		[DataMember]
		public uint ChainValidityStatus { get; set; }

		// Token: 0x1700073B RID: 1851
		// (get) Token: 0x06001EF7 RID: 7927 RVA: 0x00076D6D File Offset: 0x00074F6D
		// (set) Token: 0x06001EF8 RID: 7928 RVA: 0x00076D75 File Offset: 0x00074F75
		[DataMember]
		public string DisplayedId { get; set; }

		// Token: 0x1700073C RID: 1852
		// (get) Token: 0x06001EF9 RID: 7929 RVA: 0x00076D7E File Offset: 0x00074F7E
		// (set) Token: 0x06001EFA RID: 7930 RVA: 0x00076D86 File Offset: 0x00074F86
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x1700073D RID: 1853
		// (get) Token: 0x06001EFB RID: 7931 RVA: 0x00076D8F File Offset: 0x00074F8F
		// (set) Token: 0x06001EFC RID: 7932 RVA: 0x00076D97 File Offset: 0x00074F97
		[DataMember]
		public string Issuer { get; set; }

		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x00076DA0 File Offset: 0x00074FA0
		// (set) Token: 0x06001EFE RID: 7934 RVA: 0x00076DA8 File Offset: 0x00074FA8
		[DataMember]
		public string ChainData { get; set; }

		// Token: 0x1700073F RID: 1855
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x00076DB1 File Offset: 0x00074FB1
		// (set) Token: 0x06001F00 RID: 7936 RVA: 0x00076DB9 File Offset: 0x00074FB9
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x17000740 RID: 1856
		// (get) Token: 0x06001F01 RID: 7937 RVA: 0x00076DC2 File Offset: 0x00074FC2
		// (set) Token: 0x06001F02 RID: 7938 RVA: 0x00076DCA File Offset: 0x00074FCA
		[DataMember]
		public string SubjectKeyIdentifier { get; set; }
	}
}
