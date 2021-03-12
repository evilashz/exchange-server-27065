using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A2 RID: 1442
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class DatabaseNotAttachedReadOnlyException : LocalizedException
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x000DDF12 File Offset: 0x000DC112
		public DatabaseNotAttachedReadOnlyException() : base(Strings.DatabaseNotAttachedReadOnly)
		{
		}

		// Token: 0x060026C5 RID: 9925 RVA: 0x000DDF1F File Offset: 0x000DC11F
		public DatabaseNotAttachedReadOnlyException(Exception innerException) : base(Strings.DatabaseNotAttachedReadOnly, innerException)
		{
		}

		// Token: 0x060026C6 RID: 9926 RVA: 0x000DDF2D File Offset: 0x000DC12D
		protected DatabaseNotAttachedReadOnlyException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026C7 RID: 9927 RVA: 0x000DDF37 File Offset: 0x000DC137
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
