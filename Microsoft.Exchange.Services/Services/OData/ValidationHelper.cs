using System;
using System.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.OData
{
	// Token: 0x02000DF9 RID: 3577
	internal static class ValidationHelper
	{
		// Token: 0x06005C91 RID: 23697 RVA: 0x00120984 File Offset: 0x0011EB84
		public static void ValidateIdEmpty(string id)
		{
			if (string.IsNullOrEmpty(id))
			{
				throw new IdEmptyException();
			}
		}

		// Token: 0x06005C92 RID: 23698 RVA: 0x00120994 File Offset: 0x0011EB94
		public static void ValidateParameterEmpty(string parameterName, object parameterValue)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("parameterName", parameterName);
			if (parameterValue == null)
			{
				throw new ParameterValueEmptyException(parameterName);
			}
			string text = parameterValue as string;
			if (text != null && string.IsNullOrEmpty(text))
			{
				throw new ParameterValueEmptyException(parameterName);
			}
			ICollection collection = parameterValue as ICollection;
			if (collection != null && collection.Count == 0)
			{
				throw new ParameterValueEmptyException(parameterName);
			}
		}
	}
}
