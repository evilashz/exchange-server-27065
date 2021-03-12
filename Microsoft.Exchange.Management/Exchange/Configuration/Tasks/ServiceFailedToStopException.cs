using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001147 RID: 4423
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceFailedToStopException : LocalizedException
	{
		// Token: 0x0600B548 RID: 46408 RVA: 0x0029DF5D File Offset: 0x0029C15D
		public ServiceFailedToStopException(string service) : base(Strings.ServiceFailedToStop(service))
		{
			this.service = service;
		}

		// Token: 0x0600B549 RID: 46409 RVA: 0x0029DF72 File Offset: 0x0029C172
		public ServiceFailedToStopException(string service, Exception innerException) : base(Strings.ServiceFailedToStop(service), innerException)
		{
			this.service = service;
		}

		// Token: 0x0600B54A RID: 46410 RVA: 0x0029DF88 File Offset: 0x0029C188
		protected ServiceFailedToStopException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.service = (string)info.GetValue("service", typeof(string));
		}

		// Token: 0x0600B54B RID: 46411 RVA: 0x0029DFB2 File Offset: 0x0029C1B2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("service", this.service);
		}

		// Token: 0x17003949 RID: 14665
		// (get) Token: 0x0600B54C RID: 46412 RVA: 0x0029DFCD File Offset: 0x0029C1CD
		public string Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x040062AF RID: 25263
		private readonly string service;
	}
}
