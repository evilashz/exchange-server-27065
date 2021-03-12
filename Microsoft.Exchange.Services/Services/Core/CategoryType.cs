using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x020005AA RID: 1450
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class CategoryType
	{
		// Token: 0x06002AC8 RID: 10952 RVA: 0x000AEBAC File Offset: 0x000ACDAC
		public CategoryType(string name, int color)
		{
			this.Name = name;
			this.Color = color;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x06002AC9 RID: 10953 RVA: 0x000AEBC2 File Offset: 0x000ACDC2
		// (set) Token: 0x06002ACA RID: 10954 RVA: 0x000AEBCA File Offset: 0x000ACDCA
		[DataMember(EmitDefaultValue = false, Order = 1)]
		public string Name { get; set; }

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x06002ACB RID: 10955 RVA: 0x000AEBD3 File Offset: 0x000ACDD3
		// (set) Token: 0x06002ACC RID: 10956 RVA: 0x000AEBDB File Offset: 0x000ACDDB
		[DataMember(EmitDefaultValue = true, Order = 2)]
		public int Color { get; set; }
	}
}
