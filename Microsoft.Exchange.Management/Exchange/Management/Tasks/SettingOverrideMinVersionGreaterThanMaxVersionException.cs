using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Directory;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02001195 RID: 4501
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideMinVersionGreaterThanMaxVersionException : SettingOverrideException
	{
		// Token: 0x0600B6E0 RID: 46816 RVA: 0x002A0A31 File Offset: 0x0029EC31
		public SettingOverrideMinVersionGreaterThanMaxVersionException(string minVersion, string maxVersion) : base(Strings.ErrorMinVersionGreaterThanMaxVersion(minVersion, maxVersion))
		{
			this.minVersion = minVersion;
			this.maxVersion = maxVersion;
		}

		// Token: 0x0600B6E1 RID: 46817 RVA: 0x002A0A4E File Offset: 0x0029EC4E
		public SettingOverrideMinVersionGreaterThanMaxVersionException(string minVersion, string maxVersion, Exception innerException) : base(Strings.ErrorMinVersionGreaterThanMaxVersion(minVersion, maxVersion), innerException)
		{
			this.minVersion = minVersion;
			this.maxVersion = maxVersion;
		}

		// Token: 0x0600B6E2 RID: 46818 RVA: 0x002A0A6C File Offset: 0x0029EC6C
		protected SettingOverrideMinVersionGreaterThanMaxVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.minVersion = (string)info.GetValue("minVersion", typeof(string));
			this.maxVersion = (string)info.GetValue("maxVersion", typeof(string));
		}

		// Token: 0x0600B6E3 RID: 46819 RVA: 0x002A0AC1 File Offset: 0x0029ECC1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("minVersion", this.minVersion);
			info.AddValue("maxVersion", this.maxVersion);
		}

		// Token: 0x170039A9 RID: 14761
		// (get) Token: 0x0600B6E4 RID: 46820 RVA: 0x002A0AED File Offset: 0x0029ECED
		public string MinVersion
		{
			get
			{
				return this.minVersion;
			}
		}

		// Token: 0x170039AA RID: 14762
		// (get) Token: 0x0600B6E5 RID: 46821 RVA: 0x002A0AF5 File Offset: 0x0029ECF5
		public string MaxVersion
		{
			get
			{
				return this.maxVersion;
			}
		}

		// Token: 0x0400630F RID: 25359
		private readonly string minVersion;

		// Token: 0x04006310 RID: 25360
		private readonly string maxVersion;
	}
}
