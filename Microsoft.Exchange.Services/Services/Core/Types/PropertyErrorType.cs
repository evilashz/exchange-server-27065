using System;
using System.Runtime.Serialization;

namespace Microsoft.Exchange.Services.Core.Types
{
	// Token: 0x02000615 RID: 1557
	[DataContract(Namespace = "http://schemas.datacontract.org/2004/07/Exchange")]
	[Serializable]
	public class PropertyErrorType
	{
		// Token: 0x17000ABD RID: 2749
		// (get) Token: 0x060030D2 RID: 12498 RVA: 0x000B6B1C File Offset: 0x000B4D1C
		// (set) Token: 0x060030D3 RID: 12499 RVA: 0x000B6B24 File Offset: 0x000B4D24
		[DataMember(EmitDefaultValue = false)]
		public PropertyPath PropertyPath { get; set; }

		// Token: 0x17000ABE RID: 2750
		// (get) Token: 0x060030D4 RID: 12500 RVA: 0x000B6B2D File Offset: 0x000B4D2D
		// (set) Token: 0x060030D5 RID: 12501 RVA: 0x000B6B35 File Offset: 0x000B4D35
		[IgnoreDataMember]
		public PropertyErrorCodeType ErrorCode { get; set; }

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x060030D6 RID: 12502 RVA: 0x000B6B3E File Offset: 0x000B4D3E
		// (set) Token: 0x060030D7 RID: 12503 RVA: 0x000B6B4B File Offset: 0x000B4D4B
		[DataMember(Name = "ErrorCode", IsRequired = true)]
		public string ErrorCodeString
		{
			get
			{
				return EnumUtilities.ToString<PropertyErrorCodeType>(this.ErrorCode);
			}
			set
			{
				this.ErrorCode = EnumUtilities.Parse<PropertyErrorCodeType>(value);
			}
		}
	}
}
