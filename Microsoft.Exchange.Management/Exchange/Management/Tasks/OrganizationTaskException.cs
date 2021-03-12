using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E3A RID: 3642
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationTaskException : LocalizedException
	{
		// Token: 0x0600A62F RID: 42543 RVA: 0x0028750D File Offset: 0x0028570D
		public OrganizationTaskException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A630 RID: 42544 RVA: 0x00287516 File Offset: 0x00285716
		public OrganizationTaskException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A631 RID: 42545 RVA: 0x00287520 File Offset: 0x00285720
		protected OrganizationTaskException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A632 RID: 42546 RVA: 0x0028752A File Offset: 0x0028572A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
