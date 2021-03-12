using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000D5 RID: 213
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotCloneException : LocalizedException
	{
		// Token: 0x060007C0 RID: 1984 RVA: 0x0001A642 File Offset: 0x00018842
		public CannotCloneException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060007C1 RID: 1985 RVA: 0x0001A64B File Offset: 0x0001884B
		public CannotCloneException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060007C2 RID: 1986 RVA: 0x0001A655 File Offset: 0x00018855
		protected CannotCloneException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007C3 RID: 1987 RVA: 0x0001A65F File Offset: 0x0001885F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
