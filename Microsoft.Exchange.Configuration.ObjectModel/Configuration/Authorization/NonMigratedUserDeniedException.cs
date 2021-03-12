using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Configuration.Authorization
{
	// Token: 0x020002DC RID: 732
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class NonMigratedUserDeniedException : CmdletAccessDeniedException
	{
		// Token: 0x060019AD RID: 6573 RVA: 0x0005D630 File Offset: 0x0005B830
		public NonMigratedUserDeniedException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x0005D639 File Offset: 0x0005B839
		public NonMigratedUserDeniedException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x0005D643 File Offset: 0x0005B843
		protected NonMigratedUserDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x0005D64D File Offset: 0x0005B84D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
