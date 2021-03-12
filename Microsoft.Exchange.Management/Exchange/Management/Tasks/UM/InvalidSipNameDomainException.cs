using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011AE RID: 4526
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidSipNameDomainException : LocalizedException
	{
		// Token: 0x0600B853 RID: 47187 RVA: 0x002A472F File Offset: 0x002A292F
		public InvalidSipNameDomainException() : base(Strings.ExceptionInvalidSipNameDomain)
		{
		}

		// Token: 0x0600B854 RID: 47188 RVA: 0x002A473C File Offset: 0x002A293C
		public InvalidSipNameDomainException(Exception innerException) : base(Strings.ExceptionInvalidSipNameDomain, innerException)
		{
		}

		// Token: 0x0600B855 RID: 47189 RVA: 0x002A474A File Offset: 0x002A294A
		protected InvalidSipNameDomainException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B856 RID: 47190 RVA: 0x002A4754 File Offset: 0x002A2954
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
