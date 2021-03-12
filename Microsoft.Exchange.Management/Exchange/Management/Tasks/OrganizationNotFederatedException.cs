using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020010D1 RID: 4305
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OrganizationNotFederatedException : LocalizedException
	{
		// Token: 0x0600B308 RID: 45832 RVA: 0x0029AA90 File Offset: 0x00298C90
		public OrganizationNotFederatedException() : base(Strings.OrganizationNotFederatedException)
		{
		}

		// Token: 0x0600B309 RID: 45833 RVA: 0x0029AA9D File Offset: 0x00298C9D
		public OrganizationNotFederatedException(Exception innerException) : base(Strings.OrganizationNotFederatedException, innerException)
		{
		}

		// Token: 0x0600B30A RID: 45834 RVA: 0x0029AAAB File Offset: 0x00298CAB
		protected OrganizationNotFederatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B30B RID: 45835 RVA: 0x0029AAB5 File Offset: 0x00298CB5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
