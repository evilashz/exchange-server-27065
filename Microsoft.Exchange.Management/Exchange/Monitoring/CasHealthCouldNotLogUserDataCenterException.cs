using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F10 RID: 3856
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotLogUserDataCenterException : LocalizedException
	{
		// Token: 0x0600AA3F RID: 43583 RVA: 0x0028D19E File Offset: 0x0028B39E
		public CasHealthCouldNotLogUserDataCenterException(string testDomain, string scriptName, string errorString) : base(Strings.CasHealthCouldNotLogUserDataCenter(testDomain, scriptName, errorString))
		{
			this.testDomain = testDomain;
			this.scriptName = scriptName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA40 RID: 43584 RVA: 0x0028D1C3 File Offset: 0x0028B3C3
		public CasHealthCouldNotLogUserDataCenterException(string testDomain, string scriptName, string errorString, Exception innerException) : base(Strings.CasHealthCouldNotLogUserDataCenter(testDomain, scriptName, errorString), innerException)
		{
			this.testDomain = testDomain;
			this.scriptName = scriptName;
			this.errorString = errorString;
		}

		// Token: 0x0600AA41 RID: 43585 RVA: 0x0028D1EC File Offset: 0x0028B3EC
		protected CasHealthCouldNotLogUserDataCenterException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.testDomain = (string)info.GetValue("testDomain", typeof(string));
			this.scriptName = (string)info.GetValue("scriptName", typeof(string));
			this.errorString = (string)info.GetValue("errorString", typeof(string));
		}

		// Token: 0x0600AA42 RID: 43586 RVA: 0x0028D261 File Offset: 0x0028B461
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("testDomain", this.testDomain);
			info.AddValue("scriptName", this.scriptName);
			info.AddValue("errorString", this.errorString);
		}

		// Token: 0x1700371C RID: 14108
		// (get) Token: 0x0600AA43 RID: 43587 RVA: 0x0028D29E File Offset: 0x0028B49E
		public string TestDomain
		{
			get
			{
				return this.testDomain;
			}
		}

		// Token: 0x1700371D RID: 14109
		// (get) Token: 0x0600AA44 RID: 43588 RVA: 0x0028D2A6 File Offset: 0x0028B4A6
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x1700371E RID: 14110
		// (get) Token: 0x0600AA45 RID: 43589 RVA: 0x0028D2AE File Offset: 0x0028B4AE
		public string ErrorString
		{
			get
			{
				return this.errorString;
			}
		}

		// Token: 0x04006082 RID: 24706
		private readonly string testDomain;

		// Token: 0x04006083 RID: 24707
		private readonly string scriptName;

		// Token: 0x04006084 RID: 24708
		private readonly string errorString;
	}
}
