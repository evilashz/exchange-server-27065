using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200104A RID: 4170
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DagFswUnableToDetermineFrontendTransportServerException : LocalizedException
	{
		// Token: 0x0600B030 RID: 45104 RVA: 0x00295931 File Offset: 0x00293B31
		public DagFswUnableToDetermineFrontendTransportServerException() : base(Strings.DagFswUnableToDetermineFrontendTransportServerException)
		{
		}

		// Token: 0x0600B031 RID: 45105 RVA: 0x0029593E File Offset: 0x00293B3E
		public DagFswUnableToDetermineFrontendTransportServerException(Exception innerException) : base(Strings.DagFswUnableToDetermineFrontendTransportServerException, innerException)
		{
		}

		// Token: 0x0600B032 RID: 45106 RVA: 0x0029594C File Offset: 0x00293B4C
		protected DagFswUnableToDetermineFrontendTransportServerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B033 RID: 45107 RVA: 0x00295956 File Offset: 0x00293B56
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
