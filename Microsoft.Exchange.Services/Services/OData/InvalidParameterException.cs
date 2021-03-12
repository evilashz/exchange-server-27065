using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E16 RID: 3606
	internal class InvalidParameterException : ODataResponseException
	{
		// Token: 0x06005D4A RID: 23882 RVA: 0x00122F1D File Offset: 0x0012111D
		public InvalidParameterException(string parameterName, LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidParameter, errorMessage, null)
		{
			this.ParameterName = parameterName;
		}

		// Token: 0x06005D4B RID: 23883 RVA: 0x00122F38 File Offset: 0x00121138
		public InvalidParameterException(string parameterName) : this(parameterName, CoreResources.ErrorInvalidParameter(parameterName))
		{
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06005D4C RID: 23884 RVA: 0x00122F47 File Offset: 0x00121147
		// (set) Token: 0x06005D4D RID: 23885 RVA: 0x00122F4F File Offset: 0x0012114F
		public string ParameterName { get; private set; }
	}
}
