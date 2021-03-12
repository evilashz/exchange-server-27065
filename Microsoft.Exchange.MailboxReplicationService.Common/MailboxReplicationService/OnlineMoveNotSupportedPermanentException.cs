using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002F4 RID: 756
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnlineMoveNotSupportedPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002482 RID: 9346 RVA: 0x00050224 File Offset: 0x0004E424
		public OnlineMoveNotSupportedPermanentException(string mbxGuid) : base(MrsStrings.OnlineMoveNotSupported(mbxGuid))
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x00050239 File Offset: 0x0004E439
		public OnlineMoveNotSupportedPermanentException(string mbxGuid, Exception innerException) : base(MrsStrings.OnlineMoveNotSupported(mbxGuid), innerException)
		{
			this.mbxGuid = mbxGuid;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0005024F File Offset: 0x0004E44F
		protected OnlineMoveNotSupportedPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.mbxGuid = (string)info.GetValue("mbxGuid", typeof(string));
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x00050279 File Offset: 0x0004E479
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("mbxGuid", this.mbxGuid);
		}

		// Token: 0x17000D48 RID: 3400
		// (get) Token: 0x06002486 RID: 9350 RVA: 0x00050294 File Offset: 0x0004E494
		public string MbxGuid
		{
			get
			{
				return this.mbxGuid;
			}
		}

		// Token: 0x04000FFB RID: 4091
		private readonly string mbxGuid;
	}
}
