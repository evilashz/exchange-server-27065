using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFF RID: 2815
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsRestrictionMissingProperty : ConfigurationSettingsException
	{
		// Token: 0x060081A4 RID: 33188 RVA: 0x001A6CCD File Offset: 0x001A4ECD
		public ConfigurationSettingsRestrictionMissingProperty(string name, string propertyName) : base(DirectoryStrings.ConfigurationSettingsRestrictionMissingProperty(name, propertyName))
		{
			this.name = name;
			this.propertyName = propertyName;
		}

		// Token: 0x060081A5 RID: 33189 RVA: 0x001A6CEA File Offset: 0x001A4EEA
		public ConfigurationSettingsRestrictionMissingProperty(string name, string propertyName, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsRestrictionMissingProperty(name, propertyName), innerException)
		{
			this.name = name;
			this.propertyName = propertyName;
		}

		// Token: 0x060081A6 RID: 33190 RVA: 0x001A6D08 File Offset: 0x001A4F08
		protected ConfigurationSettingsRestrictionMissingProperty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060081A7 RID: 33191 RVA: 0x001A6D5D File Offset: 0x001A4F5D
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17002EFF RID: 12031
		// (get) Token: 0x060081A8 RID: 33192 RVA: 0x001A6D89 File Offset: 0x001A4F89
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002F00 RID: 12032
		// (get) Token: 0x060081A9 RID: 33193 RVA: 0x001A6D91 File Offset: 0x001A4F91
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040055D9 RID: 21977
		private readonly string name;

		// Token: 0x040055DA RID: 21978
		private readonly string propertyName;
	}
}
