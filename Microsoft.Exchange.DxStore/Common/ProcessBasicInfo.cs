using System;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.DxStore.Common
{
	// Token: 0x02000042 RID: 66
	[DataContract(Namespace = "http://www.outlook.com/highavailability/dxstore/v1/")]
	[Serializable]
	public class ProcessBasicInfo
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00003FE8 File Offset: 0x000021E8
		public ProcessBasicInfo(bool isInitializeWithCurrentProcess)
		{
			using (Process currentProcess = Process.GetCurrentProcess())
			{
				this.Initialize(currentProcess);
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00004024 File Offset: 0x00002224
		public ProcessBasicInfo(int id)
		{
			using (Process processById = Process.GetProcessById(id))
			{
				this.Initialize(processById);
			}
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00004064 File Offset: 0x00002264
		public ProcessBasicInfo(Process process)
		{
			this.Initialize(process);
		}

		// Token: 0x170000DF RID: 223
		// (get) Token: 0x0600022C RID: 556 RVA: 0x00004073 File Offset: 0x00002273
		// (set) Token: 0x0600022D RID: 557 RVA: 0x0000407B File Offset: 0x0000227B
		[DataMember]
		public int Id { get; set; }

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x0600022E RID: 558 RVA: 0x00004084 File Offset: 0x00002284
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000408C File Offset: 0x0000228C
		[DataMember]
		public string Name { get; set; }

		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06000230 RID: 560 RVA: 0x00004095 File Offset: 0x00002295
		// (set) Token: 0x06000231 RID: 561 RVA: 0x0000409D File Offset: 0x0000229D
		[DataMember]
		public DateTimeOffset StartTime { get; set; }

		// Token: 0x06000232 RID: 562 RVA: 0x000040A8 File Offset: 0x000022A8
		internal void Initialize(Process process)
		{
			if (process != null)
			{
				this.Id = process.Id;
				this.Name = process.ProcessName;
				try
				{
					this.StartTime = process.StartTime;
				}
				catch
				{
					this.StartTime = DateTime.MinValue;
				}
			}
		}
	}
}
