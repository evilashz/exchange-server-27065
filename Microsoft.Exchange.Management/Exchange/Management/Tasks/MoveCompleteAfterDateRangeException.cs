using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA2 RID: 3746
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveCompleteAfterDateRangeException : RecipientTaskException
	{
		// Token: 0x0600A801 RID: 43009 RVA: 0x00289549 File Offset: 0x00287749
		public MoveCompleteAfterDateRangeException(int days) : base(Strings.ErrorCompleteAfter(days))
		{
			this.days = days;
		}

		// Token: 0x0600A802 RID: 43010 RVA: 0x0028955E File Offset: 0x0028775E
		public MoveCompleteAfterDateRangeException(int days, Exception innerException) : base(Strings.ErrorCompleteAfter(days), innerException)
		{
			this.days = days;
		}

		// Token: 0x0600A803 RID: 43011 RVA: 0x00289574 File Offset: 0x00287774
		protected MoveCompleteAfterDateRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.days = (int)info.GetValue("days", typeof(int));
		}

		// Token: 0x0600A804 RID: 43012 RVA: 0x0028959E File Offset: 0x0028779E
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("days", this.days);
		}

		// Token: 0x17003696 RID: 13974
		// (get) Token: 0x0600A805 RID: 43013 RVA: 0x002895B9 File Offset: 0x002877B9
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x04005FFC RID: 24572
		private readonly int days;
	}
}
