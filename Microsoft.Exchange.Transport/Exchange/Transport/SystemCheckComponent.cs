using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000069 RID: 105
	internal sealed class SystemCheckComponent : ISystemCheckComponent, ITransportComponent
	{
		// Token: 0x06000333 RID: 819 RVA: 0x0000E5A1 File Offset: 0x0000C7A1
		public SystemCheckComponent()
		{
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000E5A9 File Offset: 0x0000C7A9
		internal SystemCheckComponent(ISystemCheck diskSystemCheck)
		{
			ArgumentValidator.ThrowIfNull("diskSystemCheck", diskSystemCheck);
			this.diskSystemCheck = diskSystemCheck;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x0000E5C3 File Offset: 0x0000C7C3
		public void SetLoadTimeDependencies(SystemCheckConfig systemCheckConfig, TransportAppConfig transportAppConfig, ITransportConfiguration transportConfiguration)
		{
			ArgumentValidator.ThrowIfNull("systemCheckConfig", systemCheckConfig);
			this.systemCheckConfig = systemCheckConfig;
			ArgumentValidator.ThrowIfNull("transportAppConfig", transportAppConfig);
			this.transportAppConfig = transportAppConfig;
			ArgumentValidator.ThrowIfNull("transportConfiguration", transportConfiguration);
			this.transportConfiguration = transportConfiguration;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public void Load()
		{
			if (!this.systemCheckConfig.IsSystemCheckEnabled)
			{
				return;
			}
			if (this.systemCheckConfig.IsDiskSystemCheckEnabled)
			{
				if (this.diskSystemCheck == null)
				{
					this.diskSystemCheck = new DiskSystemCheck(this.systemCheckConfig, this.transportAppConfig, this.transportConfiguration);
				}
				this.diskSystemCheck.Check();
			}
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000E654 File Offset: 0x0000C854
		public void Unload()
		{
			if (!this.systemCheckConfig.IsSystemCheckEnabled)
			{
				return;
			}
			this.diskSystemCheck = null;
		}

		// Token: 0x06000338 RID: 824 RVA: 0x0000E66B File Offset: 0x0000C86B
		public string OnUnhandledException(Exception e)
		{
			return null;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0000E66E File Offset: 0x0000C86E
		public ISystemCheck DiskSystemCheck
		{
			get
			{
				return this.diskSystemCheck;
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x0600033A RID: 826 RVA: 0x0000E676 File Offset: 0x0000C876
		public bool Enabled
		{
			get
			{
				return this.systemCheckConfig.IsSystemCheckEnabled;
			}
		}

		// Token: 0x040001C6 RID: 454
		private SystemCheckConfig systemCheckConfig;

		// Token: 0x040001C7 RID: 455
		private TransportAppConfig transportAppConfig;

		// Token: 0x040001C8 RID: 456
		private ITransportConfiguration transportConfiguration;

		// Token: 0x040001C9 RID: 457
		private ISystemCheck diskSystemCheck;
	}
}
