using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F4 RID: 4596
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UCMAPreReqException : LocalizedException
	{
		// Token: 0x0600B99F RID: 47519 RVA: 0x002A631F File Offset: 0x002A451F
		public UCMAPreReqException() : base(Strings.UCMAPreReqException)
		{
		}

		// Token: 0x0600B9A0 RID: 47520 RVA: 0x002A632C File Offset: 0x002A452C
		public UCMAPreReqException(Exception innerException) : base(Strings.UCMAPreReqException, innerException)
		{
		}

		// Token: 0x0600B9A1 RID: 47521 RVA: 0x002A633A File Offset: 0x002A453A
		protected UCMAPreReqException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9A2 RID: 47522 RVA: 0x002A6344 File Offset: 0x002A4544
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
