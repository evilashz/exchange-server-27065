using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E5E RID: 3678
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class WLCDManagedMemberNotExistsException : WLCDMemberException
	{
		// Token: 0x0600A6C6 RID: 42694 RVA: 0x00287CC7 File Offset: 0x00285EC7
		public WLCDManagedMemberNotExistsException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A6C7 RID: 42695 RVA: 0x00287CD0 File Offset: 0x00285ED0
		public WLCDManagedMemberNotExistsException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A6C8 RID: 42696 RVA: 0x00287CDA File Offset: 0x00285EDA
		protected WLCDManagedMemberNotExistsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A6C9 RID: 42697 RVA: 0x00287CE4 File Offset: 0x00285EE4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
