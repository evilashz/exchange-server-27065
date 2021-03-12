using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000357 RID: 855
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class SetEmailSignature : ServiceCommand<bool>
	{
		// Token: 0x06001BC2 RID: 7106 RVA: 0x0006AE00 File Offset: 0x00069000
		public SetEmailSignature(CallContext callContext, EmailSignatureConfiguration userEmailSignature) : base(callContext)
		{
			this.newAutoAddSignature = userEmailSignature.AutoAddSignature;
			this.newSignatureText = userEmailSignature.SignatureText;
			this.useDesktopSignature = userEmailSignature.UseDesktopSignature;
		}

		// Token: 0x06001BC3 RID: 7107 RVA: 0x0006AE30 File Offset: 0x00069030
		protected override bool InternalExecute()
		{
			UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.AutoAddSignatureOnMobile);
			UserConfigurationPropertyDefinition propertyDefinition2 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.SignatureTextOnMobile);
			UserConfigurationPropertyDefinition propertyDefinition3 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.UseDesktopSignature);
			new UserOptionsType
			{
				AutoAddSignatureOnMobile = this.newAutoAddSignature,
				SignatureTextOnMobile = this.newSignatureText,
				UseDesktopSignature = this.useDesktopSignature
			}.Commit(base.CallContext, new UserConfigurationPropertyDefinition[]
			{
				propertyDefinition,
				propertyDefinition2,
				propertyDefinition3
			});
			return true;
		}

		// Token: 0x04000FC0 RID: 4032
		private readonly bool newAutoAddSignature;

		// Token: 0x04000FC1 RID: 4033
		private readonly string newSignatureText;

		// Token: 0x04000FC2 RID: 4034
		private readonly bool useDesktopSignature;
	}
}
