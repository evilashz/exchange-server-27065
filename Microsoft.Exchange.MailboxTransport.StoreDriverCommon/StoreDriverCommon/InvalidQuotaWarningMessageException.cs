using System;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverCommon
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	internal class InvalidQuotaWarningMessageException : StoragePermanentException
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000239E File Offset: 0x0000059E
		public InvalidQuotaWarningMessageException(string errorDescription) : base(LocalizedString.Empty)
		{
			this.errorDescription = errorDescription;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000023B2 File Offset: 0x000005B2
		public static void ThrowExceptionForMissingProp(string propertyName)
		{
			throw new InvalidQuotaWarningMessageException("Property " + propertyName + " is not present");
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000023C9 File Offset: 0x000005C9
		public static void ThrowExceptionForUnexpectedProp(string propertyName)
		{
			throw new InvalidQuotaWarningMessageException("Property " + propertyName + " should not exist");
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000023E0 File Offset: 0x000005E0
		public override string ToString()
		{
			return this.errorDescription;
		}

		// Token: 0x04000007 RID: 7
		private readonly string errorDescription;
	}
}
