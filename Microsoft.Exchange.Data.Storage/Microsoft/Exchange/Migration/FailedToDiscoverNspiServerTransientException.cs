using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000189 RID: 393
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailedToDiscoverNspiServerTransientException : MigrationTransientException
	{
		// Token: 0x06001701 RID: 5889 RVA: 0x00070097 File Offset: 0x0006E297
		public FailedToDiscoverNspiServerTransientException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x06001702 RID: 5890 RVA: 0x000700A0 File Offset: 0x0006E2A0
		public FailedToDiscoverNspiServerTransientException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001703 RID: 5891 RVA: 0x000700AA File Offset: 0x0006E2AA
		protected FailedToDiscoverNspiServerTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001704 RID: 5892 RVA: 0x000700B4 File Offset: 0x0006E2B4
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
