using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001162 RID: 4450
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidQueryException : ReportingException
	{
		// Token: 0x0600B5D7 RID: 46551 RVA: 0x0029EEA9 File Offset: 0x0029D0A9
		public InvalidQueryException(int number) : base(Strings.InvalidQueryException(number))
		{
			this.number = number;
		}

		// Token: 0x0600B5D8 RID: 46552 RVA: 0x0029EEBE File Offset: 0x0029D0BE
		public InvalidQueryException(int number, Exception innerException) : base(Strings.InvalidQueryException(number), innerException)
		{
			this.number = number;
		}

		// Token: 0x0600B5D9 RID: 46553 RVA: 0x0029EED4 File Offset: 0x0029D0D4
		protected InvalidQueryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x0600B5DA RID: 46554 RVA: 0x0029EEFE File Offset: 0x0029D0FE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
		}

		// Token: 0x1700396C RID: 14700
		// (get) Token: 0x0600B5DB RID: 46555 RVA: 0x0029EF19 File Offset: 0x0029D119
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040062D2 RID: 25298
		private readonly int number;
	}
}
