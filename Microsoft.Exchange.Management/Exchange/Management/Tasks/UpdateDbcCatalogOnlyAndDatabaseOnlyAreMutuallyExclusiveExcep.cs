using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001099 RID: 4249
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException : LocalizedException
	{
		// Token: 0x0600B1FE RID: 45566 RVA: 0x002993CE File Offset: 0x002975CE
		public UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException() : base(Strings.UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException)
		{
		}

		// Token: 0x0600B1FF RID: 45567 RVA: 0x002993DB File Offset: 0x002975DB
		public UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException(Exception innerException) : base(Strings.UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException, innerException)
		{
		}

		// Token: 0x0600B200 RID: 45568 RVA: 0x002993E9 File Offset: 0x002975E9
		protected UpdateDbcCatalogOnlyAndDatabaseOnlyAreMutuallyExclusiveException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B201 RID: 45569 RVA: 0x002993F3 File Offset: 0x002975F3
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
