using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000E8E RID: 3726
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class MailboxConstraintsMismatchException : LocalizedException
	{
		// Token: 0x0600A79E RID: 42910 RVA: 0x00288BE1 File Offset: 0x00286DE1
		public MailboxConstraintsMismatchException(string user, string databaseName, string constraint) : base(Strings.ErrorCannotMoveToTargetDatabaseAsConstraintsAreNotMet(user, databaseName, constraint))
		{
			this.user = user;
			this.databaseName = databaseName;
			this.constraint = constraint;
		}

		// Token: 0x0600A79F RID: 42911 RVA: 0x00288C06 File Offset: 0x00286E06
		public MailboxConstraintsMismatchException(string user, string databaseName, string constraint, Exception innerException) : base(Strings.ErrorCannotMoveToTargetDatabaseAsConstraintsAreNotMet(user, databaseName, constraint), innerException)
		{
			this.user = user;
			this.databaseName = databaseName;
			this.constraint = constraint;
		}

		// Token: 0x0600A7A0 RID: 42912 RVA: 0x00288C30 File Offset: 0x00286E30
		protected MailboxConstraintsMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.user = (string)info.GetValue("user", typeof(string));
			this.databaseName = (string)info.GetValue("databaseName", typeof(string));
			this.constraint = (string)info.GetValue("constraint", typeof(string));
		}

		// Token: 0x0600A7A1 RID: 42913 RVA: 0x00288CA5 File Offset: 0x00286EA5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("user", this.user);
			info.AddValue("databaseName", this.databaseName);
			info.AddValue("constraint", this.constraint);
		}

		// Token: 0x17003683 RID: 13955
		// (get) Token: 0x0600A7A2 RID: 42914 RVA: 0x00288CE2 File Offset: 0x00286EE2
		public string User
		{
			get
			{
				return this.user;
			}
		}

		// Token: 0x17003684 RID: 13956
		// (get) Token: 0x0600A7A3 RID: 42915 RVA: 0x00288CEA File Offset: 0x00286EEA
		public string DatabaseName
		{
			get
			{
				return this.databaseName;
			}
		}

		// Token: 0x17003685 RID: 13957
		// (get) Token: 0x0600A7A4 RID: 42916 RVA: 0x00288CF2 File Offset: 0x00286EF2
		public string Constraint
		{
			get
			{
				return this.constraint;
			}
		}

		// Token: 0x04005FE9 RID: 24553
		private readonly string user;

		// Token: 0x04005FEA RID: 24554
		private readonly string databaseName;

		// Token: 0x04005FEB RID: 24555
		private readonly string constraint;
	}
}
