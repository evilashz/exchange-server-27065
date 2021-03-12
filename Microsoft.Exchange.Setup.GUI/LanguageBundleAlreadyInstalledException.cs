using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200001F RID: 31
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class LanguageBundleAlreadyInstalledException : LocalizedException
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000C43C File Offset: 0x0000A63C
		public LanguageBundleAlreadyInstalledException() : base(Strings.LanguageBundleCannotRunInstall)
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000C449 File Offset: 0x0000A649
		public LanguageBundleAlreadyInstalledException(Exception innerException) : base(Strings.LanguageBundleCannotRunInstall, innerException)
		{
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C457 File Offset: 0x0000A657
		protected LanguageBundleAlreadyInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C461 File Offset: 0x0000A661
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
		}
	}
}
