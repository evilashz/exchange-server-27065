using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AB5 RID: 2741
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidServiceInstanceIdException : LocalizedException
	{
		// Token: 0x06008049 RID: 32841 RVA: 0x001A50BD File Offset: 0x001A32BD
		public InvalidServiceInstanceIdException(string serviceInstanceId) : base(DirectoryStrings.InvalidServiceInstanceIdException(serviceInstanceId))
		{
			this.serviceInstanceId = serviceInstanceId;
		}

		// Token: 0x0600804A RID: 32842 RVA: 0x001A50D2 File Offset: 0x001A32D2
		public InvalidServiceInstanceIdException(string serviceInstanceId, Exception innerException) : base(DirectoryStrings.InvalidServiceInstanceIdException(serviceInstanceId), innerException)
		{
			this.serviceInstanceId = serviceInstanceId;
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x001A50E8 File Offset: 0x001A32E8
		protected InvalidServiceInstanceIdException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceInstanceId = (string)info.GetValue("serviceInstanceId", typeof(string));
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x001A5112 File Offset: 0x001A3312
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceInstanceId", this.serviceInstanceId);
		}

		// Token: 0x17002ECC RID: 11980
		// (get) Token: 0x0600804D RID: 32845 RVA: 0x001A512D File Offset: 0x001A332D
		public string ServiceInstanceId
		{
			get
			{
				return this.serviceInstanceId;
			}
		}

		// Token: 0x040055A6 RID: 21926
		private readonly string serviceInstanceId;
	}
}
