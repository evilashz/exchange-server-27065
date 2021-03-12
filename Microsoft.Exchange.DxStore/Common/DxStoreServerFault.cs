using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x0200002B RID: 43
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	public class DxStoreServerFault
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00002A45 File Offset: 0x00000C45
		public DxStoreServerFault()
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00002A50 File Offset: 0x00000C50
		public DxStoreServerFault(Exception ex, DxStoreFaultCode faultCode, bool isTransientError, bool isLocalized)
		{
			this.IsLocalizedException = isLocalized;
			this.FaultCode = faultCode;
			this.IsTransientError = isTransientError;
			this.TypeName = ex.GetType().FullName;
			this.Message = ex.Message;
			this.StackTrace = ex.StackTrace;
		}

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x060000E5 RID: 229 RVA: 0x00002AA2 File Offset: 0x00000CA2
		// (set) Token: 0x060000E6 RID: 230 RVA: 0x00002AAA File Offset: 0x00000CAA
		[DataMember]
		public bool IsTransientError { get; set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x060000E7 RID: 231 RVA: 0x00002AB3 File Offset: 0x00000CB3
		// (set) Token: 0x060000E8 RID: 232 RVA: 0x00002ABB File Offset: 0x00000CBB
		[DataMember]
		public string TypeName { get; set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x060000E9 RID: 233 RVA: 0x00002AC4 File Offset: 0x00000CC4
		// (set) Token: 0x060000EA RID: 234 RVA: 0x00002ACC File Offset: 0x00000CCC
		[DataMember]
		public string Message { get; set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00002AD5 File Offset: 0x00000CD5
		// (set) Token: 0x060000EC RID: 236 RVA: 0x00002ADD File Offset: 0x00000CDD
		[DataMember]
		public string StackTrace { get; set; }

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x060000ED RID: 237 RVA: 0x00002AE6 File Offset: 0x00000CE6
		// (set) Token: 0x060000EE RID: 238 RVA: 0x00002AEE File Offset: 0x00000CEE
		[DataMember]
		public string Context { get; set; }

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00002AF7 File Offset: 0x00000CF7
		// (set) Token: 0x060000F0 RID: 240 RVA: 0x00002AFF File Offset: 0x00000CFF
		[DataMember]
		public int FaultCodeAsInt { get; private set; }

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x00002B08 File Offset: 0x00000D08
		// (set) Token: 0x060000F2 RID: 242 RVA: 0x00002B10 File Offset: 0x00000D10
		[DataMember]
		public bool IsLocalizedException { get; set; }

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x060000F3 RID: 243 RVA: 0x00002B1C File Offset: 0x00000D1C
		// (set) Token: 0x060000F4 RID: 244 RVA: 0x00002B7C File Offset: 0x00000D7C
		[IgnoreDataMember]
		public DxStoreFaultCode FaultCode
		{
			get
			{
				if (this.faultCode == null)
				{
					if (Enum.IsDefined(typeof(DxStoreFaultCode), this.FaultCodeAsInt))
					{
						this.faultCode = new DxStoreFaultCode?((DxStoreFaultCode)this.FaultCodeAsInt);
					}
					else
					{
						this.faultCode = new DxStoreFaultCode?(DxStoreFaultCode.Unknown);
					}
				}
				return this.faultCode.Value;
			}
			set
			{
				this.faultCode = new DxStoreFaultCode?(value);
				this.FaultCodeAsInt = (int)this.faultCode.Value;
			}
		}

		// Token: 0x04000078 RID: 120
		[IgnoreDataMember]
		private DxStoreFaultCode? faultCode;
	}
}
