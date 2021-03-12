using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A80 RID: 2688
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainControllerFromWrongDomainException : ADOperationException
	{
		// Token: 0x06007F5D RID: 32605 RVA: 0x001A4075 File Offset: 0x001A2275
		public DomainControllerFromWrongDomainException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F5E RID: 32606 RVA: 0x001A407E File Offset: 0x001A227E
		public DomainControllerFromWrongDomainException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F5F RID: 32607 RVA: 0x001A4088 File Offset: 0x001A2288
		protected DomainControllerFromWrongDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F60 RID: 32608 RVA: 0x001A4092 File Offset: 0x001A2292
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
