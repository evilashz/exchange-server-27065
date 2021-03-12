using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace Microsoft.Exchange.Data.Directory
{
	// Token: 0x02000B0D RID: 2829
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class SettingOverrideUnexpectedException : SettingOverrideException
	{
		// Token: 0x06008200 RID: 33280 RVA: 0x001A7A8D File Offset: 0x001A5C8D
		public SettingOverrideUnexpectedException(string errorType) : base(DirectoryStrings.ErrorSettingOverrideUnexpected(errorType))
		{
			this.errorType = errorType;
		}

		// Token: 0x06008201 RID: 33281 RVA: 0x001A7AA2 File Offset: 0x001A5CA2
		public SettingOverrideUnexpectedException(string errorType, Exception innerException) : base(DirectoryStrings.ErrorSettingOverrideUnexpected(errorType), innerException)
		{
			this.errorType = errorType;
		}

		// Token: 0x06008202 RID: 33282 RVA: 0x001A7AB8 File Offset: 0x001A5CB8
		protected SettingOverrideUnexpectedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.errorType = (string)info.GetValue("errorType", typeof(string));
		}

		// Token: 0x06008203 RID: 33283 RVA: 0x001A7AE2 File Offset: 0x001A5CE2
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("errorType", this.errorType);
		}

		// Token: 0x17002F23 RID: 12067
		// (get) Token: 0x06008204 RID: 33284 RVA: 0x001A7AFD File Offset: 0x001A5CFD
		public string ErrorType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x040055FD RID: 22013
		private readonly string errorType;
	}
}
