using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000303 RID: 771
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToObtainServersInLocalSitePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x060024C9 RID: 9417 RVA: 0x0005082A File Offset: 0x0004EA2A
		public UnableToObtainServersInLocalSitePermanentException() : base(MrsStrings.UnableToObtainServersInLocalSite)
		{
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x00050837 File Offset: 0x0004EA37
		public UnableToObtainServersInLocalSitePermanentException(Exception innerException) : base(MrsStrings.UnableToObtainServersInLocalSite, innerException)
		{
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x00050845 File Offset: 0x0004EA45
		protected UnableToObtainServersInLocalSitePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0005084F File Offset: 0x0004EA4F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
