using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.IisTasks
{
	// Token: 0x02000DD8 RID: 3544
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class IsapiExtensionMustHavePhysicalPathException : LocalizedException
	{
		// Token: 0x0600A425 RID: 42021 RVA: 0x00283BD4 File Offset: 0x00281DD4
		public IsapiExtensionMustHavePhysicalPathException() : base(Strings.IsapiExtensionMustHavePhysicalPathException)
		{
		}

		// Token: 0x0600A426 RID: 42022 RVA: 0x00283BE1 File Offset: 0x00281DE1
		public IsapiExtensionMustHavePhysicalPathException(Exception innerException) : base(Strings.IsapiExtensionMustHavePhysicalPathException, innerException)
		{
		}

		// Token: 0x0600A427 RID: 42023 RVA: 0x00283BEF File Offset: 0x00281DEF
		protected IsapiExtensionMustHavePhysicalPathException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x0600A428 RID: 42024 RVA: 0x00283BF9 File Offset: 0x00281DF9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
