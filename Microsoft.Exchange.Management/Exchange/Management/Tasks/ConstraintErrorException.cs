using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001097 RID: 4247
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConstraintErrorException : LocalizedException
	{
		// Token: 0x0600B1F3 RID: 45555 RVA: 0x00299282 File Offset: 0x00297482
		public ConstraintErrorException(DataMoveReplicationConstraintParameter desired, string database) : base(Strings.ConstraintError(desired, database))
		{
			this.desired = desired;
			this.database = database;
		}

		// Token: 0x0600B1F4 RID: 45556 RVA: 0x0029929F File Offset: 0x0029749F
		public ConstraintErrorException(DataMoveReplicationConstraintParameter desired, string database, Exception innerException) : base(Strings.ConstraintError(desired, database), innerException)
		{
			this.desired = desired;
			this.database = database;
		}

		// Token: 0x0600B1F5 RID: 45557 RVA: 0x002992C0 File Offset: 0x002974C0
		protected ConstraintErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.desired = (DataMoveReplicationConstraintParameter)info.GetValue("desired", typeof(DataMoveReplicationConstraintParameter));
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600B1F6 RID: 45558 RVA: 0x00299315 File Offset: 0x00297515
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("desired", this.desired);
			info.AddValue("database", this.database);
		}

		// Token: 0x170038B4 RID: 14516
		// (get) Token: 0x0600B1F7 RID: 45559 RVA: 0x00299346 File Offset: 0x00297546
		public DataMoveReplicationConstraintParameter Desired
		{
			get
			{
				return this.desired;
			}
		}

		// Token: 0x170038B5 RID: 14517
		// (get) Token: 0x0600B1F8 RID: 45560 RVA: 0x0029934E File Offset: 0x0029754E
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x0400621A RID: 25114
		private readonly DataMoveReplicationConstraintParameter desired;

		// Token: 0x0400621B RID: 25115
		private readonly string database;
	}
}
