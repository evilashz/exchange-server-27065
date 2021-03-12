using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200114C RID: 4428
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnlineIsIntegQueryJobException : LocalizedException
	{
		// Token: 0x0600B563 RID: 46435 RVA: 0x0029E25D File Offset: 0x0029C45D
		public OnlineIsIntegQueryJobException(string database, string failure) : base(Strings.OnlineIsIntegQueryJobException(database, failure))
		{
			this.database = database;
			this.failure = failure;
		}

		// Token: 0x0600B564 RID: 46436 RVA: 0x0029E27A File Offset: 0x0029C47A
		public OnlineIsIntegQueryJobException(string database, string failure, Exception innerException) : base(Strings.OnlineIsIntegQueryJobException(database, failure), innerException)
		{
			this.database = database;
			this.failure = failure;
		}

		// Token: 0x0600B565 RID: 46437 RVA: 0x0029E298 File Offset: 0x0029C498
		protected OnlineIsIntegQueryJobException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B566 RID: 46438 RVA: 0x0029E2ED File Offset: 0x0029C4ED
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x17003950 RID: 14672
		// (get) Token: 0x0600B567 RID: 46439 RVA: 0x0029E319 File Offset: 0x0029C519
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x17003951 RID: 14673
		// (get) Token: 0x0600B568 RID: 46440 RVA: 0x0029E321 File Offset: 0x0029C521
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040062B6 RID: 25270
		private readonly string database;

		// Token: 0x040062B7 RID: 25271
		private readonly string failure;
	}
}
