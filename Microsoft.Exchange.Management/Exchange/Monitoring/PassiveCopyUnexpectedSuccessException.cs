using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x0200116F RID: 4463
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class PassiveCopyUnexpectedSuccessException : LocalizedException
	{
		// Token: 0x0600B616 RID: 46614 RVA: 0x0029F435 File Offset: 0x0029D635
		public PassiveCopyUnexpectedSuccessException(string database) : base(Strings.messagePassiveCopyUnexpectedSuccessException(database))
		{
			this.database = database;
		}

		// Token: 0x0600B617 RID: 46615 RVA: 0x0029F44A File Offset: 0x0029D64A
		public PassiveCopyUnexpectedSuccessException(string database, Exception innerException) : base(Strings.messagePassiveCopyUnexpectedSuccessException(database), innerException)
		{
			this.database = database;
		}

		// Token: 0x0600B618 RID: 46616 RVA: 0x0029F460 File Offset: 0x0029D660
		protected PassiveCopyUnexpectedSuccessException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.database = (string)info.GetValue("database", typeof(string));
		}

		// Token: 0x0600B619 RID: 46617 RVA: 0x0029F48A File Offset: 0x0029D68A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("database", this.database);
		}

		// Token: 0x17003977 RID: 14711
		// (get) Token: 0x0600B61A RID: 46618 RVA: 0x0029F4A5 File Offset: 0x0029D6A5
		public string Database
		{
			get
			{
				return this.database;
			}
		}

		// Token: 0x040062DD RID: 25309
		private readonly string database;
	}
}
