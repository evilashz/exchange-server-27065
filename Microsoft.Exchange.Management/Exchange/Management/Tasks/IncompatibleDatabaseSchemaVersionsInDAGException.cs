using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000F2D RID: 3885
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IncompatibleDatabaseSchemaVersionsInDAGException : LocalizedException
	{
		// Token: 0x0600AAD7 RID: 43735 RVA: 0x0028E1C1 File Offset: 0x0028C3C1
		public IncompatibleDatabaseSchemaVersionsInDAGException() : base(Strings.IncompatibleDatabaseSchemaVersionsInDAG)
		{
		}

		// Token: 0x0600AAD8 RID: 43736 RVA: 0x0028E1CE File Offset: 0x0028C3CE
		public IncompatibleDatabaseSchemaVersionsInDAGException(Exception innerException) : base(Strings.IncompatibleDatabaseSchemaVersionsInDAG, innerException)
		{
		}

		// Token: 0x0600AAD9 RID: 43737 RVA: 0x0028E1DC File Offset: 0x0028C3DC
		protected IncompatibleDatabaseSchemaVersionsInDAGException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600AADA RID: 43738 RVA: 0x0028E1E6 File Offset: 0x0028C3E6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
