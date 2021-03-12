using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.ExSetupUI
{
	// Token: 0x02000032 RID: 50
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class AssemblyLoadFileNotFoundException : LocalizedException
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000D2FE File Offset: 0x0000B4FE
		public AssemblyLoadFileNotFoundException() : base(Strings.AssemblyLoadError)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D30B File Offset: 0x0000B50B
		public AssemblyLoadFileNotFoundException(Exception innerException) : base(Strings.AssemblyLoadError, innerException)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D319 File Offset: 0x0000B519
		protected AssemblyLoadFileNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D323 File Offset: 0x0000B523
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
