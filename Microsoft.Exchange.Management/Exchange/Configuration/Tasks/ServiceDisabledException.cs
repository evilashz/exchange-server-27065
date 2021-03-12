using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001144 RID: 4420
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceDisabledException : LocalizedException
	{
		// Token: 0x0600B538 RID: 46392 RVA: 0x0029DDA1 File Offset: 0x0029BFA1
		public ServiceDisabledException(string servicename) : base(Strings.ServiceDisabled(servicename))
		{
			this.servicename = servicename;
		}

		// Token: 0x0600B539 RID: 46393 RVA: 0x0029DDB6 File Offset: 0x0029BFB6
		public ServiceDisabledException(string servicename, Exception innerException) : base(Strings.ServiceDisabled(servicename), innerException)
		{
			this.servicename = servicename;
		}

		// Token: 0x0600B53A RID: 46394 RVA: 0x0029DDCC File Offset: 0x0029BFCC
		protected ServiceDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.servicename = (string)info.GetValue("servicename", typeof(string));
		}

		// Token: 0x0600B53B RID: 46395 RVA: 0x0029DDF6 File Offset: 0x0029BFF6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("servicename", this.servicename);
		}

		// Token: 0x17003945 RID: 14661
		// (get) Token: 0x0600B53C RID: 46396 RVA: 0x0029DE11 File Offset: 0x0029C011
		public string Servicename
		{
			get
			{
				return this.servicename;
			}
		}

		// Token: 0x040062AB RID: 25259
		private readonly string servicename;
	}
}
