using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0C RID: 2828
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideGenericException : SettingOverrideException
	{
		// Token: 0x060081F8 RID: 33272 RVA: 0x001A7919 File Offset: 0x001A5B19
		public SettingOverrideGenericException(string errorType, string componentName, string objectName, string parameters) : base(DirectoryStrings.ErrorSettingOverrideUnknown(errorType, componentName, objectName, parameters))
		{
			this.errorType = errorType;
			this.componentName = componentName;
			this.objectName = objectName;
			this.parameters = parameters;
		}

		// Token: 0x060081F9 RID: 33273 RVA: 0x001A7948 File Offset: 0x001A5B48
		public SettingOverrideGenericException(string errorType, string componentName, string objectName, string parameters, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideUnknown(errorType, componentName, objectName, parameters), innerException)
		{
			this.errorType = errorType;
			this.componentName = componentName;
			this.objectName = objectName;
			this.parameters = parameters;
		}

		// Token: 0x060081FA RID: 33274 RVA: 0x001A797C File Offset: 0x001A5B7C
		protected SettingOverrideGenericException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorType = (string)info.GetValue("errorType", typeof(string));
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.objectName = (string)info.GetValue("objectName", typeof(string));
			this.parameters = (string)info.GetValue("parameters", typeof(string));
		}

		// Token: 0x060081FB RID: 33275 RVA: 0x001A7A14 File Offset: 0x001A5C14
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorType", this.errorType);
			info.AddValue("componentName", this.componentName);
			info.AddValue("objectName", this.objectName);
			info.AddValue("parameters", this.parameters);
		}

		// Token: 0x17002F1F RID: 12063
		// (get) Token: 0x060081FC RID: 33276 RVA: 0x001A7A6D File Offset: 0x001A5C6D
		public string ErrorType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x17002F20 RID: 12064
		// (get) Token: 0x060081FD RID: 33277 RVA: 0x001A7A75 File Offset: 0x001A5C75
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F21 RID: 12065
		// (get) Token: 0x060081FE RID: 33278 RVA: 0x001A7A7D File Offset: 0x001A5C7D
		public string ObjectName
		{
			get
			{
				return this.objectName;
			}
		}

		// Token: 0x17002F22 RID: 12066
		// (get) Token: 0x060081FF RID: 33279 RVA: 0x001A7A85 File Offset: 0x001A5C85
		public string Parameters
		{
			get
			{
				return this.parameters;
			}
		}

		// Token: 0x040055F9 RID: 22009
		private readonly string errorType;

		// Token: 0x040055FA RID: 22010
		private readonly string componentName;

		// Token: 0x040055FB RID: 22011
		private readonly string objectName;

		// Token: 0x040055FC RID: 22012
		private readonly string parameters;
	}
}
