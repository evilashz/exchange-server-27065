using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000AFB RID: 2811
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class ConfigurationSettingsDuplicateRestrictionException : ConfigurationSettingsException
	{
		// Token: 0x0600818F RID: 33167 RVA: 0x001A6A99 File Offset: 0x001A4C99
		public ConfigurationSettingsDuplicateRestrictionException(string name, string groupName) : base(DirectoryStrings.ConfigurationSettingsDuplicateRestriction(name, groupName))
		{
			this.name = name;
			this.groupName = groupName;
		}

		// Token: 0x06008190 RID: 33168 RVA: 0x001A6AB6 File Offset: 0x001A4CB6
		public ConfigurationSettingsDuplicateRestrictionException(string name, string groupName, Exception innerException) : base(DirectoryStrings.ConfigurationSettingsDuplicateRestriction(name, groupName), innerException)
		{
			this.name = name;
			this.groupName = groupName;
		}

		// Token: 0x06008191 RID: 33169 RVA: 0x001A6AD4 File Offset: 0x001A4CD4
		protected ConfigurationSettingsDuplicateRestrictionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.name = (string)info.GetValue("name", typeof(string));
			this.groupName = (string)info.GetValue("groupName", typeof(string));
		}

		// Token: 0x06008192 RID: 33170 RVA: 0x001A6B29 File Offset: 0x001A4D29
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("name", this.name);
			info.AddValue("groupName", this.groupName);
		}

		// Token: 0x17002EFA RID: 12026
		// (get) Token: 0x06008193 RID: 33171 RVA: 0x001A6B55 File Offset: 0x001A4D55
		public string Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x17002EFB RID: 12027
		// (get) Token: 0x06008194 RID: 33172 RVA: 0x001A6B5D File Offset: 0x001A4D5D
		public string GroupName
		{
			get
			{
				return this.groupName;
			}
		}

		// Token: 0x040055D4 RID: 21972
		private readonly string name;

		// Token: 0x040055D5 RID: 21973
		private readonly string groupName;
	}
}
