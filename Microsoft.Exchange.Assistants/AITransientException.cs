using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x02000042 RID: 66
	internal abstract class AITransientException : AIException
	{
		// Token: 0x06000286 RID: 646 RVA: 0x0000E6FA File Offset: 0x0000C8FA
		protected AITransientException(LocalizedString explain, Exception innerException, RetrySchedule retrySchedule) : base(explain, innerException)
		{
			this.retrySchedule = (retrySchedule ?? AITransientException.genericSchedule);
		}

		// Token: 0x06000287 RID: 647 RVA: 0x0000E714 File Offset: 0x0000C914
		protected AITransientException(Exception innerException) : this(Strings.descTransientException, innerException, null)
		{
		}

		// Token: 0x06000288 RID: 648 RVA: 0x0000E723 File Offset: 0x0000C923
		protected AITransientException(LocalizedString explain, Exception innerException) : this(explain, innerException, null)
		{
		}

		// Token: 0x06000289 RID: 649 RVA: 0x0000E72E File Offset: 0x0000C92E
		protected AITransientException(LocalizedString explain) : this(explain, null, null)
		{
		}

		// Token: 0x170000BC RID: 188
		// (get) Token: 0x0600028A RID: 650 RVA: 0x0000E739 File Offset: 0x0000C939
		// (set) Token: 0x0600028B RID: 651 RVA: 0x0000E741 File Offset: 0x0000C941
		public RetrySchedule RetrySchedule
		{
			get
			{
				return this.retrySchedule;
			}
			set
			{
				this.retrySchedule = value;
			}
		}

		// Token: 0x040001AE RID: 430
		private static RetrySchedule genericSchedule = new RetrySchedule(FinalAction.Skip, TimeSpan.FromDays(1.0), new TimeSpan[]
		{
			TimeSpan.Zero,
			TimeSpan.FromSeconds(5.0),
			TimeSpan.FromMinutes(1.0),
			TimeSpan.FromMinutes(5.0),
			TimeSpan.FromMinutes(15.0),
			TimeSpan.FromHours(1.0)
		});

		// Token: 0x040001AF RID: 431
		private RetrySchedule retrySchedule;
	}
}
