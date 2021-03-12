using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Metabase
{
	// Token: 0x02000FA7 RID: 4007
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IMSAdminHelperGetDataPathsCouldNotAllocateException : TaskTransientException
	{
		// Token: 0x0600AD0C RID: 44300 RVA: 0x00290E7E File Offset: 0x0028F07E
		public IMSAdminHelperGetDataPathsCouldNotAllocateException() : base(Strings.IMSAdminHelperGetDataPathsCouldNotAllocateException)
		{
		}

		// Token: 0x0600AD0D RID: 44301 RVA: 0x00290E8B File Offset: 0x0028F08B
		public IMSAdminHelperGetDataPathsCouldNotAllocateException(Exception innerException) : base(Strings.IMSAdminHelperGetDataPathsCouldNotAllocateException, innerException)
		{
		}

		// Token: 0x0600AD0E RID: 44302 RVA: 0x00290E99 File Offset: 0x0028F099
		protected IMSAdminHelperGetDataPathsCouldNotAllocateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AD0F RID: 44303 RVA: 0x00290EA3 File Offset: 0x0028F0A3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
