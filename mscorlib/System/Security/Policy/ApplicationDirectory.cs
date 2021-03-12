using System;
using System.Runtime.InteropServices;
using System.Security.Util;

namespace System.Security.Policy
{
	// Token: 0x02000314 RID: 788
	[ComVisible(true)]
	[Serializable]
	public sealed class ApplicationDirectory : EvidenceBase
	{
		// Token: 0x0600284A RID: 10314 RVA: 0x00094554 File Offset: 0x00092754
		public ApplicationDirectory(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.m_appDirectory = new URLString(name);
		}

		// Token: 0x0600284B RID: 10315 RVA: 0x00094576 File Offset: 0x00092776
		private ApplicationDirectory(URLString appDirectory)
		{
			this.m_appDirectory = appDirectory;
		}

		// Token: 0x17000551 RID: 1361
		// (get) Token: 0x0600284C RID: 10316 RVA: 0x00094585 File Offset: 0x00092785
		public string Directory
		{
			get
			{
				return this.m_appDirectory.ToString();
			}
		}

		// Token: 0x0600284D RID: 10317 RVA: 0x00094594 File Offset: 0x00092794
		public override bool Equals(object o)
		{
			ApplicationDirectory applicationDirectory = o as ApplicationDirectory;
			return applicationDirectory != null && this.m_appDirectory.Equals(applicationDirectory.m_appDirectory);
		}

		// Token: 0x0600284E RID: 10318 RVA: 0x000945BE File Offset: 0x000927BE
		public override int GetHashCode()
		{
			return this.m_appDirectory.GetHashCode();
		}

		// Token: 0x0600284F RID: 10319 RVA: 0x000945CB File Offset: 0x000927CB
		public override EvidenceBase Clone()
		{
			return new ApplicationDirectory(this.m_appDirectory);
		}

		// Token: 0x06002850 RID: 10320 RVA: 0x000945D8 File Offset: 0x000927D8
		public object Copy()
		{
			return this.Clone();
		}

		// Token: 0x06002851 RID: 10321 RVA: 0x000945E0 File Offset: 0x000927E0
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("System.Security.Policy.ApplicationDirectory");
			securityElement.AddAttribute("version", "1");
			if (this.m_appDirectory != null)
			{
				securityElement.AddChild(new SecurityElement("Directory", this.m_appDirectory.ToString()));
			}
			return securityElement;
		}

		// Token: 0x06002852 RID: 10322 RVA: 0x0009462C File Offset: 0x0009282C
		public override string ToString()
		{
			return this.ToXml().ToString();
		}

		// Token: 0x0400104E RID: 4174
		private URLString m_appDirectory;
	}
}
