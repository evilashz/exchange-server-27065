using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory.SystemConfiguration
{
	// Token: 0x02000AAD RID: 2733
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class GlobalThrottlingPolicyAmbiguousException : ADExternalException
	{
		// Token: 0x06008026 RID: 32806 RVA: 0x001A4E6D File Offset: 0x001A306D
		public GlobalThrottlingPolicyAmbiguousException() : base(DirectoryStrings.GlobalThrottlingPolicyAmbiguousException)
		{
		}

		// Token: 0x06008027 RID: 32807 RVA: 0x001A4E7A File Offset: 0x001A307A
		public GlobalThrottlingPolicyAmbiguousException(Exception innerException) : base(DirectoryStrings.GlobalThrottlingPolicyAmbiguousException, innerException)
		{
		}

		// Token: 0x06008028 RID: 32808 RVA: 0x001A4E88 File Offset: 0x001A3088
		protected GlobalThrottlingPolicyAmbiguousException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06008029 RID: 32809 RVA: 0x001A4E92 File Offset: 0x001A3092
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
