using System;

namespace Microsoft.Exchange.InfoWorker.Common.Availability
{
	// Token: 0x0200008D RID: 141
	internal class Win32InteropException : AvailabilityException
	{
		// Token: 0x06000351 RID: 849 RVA: 0x0000E884 File Offset: 0x0000CA84
		public Win32InteropException(Exception innerException) : base(ErrorConstants.Win32InteropError, Strings.descWin32InteropError, innerException)
		{
		}
	}
}
