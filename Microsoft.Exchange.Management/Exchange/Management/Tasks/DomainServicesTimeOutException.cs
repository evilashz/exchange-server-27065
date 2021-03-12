using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E4E RID: 3662
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainServicesTimeOutException : RecipientTaskException
	{
		// Token: 0x0600A67F RID: 42623 RVA: 0x00287819 File Offset: 0x00285A19
		public DomainServicesTimeOutException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A680 RID: 42624 RVA: 0x00287822 File Offset: 0x00285A22
		public DomainServicesTimeOutException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A681 RID: 42625 RVA: 0x0028782C File Offset: 0x00285A2C
		protected DomainServicesTimeOutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A682 RID: 42626 RVA: 0x00287836 File Offset: 0x00285A36
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
