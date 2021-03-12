using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000A87 RID: 2695
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotGetComputerNameException : ADExternalException
	{
		// Token: 0x06007F79 RID: 32633 RVA: 0x001A419E File Offset: 0x001A239E
		public CannotGetComputerNameException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06007F7A RID: 32634 RVA: 0x001A41A7 File Offset: 0x001A23A7
		public CannotGetComputerNameException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06007F7B RID: 32635 RVA: 0x001A41B1 File Offset: 0x001A23B1
		protected CannotGetComputerNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06007F7C RID: 32636 RVA: 0x001A41BB File Offset: 0x001A23BB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
