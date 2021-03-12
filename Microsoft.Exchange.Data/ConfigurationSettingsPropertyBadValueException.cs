using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000EF RID: 239
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsPropertyBadValueException : ConfigurationSettingsException
	{
		// Token: 0x06000848 RID: 2120 RVA: 0x0001B481 File Offset: 0x00019681
		public ConfigurationSettingsPropertyBadValueException(string name, string value) : base(DataStrings.ConfigurationSettingsPropertyBadValue(name, value))
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x06000849 RID: 2121 RVA: 0x0001B49E File Offset: 0x0001969E
		public ConfigurationSettingsPropertyBadValueException(string name, string value, Exception innerException) : base(DataStrings.ConfigurationSettingsPropertyBadValue(name, value), innerException)
		{
			this.name = name;
			this.value = value;
		}

		// Token: 0x0600084A RID: 2122 RVA: 0x0001B4BC File Offset: 0x000196BC
		protected ConfigurationSettingsPropertyBadValueException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.value = (string)info.GetValue("value", typeof(string));
		}

		// Token: 0x0600084B RID: 2123 RVA: 0x0001B511 File Offset: 0x00019711
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("value", this.value);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x0600084C RID: 2124 RVA: 0x0001B53D File Offset: 0x0001973D
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x0600084D RID: 2125 RVA: 0x0001B545 File Offset: 0x00019745
		public string Value
		{
			get
			{
				return this.value;
			}
		}

		// Token: 0x04000592 RID: 1426
		private readonly string name;

		// Token: 0x04000593 RID: 1427
		private readonly string value;
	}
}
