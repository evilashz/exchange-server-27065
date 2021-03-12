using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000181 RID: 385
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SyncTimeoutException : MigrationPermanentException
	{
		// Token: 0x060016DE RID: 5854 RVA: 0x0006FE44 File Offset: 0x0006E044
		public SyncTimeoutException(string timespan) : base(Strings.SyncTimeOutFailure(timespan))
		{
			this.timespan = timespan;
		}

		// Token: 0x060016DF RID: 5855 RVA: 0x0006FE59 File Offset: 0x0006E059
		public SyncTimeoutException(string timespan, Exception innerException) : base(Strings.SyncTimeOutFailure(timespan), innerException)
		{
			this.timespan = timespan;
		}

		// Token: 0x060016E0 RID: 5856 RVA: 0x0006FE6F File Offset: 0x0006E06F
		protected SyncTimeoutException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.timespan = (string)info.GetValue("timespan", typeof(string));
		}

		// Token: 0x060016E1 RID: 5857 RVA: 0x0006FE99 File Offset: 0x0006E099
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("timespan", this.timespan);
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0006FEB4 File Offset: 0x0006E0B4
		public string Timespan
		{
			get
			{
				return this.timespan;
			}
		}

		// Token: 0x04000B02 RID: 2818
		private readonly string timespan;
	}
}
