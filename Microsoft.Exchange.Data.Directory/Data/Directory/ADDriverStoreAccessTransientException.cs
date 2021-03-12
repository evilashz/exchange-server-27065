using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A85 RID: 2693
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADDriverStoreAccessTransientException : ADTransientException
	{
		// Token: 0x06007F71 RID: 32625 RVA: 0x001A4140 File Offset: 0x001A2340
		public ADDriverStoreAccessTransientException() : base(DirectoryStrings.ADDriverStoreAccessTransientError)
		{
		}

		// Token: 0x06007F72 RID: 32626 RVA: 0x001A414D File Offset: 0x001A234D
		public ADDriverStoreAccessTransientException(Exception innerException) : base(DirectoryStrings.ADDriverStoreAccessTransientError, innerException)
		{
		}

		// Token: 0x06007F73 RID: 32627 RVA: 0x001A415B File Offset: 0x001A235B
		protected ADDriverStoreAccessTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F74 RID: 32628 RVA: 0x001A4165 File Offset: 0x001A2365
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
