using System;
using System.Globalization;
using System.Resources;
using System.Web.Compilation;
using Microsoft.Exchange.Data.Common;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000553 RID: 1363
	public class EcpGlobalResourceProvider : IResourceProvider
	{
		// Token: 0x06003FC8 RID: 16328 RVA: 0x000C09D0 File Offset: 0x000BEBD0
		public EcpGlobalResourceProvider(string classKey)
		{
			if (classKey != null)
			{
				if (classKey == "OwaOptionStrings")
				{
					this.resourceManager = EcpGlobalResourceProvider.OwaOptionResourceManager;
					return;
				}
				if (classKey == "OwaOptionClientStrings")
				{
					this.resourceManager = EcpGlobalResourceProvider.OwaOptionClientResourceManager;
					return;
				}
				if (classKey == "ClientStrings")
				{
					this.resourceManager = EcpGlobalResourceProvider.ClientResourceManager;
					return;
				}
				if (!(classKey == "Strings"))
				{
				}
			}
			this.resourceManager = EcpGlobalResourceProvider.ResourceManager;
		}

		// Token: 0x06003FC9 RID: 16329 RVA: 0x000C0A4D File Offset: 0x000BEC4D
		public object GetObject(string resourceKey, CultureInfo culture)
		{
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			return this.resourceManager.GetString(resourceKey, culture);
		}

		// Token: 0x170024D5 RID: 9429
		// (get) Token: 0x06003FCA RID: 16330 RVA: 0x000C0A66 File Offset: 0x000BEC66
		public IResourceReader ResourceReader
		{
			get
			{
				throw new NotSupportedException();
			}
		}

		// Token: 0x170024D6 RID: 9430
		// (get) Token: 0x06003FCB RID: 16331 RVA: 0x000C0A6D File Offset: 0x000BEC6D
		internal static ExchangeResourceManager OwaOptionResourceManager
		{
			get
			{
				if (EcpGlobalResourceProvider.owaOptionResourceManager == null)
				{
					EcpGlobalResourceProvider.owaOptionResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionStrings", typeof(OwaOptionStrings).Assembly);
				}
				return EcpGlobalResourceProvider.owaOptionResourceManager;
			}
		}

		// Token: 0x170024D7 RID: 9431
		// (get) Token: 0x06003FCC RID: 16332 RVA: 0x000C0A99 File Offset: 0x000BEC99
		internal static ExchangeResourceManager OwaOptionClientResourceManager
		{
			get
			{
				if (EcpGlobalResourceProvider.owaOptionClientResourceManager == null)
				{
					EcpGlobalResourceProvider.owaOptionClientResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.OwaOptionClientStrings", typeof(OwaOptionClientStrings).Assembly);
				}
				return EcpGlobalResourceProvider.owaOptionClientResourceManager;
			}
		}

		// Token: 0x170024D8 RID: 9432
		// (get) Token: 0x06003FCD RID: 16333 RVA: 0x000C0AC5 File Offset: 0x000BECC5
		internal static ExchangeResourceManager ResourceManager
		{
			get
			{
				if (EcpGlobalResourceProvider.ecpResourceManager == null)
				{
					EcpGlobalResourceProvider.ecpResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.Strings", typeof(Strings).Assembly);
				}
				return EcpGlobalResourceProvider.ecpResourceManager;
			}
		}

		// Token: 0x170024D9 RID: 9433
		// (get) Token: 0x06003FCE RID: 16334 RVA: 0x000C0AF1 File Offset: 0x000BECF1
		internal static ExchangeResourceManager ClientResourceManager
		{
			get
			{
				if (EcpGlobalResourceProvider.ecpClientResourceManager == null)
				{
					EcpGlobalResourceProvider.ecpClientResourceManager = ExchangeResourceManager.GetResourceManager("Microsoft.Exchange.Management.ControlPanel.ClientStrings", typeof(ClientStrings).Assembly);
				}
				return EcpGlobalResourceProvider.ecpClientResourceManager;
			}
		}

		// Token: 0x04002A6D RID: 10861
		private static ExchangeResourceManager owaOptionResourceManager;

		// Token: 0x04002A6E RID: 10862
		private static ExchangeResourceManager owaOptionClientResourceManager;

		// Token: 0x04002A6F RID: 10863
		private static ExchangeResourceManager ecpResourceManager;

		// Token: 0x04002A70 RID: 10864
		private static ExchangeResourceManager ecpClientResourceManager;

		// Token: 0x04002A71 RID: 10865
		private ExchangeResourceManager resourceManager;
	}
}
