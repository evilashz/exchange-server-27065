using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B06 RID: 2822
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidSectionNameException : SettingOverrideException
	{
		// Token: 0x060081C9 RID: 33225 RVA: 0x001A70CD File Offset: 0x001A52CD
		public SettingOverrideInvalidSectionNameException(string componentName, string sectionName, string availableObjects) : base(DirectoryStrings.ErrorSettingOverrideInvalidSectionName(componentName, sectionName, availableObjects))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.availableObjects = availableObjects;
		}

		// Token: 0x060081CA RID: 33226 RVA: 0x001A70F2 File Offset: 0x001A52F2
		public SettingOverrideInvalidSectionNameException(string componentName, string sectionName, string availableObjects, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidSectionName(componentName, sectionName, availableObjects), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.availableObjects = availableObjects;
		}

		// Token: 0x060081CB RID: 33227 RVA: 0x001A711C File Offset: 0x001A531C
		protected SettingOverrideInvalidSectionNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.availableObjects = (string)info.GetValue("availableObjects", typeof(string));
		}

		// Token: 0x060081CC RID: 33228 RVA: 0x001A7191 File Offset: 0x001A5391
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("availableObjects", this.availableObjects);
		}

		// Token: 0x17002F08 RID: 12040
		// (get) Token: 0x060081CD RID: 33229 RVA: 0x001A71CE File Offset: 0x001A53CE
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F09 RID: 12041
		// (get) Token: 0x060081CE RID: 33230 RVA: 0x001A71D6 File Offset: 0x001A53D6
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F0A RID: 12042
		// (get) Token: 0x060081CF RID: 33231 RVA: 0x001A71DE File Offset: 0x001A53DE
		public string AvailableObjects
		{
			get
			{
				return this.availableObjects;
			}
		}

		// Token: 0x040055E2 RID: 21986
		private readonly string componentName;

		// Token: 0x040055E3 RID: 21987
		private readonly string sectionName;

		// Token: 0x040055E4 RID: 21988
		private readonly string availableObjects;
	}
}
