using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x0200015B RID: 347
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class CannotSpecifyUnicodeInCredentialsException : MigrationPermanentException
	{
		// Token: 0x0600162D RID: 5677 RVA: 0x0006EFC9 File Offset: 0x0006D1C9
		public CannotSpecifyUnicodeInCredentialsException() : base(Strings.CannotSpecifyUnicodeInCredentials)
		{
		}

		// Token: 0x0600162E RID: 5678 RVA: 0x0006EFD6 File Offset: 0x0006D1D6
		public CannotSpecifyUnicodeInCredentialsException(Exception innerException) : base(Strings.CannotSpecifyUnicodeInCredentials, innerException)
		{
		}

		// Token: 0x0600162F RID: 5679 RVA: 0x0006EFE4 File Offset: 0x0006D1E4
		protected CannotSpecifyUnicodeInCredentialsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06001630 RID: 5680 RVA: 0x0006EFEE File Offset: 0x0006D1EE
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
