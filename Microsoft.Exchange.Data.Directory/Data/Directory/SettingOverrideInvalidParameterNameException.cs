using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B08 RID: 2824
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidParameterNameException : SettingOverrideException
	{
		// Token: 0x060081D7 RID: 33239 RVA: 0x001A72FE File Offset: 0x001A54FE
		public SettingOverrideInvalidParameterNameException(string componentName, string sectionName, string parameterName, string availableParameterNames) : base(DirectoryStrings.ErrorSettingOverrideInvalidParameterName(componentName, sectionName, parameterName, availableParameterNames))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameterName = parameterName;
			this.availableParameterNames = availableParameterNames;
		}

		// Token: 0x060081D8 RID: 33240 RVA: 0x001A732D File Offset: 0x001A552D
		public SettingOverrideInvalidParameterNameException(string componentName, string sectionName, string parameterName, string availableParameterNames, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidParameterName(componentName, sectionName, parameterName, availableParameterNames), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameterName = parameterName;
			this.availableParameterNames = availableParameterNames;
		}

		// Token: 0x060081D9 RID: 33241 RVA: 0x001A7360 File Offset: 0x001A5560
		protected SettingOverrideInvalidParameterNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.parameterName = (string)info.GetValue("parameterName", typeof(string));
			this.availableParameterNames = (string)info.GetValue("availableParameterNames", typeof(string));
		}

		// Token: 0x060081DA RID: 33242 RVA: 0x001A73F8 File Offset: 0x001A55F8
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("parameterName", this.parameterName);
			info.AddValue("availableParameterNames", this.availableParameterNames);
		}

		// Token: 0x17002F0E RID: 12046
		// (get) Token: 0x060081DB RID: 33243 RVA: 0x001A7451 File Offset: 0x001A5651
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F0F RID: 12047
		// (get) Token: 0x060081DC RID: 33244 RVA: 0x001A7459 File Offset: 0x001A5659
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F10 RID: 12048
		// (get) Token: 0x060081DD RID: 33245 RVA: 0x001A7461 File Offset: 0x001A5661
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x17002F11 RID: 12049
		// (get) Token: 0x060081DE RID: 33246 RVA: 0x001A7469 File Offset: 0x001A5669
		public string AvailableParameterNames
		{
			get
			{
				return this.availableParameterNames;
			}
		}

		// Token: 0x040055E8 RID: 21992
		private readonly string componentName;

		// Token: 0x040055E9 RID: 21993
		private readonly string sectionName;

		// Token: 0x040055EA RID: 21994
		private readonly string parameterName;

		// Token: 0x040055EB RID: 21995
		private readonly string availableParameterNames;
	}
}
