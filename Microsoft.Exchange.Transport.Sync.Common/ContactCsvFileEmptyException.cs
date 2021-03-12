using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000019 RID: 25
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ContactCsvFileEmptyException : ImportContactsException
	{
		// Token: 0x06000127 RID: 295 RVA: 0x00005018 File Offset: 0x00003218
		public ContactCsvFileEmptyException() : base(Strings.ContactCsvFileEmpty)
		{
		}

		// Token: 0x06000128 RID: 296 RVA: 0x00005025 File Offset: 0x00003225
		public ContactCsvFileEmptyException(Exception innerException) : base(Strings.ContactCsvFileEmpty, innerException)
		{
		}

		// Token: 0x06000129 RID: 297 RVA: 0x00005033 File Offset: 0x00003233
		protected ContactCsvFileEmptyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000503D File Offset: 0x0000323D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
