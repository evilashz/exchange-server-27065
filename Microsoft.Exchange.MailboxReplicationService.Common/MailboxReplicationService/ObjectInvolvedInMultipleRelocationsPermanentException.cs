using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x020002EA RID: 746
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ObjectInvolvedInMultipleRelocationsPermanentException : MailboxReplicationPermanentException
	{
		// Token: 0x06002454 RID: 9300 RVA: 0x0004FE78 File Offset: 0x0004E078
		public ObjectInvolvedInMultipleRelocationsPermanentException(LocalizedString objectInvolved, string requestGuids) : base(MrsStrings.ValidationObjectInvolvedInMultipleRelocations(objectInvolved, requestGuids))
		{
			this.objectInvolved = objectInvolved;
			this.requestGuids = requestGuids;
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x0004FE95 File Offset: 0x0004E095
		public ObjectInvolvedInMultipleRelocationsPermanentException(LocalizedString objectInvolved, string requestGuids, Exception innerException) : base(MrsStrings.ValidationObjectInvolvedInMultipleRelocations(objectInvolved, requestGuids), innerException)
		{
			this.objectInvolved = objectInvolved;
			this.requestGuids = requestGuids;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x0004FEB4 File Offset: 0x0004E0B4
		protected ObjectInvolvedInMultipleRelocationsPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectInvolved = (LocalizedString)info.GetValue("objectInvolved", typeof(LocalizedString));
			this.requestGuids = (string)info.GetValue("requestGuids", typeof(string));
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x0004FF09 File Offset: 0x0004E109
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectInvolved", this.objectInvolved);
			info.AddValue("requestGuids", this.requestGuids);
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x06002458 RID: 9304 RVA: 0x0004FF3A File Offset: 0x0004E13A
		public LocalizedString ObjectInvolved
		{
			get
			{
				return this.objectInvolved;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x06002459 RID: 9305 RVA: 0x0004FF42 File Offset: 0x0004E142
		public string RequestGuids
		{
			get
			{
				return this.requestGuids;
			}
		}

		// Token: 0x04000FF5 RID: 4085
		private readonly LocalizedString objectInvolved;

		// Token: 0x04000FF6 RID: 4086
		private readonly string requestGuids;
	}
}
