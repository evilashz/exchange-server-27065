using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016A RID: 362
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class FailureToRemoveTransientException : MigrationTransientException
	{
		// Token: 0x06001679 RID: 5753 RVA: 0x0006F762 File Offset: 0x0006D962
		public FailureToRemoveTransientException() : base(Strings.FailureDuringRemoval)
		{
		}

		// Token: 0x0600167A RID: 5754 RVA: 0x0006F76F File Offset: 0x0006D96F
		public FailureToRemoveTransientException(Exception innerException) : base(Strings.FailureDuringRemoval, innerException)
		{
		}

		// Token: 0x0600167B RID: 5755 RVA: 0x0006F77D File Offset: 0x0006D97D
		protected FailureToRemoveTransientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600167C RID: 5756 RVA: 0x0006F787 File Offset: 0x0006D987
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
