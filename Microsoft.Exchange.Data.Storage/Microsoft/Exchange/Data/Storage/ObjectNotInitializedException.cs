using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200075A RID: 1882
	[Serializable]
	public class ObjectNotInitializedException : StorageTransientException
	{
		// Token: 0x0600485F RID: 18527 RVA: 0x00130EC8 File Offset: 0x0012F0C8
		public ObjectNotInitializedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x00130ED1 File Offset: 0x0012F0D1
		public ObjectNotInitializedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00130EDB File Offset: 0x0012F0DB
		protected ObjectNotInitializedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}
