using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Transport
{
	// Token: 0x02000170 RID: 368
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class InvalidHoldContentActionException : InvalidComplianceRuleActionException
	{
		// Token: 0x06000F2C RID: 3884 RVA: 0x00035EB0 File Offset: 0x000340B0
		public InvalidHoldContentActionException() : base(Strings.InvalidHoldContentAction)
		{
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x00035EBD File Offset: 0x000340BD
		public InvalidHoldContentActionException(Exception innerException) : base(Strings.InvalidHoldContentAction, innerException)
		{
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x00035ECB File Offset: 0x000340CB
		protected InvalidHoldContentActionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x00035ED5 File Offset: 0x000340D5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
