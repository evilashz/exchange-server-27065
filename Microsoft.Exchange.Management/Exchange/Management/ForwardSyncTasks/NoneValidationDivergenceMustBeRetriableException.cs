using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001157 RID: 4439
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NoneValidationDivergenceMustBeRetriableException : LocalizedException
	{
		// Token: 0x0600B59E RID: 46494 RVA: 0x0029E8D2 File Offset: 0x0029CAD2
		public NoneValidationDivergenceMustBeRetriableException(string objectId) : base(Strings.NoneValidationDivergenceMustBeRetriable(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B59F RID: 46495 RVA: 0x0029E8E7 File Offset: 0x0029CAE7
		public NoneValidationDivergenceMustBeRetriableException(string objectId, Exception innerException) : base(Strings.NoneValidationDivergenceMustBeRetriable(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B5A0 RID: 46496 RVA: 0x0029E8FD File Offset: 0x0029CAFD
		protected NoneValidationDivergenceMustBeRetriableException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B5A1 RID: 46497 RVA: 0x0029E927 File Offset: 0x0029CB27
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395F RID: 14687
		// (get) Token: 0x0600B5A2 RID: 46498 RVA: 0x0029E942 File Offset: 0x0029CB42
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C5 RID: 25285
		private readonly string objectId;
	}
}
