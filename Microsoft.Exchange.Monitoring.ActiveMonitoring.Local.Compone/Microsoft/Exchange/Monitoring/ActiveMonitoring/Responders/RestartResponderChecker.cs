using System;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders
{
	// Token: 0x02000202 RID: 514
	internal abstract class RestartResponderChecker
	{
		// Token: 0x06000E54 RID: 3668 RVA: 0x00060188 File Offset: 0x0005E388
		internal RestartResponderChecker(ResponderDefinition definition)
		{
			this.definition = definition;
		}

		// Token: 0x170002D2 RID: 722
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00060197 File Offset: 0x0005E397
		internal bool IsRestartAllowed
		{
			get
			{
				return !this.Enabled || !this.CheckChangedSetting() || this.IsWithinThreshold();
			}
		}

		// Token: 0x170002D3 RID: 723
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x000601B1 File Offset: 0x0005E3B1
		internal virtual string SkipReasonOrException
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002D4 RID: 724
		// (get) Token: 0x06000E57 RID: 3671 RVA: 0x000601B8 File Offset: 0x0005E3B8
		internal virtual string KeyOfEnabled
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002D5 RID: 725
		// (get) Token: 0x06000E58 RID: 3672 RVA: 0x000601BF File Offset: 0x0005E3BF
		internal virtual string KeyOfSetting
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170002D6 RID: 726
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x000601C6 File Offset: 0x0005E3C6
		internal virtual bool EnabledByDefault
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170002D7 RID: 727
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x000601C9 File Offset: 0x0005E3C9
		internal virtual string DefaultSetting
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000601CC File Offset: 0x0005E3CC
		protected virtual bool OnSettingChange(string newSetting)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000601D3 File Offset: 0x0005E3D3
		protected virtual bool IsWithinThreshold()
		{
			throw new NotImplementedException();
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x000601DA File Offset: 0x0005E3DA
		protected virtual bool Enabled
		{
			get
			{
				return new AttributeHelper(this.definition).GetBool(this.KeyOfEnabled, false, this.EnabledByDefault);
			}
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000601FC File Offset: 0x0005E3FC
		protected virtual bool CheckChangedSetting()
		{
			string @string = new AttributeHelper(this.definition).GetString(this.KeyOfSetting, false, this.DefaultSetting);
			if (string.Compare(this.setting, @string, true) != 0)
			{
				this.setting = @string;
				return this.OnSettingChange(@string);
			}
			return true;
		}

		// Token: 0x04000AC3 RID: 2755
		private readonly ResponderDefinition definition;

		// Token: 0x04000AC4 RID: 2756
		private string setting;
	}
}
