using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200073A RID: 1850
	[Serializable]
	public class InvalidLicenseException : StoragePermanentException
	{
		// Token: 0x060047FB RID: 18427 RVA: 0x0013081B File Offset: 0x0012EA1B
		public InvalidLicenseException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x00130824 File Offset: 0x0012EA24
		public InvalidLicenseException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060047FD RID: 18429 RVA: 0x0013082E File Offset: 0x0012EA2E
		protected InvalidLicenseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
