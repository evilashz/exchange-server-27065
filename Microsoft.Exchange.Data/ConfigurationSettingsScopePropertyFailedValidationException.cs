using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F3 RID: 243
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsScopePropertyFailedValidationException : ConfigurationSettingsException
	{
		// Token: 0x06000860 RID: 2144 RVA: 0x0001B7B1 File Offset: 0x000199B1
		public ConfigurationSettingsScopePropertyFailedValidationException(string name, string value) : base(DataStrings.ConfigurationSettingsScopePropertyFailedValidation(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000861 RID: 2145 RVA: 0x0001B7CE File Offset: 0x000199CE
		public ConfigurationSettingsScopePropertyFailedValidationException(string name, string value, Exception innerException) : base(DataStrings.ConfigurationSettingsScopePropertyFailedValidation(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000862 RID: 2146 RVA: 0x0001B7EC File Offset: 0x000199EC
		protected ConfigurationSettingsScopePropertyFailedValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06000863 RID: 2147 RVA: 0x0001B841 File Offset: 0x00019A41
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x170002E3 RID: 739
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x0001B86D File Offset: 0x00019A6D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E4 RID: 740
		// (get) Token: 0x06000865 RID: 2149 RVA: 0x0001B875 File Offset: 0x00019A75
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x0400059A RID: 1434
		private readonly string name;

		// Token: 0x0400059B RID: 1435
		private readonly string value;
	}
}
