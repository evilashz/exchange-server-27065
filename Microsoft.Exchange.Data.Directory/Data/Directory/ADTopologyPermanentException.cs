using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000ABF RID: 2751
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ADTopologyPermanentException : PermanentGlobalException
	{
		// Token: 0x06008076 RID: 32886 RVA: 0x001A53F0 File Offset: 0x001A35F0
		public ADTopologyPermanentException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06008077 RID: 32887 RVA: 0x001A53F9 File Offset: 0x001A35F9
		public ADTopologyPermanentException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06008078 RID: 32888 RVA: 0x001A5403 File Offset: 0x001A3603
		protected ADTopologyPermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008079 RID: 32889 RVA: 0x001A540D File Offset: 0x001A360D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
