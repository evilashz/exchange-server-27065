using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200119D RID: 4509
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MultipleOnPremisesOrganizationsFoundException : LocalizedException
	{
		// Token: 0x0600B704 RID: 46852 RVA: 0x002A0CD8 File Offset: 0x0029EED8
		public MultipleOnPremisesOrganizationsFoundException() : base(Strings.MultipleOnPremisesOrganizationsFoundException)
		{
		}

		// Token: 0x0600B705 RID: 46853 RVA: 0x002A0CE5 File Offset: 0x0029EEE5
		public MultipleOnPremisesOrganizationsFoundException(Exception innerException) : base(Strings.MultipleOnPremisesOrganizationsFoundException, innerException)
		{
		}

		// Token: 0x0600B706 RID: 46854 RVA: 0x002A0CF3 File Offset: 0x0029EEF3
		protected MultipleOnPremisesOrganizationsFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B707 RID: 46855 RVA: 0x002A0CFD File Offset: 0x0029EEFD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
