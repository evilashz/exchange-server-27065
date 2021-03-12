using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E37 RID: 3639
	public class ErrorDetails
	{
		// Token: 0x1700153F RID: 5439
		// (get) Token: 0x06005DD4 RID: 24020 RVA: 0x00123F8C File Offset: 0x0012218C
		// (set) Token: 0x06005DD5 RID: 24021 RVA: 0x00123F94 File Offset: 0x00122194
		public string ErrorCode { get; set; }

		// Token: 0x17001540 RID: 5440
		// (get) Token: 0x06005DD6 RID: 24022 RVA: 0x00123F9D File Offset: 0x0012219D
		// (set) Token: 0x06005DD7 RID: 24023 RVA: 0x00123FA5 File Offset: 0x001221A5
		public string ErrorMessage { get; set; }

		// Token: 0x17001541 RID: 5441
		// (get) Token: 0x06005DD8 RID: 24024 RVA: 0x00123FAE File Offset: 0x001221AE
		// (set) Token: 0x06005DD9 RID: 24025 RVA: 0x00123FB6 File Offset: 0x001221B6
		public Exception Exception { get; set; }

		// Token: 0x17001542 RID: 5442
		// (get) Token: 0x06005DDA RID: 24026 RVA: 0x00123FBF File Offset: 0x001221BF
		// (set) Token: 0x06005DDB RID: 24027 RVA: 0x00123FC7 File Offset: 0x001221C7
		public Dictionary<string, string> AdditionalProperties { get; set; }
	}
}
