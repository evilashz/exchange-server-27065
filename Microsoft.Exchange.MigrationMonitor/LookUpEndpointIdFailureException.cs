using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Servicelets.MigrationMonitor
{
	// Token: 0x0200004C RID: 76
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LookUpEndpointIdFailureException : LocalizedException
	{
		// Token: 0x0600021A RID: 538 RVA: 0x00009504 File Offset: 0x00007704
		public LookUpEndpointIdFailureException() : base(MigrationMonitorStrings.ErrorLookUpEndpointId)
		{
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00009511 File Offset: 0x00007711
		public LookUpEndpointIdFailureException(Exception innerException) : base(MigrationMonitorStrings.ErrorLookUpEndpointId, innerException)
		{
		}

		// Token: 0x0600021C RID: 540 RVA: 0x0000951F File Offset: 0x0000771F
		protected LookUpEndpointIdFailureException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00009529 File Offset: 0x00007729
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
