using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02001107 RID: 4359
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class DataCorruptionException : LocalizedException
	{
		// Token: 0x0600B413 RID: 46099 RVA: 0x0029C3ED File Offset: 0x0029A5ED
		public DataCorruptionException(string dataSource, string violation) : base(Strings.messageDataCorruptionException(dataSource, violation))
		{
			this.dataSource = dataSource;
			this.violation = violation;
		}

		// Token: 0x0600B414 RID: 46100 RVA: 0x0029C40A File Offset: 0x0029A60A
		public DataCorruptionException(string dataSource, string violation, Exception innerException) : base(Strings.messageDataCorruptionException(dataSource, violation), innerException)
		{
			this.dataSource = dataSource;
			this.violation = violation;
		}

		// Token: 0x0600B415 RID: 46101 RVA: 0x0029C428 File Offset: 0x0029A628
		protected DataCorruptionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.dataSource = (string)info.GetValue("dataSource", typeof(string));
			this.violation = (string)info.GetValue("violation", typeof(string));
		}

		// Token: 0x0600B416 RID: 46102 RVA: 0x0029C47D File Offset: 0x0029A67D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("dataSource", this.dataSource);
			info.AddValue("violation", this.violation);
		}

		// Token: 0x17003914 RID: 14612
		// (get) Token: 0x0600B417 RID: 46103 RVA: 0x0029C4A9 File Offset: 0x0029A6A9
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17003915 RID: 14613
		// (get) Token: 0x0600B418 RID: 46104 RVA: 0x0029C4B1 File Offset: 0x0029A6B1
		public string Violation
		{
			get
			{
				return this.violation;
			}
		}

		// Token: 0x0400627A RID: 25210
		private readonly string dataSource;

		// Token: 0x0400627B RID: 25211
		private readonly string violation;
	}
}
