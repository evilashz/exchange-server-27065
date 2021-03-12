using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000112 RID: 274
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationInvalidStatusException : MigrationTransientException
	{
		// Token: 0x060013E9 RID: 5097 RVA: 0x00069F36 File Offset: 0x00068136
		public MigrationInvalidStatusException(string statusType, string status) : base(ServerStrings.MigrationInvalidStatus(statusType, status))
		{
			this.statusType = statusType;
			this.status = status;
		}

		// Token: 0x060013EA RID: 5098 RVA: 0x00069F53 File Offset: 0x00068153
		public MigrationInvalidStatusException(string statusType, string status, Exception innerException) : base(ServerStrings.MigrationInvalidStatus(statusType, status), innerException)
		{
			this.statusType = statusType;
			this.status = status;
		}

		// Token: 0x060013EB RID: 5099 RVA: 0x00069F74 File Offset: 0x00068174
		protected MigrationInvalidStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.statusType = (string)info.GetValue("statusType", typeof(string));
			this.status = (string)info.GetValue("status", typeof(string));
		}

		// Token: 0x060013EC RID: 5100 RVA: 0x00069FC9 File Offset: 0x000681C9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("statusType", this.statusType);
			info.AddValue("status", this.status);
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x060013ED RID: 5101 RVA: 0x00069FF5 File Offset: 0x000681F5
		public string StatusType
		{
			get
			{
				return this.statusType;
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x060013EE RID: 5102 RVA: 0x00069FFD File Offset: 0x000681FD
		public string Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x040009A0 RID: 2464
		private readonly string statusType;

		// Token: 0x040009A1 RID: 2465
		private readonly string status;
	}
}
