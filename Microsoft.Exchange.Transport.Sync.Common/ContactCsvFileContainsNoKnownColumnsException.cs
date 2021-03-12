using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x0200001A RID: 26
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class ContactCsvFileContainsNoKnownColumnsException : ImportContactsException
	{
		// Token: 0x0600012B RID: 299 RVA: 0x00005047 File Offset: 0x00003247
		public ContactCsvFileContainsNoKnownColumnsException() : base(Strings.ContactCsvFileContainsNoKnownColumns)
		{
		}

		// Token: 0x0600012C RID: 300 RVA: 0x00005054 File Offset: 0x00003254
		public ContactCsvFileContainsNoKnownColumnsException(Exception innerException) : base(Strings.ContactCsvFileContainsNoKnownColumns, innerException)
		{
		}

		// Token: 0x0600012D RID: 301 RVA: 0x00005062 File Offset: 0x00003262
		protected ContactCsvFileContainsNoKnownColumnsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000506C File Offset: 0x0000326C
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
