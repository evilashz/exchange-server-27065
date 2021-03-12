using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200016E RID: 366
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SourceRecipientDoesNotExistException : MigrationPermanentException
	{
		// Token: 0x0600168B RID: 5771 RVA: 0x0006F8BD File Offset: 0x0006DABD
		public SourceRecipientDoesNotExistException(string email) : base(Strings.RecipientDoesNotExistAtSource(email))
		{
			this.email = email;
		}

		// Token: 0x0600168C RID: 5772 RVA: 0x0006F8D2 File Offset: 0x0006DAD2
		public SourceRecipientDoesNotExistException(string email, Exception innerException) : base(Strings.RecipientDoesNotExistAtSource(email), innerException)
		{
			this.email = email;
		}

		// Token: 0x0600168D RID: 5773 RVA: 0x0006F8E8 File Offset: 0x0006DAE8
		protected SourceRecipientDoesNotExistException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.email = (string)info.GetValue("email", typeof(string));
		}

		// Token: 0x0600168E RID: 5774 RVA: 0x0006F912 File Offset: 0x0006DB12
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("email", this.email);
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600168F RID: 5775 RVA: 0x0006F92D File Offset: 0x0006DB2D
		public string Email
		{
			get
			{
				return this.email;
			}
		}

		// Token: 0x04000AFB RID: 2811
		private readonly string email;
	}
}
