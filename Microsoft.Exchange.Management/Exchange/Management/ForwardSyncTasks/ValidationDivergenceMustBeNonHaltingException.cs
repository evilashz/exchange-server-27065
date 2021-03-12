using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001155 RID: 4437
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ValidationDivergenceMustBeNonHaltingException : LocalizedException
	{
		// Token: 0x0600B594 RID: 46484 RVA: 0x0029E7E2 File Offset: 0x0029C9E2
		public ValidationDivergenceMustBeNonHaltingException(string objectId) : base(Strings.ValidationDivergenceMustBeNonHalting(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B595 RID: 46485 RVA: 0x0029E7F7 File Offset: 0x0029C9F7
		public ValidationDivergenceMustBeNonHaltingException(string objectId, Exception innerException) : base(Strings.ValidationDivergenceMustBeNonHalting(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B596 RID: 46486 RVA: 0x0029E80D File Offset: 0x0029CA0D
		protected ValidationDivergenceMustBeNonHaltingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B597 RID: 46487 RVA: 0x0029E837 File Offset: 0x0029CA37
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395D RID: 14685
		// (get) Token: 0x0600B598 RID: 46488 RVA: 0x0029E852 File Offset: 0x0029CA52
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C3 RID: 25283
		private readonly string objectId;
	}
}
