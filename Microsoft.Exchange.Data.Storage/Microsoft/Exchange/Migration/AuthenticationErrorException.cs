using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Storage.Management;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Migration
{
	// Token: 0x02000178 RID: 376
	[ClassAccessLevel(AccessLevel.MSInternal)]
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AuthenticationErrorException : MigrationPermanentException
	{
		// Token: 0x060016BA RID: 5818 RVA: 0x0006FC9D File Offset: 0x0006DE9D
		public AuthenticationErrorException() : base(Strings.AuthenticationErrorStatus)
		{
		}

		// Token: 0x060016BB RID: 5819 RVA: 0x0006FCAA File Offset: 0x0006DEAA
		public AuthenticationErrorException(Exception innerException) : base(Strings.AuthenticationErrorStatus, innerException)
		{
		}

		// Token: 0x060016BC RID: 5820 RVA: 0x0006FCB8 File Offset: 0x0006DEB8
		protected AuthenticationErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060016BD RID: 5821 RVA: 0x0006FCC2 File Offset: 0x0006DEC2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
