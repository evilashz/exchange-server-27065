using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000020 RID: 32
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class DxStoreAccessReply : DxStoreReplyBase
	{
		// Token: 0x060000B0 RID: 176 RVA: 0x0000286C File Offset: 0x00000A6C
		public void Initialize(string self = null)
		{
			base.Responder = ((!string.IsNullOrEmpty(self)) ? self : Environment.MachineName);
			base.ProcessInfo = Utils.CurrentProcessInfo;
			base.TimeReceived = DateTimeOffset.Now;
			base.Version = 1;
		}

		// Token: 0x0400005C RID: 92
		public const int DefaultVersion = 1;

		// Token: 0x02000021 RID: 33
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class CheckKey : DxStoreAccessReply
		{
			// Token: 0x1700003D RID: 61
			// (get) Token: 0x060000B2 RID: 178 RVA: 0x000028A9 File Offset: 0x00000AA9
			// (set) Token: 0x060000B3 RID: 179 RVA: 0x000028B1 File Offset: 0x00000AB1
			[DataMember]
			public bool IsExist { get; set; }

			// Token: 0x1700003E RID: 62
			// (get) Token: 0x060000B4 RID: 180 RVA: 0x000028BA File Offset: 0x00000ABA
			// (set) Token: 0x060000B5 RID: 181 RVA: 0x000028C2 File Offset: 0x00000AC2
			[DataMember]
			public bool IsCreated { get; set; }

			// Token: 0x1700003F RID: 63
			// (get) Token: 0x060000B6 RID: 182 RVA: 0x000028CB File Offset: 0x00000ACB
			// (set) Token: 0x060000B7 RID: 183 RVA: 0x000028D3 File Offset: 0x00000AD3
			[DataMember]
			public ReadResult ReadResult { get; set; }

			// Token: 0x17000040 RID: 64
			// (get) Token: 0x060000B8 RID: 184 RVA: 0x000028DC File Offset: 0x00000ADC
			// (set) Token: 0x060000B9 RID: 185 RVA: 0x000028E4 File Offset: 0x00000AE4
			[DataMember]
			public WriteResult WriteResult { get; set; }
		}

		// Token: 0x02000022 RID: 34
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class DeleteKey : DxStoreAccessReply
		{
			// Token: 0x17000041 RID: 65
			// (get) Token: 0x060000BB RID: 187 RVA: 0x000028F5 File Offset: 0x00000AF5
			// (set) Token: 0x060000BC RID: 188 RVA: 0x000028FD File Offset: 0x00000AFD
			[DataMember]
			public bool IsExist { get; set; }

			// Token: 0x17000042 RID: 66
			// (get) Token: 0x060000BD RID: 189 RVA: 0x00002906 File Offset: 0x00000B06
			// (set) Token: 0x060000BE RID: 190 RVA: 0x0000290E File Offset: 0x00000B0E
			[DataMember]
			public ReadResult ReadResult { get; set; }

			// Token: 0x17000043 RID: 67
			// (get) Token: 0x060000BF RID: 191 RVA: 0x00002917 File Offset: 0x00000B17
			// (set) Token: 0x060000C0 RID: 192 RVA: 0x0000291F File Offset: 0x00000B1F
			[DataMember]
			public WriteResult WriteResult { get; set; }
		}

		// Token: 0x02000023 RID: 35
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class SetProperty : DxStoreAccessReply
		{
			// Token: 0x17000044 RID: 68
			// (get) Token: 0x060000C2 RID: 194 RVA: 0x00002930 File Offset: 0x00000B30
			// (set) Token: 0x060000C3 RID: 195 RVA: 0x00002938 File Offset: 0x00000B38
			[DataMember]
			public WriteResult WriteResult { get; set; }
		}

		// Token: 0x02000024 RID: 36
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class DeleteProperty : DxStoreAccessReply
		{
			// Token: 0x17000045 RID: 69
			// (get) Token: 0x060000C5 RID: 197 RVA: 0x00002949 File Offset: 0x00000B49
			// (set) Token: 0x060000C6 RID: 198 RVA: 0x00002951 File Offset: 0x00000B51
			[DataMember]
			public bool IsExist { get; set; }

			// Token: 0x17000046 RID: 70
			// (get) Token: 0x060000C7 RID: 199 RVA: 0x0000295A File Offset: 0x00000B5A
			// (set) Token: 0x060000C8 RID: 200 RVA: 0x00002962 File Offset: 0x00000B62
			[DataMember]
			public ReadResult ReadResult { get; set; }

			// Token: 0x17000047 RID: 71
			// (get) Token: 0x060000C9 RID: 201 RVA: 0x0000296B File Offset: 0x00000B6B
			// (set) Token: 0x060000CA RID: 202 RVA: 0x00002973 File Offset: 0x00000B73
			[DataMember]
			public WriteResult WriteResult { get; set; }
		}

		// Token: 0x02000025 RID: 37
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class ExecuteBatch : DxStoreAccessReply
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060000CC RID: 204 RVA: 0x00002984 File Offset: 0x00000B84
			// (set) Token: 0x060000CD RID: 205 RVA: 0x0000298C File Offset: 0x00000B8C
			[DataMember]
			public WriteResult WriteResult { get; set; }
		}

		// Token: 0x02000026 RID: 38
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetProperty : DxStoreAccessReply
		{
			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060000CF RID: 207 RVA: 0x0000299D File Offset: 0x00000B9D
			// (set) Token: 0x060000D0 RID: 208 RVA: 0x000029A5 File Offset: 0x00000BA5
			[DataMember]
			public PropertyValue Value { get; set; }

			// Token: 0x1700004A RID: 74
			// (get) Token: 0x060000D1 RID: 209 RVA: 0x000029AE File Offset: 0x00000BAE
			// (set) Token: 0x060000D2 RID: 210 RVA: 0x000029B6 File Offset: 0x00000BB6
			[DataMember]
			public ReadResult ReadResult { get; set; }
		}

		// Token: 0x02000027 RID: 39
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetAllProperties : DxStoreAccessReply
		{
			// Token: 0x1700004B RID: 75
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x000029C7 File Offset: 0x00000BC7
			// (set) Token: 0x060000D5 RID: 213 RVA: 0x000029CF File Offset: 0x00000BCF
			[DataMember]
			public Tuple<string, PropertyValue>[] Values { get; set; }

			// Token: 0x1700004C RID: 76
			// (get) Token: 0x060000D6 RID: 214 RVA: 0x000029D8 File Offset: 0x00000BD8
			// (set) Token: 0x060000D7 RID: 215 RVA: 0x000029E0 File Offset: 0x00000BE0
			[DataMember]
			public ReadResult ReadResult { get; set; }
		}

		// Token: 0x02000028 RID: 40
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetSubkeyNames : DxStoreAccessReply
		{
			// Token: 0x1700004D RID: 77
			// (get) Token: 0x060000D9 RID: 217 RVA: 0x000029F1 File Offset: 0x00000BF1
			// (set) Token: 0x060000DA RID: 218 RVA: 0x000029F9 File Offset: 0x00000BF9
			[DataMember]
			public string[] Keys { get; set; }

			// Token: 0x1700004E RID: 78
			// (get) Token: 0x060000DB RID: 219 RVA: 0x00002A02 File Offset: 0x00000C02
			// (set) Token: 0x060000DC RID: 220 RVA: 0x00002A0A File Offset: 0x00000C0A
			[DataMember]
			public ReadResult ReadResult { get; set; }
		}

		// Token: 0x02000029 RID: 41
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetPropertyNames : DxStoreAccessReply
		{
			// Token: 0x1700004F RID: 79
			// (get) Token: 0x060000DE RID: 222 RVA: 0x00002A1B File Offset: 0x00000C1B
			// (set) Token: 0x060000DF RID: 223 RVA: 0x00002A23 File Offset: 0x00000C23
			[DataMember]
			public PropertyNameInfo[] Infos { get; set; }

			// Token: 0x17000050 RID: 80
			// (get) Token: 0x060000E0 RID: 224 RVA: 0x00002A2C File Offset: 0x00000C2C
			// (set) Token: 0x060000E1 RID: 225 RVA: 0x00002A34 File Offset: 0x00000C34
			[DataMember]
			public ReadResult ReadResult { get; set; }
		}
	}
}
