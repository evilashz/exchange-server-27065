using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.ReportingTask
{
	// Token: 0x0200115D RID: 4445
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidDateParameterException : ReportingException
	{
		// Token: 0x0600B5BC RID: 46524 RVA: 0x0029EBA8 File Offset: 0x0029CDA8
		public InvalidDateParameterException(DateTime startDate, DateTime endDate) : base(Strings.InvalidDateParameterException(startDate, endDate))
		{
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x0600B5BD RID: 46525 RVA: 0x0029EBC5 File Offset: 0x0029CDC5
		public InvalidDateParameterException(DateTime startDate, DateTime endDate, Exception innerException) : base(Strings.InvalidDateParameterException(startDate, endDate), innerException)
		{
			this.startDate = startDate;
			this.endDate = endDate;
		}

		// Token: 0x0600B5BE RID: 46526 RVA: 0x0029EBE4 File Offset: 0x0029CDE4
		protected InvalidDateParameterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.startDate = (DateTime)info.GetValue("startDate", typeof(DateTime));
			this.endDate = (DateTime)info.GetValue("endDate", typeof(DateTime));
		}

		// Token: 0x0600B5BF RID: 46527 RVA: 0x0029EC39 File Offset: 0x0029CE39
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("startDate", this.startDate);
			info.AddValue("endDate", this.endDate);
		}

		// Token: 0x17003965 RID: 14693
		// (get) Token: 0x0600B5C0 RID: 46528 RVA: 0x0029EC65 File Offset: 0x0029CE65
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
		}

		// Token: 0x17003966 RID: 14694
		// (get) Token: 0x0600B5C1 RID: 46529 RVA: 0x0029EC6D File Offset: 0x0029CE6D
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
		}

		// Token: 0x040062CB RID: 25291
		private readonly DateTime startDate;

		// Token: 0x040062CC RID: 25292
		private readonly DateTime endDate;
	}
}
