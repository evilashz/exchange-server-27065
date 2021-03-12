using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x02001160 RID: 4448
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDataSourceException : ReportingException
	{
		// Token: 0x0600B5CD RID: 46541 RVA: 0x0029EDB9 File Offset: 0x0029CFB9
		public InvalidDataSourceException(int number) : base(Strings.InvalidDataSourceException(number))
		{
			this.number = number;
		}

		// Token: 0x0600B5CE RID: 46542 RVA: 0x0029EDCE File Offset: 0x0029CFCE
		public InvalidDataSourceException(int number, Exception innerException) : base(Strings.InvalidDataSourceException(number), innerException)
		{
			this.number = number;
		}

		// Token: 0x0600B5CF RID: 46543 RVA: 0x0029EDE4 File Offset: 0x0029CFE4
		protected InvalidDataSourceException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.number = (int)info.GetValue("number", typeof(int));
		}

		// Token: 0x0600B5D0 RID: 46544 RVA: 0x0029EE0E File Offset: 0x0029D00E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("number", this.number);
		}

		// Token: 0x1700396A RID: 14698
		// (get) Token: 0x0600B5D1 RID: 46545 RVA: 0x0029EE29 File Offset: 0x0029D029
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x040062D0 RID: 25296
		private readonly int number;
	}
}
