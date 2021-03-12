using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001156 RID: 4438
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotBeBothValidationDivergenceAndTenantWideDivergenceException : LocalizedException
	{
		// Token: 0x0600B599 RID: 46489 RVA: 0x0029E85A File Offset: 0x0029CA5A
		public CannotBeBothValidationDivergenceAndTenantWideDivergenceException(string objectId) : base(Strings.CannotBeBothValidationDivergenceAndTenantWideDivergence(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B59A RID: 46490 RVA: 0x0029E86F File Offset: 0x0029CA6F
		public CannotBeBothValidationDivergenceAndTenantWideDivergenceException(string objectId, Exception innerException) : base(Strings.CannotBeBothValidationDivergenceAndTenantWideDivergence(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B59B RID: 46491 RVA: 0x0029E885 File Offset: 0x0029CA85
		protected CannotBeBothValidationDivergenceAndTenantWideDivergenceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B59C RID: 46492 RVA: 0x0029E8AF File Offset: 0x0029CAAF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395E RID: 14686
		// (get) Token: 0x0600B59D RID: 46493 RVA: 0x0029E8CA File Offset: 0x0029CACA
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C4 RID: 25284
		private readonly string objectId;
	}
}
