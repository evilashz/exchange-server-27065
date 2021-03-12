using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.BackSync
{
	// Token: 0x0200115A RID: 4442
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotResolvePartitionFromInstanceIdException : LocalizedException
	{
		// Token: 0x0600B5AD RID: 46509 RVA: 0x0029EA3A File Offset: 0x0029CC3A
		public CannotResolvePartitionFromInstanceIdException(string serviceInstance) : base(Strings.CannotResolvePartitionFromInstanceId(serviceInstance))
		{
			this.serviceInstance = serviceInstance;
		}

		// Token: 0x0600B5AE RID: 46510 RVA: 0x0029EA4F File Offset: 0x0029CC4F
		public CannotResolvePartitionFromInstanceIdException(string serviceInstance, Exception innerException) : base(Strings.CannotResolvePartitionFromInstanceId(serviceInstance), innerException)
		{
			this.serviceInstance = serviceInstance;
		}

		// Token: 0x0600B5AF RID: 46511 RVA: 0x0029EA65 File Offset: 0x0029CC65
		protected CannotResolvePartitionFromInstanceIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceInstance = (string)info.GetValue("serviceInstance", typeof(string));
		}

		// Token: 0x0600B5B0 RID: 46512 RVA: 0x0029EA8F File Offset: 0x0029CC8F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceInstance", this.serviceInstance);
		}

		// Token: 0x17003962 RID: 14690
		// (get) Token: 0x0600B5B1 RID: 46513 RVA: 0x0029EAAA File Offset: 0x0029CCAA
		public string ServiceInstance
		{
			get
			{
				return this.serviceInstance;
			}
		}

		// Token: 0x040062C8 RID: 25288
		private readonly string serviceInstance;
	}
}
