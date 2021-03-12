using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data
{
	// Token: 0x020000F1 RID: 241
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsPropertyNotFoundException : ConfigurationSettingsException
	{
		// Token: 0x06000854 RID: 2132 RVA: 0x0001B619 File Offset: 0x00019819
		public ConfigurationSettingsPropertyNotFoundException(string name, string knownProperties) : base(DataStrings.ConfigurationSettingsPropertyNotFound2(name, knownProperties))
		{
			this.name = name;
			this.knownProperties = knownProperties;
		}

		// Token: 0x06000855 RID: 2133 RVA: 0x0001B636 File Offset: 0x00019836
		public ConfigurationSettingsPropertyNotFoundException(string name, string knownProperties, Exception innerException) : base(DataStrings.ConfigurationSettingsPropertyNotFound2(name, knownProperties), innerException)
		{
			this.name = name;
			this.knownProperties = knownProperties;
		}

		// Token: 0x06000856 RID: 2134 RVA: 0x0001B654 File Offset: 0x00019854
		protected ConfigurationSettingsPropertyNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.knownProperties = (string)info.GetValue("knownProperties", typeof(string));
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x0001B6A9 File Offset: 0x000198A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("knownProperties", this.knownProperties);
		}

		// Token: 0x170002DF RID: 735
		// (get) Token: 0x06000858 RID: 2136 RVA: 0x0001B6D5 File Offset: 0x000198D5
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x170002E0 RID: 736
		// (get) Token: 0x06000859 RID: 2137 RVA: 0x0001B6DD File Offset: 0x000198DD
		public string KnownProperties
		{
			get
			{
				return this.knownProperties;
			}
		}

		// Token: 0x04000596 RID: 1430
		private readonly string name;

		// Token: 0x04000597 RID: 1431
		private readonly string knownProperties;
	}
}
