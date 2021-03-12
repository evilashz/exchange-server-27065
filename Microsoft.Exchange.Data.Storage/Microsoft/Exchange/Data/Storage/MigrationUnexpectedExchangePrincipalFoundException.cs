using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x0200010D RID: 269
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class MigrationUnexpectedExchangePrincipalFoundException : MigrationPermanentException
	{
		// Token: 0x060013D0 RID: 5072 RVA: 0x00069CDE File Offset: 0x00067EDE
		public MigrationUnexpectedExchangePrincipalFoundException(string smtpAddress) : base(ServerStrings.MigrationUnexpectedExchangePrincipalFound(smtpAddress))
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x060013D1 RID: 5073 RVA: 0x00069CF3 File Offset: 0x00067EF3
		public MigrationUnexpectedExchangePrincipalFoundException(string smtpAddress, Exception innerException) : base(ServerStrings.MigrationUnexpectedExchangePrincipalFound(smtpAddress), innerException)
		{
			this.smtpAddress = smtpAddress;
		}

		// Token: 0x060013D2 RID: 5074 RVA: 0x00069D09 File Offset: 0x00067F09
		protected MigrationUnexpectedExchangePrincipalFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.smtpAddress = (string)info.GetValue("smtpAddress", typeof(string));
		}

		// Token: 0x060013D3 RID: 5075 RVA: 0x00069D33 File Offset: 0x00067F33
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("smtpAddress", this.smtpAddress);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x060013D4 RID: 5076 RVA: 0x00069D4E File Offset: 0x00067F4E
		public string SmtpAddress
		{
			get
			{
				return this.smtpAddress;
			}
		}

		// Token: 0x0400099B RID: 2459
		private readonly string smtpAddress;
	}
}
