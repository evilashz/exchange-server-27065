using System;
using System.Runtime.Serialization;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Data.Storage.ActiveManager;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000748 RID: 1864
	[Serializable]
	public class MailboxCrossSiteFailoverException : ConnectionFailedPermanentException
	{
		// Token: 0x06004821 RID: 18465 RVA: 0x001309E8 File Offset: 0x0012EBE8
		public MailboxCrossSiteFailoverException(LocalizedString message, DatabaseLocationInfo dbLocationInfo) : base(message)
		{
			this.dbLocationInfo = dbLocationInfo;
		}

		// Token: 0x06004822 RID: 18466 RVA: 0x001309F8 File Offset: 0x0012EBF8
		public MailboxCrossSiteFailoverException(LocalizedString message, Exception innerException, DatabaseLocationInfo dbLocationInfo) : base(message, innerException)
		{
			this.dbLocationInfo = dbLocationInfo;
		}

		// Token: 0x06004823 RID: 18467 RVA: 0x00130A09 File Offset: 0x0012EC09
		protected MailboxCrossSiteFailoverException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dbLocationInfo = (DatabaseLocationInfo)info.GetValue("dbLocationInfo", typeof(DatabaseLocationInfo));
		}

		// Token: 0x06004824 RID: 18468 RVA: 0x00130A33 File Offset: 0x0012EC33
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dbLocationInfo", this.dbLocationInfo);
		}

		// Token: 0x170014E1 RID: 5345
		// (get) Token: 0x06004825 RID: 18469 RVA: 0x00130A4E File Offset: 0x0012EC4E
		public DatabaseLocationInfo DatabaseLocationInfo
		{
			get
			{
				return this.dbLocationInfo;
			}
		}

		// Token: 0x0400273E RID: 10046
		private const string DbLocationInfoLabel = "dbLocationInfo";

		// Token: 0x0400273F RID: 10047
		private DatabaseLocationInfo dbLocationInfo;
	}
}
