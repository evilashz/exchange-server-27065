using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000DF2 RID: 3570
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CouldNotFindLocalhostDirectoryEntryException : LocalizedException
	{
		// Token: 0x0600A4B4 RID: 42164 RVA: 0x00284C7D File Offset: 0x00282E7D
		public CouldNotFindLocalhostDirectoryEntryException() : base(Strings.CouldNotFindLocalhostDirectoryEntryException)
		{
		}

		// Token: 0x0600A4B5 RID: 42165 RVA: 0x00284C8A File Offset: 0x00282E8A
		public CouldNotFindLocalhostDirectoryEntryException(Exception innerException) : base(Strings.CouldNotFindLocalhostDirectoryEntryException, innerException)
		{
		}

		// Token: 0x0600A4B6 RID: 42166 RVA: 0x00284C98 File Offset: 0x00282E98
		protected CouldNotFindLocalhostDirectoryEntryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A4B7 RID: 42167 RVA: 0x00284CA2 File Offset: 0x00282EA2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
