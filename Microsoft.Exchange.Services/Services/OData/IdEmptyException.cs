using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Services.Core.Types;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E19 RID: 3609
	internal class IdEmptyException : InvalidIdException
	{
		// Token: 0x06005D52 RID: 23890 RVA: 0x00122FAD File Offset: 0x001211AD
		public IdEmptyException() : base(ResponseCodeType.ErrorInvalidIdEmpty, CoreResources.ErrorInvalidIdEmpty)
		{
		}

		// Token: 0x06005D53 RID: 23891 RVA: 0x00122FBF File Offset: 0x001211BF
		public IdEmptyException(LocalizedString errorMessage) : base(ResponseCodeType.ErrorInvalidIdEmpty, errorMessage)
		{
		}
	}
}
