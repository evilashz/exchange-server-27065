using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016F RID: 367
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SourceRecipientInvalidException : MigrationPermanentException
	{
		// Token: 0x06001690 RID: 5776 RVA: 0x0006F935 File Offset: 0x0006DB35
		public SourceRecipientInvalidException(string email) : base(Strings.RecipientInfoInvalidAtSource(email))
		{
			this.email = email;
		}

		// Token: 0x06001691 RID: 5777 RVA: 0x0006F94A File Offset: 0x0006DB4A
		public SourceRecipientInvalidException(string email, Exception innerException) : base(Strings.RecipientInfoInvalidAtSource(email), innerException)
		{
			this.email = email;
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0006F960 File Offset: 0x0006DB60
		protected SourceRecipientInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.email = (string)info.GetValue("email", typeof(string));
		}

		// Token: 0x06001693 RID: 5779 RVA: 0x0006F98A File Offset: 0x0006DB8A
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("email", this.email);
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06001694 RID: 5780 RVA: 0x0006F9A5 File Offset: 0x0006DBA5
		public string Email
		{
			get
			{
				return this.email;
			}
		}

		// Token: 0x04000AFC RID: 2812
		private readonly string email;
	}
}
