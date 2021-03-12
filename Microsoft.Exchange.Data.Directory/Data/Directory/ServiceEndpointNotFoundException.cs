using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AE1 RID: 2785
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ServiceEndpointNotFoundException : ADOperationException
	{
		// Token: 0x0600811C RID: 33052 RVA: 0x001A62FA File Offset: 0x001A44FA
		public ServiceEndpointNotFoundException(string serviceEndpointName) : base(DirectoryStrings.ErrorServiceEndpointNotFound(serviceEndpointName))
		{
			this.serviceEndpointName = serviceEndpointName;
		}

		// Token: 0x0600811D RID: 33053 RVA: 0x001A630F File Offset: 0x001A450F
		public ServiceEndpointNotFoundException(string serviceEndpointName, Exception innerException) : base(DirectoryStrings.ErrorServiceEndpointNotFound(serviceEndpointName), innerException)
		{
			this.serviceEndpointName = serviceEndpointName;
		}

		// Token: 0x0600811E RID: 33054 RVA: 0x001A6325 File Offset: 0x001A4525
		protected ServiceEndpointNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.serviceEndpointName = (string)info.GetValue("serviceEndpointName", typeof(string));
		}

		// Token: 0x0600811F RID: 33055 RVA: 0x001A634F File Offset: 0x001A454F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("serviceEndpointName", this.serviceEndpointName);
		}

		// Token: 0x17002EEF RID: 12015
		// (get) Token: 0x06008120 RID: 33056 RVA: 0x001A636A File Offset: 0x001A456A
		public string ServiceEndpointName
		{
			get
			{
				return this.serviceEndpointName;
			}
		}

		// Token: 0x040055C9 RID: 21961
		private readonly string serviceEndpointName;
	}
}
