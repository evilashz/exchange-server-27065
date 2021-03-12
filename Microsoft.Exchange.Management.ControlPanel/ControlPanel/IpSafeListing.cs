using System;
using System.Configuration;
using System.Net;
using System.Runtime.Serialization;
using System.Web.Configuration;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000406 RID: 1030
	[DataContract]
	public class IpSafeListing : BaseRow
	{
		// Token: 0x060034CA RID: 13514 RVA: 0x000A4D3F File Offset: 0x000A2F3F
		public IpSafeListing(PerimeterConfig pc) : base(pc)
		{
			this.perimeterConfig = pc;
		}

		// Token: 0x170020B8 RID: 8376
		// (get) Token: 0x060034CB RID: 13515 RVA: 0x000A4D4F File Offset: 0x000A2F4F
		// (set) Token: 0x060034CC RID: 13516 RVA: 0x000A4D61 File Offset: 0x000A2F61
		[DataMember]
		public string[] GatewayIPAddresses
		{
			get
			{
				return this.perimeterConfig.GatewayIPAddresses.ToStringArray<IPAddress>();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020B9 RID: 8377
		// (get) Token: 0x060034CD RID: 13517 RVA: 0x000A4D68 File Offset: 0x000A2F68
		// (set) Token: 0x060034CE RID: 13518 RVA: 0x000A4D7A File Offset: 0x000A2F7A
		[DataMember]
		public string[] InternalServerIPAddresses
		{
			get
			{
				return this.perimeterConfig.InternalServerIPAddresses.ToStringArray<IPAddress>();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020BA RID: 8378
		// (get) Token: 0x060034CF RID: 13519 RVA: 0x000A4D81 File Offset: 0x000A2F81
		// (set) Token: 0x060034D0 RID: 13520 RVA: 0x000A4D8E File Offset: 0x000A2F8E
		[DataMember]
		public bool IPSkiplistingEnabled
		{
			get
			{
				return this.perimeterConfig.IPSkiplistingEnabled;
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170020BB RID: 8379
		// (get) Token: 0x060034D1 RID: 13521 RVA: 0x000A4D95 File Offset: 0x000A2F95
		internal SafelistingUIMode SafelistingUIMode
		{
			get
			{
				return this.perimeterConfig.SafelistingUIMode;
			}
		}

		// Token: 0x170020BC RID: 8380
		// (get) Token: 0x060034D2 RID: 13522 RVA: 0x000A4DA4 File Offset: 0x000A2FA4
		// (set) Token: 0x060034D3 RID: 13523 RVA: 0x000A4DF2 File Offset: 0x000A2FF2
		[DataMember]
		internal string FoseLink
		{
			get
			{
				if (!string.IsNullOrEmpty(this.perimeterConfig.PerimeterOrgId))
				{
					return string.Format("{0}{1}", IpSafeListing.FoseUrl, this.perimeterConfig.PerimeterOrgId);
				}
				return HelpUtil.BuildEhcHref(EACHelpId.FoseLinkNotAvailable.ToString());
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060034D4 RID: 13524 RVA: 0x000A4DFC File Offset: 0x000A2FFC
		private static string GetFoseUrl()
		{
			try
			{
				string text = WebConfigurationManager.AppSettings["FoseSsoUrl"];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
			}
			catch (ConfigurationErrorsException)
			{
			}
			return "https://login.live.com/login.srf?wa=wsignin1.0&wtrealm=urn%3afopsts%3aprod&wctx=rm%3d0%26id%3dFederatedPassiveSignIn1%26ru%3d%252f%253fwa%253dwsignin1.0%2526wtrealm%253dhttps%25253a%25252f%25252fadmin.messaging.microsoft.com%2526wctx%253drm%25253d0%252526id%25253dpassive%252526ru%25253d%2525252fSettings.mvc%2525252fSettings%2525252f";
		}

		// Token: 0x04002541 RID: 9537
		private const string FoseSsoProdUrl = "https://login.live.com/login.srf?wa=wsignin1.0&wtrealm=urn%3afopsts%3aprod&wctx=rm%3d0%26id%3dFederatedPassiveSignIn1%26ru%3d%252f%253fwa%253dwsignin1.0%2526wtrealm%253dhttps%25253a%25252f%25252fadmin.messaging.microsoft.com%2526wctx%253drm%25253d0%252526id%25253dpassive%252526ru%25253d%2525252fSettings.mvc%2525252fSettings%2525252f";

		// Token: 0x04002542 RID: 9538
		private static readonly string FoseUrl = IpSafeListing.GetFoseUrl();

		// Token: 0x04002543 RID: 9539
		private PerimeterConfig perimeterConfig;
	}
}
