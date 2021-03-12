using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks.UM
{
	// Token: 0x020011D0 RID: 4560
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SUC_ExchangePrincipalError : LocalizedException
	{
		// Token: 0x0600B8F1 RID: 47345 RVA: 0x002A5398 File Offset: 0x002A3598
		public SUC_ExchangePrincipalError() : base(Strings.ExchangePrincipalError)
		{
		}

		// Token: 0x0600B8F2 RID: 47346 RVA: 0x002A53A5 File Offset: 0x002A35A5
		public SUC_ExchangePrincipalError(Exception innerException) : base(Strings.ExchangePrincipalError, innerException)
		{
		}

		// Token: 0x0600B8F3 RID: 47347 RVA: 0x002A53B3 File Offset: 0x002A35B3
		protected SUC_ExchangePrincipalError(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B8F4 RID: 47348 RVA: 0x002A53BD File Offset: 0x002A35BD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
