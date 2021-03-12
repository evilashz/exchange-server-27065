using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Security
{
	// Token: 0x02000AD2 RID: 2770
	[CLSCompliant(false)]
	[DataContract]
	public class RPSProfile
	{
		// Token: 0x17000EB4 RID: 3764
		// (get) Token: 0x06003B49 RID: 15177 RVA: 0x00098F42 File Offset: 0x00097142
		// (set) Token: 0x06003B4A RID: 15178 RVA: 0x00098F4A File Offset: 0x0009714A
		[DataMember]
		public string HexPuid { get; set; }

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003B4B RID: 15179 RVA: 0x00098F53 File Offset: 0x00097153
		// (set) Token: 0x06003B4C RID: 15180 RVA: 0x00098F5B File Offset: 0x0009715B
		[DataMember]
		public string MemberName { get; set; }

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x00098F64 File Offset: 0x00097164
		// (set) Token: 0x06003B4E RID: 15182 RVA: 0x00098F6C File Offset: 0x0009716C
		[DataMember]
		public int TokenFlags { get; set; }

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003B4F RID: 15183 RVA: 0x00098F75 File Offset: 0x00097175
		// (set) Token: 0x06003B50 RID: 15184 RVA: 0x00098F7D File Offset: 0x0009717D
		[DataMember]
		public uint AuthFlags { get; set; }

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003B51 RID: 15185 RVA: 0x00098F86 File Offset: 0x00097186
		// (set) Token: 0x06003B52 RID: 15186 RVA: 0x00098F8E File Offset: 0x0009718E
		[DataMember]
		public uint CredsExpireIn { get; set; }

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003B53 RID: 15187 RVA: 0x00098F97 File Offset: 0x00097197
		// (set) Token: 0x06003B54 RID: 15188 RVA: 0x00098F9F File Offset: 0x0009719F
		[DataMember]
		public string RecoveryUrl { get; set; }

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x00098FA8 File Offset: 0x000971A8
		// (set) Token: 0x06003B56 RID: 15190 RVA: 0x00098FB0 File Offset: 0x000971B0
		[DataMember]
		public string ConsumerPuid { get; set; }

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003B57 RID: 15191 RVA: 0x00098FB9 File Offset: 0x000971B9
		// (set) Token: 0x06003B58 RID: 15192 RVA: 0x00098FC1 File Offset: 0x000971C1
		[DataMember]
		public bool AppPassword { get; set; }

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003B59 RID: 15193 RVA: 0x00098FCA File Offset: 0x000971CA
		// (set) Token: 0x06003B5A RID: 15194 RVA: 0x00098FD2 File Offset: 0x000971D2
		[DataMember]
		public bool HasSignedTOU { get; set; }

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003B5B RID: 15195 RVA: 0x00098FDB File Offset: 0x000971DB
		// (set) Token: 0x06003B5C RID: 15196 RVA: 0x00098FE3 File Offset: 0x000971E3
		[DataMember]
		public uint AuthInstant { get; set; }

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003B5D RID: 15197 RVA: 0x00098FEC File Offset: 0x000971EC
		// (set) Token: 0x06003B5E RID: 15198 RVA: 0x00098FF4 File Offset: 0x000971F4
		[DataMember]
		public uint IssueInstant { get; set; }

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003B5F RID: 15199 RVA: 0x00098FFD File Offset: 0x000971FD
		// (set) Token: 0x06003B60 RID: 15200 RVA: 0x00099005 File Offset: 0x00097205
		[DataMember]
		public byte ReputationByte { get; set; }

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003B61 RID: 15201 RVA: 0x0009900E File Offset: 0x0009720E
		// (set) Token: 0x06003B62 RID: 15202 RVA: 0x00099016 File Offset: 0x00097216
		[DataMember]
		public string HexCID { get; set; }

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003B63 RID: 15203 RVA: 0x0009901F File Offset: 0x0009721F
		// (set) Token: 0x06003B64 RID: 15204 RVA: 0x00099027 File Offset: 0x00097227
		[DataMember]
		public uint LoginAttributes { get; set; }

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003B65 RID: 15205 RVA: 0x00099030 File Offset: 0x00097230
		// (set) Token: 0x06003B66 RID: 15206 RVA: 0x00099038 File Offset: 0x00097238
		[DataMember]
		public string ResponseHeader { get; set; }

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003B67 RID: 15207 RVA: 0x00099041 File Offset: 0x00097241
		// (set) Token: 0x06003B68 RID: 15208 RVA: 0x00099049 File Offset: 0x00097249
		[DataMember]
		public uint RPSAuthState { get; set; }

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003B69 RID: 15209 RVA: 0x00099052 File Offset: 0x00097252
		// (set) Token: 0x06003B6A RID: 15210 RVA: 0x0009905A File Offset: 0x0009725A
		[DataMember]
		public string ConsumerCID { get; set; }

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003B6B RID: 15211 RVA: 0x00099063 File Offset: 0x00097263
		// (set) Token: 0x06003B6C RID: 15212 RVA: 0x0009906B File Offset: 0x0009726B
		[DataMember]
		public uint TicketType { get; set; }

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003B6D RID: 15213 RVA: 0x00099074 File Offset: 0x00097274
		// (set) Token: 0x06003B6E RID: 15214 RVA: 0x0009907C File Offset: 0x0009727C
		[DataMember]
		public uint ConsumerChild { get; set; }

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003B6F RID: 15215 RVA: 0x00099085 File Offset: 0x00097285
		// (set) Token: 0x06003B70 RID: 15216 RVA: 0x0009908D File Offset: 0x0009728D
		[DataMember]
		public string ConsumerConsentLevel { get; set; }

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003B71 RID: 15217 RVA: 0x00099096 File Offset: 0x00097296
		// (set) Token: 0x06003B72 RID: 15218 RVA: 0x0009909E File Offset: 0x0009729E
		[DataMember]
		public RPSProfile.UserInfo Profile { get; set; }

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06003B73 RID: 15219 RVA: 0x000990A7 File Offset: 0x000972A7
		// (set) Token: 0x06003B74 RID: 15220 RVA: 0x000990AF File Offset: 0x000972AF
		public string CurrentAlias { get; set; }

		// Token: 0x02000AD3 RID: 2771
		[DataContract]
		public class UserInfo
		{
			// Token: 0x17000ECA RID: 3786
			// (get) Token: 0x06003B76 RID: 15222 RVA: 0x000990C0 File Offset: 0x000972C0
			// (set) Token: 0x06003B77 RID: 15223 RVA: 0x000990C8 File Offset: 0x000972C8
			[DataMember]
			public string Gender { get; set; }

			// Token: 0x17000ECB RID: 3787
			// (get) Token: 0x06003B78 RID: 15224 RVA: 0x000990D1 File Offset: 0x000972D1
			// (set) Token: 0x06003B79 RID: 15225 RVA: 0x000990D9 File Offset: 0x000972D9
			[DataMember]
			public string Occupation { get; set; }

			// Token: 0x17000ECC RID: 3788
			// (get) Token: 0x06003B7A RID: 15226 RVA: 0x000990E2 File Offset: 0x000972E2
			// (set) Token: 0x06003B7B RID: 15227 RVA: 0x000990EA File Offset: 0x000972EA
			[DataMember]
			public int Region { get; set; }

			// Token: 0x17000ECD RID: 3789
			// (get) Token: 0x06003B7C RID: 15228 RVA: 0x000990F3 File Offset: 0x000972F3
			// (set) Token: 0x06003B7D RID: 15229 RVA: 0x000990FB File Offset: 0x000972FB
			[DataMember]
			public short TimeZone { get; set; }

			// Token: 0x17000ECE RID: 3790
			// (get) Token: 0x06003B7E RID: 15230 RVA: 0x00099104 File Offset: 0x00097304
			// (set) Token: 0x06003B7F RID: 15231 RVA: 0x0009910C File Offset: 0x0009730C
			[DataMember]
			public DateTime Birthday { get; set; }

			// Token: 0x17000ECF RID: 3791
			// (get) Token: 0x06003B80 RID: 15232 RVA: 0x00099115 File Offset: 0x00097315
			// (set) Token: 0x06003B81 RID: 15233 RVA: 0x0009911D File Offset: 0x0009731D
			[DataMember]
			public uint AliasVersion { get; set; }

			// Token: 0x17000ED0 RID: 3792
			// (get) Token: 0x06003B82 RID: 15234 RVA: 0x00099126 File Offset: 0x00097326
			// (set) Token: 0x06003B83 RID: 15235 RVA: 0x0009912E File Offset: 0x0009732E
			[DataMember]
			public string PostalCode { get; set; }

			// Token: 0x17000ED1 RID: 3793
			// (get) Token: 0x06003B84 RID: 15236 RVA: 0x00099137 File Offset: 0x00097337
			// (set) Token: 0x06003B85 RID: 15237 RVA: 0x0009913F File Offset: 0x0009733F
			[DataMember]
			public string FirstName { get; set; }

			// Token: 0x17000ED2 RID: 3794
			// (get) Token: 0x06003B86 RID: 15238 RVA: 0x00099148 File Offset: 0x00097348
			// (set) Token: 0x06003B87 RID: 15239 RVA: 0x00099150 File Offset: 0x00097350
			[DataMember]
			public string LastName { get; set; }

			// Token: 0x17000ED3 RID: 3795
			// (get) Token: 0x06003B88 RID: 15240 RVA: 0x00099159 File Offset: 0x00097359
			// (set) Token: 0x06003B89 RID: 15241 RVA: 0x00099161 File Offset: 0x00097361
			[DataMember]
			public short Language { get; set; }

			// Token: 0x17000ED4 RID: 3796
			// (get) Token: 0x06003B8A RID: 15242 RVA: 0x0009916A File Offset: 0x0009736A
			// (set) Token: 0x06003B8B RID: 15243 RVA: 0x00099172 File Offset: 0x00097372
			[DataMember]
			public string Country { get; set; }
		}
	}
}
