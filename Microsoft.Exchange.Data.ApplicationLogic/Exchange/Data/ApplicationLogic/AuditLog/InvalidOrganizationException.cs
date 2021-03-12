using System;
using System.Text;
using Microsoft.Office.Compliance.Audit;

namespace Microsoft.Exchange.Data.ApplicationLogic.AuditLog
{
	// Token: 0x02000092 RID: 146
	public class InvalidOrganizationException : AuditException
	{
		// Token: 0x06000675 RID: 1653 RVA: 0x00017BC8 File Offset: 0x00015DC8
		public InvalidOrganizationException(string organization)
		{
			this.Organization = organization;
		}

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000676 RID: 1654 RVA: 0x00017BD7 File Offset: 0x00015DD7
		// (set) Token: 0x06000677 RID: 1655 RVA: 0x00017BDF File Offset: 0x00015DDF
		public string Organization { get; private set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x06000678 RID: 1656 RVA: 0x00017BE8 File Offset: 0x00015DE8
		public string DecodedOrganization
		{
			get
			{
				string result;
				try
				{
					byte[] bytes = Convert.FromBase64String(this.Organization);
					result = Encoding.UTF8.GetString(bytes);
				}
				catch (ArgumentException ex)
				{
					result = ex.Message;
				}
				catch (FormatException ex2)
				{
					result = ex2.Message;
				}
				return result;
			}
		}
	}
}
