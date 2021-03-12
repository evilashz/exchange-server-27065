using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000A9B RID: 2715
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class EndpointContainerNotFoundException : MandatoryContainerNotFoundException
	{
		// Token: 0x06007FD4 RID: 32724 RVA: 0x001A484B File Offset: 0x001A2A4B
		public EndpointContainerNotFoundException(string endpointName) : base(DirectoryStrings.EndpointContainerNotFoundException(endpointName))
		{
			this.endpointName = endpointName;
		}

		// Token: 0x06007FD5 RID: 32725 RVA: 0x001A4860 File Offset: 0x001A2A60
		public EndpointContainerNotFoundException(string endpointName, Exception innerException) : base(DirectoryStrings.EndpointContainerNotFoundException(endpointName), innerException)
		{
			this.endpointName = endpointName;
		}

		// Token: 0x06007FD6 RID: 32726 RVA: 0x001A4876 File Offset: 0x001A2A76
		protected EndpointContainerNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.endpointName = (string)info.GetValue("endpointName", typeof(string));
		}

		// Token: 0x06007FD7 RID: 32727 RVA: 0x001A48A0 File Offset: 0x001A2AA0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("endpointName", this.endpointName);
		}

		// Token: 0x17002EBF RID: 11967
		// (get) Token: 0x06007FD8 RID: 32728 RVA: 0x001A48BB File Offset: 0x001A2ABB
		public string EndpointName
		{
			get
			{
				return this.endpointName;
			}
		}

		// Token: 0x04005599 RID: 21913
		private readonly string endpointName;
	}
}
