using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A84 RID: 2692
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class RemovalAPINotSupportedException : DataSourceOperationException
	{
		// Token: 0x06007F6D RID: 32621 RVA: 0x001A4111 File Offset: 0x001A2311
		public RemovalAPINotSupportedException() : base(DirectoryStrings.ErrorRemovalNotSupported)
		{
		}

		// Token: 0x06007F6E RID: 32622 RVA: 0x001A411E File Offset: 0x001A231E
		public RemovalAPINotSupportedException(Exception innerException) : base(DirectoryStrings.ErrorRemovalNotSupported, innerException)
		{
		}

		// Token: 0x06007F6F RID: 32623 RVA: 0x001A412C File Offset: 0x001A232C
		protected RemovalAPINotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F70 RID: 32624 RVA: 0x001A4136 File Offset: 0x001A2336
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
