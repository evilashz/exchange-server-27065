using System;
using System.Net;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E1A RID: 3610
	internal class InvalidPropertyException : ODataResponseException
	{
		// Token: 0x06005D54 RID: 23892 RVA: 0x00122FCD File Offset: 0x001211CD
		public InvalidPropertyException(string propertyName) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidProperty, CoreResources.ErrorInvalidProperty(propertyName), null)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x00122FED File Offset: 0x001211ED
		public InvalidPropertyException(string propertyName, LocalizedString errorMessage) : base(HttpStatusCode.BadRequest, ResponseCodeType.ErrorInvalidProperty, errorMessage, null)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x1700151D RID: 5405
		// (get) Token: 0x06005D56 RID: 23894 RVA: 0x00123008 File Offset: 0x00121208
		// (set) Token: 0x06005D57 RID: 23895 RVA: 0x00123010 File Offset: 0x00121210
		public string PropertyName { get; private set; }
	}
}
