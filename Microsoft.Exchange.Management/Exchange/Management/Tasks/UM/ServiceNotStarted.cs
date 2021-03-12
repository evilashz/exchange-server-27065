using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D4 RID: 4564
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceNotStarted : LocalizedException
	{
		// Token: 0x0600B901 RID: 47361 RVA: 0x002A5454 File Offset: 0x002A3654
		public ServiceNotStarted(string serviceName) : base(Strings.ServiceNotStarted(serviceName))
		{
			this.serviceName = serviceName;
		}

		// Token: 0x0600B902 RID: 47362 RVA: 0x002A5469 File Offset: 0x002A3669
		public ServiceNotStarted(string serviceName, Exception innerException) : base(Strings.ServiceNotStarted(serviceName), innerException)
		{
			this.serviceName = serviceName;
		}

		// Token: 0x0600B903 RID: 47363 RVA: 0x002A547F File Offset: 0x002A367F
		protected ServiceNotStarted(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceName = (string)info.GetValue("serviceName", typeof(string));
		}

		// Token: 0x0600B904 RID: 47364 RVA: 0x002A54A9 File Offset: 0x002A36A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceName", this.serviceName);
		}

		// Token: 0x17003A2E RID: 14894
		// (get) Token: 0x0600B905 RID: 47365 RVA: 0x002A54C4 File Offset: 0x002A36C4
		public string ServiceName
		{
			get
			{
				return this.serviceName;
			}
		}

		// Token: 0x04006449 RID: 25673
		private readonly string serviceName;
	}
}
