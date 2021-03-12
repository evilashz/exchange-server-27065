using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Monitoring
{
	// Token: 0x02000F11 RID: 3857
	[SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
	[Serializable]
	public class CasHealthCouldNotLogUserDataCenterNoDetailedInfoException : LocalizedException
	{
		// Token: 0x0600AA46 RID: 43590 RVA: 0x0028D2B6 File Offset: 0x0028B4B6
		public CasHealthCouldNotLogUserDataCenterNoDetailedInfoException(string testDomain, string scriptName) : base(Strings.CasHealthCouldNotLogUserDataCenterNoDetailedInfo(testDomain, scriptName))
		{
			this.testDomain = testDomain;
			this.scriptName = scriptName;
		}

		// Token: 0x0600AA47 RID: 43591 RVA: 0x0028D2D3 File Offset: 0x0028B4D3
		public CasHealthCouldNotLogUserDataCenterNoDetailedInfoException(string testDomain, string scriptName, Exception innerException) : base(Strings.CasHealthCouldNotLogUserDataCenterNoDetailedInfo(testDomain, scriptName), innerException)
		{
			this.testDomain = testDomain;
			this.scriptName = scriptName;
		}

		// Token: 0x0600AA48 RID: 43592 RVA: 0x0028D2F4 File Offset: 0x0028B4F4
		protected CasHealthCouldNotLogUserDataCenterNoDetailedInfoException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.testDomain = (string)info.GetValue("testDomain", typeof(string));
			this.scriptName = (string)info.GetValue("scriptName", typeof(string));
		}

		// Token: 0x0600AA49 RID: 43593 RVA: 0x0028D349 File Offset: 0x0028B549
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("testDomain", this.testDomain);
			info.AddValue("scriptName", this.scriptName);
		}

		// Token: 0x1700371F RID: 14111
		// (get) Token: 0x0600AA4A RID: 43594 RVA: 0x0028D375 File Offset: 0x0028B575
		public string TestDomain
		{
			get
			{
				return this.testDomain;
			}
		}

		// Token: 0x17003720 RID: 14112
		// (get) Token: 0x0600AA4B RID: 43595 RVA: 0x0028D37D File Offset: 0x0028B57D
		public string ScriptName
		{
			get
			{
				return this.scriptName;
			}
		}

		// Token: 0x04006085 RID: 24709
		private readonly string testDomain;

		// Token: 0x04006086 RID: 24710
		private readonly string scriptName;
	}
}
