using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Search.Core.Abstraction;

namespace Microsoft.Exchange.Search.MailboxOperators
{
	// Token: 0x0200002B RID: 43
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class OrgIdNotFoundException : ComponentFailedTransientException
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x0000A513 File Offset: 0x00008713
		public OrgIdNotFoundException() : base(Strings.OrgIdNotFound)
		{
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0000A520 File Offset: 0x00008720
		public OrgIdNotFoundException(Exception innerException) : base(Strings.OrgIdNotFound, innerException)
		{
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x0000A52E File Offset: 0x0000872E
		protected OrgIdNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x0000A538 File Offset: 0x00008738
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
