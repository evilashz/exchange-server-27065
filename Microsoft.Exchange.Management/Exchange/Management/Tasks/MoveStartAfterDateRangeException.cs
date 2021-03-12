using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000EA1 RID: 3745
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MoveStartAfterDateRangeException : RecipientTaskException
	{
		// Token: 0x0600A7FC RID: 43004 RVA: 0x002894D1 File Offset: 0x002876D1
		public MoveStartAfterDateRangeException(int days) : base(Strings.ErrorStartAfter(days))
		{
			this.days = days;
		}

		// Token: 0x0600A7FD RID: 43005 RVA: 0x002894E6 File Offset: 0x002876E6
		public MoveStartAfterDateRangeException(int days, Exception innerException) : base(Strings.ErrorStartAfter(days), innerException)
		{
			this.days = days;
		}

		// Token: 0x0600A7FE RID: 43006 RVA: 0x002894FC File Offset: 0x002876FC
		protected MoveStartAfterDateRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.days = (int)info.GetValue("days", typeof(int));
		}

		// Token: 0x0600A7FF RID: 43007 RVA: 0x00289526 File Offset: 0x00287726
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("days", this.days);
		}

		// Token: 0x17003695 RID: 13973
		// (get) Token: 0x0600A800 RID: 43008 RVA: 0x00289541 File Offset: 0x00287741
		public int Days
		{
			get
			{
				return this.days;
			}
		}

		// Token: 0x04005FFB RID: 24571
		private readonly int days;
	}
}
