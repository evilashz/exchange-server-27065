using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Data.Directory.Sync
{
	// Token: 0x02000AB6 RID: 2742
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceInstanceContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x0600804E RID: 32846 RVA: 0x001A5135 File Offset: 0x001A3335
		public ServiceInstanceContainerNotFoundException(string serviceInstance) : base(DirectoryStrings.ServiceInstanceContainerNotFoundException(serviceInstance))
		{
			this.serviceInstance = serviceInstance;
		}

		// Token: 0x0600804F RID: 32847 RVA: 0x001A514A File Offset: 0x001A334A
		public ServiceInstanceContainerNotFoundException(string serviceInstance, Exception innerException) : base(DirectoryStrings.ServiceInstanceContainerNotFoundException(serviceInstance), innerException)
		{
			this.serviceInstance = serviceInstance;
		}

		// Token: 0x06008050 RID: 32848 RVA: 0x001A5160 File Offset: 0x001A3360
		protected ServiceInstanceContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceInstance = (string)info.GetValue("serviceInstance", typeof(string));
		}

		// Token: 0x06008051 RID: 32849 RVA: 0x001A518A File Offset: 0x001A338A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceInstance", this.serviceInstance);
		}

		// Token: 0x17002ECD RID: 11981
		// (get) Token: 0x06008052 RID: 32850 RVA: 0x001A51A5 File Offset: 0x001A33A5
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstance;
			}
		}

		// Token: 0x040055A7 RID: 21927
		private readonly string serviceInstance;
	}
}
