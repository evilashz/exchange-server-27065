using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000016 RID: 22
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class DxStoreAccessRequest : DxStoreRequestBase
	{
		// Token: 0x17000028 RID: 40
		// (get) Token: 0x0600007B RID: 123 RVA: 0x0000264E File Offset: 0x0000084E
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00002656 File Offset: 0x00000856
		[DataMember]
		public string FullKeyName { get; set; }

		// Token: 0x0600007D RID: 125 RVA: 0x00002660 File Offset: 0x00000860
		public void Initialize(string fullKeyName, bool isPrivate = false, string self = null)
		{
			string path = isPrivate ? "Private" : "Public";
			this.FullKeyName = Utils.CombinePathNullSafe(path, fullKeyName);
			base.Requester = ((!string.IsNullOrEmpty(self)) ? self : Environment.MachineName);
			base.ProcessInfo = Utils.CurrentProcessInfo;
			base.TimeRequested = DateTimeOffset.Now;
			base.Id = Guid.NewGuid();
			base.Version = 1;
		}

		// Token: 0x04000046 RID: 70
		public const int DefaultVersion = 1;

		// Token: 0x02000017 RID: 23
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class CheckKey : DxStoreAccessRequest
		{
			// Token: 0x17000029 RID: 41
			// (get) Token: 0x0600007F RID: 127 RVA: 0x000026D0 File Offset: 0x000008D0
			// (set) Token: 0x06000080 RID: 128 RVA: 0x000026D8 File Offset: 0x000008D8
			[DataMember]
			public string SubkeyName { get; set; }

			// Token: 0x1700002A RID: 42
			// (get) Token: 0x06000081 RID: 129 RVA: 0x000026E1 File Offset: 0x000008E1
			// (set) Token: 0x06000082 RID: 130 RVA: 0x000026E9 File Offset: 0x000008E9
			[DataMember]
			public bool IsCreateIfNotExist { get; set; }

			// Token: 0x1700002B RID: 43
			// (get) Token: 0x06000083 RID: 131 RVA: 0x000026F2 File Offset: 0x000008F2
			// (set) Token: 0x06000084 RID: 132 RVA: 0x000026FA File Offset: 0x000008FA
			[DataMember]
			public ReadOptions ReadOptions { get; set; }

			// Token: 0x1700002C RID: 44
			// (get) Token: 0x06000085 RID: 133 RVA: 0x00002703 File Offset: 0x00000903
			// (set) Token: 0x06000086 RID: 134 RVA: 0x0000270B File Offset: 0x0000090B
			[DataMember]
			public WriteOptions WriteOptions { get; set; }
		}

		// Token: 0x02000018 RID: 24
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class DeleteKey : DxStoreAccessRequest
		{
			// Token: 0x1700002D RID: 45
			// (get) Token: 0x06000088 RID: 136 RVA: 0x0000271C File Offset: 0x0000091C
			// (set) Token: 0x06000089 RID: 137 RVA: 0x00002724 File Offset: 0x00000924
			[DataMember]
			public string SubkeyName { get; set; }

			// Token: 0x1700002E RID: 46
			// (get) Token: 0x0600008A RID: 138 RVA: 0x0000272D File Offset: 0x0000092D
			// (set) Token: 0x0600008B RID: 139 RVA: 0x00002735 File Offset: 0x00000935
			[DataMember]
			public ReadOptions ReadOptions { get; set; }

			// Token: 0x1700002F RID: 47
			// (get) Token: 0x0600008C RID: 140 RVA: 0x0000273E File Offset: 0x0000093E
			// (set) Token: 0x0600008D RID: 141 RVA: 0x00002746 File Offset: 0x00000946
			[DataMember]
			public WriteOptions WriteOptions { get; set; }
		}

		// Token: 0x02000019 RID: 25
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class SetProperty : DxStoreAccessRequest
		{
			// Token: 0x17000030 RID: 48
			// (get) Token: 0x0600008F RID: 143 RVA: 0x00002757 File Offset: 0x00000957
			// (set) Token: 0x06000090 RID: 144 RVA: 0x0000275F File Offset: 0x0000095F
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17000031 RID: 49
			// (get) Token: 0x06000091 RID: 145 RVA: 0x00002768 File Offset: 0x00000968
			// (set) Token: 0x06000092 RID: 146 RVA: 0x00002770 File Offset: 0x00000970
			[DataMember]
			public PropertyValue Value { get; set; }

			// Token: 0x17000032 RID: 50
			// (get) Token: 0x06000093 RID: 147 RVA: 0x00002779 File Offset: 0x00000979
			// (set) Token: 0x06000094 RID: 148 RVA: 0x00002781 File Offset: 0x00000981
			[DataMember]
			public WriteOptions WriteOptions { get; set; }
		}

		// Token: 0x0200001A RID: 26
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class DeleteProperty : DxStoreAccessRequest
		{
			// Token: 0x17000033 RID: 51
			// (get) Token: 0x06000096 RID: 150 RVA: 0x00002792 File Offset: 0x00000992
			// (set) Token: 0x06000097 RID: 151 RVA: 0x0000279A File Offset: 0x0000099A
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17000034 RID: 52
			// (get) Token: 0x06000098 RID: 152 RVA: 0x000027A3 File Offset: 0x000009A3
			// (set) Token: 0x06000099 RID: 153 RVA: 0x000027AB File Offset: 0x000009AB
			[DataMember]
			public ReadOptions ReadOptions { get; set; }

			// Token: 0x17000035 RID: 53
			// (get) Token: 0x0600009A RID: 154 RVA: 0x000027B4 File Offset: 0x000009B4
			// (set) Token: 0x0600009B RID: 155 RVA: 0x000027BC File Offset: 0x000009BC
			[DataMember]
			public WriteOptions WriteOptions { get; set; }
		}

		// Token: 0x0200001B RID: 27
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class ExecuteBatch : DxStoreAccessRequest
		{
			// Token: 0x17000036 RID: 54
			// (get) Token: 0x0600009D RID: 157 RVA: 0x000027CD File Offset: 0x000009CD
			// (set) Token: 0x0600009E RID: 158 RVA: 0x000027D5 File Offset: 0x000009D5
			[DataMember]
			public DxStoreBatchCommand[] Commands { get; set; }

			// Token: 0x17000037 RID: 55
			// (get) Token: 0x0600009F RID: 159 RVA: 0x000027DE File Offset: 0x000009DE
			// (set) Token: 0x060000A0 RID: 160 RVA: 0x000027E6 File Offset: 0x000009E6
			[DataMember]
			public WriteOptions WriteOptions { get; set; }
		}

		// Token: 0x0200001C RID: 28
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetProperty : DxStoreAccessRequest
		{
			// Token: 0x17000038 RID: 56
			// (get) Token: 0x060000A2 RID: 162 RVA: 0x000027F7 File Offset: 0x000009F7
			// (set) Token: 0x060000A3 RID: 163 RVA: 0x000027FF File Offset: 0x000009FF
			[DataMember]
			public string Name { get; set; }

			// Token: 0x17000039 RID: 57
			// (get) Token: 0x060000A4 RID: 164 RVA: 0x00002808 File Offset: 0x00000A08
			// (set) Token: 0x060000A5 RID: 165 RVA: 0x00002810 File Offset: 0x00000A10
			[DataMember]
			public ReadOptions ReadOptions { get; set; }
		}

		// Token: 0x0200001D RID: 29
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetAllProperties : DxStoreAccessRequest
		{
			// Token: 0x1700003A RID: 58
			// (get) Token: 0x060000A7 RID: 167 RVA: 0x00002821 File Offset: 0x00000A21
			// (set) Token: 0x060000A8 RID: 168 RVA: 0x00002829 File Offset: 0x00000A29
			[DataMember]
			public ReadOptions ReadOptions { get; set; }
		}

		// Token: 0x0200001E RID: 30
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetSubkeyNames : DxStoreAccessRequest
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000AA RID: 170 RVA: 0x0000283A File Offset: 0x00000A3A
			// (set) Token: 0x060000AB RID: 171 RVA: 0x00002842 File Offset: 0x00000A42
			[DataMember]
			public ReadOptions ReadOptions { get; set; }
		}

		// Token: 0x0200001F RID: 31
		[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
		[Serializable]
		public class GetPropertyNames : DxStoreAccessRequest
		{
			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000AD RID: 173 RVA: 0x00002853 File Offset: 0x00000A53
			// (set) Token: 0x060000AE RID: 174 RVA: 0x0000285B File Offset: 0x00000A5B
			[DataMember]
			public ReadOptions ReadOptions { get; set; }
		}
	}
}
