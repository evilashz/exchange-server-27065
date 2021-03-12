using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200114B RID: 4427
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class OnlineIsIntegException : LocalizedException
	{
		// Token: 0x0600B55D RID: 46429 RVA: 0x0029E191 File Offset: 0x0029C391
		public OnlineIsIntegException(string database, string failure) : base(Strings.OnlineIsIntegException(database, failure))
		{
			this.database = database;
			this.failure = failure;
		}

		// Token: 0x0600B55E RID: 46430 RVA: 0x0029E1AE File Offset: 0x0029C3AE
		public OnlineIsIntegException(string database, string failure, Exception innerException) : base(Strings.OnlineIsIntegException(database, failure), innerException)
		{
			this.database = database;
			this.failure = failure;
		}

		// Token: 0x0600B55F RID: 46431 RVA: 0x0029E1CC File Offset: 0x0029C3CC
		protected OnlineIsIntegException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
			this.failure = (string)info.GetValue("failure", typeof(string));
		}

		// Token: 0x0600B560 RID: 46432 RVA: 0x0029E221 File Offset: 0x0029C421
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
			info.AddValue("failure", this.failure);
		}

		// Token: 0x1700394E RID: 14670
		// (get) Token: 0x0600B561 RID: 46433 RVA: 0x0029E24D File Offset: 0x0029C44D
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x1700394F RID: 14671
		// (get) Token: 0x0600B562 RID: 46434 RVA: 0x0029E255 File Offset: 0x0029C455
		public string Failure
		{
			get
			{
				return this.failure;
			}
		}

		// Token: 0x040062B4 RID: 25268
		private readonly string database;

		// Token: 0x040062B5 RID: 25269
		private readonly string failure;
	}
}
