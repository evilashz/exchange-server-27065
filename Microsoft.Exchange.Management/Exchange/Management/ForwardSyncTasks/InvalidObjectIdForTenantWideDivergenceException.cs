using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001153 RID: 4435
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidObjectIdForTenantWideDivergenceException : LocalizedException
	{
		// Token: 0x0600B58A RID: 46474 RVA: 0x0029E6F2 File Offset: 0x0029C8F2
		public InvalidObjectIdForTenantWideDivergenceException(string objectId) : base(Strings.InvalidObjectIdForTenantWideDivergence(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B58B RID: 46475 RVA: 0x0029E707 File Offset: 0x0029C907
		public InvalidObjectIdForTenantWideDivergenceException(string objectId, Exception innerException) : base(Strings.InvalidObjectIdForTenantWideDivergence(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B58C RID: 46476 RVA: 0x0029E71D File Offset: 0x0029C91D
		protected InvalidObjectIdForTenantWideDivergenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B58D RID: 46477 RVA: 0x0029E747 File Offset: 0x0029C947
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395B RID: 14683
		// (get) Token: 0x0600B58E RID: 46478 RVA: 0x0029E762 File Offset: 0x0029C962
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C1 RID: 25281
		private readonly string objectId;
	}
}
