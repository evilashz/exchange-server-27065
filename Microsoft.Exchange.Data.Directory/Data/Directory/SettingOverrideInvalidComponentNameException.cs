using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B04 RID: 2820
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideInvalidComponentNameException : SettingOverrideException
	{
		// Token: 0x060081BD RID: 33213 RVA: 0x001A6F33 File Offset: 0x001A5133
		public SettingOverrideInvalidComponentNameException(string componentName, string availableComponents) : base(DirectoryStrings.ErrorSettingOverrideInvalidComponentName(componentName, availableComponents))
		{
			this.componentName = componentName;
			this.availableComponents = availableComponents;
		}

		// Token: 0x060081BE RID: 33214 RVA: 0x001A6F50 File Offset: 0x001A5150
		public SettingOverrideInvalidComponentNameException(string componentName, string availableComponents, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideInvalidComponentName(componentName, availableComponents), innerException)
		{
			this.componentName = componentName;
			this.availableComponents = availableComponents;
		}

		// Token: 0x060081BF RID: 33215 RVA: 0x001A6F70 File Offset: 0x001A5170
		protected SettingOverrideInvalidComponentNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.componentName = (string)info.GetValue("componentName", typeof(string));
			this.availableComponents = (string)info.GetValue("availableComponents", typeof(string));
		}

		// Token: 0x060081C0 RID: 33216 RVA: 0x001A6FC5 File Offset: 0x001A51C5
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("componentName", this.componentName);
			info.AddValue("availableComponents", this.availableComponents);
		}

		// Token: 0x17002F04 RID: 12036
		// (get) Token: 0x060081C1 RID: 33217 RVA: 0x001A6FF1 File Offset: 0x001A51F1
		public string ComponentName
		{
			get
			{
				return this.componentName;
			}
		}

		// Token: 0x17002F05 RID: 12037
		// (get) Token: 0x060081C2 RID: 33218 RVA: 0x001A6FF9 File Offset: 0x001A51F9
		public string AvailableComponents
		{
			get
			{
				return this.availableComponents;
			}
		}

		// Token: 0x040055DE RID: 21982
		private readonly string componentName;

		// Token: 0x040055DF RID: 21983
		private readonly string availableComponents;
	}
}
