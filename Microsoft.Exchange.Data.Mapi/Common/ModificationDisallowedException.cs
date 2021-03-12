using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Mapi.Common
{
	// Token: 0x02000047 RID: 71
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ModificationDisallowedException : MapiOperationException
	{
		// Token: 0x0600028D RID: 653 RVA: 0x0000E322 File Offset: 0x0000C522
		public ModificationDisallowedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600028E RID: 654 RVA: 0x0000E32B File Offset: 0x0000C52B
		public ModificationDisallowedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600028F RID: 655 RVA: 0x0000E335 File Offset: 0x0000C535
		protected ModificationDisallowedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000E33F File Offset: 0x0000C53F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
