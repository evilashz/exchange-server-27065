using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ForwardSyncTasks
{
	// Token: 0x02001151 RID: 4433
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceInstanceNotMatchException : LocalizedException
	{
		// Token: 0x0600B57E RID: 46462 RVA: 0x0029E561 File Offset: 0x0029C761
		public ServiceInstanceNotMatchException(string objectId, string requestServiceInstance, string objectServiceInstance) : base(Strings.ServiceInstanceNotMatchMessage(objectId, requestServiceInstance, objectServiceInstance))
		{
			this.objectId = objectId;
			this.requestServiceInstance = requestServiceInstance;
			this.objectServiceInstance = objectServiceInstance;
		}

		// Token: 0x0600B57F RID: 46463 RVA: 0x0029E586 File Offset: 0x0029C786
		public ServiceInstanceNotMatchException(string objectId, string requestServiceInstance, string objectServiceInstance, Exception innerException) : base(Strings.ServiceInstanceNotMatchMessage(objectId, requestServiceInstance, objectServiceInstance), innerException)
		{
			this.objectId = objectId;
			this.requestServiceInstance = requestServiceInstance;
			this.objectServiceInstance = objectServiceInstance;
		}

		// Token: 0x0600B580 RID: 46464 RVA: 0x0029E5B0 File Offset: 0x0029C7B0
		protected ServiceInstanceNotMatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.objectId = (string)info.GetValue("objectId", typeof(string));
			this.requestServiceInstance = (string)info.GetValue("requestServiceInstance", typeof(string));
			this.objectServiceInstance = (string)info.GetValue("objectServiceInstance", typeof(string));
		}

		// Token: 0x0600B581 RID: 46465 RVA: 0x0029E625 File Offset: 0x0029C825
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("objectId", this.objectId);
			info.AddValue("requestServiceInstance", this.requestServiceInstance);
			info.AddValue("objectServiceInstance", this.objectServiceInstance);
		}

		// Token: 0x17003957 RID: 14679
		// (get) Token: 0x0600B582 RID: 46466 RVA: 0x0029E662 File Offset: 0x0029C862
		public string ObjectId
		{
			get
			{
				return this.objectId;
			}
		}

		// Token: 0x17003958 RID: 14680
		// (get) Token: 0x0600B583 RID: 46467 RVA: 0x0029E66A File Offset: 0x0029C86A
		public string RequestServiceInstance
		{
			get
			{
				return this.requestServiceInstance;
			}
		}

		// Token: 0x17003959 RID: 14681
		// (get) Token: 0x0600B584 RID: 46468 RVA: 0x0029E672 File Offset: 0x0029C872
		public string ObjectServiceInstance
		{
			get
			{
				return this.objectServiceInstance;
			}
		}

		// Token: 0x040062BD RID: 25277
		private readonly string objectId;

		// Token: 0x040062BE RID: 25278
		private readonly string requestServiceInstance;

		// Token: 0x040062BF RID: 25279
		private readonly string objectServiceInstance;
	}
}
