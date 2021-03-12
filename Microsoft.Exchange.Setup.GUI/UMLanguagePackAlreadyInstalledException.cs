using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Setup.GUI
{
	// Token: 0x0200001E RID: 30
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	internal class UMLanguagePackAlreadyInstalledException : LocalizedException
	{
		// Token: 0x0600015F RID: 351 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public UMLanguagePackAlreadyInstalledException(string culture) : base(Strings.UnifiedMessagingCannotRunInstall(culture))
		{
			this.culture = culture;
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000C3D9 File Offset: 0x0000A5D9
		public UMLanguagePackAlreadyInstalledException(string culture, Exception innerException) : base(Strings.UnifiedMessagingCannotRunInstall(culture), innerException)
		{
			this.culture = culture;
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000C3EF File Offset: 0x0000A5EF
		protected UMLanguagePackAlreadyInstalledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.culture = (string)info.GetValue("culture", typeof(string));
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000C419 File Offset: 0x0000A619
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("culture", this.culture);
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x06000163 RID: 355 RVA: 0x0000C434 File Offset: 0x0000A634
		public string Culture
		{
			get
			{
				return this.culture;
			}
		}

		// Token: 0x04000102 RID: 258
		private readonly string culture;
	}
}
