using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F9 RID: 4601
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TUC_NoDTMFSwereReceived : LocalizedException
	{
		// Token: 0x0600B9B6 RID: 47542 RVA: 0x002A64F0 File Offset: 0x002A46F0
		public TUC_NoDTMFSwereReceived() : base(Strings.NoDTMFSwereReceived)
		{
		}

		// Token: 0x0600B9B7 RID: 47543 RVA: 0x002A64FD File Offset: 0x002A46FD
		public TUC_NoDTMFSwereReceived(Exception innerException) : base(Strings.NoDTMFSwereReceived, innerException)
		{
		}

		// Token: 0x0600B9B8 RID: 47544 RVA: 0x002A650B File Offset: 0x002A470B
		protected TUC_NoDTMFSwereReceived(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9B9 RID: 47545 RVA: 0x002A6515 File Offset: 0x002A4715
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
