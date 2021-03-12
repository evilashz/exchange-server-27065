using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001146 RID: 4422
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceFailedToStartException : LocalizedException
	{
		// Token: 0x0600B543 RID: 46403 RVA: 0x0029DEE5 File Offset: 0x0029C0E5
		public ServiceFailedToStartException(string service) : base(Strings.ServiceFailedToStart(service))
		{
			this.service = service;
		}

		// Token: 0x0600B544 RID: 46404 RVA: 0x0029DEFA File Offset: 0x0029C0FA
		public ServiceFailedToStartException(string service, Exception innerException) : base(Strings.ServiceFailedToStart(service), innerException)
		{
			this.service = service;
		}

		// Token: 0x0600B545 RID: 46405 RVA: 0x0029DF10 File Offset: 0x0029C110
		protected ServiceFailedToStartException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.service = (string)info.GetValue("service", typeof(string));
		}

		// Token: 0x0600B546 RID: 46406 RVA: 0x0029DF3A File Offset: 0x0029C13A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("service", this.service);
		}

		// Token: 0x17003948 RID: 14664
		// (get) Token: 0x0600B547 RID: 46407 RVA: 0x0029DF55 File Offset: 0x0029C155
		public string Service
		{
			get
			{
				return this.service;
			}
		}

		// Token: 0x040062AE RID: 25262
		private readonly string service;
	}
}
