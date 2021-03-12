using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001152 RID: 4434
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveTenantWideDivergenceException : LocalizedException
	{
		// Token: 0x0600B585 RID: 46469 RVA: 0x0029E67A File Offset: 0x0029C87A
		public CannotRemoveTenantWideDivergenceException(string objectId) : base(Strings.CannotRemoveTenantWideDivergence(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B586 RID: 46470 RVA: 0x0029E68F File Offset: 0x0029C88F
		public CannotRemoveTenantWideDivergenceException(string objectId, Exception innerException) : base(Strings.CannotRemoveTenantWideDivergence(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B587 RID: 46471 RVA: 0x0029E6A5 File Offset: 0x0029C8A5
		protected CannotRemoveTenantWideDivergenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B588 RID: 46472 RVA: 0x0029E6CF File Offset: 0x0029C8CF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395A RID: 14682
		// (get) Token: 0x0600B589 RID: 46473 RVA: 0x0029E6EA File Offset: 0x0029C8EA
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C0 RID: 25280
		private readonly string objectId;
	}
}
