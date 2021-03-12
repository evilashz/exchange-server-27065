using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B00 RID: 2816
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsRestrictionExtraProperty : ConfigurationSettingsException
	{
		// Token: 0x060081AA RID: 33194 RVA: 0x001A6D99 File Offset: 0x001A4F99
		public ConfigurationSettingsRestrictionExtraProperty(string name, string propertyName) : base(DirectoryStrings.ConfigurationSettingsRestrictionExtraProperty(name, propertyName))
		{
			this.name = name;
			this.propertyName = propertyName;
		}

		// Token: 0x060081AB RID: 33195 RVA: 0x001A6DB6 File Offset: 0x001A4FB6
		public ConfigurationSettingsRestrictionExtraProperty(string name, string propertyName, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsRestrictionExtraProperty(name, propertyName), innerException)
		{
			this.name = name;
			this.propertyName = propertyName;
		}

		// Token: 0x060081AC RID: 33196 RVA: 0x001A6DD4 File Offset: 0x001A4FD4
		protected ConfigurationSettingsRestrictionExtraProperty(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.propertyName = (string)info.GetValue("propertyName", typeof(string));
		}

		// Token: 0x060081AD RID: 33197 RVA: 0x001A6E29 File Offset: 0x001A5029
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("propertyName", this.propertyName);
		}

		// Token: 0x17002F01 RID: 12033
		// (get) Token: 0x060081AE RID: 33198 RVA: 0x001A6E55 File Offset: 0x001A5055
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002F02 RID: 12034
		// (get) Token: 0x060081AF RID: 33199 RVA: 0x001A6E5D File Offset: 0x001A505D
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x040055DB RID: 21979
		private readonly string name;

		// Token: 0x040055DC RID: 21980
		private readonly string propertyName;
	}
}
