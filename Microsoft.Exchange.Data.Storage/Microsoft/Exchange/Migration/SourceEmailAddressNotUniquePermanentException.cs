using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200018C RID: 396
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class SourceEmailAddressNotUniquePermanentException : MigrationPermanentException
	{
		// Token: 0x0600170D RID: 5901 RVA: 0x0007011C File Offset: 0x0006E31C
		public SourceEmailAddressNotUniquePermanentException(string smtpAddress) : base(Strings.SourceEmailAddressNotUnique(smtpAddress))
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600170E RID: 5902 RVA: 0x00070131 File Offset: 0x0006E331
		public SourceEmailAddressNotUniquePermanentException(string smtpAddress, Exception innerException) : base(Strings.SourceEmailAddressNotUnique(smtpAddress), innerException)
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00070147 File Offset: 0x0006E347
		protected SourceEmailAddressNotUniquePermanentException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
		}

		// Token: 0x06001710 RID: 5904 RVA: 0x00070171 File Offset: 0x0006E371
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("smtpAddress", this.smtpAddress);
		}

		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x0007018C File Offset: 0x0006E38C
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x04000B05 RID: 2821
		private readonly string smtpAddress;
	}
}
