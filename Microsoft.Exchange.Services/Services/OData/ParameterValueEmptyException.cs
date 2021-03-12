using System;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000E17 RID: 3607
	internal class ParameterValueEmptyException : InvalidParameterException
	{
		// Token: 0x06005D4E RID: 23886 RVA: 0x00122F58 File Offset: 0x00121158
		public ParameterValueEmptyException(string parameterName) : base(parameterName, CoreResources.ErrorParameterValueEmpty(parameterName))
		{
		}
	}
}
