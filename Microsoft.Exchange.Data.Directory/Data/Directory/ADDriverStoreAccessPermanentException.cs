using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A86 RID: 2694
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADDriverStoreAccessPermanentException : ADOperationException
	{
		// Token: 0x06007F75 RID: 32629 RVA: 0x001A416F File Offset: 0x001A236F
		public ADDriverStoreAccessPermanentException() : base(DirectoryStrings.ADDriverStoreAccessPermanentError)
		{
		}

		// Token: 0x06007F76 RID: 32630 RVA: 0x001A417C File Offset: 0x001A237C
		public ADDriverStoreAccessPermanentException(Exception innerException) : base(DirectoryStrings.ADDriverStoreAccessPermanentError, innerException)
		{
		}

		// Token: 0x06007F77 RID: 32631 RVA: 0x001A418A File Offset: 0x001A238A
		protected ADDriverStoreAccessPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F78 RID: 32632 RVA: 0x001A4194 File Offset: 0x001A2394
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
