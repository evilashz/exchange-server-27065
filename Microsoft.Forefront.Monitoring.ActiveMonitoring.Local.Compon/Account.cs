using System;
using System.Xml;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200002E RID: 46
	public class Account
	{
		// Token: 0x06000147 RID: 327 RVA: 0x0000A2C4 File Offset: 0x000084C4
		public Account(string username, string password)
		{
			this.username = username;
			this.password = password;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000148 RID: 328 RVA: 0x0000A2DA File Offset: 0x000084DA
		public string Username
		{
			get
			{
				return this.username;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x06000149 RID: 329 RVA: 0x0000A2E2 File Offset: 0x000084E2
		public string Password
		{
			get
			{
				return this.password;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x0000A2EC File Offset: 0x000084EC
		public static Account FromXml(XmlNode workContext)
		{
			XmlElement xmlElement = workContext as XmlElement;
			string text = Utils.CheckNullOrWhiteSpace(xmlElement.GetAttribute("Username"), "Username");
			string attribute = xmlElement.GetAttribute("Password");
			return new Account(text, attribute);
		}

		// Token: 0x040000DB RID: 219
		private readonly string username;

		// Token: 0x040000DC RID: 220
		private readonly string password;
	}
}
