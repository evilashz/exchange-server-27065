using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000AB RID: 171
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class UnexpectedPropertyTypeException : RopExecutionException
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x0000E17C File Offset: 0x0000C37C
		public UnexpectedPropertyTypeException(Type expectedType, PropertyValue sourcePropertyValue) : this(expectedType, sourcePropertyValue, null)
		{
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000E187 File Offset: 0x0000C387
		public UnexpectedPropertyTypeException(Type expectedType, PropertyValue sourcePropertyValue, Exception innerException) : base(UnexpectedPropertyTypeException.GetErrorMessage(expectedType, sourcePropertyValue), (ErrorCode)2147942487U, innerException)
		{
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000E19C File Offset: 0x0000C39C
		private static string GetErrorMessage(Type expectedType, PropertyValue sourcePropertyValue)
		{
			return string.Format("Unable to cast {0} to {1}", sourcePropertyValue, expectedType);
		}
	}
}
