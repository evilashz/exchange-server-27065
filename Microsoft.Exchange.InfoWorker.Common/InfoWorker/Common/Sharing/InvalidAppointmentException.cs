using System;

namespace Microsoft.Exchange.InfoWorker.Common.Sharing
{
	// Token: 0x02000263 RID: 611
	[Serializable]
	public sealed class InvalidAppointmentException : SharingSynchronizationException
	{
		// Token: 0x06001187 RID: 4487 RVA: 0x00050C44 File Offset: 0x0004EE44
		public InvalidAppointmentException() : base(Strings.InvalidAppointmentException)
		{
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x00050C51 File Offset: 0x0004EE51
		public InvalidAppointmentException(Exception innerException) : base(Strings.InvalidAppointmentException, innerException)
		{
		}
	}
}
