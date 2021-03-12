using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Web;
using Microsoft.Exchange.Configuration.Authorization;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Management.ControlPanel.WebControls;
using Microsoft.Exchange.PowerShell.RbacHostingTools;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.CsmSdk;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x02000249 RID: 585
	public class DiagnosticsPage : EcpContentPage
	{
		// Token: 0x06002875 RID: 10357 RVA: 0x0007F587 File Offset: 0x0007D787
		protected void RenderTitle(string title)
		{
			base.Response.Output.Write("<div class='diagTitle'>{0}</div>", title);
		}

		// Token: 0x06002876 RID: 10358 RVA: 0x0007F5A0 File Offset: 0x0007D7A0
		protected void RenderGeneralInformation()
		{
			base.Response.Write("<div class='diagBlock'>");
			LocalSession localSession = LocalSession.Current;
			ExchangeRunspaceConfiguration rbacConfiguration = localSession.RbacConfiguration;
			bool flag = localSession.IsInRole("Mailbox+MailboxFullAccess");
			this.Write("Logon user:", string.Format("{0} [{1}]", localSession.Name, rbacConfiguration.ExecutingUserPrimarySmtpAddress));
			this.Write("User-Agent:", HttpContext.Current.Request.UserAgent);
			this.Write("SKU:", Util.IsDataCenter ? "DataCenter" : "On-Premise");
			if (flag)
			{
				this.Write("Mailbox server version:", localSession.Context.MailboxServerVersion);
			}
			else
			{
				this.Write("Mailbox account:", "None mailbox account.");
			}
			this.Write("Current server version:", Util.ApplicationVersion);
			this.Write("Request URL:", HttpContext.Current.GetRequestUrl().ToString());
			this.Write("Display language:", CultureInfo.CurrentCulture.IetfLanguageTag);
			if (flag)
			{
				this.Write("User time zone:", (localSession.UserTimeZone != null) ? localSession.UserTimeZone.DisplayName : "Not set.");
			}
			this.Write("RBAC roles:", this.GetRoles(localSession));
			this.Write("Features:", FlightProvider.Instance.GetAllEnabledFeatures().ToLogString<string>());
			VariantConfigurationSnapshot snapshotForCurrentUser = EacFlightUtility.GetSnapshotForCurrentUser();
			this.Write("Flights:", snapshotForCurrentUser.Flights.ToLogString<string>());
			base.Response.Write("</div>");
		}

		// Token: 0x06002877 RID: 10359 RVA: 0x0007F720 File Offset: 0x0007D920
		private string GetRoles(RbacPrincipal rbacPrincipal)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (object obj in Enum.GetValues(typeof(RoleType)))
			{
				RoleType roleType = (RoleType)obj;
				string text = roleType.ToString();
				if (rbacPrincipal.IsInRole(text))
				{
					if (flag)
					{
						flag = false;
					}
					else
					{
						stringBuilder.Append(", ");
					}
					stringBuilder.Append(text);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002878 RID: 10360 RVA: 0x0007F7C0 File Offset: 0x0007D9C0
		protected void Write(object content)
		{
			this.Write(null, content);
		}

		// Token: 0x06002879 RID: 10361 RVA: 0x0007F7CA File Offset: 0x0007D9CA
		protected void Write(string label, object content)
		{
			DiagnosticsPage.Write(base.Response.Output, label, content, null);
		}

		// Token: 0x0600287A RID: 10362 RVA: 0x0007F7E0 File Offset: 0x0007D9E0
		internal static void Write(TextWriter output, string label, object content, string format = null)
		{
			if (format == null)
			{
				format = ((label == null) ? "<div class='diagLn'><span class='diagTxt'>{0}</span></div>" : "<div class='diagLn'><span class='diagLbl'>{0}</span> <span class='diagTxt'>{1}</span></div>");
			}
			label = ((label == null) ? string.Empty : HttpUtility.HtmlEncode(label));
			string arg = (content == null) ? string.Empty : HttpUtility.HtmlEncode(content.ToString());
			output.Write(format ?? "<div class='diagLn'><span class='diagLbl'>{0}</span> <span class='diagTxt'>{1}</span></div>", label, arg);
		}

		// Token: 0x04002060 RID: 8288
		private const string TitleFormat = "<div class='diagTitle'>{0}</div>";

		// Token: 0x04002061 RID: 8289
		protected const string BlockStart = "<div class='diagBlock'>";

		// Token: 0x04002062 RID: 8290
		protected const string BlockEnd = "</div>";

		// Token: 0x04002063 RID: 8291
		private const string LabelAndContentFormat = "<div class='diagLn'><span class='diagLbl'>{0}</span> <span class='diagTxt'>{1}</span></div>";

		// Token: 0x04002064 RID: 8292
		private const string ContentFormat = "<div class='diagLn'><span class='diagTxt'>{0}</span></div>";
	}
}
