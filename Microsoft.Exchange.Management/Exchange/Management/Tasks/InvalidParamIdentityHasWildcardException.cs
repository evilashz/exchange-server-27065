using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001062 RID: 4194
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class InvalidParamIdentityHasWildcardException : LocalizedException
	{
		// Token: 0x0600B0BE RID: 45246 RVA: 0x00296B85 File Offset: 0x00294D85
		public InvalidParamIdentityHasWildcardException() : base(Strings.InvalidParamIdentityHasWildcardException)
		{
		}

		// Token: 0x0600B0BF RID: 45247 RVA: 0x00296B92 File Offset: 0x00294D92
		public InvalidParamIdentityHasWildcardException(Exception innerException) : base(Strings.InvalidParamIdentityHasWildcardException, innerException)
		{
		}

		// Token: 0x0600B0C0 RID: 45248 RVA: 0x00296BA0 File Offset: 0x00294DA0
		protected InvalidParamIdentityHasWildcardException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600B0C1 RID: 45249 RVA: 0x00296BAA File Offset: 0x00294DAA
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
