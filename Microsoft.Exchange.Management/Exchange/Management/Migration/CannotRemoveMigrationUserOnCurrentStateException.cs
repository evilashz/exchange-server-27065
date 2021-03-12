using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.Migration
{
	// Token: 0x02001128 RID: 4392
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotRemoveMigrationUserOnCurrentStateException : LocalizedException
	{
		// Token: 0x0600B4B8 RID: 46264 RVA: 0x0029D3E5 File Offset: 0x0029B5E5
		public CannotRemoveMigrationUserOnCurrentStateException(string user, string batchName) : base(Strings.ErrorCannotRemoveMigrationUserOnCurrentState(user, batchName))
		{
			this.user = user;
			this.batchName = batchName;
		}

		// Token: 0x0600B4B9 RID: 46265 RVA: 0x0029D402 File Offset: 0x0029B602
		public CannotRemoveMigrationUserOnCurrentStateException(string user, string batchName, Exception innerException) : base(Strings.ErrorCannotRemoveMigrationUserOnCurrentState(user, batchName), innerException)
		{
			this.user = user;
			this.batchName = batchName;
		}

		// Token: 0x0600B4BA RID: 46266 RVA: 0x0029D420 File Offset: 0x0029B620
		protected CannotRemoveMigrationUserOnCurrentStateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.batchName = (string)info.GetValue("batchName", typeof(string));
		}

		// Token: 0x0600B4BB RID: 46267 RVA: 0x0029D475 File Offset: 0x0029B675
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("batchName", this.batchName);
		}

		// Token: 0x17003935 RID: 14645
		// (get) Token: 0x0600B4BC RID: 46268 RVA: 0x0029D4A1 File Offset: 0x0029B6A1
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003936 RID: 14646
		// (get) Token: 0x0600B4BD RID: 46269 RVA: 0x0029D4A9 File Offset: 0x0029B6A9
		public string BatchName
		{
			get
			{
				return this.batchName;
			}
		}

		// Token: 0x0400629B RID: 25243
		private readonly string user;

		// Token: 0x0400629C RID: 25244
		private readonly string batchName;
	}
}
