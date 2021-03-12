using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B07 RID: 2823
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidParameterSyntaxException : SettingOverrideException
	{
		// Token: 0x060081D0 RID: 33232 RVA: 0x001A71E6 File Offset: 0x001A53E6
		public SettingOverrideInvalidParameterSyntaxException(string componentName, string sectionName, string parameter) : base(DirectoryStrings.ErrorSettingOverrideInvalidParameterSyntax(componentName, sectionName, parameter))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameter = parameter;
		}

		// Token: 0x060081D1 RID: 33233 RVA: 0x001A720B File Offset: 0x001A540B
		public SettingOverrideInvalidParameterSyntaxException(string componentName, string sectionName, string parameter, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidParameterSyntax(componentName, sectionName, parameter), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameter = parameter;
		}

		// Token: 0x060081D2 RID: 33234 RVA: 0x001A7234 File Offset: 0x001A5434
		protected SettingOverrideInvalidParameterSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.parameter = (string)info.GetValue("parameter", typeof(string));
		}

		// Token: 0x060081D3 RID: 33235 RVA: 0x001A72A9 File Offset: 0x001A54A9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("parameter", this.parameter);
		}

		// Token: 0x17002F0B RID: 12043
		// (get) Token: 0x060081D4 RID: 33236 RVA: 0x001A72E6 File Offset: 0x001A54E6
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F0C RID: 12044
		// (get) Token: 0x060081D5 RID: 33237 RVA: 0x001A72EE File Offset: 0x001A54EE
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F0D RID: 12045
		// (get) Token: 0x060081D6 RID: 33238 RVA: 0x001A72F6 File Offset: 0x001A54F6
		public string Parameter
		{
			get
			{
				return this.parameter;
			}
		}

		// Token: 0x040055E5 RID: 21989
		private readonly string componentName;

		// Token: 0x040055E6 RID: 21990
		private readonly string sectionName;

		// Token: 0x040055E7 RID: 21991
		private readonly string parameter;
	}
}
