using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001154 RID: 4436
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DivergenceAlreadyExistsException : LocalizedException
	{
		// Token: 0x0600B58F RID: 46479 RVA: 0x0029E76A File Offset: 0x0029C96A
		public DivergenceAlreadyExistsException(string objectId) : base(Strings.DivergenceAlreadyExists(objectId))
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B590 RID: 46480 RVA: 0x0029E77F File Offset: 0x0029C97F
		public DivergenceAlreadyExistsException(string objectId, Exception innerException) : base(Strings.DivergenceAlreadyExists(objectId), innerException)
		{
			this.objectId = objectId;
		}

		// Token: 0x0600B591 RID: 46481 RVA: 0x0029E795 File Offset: 0x0029C995
		protected DivergenceAlreadyExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
		}

		// Token: 0x0600B592 RID: 46482 RVA: 0x0029E7BF File Offset: 0x0029C9BF
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
		}

		// Token: 0x1700395C RID: 14684
		// (get) Token: 0x0600B593 RID: 46483 RVA: 0x0029E7DA File Offset: 0x0029C9DA
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x040062C2 RID: 25282
		private readonly string objectId;
	}
}
