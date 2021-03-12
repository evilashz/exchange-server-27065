using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020000B1 RID: 177
	[DataContract]
	public class SetResponseMessageConfiguration : SetResourceConfigurationBase
	{
		// Token: 0x170018E7 RID: 6375
		// (get) Token: 0x06001C4D RID: 7245 RVA: 0x0005871E File Offset: 0x0005691E
		// (set) Token: 0x06001C4E RID: 7246 RVA: 0x0005873A File Offset: 0x0005693A
		[DataMember]
		public bool AddAdditionalResponse
		{
			get
			{
				return (bool)(base["AddAdditionalResponse"] ?? false);
			}
			set
			{
				base["AddAdditionalResponse"] = value;
			}
		}

		// Token: 0x170018E8 RID: 6376
		// (get) Token: 0x06001C4F RID: 7247 RVA: 0x0005874D File Offset: 0x0005694D
		// (set) Token: 0x06001C50 RID: 7248 RVA: 0x0005875F File Offset: 0x0005695F
		[DataMember]
		public string AdditionalResponse
		{
			get
			{
				return (string)base["AdditionalResponse"];
			}
			set
			{
				base["AdditionalResponse"] = value;
			}
		}
	}
}
