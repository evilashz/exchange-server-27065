using System;
using System.CodeDom.Compiler;
using System.Diagnostics;
using System.Runtime.Serialization;

namespace schemas.microsoft.com.O365Filtering.GlobalLocatorService.Data
{
	// Token: 0x02000C2A RID: 3114
	[DataContract(Name = "GLSProperty", Namespace = "http://schemas.microsoft.com/O365Filtering/GlobalLocatorService/Data")]
	[GeneratedCode("System.Runtime.Serialization", "4.0.0.0")]
	[DebuggerStepThrough]
	public class GLSProperty : DITimeStamp
	{
		// Token: 0x17001128 RID: 4392
		// (get) Token: 0x06004449 RID: 17481 RVA: 0x000B6B8C File Offset: 0x000B4D8C
		// (set) Token: 0x0600444A RID: 17482 RVA: 0x000B6B94 File Offset: 0x000B4D94
		[DataMember(IsRequired = true)]
		public string PropertyName
		{
			get
			{
				return this.PropertyNameField;
			}
			set
			{
				this.PropertyNameField = value;
			}
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x000B6B9D File Offset: 0x000B4D9D
		// (set) Token: 0x0600444C RID: 17484 RVA: 0x000B6BA5 File Offset: 0x000B4DA5
		[DataMember(IsRequired = true)]
		public string PropertyValue
		{
			get
			{
				return this.PropertyValueField;
			}
			set
			{
				this.PropertyValueField = value;
			}
		}

		// Token: 0x040039E3 RID: 14819
		private string PropertyNameField;

		// Token: 0x040039E4 RID: 14820
		private string PropertyValueField;
	}
}
