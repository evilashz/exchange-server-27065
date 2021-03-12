using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.RpcClientAccess.Monitoring
{
	// Token: 0x02000084 RID: 132
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class WrongPropertyValueCombinationException : LocalizedException
	{
		// Token: 0x060002C6 RID: 710 RVA: 0x0000B7B4 File Offset: 0x000099B4
		public WrongPropertyValueCombinationException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000B7BD File Offset: 0x000099BD
		public WrongPropertyValueCombinationException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000B7C7 File Offset: 0x000099C7
		protected WrongPropertyValueCombinationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000B7D1 File Offset: 0x000099D1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
