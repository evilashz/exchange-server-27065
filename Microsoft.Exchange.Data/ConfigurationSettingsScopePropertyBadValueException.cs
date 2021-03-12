using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F4 RID: 244
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsScopePropertyBadValueException : ConfigurationSettingsException
	{
		// Token: 0x06000866 RID: 2150 RVA: 0x0001B87D File Offset: 0x00019A7D
		public ConfigurationSettingsScopePropertyBadValueException(string name, string value) : base(DataStrings.ConfigurationSettingsScopePropertyBadValue(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0001B89A File Offset: 0x00019A9A
		public ConfigurationSettingsScopePropertyBadValueException(string name, string value, Exception innerException) : base(DataStrings.ConfigurationSettingsScopePropertyBadValue(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0001B8B8 File Offset: 0x00019AB8
		protected ConfigurationSettingsScopePropertyBadValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0001B90D File Offset: 0x00019B0D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x170002E5 RID: 741
		// (get) Token: 0x0600086A RID: 2154 RVA: 0x0001B939 File Offset: 0x00019B39
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E6 RID: 742
		// (get) Token: 0x0600086B RID: 2155 RVA: 0x0001B941 File Offset: 0x00019B41
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400059C RID: 1436
		private readonly string name;

		// Token: 0x0400059D RID: 1437
		private readonly string value;
	}
}
