using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;

namespace Microsoft.Exchange.Transport.Sync.Common
{
	// Token: 0x02000017 RID: 23
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidCsvFileFormatException : ImportContactsException
	{
		// Token: 0x0600011E RID: 286 RVA: 0x00004F71 File Offset: 0x00003171
		public InvalidCsvFileFormatException() : base(Strings.InvalidCsvFileFormat)
		{
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00004F7E File Offset: 0x0000317E
		public InvalidCsvFileFormatException(Exception innerException) : base(Strings.InvalidCsvFileFormat, innerException)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00004F8C File Offset: 0x0000318C
		protected InvalidCsvFileFormatException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00004F96 File Offset: 0x00003196
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
