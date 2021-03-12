using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Services.Core.Types;
using Microsoft.Exchange.Services.Wcf;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x02000328 RID: 808
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class GetEmailSignature : ServiceCommand<EmailSignatureConfiguration>
	{
		// Token: 0x06001ACA RID: 6858 RVA: 0x000654E0 File Offset: 0x000636E0
		public GetEmailSignature(CallContext callContext) : base(callContext)
		{
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x000654E9 File Offset: 0x000636E9
		protected override EmailSignatureConfiguration InternalExecute()
		{
			return GetEmailSignature.GetSetting(base.CallContext);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000654F8 File Offset: 0x000636F8
		public static EmailSignatureConfiguration GetSetting(CallContext callContext)
		{
			EmailSignatureConfiguration emailSignatureConfiguration = new EmailSignatureConfiguration();
			UserConfigurationPropertyDefinition propertyDefinition = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.AutoAddSignatureOnMobile);
			UserConfigurationPropertyDefinition propertyDefinition2 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.SignatureTextOnMobile);
			UserConfigurationPropertyDefinition propertyDefinition3 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.UseDesktopSignature);
			UserConfigurationPropertyDefinition propertyDefinition4 = UserOptionPropertySchema.Instance.GetPropertyDefinition(UserConfigurationPropertyId.SignatureText);
			UserOptionsType userOptionsType = new UserOptionsType();
			userOptionsType.Load(callContext, new UserConfigurationPropertyDefinition[]
			{
				propertyDefinition,
				propertyDefinition2,
				propertyDefinition3,
				propertyDefinition4
			});
			emailSignatureConfiguration.AutoAddSignature = userOptionsType.AutoAddSignatureOnMobile;
			emailSignatureConfiguration.SignatureText = userOptionsType.SignatureTextOnMobile;
			emailSignatureConfiguration.UseDesktopSignature = userOptionsType.UseDesktopSignature;
			emailSignatureConfiguration.DesktopSignatureText = userOptionsType.SignatureText;
			return emailSignatureConfiguration;
		}
	}
}
