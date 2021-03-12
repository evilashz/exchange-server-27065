using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0B RID: 2827
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideSyntaxException : SettingOverrideException
	{
		// Token: 0x060081F0 RID: 33264 RVA: 0x001A77A6 File Offset: 0x001A59A6
		public SettingOverrideSyntaxException(string componentName, string sectionName, string parameters, string error) : base(DirectoryStrings.ErrorSettingOverrideSyntax(componentName, sectionName, parameters, error))
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameters = parameters;
			this.error = error;
		}

		// Token: 0x060081F1 RID: 33265 RVA: 0x001A77D5 File Offset: 0x001A59D5
		public SettingOverrideSyntaxException(string componentName, string sectionName, string parameters, string error, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideSyntax(componentName, sectionName, parameters, error), innerException)
		{
			this.componentName = componentName;
			this.sectionName = sectionName;
			this.parameters = parameters;
			this.error = error;
		}

		// Token: 0x060081F2 RID: 33266 RVA: 0x001A7808 File Offset: 0x001A5A08
		protected SettingOverrideSyntaxException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.sectionName = (string)info.GetValue("sectionName", typeof(string));
			this.parameters = (string)info.GetValue("parameters", typeof(string));
			this.error = (string)info.GetValue("error", typeof(string));
		}

		// Token: 0x060081F3 RID: 33267 RVA: 0x001A78A0 File Offset: 0x001A5AA0
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("sectionName", this.sectionName);
			info.AddValue("parameters", this.parameters);
			info.AddValue("error", this.error);
		}

		// Token: 0x17002F1B RID: 12059
		// (get) Token: 0x060081F4 RID: 33268 RVA: 0x001A78F9 File Offset: 0x001A5AF9
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F1C RID: 12060
		// (get) Token: 0x060081F5 RID: 33269 RVA: 0x001A7901 File Offset: 0x001A5B01
		public string SectionName
		{
			get
			{
				return this.sectionName;
			}
		}

		// Token: 0x17002F1D RID: 12061
		// (get) Token: 0x060081F6 RID: 33270 RVA: 0x001A7909 File Offset: 0x001A5B09
		public string Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x17002F1E RID: 12062
		// (get) Token: 0x060081F7 RID: 33271 RVA: 0x001A7911 File Offset: 0x001A5B11
		public string Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x040055F5 RID: 22005
		private readonly string componentName;

		// Token: 0x040055F6 RID: 22006
		private readonly string sectionName;

		// Token: 0x040055F7 RID: 22007
		private readonly string parameters;

		// Token: 0x040055F8 RID: 22008
		private readonly string error;
	}
}
