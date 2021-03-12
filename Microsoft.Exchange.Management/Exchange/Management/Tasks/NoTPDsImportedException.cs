using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D2 RID: 4306
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoTPDsImportedException : LocalizedException
	{
		// Token: 0x0600B30C RID: 45836 RVA: 0x0029AABF File Offset: 0x00298CBF
		public NoTPDsImportedException() : base(Strings.NoTPDsImported)
		{
		}

		// Token: 0x0600B30D RID: 45837 RVA: 0x0029AACC File Offset: 0x00298CCC
		public NoTPDsImportedException(Exception innerException) : base(Strings.NoTPDsImported, innerException)
		{
		}

		// Token: 0x0600B30E RID: 45838 RVA: 0x0029AADA File Offset: 0x00298CDA
		protected NoTPDsImportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B30F RID: 45839 RVA: 0x0029AAE4 File Offset: 0x00298CE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
