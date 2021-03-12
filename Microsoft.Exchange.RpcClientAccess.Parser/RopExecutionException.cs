using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess
{
	// Token: 0x020000A5 RID: 165
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class RopExecutionException : Exception
	{
		// Token: 0x06000404 RID: 1028 RVA: 0x0000E05E File Offset: 0x0000C25E
		public RopExecutionException(string message, ErrorCode error) : base(RopExecutionException.GetErrorMessage(message, error))
		{
			this.ErrorCode = error;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x0000E074 File Offset: 0x0000C274
		public RopExecutionException(string message, ErrorCode error, Exception innerException) : base(RopExecutionException.GetErrorMessage(message, error), innerException)
		{
			this.ErrorCode = error;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x0000E08C File Offset: 0x0000C28C
		public override bool Equals(object obj)
		{
			RopExecutionException ex = obj as RopExecutionException;
			return ex != null && ex.ErrorCode == this.ErrorCode;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x0000E0B3 File Offset: 0x0000C2B3
		public override int GetHashCode()
		{
			return (int)this.ErrorCode;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x0000E0BB File Offset: 0x0000C2BB
		private static string GetErrorMessage(string message, ErrorCode error)
		{
			if (message != null)
			{
				return string.Format("{0}. Error code = {1} (0x{1:X})", message, error);
			}
			return error.ToString();
		}

		// Token: 0x04000270 RID: 624
		public readonly ErrorCode ErrorCode;
	}
}
