using System;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	public class OwaLoadSharedCalendarFailedException : OwaPermanentException
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x0005E9A6 File Offset: 0x0005CBA6
		public OwaLoadSharedCalendarFailedException() : base(null)
		{
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0005E9AF File Offset: 0x0005CBAF
		public OwaLoadSharedCalendarFailedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0005E9B9 File Offset: 0x0005CBB9
		public OwaLoadSharedCalendarFailedException(string message) : base(message)
		{
		}
	}
}
