using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F0 RID: 240
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsPropertyFailedValidationException : ConfigurationSettingsException
	{
		// Token: 0x0600084E RID: 2126 RVA: 0x0001B54D File Offset: 0x0001974D
		public ConfigurationSettingsPropertyFailedValidationException(string name, string value) : base(DataStrings.ConfigurationSettingsPropertyFailedValidation(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x0600084F RID: 2127 RVA: 0x0001B56A File Offset: 0x0001976A
		public ConfigurationSettingsPropertyFailedValidationException(string name, string value, Exception innerException) : base(DataStrings.ConfigurationSettingsPropertyFailedValidation(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000850 RID: 2128 RVA: 0x0001B588 File Offset: 0x00019788
		protected ConfigurationSettingsPropertyFailedValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x06000851 RID: 2129 RVA: 0x0001B5DD File Offset: 0x000197DD
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000852 RID: 2130 RVA: 0x0001B609 File Offset: 0x00019809
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002DE RID: 734
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x0001B611 File Offset: 0x00019811
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000594 RID: 1428
		private readonly string name;

		// Token: 0x04000595 RID: 1429
		private readonly string value;
	}
}
