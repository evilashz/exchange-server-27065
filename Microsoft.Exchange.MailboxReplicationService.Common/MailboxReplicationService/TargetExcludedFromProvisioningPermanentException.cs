using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000328 RID: 808
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TargetExcludedFromProvisioningPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x0600257C RID: 9596 RVA: 0x00051859 File Offset: 0x0004FA59
		public TargetExcludedFromProvisioningPermanentException(Guid mdbName) : base(MrsStrings.IsExcludedFromProvisioningError(mdbName))
		{
			this.mdbName = mdbName;
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x0005186E File Offset: 0x0004FA6E
		public TargetExcludedFromProvisioningPermanentException(Guid mdbName, Exception innerException) : base(MrsStrings.IsExcludedFromProvisioningError(mdbName), innerException)
		{
			this.mdbName = mdbName;
		}

		// Token: 0x0600257E RID: 9598 RVA: 0x00051884 File Offset: 0x0004FA84
		protected TargetExcludedFromProvisioningPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbName = (Guid)info.GetValue("mdbName", typeof(Guid));
		}

		// Token: 0x0600257F RID: 9599 RVA: 0x000518AE File Offset: 0x0004FAAE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbName", this.mdbName);
		}

		// Token: 0x17000D72 RID: 3442
		// (get) Token: 0x06002580 RID: 9600 RVA: 0x000518CE File Offset: 0x0004FACE
		public Guid MdbName
		{
			get
			{
				return this.mdbName;
			}
		}

		// Token: 0x04001025 RID: 4133
		private readonly Guid mdbName;
	}
}
