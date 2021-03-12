using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Services.Wcf.Types
{
	// Token: 0x02000A4A RID: 2634
	[DataContract]
	public class Extension
	{
		// Token: 0x170010D2 RID: 4306
		// (get) Token: 0x06004A7E RID: 19070 RVA: 0x001047FC File Offset: 0x001029FC
		// (set) Token: 0x06004A7F RID: 19071 RVA: 0x00104804 File Offset: 0x00102A04
		[DataMember]
		public string Id { get; set; }

		// Token: 0x170010D3 RID: 4307
		// (get) Token: 0x06004A80 RID: 19072 RVA: 0x0010480D File Offset: 0x00102A0D
		// (set) Token: 0x06004A81 RID: 19073 RVA: 0x00104815 File Offset: 0x00102A15
		[DataMember]
		public string Version { get; set; }

		// Token: 0x170010D4 RID: 4308
		// (get) Token: 0x06004A82 RID: 19074 RVA: 0x0010481E File Offset: 0x00102A1E
		// (set) Token: 0x06004A83 RID: 19075 RVA: 0x00104826 File Offset: 0x00102A26
		[DataMember]
		public string DisplayName { get; set; }

		// Token: 0x170010D5 RID: 4309
		// (get) Token: 0x06004A84 RID: 19076 RVA: 0x0010482F File Offset: 0x00102A2F
		// (set) Token: 0x06004A85 RID: 19077 RVA: 0x00104837 File Offset: 0x00102A37
		[DataMember]
		public string ProviderName { get; set; }

		// Token: 0x170010D6 RID: 4310
		// (get) Token: 0x06004A86 RID: 19078 RVA: 0x00104840 File Offset: 0x00102A40
		// (set) Token: 0x06004A87 RID: 19079 RVA: 0x00104848 File Offset: 0x00102A48
		[DataMember]
		public string IconUrl { get; set; }

		// Token: 0x170010D7 RID: 4311
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x00104851 File Offset: 0x00102A51
		// (set) Token: 0x06004A89 RID: 19081 RVA: 0x00104859 File Offset: 0x00102A59
		[DataMember]
		public string HighResolutionIconUrl { get; set; }

		// Token: 0x170010D8 RID: 4312
		// (get) Token: 0x06004A8A RID: 19082 RVA: 0x00104862 File Offset: 0x00102A62
		// (set) Token: 0x06004A8B RID: 19083 RVA: 0x0010486A File Offset: 0x00102A6A
		[DataMember]
		public ExtensionInstallScope Origin { get; set; }

		// Token: 0x170010D9 RID: 4313
		// (get) Token: 0x06004A8C RID: 19084 RVA: 0x00104873 File Offset: 0x00102A73
		// (set) Token: 0x06004A8D RID: 19085 RVA: 0x0010487B File Offset: 0x00102A7B
		[DataMember]
		public ExtensionType Type { get; set; }

		// Token: 0x170010DA RID: 4314
		// (get) Token: 0x06004A8E RID: 19086 RVA: 0x00104884 File Offset: 0x00102A84
		// (set) Token: 0x06004A8F RID: 19087 RVA: 0x0010488C File Offset: 0x00102A8C
		[DataMember]
		public string AppStatus { get; set; }

		// Token: 0x170010DB RID: 4315
		// (get) Token: 0x06004A90 RID: 19088 RVA: 0x00104895 File Offset: 0x00102A95
		// (set) Token: 0x06004A91 RID: 19089 RVA: 0x0010489D File Offset: 0x00102A9D
		[DataMember]
		public string EndNodeUrl { get; set; }

		// Token: 0x170010DC RID: 4316
		// (get) Token: 0x06004A92 RID: 19090 RVA: 0x001048A6 File Offset: 0x00102AA6
		// (set) Token: 0x06004A93 RID: 19091 RVA: 0x001048AE File Offset: 0x00102AAE
		[DataMember]
		public LicenseType LicenseType { get; set; }

		// Token: 0x170010DD RID: 4317
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x001048B7 File Offset: 0x00102AB7
		// (set) Token: 0x06004A95 RID: 19093 RVA: 0x001048BF File Offset: 0x00102ABF
		[DataMember(EmitDefaultValue = false)]
		public string AuthTokenId { get; set; }

		// Token: 0x170010DE RID: 4318
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x001048C8 File Offset: 0x00102AC8
		// (set) Token: 0x06004A97 RID: 19095 RVA: 0x001048D0 File Offset: 0x00102AD0
		[DataMember(Name = "Rule")]
		public ActivationRule ActivationRule { get; set; }

		// Token: 0x170010DF RID: 4319
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x001048D9 File Offset: 0x00102AD9
		// (set) Token: 0x06004A99 RID: 19097 RVA: 0x001048E1 File Offset: 0x00102AE1
		[DataMember(EmitDefaultValue = false)]
		public string Settings { get; set; }

		// Token: 0x170010E0 RID: 4320
		// (get) Token: 0x06004A9A RID: 19098 RVA: 0x001048EA File Offset: 0x00102AEA
		// (set) Token: 0x06004A9B RID: 19099 RVA: 0x001048F2 File Offset: 0x00102AF2
		[DataMember]
		public RequestedCapabilities RequestedCapabilities { get; set; }

		// Token: 0x170010E1 RID: 4321
		// (get) Token: 0x06004A9C RID: 19100 RVA: 0x001048FB File Offset: 0x00102AFB
		// (set) Token: 0x06004A9D RID: 19101 RVA: 0x00104903 File Offset: 0x00102B03
		[DataMember]
		public bool DisableEntityHighlighting { get; set; }

		// Token: 0x170010E2 RID: 4322
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x0010490C File Offset: 0x00102B0C
		// (set) Token: 0x06004A9F RID: 19103 RVA: 0x00104914 File Offset: 0x00102B14
		[DataMember]
		public FormSettings[] FormSettingsList { get; set; }
	}
}
