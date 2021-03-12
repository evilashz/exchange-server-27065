using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Configuration.Common.LocStrings;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Tasks
{
	// Token: 0x020002B3 RID: 691
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceNotInstalledException : LocalizedException
	{
		// Token: 0x060018F0 RID: 6384 RVA: 0x0005C7BA File Offset: 0x0005A9BA
		public ServiceNotInstalledException(string servicename) : base(Strings.ServiceNotInstalled(servicename))
		{
			this.servicename = servicename;
		}

		// Token: 0x060018F1 RID: 6385 RVA: 0x0005C7CF File Offset: 0x0005A9CF
		public ServiceNotInstalledException(string servicename, Exception innerException) : base(Strings.ServiceNotInstalled(servicename), innerException)
		{
			this.servicename = servicename;
		}

		// Token: 0x060018F2 RID: 6386 RVA: 0x0005C7E5 File Offset: 0x0005A9E5
		protected ServiceNotInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.servicename = (string)info.GetValue("servicename", typeof(string));
		}

		// Token: 0x060018F3 RID: 6387 RVA: 0x0005C80F File Offset: 0x0005AA0F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("servicename", this.servicename);
		}

		// Token: 0x170004B6 RID: 1206
		// (get) Token: 0x060018F4 RID: 6388 RVA: 0x0005C82A File Offset: 0x0005AA2A
		public string Servicename
		{
			get
			{
				return this.servicename;
			}
		}

		// Token: 0x04000989 RID: 2441
		private readonly string servicename;
	}
}
