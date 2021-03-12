using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200114D RID: 4429
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnlineIsIntegRemoveJobException : LocalizedException
	{
		// Token: 0x0600B569 RID: 46441 RVA: 0x0029E329 File Offset: 0x0029C529
		public OnlineIsIntegRemoveJobException(string database, string job, string failure) : base(Strings.OnlineIsIntegRemoveJobException(database, job, failure))
		{
			this.database = database;
			this.job = job;
			this.failure = failure;
		}

		// Token: 0x0600B56A RID: 46442 RVA: 0x0029E34E File Offset: 0x0029C54E
		public OnlineIsIntegRemoveJobException(string database, string job, string failure, Exception innerException) : base(Strings.OnlineIsIntegRemoveJobException(database, job, failure), innerException)
		{
			this.database = database;
			this.job = job;
			this.failure = failure;
		}

		// Token: 0x0600B56B RID: 46443 RVA: 0x0029E378 File Offset: 0x0029C578
		protected OnlineIsIntegRemoveJobException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.job = (string)info.GetValue("job", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B56C RID: 46444 RVA: 0x0029E3ED File Offset: 0x0029C5ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("job", this.job);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003952 RID: 14674
		// (get) Token: 0x0600B56D RID: 46445 RVA: 0x0029E42A File Offset: 0x0029C62A
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17003953 RID: 14675
		// (get) Token: 0x0600B56E RID: 46446 RVA: 0x0029E432 File Offset: 0x0029C632
		public string Job
		{
			get
			{
				return this.job;
			}
		}

		// Token: 0x17003954 RID: 14676
		// (get) Token: 0x0600B56F RID: 46447 RVA: 0x0029E43A File Offset: 0x0029C63A
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040062B8 RID: 25272
		private readonly string database;

		// Token: 0x040062B9 RID: 25273
		private readonly string job;

		// Token: 0x040062BA RID: 25274
		private readonly string failure;
	}
}
