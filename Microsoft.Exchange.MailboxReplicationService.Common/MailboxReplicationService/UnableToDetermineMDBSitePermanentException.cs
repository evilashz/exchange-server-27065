using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002BC RID: 700
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UnableToDetermineMDBSitePermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002371 RID: 9073 RVA: 0x0004E918 File Offset: 0x0004CB18
		public UnableToDetermineMDBSitePermanentException(Guid mdbGuid) : base(MrsStrings.UnableToDetermineMDBSite(mdbGuid))
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x06002372 RID: 9074 RVA: 0x0004E92D File Offset: 0x0004CB2D
		public UnableToDetermineMDBSitePermanentException(Guid mdbGuid, Exception innerException) : base(MrsStrings.UnableToDetermineMDBSite(mdbGuid), innerException)
		{
			this.mdbGuid = mdbGuid;
		}

		// Token: 0x06002373 RID: 9075 RVA: 0x0004E943 File Offset: 0x0004CB43
		protected UnableToDetermineMDBSitePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mdbGuid = (Guid)info.GetValue("mdbGuid", typeof(Guid));
		}

		// Token: 0x06002374 RID: 9076 RVA: 0x0004E96D File Offset: 0x0004CB6D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mdbGuid", this.mdbGuid);
		}

		// Token: 0x17000D17 RID: 3351
		// (get) Token: 0x06002375 RID: 9077 RVA: 0x0004E98D File Offset: 0x0004CB8D
		public Guid MdbGuid
		{
			get
			{
				return this.mdbGuid;
			}
		}

		// Token: 0x04000FCA RID: 4042
		private readonly Guid mdbGuid;
	}
}
