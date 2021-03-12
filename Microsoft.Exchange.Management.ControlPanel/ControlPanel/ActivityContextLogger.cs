using System;
using System.Collections.Generic;
using System.Web;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.WorkloadManagement;
using Microsoft.Exchange.Net.Protocols;
using Microsoft.Exchange.PowerShell.RbacHostingTools;

namespace Microsoft.Exchange.Management.ControlPanel
{
	// Token: 0x020001AA RID: 426
	internal sealed class ActivityContextLogger : ExtensibleLogger
	{
		// Token: 0x06002397 RID: 9111 RVA: 0x0006CE73 File Offset: 0x0006B073
		private ActivityContextLogger() : base(new ActivityContextLogConfiguration())
		{
		}

		// Token: 0x17001ADB RID: 6875
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0006CE80 File Offset: 0x0006B080
		public static ActivityContextLogger Instance
		{
			get
			{
				if (ActivityContextLogger.instance == null)
				{
					lock (ActivityContextLogger.syncRoot)
					{
						if (ActivityContextLogger.instance == null)
						{
							ActivityContext.RegisterMetadata(typeof(ActivityContextLoggerMetaData));
							ActivityContextLogger.instance = new ActivityContextLogger();
						}
					}
				}
				return ActivityContextLogger.instance;
			}
		}

		// Token: 0x06002399 RID: 9113 RVA: 0x0006CEE8 File Offset: 0x0006B0E8
		protected override ICollection<KeyValuePair<string, object>> GetComponentSpecificData(IActivityScope activityScope, string eventId)
		{
			Dictionary<string, object> dictionary = null;
			if (activityScope != null)
			{
				HttpContext httpContext = HttpContext.Current;
				if (httpContext != null)
				{
					string sourceCafeServer = CafeHelper.GetSourceCafeServer(httpContext.Request);
					if (!string.IsNullOrEmpty(sourceCafeServer))
					{
						activityScope.SetProperty(ActivityContextLoggerMetaData.FrontEndServer, sourceCafeServer);
					}
					string requestUrlForLog = httpContext.GetRequestUrlForLog();
					activityScope.SetProperty(ActivityContextLoggerMetaData.Url, requestUrlForLog);
				}
				RbacPrincipal current = RbacPrincipal.GetCurrent(false);
				if (current != null)
				{
					string value;
					if (current.RbacConfiguration.DelegatedPrincipal != null)
					{
						value = current.RbacConfiguration.DelegatedPrincipal.UserId;
					}
					else
					{
						SmtpAddress executingUserPrimarySmtpAddress = current.RbacConfiguration.ExecutingUserPrimarySmtpAddress;
						value = (executingUserPrimarySmtpAddress.IsValidAddress ? executingUserPrimarySmtpAddress.ToString() : current.RbacConfiguration.ExecutingUserPrincipalName);
					}
					if (!string.IsNullOrEmpty(value))
					{
						activityScope.SetProperty(ActivityContextLoggerMetaData.PrimarySmtpAddress, value);
					}
					OrganizationId organizationId = current.RbacConfiguration.OrganizationId;
					if (organizationId != null && organizationId.OrganizationalUnit != null)
					{
						string name = organizationId.OrganizationalUnit.Name;
						activityScope.SetProperty(ActivityContextLoggerMetaData.Organization, name);
					}
				}
				dictionary = new Dictionary<string, object>(ActivityContextLogger.EnumToShortKeyMapping.Count);
				ExtensibleLogger.CopyPIIProperty(activityScope, dictionary, ActivityContextLoggerMetaData.PrimarySmtpAddress, ActivityContextLogger.PrimarySmtpAddressKey);
				ExtensibleLogger.CopyProperties(activityScope, dictionary, ActivityContextLogger.EnumToShortKeyMapping);
			}
			return dictionary;
		}

		// Token: 0x04001E06 RID: 7686
		private static readonly Dictionary<Enum, string> EnumToShortKeyMapping = new Dictionary<Enum, string>
		{
			{
				ActivityContextLoggerMetaData.Organization,
				"ORG"
			},
			{
				ActivityContextLoggerMetaData.FrontEndServer,
				"FE"
			},
			{
				ActivityContextLoggerMetaData.Url,
				"URL"
			}
		};

		// Token: 0x04001E07 RID: 7687
		private static string PrimarySmtpAddressKey = "PSA";

		// Token: 0x04001E08 RID: 7688
		private static object syncRoot = new object();

		// Token: 0x04001E09 RID: 7689
		private static ActivityContextLogger instance;
	}
}
