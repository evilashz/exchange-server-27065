using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Autodiscover.ConfigurationSettings
{
	// Token: 0x02000047 RID: 71
	public class UserConfigurationSettings
	{
		// Token: 0x060001C8 RID: 456 RVA: 0x00008C66 File Offset: 0x00006E66
		public UserConfigurationSettings(HashSet<UserConfigurationSettingName> requestedSettings)
		{
			this.settings = new Dictionary<UserConfigurationSettingName, object>();
			this.requestedSettings = requestedSettings;
			this.errorCode = UserConfigurationSettingsErrorCode.NoError;
			this.errorMessage = string.Empty;
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00008C92 File Offset: 0x00006E92
		public UserConfigurationSettings() : this(new HashSet<UserConfigurationSettingName>())
		{
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060001CA RID: 458 RVA: 0x00008C9F File Offset: 0x00006E9F
		// (set) Token: 0x060001CB RID: 459 RVA: 0x00008CA7 File Offset: 0x00006EA7
		public UserConfigurationSettingsErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x1700008E RID: 142
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00008CB0 File Offset: 0x00006EB0
		// (set) Token: 0x060001CD RID: 461 RVA: 0x00008CB8 File Offset: 0x00006EB8
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x060001CE RID: 462 RVA: 0x00008CC1 File Offset: 0x00006EC1
		// (set) Token: 0x060001CF RID: 463 RVA: 0x00008CC9 File Offset: 0x00006EC9
		public string RedirectTarget
		{
			get
			{
				return this.redirectTarget;
			}
			set
			{
				this.redirectTarget = value;
			}
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00008CD2 File Offset: 0x00006ED2
		public void Add(UserConfigurationSettingName name, object value)
		{
			this.settings.Add(name, value);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008CE4 File Offset: 0x00006EE4
		public T Get<T>(UserConfigurationSettingName name)
		{
			object obj;
			if (this.settings.TryGetValue(name, out obj))
			{
				try
				{
					return (T)((object)obj);
				}
				catch (InvalidCastException)
				{
					string arg = string.Empty;
					if (obj != null)
					{
						arg = obj.GetType().FullName;
					}
					throw new ArgumentException(string.Format("Unable to cast Setting {0} value type '{1}' to '{2}'", name, arg, typeof(T).FullName));
				}
			}
			if (this.requestedSettings.Contains(name))
			{
				return default(T);
			}
			throw new ArgumentException(name + " was not a requested setting");
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008D88 File Offset: 0x00006F88
		public string GetString(UserConfigurationSettingName name)
		{
			return this.Get<string>(name);
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x060001D3 RID: 467 RVA: 0x00008D91 File Offset: 0x00006F91
		public IEnumerable<UserConfigurationSettingName> Keys
		{
			get
			{
				return this.settings.Keys;
			}
		}

		// Token: 0x04000210 RID: 528
		private Dictionary<UserConfigurationSettingName, object> settings;

		// Token: 0x04000211 RID: 529
		private HashSet<UserConfigurationSettingName> requestedSettings;

		// Token: 0x04000212 RID: 530
		private UserConfigurationSettingsErrorCode errorCode;

		// Token: 0x04000213 RID: 531
		private string errorMessage;

		// Token: 0x04000214 RID: 532
		private string redirectTarget;
	}
}
