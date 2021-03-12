using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B5 RID: 693
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceStopFailureException : LocalizedException
	{
		// Token: 0x060018FB RID: 6395 RVA: 0x0005C901 File Offset: 0x0005AB01
		public ServiceStopFailureException(string name, string msg) : base(Strings.ServiceStopFailure(name, msg))
		{
			this.name = name;
			this.msg = msg;
		}

		// Token: 0x060018FC RID: 6396 RVA: 0x0005C91E File Offset: 0x0005AB1E
		public ServiceStopFailureException(string name, string msg, Exception innerException) : base(Strings.ServiceStopFailure(name, msg), innerException)
		{
			this.name = name;
			this.msg = msg;
		}

		// Token: 0x060018FD RID: 6397 RVA: 0x0005C93C File Offset: 0x0005AB3C
		protected ServiceStopFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.msg = (string)info.GetValue("msg", typeof(string));
		}

		// Token: 0x060018FE RID: 6398 RVA: 0x0005C991 File Offset: 0x0005AB91
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("msg", this.msg);
		}

		// Token: 0x170004B9 RID: 1209
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0005C9BD File Offset: 0x0005ABBD
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170004BA RID: 1210
		// (get) Token: 0x06001900 RID: 6400 RVA: 0x0005C9C5 File Offset: 0x0005ABC5
		public string Msg
		{
			get
			{
				return this.msg;
			}
		}

		// Token: 0x0400098C RID: 2444
		private readonly string name;

		// Token: 0x0400098D RID: 2445
		private readonly string msg;
	}
}
