using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x02001145 RID: 4421
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceDidNotReachStatusException : LocalizedException
	{
		// Token: 0x0600B53D RID: 46397 RVA: 0x0029DE19 File Offset: 0x0029C019
		public ServiceDidNotReachStatusException(string servicename, string status) : base(Strings.ServiceDidNotReachStatus(servicename, status))
		{
			this.servicename = servicename;
			this.status = status;
		}

		// Token: 0x0600B53E RID: 46398 RVA: 0x0029DE36 File Offset: 0x0029C036
		public ServiceDidNotReachStatusException(string servicename, string status, Exception innerException) : base(Strings.ServiceDidNotReachStatus(servicename, status), innerException)
		{
			this.servicename = servicename;
			this.status = status;
		}

		// Token: 0x0600B53F RID: 46399 RVA: 0x0029DE54 File Offset: 0x0029C054
		protected ServiceDidNotReachStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.servicename = (string)info.GetValue("servicename", typeof(string));
			this.status = (string)info.GetValue("status", typeof(string));
		}

		// Token: 0x0600B540 RID: 46400 RVA: 0x0029DEA9 File Offset: 0x0029C0A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("servicename", this.servicename);
			info.AddValue("status", this.status);
		}

		// Token: 0x17003946 RID: 14662
		// (get) Token: 0x0600B541 RID: 46401 RVA: 0x0029DED5 File Offset: 0x0029C0D5
		public string Servicename
		{
			get
			{
				return this.servicename;
			}
		}

		// Token: 0x17003947 RID: 14663
		// (get) Token: 0x0600B542 RID: 46402 RVA: 0x0029DEDD File Offset: 0x0029C0DD
		public string Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x040062AC RID: 25260
		private readonly string servicename;

		// Token: 0x040062AD RID: 25261
		private readonly string status;
	}
}
