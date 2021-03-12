using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E1B RID: 3611
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DomainControllerNotSpecifiedException : LocalizedException
	{
		// Token: 0x0600A58A RID: 42378 RVA: 0x00286332 File Offset: 0x00284532
		public DomainControllerNotSpecifiedException() : base(Strings.DomainControllerNotSpecifiedException)
		{
		}

		// Token: 0x0600A58B RID: 42379 RVA: 0x0028633F File Offset: 0x0028453F
		public DomainControllerNotSpecifiedException(Exception innerException) : base(Strings.DomainControllerNotSpecifiedException, innerException)
		{
		}

		// Token: 0x0600A58C RID: 42380 RVA: 0x0028634D File Offset: 0x0028454D
		protected DomainControllerNotSpecifiedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A58D RID: 42381 RVA: 0x00286357 File Offset: 0x00284557
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
