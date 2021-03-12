using System;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020006C6 RID: 1734
	public class ShouldContinueException : TransientException, IExceptionDetails
	{
		// Token: 0x060049CD RID: 18893 RVA: 0x000E13F9 File Offset: 0x000DF5F9
		public ShouldContinueException(string message) : this(new LocalizedString(message), null, null)
		{
		}

		// Token: 0x060049CE RID: 18894 RVA: 0x000E140E File Offset: 0x000DF60E
		public ShouldContinueException(string message, string cmdlet, string suppressConfirmParameterName) : base(new LocalizedString(message))
		{
			this.Details = new ShouldContinueExceptionDetails(cmdlet, suppressConfirmParameterName);
		}

		// Token: 0x17002808 RID: 10248
		// (get) Token: 0x060049CF RID: 18895 RVA: 0x000E1429 File Offset: 0x000DF629
		// (set) Token: 0x060049D0 RID: 18896 RVA: 0x000E1431 File Offset: 0x000DF631
		public ShouldContinueExceptionDetails Details { get; private set; }

		// Token: 0x17002809 RID: 10249
		// (get) Token: 0x060049D1 RID: 18897 RVA: 0x000E143A File Offset: 0x000DF63A
		object IExceptionDetails.Details
		{
			get
			{
				return this.Details;
			}
		}
	}
}
