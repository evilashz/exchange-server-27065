using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EE0 RID: 3808
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CrossOrgMigrationCleanupException : LocalizedException
	{
		// Token: 0x0600A940 RID: 43328 RVA: 0x0028B5E1 File Offset: 0x002897E1
		public CrossOrgMigrationCleanupException(LocalizedString message) : base(message)
		{
		}

		// Token: 0x0600A941 RID: 43329 RVA: 0x0028B5EA File Offset: 0x002897EA
		public CrossOrgMigrationCleanupException(LocalizedString message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600A942 RID: 43330 RVA: 0x0028B5F4 File Offset: 0x002897F4
		protected CrossOrgMigrationCleanupException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A943 RID: 43331 RVA: 0x0028B5FE File Offset: 0x002897FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
