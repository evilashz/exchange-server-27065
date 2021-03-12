using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011F5 RID: 4597
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UCMAPreReqUpgradeException : LocalizedException
	{
		// Token: 0x0600B9A3 RID: 47523 RVA: 0x002A634E File Offset: 0x002A454E
		public UCMAPreReqUpgradeException() : base(Strings.UCMAPreReqUpgradeException)
		{
		}

		// Token: 0x0600B9A4 RID: 47524 RVA: 0x002A635B File Offset: 0x002A455B
		public UCMAPreReqUpgradeException(Exception innerException) : base(Strings.UCMAPreReqUpgradeException, innerException)
		{
		}

		// Token: 0x0600B9A5 RID: 47525 RVA: 0x002A6369 File Offset: 0x002A4569
		protected UCMAPreReqUpgradeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B9A6 RID: 47526 RVA: 0x002A6373 File Offset: 0x002A4573
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
