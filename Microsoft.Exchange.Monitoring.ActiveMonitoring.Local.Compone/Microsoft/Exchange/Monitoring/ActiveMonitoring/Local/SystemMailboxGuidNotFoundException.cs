using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Local
{
	// Token: 0x020005A0 RID: 1440
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SystemMailboxGuidNotFoundException : LocalizedException
	{
		// Token: 0x060026BB RID: 9915 RVA: 0x000DDE6B File Offset: 0x000DC06B
		public SystemMailboxGuidNotFoundException() : base(Strings.SystemMailboxGuidNotFound)
		{
		}

		// Token: 0x060026BC RID: 9916 RVA: 0x000DDE78 File Offset: 0x000DC078
		public SystemMailboxGuidNotFoundException(Exception innerException) : base(Strings.SystemMailboxGuidNotFound, innerException)
		{
		}

		// Token: 0x060026BD RID: 9917 RVA: 0x000DDE86 File Offset: 0x000DC086
		protected SystemMailboxGuidNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060026BE RID: 9918 RVA: 0x000DDE90 File Offset: 0x000DC090
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
