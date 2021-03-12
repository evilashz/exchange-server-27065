using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000184 RID: 388
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UnexpectedSubscriptionStatusException : MigrationPermanentException
	{
		// Token: 0x060016EB RID: 5867 RVA: 0x0006FF1A File Offset: 0x0006E11A
		public UnexpectedSubscriptionStatusException(string status) : base(Strings.UnexpectedSubscriptionStatus(status))
		{
			this.status = status;
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x0006FF2F File Offset: 0x0006E12F
		public UnexpectedSubscriptionStatusException(string status, Exception innerException) : base(Strings.UnexpectedSubscriptionStatus(status), innerException)
		{
			this.status = status;
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x0006FF45 File Offset: 0x0006E145
		protected UnexpectedSubscriptionStatusException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.status = (string)info.GetValue("status", typeof(string));
		}

		// Token: 0x060016EE RID: 5870 RVA: 0x0006FF6F File Offset: 0x0006E16F
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("status", this.status);
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x060016EF RID: 5871 RVA: 0x0006FF8A File Offset: 0x0006E18A
		public string Status
		{
			get
			{
				return this.status;
			}
		}

		// Token: 0x04000B03 RID: 2819
		private readonly string status;
	}
}
