using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x0200111B RID: 4379
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class TestMigrationServerAvailabilityProtocolArgumentException : LocalizedException
	{
		// Token: 0x0600B473 RID: 46195 RVA: 0x0029CC46 File Offset: 0x0029AE46
		public TestMigrationServerAvailabilityProtocolArgumentException() : base(Strings.TestMigrationServerAvailabilityProtocolArgument)
		{
		}

		// Token: 0x0600B474 RID: 46196 RVA: 0x0029CC53 File Offset: 0x0029AE53
		public TestMigrationServerAvailabilityProtocolArgumentException(Exception innerException) : base(Strings.TestMigrationServerAvailabilityProtocolArgument, innerException)
		{
		}

		// Token: 0x0600B475 RID: 46197 RVA: 0x0029CC61 File Offset: 0x0029AE61
		protected TestMigrationServerAvailabilityProtocolArgumentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B476 RID: 46198 RVA: 0x0029CC6B File Offset: 0x0029AE6B
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
