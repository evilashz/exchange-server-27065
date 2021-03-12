using System;
using System.Globalization;

namespace Microsoft.Exchange.Clients.Owa.Core
{
	// Token: 0x020000EA RID: 234
	internal sealed class ClientMapping
	{
		// Token: 0x17000227 RID: 551
		// (get) Token: 0x060007D3 RID: 2003 RVA: 0x0003B034 File Offset: 0x00039234
		// (set) Token: 0x060007D4 RID: 2004 RVA: 0x0003B03C File Offset: 0x0003923C
		internal string Application
		{
			get
			{
				return this.application;
			}
			set
			{
				this.application = value;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x060007D5 RID: 2005 RVA: 0x0003B045 File Offset: 0x00039245
		// (set) Token: 0x060007D6 RID: 2006 RVA: 0x0003B04D File Offset: 0x0003924D
		internal UserAgentParser.UserAgentVersion MinimumVersion
		{
			get
			{
				return this.minimumVersion;
			}
			set
			{
				this.minimumVersion = value;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x060007D7 RID: 2007 RVA: 0x0003B056 File Offset: 0x00039256
		// (set) Token: 0x060007D8 RID: 2008 RVA: 0x0003B05E File Offset: 0x0003925E
		internal string Platform
		{
			get
			{
				return this.platform;
			}
			set
			{
				this.platform = value;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x060007D9 RID: 2009 RVA: 0x0003B067 File Offset: 0x00039267
		// (set) Token: 0x060007DA RID: 2010 RVA: 0x0003B06F File Offset: 0x0003926F
		internal ClientControl Control
		{
			get
			{
				return this.control;
			}
			set
			{
				this.control = value;
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x060007DB RID: 2011 RVA: 0x0003B078 File Offset: 0x00039278
		// (set) Token: 0x060007DC RID: 2012 RVA: 0x0003B080 File Offset: 0x00039280
		internal Experience Experience
		{
			get
			{
				return this.experience;
			}
			set
			{
				this.experience = value;
			}
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x0003B089 File Offset: 0x00039289
		internal ClientMapping()
		{
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x0003B0A8 File Offset: 0x000392A8
		internal ClientMapping(string application, UserAgentParser.UserAgentVersion minimumVersion, string platform, ClientControl control, Experience experience)
		{
			this.application = application;
			this.minimumVersion = minimumVersion;
			this.platform = platform;
			this.control = control;
			this.experience = experience;
		}

		// Token: 0x060007DF RID: 2015 RVA: 0x0003B0F8 File Offset: 0x000392F8
		internal static ClientMapping Copy(ClientMapping clientMapping)
		{
			return new ClientMapping
			{
				application = string.Copy(clientMapping.Application),
				minimumVersion = clientMapping.MinimumVersion,
				platform = clientMapping.Platform,
				control = clientMapping.Control,
				experience = Experience.Copy(clientMapping.Experience)
			};
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x0003B154 File Offset: 0x00039354
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "Experience = ({0}), Application = {1}, Platform = {2}, Control = {3}, Minimum Version = {4}", new object[]
			{
				this.experience,
				this.application,
				this.platform,
				this.control,
				this.minimumVersion
			});
		}

		// Token: 0x04000594 RID: 1428
		private string application = string.Empty;

		// Token: 0x04000595 RID: 1429
		private UserAgentParser.UserAgentVersion minimumVersion;

		// Token: 0x04000596 RID: 1430
		private string platform = string.Empty;

		// Token: 0x04000597 RID: 1431
		private ClientControl control;

		// Token: 0x04000598 RID: 1432
		private Experience experience;
	}
}
