using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020010C5 RID: 4293
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CannotDisableFederationException : FederationException
	{
		// Token: 0x0600B2D2 RID: 45778 RVA: 0x0029A6A1 File Offset: 0x002988A1
		public CannotDisableFederationException() : base(Strings.ErrorCannotDisableFederation)
		{
		}

		// Token: 0x0600B2D3 RID: 45779 RVA: 0x0029A6AE File Offset: 0x002988AE
		public CannotDisableFederationException(Exception innerException) : base(Strings.ErrorCannotDisableFederation, innerException)
		{
		}

		// Token: 0x0600B2D4 RID: 45780 RVA: 0x0029A6BC File Offset: 0x002988BC
		protected CannotDisableFederationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B2D5 RID: 45781 RVA: 0x0029A6C6 File Offset: 0x002988C6
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
